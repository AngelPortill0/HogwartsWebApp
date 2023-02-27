using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HogwartsWebApp.DataAccess;

public partial class Status
{
    public int StatusId { get; set; }

    public string Description { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<StudentHistory> StudentHistories { get; } = new List<StudentHistory>();
}
