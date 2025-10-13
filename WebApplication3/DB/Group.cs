using System;
using System.Collections.Generic;

namespace WebApplication3.DB;

public partial class Group
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? IdSpecial { get; set; }

    public virtual Special? IdSpecialNavigation { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
