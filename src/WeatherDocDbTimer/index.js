var request = require('request');

module.exports = function (context, myTimer) {
    var timeStamp = new Date().toISOString();

    if (myTimer.isPastDue) {
        context.log('JavaScript is running late!');
    }
    context.log('JavaScript timer trigger function ran!', timeStamp);


    request({
        uri: 'http://api.wunderground.com/api/' + GetEnvironmentVariable("WeatherUndergroundAPIKey") + '/conditions/q/OH/Columbus.json',
        method: 'GET'
    }, function (error, response, body) {
        if (!error && response.statusCode == 200) {
            //context.log(body);
            var data = JSON.parse(body);
            context.log('The current temp is: ' + data.current_observation.temp_f);
            context.log('The temp was observed at: ' + data.current_observation.observation_time_rfc822);

            context.bindings.outputDocument = JSON.stringify({
                observation_time: data.current_observation.observation_time_rfc822,
                temp_f: data.current_observation.temp_f
            });

            context.done();
        }
        else {
            context.log('Something went horribly wrong!');

            context.done();
        }
    });

    /*
    context.done();
    */
};

function GetEnvironmentVariable(name) {
    return process.env[name];
}