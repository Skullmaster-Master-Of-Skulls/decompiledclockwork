using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Internal;
using System.Net.Http.Properties;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using Newtonsoft.Json;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000043 RID: 67
	public class JsonMediaTypeFormatter : BaseJsonMediaTypeFormatter
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00009660 File Offset: 0x00007860
		public JsonMediaTypeFormatter()
		{
			base.SupportedMediaTypes.Add(MediaTypeConstants.ApplicationJsonMediaType);
			base.SupportedMediaTypes.Add(MediaTypeConstants.TextJsonMediaType);
			this._requestHeaderMapping = new XmlHttpRequestHeaderMapping();
			base.MediaTypeMappings.Add(this._requestHeaderMapping);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000096C5 File Offset: 0x000078C5
		protected JsonMediaTypeFormatter(JsonMediaTypeFormatter formatter) : base(formatter)
		{
			this.UseDataContractJsonSerializer = formatter.UseDataContractJsonSerializer;
			this.Indent = formatter.Indent;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000096FC File Offset: 0x000078FC
		public static MediaTypeHeaderValue DefaultMediaType
		{
			get
			{
				return MediaTypeConstants.ApplicationJsonMediaType;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00009703 File Offset: 0x00007903
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000970B File Offset: 0x0000790B
		public bool UseDataContractJsonSerializer { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00009714 File Offset: 0x00007914
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000971C File Offset: 0x0000791C
		public bool Indent { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00009725 File Offset: 0x00007925
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000972D File Offset: 0x0000792D
		public sealed override int MaxDepth
		{
			get
			{
				return base.MaxDepth;
			}
			set
			{
				base.MaxDepth = value;
				this._readerQuotas.MaxDepth = value;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009742 File Offset: 0x00007942
		public override JsonReader CreateJsonReader(Type type, Stream readStream, Encoding effectiveEncoding)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			if (effectiveEncoding == null)
			{
				throw Error.ArgumentNull("effectiveEncoding");
			}
			return new JsonTextReader(new StreamReader(readStream, effectiveEncoding));
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009780 File Offset: 0x00007980
		public override JsonWriter CreateJsonWriter(Type type, Stream writeStream, Encoding effectiveEncoding)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (writeStream == null)
			{
				throw Error.ArgumentNull("writeStream");
			}
			if (effectiveEncoding == null)
			{
				throw Error.ArgumentNull("effectiveEncoding");
			}
			JsonWriter jsonWriter = new JsonTextWriter(new StreamWriter(writeStream, effectiveEncoding));
			if (this.Indent)
			{
				jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented;
			}
			return jsonWriter;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000097E4 File Offset: 0x000079E4
		public override bool CanReadType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (this.UseDataContractJsonSerializer)
			{
				DataContractJsonSerializer orAdd = this._dataContractSerializerCache.GetOrAdd(type, (Type t) => this.CreateDataContractSerializer(t, false));
				return orAdd != null;
			}
			return base.CanReadType(type);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009848 File Offset: 0x00007A48
		public override bool CanWriteType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (this.UseDataContractJsonSerializer)
			{
				MediaTypeFormatter.TryGetDelegatingTypeForIQueryableGenericOrSame(ref type);
				object orAdd = this._dataContractSerializerCache.GetOrAdd(type, (Type t) => this.CreateDataContractSerializer(t, false));
				return orAdd != null;
			}
			return base.CanWriteType(type);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000098A8 File Offset: 0x00007AA8
		public override object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			if (effectiveEncoding == null)
			{
				throw Error.ArgumentNull("effectiveEncoding");
			}
			if (this.UseDataContractJsonSerializer)
			{
				DataContractJsonSerializer dataContractSerializer = this.GetDataContractSerializer(type);
				using (XmlReader xmlReader = JsonReaderWriterFactory.CreateJsonReader(new NonClosingDelegatingStream(readStream), effectiveEncoding, this._readerQuotas, null))
				{
					return dataContractSerializer.ReadObject(xmlReader);
				}
			}
			return base.ReadFromStream(type, readStream, effectiveEncoding, formatterLogger);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000993C File Offset: 0x00007B3C
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (writeStream == null)
			{
				throw Error.ArgumentNull("writeStream");
			}
			if (this.UseDataContractJsonSerializer && this.Indent)
			{
				throw Error.NotSupported(Resources.UnsupportedIndent, new object[]
				{
					typeof(DataContractJsonSerializer)
				});
			}
			return base.WriteToStreamAsync(type, value, writeStream, content, transportContext, cancellationToken);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000099AC File Offset: 0x00007BAC
		public override void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (writeStream == null)
			{
				throw Error.ArgumentNull("writeStream");
			}
			if (effectiveEncoding == null)
			{
				throw Error.ArgumentNull("effectiveEncoding");
			}
			if (this.UseDataContractJsonSerializer)
			{
				if (MediaTypeFormatter.TryGetDelegatingTypeForIQueryableGenericOrSame(ref type) && value != null)
				{
					value = MediaTypeFormatter.GetTypeRemappingConstructor(type).Invoke(new object[]
					{
						value
					});
				}
				DataContractJsonSerializer dataContractSerializer = this.GetDataContractSerializer(type);
				using (XmlWriter xmlWriter = JsonReaderWriterFactory.CreateJsonWriter(writeStream, effectiveEncoding, false))
				{
					dataContractSerializer.WriteObject(xmlWriter, value);
					return;
				}
			}
			base.WriteToStream(type, value, writeStream, effectiveEncoding);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009A58 File Offset: 0x00007C58
		private DataContractJsonSerializer CreateDataContractSerializer(Type type, bool throwOnError)
		{
			DataContractJsonSerializer dataContractJsonSerializer = null;
			Exception ex = null;
			try
			{
				FormattingUtilities.XsdDataContractExporter.GetRootElementName(type);
				dataContractJsonSerializer = this.CreateDataContractSerializer(type);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (dataContractJsonSerializer != null || !throwOnError)
			{
				return dataContractJsonSerializer;
			}
			if (ex != null)
			{
				throw Error.InvalidOperation(ex, Resources.SerializerCannotSerializeType, new object[]
				{
					typeof(DataContractJsonSerializer).Name,
					type.Name
				});
			}
			throw Error.InvalidOperation(Resources.SerializerCannotSerializeType, new object[]
			{
				typeof(DataContractJsonSerializer).Name,
				type.Name
			});
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009B00 File Offset: 0x00007D00
		public virtual DataContractJsonSerializer CreateDataContractSerializer(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return new DataContractJsonSerializer(type);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009B38 File Offset: 0x00007D38
		private DataContractJsonSerializer GetDataContractSerializer(Type type)
		{
			DataContractJsonSerializer orAdd = this._dataContractSerializerCache.GetOrAdd(type, (Type t) => this.CreateDataContractSerializer(type, true));
			if (orAdd == null)
			{
				throw Error.InvalidOperation(Resources.SerializerCannotSerializeType, new object[]
				{
					typeof(DataContractJsonSerializer).Name,
					type.Name
				});
			}
			return orAdd;
		}

		// Token: 0x040000A4 RID: 164
		private ConcurrentDictionary<Type, DataContractJsonSerializer> _dataContractSerializerCache = new ConcurrentDictionary<Type, DataContractJsonSerializer>();

		// Token: 0x040000A5 RID: 165
		private XmlDictionaryReaderQuotas _readerQuotas = FormattingUtilities.CreateDefaultReaderQuotas();

		// Token: 0x040000A6 RID: 166
		private RequestHeaderMapping _requestHeaderMapping;
	}
}
