using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Google.Apis.Services;

namespace Google.Apis.Requests
{
	// Token: 0x02000014 RID: 20
	internal static class HttpRequestMessageExtenstions
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00003C50 File Offset: 0x00001E50
		internal static void SetRequestSerailizedContent(this HttpRequestMessage request, IClientService service, object body, bool gzipEnabled)
		{
			if (body == null)
			{
				return;
			}
			string mediaType = "application/" + service.Serializer.Format;
			string content = service.SerializeObject(body);
			HttpContent httpContent;
			if (gzipEnabled)
			{
				httpContent = HttpRequestMessageExtenstions.CreateZipContent(content);
				httpContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType)
				{
					CharSet = Encoding.UTF8.WebName
				};
			}
			else
			{
				httpContent = new StringContent(content, Encoding.UTF8, mediaType);
			}
			request.Content = httpContent;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003CC2 File Offset: 0x00001EC2
		internal static HttpContent CreateZipContent(string content)
		{
			return new StreamContent(HttpRequestMessageExtenstions.CreateGZipStream(content))
			{
				Headers = 
				{
					ContentEncoding = 
					{
						"gzip"
					}
				}
			};
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003CE4 File Offset: 0x00001EE4
		private static Stream CreateGZipStream(string serializedObject)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(serializedObject);
			Stream result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					gzipStream.Write(bytes, 0, bytes.Length);
				}
				memoryStream.Position = 0L;
				byte[] array = new byte[memoryStream.Length];
				memoryStream.Read(array, 0, array.Length);
				result = new MemoryStream(array);
			}
			return result;
		}
	}
}
