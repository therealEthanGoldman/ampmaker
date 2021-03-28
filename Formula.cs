using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ampmaker
{
    internal class Formula
    {
        int NEE = 0;
        int procednum = 1;
        string discipline = "Huge";
        string prefix = "";
        string suffix = "";
        string addiction = "";
        string useafter = "";
        Procedure baseProcedure;
        List <Procedure> formulalist = new List<Procedure>();

        int B = 0;
        int A = 0;
        int D = 0;
        int Q = 0;
        int V = 0;
        int T = 0;

        string label;

        double Complexity;
        int BuildCost;
        int TheoryLevel;
        string Name;



        //Technician User = new Technician();

        /*Console.WriteLine("What is your procedure's name");
        string alias = Console.ReadLine();
        Console.WriteLine("What is it's discipline:");
        string field = Console.ReadLine(); */

        public void WriteFormula()
        {
            Console.WriteLine("Enter Formula name");
            Name = Console.ReadLine();

            baseProcedure = new Procedure();
            discipline = baseProcedure.getDiscipline();


            //Additive
            SetAdditive();

            //Delivery
            SetDelivery();


            //Quanity
            SetQuanity();

            for(int i = 0; i < procednum; i++)
            {
                formulalist.Add(new Procedure( baseProcedure.getName(), baseProcedure.getDiscipline(), baseProcedure.getLevel(), baseProcedure.getEffect()));
            }

            //Variety        
            V = 0;


            //Timing   
            SetTiming();

            B = baseProcedure.getLevel() * procednum;

            Complexity = A + D + Q + V + T;
            BuildCost = B + (int)Complexity;
            TheoryLevel = (int)((Complexity / 2) + .5);
            string formcore = "";




            //NE is added wholesale
            if (NEE > 0)
            {
                NEUpgrade(NEE);
            }
            for (int i = 0; i < procednum; i++)
            {
                formcore += formulalist[i].getEffect() + " ";
            }


            

            label = discipline + ", " + prefix + formcore + suffix + addiction;
            if (useafter != "")
            {
                label = useafter + "\n" + label;
            }
        }

        public void WriteMultiFormula()
        {
            Console.WriteLine("Enter Formula name");
            Name = Console.ReadLine();

            SetQuanity();

            List<string> disilist = new List<string>();
            int count = 0;

            while (count < procednum)
            {
                int count2 = 0;
                Console.WriteLine("What is the new procedure you are using?");
                Procedure Proc = new Procedure();
                bool wrongnumber = true;
                while (wrongnumber)
                {

                    Console.WriteLine("How many copies of "+ Proc.getName() +" are you using? If wrong procedure enter 0");
                    string strincop = Console.ReadLine();                    
                    if (int.TryParse(strincop, out count2))
                    {
                        if ((count2 + count) > procednum)
                        {
                            Console.WriteLine("Wrong Number");
                        }
                        else 
                        {
                            wrongnumber = false;
                                                                                   
                            if (!disilist.Contains(Proc.getDiscipline()))
                            {
                                if (disilist.Count < 6)
                                {
                                    disilist.Add(Proc.getDiscipline());                                    
                                }
                                else
                                {
                                    Console.WriteLine("This is impossible to use.");
                                    count2 = 0;
                                }
                            }
                            
                        }
                    }
                }

                for (int i = 0; i < count2; i++)                {
                    formulalist.Add(new Procedure(Proc.getName(), Proc.getDiscipline(), Proc.getLevel(), Proc.getEffect()));
                    B += Proc.getLevel();
                }
                count += count2;
            }
            int disclen = disilist.Count;

            SetVariety(disclen);
            SetAdditive();
            SetDelivery();
            SetTiming();

            Complexity = A + D + Q + V + T;
            BuildCost = B + (int)Complexity;
            TheoryLevel = (int)((Complexity / 2) + .5);
            string formcore = "";




            //NE is added wholesale
            if (NEE > 0)
            {
                NEUpgrade(NEE);
            }
            for (int i = 0; i < procednum; i++)
            {
                formcore += formulalist[i].getEffect() + " ";
            }




            label = prefix + formcore + suffix + addiction;
            if (useafter != "")
            {
                label = useafter + "\n" + label;
            }


        }


        private void SetAdditive()
        {
            bool success = false;
            while (success == false)
            {
                Console.WriteLine("Enter Additive ");
                string stringA = Console.ReadLine();
                if (!int.TryParse(stringA, out A))
                {
                    Console.WriteLine("Please use a number");
                }
                else
                {
                    A = Convert.ToInt32(stringA);
                    if ((0 <= A) && (A <= 6))
                    {
                        if (A == 0)
                        {
                            success = true;
                        }
                        else
                        {

                            while (success == false)
                            {
                                Console.WriteLine("Type A for Addictive, C for Catalyst");
                                string answerA = Console.ReadLine();
                                if ((answerA == "C") || (answerA == "c") || (answerA == "A") || (answerA == "a"))
                                {
                                    success = true;
                                    if ((answerA == "C") || (answerA == "c"))
                                    {
                                        NEE += A;
                                    }
                                    else if ((answerA == "A") || (answerA == "a"))
                                    {
                                        NEE += 2 * A;
                                        string addictype = "";
                                        switch (A)
                                        {
                                            case 1:
                                                addictype = "trivial";
                                                break;
                                            case 2:
                                                addictype = "minor";
                                                break;
                                            case 3:
                                                addictype = "moderate";
                                                break;

                                            case 4:
                                                addictype = "major";
                                                break;
                                            case 5:
                                                addictype = "industrial";
                                                break;
                                            case 6:
                                                addictype = "designer";
                                                break;
                                            default:
                                                break;
                                        }
                                        addiction = " Inflict " + addictype + " addiction to " + Name + ".";
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Wrong input");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong Number");
                    }
                }
            }
        }

        private void SetDelivery()
        {
            bool success = false;
            while (success == false)
            {
                Console.WriteLine("Enter Delivery");
                string stringD = Console.ReadLine();
                if (!int.TryParse(stringD, out D))
                {
                    Console.WriteLine("Please use a number");
                }
                else
                {
                    D = Convert.ToInt32(stringD);
                    if ((0 <= D) && (D <= 6))
                    {
                        string answerD;

                        switch (D)
                        {
                            case 1:
                                while (success == false)
                                {
                                    Console.WriteLine("Type P for Packet, F for Fist");
                                    answerD = Console.ReadLine();
                                    if ((answerD == "P") || (answerD == "p") || (answerD == "F") || (answerD == "f"))
                                    {
                                        success = true;
                                        if ((answerD == "P") || (answerD == "p"))
                                        {
                                            prefix = "By packet: ";
                                        }
                                        else if ((answerD == "F") || (answerD == "f"))
                                        {
                                            prefix = "By fist: ";
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong input");
                                    }

                                }
                                break;

                            case 2:
                                while (success == false)
                                {
                                    Console.WriteLine("Type B for Blade, G for Gun");
                                    answerD = Console.ReadLine();
                                    if ((answerD == "B") || (answerD == "b") || (answerD == "G") || (answerD == "g"))
                                    {
                                        success = true;
                                        if ((answerD == "B") || (answerD == "b"))
                                        {
                                            prefix = "By blade: ";
                                        }
                                        else if ((answerD == "G") || (answerD == "g"))
                                        {
                                            prefix = "By Gun: ";
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong input");
                                    }

                                }
                                break;

                            case 3:
                                while (success == false)
                                {
                                    Console.WriteLine("Enter appropiate letter for Slot:");
                                    Console.WriteLine("B for Body");
                                    Console.WriteLine("G for Gear");
                                    Console.WriteLine("T for Tactics");
                                    Console.WriteLine("M for Mind");
                                    Console.WriteLine("W for Weapon");

                                    answerD = Console.ReadLine();
                                    if ((answerD == "B") || (answerD == "b") || (answerD == "G") || (answerD == "g") || (answerD == "T") || (answerD == "t") || (answerD == "M") || (answerD == "m") || (answerD == "W") || (answerD == "w"))
                                    {
                                        success = true;
                                        if ((answerD == "B") || (answerD == "b"))
                                        {
                                            prefix = "Bestow \"";
                                            suffix = "\", through Body";
                                        }
                                        else if ((answerD == "G") || (answerD == "g"))
                                        {
                                            prefix = "Bestow \"";
                                            suffix = "\", through Gear";
                                        }
                                        else if ((answerD == "T") || (answerD == "t"))
                                        {
                                            prefix = "Bestow \"";
                                            suffix = "\", through Tactics";
                                        }
                                        else if ((answerD == "M") || (answerD == "m"))
                                        {
                                            prefix = "Bestow \"";
                                            suffix = "\", through Mind";
                                        }
                                        else if ((answerD == "W") || (answerD == "w"))
                                        {
                                            prefix = "Bestow \"";
                                            suffix = "\", through Weapon";
                                        }


                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong input");
                                    }

                                }
                                break;

                            case 4:
                                string trigger;
                                Console.WriteLine("Enter Trigger (Do not start with \"Trigger:\")");
                                trigger = " Trigger: " + Console.ReadLine();
                                while (success == false)
                                {
                                    Console.WriteLine("Enter appropiate letter for Slot:");
                                    Console.WriteLine("B for Body");
                                    Console.WriteLine("G for Gear");
                                    Console.WriteLine("T for Tactics");
                                    Console.WriteLine("M for Mind");
                                    Console.WriteLine("W for Weapon");


                                    answerD = Console.ReadLine();
                                    if ((answerD == "B") || (answerD == "b") || (answerD == "G") || (answerD == "g") || (answerD == "T") || (answerD == "t") || (answerD == "M") || (answerD == "m") || (answerD == "W") || (answerD == "w"))
                                    {
                                        success = true;
                                        if ((answerD == "B") || (answerD == "b"))
                                        {
                                            prefix = "Bestow Contingency: ";
                                            suffix = trigger + ", through Body";
                                        }
                                        else if ((answerD == "G") || (answerD == "g"))
                                        {
                                            prefix = "Bestow Contingency: ";
                                            suffix = trigger + ", through Gear";
                                        }
                                        else if ((answerD == "T") || (answerD == "t"))
                                        {
                                            prefix = "Bestow Contingency: ";
                                            suffix = trigger + ", through Tactics";
                                        }
                                        else if ((answerD == "M") || (answerD == "m"))
                                        {
                                            prefix = "Bestow Contingency: ";
                                            suffix = trigger + ", through Mind";
                                        }
                                        else if ((answerD == "W") || (answerD == "w"))
                                        {
                                            prefix = "Bestow Contingency: ";
                                            suffix = trigger + ", through Weapon";
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong input");
                                    }

                                }
                                break;

                            case 5:
                                Console.WriteLine("Please create S.O.P sheet seperately.");
                                prefix = "Using Infrastructure: ";
                                break;

                            case 6:
                                prefix = "Autonomous: ";
                                break;


                            default:
                                success = true;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong Number");
                    }
                }
            }
        }

        private void SetQuanity()
        {
            bool success = false;
            while (success == false)
            {
                Console.WriteLine("Enter Quanity");
                Console.WriteLine("0     1 Procedure\n1     2 Procedures\n2     3 Procedures\n3     6 Procedures\n4     12 Procedures\n5     18 Procedures\n6     36 Procedures");
                string stringQ = Console.ReadLine();
                if (!int.TryParse(stringQ, out Q))
                {
                    Console.WriteLine("Please use a number");
                }
                else
                {
                    Q = Convert.ToInt32(stringQ);
                    if ((0 <= Q) && (Q <= 6))
                    {
                        if (Q == 0)
                        {
                            success = true;
                        }
                        else
                        {
                            success = true;
                            switch (Q)
                            {
                                case 1:
                                    procednum = 2;
                                    break;
                                case 2:
                                    procednum = 3;
                                    break;
                                case 3:
                                    procednum = 6;
                                    break;
                                case 4:
                                    procednum = 12;
                                    break;
                                case 5:
                                    procednum = 18;
                                    break;
                                case 6:
                                    procednum = 36;
                                    break;
                                default:
                                    break;
                            }


                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong Number");
                    }
                }
            }
        }

        private void SetVariety(int len)
        {
            bool success = false;
            bool iszero = true;
            while (success == false)
            {
                if (len == 1)
                {
                    for (int i = 0; i < formulalist.Count; i++)
                    {
                        if(formulalist[i].getName() != formulalist[0].getName())
                        {
                            iszero = false;
                        }

                    }
                    if(iszero == true)
                    {
                        len = 0;
                    }
                }
                Console.WriteLine("Enter Variety. You need at least " + len + " to be legal");
                string stringV = Console.ReadLine();
                if (!int.TryParse(stringV, out V))
                {
                    Console.WriteLine("Please use a number");
                }
                else
                {
                    if ((len <= V) && (V <= 6))
                    {
                        if (V == len)
                        {
                            success = true;
                        }
                        else
                        {
                            success = true;
                            Console.WriteLine("Warning: unnecessary expense");

                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong Number");
                    }
                }
            }

        }

        private void SetTiming()
        {
            bool success = false;
            int Y = 0;
            int S = 0;
            while (success == false)
            {
                Console.WriteLine("Enter Timing ");
                string stringT = Console.ReadLine();
                if (!int.TryParse(stringT, out T))
                {
                    Console.WriteLine("Please use a number");
                }
                else
                {
                    T = Convert.ToInt32(stringT);
                    if ((0 <= T) && (T <= 6))
                    {
                        if (T == 0)
                        {
                            success = true;
                        }
                        else
                        {

                            while (success == false)
                            {
                                Console.WriteLine("Type B for Use By, A for Use After");
                                string answerT = Console.ReadLine();
                                if ((answerT == "B") || (answerT == "B") || (answerT == "A") || (answerT == "a"))
                                {
                                    if ((answerT == "B") || (answerT == "b"))
                                    {
                                        success = true;
                                        switch (T)
                                        {
                                            case 1:
                                                useafter = "You have 5 minutes to deliver the following effects:";
                                                break;
                                            case 2:
                                                useafter = "You have 20 minutes to deliver the following effects:";
                                                break;
                                            case 3:
                                                useafter = "You have 1 Hours to deliver the following effects:";
                                                break;
                                            case 4:
                                                useafter = "You have 2 Hour to deliver the following effects:";
                                                break;
                                            case 5:
                                                useafter = "You have until the end of the day to deliver the following effects:";
                                                break;
                                            case 6:
                                                useafter = "You have until the end of the event to deliver the following effects:";
                                                break;
                                            default:
                                                break;
                                        }
                                        
                                    }
                                    else if ((answerT == "A") || (answerT == "a"))
                                    {
                                        Console.WriteLine("What year in game is this made?");
                                        string stringY = Console.ReadLine();
                                        if (Regex.Match(stringY, @"\d+").Value == "")
                                        {
                                            Console.WriteLine("Please use a number");
                                        }
                                        else {
                                            
                                            Y = Convert.ToInt32(stringY);

                                            Console.WriteLine("What season is it, hit W for winter, F for fall, S for spring.");
                                            stringY = Console.ReadLine();

                                            if ((stringY == "W") || (stringY == "w"))
                                            {
                                                S = 5;                                                
                                            }else if ((stringY == "F") || (stringY == "f") || (stringY == "s") || (stringY == "S"))
                                            {
                                                while (success == false) {
                                                    Console.WriteLine("What event is it in this season, enter 1 or 2.");
                                                    S = Convert.ToInt32(Console.ReadLine());
                                                    if ((S == 1) || (S == 2))
                                                    {
                                                        success = true;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Please enter 1 or 2");
                                                    }
                                                        }
                                                if ((stringY == "F") || (stringY == "f"))
                                                {
                                                    S = S + 2;
                                                }
                                            }
                                        }
                                        S = S + Convert.ToInt32(T);
                                        while(S > 5)
                                        {
                                            S = S - 5;
                                            Y++;
                                        }
                                        if((S == 3) || (S == 4)){
                                            useafter = "USABLE ONLY AFTER FALL " + (S-2) + " CY " + Y;
                                        }
                                        else if(S == 5)
                                        {
                                            useafter = "USABLE ONLY AFTER WINTER" + " CY " + Y;
                                        }else if((S == 1) || (S== 2))
                                        {

                                            useafter = "USABLE ONLY AFTER SPRING " + (S) + " CY " + Y;

                                        }
                                        NEE += T;
                                    } 
                                }                                
                                else
                                {
                                    Console.WriteLine("Wrong input");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong Number");
                    }
                }
            }

        }

        public string GetLabel() 
        {
            return label;
        }

        public double GetComplexity()
        {
            return Complexity;
        }
        public int GetBuildCost()
        {
            return BuildCost;
        }
        public int GetTheoryLevel()
        {
            return TheoryLevel;
        }

        private void NEUpgrade(int NEE)
        {
            int arraylength = Convert.ToInt32(procednum);
            bool isNE = false;
            List <int> slots = new List<int>();
            int minEffort = 10;

            for (int i = 0; i < arraylength; i++)
            {
                if (formulalist[i].getNE() && (NEE > formulalist[i].getLevel()))
                {
                    isNE = true;
                    slots.Add(i);
                    if(minEffort > formulalist[i].getLevel())
                    {
                        minEffort = formulalist[i].getLevel();
                    }

                }
            }
            int slotlength = slots.Count;

            if (isNE)
            {
                while (NEE >= minEffort)
                {

                    Console.WriteLine("You have " + NEE + " Numerical points. Which effects do you want to raise?");
                    for (int i = 0; i < slotlength; i++)
                    {

                            Console.WriteLine(i + " " + formulalist[slots[i]].getName() + " " + formulalist[slots[i]].getLevel() + " " + formulalist[slots[i]].getEffect());

                    }
                    string stringNE = Console.ReadLine();
                    if (Regex.Match(stringNE, @"\d+").Value == "")
                    {
                        Console.WriteLine("Please use a number");
                    }
                    else
                    {
                        int input = Convert.ToInt32(stringNE);
                        if ((0 <= D) && (D <= slotlength))
                        {
                            string[] yank = formulalist[slots[input]].geteffectarray();
                            int tech = formulalist[slots[input]].getEffectnum();
                            
                            int formlength = yank.Length;

                            if (tech == -2) //This doesn't work. This should add 2x AFTER Bestow not at the end of the formula.
                            {
                                List<string> bigyank = new List<string>();
                                int Bestownum = -2;
                                while(Bestownum == -2)
                                {
                                    for (int i = 0; i < formlength; i++)
                                    {
                                        if (yank[i] == "Bestow" || formulalist[slots[input]].geteffectarray()[i] == "bestow")
                                        {
                                            Bestownum = i;
                                        }
                                        
                                    }
                                }

                                for(int i = 0; i < Bestownum+1; i++)
                                {
                                    bigyank[i] = yank[i];
                                }
                                bigyank[Bestownum + 1] = "2";
                                for (int i = Bestownum+1; i < formlength; i++)
                                {
                                    bigyank[i+1] = yank[i];
                                }
                                formulalist[slots[input]].setEffectnum(2);
                                string yank2 = String.Join(" ", bigyank);
                                string[] yank3 = yank2.Split(" ");
                                formulalist[slots[input]].setEffect(yank2, yank3);


                            }
                            else
                            {
                                int step1 = Convert.ToInt32(yank[tech]);
                                step1++;
                                yank[tech] = "" + step1;
                                string yank2 = string.Join(" ",yank);

                                
                                formulalist[slots[input]].setEffect(yank2, yank);

                            }

                            
                            NEE -= formulalist[slots[input]].getLevel();
                            if (NEE < formulalist[slots[input]].getLevel())
                            {
                                slots.Remove(input);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong Number");
                        }

                    }
                }
            }
        }

        public string GetName()
        {
            return Name;
        }

        public Formula()
        {
        }
    }
}