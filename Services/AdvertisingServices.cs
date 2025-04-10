
namespace AdvertisingService.Services
{
    public class AdvertisingServices
    {
        private readonly Dictionary<string, List<string>> _locationToPlatforms = new();
        private readonly object _lock = new();

        public void LoadDataFromFile(string filePath)
        {
            try
            {
                var newData = new Dictionary<string, List<string>>();

                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split(':', 2);
                    if (parts.Length != 2) continue;

                    var platform = parts[0].Trim();
                    var locations = parts[1].Split(',')
                        .Select(x => x.Trim())
                        .Where(x => !string.IsNullOrEmpty(x))
                        .ToList();

                    foreach (var location in locations)
                    {
                        if (!newData.ContainsKey(location))
                        {
                            newData[location] = new List<string>();
                        }
                        newData[location].Add(platform);
                    }
                }

                lock (_lock)
                {
                    _locationToPlatforms.Clear();
                    foreach (var kvp in newData)
                    {
                        _locationToPlatforms[kvp.Key] = kvp.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        public List<string> GetPlatformsForLocation(string location)
        {
            var result = new List<string>();

            lock (_lock)
            {
                var matchingLocations = _locationToPlatforms.Keys
                    .Where(loc => location.StartsWith(loc, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(loc => loc.Length)
                    .ToList();

                var platforms = new HashSet<string>();
                foreach (var loc in matchingLocations)
                {
                    foreach (var platform in _locationToPlatforms[loc])
                    {
                        platforms.Add(platform);
                    }
                }

                result = platforms.ToList();
            }

            return result;
        }
    }
}
