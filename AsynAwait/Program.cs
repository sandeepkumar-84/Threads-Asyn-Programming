

string url = "https://jsonplaceholder.typicode.com/posts/1";

string result = await GetApiDataAsync(url);

Console.WriteLine(result);


async Task<string> GetApiDataAsync(string url)
{
    using (HttpClient client = new HttpClient())
    { 
        HttpResponseMessage res = await client.GetAsync(url);
        if (res.IsSuccessStatusCode)
        {
            string apiData = await res.Content.ReadAsStringAsync();          
            return apiData;
        }
        else
        {
            return $"Error: {res.StatusCode}";
        }
       
    }
}