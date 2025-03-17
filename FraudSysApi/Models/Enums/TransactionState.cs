namespace FraudSysApi.Models.Enums
{
    public enum TransactionState
    {
        ACCOUNT_NOT_FOUND,
        INSUFFICIENT_FUNDS,
        FRAUD_SUSPECTED,
        SAME_TRANSACTION_RECENTLY,
        SAME_ACCOUNT,
        VALID
    }
}
