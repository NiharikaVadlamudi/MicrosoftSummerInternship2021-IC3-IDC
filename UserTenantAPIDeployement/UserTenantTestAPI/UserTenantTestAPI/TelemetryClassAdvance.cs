using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using static System.Net.WebRequestMethods;


namespace UserTenantTestAPI
{
    public class TelemetryClassAdvance : ITelemetryInitializer
    {
       
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleInstance = "Niharika@Demo";
        }
    }
}
