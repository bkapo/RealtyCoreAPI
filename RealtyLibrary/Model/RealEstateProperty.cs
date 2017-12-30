using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealtyLibrary.Model
{
    public class RealEstateProperty
    {
        public int RealEstatePropertyId { get; set; }
        public Purpose Purpose { get; set; }
        public PropertyCategory PropertyCategory { get; set; }
        public PropertyType PropertyType { get; set; }
        public string SiteCode { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public decimal? SqFeetInterior { get; set; }
        public decimal? SqfFeetLand { get; set; }
        public int? Year { get; set; }
        public bool? Renovated { get; set; }
        public bool? NewConstruction { get; set; }
        public int? Rooms { get; set; }
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

        //For Rental
        public DateTime? RentStart { get; set; }
        public DateTime? RentEnd { get; set; }
        public bool? RentDamageDeposit { get; set; }
        public bool? Furnished { get; set; }

        //Distances from key points
        public string Orientation { get; set; }
        public int? DistanceFromVillage { get; set; }
        public int? DistanceFromCity { get; set; }
        public int? DistanceFromSea { get; set; }
        public int? DistanceFromAirport { get; set; }
        public bool? NearMetro { get; set; }

        //Map
        public string YoutubeURL { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
        public bool? UploadMapToRealEstatePortals { get; set; }

        //Navigation Properties
        public int? ResponsibleId { get; set; }
        [ForeignKey("ResponsibleId")]
        public InvolvedParty Responsible { get; set; }

        public int? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public InvolvedParty Owner { get; set; }

        public int? ProposedId { get; set; }
        [ForeignKey("ProposedId")]
        public InvolvedParty Proposed { get; set; }

    }


    public enum Purpose
    {
        Rental = 1,
        Sale = 2,
        RentalOrSale = 3
    }


    public enum PropertyCategory
    {
        Katoikia = 1,
        Epagelmatiko = 2,
        Oikopedo = 3,
        Loipa = 4,
    }

    public enum PropertyType
    {
        //Katoikia
        Diamerisma = 1,
        Villa = 2,
        Gkarsoniera,
        Orofodiaerisma,
        DiamerismaDublex,
        Mezoneta,
        Monokatoikia,
        Retire,
        Sigkrotima,
        Studio,
        Liomeno,
        //Epagelmatiko
        Katastima,
        Ktirio,
        Aithousa,
        ApothikeytikosXoros,
        BiomixanikosXoros,
        BiotexnikosXoros,
        Grafeio,
        Apothiki,
        DiafimistikosXoros,
        EkthesiakosXoros,
        EpaggelmatikosXoros,
        KtirioParkings,
        Oikia,
        OikopedoEpaggelmatiko,
        OrofodiaerismaEpaggelmatiko,
        //Oikopedo
        Ektasi,
        Oikopedo,
        Agrotemaxio,
        BiomixanikoOikopedo,
        Kthma,
        //Loipa
        EpenditikoAkinito,
        Epixirisi,
        Nisi,
        Ksenodoxio,
        Polikatikia,
        XorosStathmeysis,

        Loipa
    }
}
