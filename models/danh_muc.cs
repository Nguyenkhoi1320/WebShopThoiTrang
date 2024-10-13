using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class danh_muc
    {
        public int Id { get; set; }                
        public string TenCategory { get; set; }    
        public string MoTa { get; set; }           
        public DateTime CreatedAt { get; set; }    
        public DateTime? UpdatedAt { get; set; }   
    }
}
