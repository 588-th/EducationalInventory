using System.Text.RegularExpressions;

namespace ValidationData
{
    /// <summary>
    /// Contains methods for validating network settings such as IP addresses, subnet masks, gateways, and DNS addresses.
    /// </summary>
    public static class NetworkSettingValidator
    {
        /// <summary>
        /// Validates an IPv4 address.
        /// </summary>
        /// <param name="ip">The IP address to validate.</param>
        /// <returns>True if the IP address is valid, otherwise false.</returns>
        public static bool ValidateIP(string ip)
        {
            string ipv4Pattern = @"^(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)$";

            if (!Regex.IsMatch(ip, ipv4Pattern))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a subnet mask.
        /// </summary>
        /// <param name="mask">The subnet mask to validate.</param>
        /// <returns>True if the subnet mask is valid, otherwise false.</returns>
        public static bool ValidateMask(string mask)
        {
            string maskPattern = @"^(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)$";

            if (!Regex.IsMatch(mask, maskPattern))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a gateway IP address.
        /// </summary>
        /// <param name="gateway">The gateway IP address to validate.</param>
        /// <returns>True if the gateway IP address is valid, otherwise false.</returns>
        public static bool ValidateGateway(string gateway)
        {
            string gatewayPattern = @"^(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)$";

            if (!Regex.IsMatch(gateway, gatewayPattern))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a DNS address.
        /// </summary>
        /// <param name="dns">The DNS address to validate.</param>
        /// <returns>True if the DNS address is valid, otherwise false.</returns>
        public static bool ValidateDns(string dns)
        {
            string dnsPattern = @"^(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)$";

            if (!Regex.IsMatch(dns, dnsPattern))
            {
                return false;
            }

            return true;
        }
    }
}