#r "System.Runtime"

using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Twilio.TwiML;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    var data = await req.Content.ReadAsStringAsync();
    var formValues = data.Split('&')
        .Select(value => value.Split('='))
        .ToDictionary(pair => Uri.UnescapeDataString(pair[0]).Replace("+", " "), 
                      pair => Uri.UnescapeDataString(pair[1]).Replace("+", " "));

    // Perform calculations, API lookups, etc. here
    log.Info($"Message from {formValues["From"]} in {formValues["FromCity"]} said '{formValues["Body"]}'.");
    
    // What am I sending back to caller
    var response = new MessagingResponse()
        .Message($"You said: {formValues["Body"]}");

    var twiml = response.ToString();
    twiml = twiml.Replace("utf-16", "utf-8");

    return new HttpResponseMessage
    {
        Content = new StringContent(twiml, Encoding.UTF8, "application/xml")
    };
}
