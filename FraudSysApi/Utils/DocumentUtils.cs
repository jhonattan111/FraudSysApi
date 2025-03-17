namespace FraudSysApi.Utils
{
    public static class DocumentUtils
    {
        public static bool IsValid(this string document)
        {
            return document.Length == 11 && document.CheckDocument();
        }

        public static bool CheckDocument(this string document)
        {
            int[] digits = document.Select(c => int.Parse(c.ToString())).ToArray();

            for(int i = 0; i < 9; i++)
            {
                int sum = 0;
                for (int j = 0; j < 9; j++)
                {
                    sum += digits[j] * (i + 1 - j);
                }
                if (sum % 11 < 2)
                {
                    if (digits[9] != 0)
                    {
                        return false;
                    }
                }
                else
                {
                    if (digits[9] != 11 - sum % 11)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
