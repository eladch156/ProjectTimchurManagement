using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Database;

namespace WebApplication1.Infrastructure
{
    /// <summary>
    /// Result for specific user ID invalidation errors. 
    /// </summary>
    public class CustomIdErrorAttribu : ValidationAttribute
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CustomIdErrorAttribu()
        {
          
        }
        /// <summary>
        /// Check for specific user ID conditions.
        /// </summary>
        /// <param name="value">the ID card number.</param>
        /// <param name="validationContext">Context for validation.</param>
        /// <returns>The validation result of the query.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var IDCardNumber = (string)value;
            if (IDCardNumber == null)
            {
                return new ValidationResult("הערך ת.ז. נדרש");
            }
            if (IDCardNumber.Length!=9)
            {
                return new ValidationResult("תעודת זהות אינה באורך המתאים");
            }
            int [] id = new int[IDCardNumber.Length];
            int[] w = new int[IDCardNumber.Length];
            int i = 0;
            foreach(char s in IDCardNumber)
            {
                id[i] = (s - '0');
                if (i % 2 == 0)
                {
                    w[i] = 1;
                }
                else
                {
                    w[i] = 2;
                }
                id[i] *= w[i];
                int sum = 0;
                while (id[i] != 0)
                {
                    sum += id[i] % 10;
                    id[i] /= 10;
                }
                id[i] = sum;

                i++;
            }
            int sumF = 0;
            for (int j = 0; j < IDCardNumber.Length; j++)
                sumF += id[j];
            if(sumF%10!=0)
            {
                return new ValidationResult("תעודת זהות אינה תקינה");
            }
            return null;
        }
    }
}