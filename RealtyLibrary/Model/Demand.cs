using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyLibrary.Model
{
    public class Demand
    {
        public int DemandId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Comments { get; set; }
        public bool Active { get; set; }

        public Purpose Purpose { get; set; }
        public PropertyCategory PropertyCategory { get; set; }
        public PropertyType PropertyType { get; set; }

        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public decimal SqFeetInteriorFrom { get; set; }
        public decimal SqFeetInteriorTo { get; set; }

        public decimal? SqfFeetLand { get; set; }
        public int Year { get; set; }
        public bool Renovated { get; set; }
        public bool NewConstruction { get; set; }
        public int Rooms { get; set; }
        public int? NoOfKitchen { get; set; }
        public int? FullBedrooms { get; set; }
        public int? HalfBedrooms { get; set; }
        public decimal? SemiOutdoorSpaces { get; set; }
        public bool? LegalSemiOutdoorSpaces { get; set; }
        public int? Levels { get; set; }
        public int? FirePlaces { get; set; }
        public bool? EnergyPerformanceCertificates { get; set; }
        public bool? Mortgage { get; set; }
        public bool? HolidayHome { get; set; }
        public bool? StoneHome { get; set; }
        public bool? NeoClassicalHouse { get; set; }
        public bool? TraditionalHouse { get; set; }
        public bool? ListedBuildings { get; set; }
        public bool? LuxuryHouse { get; set; }
        public bool? Penthouse { get; set; }

        //Parking
        public int? Parkings { get; set; }
        public bool? ClosedParking { get; set; }
        public bool? HeatedParking { get; set; }

        //Additional Rooms
        public bool? Basement { get; set; }
        public bool? FitnessRoom { get; set; }
        public bool? HomeTheatre { get; set; }
        public bool? Library { get; set; }
        public bool? Spa { get; set; }
        public bool? WineCellar { get; set; }
        public bool? GuestSuite { get; set; }
        public bool? HomeOffice { get; set; }
        public bool? Attic { get; set; }

        //Heating
        public bool? AC { get; set; }
        public bool? SolarHeating { get; set; }
        public bool? FloorHeating { get; set; }
        public bool? Boiler { get; set; }

        //Security
        public bool? SafetyDoor { get; set; }
        public bool? Alarm { get; set; }
        public bool? SafetyDepositBox { get; set; }
        public bool? VideoDoorPhone { get; set; }

        //Other
        public bool? Terraces { get; set; }
        public bool? InternalStairs { get; set; }
        public bool? Corner { get; set; }
        public bool? IndoorBBQ { get; set; }
        public bool? Elevator { get; set; }
        public bool? SatteliteTV { get; set; }
        public bool? DoubleWindows { get; set; }
        public bool? TripleWindows { get; set; }
        public bool? Internet { get; set; }
        public bool? AnimalFriendly { get; set; }
        public bool? StudentsHousing { get; set; }
        public bool? WithoutCharges { get; set; }

        //Surrounding Area
        public bool? SportField { get; set; }
        public bool? Grass { get; set; }
        public bool? Trees { get; set; }
        public bool? OutdoorPool { get; set; }
        public bool? OutdoorPoolHeated { get; set; }
        public bool? IndoorPool { get; set; }
        public bool? IndoorPoolHeated { get; set; }
        public bool? Garden { get; set; }
        public bool? OutdoorBBQ { get; set; }
        public bool? ElectronicGates { get; set; }
        public bool? AutomaticWatering { get; set; }

        //Navigation Properties
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public InvolvedParty Customer { get; set; }

        public int ResponsibleId { get; set; }
        [ForeignKey("ResponsibleId")]
        public InvolvedParty Responsible { get; set; }

    }
}
