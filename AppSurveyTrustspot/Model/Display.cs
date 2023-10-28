using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using AppSurveyTrustspot.Model;


#nullable disable

namespace AppSurveyTrustspot.Model
{
    public partial class Display
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppId { get; set; }
		public int? ViewId { get; set; }
        public string AppName { get; set; }
        public string ReviewText { get; set; }
        public virtual App App { get; set; }
    }

}
