using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace José_finLogistics.Models
{
    public class Client
    {
        /* Clients Information */
        [Key]
        public int ClientId { get; set; }

        [DisplayName("First name")]
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is Required")]
        public string Phone { get; set; }



        /***********************/
        /* Pickup information */
        [DisplayName("Pickup Address")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Pickup Address is Required")]
        public string PickupAddress { get; set; }

        [DisplayName("Item Description")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Item Description is Required")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "Item Quantity is Required")]
        public int ItemQuantity { get; set; }

        /***********************/
        /*  Destination info   */

        [DisplayName("Receiver Full Name")]
        [Required(ErrorMessage = "Full Name is Required")]
        public string FullName { get; set; }

        [DisplayName("Receiver Phone")]
        [Required(ErrorMessage = "Receiver Phone number is Required")]
        public string ReceiverPhone { get; set; }

        [DisplayName("Receiver Address")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Receiver Address is Required")]
        public string ReceiverAddress { get; set; }


        //Since each patient record is usually related to a doctor, the list is required here as well
        public virtual ICollection<Package> Package { get; set; }
    }
}