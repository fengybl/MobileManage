using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YJL.Model.DBModel.YJLDB.Table
{
    [Table("Sys_Menu")]
    public class SysMenu
    {
        public long Id { get; set; }
        public string MenuName { get; set; }
        public string SysCode { get; set; }
        public string MenuUrl { get; set; }
        public int Level { get; set; }
        public long ParentId { get; set; }
        public string ControlName { get; set; }
        public string ClassName { get; set; }
        public string BgColor { get; set; }
        public bool IsInside { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
    }
}