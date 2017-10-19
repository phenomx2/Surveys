using System;

namespace Surveys.Entities
{
    public class Survey
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int TeamId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public override string ToString()
        {
            return $"{Name} | {BirthDate} | {TeamId} | {Latitude} | {Longitude}";
        }
    }
}