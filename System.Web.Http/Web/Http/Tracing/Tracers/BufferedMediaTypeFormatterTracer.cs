using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000165 RID: 357
	internal class BufferedMediaTypeFormatterTracer : BufferedMediaTypeFormatter, IFormatterTracer, IDecorator<BufferedMediaTypeFormatter>
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x0001DA8B File Offset: 0x0001BC8B
		public BufferedMediaTypeFormatterTracer(BufferedMediaTypeFormatter innerFormatter, ITraceWriter traceWriter, HttpRequestMessage request) : base(innerFormatter)
		{
			this._inner = innerFormatter;
			this._innerTracer = new MediaTypeFormatterTracer(innerFormatter, traceWriter, request);
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0001DAA9 File Offset: 0x0001BCA9
		HttpRequestMessage IFormatterTracer.Request
		{
			get
			{
				return this._innerTracer.Request;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0001DAB6 File Offset: 0x0001BCB6
		public BufferedMediaTypeFormatter Inner
		{
			get
			{
				return this._inner;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0001DABE File Offset: 0x0001BCBE
		public MediaTypeFormatter InnerFormatter
		{
			get
			{
				return this._innerTracer.InnerFormatter;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0001DACB File Offset: 0x0001BCCB
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x0001DAD8 File Offset: 0x0001BCD8
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

		// Token: 0x06000909 RID: 2313 RVA: 0x0001DAE6 File Offset: 0x0001BCE6
		public override bool CanReadType(Type type)
		{
			return this._innerTracer.CanReadType(type);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001DAF4 File Offset: 0x0001BCF4
		public override bool CanWriteType(Type type)
		{
			return this._innerTracer.CanWriteType(type);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001DB02 File Offset: 0x0001BD02
		public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			return this._innerTracer.GetPerRequestFormatterInstance(type, request, mediaType);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001DB12 File Offset: 0x0001BD12
		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			this._innerTracer.SetDefaultContentHeaders(type, headers, mediaType);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001DB24 File Offset: 0x0001BD24
		public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this.ReadFromStreamCore(type, readStream, content, formatterLogger, null);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001DB45 File Offset: 0x0001BD45
		public override object ReadFromStream(Type type, Stream stream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this.ReadFromStreamCore(type, stream, content, formatterLogger, new CancellationToken?(cancellationToken));
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001DC64 File Offset: 0x0001BE64
		private object ReadFromStreamCore(Type type, Stream stream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken? cancellationToken = null)
		{
			BufferedMediaTypeFormatter innerFormatter = this.InnerFormatter as BufferedMediaTypeFormatter;
			HttpContentHeaders httpContentHeaders = (content == null) ? null : content.Headers;
			MediaTypeHeaderValue contentType = (httpContentHeaders == null) ? null : httpContentHeaders.ContentType;
			object value = null;
			this._innerTracer.TraceWriter.TraceBeginEnd(this._innerTracer.Request, TraceCategories.FormattingCategory, TraceLevel.Info, this._innerTracer.InnerFormatter.GetType().Name, "ReadFromStream", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceReadFromStreamMessage, new object[]
				{
					type.Name,
					(contentType == null) ? SRResources.TraceNoneObjectMessage : contentType.ToString()
				});
			}, delegate
			{
				if (cancellationToken != null)
				{
					value = innerFormatter.ReadFromStream(type, stream, content, formatterLogger, cancellationToken.Value);
					return;
				}
				value = innerFormatter.ReadFromStream(type, stream, content, formatterLogger);
			}, delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceReadFromStreamValueMessage, new object[]
				{
					FormattingUtilities.ValueToString(value, CultureInfo.CurrentCulture)
				});
			}, null);
			return value;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001DD44 File Offset: 0x0001BF44
		public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			this.WriteToStreamCore(type, value, writeStream, content, null);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001DD65 File Offset: 0x0001BF65
		public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content, CancellationToken cancellationToken)
		{
			this.WriteToStreamCore(type, value, writeStream, content, new CancellationToken?(cancellationToken));
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001DE54 File Offset: 0x0001C054
		private void WriteToStreamCore(Type type, object value, Stream writeStream, HttpContent content, CancellationToken? cancellationToken = null)
		{
			BufferedMediaTypeFormatter innerFormatter = this.InnerFormatter as BufferedMediaTypeFormatter;
			HttpContentHeaders httpContentHeaders = (content == null) ? null : content.Headers;
			MediaTypeHeaderValue contentType = (httpContentHeaders == null) ? null : httpContentHeaders.ContentType;
			this._innerTracer.TraceWriter.TraceBeginEnd(this._innerTracer.Request, TraceCategories.FormattingCategory, TraceLevel.Info, this._innerTracer.InnerFormatter.GetType().Name, "WriteToStream", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceWriteToStreamMessage, new object[]
				{
					FormattingUtilities.ValueToString(value, CultureInfo.CurrentCulture),
					type.Name,
					(contentType == null) ? SRResources.TraceNoneObjectMessage : contentType.ToString()
				});
			}, delegate
			{
				if (cancellationToken != null)
				{
					innerFormatter.WriteToStream(type, value, writeStream, content, cancellationToken.Value);
					return;
				}
				innerFormatter.WriteToStream(type, value, writeStream, content);
			}, null, null);
		}

		// Token: 0x040002AF RID: 687
		private const string OnReadFromStreamMethodName = "ReadFromStream";

		// Token: 0x040002B0 RID: 688
		private const string OnWriteToStreamMethodName = "WriteToStream";

		// Token: 0x040002B1 RID: 689
		private readonly BufferedMediaTypeFormatter _inner;

		// Token: 0x040002B2 RID: 690
		private MediaTypeFormatterTracer _innerTracer;
	}
}
