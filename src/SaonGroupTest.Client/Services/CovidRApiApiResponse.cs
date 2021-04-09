namespace SaonGroupTest.Client.Services
{
    public class CovidRapidApiResponse<TData>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TData Data { get; set; }
    }
}
