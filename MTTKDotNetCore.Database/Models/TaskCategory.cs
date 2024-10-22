﻿using System;
using System.Collections.Generic;

namespace MTTKDotNetCore.Database.Models;

public partial class TaskCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool DeleteFlag { get; set; }

    public virtual ICollection<ToDoList> ToDoLists { get; set; } = new List<ToDoList>();
}
