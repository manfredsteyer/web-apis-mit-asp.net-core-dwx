




















using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

// ReSharper disable All

namespace SwaggerServer{
/// <summary>
/// Web Proxy for Flight
/// </summary>
public class FlightWebProxy : Swagger.WebApiProxy.Template.BaseProxy
{
public FlightWebProxy(Uri baseUrl) : base(baseUrl)
{}

					// helper function for building uris. 
					private string AppendQuery(string currentUrl, string paramName, string value)
					{
						if (currentUrl.Contains("?"))
							currentUrl += string.Format("&{0}={1}", paramName, Uri.EscapeUriString(value));
						else
							currentUrl += string.Format("?{0}={1}", paramName, Uri.EscapeUriString(value));
						return currentUrl;
					}
				/// <summary>
/// 
/// </summary>
public async Task<List<Flight>> ApiFlightGet ()
{
var url = "api/Flight";

using (var client = BuildHttpClient())
{
var response = await client.GetAsync(url).ConfigureAwait(false);
response.EnsureSuccessStatusCode();
return await response.Content.ReadAsAsync<List<Flight>>().ConfigureAwait(false);
}
}
/// <summary>
/// 
/// </summary>
/// <param name="flight"></param>
public async Task<Flight> ApiFlightPost (Flight flight = null)
{
var url = "api/Flight";

using (var client = BuildHttpClient())
{
var response = await client.PostAsJsonAsync(url, flight).ConfigureAwait(false);
response.EnsureSuccessStatusCode();
return await response.Content.ReadAsAsync<Flight>().ConfigureAwait(false);
}
}
/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
public async Task<Flight> ApiFlightByIdGet (int id)
{
var url = "api/Flight/{id}"
	.Replace("{id}", id.ToString());

using (var client = BuildHttpClient())
{
var response = await client.GetAsync(url).ConfigureAwait(false);
response.EnsureSuccessStatusCode();
return await response.Content.ReadAsAsync<Flight>().ConfigureAwait(false);
}
}
/// <summary>
/// 
/// </summary>
/// <param name="from"></param>
/// <param name="to"></param>
public async Task<List<Flight>> ApiFlightByRouteGet (string from = null, string to = null)
{
var url = "api/Flight/byRoute";

using (var client = BuildHttpClient())
{
var response = await client.GetAsync(url).ConfigureAwait(false);
response.EnsureSuccessStatusCode();
return await response.Content.ReadAsAsync<List<Flight>>().ConfigureAwait(false);
}
}
}
public class Flight 
{
public int Id { get; set; }
public string From { get; set; }
public string To { get; set; }
public DateTime Date { get; set; }
public string FlightNumber { get; set; }
public List<Booking> Bookings { get; set; }
}
public class Booking 
{
public int BookingId { get; set; }
public DateTime Datum { get; set; }
public int FlugId { get; set; }
public Flight Flug { get; set; }
public int PassagierId { get; set; }
}
}
        

    