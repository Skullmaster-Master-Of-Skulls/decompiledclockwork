using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200016F RID: 367
	internal class MediaTypeFormatterTracer : MediaTypeFormatter, IFormatterTracer, IDecorator<MediaTypeFormatter>
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x0001EDF3 File Offset: 0x0001CFF3
		public MediaTypeFormatterTracer(MediaTypeFormatter innerFormatter, ITraceWriter traceWriter, HttpRequestMessage request) : base(innerFormatter)
		{
			this.InnerFormatter = innerFormatter;
			this.TraceWriter = traceWriter;
			this.Request = request;
			this._inner = innerFormatter;
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0001EE18 File Offset: 0x0001D018
		public MediaTypeFormatter Inner
		{
			get
			{
				return this._inner;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001EE20 File Offset: 0x0001D020
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x0001EE28 File Offset: 0x0001D028
		public MediaTypeFormatter InnerFormatter { get; private set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0001EE31 File Offset: 0x0001D031
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x0001EE39 File Offset: 0x0001D039
		public ITraceWriter TraceWriter { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001EE42 File Offset: 0x0001D042
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x0001EE4A File Offset: 0x0001D04A
		public HttpRequestMessage Request { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0001EE53 File Offset: 0x0001D053
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0001EE60 File Offset: 0x0001D060
		public override IRequiredMemberSelector RequiredMemberSelector
		{
			get
			{
				return this.InnerFormatter.RequiredMemberSelector;
			}
			set
			{
				this.InnerFormatter.RequiredMemberSelector = value;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001EE70 File Offset: 0x0001D070
		public static MediaTypeFormatter ActualMediaTypeFormatter(MediaTypeFormatter formatter)
		{
			IFormatterTracer formatterTracer = formatter as IFormatterTracer;
			if (formatterTracer != null)
			{
				return formatterTracer.InnerFormatter;
			}
			return formatter;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001EE90 File Offset: 0x0001D090
		public static MediaTypeFormatter CreateTracer(MediaTypeFormatter formatter, ITraceWriter traceWriter, HttpRequestMessage request)
		{
			IFormatterTracer formatterTracer = formatter as IFormatterTracer;
			if (formatterTracer != null)
			{
				if (formatterTracer.Request == request)
				{
					return formatter;
				}
				formatter = formatterTracer.InnerFormatter;
			}
			XmlMediaTypeFormatter xmlMediaTypeFormatter = formatter as XmlMediaTypeFormatter;
			JsonMediaTypeFormatter jsonMediaTypeFormatter = formatter as JsonMediaTypeFormatter;
			FormUrlEncodedMediaTypeFormatter formUrlEncodedMediaTypeFormatter = formatter as FormUrlEncodedMediaTypeFormatter;
			BufferedMediaTypeFormatter bufferedMediaTypeFormatter = formatter as BufferedMediaTypeFormatter;
			MediaTypeFormatter result;
			if (xmlMediaTypeFormatter != null)
			{
				result = new XmlMediaTypeFormatterTracer(xmlMediaTypeFormatter, traceWriter, request);
			}
			else if (jsonMediaTypeFormatter != null)
			{
				result = new JsonMediaTypeFormatterTracer(jsonMediaTypeFormatter, traceWriter, request);
			}
			else if (formUrlEncodedMediaTypeFormatter != null)
			{
				result = new FormUrlEncodedMediaTypeFormatterTracer(formUrlEncodedMediaTypeFormatter, traceWriter, request);
			}
			else if (bufferedMediaTypeFormatter != null)
			{
				result = new BufferedMediaTypeFormatterTracer(bufferedMediaTypeFormatter, traceWriter, request);
			}
			else
			{
				result = new MediaTypeFormatterTracer(formatter, traceWriter, request);
			}
			return result;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001F01C File Offset: 0x0001D21C
		public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			MediaTypeFormatter formatter = null;
			this.TraceWriter.TraceBeginEnd(request, TraceCategories.FormattingCategory, TraceLevel.Info, this.InnerFormatter.GetType().Name, "GetPerRequestFormatterInstance", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceGetPerRequestFormatterMessage, new object[]
				{
					this.InnerFormatter.GetType().Name,
					type.Name,
					mediaType
				});
			}, delegate
			{
				formatter = this.InnerFormatter.GetPerRequestFormatterInstance(type, request, mediaType);
			}, delegate(TraceRecord tr)
			{
				if (formatter == null)
				{
					tr.Message = SRResources.TraceGetPerRequestNullFormatterEndMessage;
					return;
				}
				string format = object.ReferenceEquals(MediaTypeFormatterTracer.ActualMediaTypeFormatter(formatter), this.InnerFormatter) ? SRResources.TraceGetPerRequestFormatterEndMessage : SRResources.TraceGetPerRequestFormatterEndMessageNew;
				tr.Message = Error.Format(format, new object[]
				{
					formatter.GetType().Name
				});
			}, null);
			if (formatter != null && !(formatter is IFormatterTracer))
			{
				formatter = MediaTypeFormatterTracer.CreateTracer(formatter, this.TraceWriter, request);
			}
			return formatter;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001F0DB File Offset: 0x0001D2DB
		public override bool CanReadType(Type type)
		{
			return this.InnerFormatter.CanReadType(type);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001F0E9 File Offset: 0x0001D2E9
		public override bool CanWriteType(Type type)
		{
			return this.InnerFormatter.CanWriteType(type);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001F0F7 File Offset: 0x0001D2F7
		public override bool Equals(object obj)
		{
			return this.InnerFormatter.Equals(obj);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001F105 File Offset: 0x0001D305
		public override int GetHashCode()
		{
			return this.InnerFormatter.GetHashCode();
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001F112 File Offset: 0x0001D312
		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			this.InnerFormatter.SetDefaultContentHeaders(type, headers, mediaType);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001F122 File Offset: 0x0001D322
		public override string ToString()
		{
			return this.InnerFormatter.ToString();
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001F130 File Offset: 0x0001D330
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this.ReadFromStreamAsyncCore(type, readStream, content, formatterLogger, null);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001F151 File Offset: 0x0001D351
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this.ReadFromStreamAsyncCore(type, readStream, content, formatterLogger, new CancellationToken?(cancellationToken));
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001F26C File Offset: 0x0001D46C
		private Task<object> ReadFromStreamAsyncCore(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken? cancellationToken)
		{
			HttpContentHeaders httpContentHeaders = (content == null) ? null : content.Headers;
			MediaTypeHeaderValue contentType = (httpContentHeaders == null) ? null : httpContentHeaders.ContentType;
			IFormatterLogger formatterLoggerTraceWrapper = (formatterLogger == null) ? null : new FormatterLoggerTraceWrapper(formatterLogger, this.TraceWriter, this.Request, this.InnerFormatter.GetType().Name, "ReadFromStreamAsync");
			return this.TraceWriter.TraceBeginEndAsync(this.Request, TraceCategories.FormattingCategory, TraceLevel.Info, this.InnerFormatter.GetType().Name, "ReadFromStreamAsync", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceReadFromStreamMessage, new object[]
				{
					type.Name,
					(contentType == null) ? SRResources.TraceNoneObjectMessage : contentType.ToString()
				});
			}, delegate()
			{
				if (cancellationToken != null)
				{
					return this.InnerFormatter.ReadFromStreamAsync(type, readStream, content, formatterLoggerTraceWrapper, cancellationToken.Value);
				}
				return this.InnerFormatter.ReadFromStreamAsync(type, readStream, content, formatterLoggerTraceWrapper);
			}, delegate(TraceRecord tr, object value)
			{
				tr.Message = Error.Format(SRResources.TraceReadFromStreamValueMessage, new object[]
				{
					FormattingUtilities.ValueToString(value, CultureInfo.CurrentCulture)
				});
			}, null);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001F364 File Offset: 0x0001D564
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this.WriteToStreamAsyncCore(type, value, writeStream, content, transportContext, null);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001F387 File Offset: 0x0001D587
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			return this.WriteToStreamAsyncCore(type, value, writeStream, content, transportContext, new CancellationToken?(cancellationToken));
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001F490 File Offset: 0x0001D690
		private Task WriteToStreamAsyncCore(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken? cancellationToken = null)
		{
			HttpContentHeaders httpContentHeaders = (content == null) ? null : content.Headers;
			MediaTypeHeaderValue contentType = (httpContentHeaders == null) ? null : httpContentHeaders.ContentType;
			return this.TraceWriter.TraceBeginEndAsync(this.Request, TraceCategories.FormattingCategory, TraceLevel.Info, this.InnerFormatter.GetType().Name, "WriteToStreamAsync", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceWriteToStreamMessage, new object[]
				{
					FormattingUtilities.ValueToString(value, CultureInfo.CurrentCulture),
					type.Name,
					(contentType == null) ? SRResources.TraceNoneObjectMessage : contentType.ToString()
				});
			}, delegate()
			{
				if (cancellationToken != null)
				{
					return this.InnerFormatter.WriteToStreamAsync(type, value, writeStream, content, transportContext, cancellationToken.Value);
				}
				return this.InnerFormatter.WriteToStreamAsync(type, value, writeStream, content, transportContext);
			}, null, null);
		}

		// Token: 0x040002C9 RID: 713
		private const string ReadFromStreamAsyncMethodName = "ReadFromStreamAsync";

		// Token: 0x040002CA RID: 714
		private const string WriteToStreamAsyncMethodName = "WriteToStreamAsync";

		// Token: 0x040002CB RID: 715
		private const string GetPerRequestFormatterInstanceMethodName = "GetPerRequestFormatterInstance";

		// Token: 0x040002CC RID: 716
		private readonly MediaTypeFormatter _inner;
	}
}
