using Common;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ValidationData
{
    /// <summary>
    /// Contains methods for validating network settings such as IP addresses, subnet masks, gateways, and DNS addresses.
    /// </summary>
    public static class NetworkSettingValidator
    {
        public static (bool result, string error) ValidateNetworkSetting(NetworkSetting networkSetting)
        {
            Dictionary<Func<string, bool>, string> validationChecks = new Dictionary<Func<string, bool>, string>
            {
                { ValidateIP, "Invalid IP format" },
                { ValidateMask, "Invalid mask format" },
                { ValidateGateway, "Invalid gateway format" },
                { ValidateDns, "Invalid DNS format" }
            };

            foreach (var validationCheck in validationChecks)
            {
                bool isValid = validationCheck.Key.Invoke(GetNetworkSettingField(networkSetting, validationCheck.Key));
                if (!isValid)
                {
                    return (false, validationCheck.Value);
                }
            }

            return (true, "");
        }

        private static string GetNetworkSettingField(NetworkSetting networkSetting, Func<string, bool> validationFunction)
        {
            if (validationFunction == ValidateIP) return networkSetting.Ip;
            if (validationFunction == ValidateMask) return networkSetting.Mask;
            if (validationFunction == ValidateGateway) return networkSetting.Gateway;
            if (validationFunction == ValidateDns) return networkSetting.Dns;

            return string.Empty;
        }

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