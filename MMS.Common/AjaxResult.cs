namespace MMS.Common
{
    public class AjaxResult<TEntity> where TEntity : class 
    {
        public bool Result { get; set; }
        public string Msg { get; set; }
        public TEntity Data { get; set; }
        //用于分页
        public int CurrentIndex { get; set; }
        public int RecordsTotal { get; set; } = 0;
    }
}
