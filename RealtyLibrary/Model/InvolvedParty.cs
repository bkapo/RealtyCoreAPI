using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyLibrary.Model
{
    public class InvolvedParty
    {
        public int InvolvedPartyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Sizigou { get; set; }
        public string WorkTelephone { get; set; }
        public string Mobile { get; set; }
        public string HomeTelephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string website { get; set; }
        public string AFM { get; set; }
        public string IDNumber { get; set; }
        public InvolvedPartyType InvolvedPartyType { get; set; }

        //Navigation Property
        [InverseProperty("Customer")]
        public List<Demand> Demands { get; set; }

        [InverseProperty("Responsible")]
        public List<Demand> AgentDemands { get; set; }
    }

    public enum InvolvedPartyType {
        Agent = 1 , 
        Contact = 2, 
        Collaborator = 3,
        Owner = 4,
        Customer = 5
    }
}
