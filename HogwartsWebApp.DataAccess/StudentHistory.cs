using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HogwartsWebApp.DataAccess;

public partial class StudentHistory
{
    public int StudentHistoryId { get; set; }

    public int Student { get; set; }

    [RegularExpression(@"^[1-4]{1}$", ErrorMessage = "Solo existen 4 casas en Hogwarts")]
    public int House { get; set; }

    public int Status { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime LastestUpdate { get; set; }

    [JsonIgnore]
    [ForeignKey("House")]
    public virtual House HouseFK { get; set; }

    [JsonIgnore]
    [ForeignKey("Status")]
    public virtual Status StatusFK { get; set; }

    [JsonIgnore]
    [ForeignKey("Student")]
    public virtual Student StudentFK { get; set; }
}
