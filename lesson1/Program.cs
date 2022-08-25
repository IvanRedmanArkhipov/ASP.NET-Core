using hw1;
using Newtonsoft.Json;

Class class1 = new();

for (int i = 4; i <= 13; i++)
{
    usersGet(class1, i);
}


static async Task<Class> usersGet(Class class0, int i)
{
    HttpClient client = new();
    var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://jsonplaceholder.typicode.com/posts/{i}");

    try
    {
        HttpResponseMessage httpResponseMessage = client.SendAsync(httpRequest).Result;
        string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
        class0 = (Class)JsonConvert.DeserializeObject(responseBody, typeof(Class));
        await fileWrite(class0);

        return class0;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return class0;
    }
}


static async Task<string> fileWrite(Class class0)
{
    string path = Directory.GetParent(Directory.GetCurrentDirectory()) + "/result.txt";
    string request = "User Id: " + Convert.ToString(class0.userId) + "\n" + "Id: " + Convert.ToString(class0.id) + "\n" + "Title: " + class0.title + "\n" + "Body: " + class0.body;

    using (StreamWriter writer = new StreamWriter(path, true))
    {
        await writer.WriteAsync(request + "\n" + "\n");
    }

    return "";
}