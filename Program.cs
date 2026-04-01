using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

class IntergalacticCinema
{
    static string Normalize(string s)
    {
        if (string.IsNullOrEmpty(s)) return "";
        s = s.ToLowerInvariant();
        var normalized = s.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var ch in normalized)
        {
            var cat = CharUnicodeInfo.GetUnicodeCategory(ch);
            if(cat != UnicodeCategory.NonSpacingMark) sb.Append(ch);
        }
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    static void Main()
    {
        Console.Write("Enter age: ");
        int age = int.Parse(Console.ReadLine() ?? "0");
        
        Console.Write("Enter movie type (release, classic, 3d, marathon, special): ");
        string movieType = Console.ReadLine() ?? "release";

        Console.Write("Enter day (monday, tuesday,wednesday,thursday,friday,saturday,sunday): ");
        string day = Console.ReadLine() ?? "monday";

        Console.Write("Enter time (morning, afternoon, night): ");
        string time = Console.ReadLine() ?? "night";

        Console.Write("Enter membership (none, silver, gold, platinum): ");
        string membership = Console.ReadLine() ?? "none";

        Console.Write("Is promo active? (true/false): ");
        bool promoActive = bool.Parse(Console.ReadLine() ?? "false");

        Console.Write("Is student? (true/false): ");
        bool student = bool.Parse(Console.ReadLine() ?? "false");

        Console.Write("Couple ticket? (true/false): ");
        bool couple = bool.Parse(Console.ReadLine() ?? "false");

        Console.Write("Enter base price: ");
        decimal basePrice = decimal.Parse(Console.ReadLine() ?? "0");
        
        
        movieType = Normalize(movieType);
        day = Normalize(day);
        time = Normalize(time);
        membership = Normalize(membership);
        
        decimal discontAccumulator = 0m;
        decimal overrideDiscont = -1m;
        bool blockMembership =  false;
        List<string> reasons = new List<string>();

        if (age < 12)
        {
            if (movieType == "special")
            {
                Console.WriteLine("Entry denied: Kids cannot attend special functions");
                return;
            }

            if (movieType == "classic" && (day == "monday" || day == "wednesday"))
            {
                overrideDiscont = 100m;
                reasons.Add($"Kid: free for {movieType} on Monday/Wednesday");
            }
            else if (movieType == "3d" && time != "night")
            {
                discontAccumulator += 70m;
                reasons.Add("Kid: 3D before 6 PM, pays 30% (70% off)");
            }
        }
        else if (age < 18)
        {
            if (movieType == "classic" && day == "wednesday")
            {
                discontAccumulator += 50m;
                reasons.Add("Teen: Classic on Wednesday, Pays 50%.");
            }
        }
        else if (age >= 60)
        {
            discontAccumulator += 40m;
            reasons.Add("Senior: 40% discount");
            if (movieType == "marathon")
            {
                overrideDiscont = 50m;
                reasons.Add("Senior: Marathon fixed 50%.");
            }

            if (day == "sunday" && promoActive)
            {
                overrideDiscont = 70m;
                reasons.Add("Senior: Sunday promo 70%.");
            }
        }

        if (day == "wednesday" && movieType != "special")
        {
            discontAccumulator += 20m;
            reasons.Add("Wednesday global 20% off.");
        }

        if ((day == "friday" || day == "saturday") && time == "night")
        {
            blockMembership = true;
            reasons.Add("Friday/Saturday night: membership discounts blocked");
        }

        bool release_constrain = false;
        if (movieType == "special")
        {
            if (age < 18)
            {
                Console.WriteLine("Entry denied: Only adults and seniors can attend special functions");
                return;
            }
            overrideDiscont = 0m;
            reasons.Add("Special fuctions: no discount allowed");
        }

        if (movieType == "release")
        {
            release_constrain = true;
            discontAccumulator = 0m;
            overrideDiscont = -1m;
            reasons.Add("Release: Only student (15%) or platinum(35%) allowed");
        }

        if (movieType == "marathon" && age < 60)
        {
            discontAccumulator += 20m;
            reasons.Add("Marathon: 20% discount");
        }

        if (!blockMembership)
        {
            if (membership == "silver" && age <= 17 && movieType == "3d" && day != "sunday")
            {
                discontAccumulator += 20m;
                reasons.Add("Silver membership: teen 20% off 3D (not Sunday).");
            }

            if (membership == "gold" && age >= 18 && age < 60)
            {
                if (movieType == "classic")
                {
                    discontAccumulator += 25m;
                    reasons.Add("Gold membership: gold 25% off classic.");
                }

                if (movieType == "3d" && day != "sunday")
                {
                    discontAccumulator += 15m;
                    reasons.Add("Gold membership: 15% off 3D (not Sunday).");
                }
            }

            if (membership == "platinum" && age >= 18 && age < 60)
            {
                if (!(movieType == "release" && day == "saturday" && time == "night"))
                {
                    discontAccumulator += 35m;
                    reasons.Add("Platinum: release on Saturday night pays full price");
                }
                else
                {
                    reasons.Add("Platinum: release on Saturday night pays full price");
                }
            }
        }

        if (release_constrain && !(student || membership == "platinum"))
        {
            discontAccumulator += 0m;
            reasons.Add("Release : no discount (not student or platinum).");
        }

        if (student)
        {
            if (movieType == "release")
            {
                discontAccumulator += 15m;
                reasons.Add("Student: 15% off release.");
            }

            if (day == "monday" || day == "wednesday")
            {
                discontAccumulator += 10m;
                reasons.Add("Student: extra 10% on Monday/Wednesday.");
            }
        }

        if (promoActive)
        {
            if (day == "sunday")
            {
                if (movieType == "special")
                {
                    discontAccumulator += 10m;
                    reasons.Add("Sunday promo: extra 10% off special.");
                }
            }
            else
            {
                if (membership == "silver" || membership == "gold" || membership == "platinum")
                {
                    discontAccumulator += 5m;
                    reasons.Add("Promo active + membership (Silver+): +5%.");
                }
            }
        }
        
        bool coupleDiscount = false;

        if (couple && day != "sunday")
        {
            coupleDiscount = true;
            reasons.Add("Couple: second ticket 50% off.");
        }
        
        decimal finalDiscount = overrideDiscont >= 0 ? overrideDiscont : Math.Min(100m,discontAccumulator);
        decimal priceAfter = basePrice * (1m -  finalDiscount / 100m);

        if (movieType == "3d")
        {
            priceAfter *= 1.10m;
            reasons.Add("3D surcharge: +10% after discounts.");
        }
        
        Console.Clear();
        decimal ticketPrice = Math.Round(priceAfter, 2);
        decimal finalPrice = couple 
            ? (coupleDiscount ? ticketPrice + Math.Round(ticketPrice * 0.5m, 2) : ticketPrice * 2) : ticketPrice;
        Console.WriteLine("\n--- INTERGALACTIC CINEMA RECEIPT ---");
        Console.WriteLine($"Base price: ${basePrice:0.00}");
        Console.WriteLine($"Age: {age}, Type: {movieType}, Day: {day}, Time: {time}, Membership: {membership}");
        Console.WriteLine("\nDiscount reasons:");
        foreach (var r in reasons)
            Console.WriteLine(" - " + r);
        Console.WriteLine($"\nTotal discount applied: {finalDiscount}%");
        Console.WriteLine($"Final ticket price: ${ticketPrice:0.00}");
        if (couple)
            Console.WriteLine($"Total to pay (couple): ${finalPrice:0.00}");
        else
            Console.WriteLine($"Total to pay: ${finalPrice:0.00}");
        Console.WriteLine("------------------------------------");
    }
    
    
}

