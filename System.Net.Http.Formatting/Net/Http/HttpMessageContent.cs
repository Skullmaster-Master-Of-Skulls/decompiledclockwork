using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000059 RID: 89
	public class HttpMessageContent : HttpContent
	{
		// Token: 0x06000344 RID: 836 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		public HttpMessageContent(HttpRequestMessage httpRequest)
		{
			if (httpRequest == null)
			{
				throw Error.ArgumentNull("httpRequest");
			}
			this.HttpRequestMessage = httpRequest;
			base.Headers.ContentType = new MediaTypeHeaderValue("application/http");
			base.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("msgtype", "request"));
			this.InitializeStreamTask();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000CC50 File Offset: 0x0000AE50
		public HttpMessageContent(HttpResponseMessage httpResponse)
		{
			if (httpResponse == null)
			{
				throw Error.ArgumentNull("httpResponse");
			}
			this.HttpResponseMessage = httpResponse;
			base.Headers.ContentType = new MediaTypeHeaderValue("application/http");
			base.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("msgtype", "response"));
			this.InitializeStreamTask();
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000CCB7 File Offset: 0x0000AEB7
		private HttpContent Content
		{
			get
			{
				if (this.HttpRequestMessage == null)
				{
					return this.HttpResponseMessage.Content;
				}
				return this.HttpRequestMessage.Content;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		public HttpRequestMessage HttpRequestMessage { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000CCE9 File Offset: 0x0000AEE9
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000CCF1 File Offset: 0x0000AEF1
		public HttpResponseMessage HttpResponseMessage { get; private set; }

		// Token: 0x0600034B RID: 843 RVA: 0x0000CD11 File Offset: 0x0000AF11
		private void InitializeStreamTask()
		{
			this._streamTask = new Lazy<Task<Stream>>(delegate()
			{
				if (this.Content != null)
				{
					return this.Content.ReadAsStreamAsync();
				}
				return null;
			});
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		internal static bool ValidateHttpMessageContent(HttpContent content, bool isRequest, bool throwOnError)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			MediaTypeHeaderValue contentType = content.Headers.ContentType;
			if (contentType != null)
			{
				if (!contentType.MediaType.Equals("application/http", StringComparison.OrdinalIgnoreCase))
				{
					if (throwOnError)
					{
						throw Error.Argument("content", Resources.HttpMessageInvalidMediaType, new object[]
						{
							FormattingUtilities.HttpContentType.Name,
							isRequest ? "application/http; msgtype=request" : "application/http; msgtype=response"
						});
					}
					return false;
				}
				else
				{
					foreach (NameValueHeaderValue nameValueHeaderValue in contentType.Parameters)
					{
						if (nameValueHeaderValue.Name.Equals("msgtype", StringComparison.OrdinalIgnoreCase))
						{
							string text = FormattingUtilities.UnquoteToken(nameValueHeaderValue.Value);
							if (text.Equals(isRequest ? "request" : "response", StringComparison.OrdinalIgnoreCase))
							{
								return true;
							}
							if (throwOnError)
							{
								throw Error.Argument("content", Resources.HttpMessageInvalidMediaType, new object[]
								{
									FormattingUtilities.HttpContentType.Name,
									isRequest ? "application/http; msgtype=request" : "application/http; msgtype=response"
								});
							}
							return false;
						}
					}
				}
			}
			if (throwOnError)
			{
				throw Error.Argument("content", Resources.HttpMessageInvalidMediaType, new object[]
				{
					FormattingUtilities.HttpContentType.Name,
					isRequest ? "application/http; msgtype=request" : "application/http; msgtype=response"
				});
			}
			return false;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000D0E0 File Offset: 0x0000B2E0
		protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			if (stream == null)
			{
				throw Error.ArgumentNull("stream");
			}
			byte[] header = this.SerializeHeader();
			await stream.WriteAsync(header, 0, header.Length);
			if (this.Content != null)
			{
				Stream readStream = await this._streamTask.Value;
				this.ValidateStreamForReading(readStream);
				await this.Content.CopyToAsync(stream);
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000D130 File Offset: 0x0000B330
		protected override bool TryComputeLength(out long length)
		{
			bool flag = this._streamTask.Value != null;
			length = 0L;
			if (flag)
			{
				Stream stream;
				if (!this._streamTask.Value.TryGetResult(out stream) || stream == null || !stream.CanSeek)
				{
					length = -1L;
					return false;
				}
				length = stream.Length;
			}
			byte[] array = this.SerializeHeader();
			length += (long)array.Length;
			return true;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000D194 File Offset: 0x0000B394
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.HttpRequestMessage != null)
				{
					this.HttpRequestMessage.Dispose();
					this.HttpRequestMessage = null;
				}
				if (this.HttpResponseMessage != null)
				{
					this.HttpResponseMessage.Dispose();
					this.HttpResponseMessage = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		private static void SerializeRequestLine(StringBuilder message, HttpRequestMessage httpRequest)
		{
			message.Append(httpRequest.Method + " ");
			message.Append(httpRequest.RequestUri.PathAndQuery + " ");
			message.Append("HTTP/" + ((httpRequest.Version != null) ? httpRequest.Version.ToString(2) : "1.1") + "\r\n");
			if (httpRequest.Headers.Host == null)
			{
				message.Append("Host: " + httpRequest.RequestUri.Authority + "\r\n");
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000D27C File Offset: 0x0000B47C
		private static void SerializeStatusLine(StringBuilder message, HttpResponseMessage httpResponse)
		{
			message.Append("HTTP/" + ((httpResponse.Version != null) ? httpResponse.Version.ToString(2) : "1.1") + " ");
			message.Append((int)httpResponse.StatusCode + " ");
			message.Append(httpResponse.ReasonPhrase + "\r\n");
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		private static void SerializeHeaderFields(StringBuilder message, HttpHeaders headers)
		{
			if (headers != null)
			{
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in headers)
				{
					if (HttpMessageContent._singleValueHeaderFields.Contains(keyValuePair.Key))
					{
						using (IEnumerator<string> enumerator2 = keyValuePair.Value.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string str = enumerator2.Current;
								message.Append(keyValuePair.Key + ": " + str + "\r\n");
							}
							continue;
						}
					}
					if (HttpMessageContent._spaceSeparatedValueHeaderFields.Contains(keyValuePair.Key))
					{
						message.Append(keyValuePair.Key + ": " + string.Join(" ", keyValuePair.Value) + "\r\n");
					}
					else
					{
						message.Append(keyValuePair.Key + ": " + string.Join(", ", keyValuePair.Value) + "\r\n");
					}
				}
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000D41C File Offset: 0x0000B61C
		private byte[] SerializeHeader()
		{
			StringBuilder stringBuilder = new StringBuilder(2048);
			HttpHeaders headers;
			HttpContent content;
			if (this.HttpRequestMessage != null)
			{
				HttpMessageContent.SerializeRequestLine(stringBuilder, this.HttpRequestMessage);
				headers = this.HttpRequestMessage.Headers;
				content = this.HttpRequestMessage.Content;
			}
			else
			{
				HttpMessageContent.SerializeStatusLine(stringBuilder, this.HttpResponseMessage);
				headers = this.HttpResponseMessage.Headers;
				content = this.HttpResponseMessage.Content;
			}
			HttpMessageContent.SerializeHeaderFields(stringBuilder, headers);
			if (content != null)
			{
				HttpMessageContent.SerializeHeaderFields(stringBuilder, content.Headers);
			}
			stringBuilder.Append("\r\n");
			return Encoding.UTF8.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		private void ValidateStreamForReading(Stream stream)
		{
			if (this._contentConsumed)
			{
				if (stream == null || !stream.CanRead)
				{
					throw Error.InvalidOperation(Resources.HttpMessageContentAlreadyRead, new object[]
					{
						FormattingUtilities.HttpContentType.Name,
						(this.HttpRequestMessage != null) ? FormattingUtilities.HttpRequestMessageType.Name : FormattingUtilities.HttpResponseMessageType.Name
					});
				}
				stream.Position = 0L;
			}
			this._contentConsumed = true;
		}

		// Token: 0x040000E9 RID: 233
		private const string SP = " ";

		// Token: 0x040000EA RID: 234
		private const string ColonSP = ": ";

		// Token: 0x040000EB RID: 235
		private const string CRLF = "\r\n";

		// Token: 0x040000EC RID: 236
		private const string CommaSeparator = ", ";

		// Token: 0x040000ED RID: 237
		private const int DefaultHeaderAllocation = 2048;

		// Token: 0x040000EE RID: 238
		private const string DefaultMediaType = "application/http";

		// Token: 0x040000EF RID: 239
		private const string MsgTypeParameter = "msgtype";

		// Token: 0x040000F0 RID: 240
		private const string DefaultRequestMsgType = "request";

		// Token: 0x040000F1 RID: 241
		private const string DefaultResponseMsgType = "response";

		// Token: 0x040000F2 RID: 242
		private const string DefaultRequestMediaType = "application/http; msgtype=request";

		// Token: 0x040000F3 RID: 243
		private const string DefaultResponseMediaType = "application/http; msgtype=response";

		// Token: 0x040000F4 RID: 244
		private static readonly HashSet<string> _singleValueHeaderFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"Cookie",
			"Set-Cookie",
			"X-Powered-By"
		};

		// Token: 0x040000F5 RID: 245
		private static readonly HashSet<string> _spaceSeparatedValueHeaderFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"User-Agent"
		};

		// Token: 0x040000F6 RID: 246
		private bool _contentConsumed;

		// Token: 0x040000F7 RID: 247
		private Lazy<Task<Stream>> _streamTask;
	}
}
