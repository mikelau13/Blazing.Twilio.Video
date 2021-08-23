using Blazing.Twilio.WasmVideo.Server.Hubs;
using Blazing.Twilio.WasmVideo.Server.Options;
using Blazing.Twilio.WasmVideo.Server.Services;
using Blazing.Twilio.WasmVideo.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System.Linq;
using static System.Environment;

namespace Blazing.Twilio.WasmVideo.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(options => options.EnableDetailedErrors = true)
                    .AddMessagePackProtocol();
            services.Configure<TwilioSettings>(settings =>
            {
                if (!string.IsNullOrWhiteSpace(Configuration.GetValue<string>("TwilioAccountSid")))
                {
                    settings.AccountSid = Configuration.GetValue<string>("TwilioAccountSid");
                    settings.ApiSecret = Configuration.GetValue<string>("TwilioApiSecret");
                    settings.ApiKey = Configuration.GetValue<string>("TwilioApiKey");
                }
                else if (!string.IsNullOrWhiteSpace(GetEnvironmentVariable("TWILIO_ACCOUNT_SID")))
                {
                    settings.AccountSid = GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
                    settings.ApiSecret = GetEnvironmentVariable("TWILIO_API_SECRET");
                    settings.ApiKey = GetEnvironmentVariable("TWILIO_API_KEY");
                }
                else
                {
                    throw new System.ArgumentNullException("Missing environment variables.");
                }
            });
            services.AddSingleton<TwilioService>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddResponseCompression(opts =>
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" }));
            //services.AddCors(options =>
            //{
            //    // this defines a CORS policy called "default"
            //    options.AddPolicy("default", policy =>
            //    {
            //        // allow Ajax calls to be made from https://localhost:3000
            //        policy.WithOrigins("http://localhost:3000")
            //            .AllowAnyHeader()
            //            .AllowAnyMethod();
            //    });
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                HttpsCompression = HttpsCompressionMode.Compress,
                OnPrepareResponse = context =>
                    context.Context.Response.Headers[HeaderNames.CacheControl] =
                        $"public,max-age={86_400}"
            });
            app.UseRouting();
            //app.UseCors("default"); // CORS middleware to the pipeline

            // This maps the notification endpoint to the implementation of the NotificationHub.
            // Using this endpoint, the SPA running in client browsers can send messages
            // to all the other clients. SignalR provides the notification infrastructure for
            // this process.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>(HubEndpoints.NotificationHub);
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
