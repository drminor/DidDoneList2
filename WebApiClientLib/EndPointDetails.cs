using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiClientLib
{
    public class EndPointDetails
    {

        ////http://192.168.1.125:8080/api/customers/2

        //public const string BASE_URI_HOST = "192.168.1.125";
        //public const int BASE_URI_PORT_NUMBER = 8080;

        ////public const string BASE_URI_HOST = "localhost";
        ////public const int BASE_URI_PORT_NUMBER = 44367;

        //public const string CUSTOMER_END_POINT = "api/Customers";

        #region Private Properties
        Dictionary<string, string> _endPointAddresses;

        public EndPointDetails(Dictionary<string, string> endPointAddresses, string hostName)
            : this(endPointAddresses, hostName, -1)
        {
        }

        public EndPointDetails(Dictionary<string, string> endPointAddresses, string hostName, int portNumber)
        {
            if (endPointAddresses == null)
            {
                // Create a new, empty dictionary of end point adddresses.
                _endPointAddresses = new Dictionary<string, string>();
            }
            else
            {
                // Create a new Dictionary using values copied from the dictionary provided.
                _endPointAddresses = new Dictionary<string, string>(endPointAddresses);
            }

            HostName = hostName ?? throw new ArgumentNullException(nameof(hostName));
            PortNumber = portNumber;
        }

        #endregion

        #region Public Properties

        public string HostName { get; }
        public int PortNumber { get; }

        private string _baseUri;
        public string BaseUri
        {
            get
            {
                if(_baseUri == null)
                {
                    _baseUri = $"http://{HostName}";
                    if(PortNumber > 0)
                    {
                        _baseUri += $":{PortNumber}";
                    }
                }
                return _baseUri;
            }
        }

        public string this[string serviceName]
        {
            get
            {
                return _endPointAddresses[serviceName];
            }
        }

        #endregion


    }
}
