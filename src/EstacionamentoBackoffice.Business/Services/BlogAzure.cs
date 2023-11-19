using Azure.Identity;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Services
{
    public class BlogAzure
    {
        public BlogAzure() 
        {
                var blobServiceClient = new BlobServiceClient(
                new Uri("https://<storage-account-name>.blob.core.windows.net"),
                new DefaultAzureCredential());
        }
    }
}
