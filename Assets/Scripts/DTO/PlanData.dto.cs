using System.Collections.Generic;
namespace DataTypes
{

    public class PlanData
    {
        public bool ok;
        public Plan[] plans;
        public int pages;
    }

    public class Plan
    {
        public int id;
        public string title;
        public PlanTag tag;
        public string start;
        public string end;
        public string city;
        public int totalCost;
        public PlanTravel[] travels;
    }

    public class PlanTag : Dictionary<string, string[]> { }

    public class PlanTravel
    {
        public int id;
        public string startDay;
        public TravelDestination destination;
    }

    public class TravelDestination
    {
        public string title;
        public string firstImage;
    }
}