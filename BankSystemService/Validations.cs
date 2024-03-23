using System.Text.RegularExpressions;
namespace DotNetBank
{
    public enum AccountType
    {
        Savings,
        Salary,
        Current,
        savings,
        salary,
        current
    }
    public enum Gender
    {
        Male,
        Female,
        other,
        Other,
        female,
        male
    }
    public enum TransactionType
    {
        Deposit,
        deposit,
        Withrawal,
        withdrawal
    }
    public class Validations : IValidations
    {
        public void EmailCheck(string Email)
        {
            if (!(new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))[a-zA-Z]{2,4}(\]?)$")).IsMatch(Email))
                throw new ArgumentException($"Not a valid Email");

        }
        public void ContactNumberCheck(long ContactNumber)
        {

            string s3 = ContactNumber.ToString();
            if (s3.Length != 10) throw new ArgumentException($"Invalid Contact Number");
        }
        public void AddressCheck(Address address)
        {
            if (address.Line1 == "" || address.Line2 == "" ||
              address.PinCode == 0 || address.Country == "" ||
              address.State == "") throw new ArgumentException($"Invalid Address");

            int Pin = address.PinCode;
            string s4 = Pin.ToString();
            if (s4.Length != 6) throw new ArgumentException($"Invalid PIN code");
        }
        public void NameCheck(string Name)
        {
            if (Name == "") throw new ArgumentException($"Not a valid Name");

            string lower = Name.ToLower();
            int namelength = lower.Length;
            for (int i = 0; i < namelength; i++)
            {
                if (lower[i] >= 'a' && lower[i] <= 'z') continue;

                else if (lower[i] == ' ') continue;
                else
                {
                    throw new ArgumentException($"Not a valid Name");
                }
            }
        }

        public void PANCheck(string PAN, string FirstName, string LastName)
        {
            string pan = PAN;
            if (pan.Length != 10)
            {

                throw new ArgumentException($"Invalid PAN");

            }
            string four = pan.Substring(0, 4);
            string five = pan.Substring(4, 1);
            string no = pan.Substring(5, 4);
            string last = pan.Substring(9, 1);


            //1-4 check pan

            for (int i = 0; i < 4; i++)
            {
                if (four[i] >= 'A' && four[i] <= 'Z')
                {
                    continue;
                }
                else
                {
                    throw new ArgumentException($"Invalid PAN");


                }
            }

            //number  check
            var No = Int16.Parse(no);
            string nostring = No.ToString();
            if (nostring.Length != 4)
            {

                throw new ArgumentException($"Invalid PAN");
            }



            string pattern = @"[0-9]{4}$";
            Regex re = new Regex(pattern);


            if (!re.IsMatch(no))
                throw new ArgumentException($"Invalid PAN");


            string Five = five.ToLower();
            string fname = FirstName;
            string lname = LastName;
            string fn = fname.Substring(0, 1);
            string ln = lname.Substring(0, 1);
            string fl = fn.ToLower();
            string ll = ln.ToLower();
            if (fl == Five || ll == Five)
            {
                Console.WriteLine("ok");

            }
            else
            {

                throw new ArgumentException($"Invalid PAN");
            }


        }
        public int DOBCheck(string DOB)
        {
            if (DOB == "") throw new ArgumentException($"DOB field is empty");

            if (!(new Regex(@"\b(((0?[469]|11)/(0?[1-9]|[12]\d|30)|(0?[13578]|1[02])/(0?[1-9]|[12]\d|3[01])|0?2/(0?[1-9]|1\d|2[0-8]))/([1-9]\d{3}|\d{2})|0?2/29/([1-9]\d)?([02468][048]|[13579][26]))\b")).IsMatch(DOB))
                throw new ArgumentException($"Not a valid DOB");


            int age = 0;
            age = DateTime.Now.Subtract(Convert.ToDateTime(DOB)).Days;
            age = age / 365;
            if (age < 5 || age > 120) throw new ArgumentException($"Minimum age for opening an account is 5 ,And maximum age is 120");
            return age;


        }

        public void CheckIFSC(string IFSC)
        {
            if (IFSC == "") throw new ArgumentException($"IFSC field is empty");
            if (IFSC.Length != 11) throw new ArgumentException($"Invalid IFSC, Please check the IFSC code");

        }
    }

}