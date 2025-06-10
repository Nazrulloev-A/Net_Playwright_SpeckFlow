using Microsoft.Extensions.Configuration;

namespace WebOrders_PW.Models;

public class ApplicationOptionsFixture
{
    public ApplicationOptionsFixture(IConfiguration configuration)
    {
        Options = ApplicationOptions.GetConfig(configuration);
    }

    public ApplicationOptions Options { get; private set; }
}