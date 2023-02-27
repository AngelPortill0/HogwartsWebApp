using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HogwartsWebApp.DataAccess;

public partial class Student
{
    public int StudentId { get; set; }

    [StringLength(20)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string LastName { get; set; } = null!;

    [RegularExpression(@"^[0-9]{1,2}$", ErrorMessage = "Solo se permiten digitos: min. 1 - max. 2")]
    public int Age { get; set; }

    [RegularExpression(@"^[0-9]{1,10}$", ErrorMessage = "Solo se permiten digitos: min. 1 - max. 10")]
    public int IdentityNumber { get; set; }

    public virtual ICollection<StudentHistory> StudentHistories { get; } = new List<StudentHistory>();
}
