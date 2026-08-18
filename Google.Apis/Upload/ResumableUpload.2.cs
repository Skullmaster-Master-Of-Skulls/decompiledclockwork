using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Util;

namespace Google.Apis.Upload
{
	// Token: 0x02000007 RID: 7
	public class ResumableUpload<TRequest> : ResumableUpload
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000027CC File Offset: 0x000009CC
		protected ResumableUpload(IClientService service, string path, string httpMethod, Stream contentStream, string contentType) : base(contentStream, new ResumableUploadOptions
		{
			HttpClient = service.HttpClient,
			Serializer = service.Serializer,
			ServiceName = service.Name
		})
		{
			service.ThrowIfNull("service");
			path.ThrowIfNull("path");
			httpMethod.ThrowIfNullOrEmpty("httpMethod");
			contentStream.ThrowIfNull("contentStream");
			this.Service = service;
			this.Path = path;
			this.HttpMethod = httpMethod;
			this.ContentType = contentType;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002858 File Offset: 0x00000A58
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002860 File Offset: 0x00000A60
		public IClientService Service { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002869 File Offset: 0x00000A69
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002871 File Offset: 0x00000A71
		public string Path { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000287A File Offset: 0x00000A7A
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002882 File Offset: 0x00000A82
		public string HttpMethod { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000288B File Offset: 0x00000A8B
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002893 File Offset: 0x00000A93
		public string ContentType { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000289C File Offset: 0x00000A9C
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000028A4 File Offset: 0x00000AA4
		public TRequest Body { get; set; }

		// Token: 0x06000041 RID: 65 RVA: 0x000028B0 File Offset: 0x00000AB0
		public override async Task<Uri> InitiateSessionAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			HttpRequestMessage httpRequestMessage = this.CreateInitializeRequest();
			ResumableUploadOptions options = this.Options;
			if (options != null)
			{
				Action<HttpRequestMessage> modifySessionInitiationRequest = options.ModifySessionInitiationRequest;
				if (modifySessionInitiationRequest != null)
				{
					modifySessionInitiationRequest(httpRequestMessage);
				}
			}
			HttpResponseMessage httpResponseMessage = await this.Service.HttpClient.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);
			HttpResponseMessage response = httpResponseMessage;
			if (!response.IsSuccessStatusCode)
			{
				throw await this.ExceptionForResponseAsync(response).ConfigureAwait(false);
			}
			return response.Headers.Location;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002900 File Offset: 0x00000B00
		private HttpRequestMessage CreateInitializeRequest()
		{
			RequestBuilder requestBuilder = new RequestBuilder
			{
				BaseUri = new Uri(this.Service.BaseUri),
				Path = this.Path,
				Method = this.HttpMethod
			};
			requestBuilder.AddParameter(RequestParameterType.Query, "key", this.Service.ApiKey);
			requestBuilder.AddParameter(RequestParameterType.Query, "uploadType", "resumable");
			this.SetAllPropertyValues(requestBuilder);
			HttpRequestMessage httpRequestMessage = requestBuilder.CreateRequest();
			if (this.ContentType != null)
			{
				httpRequestMessage.Headers.Add("X-Upload-Content-Type", this.ContentType);
			}
			if (base.ContentStream.CanSeek)
			{
				httpRequestMessage.Headers.Add("X-Upload-Content-Length", base.StreamLength.ToString());
			}
			this.Service.SetRequestSerailizedContent(httpRequestMessage, this.Body);
			return httpRequestMessage;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029D8 File Offset: 0x00000BD8
		private void SetAllPropertyValues(RequestBuilder requestBuilder)
		{
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties())
			{
				RequestParameterAttribute customAttribute = propertyInfo.GetCustomAttribute<RequestParameterAttribute>();
				if (customAttribute != null)
				{
					string name = customAttribute.Name ?? propertyInfo.Name.ToLower();
					object value = propertyInfo.GetValue(this, null);
					if (value != null)
					{
						IEnumerable enumerable = value as IEnumerable;
						if (!(value is string) && enumerable != null)
						{
							using (IEnumerator enumerator = enumerable.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object o = enumerator.Current;
									requestBuilder.AddParameter(customAttribute.Type, name, Utilities.ConvertToString(o));
								}
								goto IL_BD;
							}
						}
						requestBuilder.AddParameter(customAttribute.Type, name, Utilities.ConvertToString(value));
					}
				}
				IL_BD:;
			}
		}

		// Token: 0x04000021 RID: 33
		private const string PayloadContentTypeHeader = "X-Upload-Content-Type";

		// Token: 0x04000022 RID: 34
		private const string PayloadContentLengthHeader = "X-Upload-Content-Length";

		// Token: 0x04000023 RID: 35
		private const string UploadType = "uploadType";

		// Token: 0x04000024 RID: 36
		private const string Resumable = "resumable";
	}
}
