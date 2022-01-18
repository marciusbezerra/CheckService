using System;

namespace CheckService.Models
{
    public class GoogleResult
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Result { get; set; }
        public string Error { get; set; }
    }
}