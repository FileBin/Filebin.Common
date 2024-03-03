using Filebin.Domain.Azure.Blob.Abstraction;

namespace Filebin.Common.Azure.Blob;

public class BlobDescriptor : IBlobDescriptor {
    public required string Id { get; set; }

    public static IBlobDescriptor Create() {
        return new BlobDescriptor() {
            Id = new Guid().ToString(),
        };
    }
}
