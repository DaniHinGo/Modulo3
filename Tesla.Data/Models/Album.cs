using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tesla.Data.Models;

public class Album : BaseEntity<int>
{
    public string Name{get; set;} = String.Empty;
    public int Year{get;set;}
    public Genre Genre{get;set;} = Genre.Unknown;
    [ForeignKey("Artist")]
    public int ArtistId{get;set;}


    public virtual Artist? Artist{get;set;}
}
public enum Genre
{
    Synthpop,
    Punk,
    Hard_rock,
    Folk_Metal,
    Rock_alternatico,
    Rock,
    Punk_rock,
    Unknown
}
