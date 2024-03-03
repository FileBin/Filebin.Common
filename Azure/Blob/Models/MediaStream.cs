using Filebin.Domain.Azure.Blob.Abstraction;

namespace Filebin.Common.Azure.Blob;

public class MediaStream : IMediaStream {
    public required string ContentType { get; set; }

    public required Stream Stream { get; set; }
}
