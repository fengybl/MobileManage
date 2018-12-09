using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Model.DBModel.MMSDB.Table
{
    [Table("Rel_User_Group")]
    public class RelUserGroup
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long GroupId { get; set; }
        public DateTime CreationDate { get; set; }
        public long CreationBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public long LastUpdateBy { get; set; }
    }
}