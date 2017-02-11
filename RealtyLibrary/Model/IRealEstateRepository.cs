using System.Collections.Generic;

namespace RealtyLibrary.Model
{
    public interface IRealEstateRepository
    {

        //InvolvedParty
        void InvolvedPartyAdd(InvolvedParty item);
        IEnumerable<InvolvedParty> InvolvedPartyGetAll();
        InvolvedParty InvolvedPartyFind(int id);
        IEnumerable<InvolvedParty> InvolvedPartyFindByType(InvolvedPartyType typeId);
        IEnumerable<InvolvedParty> InvolvedPartyFindByName(string name);
        IEnumerable<InvolvedParty> InvolvedPartyFindByNameAndType(InvolvedPartyType typeId, string name);
        void InvolvedPartyRemove(int id);
        void InvolvedPartyUpdate(InvolvedParty item);

        //RealEstateProperty
        void RealEstatePropertyAdd(RealEstateProperty item);
        IEnumerable<RealEstateProperty> RealEstatePropertyGetAll();
        IEnumerable<RealEstateProperty> RealEstatePropertyOfCustomer(int ownerid);
        IEnumerable<RealEstateProperty> RealEstatePropertyOfAgent(int agentid);
        RealEstateProperty RealEstatePropertyFindbyId(string id);

        IEnumerable<RealEstateProperty> RealEstatePropertySearch(string src);

        void RealEstatePropertyRemove(string id);
        void RealEstatePropertyUpdate(RealEstateProperty item);

        //Demands
        Demand DemandGet(int id);
		void DemandAdd(Demand item);
        void DemandUpdate(int id, Demand dm);
        IEnumerable<Demand> DemandList(int invpartyId, InvolvedPartyType inv);
    }
}
