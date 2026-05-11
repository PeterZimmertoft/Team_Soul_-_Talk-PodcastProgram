namespace Soul_Talk.Services
{
    public class ValidationService : IValidationService
    {
        public bool IsValidCprNumber(string cprNumber)
        {
            if (string.IsNullOrWhiteSpace(cprNumber) || cprNumber.Length != 11 || cprNumber[6] != '-')
            {
                return false;
            }

            for (int i = 0; i < 6; i++)
            {
                if (!char.IsDigit(cprNumber[i]))
                {
                    return false;
                }
            }

            for (int i = 7; i < 11; i++)
            {
                if (!char.IsDigit(cprNumber[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsValidConsentStatus(string consentStatus)
        {
            if (string.IsNullOrWhiteSpace(consentStatus))
            {
                return false;
            }

            string value = consentStatus.Trim().ToLower();
            return value == "ja" || value == "nej";
        }
    }
}