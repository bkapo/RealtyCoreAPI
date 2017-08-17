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
			var _inv1 = new InvolvedParty();
			_inv1.InvolvedPartyId = 1;
			_inv1.FirstName = "Bill";
			_inv1.LastName = "Customer";
			_inv1.InvolvedPartyType = InvolvedPartyType.Customer;
			_inv1.AFM = "1234567890";
			_inv1.FatherName = "George";
			_inv1.Email = "billkapo@gmail.com";
			_inv1.Mobile = "6997791425";

			context.InvolvedParties.Add(_inv1);
			context.SaveChanges();

			//Agent
			InvolvedParty _inv2 = new InvolvedParty();
			_inv2.InvolvedPartyId = 2;
			_inv2.FirstName = "Bill";
			_inv2.LastName = "Agent";
			_inv2.InvolvedPartyType = InvolvedPartyType.Agent;
			_inv2.AFM = "5555555555";
			_inv2.FatherName = "George";
			_inv2.Email = "kapo@gmail.com";
			_inv2.Mobile = "699999999";

			context.InvolvedParties.Add(_inv2);
			context.SaveChanges();

			//Owner
			InvolvedParty _inv3 = new InvolvedParty();
			_inv3.InvolvedPartyId = 3;
			_inv3.FirstName = "Mark";
			_inv3.LastName = "Smith-Owner";
			_inv3.InvolvedPartyType = InvolvedPartyType.Owner;
			_inv3.AFM = "88888888";
			_inv3.FatherName = "Klok";
			_inv3.Email = "Klok@gmail.com";
			_inv3.Mobile = "455555555";

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
			InvolvedParty _inv5 = new InvolvedParty();
			_inv5.InvolvedPartyId = 5;
			_inv5.FirstName = "George";
			_inv5.LastName = "Kapo-owner";
			_inv5.InvolvedPartyType = InvolvedPartyType.Owner;
			_inv5.AFM = "234234234";
			_inv5.FatherName = "Bill";
			_inv5.Email = "george@gmail.com";
			_inv5.Mobile = "111111111";

			context.InvolvedParties.Add(_inv5);
			context.SaveChanges();

			//Demand of Customerid = 1 - Agentid = 2
			Demand _dm = new Demand();
			_dm.DemandId = 1;
			_dm.UserId = "Bill";
			_dm.CreatedDate = DateTime.Now;
			_dm.ModifiedDate = DateTime.Now;
			_dm.Comments = "New demand for mr....";
			_dm.Active = true;
			_dm.PropertyCategory = PropertyCategory.Katoikia;
			_dm.PropertyType = PropertyType.Diamerisma;
			_dm.PriceFrom = 300;
			_dm.PriceTo = 700;
			_dm.SqFeetInteriorFrom = 90;
			_dm.SqFeetInteriorTo = 130;
			_dm.Customer = _inv1;
			_dm.CustomerId = _inv1.InvolvedPartyId;
			_dm.Purpose = Purpose.Rental;
			_dm.Responsible = _inv2;
			_dm.ResponsibleId = _inv2.InvolvedPartyId;

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
