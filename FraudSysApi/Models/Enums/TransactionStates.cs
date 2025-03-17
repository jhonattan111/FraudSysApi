namespace FraudSysApi.Models.Enums
{
    public enum TransactionStates : int
    {
        ACCOUNT_NOT_FOUND = 404,
        INSUFFICIENT_FUNDS,
        FRAUD_SUSPECTED,
        SAME_TRANSACTION_RECENTLY,
        SAME_ACCOUNT,
        VALID = 200
    }
}
