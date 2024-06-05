using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTrackerClient.Models.DTOs;

internal struct MarkerDTO
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Name { get; set; }
    public string Guid { get; set; }
}
