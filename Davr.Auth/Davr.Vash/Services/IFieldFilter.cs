﻿
using System.ComponentModel.DataAnnotations;

namespace Davr.Vash.Services
{
    public interface IFieldFilter
    {

    }

    public class FieldFilter : IFieldFilter
    {
        [Required(AllowEmptyStrings = false)]
        public string f { get; set; }

        public string v { get; set; }
    }
}
