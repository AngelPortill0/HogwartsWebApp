using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HogwartsWebApp.DataAccess;

public partial class House
{
    public int HouseId { get; set; }

    public string Description { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<StudentHistory> StudentHistories { get; } = new List<StudentHistory>();
}
