using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTT.Core.Models;

public class Product : BaseEntity
{
    public string Name {  get; set; }
    public double Price { get; set; }
    public string? ImageUrl {  get; set; }
    [NotMapped]
    public IFormFile? FileImage { get; set; }
}
