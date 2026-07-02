namespace CipherTool.Models
{
    public class CipherResponse
    {
        public bool Success { get; set; }
        public string Result { get; set; } = "";
        public string? Error { get; set; }
    }
}
