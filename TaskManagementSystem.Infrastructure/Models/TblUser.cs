using System;
using System.Collections.Generic;

namespace TaskManagementSystem.Infrastructure.Models;

public partial class TblUser
{
    public int UserId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
