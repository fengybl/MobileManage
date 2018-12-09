using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YJL.Model.DBModel.YJLDB.Table
{
    [Table("Sys_Group")]
    public class SysGroup
    {
        public long Id { get; set; }
        public string GroupName { get; set; }
        public string GroupSys { get; set; }
        public DateTime CreationDate { get; set; }
        public long CreationBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public long LastUpdateBy { get; set; }
    }
}