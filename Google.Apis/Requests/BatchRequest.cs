using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Testing;

namespace Google.Apis.Requests
{
	// Token: 0x02000012 RID: 18
	public sealed class BatchRequest
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000316A File Offset: 0x0000136A
		internal string BatchUrl
		{
			get
			{
				return this.batchUrl;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003172 File Offset: 0x00001372
		public BatchRequest(IClientService service)
		{
			BaseClientService baseClientService = service as BaseClientService;
			this..ctor(service, ((baseClientService != null) ? baseClientService.BatchUri : null) ?? "https://www.googleapis.com/batch");
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003196 File Offset: 0x00001396
		public BatchRequest(IClientService service, string batchUrl)
		{
			this.allRequests = new List<BatchRequest.InnerRequest>();
			base..ctor();
			this.batchUrl = batchUrl;
			this.service = service;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000031B7 File Offset: 0x000013B7
		public int Count
		{
			get
			{
				return this.allRequests.Count;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000031C4 File Offset: 0x000013C4
		public void Queue<TResponse>(IClientServiceRequest request, BatchRequest.OnResponse<TResponse> callback) where TResponse : class
		{
			if (this.Count > 1000)
			{
				throw new InvalidOperationException("A batch request cannot contain more than 1000 single requests");
			}
			this.allRequests.Add(new BatchRequest.InnerRequest<TResponse>
			{
				ClientRequest = request,
				ResponseType = typeof(TResponse),
				OnResponseCallback = callback
			});
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003217 File Offset: 0x00001417
		public Task ExecuteAsync()
		{
			return this.ExecuteAsync(CancellationToken.None);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003224 File Offset: 0x00001424
		public async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			if (this.Count >= 1)
			{
				ConfigurableHttpClient httpClient = this.service.HttpClient;
				HttpContent content = await BatchRequest.CreateOuterRequestContent(from r in this.allRequests
				select r.ClientRequest).ConfigureAwait(false);
				HttpResponseMessage result = await httpClient.PostAsync(new Uri(this.batchUrl), content, cancellationToken).ConfigureAwait(false);
				result.EnsureSuccessStatusCode();
				string fullContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
				string text = result.Content.Headers.GetValues("Content-Type").First<string>();
				string boundary = text.Substring(text.IndexOf("boundary=") + "boundary=".Length);
				int requestIndex = 0;
				for (;;)
				{
					cancellationToken.ThrowIfCancellationRequested();
					int num = fullContent.IndexOf("--" + boundary);
					if (num == -1)
					{
						break;
					}
					fullContent = fullContent.Substring(num + boundary.Length + 2);
					int endIndex = fullContent.IndexOf("--" + boundary);
					if (endIndex == -1)
					{
						break;
					}
					HttpResponseMessage responseMessage = BatchRequest.ParseAsHttpResponse(fullContent.Substring(0, endIndex));
					if (responseMessage.IsSuccessStatusCode)
					{
						string input = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
						object content2 = this.service.Serializer.Deserialize(input, this.allRequests[requestIndex].ResponseType);
						this.allRequests[requestIndex].OnResponse(content2, null, requestIndex, responseMessage);
					}
					else
					{
						RequestError error = await this.service.DeserializeError(responseMessage).ConfigureAwait(false);
						this.allRequests[requestIndex].OnResponse(null, error, requestIndex, responseMessage);
					}
					requestIndex++;
					fullContent = fullContent.Substring(endIndex);
					responseMessage = null;
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003274 File Offset: 0x00001474
		[VisibleForTestOnly]
		internal static HttpResponseMessage ParseAsHttpResponse(string content)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
			using (StringReader stringReader = new StringReader(content))
			{
				string text = stringReader.ReadLine();
				while (string.IsNullOrEmpty(text))
				{
					text = stringReader.ReadLine();
				}
				while (!string.IsNullOrEmpty(text))
				{
					text = stringReader.ReadLine();
				}
				text = stringReader.ReadLine();
				while (string.IsNullOrEmpty(text))
				{
					text = stringReader.ReadLine();
				}
				int statusCode = int.Parse(text.Split(new char[]
				{
					' '
				})[1]);
				httpResponseMessage.StatusCode = (HttpStatusCode)statusCode;
				IDictionary<string, string> dictionary = new Dictionary<string, string>();
				while (!string.IsNullOrEmpty(text = stringReader.ReadLine()))
				{
					int num = text.IndexOf(':');
					string key = text.Substring(0, num).Trim();
					string text2 = text.Substring(num + 1).Trim();
					if (dictionary.ContainsKey(key))
					{
						dictionary[key] = dictionary[key] + ", " + text2;
					}
					else
					{
						dictionary.Add(key, text2);
					}
				}
				string mediaType = null;
				if (dictionary.ContainsKey("Content-Type"))
				{
					mediaType = dictionary["Content-Type"].Split(new char[]
					{
						';',
						' '
					})[0];
					dictionary.Remove("Content-Type");
				}
				httpResponseMessage.Content = new StringContent(stringReader.ReadToEnd(), Encoding.UTF8, mediaType);
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					HttpHeaders headers = httpResponseMessage.Headers;
					if (typeof(HttpContentHeaders).GetProperty(keyValuePair.Key.Replace("-", "")) != null)
					{
						headers = httpResponseMessage.Content.Headers;
					}
					if (!headers.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value))
					{
						throw new FormatException(string.Format("Could not parse header {0} from batch reply", keyValuePair.Key));
					}
				}
			}
			return httpResponseMessage;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000034A0 File Offset: 0x000016A0
		[VisibleForTestOnly]
		internal static async Task<HttpContent> CreateOuterRequestContent(IEnumerable<IClientServiceRequest> requests)
		{
			MultipartContent mixedContent = new MultipartContent("mixed");
			foreach (IClientServiceRequest request in requests)
			{
				MultipartContent multipartContent = mixedContent;
				HttpContent content = await BatchRequest.CreateIndividualRequest(request).ConfigureAwait(false);
				multipartContent.Add(content);
				multipartContent = null;
			}
			IEnumerator<IClientServiceRequest> enumerator = null;
			return mixedContent;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000034E8 File Offset: 0x000016E8
		[VisibleForTestOnly]
		internal static async Task<HttpContent> CreateIndividualRequest(IClientServiceRequest request)
		{
			return new StringContent(await BatchRequest.CreateRequestContentString(request.CreateRequest(new bool?(false))).ConfigureAwait(false))
			{
				Headers = 
				{
					ContentType = new MediaTypeHeaderValue("application/http")
				}
			};
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003530 File Offset: 0x00001730
		[VisibleForTestOnly]
		internal static async Task<string> CreateRequestContentString(HttpRequestMessage requestMessage)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0} {1}", requestMessage.Method, requestMessage.RequestUri.AbsoluteUri);
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in requestMessage.Headers)
			{
				sb.Append(Environment.NewLine).AppendFormat("{0}: {1}", keyValuePair.Key, string.Join(", ", keyValuePair.Value.ToArray<string>()));
			}
			if (requestMessage.Content != null)
			{
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair2 in requestMessage.Content.Headers)
				{
					sb.Append(Environment.NewLine).AppendFormat("{0}: {1}", keyValuePair2.Key, string.Join(", ", keyValuePair2.Value.ToArray<string>()));
				}
			}
			if (requestMessage.Content != null)
			{
				sb.Append(Environment.NewLine);
				string text = await requestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
				sb.Append("Content-Length:  ").Append(text.Length);
				sb.Append(Environment.NewLine).Append(Environment.NewLine).Append(text);
			}
			return sb.Append(Environment.NewLine).ToString();
		}

		// Token: 0x04000046 RID: 70
		private const string DefaultBatchUrl = "https://www.googleapis.com/batch";

		// Token: 0x04000047 RID: 71
		private const int QueueLimit = 1000;

		// Token: 0x04000048 RID: 72
		private readonly IList<BatchRequest.InnerRequest> allRequests;

		// Token: 0x04000049 RID: 73
		private readonly string batchUrl;

		// Token: 0x0400004A RID: 74
		private readonly IClientService service;

		// Token: 0x0200002D RID: 45
		// (Invoke) Token: 0x06000130 RID: 304
		public delegate void OnResponse<in TResponse>(TResponse content, RequestError error, int index, HttpResponseMessage message) where TResponse : class;

		// Token: 0x0200002E RID: 46
		private class InnerRequest
		{
			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000133 RID: 307 RVA: 0x00005F56 File Offset: 0x00004156
			// (set) Token: 0x06000134 RID: 308 RVA: 0x00005F5E File Offset: 0x0000415E
			public IClientServiceRequest ClientRequest { get; set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000135 RID: 309 RVA: 0x00005F67 File Offset: 0x00004167
			// (set) Token: 0x06000136 RID: 310 RVA: 0x00005F6F File Offset: 0x0000416F
			public Type ResponseType { get; set; }

			// Token: 0x06000137 RID: 311 RVA: 0x00005F78 File Offset: 0x00004178
			public virtual void OnResponse(object content, RequestError error, int index, HttpResponseMessage message)
			{
				string text = (message.Headers.ETag != null) ? message.Headers.ETag.Tag : null;
				IDirectResponseSchema directResponseSchema = content as IDirectResponseSchema;
				if (directResponseSchema != null && directResponseSchema.ETag == null && text != null)
				{
					directResponseSchema.ETag = text;
				}
			}
		}

		// Token: 0x0200002F RID: 47
		private class InnerRequest<TResponse> : BatchRequest.InnerRequest where TResponse : class
		{
			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000139 RID: 313 RVA: 0x00005FC4 File Offset: 0x000041C4
			// (set) Token: 0x0600013A RID: 314 RVA: 0x00005FCC File Offset: 0x000041CC
			public BatchRequest.OnResponse<TResponse> OnResponseCallback { get; set; }

			// Token: 0x0600013B RID: 315 RVA: 0x00005FD5 File Offset: 0x000041D5
			public override void OnResponse(object content, RequestError error, int index, HttpResponseMessage message)
			{
				base.OnResponse(content, error, index, message);
				if (this.OnResponseCallback == null)
				{
					return;
				}
				this.OnResponseCallback(content as TResponse, error, index, message);
			}
		}
	}
}
