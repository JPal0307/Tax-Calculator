using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    static class TaxCalculator
    {
        // Created a static dictionary field that holds a List of TaxRecords and is keyed by a string
        static Dictionary<string, List<TaxRecord>> TaxRecords; 

        // Create a static constructor that:
        static TaxCalculator()
        {
            var reader = System.IO.File.OpenText("taxtable.csv");
            try
            {
                TaxRecords = new Dictionary<string, List<TaxRecord>>();
                do
                {
                    try
                    {
                        string line = reader.ReadLine();
                        List<TaxRecord> tData;
                        TaxRecord TR = new TaxRecord(line);//TR is for Tax Record
                       //adding values from dictionary to list called tData
                        bool spaceFound = TaxRecords.TryGetValue(TR.StateCode, out tData);
                        if(spaceFound)
                        {
                            tData.Add(TR);
                        }
                        else
                        {
                            tData = new List<TaxRecord>();
                            tData.Add(TR);
                            TaxRecords.Add(TR.StateCode, tData);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);//error message
                    }
                } while (!reader.EndOfStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);//error message
            }
            reader.Close();//streamreader is disposed
        }
        
        public static decimal ComputeTaxFor(string stateCode, decimal income, bool verboseTF)
        {
            decimal finalTax = 0;
            
            do
            {
                //created a list of objects associated with the statename key in the dictionary
                var records = TaxRecords[stateCode];
                //loop to get into each record from dictionary
                for (int i = 0; records.Count > i; i++)
                { if( (income > records[i].Floor) && (income <= records[i].Ceiling)) 
                    {
                        if (verboseTF == true) //verbose mode
                        {
                            Console.WriteLine("========================================================================================================");
                            Console.WriteLine("The records floor is being subtracted by income and then multiplied by rate to get the final tax.");
                            Console.WriteLine("The tax owed will be displaying below the double lines");
                            Console.WriteLine("========================================================================================================");
                        }

                        finalTax += (income - records[i].Floor) * records[i].Rate;
                        return finalTax;
                    }
                    finalTax += (records[i].Ceiling - records[i].Floor) * records[i].Rate; 
                }
                return finalTax;
            } while (true);
        }
    }  // this is the end of the Tax Calculator


    class TaxRecord
    {

        //Private fields below
        private string stateCode; 
        private string state;
        private decimal floor;
        private decimal ceiling;
        private decimal rate;

        //Public Properties Below
        public string StateCode
        {
            get => stateCode;
            set => stateCode = value;
        }
        public string State
        {
            get => state;
            set => state = value;
        }
        public decimal Floor
        {
            get => floor;
            set => floor = value;
        }
        public decimal Ceiling
        {
            get => ceiling;
            set => ceiling = value;
        }
        public decimal Rate
        {
            get => rate;
            set => rate = value;
        }

        //Created a constructor that takes a single string (a csv) and uses it to load the record
        public TaxRecord(string csv)
        {
            
            string[] data = csv.Split(',');
            if (string.IsNullOrWhiteSpace(csv)) { throw new Exception("Y u put no values?!"); } ; 
            if (data.Length != 5)
            { throw new Exception($"The line '{csv}' is not valid: it must have 5 parts serperated by commas");}
            StateCode = data[0];
            State = data[1];

            //error checking below
            if (!decimal.TryParse(data[2], out floor))
            { throw new Exception($"The line'{csv}'is not valid: The floor part must be a decimal value.");}
            if (!decimal.TryParse(data[3], out ceiling))
            { throw new Exception($"The line '{csv}' is not valid: The ceiling part must be a decimal value."); }
            if (!decimal.TryParse(data[4], out rate))
            { throw new Exception($"The line '{csv}' is not valid: The rate part must be a decimal value.");}
        }
        //
        //  Create an override of ToString to print out the tax record info nicely
        public override string ToString()
        {
            return $"State Code: {stateCode}, State: {state}, Floor: {floor}, Ceiling: {ceiling}, Rate: {rate}";
        }

    }  // this is the end of the TaxRecord

    class Program
    {
        public static void Main()
        {

            //infinite loop tp prompt the user for a state and income while validating the data  with printing out the total. Also asking user if they want a verbose mode at the end of loop.
            do
            {
                try
                { 
                    decimal income;
                    string state;
                    var verbose = System.IO.File.OpenText("verbose.txt");
                    bool verboseTF = bool.Parse(verbose.ReadLine());//verbose true false, adds the value of verbose into verboseTF as false
                    Console.WriteLine("Hello user, input a state in all caps."); 
                    state = Console.ReadLine();
                    Console.WriteLine("========================================================================================================");
                    Console.WriteLine("Input a income.");
                    income = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("========================================================================================================");
                    Console.WriteLine("Tax owed:" + TaxCalculator.ComputeTaxFor(state, income,verboseTF));
                    Console.WriteLine("========================================================================================================");
                    Console.WriteLine("Would you like to try verbose mode for more detailed information? If yes, enter y. If not, press enter");
                    var verbMode = Console.ReadLine();
                    if(verbMode == "y")
                    {
                        do
                        {
                            try
                            { //verbose mode below
                                verboseTF = true;
                                Console.WriteLine("Verbose Mode Activated!");
                                Console.WriteLine("========================================================================================================");
                                Console.WriteLine("Hello user, input a state in all caps."); 
                                state = Console.ReadLine();
                                Console.WriteLine("========================================================================================================");
                                Console.WriteLine("Input a income.");
                                income = decimal.Parse(Console.ReadLine());
                                Console.WriteLine("Tax owed:" + TaxCalculator.ComputeTaxFor(state, income, verboseTF));
                                Console.WriteLine("Would you like to continue in verbose mode? If yes, enter y. If not, press enter");
                                verbMode = Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message); //error message
                            }
                        } while (verbMode == "y");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);


        }
    }

}

