using Filebin.Domain.Azure.Blob.Abstraction;

namespace Filebin.Common.Azure.Blob.Models;

public class Blob : IBlob {
    public required IDictionary<string, string> Metadata { get; init; }

    public required IMediaStream MediaStream { get; init; }

    public required string Id { get; init; }
}