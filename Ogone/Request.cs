using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogone
{
    /// <summary>
    /// An order to send to Ogone
    /// </summary>
    public class Request
    {
        protected SHA _sha;
        protected string _shaInSignature;
        protected string _pspID;
        protected string _orderID;
        protected decimal _price;
        protected Language _language = Language.en_US;
        protected Currency _currency = Currency.EUR;
        protected IDictionary<InFields, string> extrafields;

        protected Request()
        {

        }
        public Request(SHA sha, string shaInSignature, string pspID, string orderID, decimal price)
            : this(sha, shaInSignature, pspID, orderID, price, Encoding.UTF8, Environment.Test)
        {
        }

        public Request(SHA sha, string shaInSignature, string pspID, string orderID, decimal price, Encoding encoding, Environment environment)
        {
            if (string.IsNullOrWhiteSpace(shaInSignature))
            {
                throw new ArgumentException("SHA-IN is required");
            }

            if (string.IsNullOrWhiteSpace(pspID))
            {
                throw new ArgumentException("PSPID is required");
            }

            if (string.IsNullOrEmpty(orderID))
            {
                throw new ArgumentException("Invalid Order ID");
            }

            if (price <= 0)
            {
                throw new ArgumentException("Invalid price");
            }

            this._sha = sha;
            this._shaInSignature = shaInSignature;
            this._pspID = pspID;
            this._orderID = orderID;
            this._price = price;
            extrafields = new Dictionary<InFields, string>();

            this.Encoding = encoding;
            this.Environment = environment;
        }

        public SHA SHA
        {
            get
            {
                return _sha;
            }
        }

        public string SHAInSignature
        {
            get
            {
                return _shaInSignature;
            }
        }

        public string PSPID
        {
            get
            {
                return _pspID;
            }
        }

        public string OrderID
        {
            get
            {
                return _orderID;
            }
        }

        public int Price
        {
            get
            {
                return Convert.ToInt32(Math.Floor(_price * 100));
            }
        }

        public Currency Currency
        {
            get
            {
                return _currency;
            }
            set
            {
                _currency = value;
            }
        }

        public Language Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
            }
        }

        public string? CustomerID
        {
            get;
            set;
        }

        public string? CustomerName
        {
            get;
            set;
        }

        public string? CustomerEMail
        {
            get;
            set;
        }

        public string? CustomerAddress
        {
            get;
            set;
        }

        public string? CustomerCity
        {
            get;
            set;
        }

        public string? CustomerZipcode
        {
            get;
            set;
        }

        public string? CustomerCountryCode
        {
            get;
            set;
        }

        public string? Logo
        {
            get;
            set;
        }

        public string? HomeURL
        {
            get;
            set;
        }

        public string? BackURL
        {
            get;
            set;
        }

        public string? CancelURL
        {
            get;
            set;
        }

        public string? AcceptURL
        {
            get;
            set;
        }

        public string? DeclineURL
        {
            get;
            set;
        }

        public string? ExceptionURL
        {
            get;
            set;
        }

        public Environment Environment
        {
            get;
            set;
        } = Environment.Production;

        public Encoding Encoding
        {
            get;
            set;
        } = Encoding.UTF8;

        public virtual string OgoneUrl
        {
            get
            {
                if (this.Environment == Environment.Production)
                {
                    if (this.Encoding == Ogone.Encoding.ISO_8859_1)
                    {
                        return "https://secure.ogone.com/ncol/prod/orderstandard.asp";
                    }
                    else
                    {
                        return "https://secure.ogone.com/ncol/prod/orderstandard_utf8.asp";
                    }
                }
                else
                {
                    if (this.Encoding == Ogone.Encoding.ISO_8859_1)
                    {
                        return "https://secure.ogone.com/ncol/test/orderstandard.asp";
                    }
                    else
                    {
                        return "https://secure.ogone.com/ncol/test/orderstandard_utf8.asp";
                    }
                }
            }
        }

        /// <summary>
        /// Get a dictionary with all Ogone parameters that were filled in and ordered by key
        /// </summary>
        /// <returns></returns>
        protected virtual IDictionary<string, string> GetAllParameters()
        {
            IDictionary<string, string> allParameters = new Dictionary<string, string>();
            IDictionary<string, string> parameters = new Dictionary<string, string>();

            allParameters.Add("PSPID", PSPID);
            allParameters.Add("ORDERID", OrderID.ToString());
            allParameters.Add("AMOUNT", Price.ToString());
            allParameters.Add("CURRENCY", Currency.ToString());
            allParameters.Add("LANGUAGE", Language.ToString());
            allParameters.Add("COMPLUS", CustomerID ?? string.Empty);
            allParameters.Add("CN", CustomerName ?? string.Empty);
            allParameters.Add("EMAIL", CustomerEMail ?? string.Empty);
            allParameters.Add("OWNERADDRESS", CustomerAddress ?? string.Empty);
            allParameters.Add("OWNERTOWN", CustomerCity ?? string.Empty);
            allParameters.Add("OWNERZIP", CustomerZipcode ?? string.Empty);
            allParameters.Add("OWNERCTY", CustomerCountryCode ?? string.Empty);
            allParameters.Add("LOGO", Logo ?? string.Empty);
            allParameters.Add("HOMEURL", HomeURL ?? string.Empty);
            allParameters.Add("BACKURL", BackURL ?? string.Empty);
            allParameters.Add("CANCELURL", CancelURL ?? string.Empty);
            allParameters.Add("ACCEPTURL", AcceptURL ?? string.Empty);
            allParameters.Add("DECLINEURL", DeclineURL ?? string.Empty);
            allParameters.Add("EXCEPTIONURL", ExceptionURL ?? string.Empty);

            foreach (KeyValuePair<InFields, string> extrafield in extrafields)
            {
                allParameters.Add(extrafield.Key.ToString(), extrafield.Value);
            }

            foreach (string key in allParameters.Where(ap => !string.IsNullOrWhiteSpace(ap.Value)).OrderBy(ap => ap.Key).Select(ap => ap.Key))
            {
                parameters.Add(key, allParameters[key]);
            }

            return parameters;
        }

        public string SHAOrder
        {
            get
            {
                string result = null;

                IDictionary<string, string> parameters = GetAllParameters();
                StringBuilder sbHashString = new StringBuilder();
                foreach (KeyValuePair<string, string> item in parameters)
                {
                    sbHashString.Append(item.Key.Replace("_XX_","*XX*")  + "=" + item.Value + SHAInSignature);
                }

                result = HashString.GenerateHash(sbHashString.ToString(), this.Encoding, this.SHA);

                return result;
            }
        }

        /// <summary>
        /// A list with the parameters and their values to add to the post to Ogone
        /// </summary>
        public IDictionary<string, string> ParametersToSend
        {
            get
            {
                IDictionary<string, string> result = new Dictionary<string, string>();

                foreach (KeyValuePair<string, string> item in GetAllParameters())
                {
                    result.Add(item);
                }
                result.Add("SHASIGN", SHAOrder);

                return result;
            }
        }

        /// <summary>
        /// Add a supplementary field from one of the allowed fields
        /// </summary>
        /// <param name="key">Parametername for Ogone</param>
        /// <param name="value">The value of the parameter</param>
        public void AddField(InFields key, string value)
        {
            if (!extrafields.ContainsKey(key) && !string.IsNullOrWhiteSpace(value))
            {
                extrafields.Add(key, value);
            }
        }

        /// <summary>
        /// Remove a field from the added extra supplementary fields
        /// </summary>
        /// <param name="key">Parametername for Ogone</param>
        public void RemoveField(InFields key)
        {
            if (extrafields.ContainsKey(key))
            {
                extrafields.Remove(key);
            }
        }

        /// <summary>
        /// Get a value of a parameter if it already was added
        /// </summary>
        /// <param name="key">Parametername for Ogone</param>
        /// <returns>Null when not found, otherwise the corresponding value</returns>
        public string GetField(InFields key)
        {
            string result = null;
            if (extrafields.ContainsKey(key))
            {
                result = extrafields[key];
            }
            return result;
        }

        public string RenderHtmlForm(string submitButtonText)
        {
            var result = new StringBuilder();

            result.AppendLine("<form action=\"" + this.OgoneUrl + "\" method=\"post\">");

            foreach (var item in this.ParametersToSend)
            {
                result.AppendLine("\t<input type=\"hidden\" name=\"" + item.Key + "\" value=\"" + item.Value + "\" />");
            }
            result.AppendLine(@"
        <input type=""text""dxwcfdscss name=""CVC"" value=""123""/>
        <input type=""text"" name=""CARDNO"" value=""4111111111111111"" />
        <input type=""text"" name=""ED"" value=""1020"" />
        <input type=""text"" name=""CN"" value=""T. Ester"" />");
            result.AppendLine("\t<input type=\"submit\" value=\"" + submitButtonText + "\" />");
            result.AppendLine("</form>");


            return result.ToString();
        }
    }

}
