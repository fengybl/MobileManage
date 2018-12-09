using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YJL.Model.DBModel.YJLDB.Table
{
    [Table("User_Info")]
    public class UserInfo
    {
        public long Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public bool Enable { get; set; }
        public string Tel { get; set; }
        public long DeptId { get; set; }
        public string Mail { get; set; }
        public string HeadImg { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
    }
}