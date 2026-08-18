using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Services;
using Newtonsoft.Json;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200016E RID: 366
	internal class JsonMediaTypeFormatterTracer : JsonMediaTypeFormatter, IFormatterTracer, IDecorator<JsonMediaTypeFormatter>
	{
		// Token: 0x0600094E RID: 2382 RVA: 0x0001ECAD File Offset: 0x0001CEAD
		public JsonMediaTypeFormatterTracer(JsonMediaTypeFormatter innerFormatter, ITraceWriter traceWriter, HttpRequestMessage request) : base(innerFormatter)
		{
			this._inner = innerFormatter;
			this._innerTracer = new MediaTypeFormatterTracer(innerFormatter, traceWriter, request);
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0001ECCB File Offset: 0x0001CECB
		HttpRequestMessage IFormatterTracer.Request
		{
			get
			{
				return this._innerTracer.Request;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0001ECD8 File Offset: 0x0001CED8
		public JsonMediaTypeFormatter Inner
		{
			get
			{
				return this._inner;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
		public MediaTypeFormatter InnerFormatter
		{
			get
			{
				return this._innerTracer.InnerFormatter;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0001ECED File Offset: 0x0001CEED
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x0001ECFA File Offset: 0x0001CEFA
		public override IRequiredMemberSelector RequiredMemberSelector
		{
			get
			{
				return this._innerTracer.RequiredMemberSelector;
			}
			set
			{
				this._innerTracer.RequiredMemberSelector = value;
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001ED08 File Offset: 0x0001CF08
		public override bool CanReadType(Type type)
		{
			return this._innerTracer.CanReadType(type);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001ED16 File Offset: 0x0001CF16
		public override bool CanWriteType(Type type)
		{
			return this._innerTracer.CanWriteType(type);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001ED24 File Offset: 0x0001CF24
		public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			return this._innerTracer.GetPerRequestFormatterInstance(type, request, mediaType);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001ED34 File Offset: 0x0001CF34
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0001ED46 File Offset: 0x0001CF46
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger, cancellationToken);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0001ED5A File Offset: 0x0001CF5A
		public override object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
		{
			return this._inner.ReadFromStream(type, readStream, effectiveEncoding, formatterLogger);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0001ED6C File Offset: 0x0001CF6C
		public override JsonReader CreateJsonReader(Type type, Stream readStream, Encoding effectiveEncoding)
		{
			return this._inner.CreateJsonReader(type, readStream, effectiveEncoding);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001ED7C File Offset: 0x0001CF7C
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001ED90 File Offset: 0x0001CF90
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext, cancellationToken);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001EDA6 File Offset: 0x0001CFA6
		public override void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
		{
			this._inner.WriteToStream(type, value, writeStream, effectiveEncoding);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001EDB8 File Offset: 0x0001CFB8
		public override JsonWriter CreateJsonWriter(Type type, Stream writeStream, Encoding effectiveEncoding)
		{
			return this._inner.CreateJsonWriter(type, writeStream, effectiveEncoding);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001EDC8 File Offset: 0x0001CFC8
		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			this._innerTracer.SetDefaultContentHeaders(type, headers, mediaType);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001EDD8 File Offset: 0x0001CFD8
		public override JsonSerializer CreateJsonSerializer()
		{
			return this._inner.CreateJsonSerializer();
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001EDE5 File Offset: 0x0001CFE5
		public override DataContractJsonSerializer CreateDataContractSerializer(Type type)
		{
			return this._inner.CreateDataContractSerializer(type);
		}

		// Token: 0x040002C7 RID: 711
		private readonly JsonMediaTypeFormatter _inner;

		// Token: 0x040002C8 RID: 712
		private MediaTypeFormatterTracer _innerTracer;
	}
}
