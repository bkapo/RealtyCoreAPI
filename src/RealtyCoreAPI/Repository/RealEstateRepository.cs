using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealtyLibrary.Model;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;

namespace RealtyCoreAPI.Repository
{
    public class RealEstateRepository: DbContext, IRealEstateRepository
    {

        public RealEstateRepository(DbContextOptions<RealEstateRepository> options)
            : base(options)
        { }

        public DbSet<InvolvedParty> InvolvedParties { get; set; }
        public DbSet<RealEstateProperty> RealEstateProperties { get; set; }
        public DbSet<Demand> Demands { get; set; }

        #region Involved Party

        public IEnumerable<InvolvedParty> InvolvedPartyGetAll()
        {
            var res = InvolvedParties.Include(x => x.Demands);
            return res.ToList();
        }

        public InvolvedParty InvolvedPartyFind(int id)
        {
           return  InvolvedParties.Include(x => x.Demands).Where(x => x.InvolvedPartyId == id).FirstOrDefault();
        }

        public IEnumerable<InvolvedParty> InvolvedPartyFindByName(string name)
        {
            return InvolvedParties.Include(x => x.Demands).Where(x => x.LastName.ToUpper().StartsWith(name.ToUpper()));
        }

        public IEnumerable<InvolvedParty> InvolvedPartyFindByType(InvolvedPartyType typeId)
        {
            return InvolvedParties.Include(x => x.Demands).Where(x => x.InvolvedPartyType == typeId);
        }

        public IEnumerable<InvolvedParty> InvolvedPartyFindByNameAndType(InvolvedPartyType typeId, string name)
        {
            return InvolvedParties.Include(x => x.Demands).Where(x => x.InvolvedPartyType == typeId && x.LastName.ToUpper().StartsWith(name.ToUpper()));
        }

        public void InvolvedPartyAdd(InvolvedParty item)
        {
            InvolvedParties.Add(item);
        }

        public void InvolvedPartyUpdate(InvolvedParty item)
        {
            //_ip[item.Key] = item;
        }

        public void InvolvedPartyRemove(int id)
        {
            InvolvedParties.Remove(InvolvedPartyFind(id));
        }

        #endregion

        #region RealEstateProperty


        public IEnumerable<RealEstateProperty> RealEstatePropertyGetAll()
        {

            var res = RealEstateProperties
                .Include(x => x.Responsible)
                .Include(x => x.Owner)
                .Include(x => x.Proposed);

            return res.ToList();
        }

        public IEnumerable<RealEstateProperty> RealEstatePropertyOfCustomer(int ownerid)
        {
            return RealEstateProperties
                .Include(x => x.Responsible)
                .Include(x => x.Owner)
                .Include(x => x.Proposed)               
                .Where(x=>x.OwnerId == ownerid);
        }


        public IEnumerable<RealEstateProperty> RealEstatePropertyOfAgent(int agentid)
        {
            return RealEstateProperties
                .Include(x => x.Responsible)
                .Include(x => x.Owner)
                .Include(x => x.Proposed)
                .Where(x=>x.ResponsibleId == agentid);
        }

        public IEnumerable<RealEstateProperty> RealEstatePropertySearch(string src)
        {
            return RealEstateProperties
                .Include(x => x.Responsible)
                .Include(x => x.Owner)
                .Include(x => x.Proposed)
                .Where(x=>x.SiteCode.ToUpper()
                .Contains(src.ToUpper()) || x.RealEstatePropertyId.ToString().ToUpper() == src.ToUpper());
        }

        public RealEstateProperty RealEstatePropertyFindbyId(string id)
        {
            return RealEstateProperties
                .Include(x => x.Responsible)
                .Include(x => x.Owner)
                .Include(x => x.Proposed)
                .Where(x => x.RealEstatePropertyId.ToString() == id).FirstOrDefault();
        }

        public void RealEstatePropertyRemove(string id)
        {
            throw new NotImplementedException();
        }


        public void RealEstatePropertyAdd(RealEstateProperty item)
        {
            RealEstateProperties.Add(item);
        }

        public void RealEstatePropertyUpdate(RealEstateProperty item)
        {
            //throw new NotImplementedException();
        } 
        #endregion


        public Demand DemandGet(int id)
        {
            return Demands
                .Include(d => d.Customer)
                .Include(d => d.Responsible)
                .Where(d => d.DemandId == id).FirstOrDefault();
        }

        public IEnumerable<Demand> DemandList(int invpartyId, InvolvedPartyType invType)
        {
            if (invType == InvolvedPartyType.Customer)
            {
                return Demands
                    .Include(d => d.Customer)
                    .Include(d => d.Responsible)
                    .Where(d => d.CustomerId == invpartyId).ToList();
            }
            else
            {
                return Demands
                    .Include(d => d.Customer)
                    .Include(d => d.Responsible)
                    .Where(d => d.ResponsibleId == invpartyId).ToList();
            }
        }

        public void DemandUpdate(int id, Demand dm)
        {
            //return Demands.Include(d => d.Customer).Where(d => d.DemandId == id).FirstOrDefault();
        }

		public void DemandAdd(Demand dm)
		{
			Demands.Add(dm);
		}

    }
}
