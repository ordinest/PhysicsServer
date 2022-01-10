using Asterism;
using System.Text;
using System.Text.Json;

namespace PhysicsServer.Domains.Astronomy
{
    public class AstroObjectService : IAstroObjectService
    {
        private readonly HttpClient _httpClient;

        public AstroObjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IEnumerable<AstroObject>> GetAll()
        {
            var res = await _httpClient.GetAsync("");
            res.EnsureSuccessStatusCode();
            var output = await ReadHttpContent<IEnumerable<AstroObject>>(res.Content);
            return output;
        }

        public async Task<IEnumerable<AstroObject>> GetObjectsInRegion(EquitorialCoords minCoords, EquitorialCoords maxCoords)
        {
            // If max coords wrap around 360degrees and are less than min coords,
            // extend them above 360
            bool extendRA = minCoords.RightAscension > maxCoords.RightAscension;
            bool extendDec = minCoords.Declination > maxCoords.Declination;
            var extendedMaxCoords = maxCoords with
            {
                RightAscension = maxCoords.RightAscension + (extendRA ? 360d : 0d),
                Declination = maxCoords.Declination + (extendDec ? 360d : 0d)
            };

            // Fetch objects
            var json = JsonSerializer.Serialize(
               new GetInRegionBody(minCoords, extendedMaxCoords),
               new JsonSerializerOptions
               {
                   WriteIndented = true,
                   PropertyNameCaseInsensitive = true,
               });
            var res = await _httpClient.PostAsync("InRegion", new StringContent(
                json, Encoding.UTF8, "application/json"));
            res.EnsureSuccessStatusCode();

            return await ReadHttpContent<IEnumerable<AstroObject>>(res.Content);
        }


        private record GetInRegionBody(EquitorialCoords minCoords, EquitorialCoords maxCoords);

        private async Task<T> ReadHttpContent<T>(HttpContent content)
        {
            using var rs = await content.ReadAsStreamAsync();
            var output = await JsonSerializer.DeserializeAsync<T>(
                rs, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true
                });
            return output;
        }
    }
}
