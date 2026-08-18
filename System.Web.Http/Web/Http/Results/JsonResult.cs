using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace System.Web.Http.Results
{
	// Token: 0x02000055 RID: 85
	public class JsonResult<T> : IHttpActionResult
	{
		// Token: 0x0600026E RID: 622 RVA: 0x00008E91 File Offset: 0x00007091
		public JsonResult(T content, JsonSerializerSettings serializerSettings, Encoding encoding, HttpRequestMessage request) : this(content, serializerSettings, encoding, new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008EA3 File Offset: 0x000070A3
		public JsonResult(T content, JsonSerializerSettings serializerSettings, Encoding encoding, ApiController controller) : this(content, serializerSettings, encoding, new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008EB8 File Offset: 0x000070B8
		private JsonResult(T content, JsonSerializerSettings serializerSettings, Encoding encoding, StatusCodeResult.IDependencyProvider dependencies)
		{
			if (serializerSettings == null)
			{
				throw new ArgumentNullException("serializerSettings");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this._content = content;
			this._serializerSettings = serializerSettings;
			this._encoding = encoding;
			this._dependencies = dependencies;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00008F04 File Offset: 0x00007104
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00008F0C File Offset: 0x0000710C
		public JsonSerializerSettings SerializerSettings
		{
			get
			{
				return this._serializerSettings;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00008F14 File Offset: 0x00007114
		public Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00008F1C File Offset: 0x0000711C
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00008F29 File Offset: 0x00007129
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00008F38 File Offset: 0x00007138
		private HttpResponseMessage Execute()
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
			try
			{
				ArraySegment<byte> arraySegment = this.Serialize();
				httpResponseMessage.Content = new ByteArrayContent(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue("application/json");
				mediaTypeHeaderValue.CharSet = this._encoding.WebName;
				httpResponseMessage.Content.Headers.ContentType = mediaTypeHeaderValue;
				httpResponseMessage.RequestMessage = this._dependencies.Request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008FD4 File Offset: 0x000071D4
		private ArraySegment<byte> Serialize()
		{
			JsonSerializer jsonSerializer = JsonSerializer.Create(this._serializerSettings);
			ArraySegment<byte> result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (TextWriter textWriter = new StreamWriter(memoryStream, this._encoding, 1024, true))
				{
					using (JsonWriter jsonWriter = new JsonTextWriter(textWriter)
					{
						CloseOutput = false
					})
					{
						jsonSerializer.Serialize(jsonWriter, this._content);
						jsonWriter.Flush();
					}
				}
				result = new ArraySegment<byte>(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			return result;
		}

		// Token: 0x040000AE RID: 174
		private readonly T _content;

		// Token: 0x040000AF RID: 175
		private readonly JsonSerializerSettings _serializerSettings;

		// Token: 0x040000B0 RID: 176
		private readonly Encoding _encoding;

		// Token: 0x040000B1 RID: 177
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
