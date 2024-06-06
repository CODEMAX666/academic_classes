using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mentor.Controllers
{
    public class MeetingController : Controller
    {
        // GET: Meeting
        public void call()
        {
            var task = ScheduleMeeting();
        }

        public async Task ScheduleMeeting()
        {
            // The Azure AD tenant ID or a verified domain (e.g. contoso.onmicrosoft.com) 
            var tenantId = "f8cdef31-a31e-4b4a-93e4-5f571e91255a";

            // The client ID of the app registered in Azure AD
            var clientId = "031ad616-1868-4f10-a381-d863b415344c";

            // *Never* include client secrets in source code!
            var clientSecret = "Meeting"; // Or some other secure place.

            // The app registration should be configured to require access to permissions
            // sufficient for the Microsoft Graph API calls the app will be making, and
            // those permissions should be granted by a tenant administrator.
            var scopes = new string[] { "https://graph.microsoft.com/.default" };

            // Configure the MSAL client as a confidential client
            var confidentialClient = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithAuthority($"https://login.microsoftonline.com/$tenantId/v2.0")
                .WithClientSecret(clientSecret)
                .Build();

            // Build the Microsoft Graph client. As the authentication provider, set an async lambda
            // which uses the MSAL client to obtain an app-only access token to Microsoft Graph,
            // and inserts this access token in the Authorization header of each API request. 
            GraphServiceClient graphServiceClient =
                new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) => {

            // Retrieve an access token for Microsoft Graph (gets a fresh token if needed).
            var authResult = await confidentialClient
                .AcquireTokenForClient(scopes)
                .ExecuteAsync();

            // Add the access token in the Authorization header of the API request.
            requestMessage.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                    })
                );

            // Make a Microsoft Graph API query
            var users = await graphServiceClient.Users.Request().GetAsync();

            GraphServiceClient graphClient = new GraphServiceClient(graphServiceClient.AuthenticationProvider);

            var onlineMeeting = new OnlineMeeting
            {
                StartDateTime = DateTimeOffset.Parse("2019-07-12T21:30:34.2444915+00:00"),
                EndDateTime = DateTimeOffset.Parse("2019-07-12T22:00:34.2464912+00:00"),
                Subject = "User Token Meeting"
            };

            await graphClient.Me.OnlineMeetings
                .Request()
                .AddAsync(onlineMeeting);

            //await users..OnlineMeetings
            //    .Request()
            //    .AddAsync(onlineMeeting);

            //return View();
        }
    }
}