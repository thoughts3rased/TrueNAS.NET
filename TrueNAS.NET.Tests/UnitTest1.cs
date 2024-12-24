using TrueNAS.NET;

namespace TrueNAS.NET.Tests
{
    public class Tests
    {
        public TrueNASApiClient client;

        [SetUp]
        public void Setup()
        {
            client = new TrueNASApiClient("3-tiGAnclYNUJ1up7Ps6ZmPQHCgmqE1lqrRZ7PMV91M7qAdnLamzf6PL4TgHynIeeZ", "nas");
        }

        [Test]
        public void Test1()
        {
            var result = client.GetPoolCount().Result;

            Assert.That(result, Is.Not.Zero);
        }
    }
}