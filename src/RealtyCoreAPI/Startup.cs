using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RealtyCoreAPI.Repository;
using RealtyLibrary.Model;

namespace RealtyCoreAPI
{
	public class Startup
	{

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			//services.AddApplicationInsightsTelemetry(Configuration);

			services.AddCors(options =>

			 {
				 options.AddPolicy("AllowAllOrigins",
								   builder => builder.AllowAnyOrigin()
												 .AllowAnyMethod()
												.AllowAnyHeader()
								   .AllowCredentials());
			 });

			//InMemeory DB
            services.AddDbContext<RealEstateRepository>(opt => opt.UseInMemoryDatabase("RealtyDB"));

            services.AddMvc().AddJsonOptions(options =>
            {
            	options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });


			services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigins"));
			});


			services.AddSwaggerGen(options =>
			{
				options.SingleApiVersion(new Swashbuckle.Swagger.Model.Info
				{
					Version = "v1",
					Title = "Real Estate CORE RestAPI",
					Description = "Real Estate Rest API (ASP.NET Core Web Api) - Api App",
					TermsOfService = "None"
				});
				options.DescribeAllEnumsAsStrings();

                //Determine base path for the application

                var basePath = System.AppContext.BaseDirectory;

				//Set the comments path for the swagger json an ui
				// options.IncludeXmlComments(basePath + "\\RealEstate.CoreWebAPI.xml");

				//options.OperationFilter(new Swashbuckle.SwaggerGen.XmlComments.ApplyXmlActionComments(string.Format(@"{0}\bin\RealEstate.CoreWebAPI.XML", System.AppDomain.CurrentDomain.BaseDirectory)));
				//options.ModelFilter(new Swashbuckle.SwaggerGen.XmlComments.ApplyXmlTypeComments(string.Format(@"{0}\bin\RealEstate.CoreWebAPI.XML", System.)));
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();

			//seeding with test data on startup
			var context = app.ApplicationServices.GetService<RealEstateRepository>();
			AddTestData(context);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

			}

			// global policy - assign here or on each controller
			app.UseCors("AllowAllOrigins");

			app.UseMvc();

			app.UseSwagger();
			app.UseSwaggerUi();
		}


		private static void AddTestData(RealEstateRepository context)
		{
            //Customer
            var _inv1 = new InvolvedParty
            {
                InvolvedPartyId = 1,
                FirstName = "Bill",
                LastName = "Customer",
                InvolvedPartyType = InvolvedPartyType.Customer,
                AFM = "1234567890",
                FatherName = "George",
                Email = "billkapo@gmail.com",
                Mobile = "6997791425"
            };
            context.InvolvedParties.Add(_inv1);
            context.SaveChanges();

            //Agent
            InvolvedParty _inv2 = new InvolvedParty
            {
                InvolvedPartyId = 2,
                FirstName = "Bill",
                LastName = "Agent",
                InvolvedPartyType = InvolvedPartyType.Agent,
                AFM = "5555555555",
                FatherName = "George",
                Email = "kapo@gmail.com",
                Mobile = "699999999"
            };
            context.InvolvedParties.Add(_inv2);
            context.SaveChanges();

            //Owner
            InvolvedParty _inv3 = new InvolvedParty
            {
                InvolvedPartyId = 3,
                FirstName = "Mark",
                LastName = "Smith-Owner",
                InvolvedPartyType = InvolvedPartyType.Owner,
                AFM = "88888888",
                FatherName = "Klok",
                Email = "Klok@gmail.com",
                Mobile = "455555555"
            };
            context.InvolvedParties.Add(_inv3);
            context.SaveChanges();

            //Agent
            context.InvolvedParties.Add(new InvolvedParty
            {
                InvolvedPartyId = 4,
                FirstName = "Nick",
                LastName = "Johnson-agent",
                InvolvedPartyType = InvolvedPartyType.Agent
            });

            //Owner
            InvolvedParty _inv5 = new InvolvedParty
            {
                InvolvedPartyId = 5,
                FirstName = "George",
                LastName = "Kapo-owner",
                InvolvedPartyType = InvolvedPartyType.Owner,
                AFM = "234234234",
                FatherName = "Bill",
                Email = "george@gmail.com",
                Mobile = "111111111"
            };
            context.InvolvedParties.Add(_inv5);
            context.SaveChanges();

            //Demand of Customerid = 1 - Agentid = 2
            Demand _dm = new Demand
            {
                DemandId = 1,
                UserId = "Bill",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Comments = "New demand for mr....",
                Active = true,
                PropertyCategory = PropertyCategory.Katoikia,
                PropertyType = PropertyType.Diamerisma,
                PriceFrom = 300,
                PriceTo = 700,
                SqFeetInteriorFrom = 90,
                SqFeetInteriorTo = 130,
                Customer = _inv1,
                CustomerId = _inv1.InvolvedPartyId,
                Purpose = Purpose.Rental,
                Responsible = _inv2,
                ResponsibleId = _inv2.InvolvedPartyId
            };
            context.Demands.Add(_dm);
			context.SaveChanges();

            context.RealEstateProperties.Add(new RealEstateProperty
            {
                RealEstatePropertyId = 555,
                Purpose = Purpose.RentalOrSale,
                PropertyCategory = PropertyCategory.Katoikia,
                PropertyType = PropertyType.Diamerisma,
                Title = "Διαμέρισμα Lux",
                SiteCode = "E555",
                SqFeetInterior = 120,
                SqfFeetLand = 200,
                Price = 550,
                Year = 2010,
                Renovated = false,
                NewConstruction = true,
                Rooms = 4,
                NoOfKitchen = 2,
                FullBedrooms = 3,
                HalfBedrooms = 2,
                AC = true,
                Alarm = true,
                AnimalFriendly = false,
                Basement = true,
                Attic = true,
                Boiler = true,
                ClosedParking = true,
                Trees = true,
                VideoDoorPhone = true,
                SolarHeating = true,
                IndoorPool = false,
                OutdoorPool = true,
                SafetyDepositBox = true,
                ResponsibleId = _inv2.InvolvedPartyId,
                Responsible = _inv2,
                OwnerId = _inv3.InvolvedPartyId,
                Owner = _inv3
            });

            context.RealEstateProperties.Add(new RealEstateProperty
            {
                RealEstatePropertyId = 444,
                Purpose = Purpose.Sale,
                PropertyCategory = PropertyCategory.Katoikia,
                PropertyType = PropertyType.Villa,
                Title = "Villa Relax Lifestyle",
                SiteCode = "E444",
                SqFeetInterior = 220,
                SqfFeetLand = 100,
                Price = 250000,
                Year = 2015,
                Renovated = false,
                NewConstruction = true,
                Rooms = 5,
                NoOfKitchen = 2,
                FullBedrooms = 3,
                HalfBedrooms = 2,
                AC = true,
                Alarm = true,
                AnimalFriendly = false,
                Basement = true,
                Attic = true,
                Boiler = true,
                ClosedParking = true,
                Trees = true,
                VideoDoorPhone = true,
                SolarHeating = true,
                IndoorPool = false,
                OutdoorPool = true,
                SafetyDepositBox = true,
                ResponsibleId = _inv2.InvolvedPartyId,
                Responsible = _inv2,
                OwnerId = _inv5.InvolvedPartyId,
                Owner = _inv5
            });

            context.SaveChanges();
        }

	}
}
