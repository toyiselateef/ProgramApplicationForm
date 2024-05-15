﻿using ApplicationForm.Domain.Entities;

namespace ProgramApplicationForm.Domain.Entities;
public class DropdownQuestion : Question
{
    public List<string> Options { get; set; }
}
