using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Filebin.Common.Util;
using Filebin.Common.Util.Exceptions;
using Filebin.Domain.Azure.Blob.Abstraction;
using Filebin.Domain.Azure.Blob.Abstraction.Services;
using Microsoft.Extensions.Configuration;

namespace Filebin.Common.Azure.Blob;

public class BlobStorage(IConfiguration configuration) : IBlobStorage {
    private readonly string storageConnectionString = configuration.GetOrThrow("BlobConnectionString");
    public async Task<IBlobDescriptor> UploadViaStreamAsync(string containerName, IMediaStream media, object? metadata = null) {
        var container = await GetContainerClientAsync(containerName);

        var fileDesc = BlobDescriptor.Create();
        var blobClient = container.GetBlobClient(fileDesc.Id);

        var httpHeaders = new BlobHttpHeaders() {
            ContentType = media.ContentType,
        };

        await blobClient.UploadAsync(media.Stream, httpHeaders);
        if (metadata is not null)
            await blobClient.SetMetadataAsync(Misc.AnonymousToStringDictionary(metadata));

        return fileDesc;
    }

    public async Task<IBlob> GetBlobAsync(string containerName, IBlobDescriptor fileDesc) {
        var container = await GetContainerClientAsync(containerName);
        var blobClient = container.GetBlobClient(fileDesc.Id);

        if (!await blobClient.ExistsAsync()) {
            throw new NotFoundException($"File with id {fileDesc.Id} not found!");
        }

        var properties = await blobClient.GetPropertiesAsync();

        return new Models.Blob {
            Id = fileDesc.Id,
            Metadata = properties.Value.Metadata,
            MediaStream = new MediaStream {
                ContentType = properties.Value.ContentType,
                Stream = await blobClient.OpenReadAsync(),
            }
        };
    }

    public async Task DeleteAsync(string containerName, IBlobDescriptor fileDesc) {
        var container = await GetContainerClientAsync(containerName);
        var blobClient = container.GetBlobClient(fileDesc.Id);

        if (!await blobClient.ExistsAsync()) {
            throw new NotFoundException($"File with id {fileDesc.Id} not found!");
        }

        await blobClient.DeleteAsync();
    }

    public async Task<IEnumerable<IBlobDescriptor>> ListFilesAsync(string containerName) {
        var container = await GetContainerClientAsync(containerName);
        return container.GetBlobs().Select(x => new BlobDescriptor { Id = x.Name });
    }

    async Task<BlobContainerClient> GetContainerClientAsync(string containerName) {
        var container = new BlobContainerClient(storageConnectionString, containerName);

        await container.CreateIfNotExistsAsync();

        return container;
    }
}
