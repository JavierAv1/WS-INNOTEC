namespace DL
{
    public class Result
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public object Object { get; set; }
        public List<Object> Results { get; set; }
        public Exception Ex { get; set; }
    }
}
