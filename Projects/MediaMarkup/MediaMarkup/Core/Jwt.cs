using System;
using System.Text;
using Newtonsoft.Json;

namespace MediaMarkup
{
    public static class Jwt
    {
        public static JwtTokenData GetTokenData(string token)
        {
            try
            {
                var tokenParts = token.Split(".");

                if (tokenParts.Length != 3)
                {
                    return null;
                }

                var headerJsonString = Encoding.Default.GetString(Base64UrlDecode(tokenParts[0]));

                dynamic header = JsonConvert.DeserializeObject(headerJsonString);

                if (header == null)
                {
                    return null;
                }

                if (header.typ == null || header.alg == null || header.typ.ToString() != "JWT")
                {
                    return null;
                }

                var payloadJsonString = Encoding.Default.GetString(Base64UrlDecode(tokenParts[1]));

                var signature = tokenParts[2];

                return new JwtTokenData { Header = headerJsonString, Payload = payloadJsonString, Signature = signature };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        private static string Base64UrlEncode(string value)
        {
            return Convert.ToBase64String(Convert.FromBase64String(value)).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        private static byte[] Base64UrlDecode(string arg)
        {
            var s = arg;
            s = s.Replace('-', '+');
            s = s.Replace('_', '/');
            switch (s.Length % 4) 
            {
                case 0: break;
                case 2: s += "=="; break;
                case 3: s += "="; break;
                default: throw new Exception("Error decoding base64url string");
            }
            return Convert.FromBase64String(s);
        }

        private static long ToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(Convert.ToDouble(unixTimeStamp)).ToLocalTime();
        }
    }
}