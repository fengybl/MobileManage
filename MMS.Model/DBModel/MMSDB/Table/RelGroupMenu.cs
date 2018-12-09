using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Model.DBModel.MMSDB.Table
{
    [Table("Rel_Group_Menu")]
    public class RelGroupMenu
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long MenuId { get; set; }
        public int Permission { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
    }
}