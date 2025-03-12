
using static API.UnitTests.Testing;

namespace API.UnitTests;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetup() => await ResetState();
}
