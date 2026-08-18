using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200016C RID: 364
	internal class FormUrlEncodedMediaTypeFormatterTracer : FormUrlEncodedMediaTypeFormatter, IFormatterTracer, IDecorator<FormUrlEncodedMediaTypeFormatter>
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x0001E978 File Offset: 0x0001CB78
		public FormUrlEncodedMediaTypeFormatterTracer(FormUrlEncodedMediaTypeFormatter innerFormatter, ITraceWriter traceWriter, HttpRequestMessage request) : base(innerFormatter)
		{
			this._inner = innerFormatter;
			this._innerTracer = new MediaTypeFormatterTracer(innerFormatter, traceWriter, request);
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0001E996 File Offset: 0x0001CB96
		HttpRequestMessage IFormatterTracer.Request
		{
			get
			{
				return this._innerTracer.Request;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0001E9A3 File Offset: 0x0001CBA3
		public FormUrlEncodedMediaTypeFormatter Inner
		{
			get
			{
				return this._inner;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0001E9AB File Offset: 0x0001CBAB
		public MediaTypeFormatter InnerFormatter
		{
			get
			{
				return this._innerTracer.InnerFormatter;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0001E9B8 File Offset: 0x0001CBB8
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x0001E9C5 File Offset: 0x0001CBC5
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

		// Token: 0x0600093B RID: 2363 RVA: 0x0001E9D3 File Offset: 0x0001CBD3
		public override bool CanReadType(Type type)
		{
			return this._innerTracer.CanReadType(type);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001E9E1 File Offset: 0x0001CBE1
		public override bool CanWriteType(Type type)
		{
			return this._innerTracer.CanWriteType(type);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001E9EF File Offset: 0x0001CBEF
		public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			return this._innerTracer.GetPerRequestFormatterInstance(type, request, mediaType);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001E9FF File Offset: 0x0001CBFF
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger, cancellationToken);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001EA13 File Offset: 0x0001CC13
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001EA25 File Offset: 0x0001CC25
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext, cancellationToken);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001EA3B File Offset: 0x0001CC3B
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001EA4F File Offset: 0x0001CC4F
		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			this._innerTracer.SetDefaultContentHeaders(type, headers, mediaType);
		}

		// Token: 0x040002C2 RID: 706
		private readonly FormUrlEncodedMediaTypeFormatter _inner;

		// Token: 0x040002C3 RID: 707
		private MediaTypeFormatterTracer _innerTracer;
	}
}
