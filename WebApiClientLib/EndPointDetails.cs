using System;
using System.Collections.Generic;

namespace WebApiClientLib
{
    public class EndPointDetails
    {
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
