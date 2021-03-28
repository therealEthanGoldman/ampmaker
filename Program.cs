using System;
using System.Text.RegularExpressions;



/*
 * This is TomCon my app
 * Patch 0.1 Added Suffix, Prefix and the like.
 * Patch 0.1.1 Added Timing After effects
 * Patch 0.2 Added NEUpgrade
 * Patch 0.9 Its done?
 * 
 * Future updates:
 * NUMERICAL EFFECT ONLY WORKS ON BESTOW
 * Use Arrays to make variety and quanity possible
 * Create a way to isolate a number fron a string (UNLESS that number is followed by " minutes rp") and save the preceding and previous effects into seperate variables, then reassemble. OR to add "X 2" or other appropiate modifies to the end of a effect
 * Use a loop to create a means for NEE to be added to individual procedure calls in that array.
 * 
 * Add Engineering calculator for giggles.
 * 
 * Convert this to a html app
 * 
 * Convert this to a mobile app
 * 
 * Find a way to create a database of procedures and modifiers so one can assemble amps out of techniques you already have possible. As well as track technician and theory level.
 * 
 * Get a cool logo
 * 
 */

namespace Ampmaker
{
    class Program
    {

        static void Main(string[] args)
        {
            Formula amp = new Formula();


                        amp.WriteMultiFormula();

                        Console.WriteLine("");
                        Console.WriteLine("The Amp is now complete");
                        Console.WriteLine(amp.GetLabel());
                        Console.WriteLine("The total theory level needed is: " + amp.GetComplexity() + " to develop, " + amp.GetTheoryLevel() + " to learn.");
                        Console.WriteLine("The total cost is: " + amp.GetBuildCost() + " Common Equivalents");
                        Console.WriteLine("Thanks for Playing");


        }
    }
}






