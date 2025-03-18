using System.Globalization;

namespace FraudSysApi.Utils
{
    public static class DocumentUtils
    {
        public static bool IsValid(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;
            
            cpf = new string(cpf.Where(char.IsDigit).ToArray());
            
            if(cpf.Length != 11) return false;
            
            int[] cpfDigits = cpf.Select(d => int.Parse(d.ToString())).ToArray();
            return VerifyDigit(cpfDigits, 9) && VerifyDigit(cpfDigits, 10);
        }

        private static bool VerifyDigit(int[] cpf, int position)
        {
            int sum = 0;
            int multiplier = position + 1;

            for (int i = 0; i < position; i++)
            {
                sum += cpf[i] * multiplier--;
            }
            
            int rest = sum % 11;
            int verifierDigit = rest < 2 ? 0 : 11 - rest;
            
            return verifierDigit == cpf[position];
        }
    }
}
