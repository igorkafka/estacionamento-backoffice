using EstacionamentoBackoffice.API.Configuration;
using EstacionamentoBackoffice.API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace EstacionamentoBackoffice.API.Tests
{
    public class BlobService_Tests
    {
        private const string Asset = "assets/spyro.txt";

        private IBlobService? _blobService;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            services.ConfigBlobClient(configuration.Build());

            using var serviceProvider = services.BuildServiceProvider();
            _blobService = serviceProvider.GetRequiredService<IBlobService>();
        }

        [Test]
        public async Task CreateBlobFile_Created()
        {
            var blobCount = _blobService!.GetBlobCount();

            var data = File.ReadAllBytes(Asset);
            Assert.True(await _blobService.CreateBlobFileAsync(Guid.NewGuid().ToString(), data));

            Assert.AreEqual(_blobService.GetBlobCount(), blobCount + 1);
        }

        [Test]
        public async Task GetBlobFile_MatchesOriginal()
        {
            var fileName = Guid.NewGuid().ToString();

            var data = File.ReadAllText(Asset);

            await _blobService!.CreateBlobFileAsync(fileName, Encoding.UTF8.GetBytes(data));

            var blobStream = await _blobService!.GetBlobFileAsync(fileName);

            var streamReader = new StreamReader(blobStream!);

            Assert.AreEqual(await streamReader.ReadToEndAsync(), data);
        }

        [Test]
        public async Task CreateAndDeleteBlob_Success()
        {
            var fileName = Guid.NewGuid().ToString();
            var blobCount = _blobService!.GetBlobCount();

            var data = File.ReadAllBytes(Asset);
            Assert.True(await _blobService!.CreateBlobFileAsync(fileName, data));
            Assert.AreEqual(_blobService!.GetBlobCount(), blobCount + 1);

            Assert.True(await _blobService!.DeleteBlobFileAsync(fileName));
            Assert.AreEqual(_blobService!.GetBlobCount(), blobCount);
        }
    }
}
