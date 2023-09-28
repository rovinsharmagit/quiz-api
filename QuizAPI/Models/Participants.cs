using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Models
{
    public class Participants
    {
        [Key]
        public int ParticipantId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        public int Score { get; set; }

        public int TimeTaken { get; set; }
    }

    public class ParticipantResult
    {
        public int ParticipantId { get; set; }
        public int Score { get; set; }
        public int TimeTaken { get; set; }
    }
}
