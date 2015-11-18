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
        private SHA _sha;
        private string _shaInKey;
        private string _pspID;
        private int _orderID;
        private decimal _price;
        private Language _language = Language.en_US;
        private Currency _currency = Currency.EUR;
        private IDictionary<InFields, string> extrafields;

        public Request(SHA sha, string shaInKey, string pspID, int orderID, decimal price)
        {

            if (string.IsNullOrWhiteSpace(shaInKey))
            {
                throw new ArgumentException("SHA-IN is required");
            }

            if (string.IsNullOrWhiteSpace(pspID))
            {
                throw new ArgumentException("PSPID is required");
            }

            if (orderID < 1)
            {
                throw new ArgumentException("Invalid Order ID");
            }

            if (price <= 0)
            {
                throw new ArgumentException("Invalid price");
            }

            this._sha = sha;
            this._shaInKey = shaInKey;
            this._pspID = pspID;
            this._orderID = orderID;
            this._price = price;
            extrafields = new Dictionary<InFields, string>();
        }

        public SHA SHA
        {
            get
            {
                return _sha;
            }
        }

        public string SHAOrderKey
        {
            get
            {
                return _shaInKey;
            }
        }

        public string PSPID
        {
            get
            {
                return _pspID;
            }
        }

        public int OrderID
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

        public string CustomerID
        {
            get;
            set;
        }

        public string CustomerName
        {
            get;
            set;
        }

        public string CustomerEMail
        {
            get;
            set;
        }

        public string CustomerAddress
        {
            get;
            set;
        }

        public string CustomerCity
        {
            get;
            set;
        }

        public string CustomerZipcode
        {
            get;
            set;
        }

        public string CustomerCountryCode
        {
            get;
            set;
        }

        public string Logo
        {
            get;
            set;
        }

        public string HomeURL
        {
            get;
            set;
        }

        public string BackURL
        {
            get;
            set;
        }

        public string CancelURL
        {
            get;
            set;
        }

        public string AcceptURL
        {
            get;
            set;
        }

        public string DeclineURL
        {
            get;
            set;
        }

        public string ExceptionURL
        {
            get;
            set;
        }

        /// <summary>
        /// Get a dictionary with all Ogone parameters that were filled in and ordered by key
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> GetAllParameters()
        {
            IDictionary<string, string> allParameters = new Dictionary<string, string>();
            IDictionary<string, string> parameters = new Dictionary<string, string>();

            allParameters.Add("PSPID", PSPID);
            allParameters.Add("ORDERID", OrderID.ToString());
            allParameters.Add("AMOUNT", Price.ToString());
            allParameters.Add("CURRENCY", Currency.ToString());
            allParameters.Add("LANGUAGE", Language.ToString());
            allParameters.Add("COMPLUS", CustomerID.ToString());
            allParameters.Add("CN", CustomerName);
            allParameters.Add("EMAIL", CustomerEMail);
            allParameters.Add("OWNERADDRESS", CustomerAddress);
            allParameters.Add("OWNERTOWN", CustomerCity);
            allParameters.Add("OWNERZIP", CustomerZipcode);
            allParameters.Add("OWNERCTY", CustomerCountryCode);
            allParameters.Add("LOGO", Logo);
            allParameters.Add("HOMEURL", HomeURL);
            allParameters.Add("BACKURL", BackURL);
            allParameters.Add("CANCELURL", CancelURL);
            allParameters.Add("ACCEPTURL", AcceptURL);
            allParameters.Add("DECLINEURL", DeclineURL);
            allParameters.Add("EXCEPTIONURL", ExceptionURL);

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
                    sbHashString.Append(item.Key.Replace("_XX_","*XX*")  + "=" + item.Value + SHAOrderKey);
                }

                result = HashString.GenerateHash(sbHashString.ToString(), new UTF8Encoding(), _sha);

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

    }

}
