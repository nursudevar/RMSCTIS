using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ResourceModel
    {
        #region Properties copied from the related entity
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(50, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Title { get; set; }

        public string Content { get; set; }

        public decimal Score { get; set; }

        public DateTime? Date { get; set; }
        #endregion



        #region Extra properties required for the views
        [DisplayName("Score")]
        public string ScoreOutput { get; set; }

        [DisplayName("Date")]
        public string DateOutput { get; set; }
        #endregion

    }
}
