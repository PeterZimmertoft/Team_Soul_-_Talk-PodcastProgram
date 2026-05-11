namespace Soul_Talk.Services
{
    public interface IValidationService
    {
        bool IsValidCprNumber(string cprNumber);
        bool IsValidConsentStatus(string consentStatus);
    }
}
