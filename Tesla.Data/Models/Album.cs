using System;

namespace Tesla.Data.Models;

public class Album : BaseEntity<int>
{   
    public string Name { get; set; } = String.Empty;
    public int Year { get; set; }
    
    public Artist Artist { get; set; }
    public Gender Gender { get; set; } 

}
public enum Gender
{
    Synthpop,
    Punk,
    Hard_rock,
    Folk_Metal,
    Rock_alternatico,
    Rock,
    Punk_rock
}
