using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Appellation.Models
{
    public class ErrorViewModel
    {
        //public ObjectResult? ErrorDetails { get; set; } 
        
        public string? Details { get; set;}
    }
}