using Newtonsoft.Json;

namespace Base.Domain.DTOs
{
    public class ApiReturn
    {
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }

        [JsonProperty(PropertyName = "token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "draw", NullValueHandling = NullValueHandling.Ignore)]
        public int Draw { get; set; }
        [JsonProperty(PropertyName = "recordsFiltered", NullValueHandling = NullValueHandling.Ignore)]
        public int RecordsFiltered { get; set; }
        [JsonProperty(PropertyName = "recordsTotal", NullValueHandling = NullValueHandling.Ignore)]
        public int RecordsTotal { get; set; }

        public static ApiReturn Sucesso(object data = null, string token = "", int draw = 0, int recordsFiltered = 0, int recordsTotal = 0)
        {
            return new ApiReturn()
            {
                Draw = draw,
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal,
                Status = "success",
                Data = data,
                Token = token
            };
        }

        public static ApiReturn Erro(string message)
        {
            return new ApiReturn()
            {
                Status = "error",
                Message = message
            };
        }
    }
}
