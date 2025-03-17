namespace FraudSysApi.Models.TransactionModels
{
    public record ValidateTransaction(string FromDocument, string ToDocument, decimal Value);
}
