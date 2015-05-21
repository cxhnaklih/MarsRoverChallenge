using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Routing;

namespace MarsRoverChallengeWebApi.Models
{
   
    public class PlateauModel
    {
        [Key]
        public int PlateauId { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
    }

    public class MarsRoverModel
    {
        [Key]
        public int MarsRoverId { get; set; }
        [Required]
        //Foreign Key
        public int PlateauId { get; set; }
        [RegularExpression(("[nsewNSEW]{1}"))]
        public string Direction { get; set; }
        public string Name { get; set; }
        public int InitialX { get; set; }
        public int InitialY { get; set; }
       
        
    }

   public class MarsRoverDestinationModel
    {
       public int X { get; set; }
       public int Y { get; set; }
       public string Direction { get; set; }
    }

}