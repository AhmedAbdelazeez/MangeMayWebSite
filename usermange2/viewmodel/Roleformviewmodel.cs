using System.ComponentModel.DataAnnotations;

namespace usermange2.viewmodel
{
    public class Roleformviewmodel
    {
        [Required,StringLength(250)]
      
        public string Name { get; set; }
    }
}
