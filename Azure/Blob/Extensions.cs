using System.Net;
using System.Net.Http.Headers;
using Filebin.Domain.Azure.Blob.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Filebin.Common.Azure.Blob;

public static class Extensions {
    public static HttpResponseMessage ToHttpResponseMessage(this IMediaStream media, string? filename = null) {
        var response = new HttpResponseMessage(HttpStatusCode.OK) {
            Content = new StreamContent(media.Stream),
        };

        response.Content.Headers.ContentType = new MediaTypeHeaderValue(media.ContentType);
        response.Content.Headers.ContentLength = media.Stream.Length;

        if (filename is not null)
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                FileName = filename,
            };

        return response;
    } 
}
