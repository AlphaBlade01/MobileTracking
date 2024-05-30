namespace MobileTrackerServer.Models.DTOs;

internal struct MarkerDTO
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Name { get; set; }
    public string Guid { get; set; }
}
