using IdentityServer4.Models;

namespace IdentityServer.Configurations;

public class IdConfig
{
    // definera vilken identifiering som gäller? är det här man definerar ouath?
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[] {

                new IdentityResources.OpenId(),
                new IdentityResources.Profile()

        };


    // injecera möjlig funktionalitet som klient får ta del av via scopes
    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[] {

                new ApiScope("ZenBuilds.write"),
                new ApiScope("ZenBuilds.read")

        };

    // vilket api som identity server ger tillstånd att kommunicera med
    public static IEnumerable<ApiResource> ApiResources =>
     new ApiResource[] {

                new ApiResource("ZenBuilds")
                {

                    Scopes = new List<string>{ "ZenBuilds.write", "ZenBuilds.read" },
                    ApiSecrets = new List<Secret> { new Secret("4J3f3fg2f88fwsskHwrwf3fOhzzg2f922".Sha256())}
                    
                }


     };

    // ange ZenBuildUI som lämplig client att använda ZenBuilds API
    public static IEnumerable<Client> Clients =>
        new Client[] {

                new Client{

                ClientName = "ZenBuildUI",
                ClientId   = "774",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // hemlig nyckel + vilken algoritm som skall hasha lösenordet
                ClientSecrets = new List<Secret> { new Secret("gj381jklJKK3Hf02kl29234".Sha256())},
                AllowedScopes = new List<String> { "ZenBuilds.write", "ZenBuilds.read" }

                }

             };

}
