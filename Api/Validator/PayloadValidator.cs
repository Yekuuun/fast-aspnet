using System.Reflection;
using System.Text.RegularExpressions;

namespace Api.Services;

/// <summary>
/// this class contains code related to Payloads handling avoiding SQL, XSS and other attacks based on malicious payloads in Params controllers based.
/// </summary>
public static class PayloadValidator
{
    
    /// <summary>
    /// Base pattern against SQL injection queries.
    /// </summary>
    private static readonly Regex _sqlInjectionPattern = new Regex(
        @"(\b(SELECT|INSERT|UPDATE|DELETE|DROP|EXEC|UNION|--|')\b)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled
    );

    /// <summary>
    /// Base pattern against XSS attacks.
    /// </summary>
    private static readonly Regex _xssPattern = new Regex(
        @"<.*?>",
        RegexOptions.IgnoreCase | RegexOptions.Compiled
    );

    /// <summary>
    /// base check on strings.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsSafeString(string input)
    {
        if(string.IsNullOrEmpty(input))
        {
            return false;
        }

        if(_sqlInjectionPattern.IsMatch(input) || _xssPattern.IsMatch(input))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Base check on integers values.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsValidInt(string input)
    {
        if(string.IsNullOrEmpty(input))
        {
            return false;
        }

        return int.TryParse(input, out _);
    }

    /// <summary>
    /// Base check on URL passed.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static bool IsValidUrl(string url)
    {
        string urlPattern = @"^(https?:\/\/)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,6}(:[0-9]{1,5})?(\/[^\s]*)?$";

        return Regex.IsMatch(url, urlPattern);
    }

    /// <summary>
    /// Doing base check on DCO payload used as params.
    /// </summary>
    /// <param name="dco"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsDcoPayloadSafe<T>(T dco) where T : BaseDto
    {
        try
        {
            if(dco == null)
            {
                return false;
            }

            foreach(PropertyInfo prop in typeof(T).GetProperties())
            {
                object? value = prop.GetValue(dco);

                if(value is string strValue)
                {
                    if(!string.IsNullOrEmpty(strValue) && !IsSafeString(strValue))
                    {
                        return false;
                    }
                }

                if(value is int intValue)
                {
                    if(!IsValidInt(intValue.ToString()))
                    {
                        return false;
                    }
                }

                else
                {
                    //TO DEVELOP handling different cases of values.
                    return true;
                }
            }

            return true;
        }
        catch(Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// building base bad Request service response object.
    /// </summary>
    /// <param name="message"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ServiceResponse<T> BuildError<T>(string message) where T : BaseDto 
    {
        ServiceResponse<T> badResponse = new()
        {
            Message = message,
            ErrorType = EErrorType.BAD
        };

        return badResponse;
    }

    /// <summary>
    /// Checking if an email is in a blacklisted list.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool CheckBlackListedEmail(string email)
    {
        string domainPrefix = email.Split("@").Last();
        if(string.IsNullOrEmpty(domainPrefix))
        {
            return false;
        }
        else
        {
            HashSet<string> disposableEmails =
            [
                "10minutemail.com",
                "20minutemail.com",
                "30minutemail.com",
                "anonymail.net",
                "bouncemail.com",
                "burnermail.com",
                "discard.email",
                "disposableemail.com",
                "dropmail.me",
                "emailondeck.com",
                "fakeinbox.com",
                "getairmail.com",
                "guerrillamail.com",
                "hottempmail.com",
                "incognitomail.com",
                "instant-email.org",
                "mail-temp.com",
                "mail1a.de",
                "mailcatch.com",
                "maildrop.cc",
                "mailinator.com",
                "mailnesia.com",
                "moakt.com",
                "my10minutemail.com",
                "mytrashmail.com",
                "nowmymail.com",
                "sharklasers.com",
                "spam4.me",
                "temp-mail.org",
                "tempmail.com",
                "tempmail.de",
                "tempmail.net",
                "throwawaymail.com",
                "trash-mail.com",
                "trashmail.com",
                "trashmail.de",
                "yopmail.com",
                "yopmail.fr",
                "yopmail.net",
                "zmail.com"
            ];

            return disposableEmails.Contains(domainPrefix);
        }
    }
}