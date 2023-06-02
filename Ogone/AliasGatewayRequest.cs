using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogone
{
    public class AliasGatewayRequest : Request
    {
        public AliasGatewayRequest(SHA sha, string shaInSignature, string pspID, string orderID)
            : this(sha, shaInSignature, pspID, orderID, Encoding.UTF8, Environment.Test)
        {
        }

        public AliasGatewayRequest(SHA sha, string shaInSignature, string pspID, string orderID, Encoding encoding, Environment environment)
        {
            if (string.IsNullOrWhiteSpace(shaInSignature))
            {
                throw new ArgumentException("SHA-IN is required");
            }

            if (string.IsNullOrWhiteSpace(pspID))
            {
                throw new ArgumentException("PSPID is required");
            }
            

            this._sha = sha;
            this._shaInSignature = shaInSignature;
            this._pspID = pspID;
            this._orderID = orderID;
            extrafields = new Dictionary<InFields, string>();

            this.Encoding = encoding;
            this.Environment = environment;
        }

        public override string OgoneUrl
        {
            get
            {
                if (this.Encoding == Ogone.Encoding.ISO_8859_1)
                {
                    return string.Format("https://secure.ogone.com/ncol/{0}/alias_gateway.asp", this.Environment == Environment.Production ? "prod" : "test");
                }
                else
                {
                    return string.Format("https://secure.ogone.com/ncol/{0}/alias_gateway_utf8.asp", this.Environment == Environment.Production ? "prod" : "test");
                }
            }
        }

        public string Alias
        {
            get;set;
        }
        public string Brand
        {
            get;set;
        }

        public string CardNo
        {
            get;set;
        }

        public string Cvc { get; set; }
        
        public bool AliasPersistedAfterUse { get; set; }
        public string ExpirationDate { get; set; }
        protected override IDictionary<string, string> GetAllParameters()
        {
            
            IDictionary<string, string> allParameters = base.GetAllParameters();
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            allParameters.Add("ALIAS", Alias);
            allParameters.Add("ALIASPERSISTEDAFTERUSE", AliasPersistedAfterUse ? "Y" : "N");
            allParameters.Add("BRAND", Brand);
            allParameters.Add("CARDNO", CardNo);
            allParameters.Add("CVC", Cvc);
            allParameters.Add("ED", ExpirationDate);
            allParameters.Remove("AMOUNT");
            allParameters.Remove("CURRENCY");
            allParameters.Remove("SHASIGN");
            foreach (string key in allParameters.Where(ap => !string.IsNullOrWhiteSpace(ap.Value)).OrderBy(ap => ap.Key).Select(ap => ap.Key))
            {
                parameters.Add(key, allParameters[key]);
            }

            return parameters;
        }
    }
}
