using System;
using System.Collections.Generic;

namespace WebApplication3.DB;

public partial class Special
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
