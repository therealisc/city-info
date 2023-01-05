using CityInfo.API.Models;

namespace CityInfo.API;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }
    public static CitiesDataStore Current { get; } = new CitiesDataStore();

    public CitiesDataStore()
    {
        Cities = new List<CityDto>()
        {
            new CityDto()
            {
                Id = 1, 
                Name = "New York City",
                Description = "The one with that big park.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1, 
                        Name = "Central Park",
                        Description = "The most visited urban park in the United States."
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "Empire State Building",
                        Description = "A 102-story sckyscraper located in Midtown Manhattan."
                    }
                }
            },
            new CityDto()
            {
                Id = 2, 
                Name = "Antwerp",
                Description = "The one with the cathedral that that was never really finished",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 3, 
                        Name = "Cathedral of Our Lady",
                        Description = "A Gothic style cathedral"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 4,
                        Name = "Antwerp Central Station",
                        Description = "The finest example of railway architecture in Belgium."
                    }
                }
            },
            new CityDto()
            {
                Id = 3, 
                Name = "Paris",
                Description = "The one with that big tower.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 5, 
                        Name = "Eiffel Tower",
                        Description = "Iron lattice tower on the Champ de Mars."
                    },
                    new PointOfInterestDto()
                    {
                        Id = 6,
                        Name = "Empire State Building",
                        Description = "The world's largest museum."
                    }
                }
            },
        };
    }
}
