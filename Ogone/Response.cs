using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogone
{

    /// <summary>
    /// A response received from Ogone
    /// </summary>
    public class Response
    {
        private string _shaSign;
        private SHA _sha;
        private string _shaAcceptKey;
        private IDictionary<OutFields, string> fields;

        public Response(string shaSign, SHA sha, string shaAcceptKey)
        {

            if (string.IsNullOrWhiteSpace(shaSign) || string.IsNullOrWhiteSpace(shaAcceptKey))
            {
                throw new ArgumentException("SHA-key's and ID are required");
            }

            this._shaSign = shaSign;
            this._sha = sha;
            this._shaAcceptKey = shaAcceptKey;
            fields = new Dictionary<OutFields, string>();
        }

        public string SHASign
        {
            get
            {
                return _shaSign;
            }
        }

        public SHA SHA
        {
            get
            {
                return _sha;
            }
        }

        public string SHAAcceptKey
        {
            get
            {
                return _shaAcceptKey;
            }
        }

        public string SHAAccept
        {
            get
            {
                string result = null;

                if (fields.Count > 0)
                {
                    StringBuilder texttohash = new StringBuilder();
                    foreach (KeyValuePair<OutFields, string> parameter in fields.OrderBy(f => f.Key.ToString()))
                    {
                        texttohash.Append(parameter.Key.ToString() + "=" + parameter.Value + SHAAcceptKey);
                    }

                    result = HashString.GenerateHash(texttohash.ToString(), new UTF8Encoding(), SHA);
                }

                return result;
            }
        }

        /// <summary>
        /// Check if the read data and the check sign are equal (case insensitive), to validate the response
        /// </summary>
        public bool IsCorrect
        {
            get
            {
                return SHASign.ToLower().Equals(SHAAccept.ToLower());
            }
        }

        /// <summary>
        /// Add a parameter to the response
        /// </summary>
        /// <param name="key">Name of the parameter from Ogone</param>
        /// <param name="value">The value of the parameter</param>
        public void AddField(OutFields key, string value)
        {
            if (!fields.ContainsKey(key) && !string.IsNullOrWhiteSpace(value))
            {
                fields.Add(key, value);
            }
        }

        /// <summary>
        /// Add a parameter to the response
        /// </summary>
        /// <param name="key">Name of the parameter from Ogone</param>
        /// <param name="value">The value of the parameter</param>
        public void AddField(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = key.ToUpper();
                if (Enum.IsDefined(typeof(OutFields), key))
                {
                    AddField((OutFields)Enum.Parse(typeof(OutFields), key), value);
                }
            }
        }

        /// <summary>
        /// Remove a parameter from the response
        /// </summary>
        /// <param name="key">Name of the parameter from Ogone</param>
        public void RemoveField(OutFields key)
        {
            if (fields.ContainsKey(key))
            {
                fields.Remove(key);
            }
        }

        /// <summary>
        /// Get a value of a parameter if it already was added
        /// </summary>
        /// <param name="key">Name of the parameter from Ogone</param>
        /// <returns>Null when not found, otherwise the corresponding value</returns>
        public string GetField(OutFields key)
        {
            string result = null;
            if (fields.ContainsKey(key))
            {
                result = fields[key];
            }
            return result;
        }

    }
}
