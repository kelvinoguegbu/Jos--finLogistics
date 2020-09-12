using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace José_finLogistics.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }
        public string TrackingId { get; set; }
        public PackageStatus PackageStatus { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }

    public enum PackageStatus
    {
        In_Process,
        In_Transit,
        Delivered
    }
}