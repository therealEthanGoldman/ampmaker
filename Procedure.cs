using System;
using System.Text.RegularExpressions;

namespace Ampmaker
{
    internal class Procedure
    {

            
        string Name = "Thumbtack Grenade";
        string Discipline;
        int Level;
        string Effect = "Damage 1";
        bool NE = false;
        string[] effectarray;
        int effectnum;

        public Procedure(string alias, string field, int power, string use)
        {
            Name = alias;
            Discipline = field;
            Level = power;
            Effect = use;
            effectarray = Effect.Split(" ");
            int inum = effectarray.Length;

            for (int i = 0; i < inum; i++)
            {
                //Console.WriteLine(effectarray[i]);

                if (Regex.Match(effectarray[i], @"\d+").Value != "")
                {
                    if ((i + 1) != inum)
                    {
                        if (Regex.Match(effectarray[i + 1], "Minutes").Value == "")
                        {
                            NE = true;
                            effectnum = i;
                        }
                    }
                    else
                    {
                        NE = true;
                        effectnum = i;
                    }
                }
                else if (Regex.Match(effectarray[i], "Bestow").Value != "")
                {
                    NE = true;
                    effectnum = -2;
                }

            }
        }


        public Procedure()
        {
            bool success = false;

            Console.WriteLine("What is it's name?");
            Name = Console.ReadLine();

            while (success == false)
            {
                Console.WriteLine("What is it's Discipline?");
                string check = Console.ReadLine();
                if ((check == "Biology") || (check == "biology") || (check == "Bio") || (check == "bio"))
                {
                    Discipline = "Bio";
                    success = true;
                }
                else if ((check == "Chemistry") || (check == "chemistry") || (check == "Chem") || (check == "chem"))
                {
                    Discipline = "Chem";
                    success = true;
                }
                else if ((check == "Math") || (check == "math") || (check == "Mathmatics") || (check == "mathmatics"))
                {
                    Discipline = "Math";
                    success = true;
                }
                else if ((check == "Physics") || (check == "physics"))
                {
                    Discipline = "Physics";
                    success = true;
                }
                else if ((check == "Psychology") || (check == "psychology") || (check == "Psych") || (check == "psych"))
                {
                    Discipline = "Psych";
                    success = true;
                }
                else if ((check == "Combat") || (check == "combat"))
                {
                    Discipline = "Combat";
                    success = true;
                }
                else if ((check == "Stealth") || (check == "stealth"))
                {
                    Discipline = "Stealth";
                    success = true;
                }
                else if ((check == "Production") || (check == "production"))
                {
                    Discipline = "Production";
                    success = true;
                }
                else
                {
                    Console.WriteLine("Please use  Bio, Chemistry, Math, Physics, Psych, Combat, Stealth, or Production");
                }
            }
            success = false;
            while (success == false)
            {
                Console.WriteLine("What is it's level?");
                string stringpower = Console.ReadLine();
                if (!int.TryParse(stringpower, out Level))
                {
                    Console.WriteLine("Please use a number");
                }
                else
                {
                    success = true;
                }
            }
            Console.WriteLine("What is it's effect?");
            Effect = Console.ReadLine();
            effectarray = Effect.Split(" ");
            int inum = effectarray.Length;

            for( int i = 0; i < inum; i++)
            {
                //Console.WriteLine(effectarray[i]);

                if (Regex.Match(effectarray[i], @"\d+").Value != "")
                {
                    if ((i+1) != inum)
                    {
                        if (Regex.Match(effectarray[i + 1], "Minutes").Value == "")
                        {
                            NE = true;
                            effectnum = i;
                        }
                    }
                    else
                    {
                        NE = true;
                        effectnum = i;
                    }
                }else if(Regex.Match(effectarray[i], "Bestow").Value != "")
                {
                    NE = true;
                    effectnum = -2;
                }

            }

           /*
            if (Regex.Match(Effect, @"\d+").Value == "")
            {
                NE = false;
                //Console.WriteLine("This worked");
            }
            else
            {
                NE = true;
                //string numericalstring = Regex.Match(use, @"\d+").Value;
                //Effect = Int32.Parse(numericalstring);
                //Console.WriteLine("This suceeded " + BaseEffect);
            }*/
        }

        public string getDiscipline()
        {
            return Discipline;
        }

        public int getLevel()
        {
            return Level;
        }

        public string getEffect()
        {
            return Effect;
        }

        public void setEffect(string use, string[] usearray)
        {
            Effect = use;
            effectarray = usearray;

        }

        public bool getNE()
        {
            return NE;
        }

        public string[] geteffectarray()
        {
            return effectarray;
        }

        public int getEffectnum()
        {
            return effectnum;
        }

        public void setEffectnum(int newnum)
        {
            effectnum = newnum;
        }

        public string getName()
        {
            return Name;
        }

    }

    
}