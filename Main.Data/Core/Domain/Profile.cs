using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Data.Core.Domain
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public DateTime? CreateDate { get; set; }


        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
