using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Services;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000172 RID: 370
	internal class XmlMediaTypeFormatterTracer : XmlMediaTypeFormatter, IFormatterTracer, IDecorator<XmlMediaTypeFormatter>
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x0001F7D5 File Offset: 0x0001D9D5
		public XmlMediaTypeFormatterTracer(XmlMediaTypeFormatter innerFormatter, ITraceWriter traceWriter, HttpRequestMessage request) : base(innerFormatter)
		{
			this._inner = innerFormatter;
			this._innerTracer = new MediaTypeFormatterTracer(innerFormatter, traceWriter, request);
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x0001F7F3 File Offset: 0x0001D9F3
		HttpRequestMessage IFormatterTracer.Request
		{
			get
			{
				return this._innerTracer.Request;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0001F800 File Offset: 0x0001DA00
		public XmlMediaTypeFormatter Inner
		{
			get
			{
				return this._inner;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001F808 File Offset: 0x0001DA08
		public MediaTypeFormatter InnerFormatter
		{
			get
			{
				return this._innerTracer.InnerFormatter;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0001F815 File Offset: 0x0001DA15
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0001F822 File Offset: 0x0001DA22
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

		// Token: 0x0600098B RID: 2443 RVA: 0x0001F830 File Offset: 0x0001DA30
		public override bool CanReadType(Type type)
		{
			return this._innerTracer.CanReadType(type);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001F83E File Offset: 0x0001DA3E
		public override bool CanWriteType(Type type)
		{
			return this._innerTracer.CanWriteType(type);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001F84C File Offset: 0x0001DA4C
		public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			return this._innerTracer.GetPerRequestFormatterInstance(type, request, mediaType);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001F85C File Offset: 0x0001DA5C
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger, cancellationToken);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001F870 File Offset: 0x0001DA70
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001F882 File Offset: 0x0001DA82
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext, cancellationToken);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001F898 File Offset: 0x0001DA98
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001F8AC File Offset: 0x0001DAAC
		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			this._innerTracer.SetDefaultContentHeaders(type, headers, mediaType);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001F8BC File Offset: 0x0001DABC
		public override XmlSerializer CreateXmlSerializer(Type type)
		{
			return this._inner.CreateXmlSerializer(type);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001F8CA File Offset: 0x0001DACA
		public override DataContractSerializer CreateDataContractSerializer(Type type)
		{
			return this._inner.CreateDataContractSerializer(type);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001F8D8 File Offset: 0x0001DAD8
		protected override XmlReader CreateXmlReader(Stream readStream, HttpContent content)
		{
			return this._inner.InvokeCreateXmlReader(readStream, content);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001F8E7 File Offset: 0x0001DAE7
		protected override XmlWriter CreateXmlWriter(Stream writeStream, HttpContent content)
		{
			return this._inner.InvokeCreateXmlWriter(writeStream, content);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001F8F6 File Offset: 0x0001DAF6
		protected override object GetDeserializer(Type type, HttpContent content)
		{
			return this._inner.InvokeGetDeserializer(type, content);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001F905 File Offset: 0x0001DB05
		protected override object GetSerializer(Type type, object value, HttpContent content)
		{
			return this._inner.InvokeGetSerializer(type, value, content);
		}

		// Token: 0x040002D7 RID: 727
		private readonly XmlMediaTypeFormatter _inner;

		// Token: 0x040002D8 RID: 728
		private readonly MediaTypeFormatterTracer _innerTracer;
	}
}
