using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace TaskManagement.Models
{
    public class ColumnModel
    {
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int Position { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
    }

    public class TaskModel
    {
        public Guid ID { get; set; }
        [Required]
        public Guid ColumnID { get; set; }
        public string UserID { get; set; }

        [Required]
        public string Name { get; set; }
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public int Position { get; set; }

        public virtual ColumnModel Column { get; set; }
        public virtual Users User { get; set; }
    }
}
