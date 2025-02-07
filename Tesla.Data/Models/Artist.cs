using System.Reflection.Metadata.Ecma335;
using Tesla.Data.Models;

namespace Tesla.Data.Models;

public class Artist : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public bool IsOnTour { get; set; }
}