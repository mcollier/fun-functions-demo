
#r "Newtonsoft.Json"
#r "Twilio.Api"

using System;
using Newtonsoft.Json;
using Twilio;


public static void Run(TimerInfo myTimer, out SMSMessage message, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

    float current_temp_f = 0;

    HttpClient httpClient = new HttpClient();
    HttpResponseMessage response =
        httpClient.GetAsync($"http://api.wunderground.com/api/{GetEnvironmentVariable("WeatherUndergroundAPIKey")}/conditions/q/OH/Columbus.json").Result;

    if (response.IsSuccessStatusCode)
    {
        string body = response.Content.ReadAsStringAsync().Result;

        //log.Info(body);

        var current = Newtonsoft.Json.Linq.JObject.Parse(body);

        current_temp_f = (float)current["current_observation"]["temp_f"];

        log.Info($"The current temperature is {current_temp_f} F.");
    }

    message = null;

    if (current_temp_f < 30)
    {
        message = new SMSMessage();
        message.Body = $"The current temperature is {current_temp_f}! Better put on a jacket!!";
        message.To = GetEnvironmentVariable("MyPhoneNumber"); ;
    }
    else
    {
        message = new SMSMessage();
        message.Body = $"The current temperature is {current_temp_f}! Shorts, baby!!!";
        message.To = GetEnvironmentVariable("MyPhoneNumber");
    }
}

public static string GetEnvironmentVariable(string name)
{
    return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
}
