using System.ComponentModel.DataAnnotations;
using Davr.Gumon.Entities.Abstracts;

namespace Davr.Gumon.Entities
{
    public class TransactionStatus : EntityBase
    {
        [Required] public string Name { get; set; }
    }
}
