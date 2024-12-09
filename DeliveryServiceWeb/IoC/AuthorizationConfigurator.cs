using DeliveryServiceDataAccess.Entities;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ApiScope = Duende.IdentityServer.EntityFramework.Entities.ApiScope;
using Client = Duende.IdentityServer.EntityFramework.Entities.Client;
using Secret = Microsoft.AspNetCore.DataProtection.Secret;

namespace DeliveryServiceWeb.Service.IoC;

public static class AuthorizationConfigurator
{
    public static void ConfigureServices(IServiceCollection services, Settings.Settings settings)
    {
        IdentityModelEventSource.ShowPII = true;
        services.AddIdentity<User, UserRoleEntity>(options =>
            {
                options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<DbContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders();

        services.AddIdentityServer()
            .AddInMemoryApiScopes([new ApiScope(settings.ApiName)])
            .AddInMemoryClients([
                new Client
                {
                    ClientName = settings.ClientId,
                    ClientId = settings.ClientId,
                    Enabled = true,
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = [
                        GrantType.ClientCredentials,
                        GrantType.ResourceOwnerPassword],
                    ClientSecrets = [new Secret(settings.ClientSecret.Sha256())],
                    AllowedScopes = [settings.ApiName]
                }
            ])
            .AddAspNetIdentity<UserEntity>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }
        ).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.RequireHttpsMetadata = false;
            options.Authority = settings.IdentityServerUri;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            options.Audience = settings.ApiName;
        });
        
        services.AddAuthorization();
    }
    
    public static void ConfigureApplication(IApplicationBuilder app)
    {
        app.UseIdentityServer();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}