using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Internal;
using System.Net.Http.Properties;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000050 RID: 80
	public class XmlMediaTypeFormatter : MediaTypeFormatter
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x0000A950 File Offset: 0x00008B50
		public XmlMediaTypeFormatter()
		{
			base.SupportedMediaTypes.Add(MediaTypeConstants.ApplicationXmlMediaType);
			base.SupportedMediaTypes.Add(MediaTypeConstants.TextXmlMediaType);
			base.SupportedEncodings.Add(new UTF8Encoding(false, true));
			base.SupportedEncodings.Add(new UnicodeEncoding(false, true, true));
			this.WriterSettings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				CloseOutput = false,
				CheckCharacters = false
			};
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		protected XmlMediaTypeFormatter(XmlMediaTypeFormatter formatter) : base(formatter)
		{
			this.UseXmlSerializer = formatter.UseXmlSerializer;
			this.WriterSettings = formatter.WriterSettings;
			this.MaxDepth = formatter.MaxDepth;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000AA2E File Offset: 0x00008C2E
		public static MediaTypeHeaderValue DefaultMediaType
		{
			get
			{
				return MediaTypeConstants.ApplicationXmlMediaType;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000AA35 File Offset: 0x00008C35
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000AA3D File Offset: 0x00008C3D
		[DefaultValue(false)]
		public bool UseXmlSerializer { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000AA46 File Offset: 0x00008C46
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000AA53 File Offset: 0x00008C53
		public bool Indent
		{
			get
			{
				return this.WriterSettings.Indent;
			}
			set
			{
				this.WriterSettings.Indent = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000AA61 File Offset: 0x00008C61
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000AA69 File Offset: 0x00008C69
		public XmlWriterSettings WriterSettings { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000AA72 File Offset: 0x00008C72
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000AA7F File Offset: 0x00008C7F
		public int MaxDepth
		{
			get
			{
				return this._readerQuotas.MaxDepth;
			}
			set
			{
				if (value < 1)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
				}
				this._readerQuotas.MaxDepth = value;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		public void SetSerializer(Type type, XmlObjectSerializer serializer)
		{
			this.VerifyAndSetSerializer(type, serializer);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000AAB2 File Offset: 0x00008CB2
		public void SetSerializer<T>(XmlObjectSerializer serializer)
		{
			this.SetSerializer(typeof(T), serializer);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000AAC5 File Offset: 0x00008CC5
		public void SetSerializer(Type type, XmlSerializer serializer)
		{
			this.VerifyAndSetSerializer(type, serializer);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000AACF File Offset: 0x00008CCF
		public void SetSerializer<T>(XmlSerializer serializer)
		{
			this.SetSerializer(typeof(T), serializer);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		public bool RemoveSerializer(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			object obj;
			return this._serializerCache.TryRemove(type, out obj);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000AB14 File Offset: 0x00008D14
		public override bool CanReadType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			object cachedSerializer = this.GetCachedSerializer(type, false);
			return cachedSerializer != null;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000AB48 File Offset: 0x00008D48
		public override bool CanWriteType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (this.UseXmlSerializer)
			{
				MediaTypeFormatter.TryGetDelegatingTypeForIEnumerableGenericOrSame(ref type);
			}
			else
			{
				MediaTypeFormatter.TryGetDelegatingTypeForIQueryableGenericOrSame(ref type);
			}
			object cachedSerializer = this.GetCachedSerializer(type, false);
			return cachedSerializer != null;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000AB94 File Offset: 0x00008D94
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			Task<object> result;
			try
			{
				result = Task.FromResult<object>(this.ReadFromStream(type, readStream, content, formatterLogger));
			}
			catch (Exception exception)
			{
				result = TaskHelpers.FromError<object>(exception);
			}
			return result;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000ABF4 File Offset: 0x00008DF4
		private object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			HttpContentHeaders httpContentHeaders = (content == null) ? null : content.Headers;
			if (httpContentHeaders != null && httpContentHeaders.ContentLength == 0L)
			{
				return MediaTypeFormatter.GetDefaultValueForType(type);
			}
			object deserializer = this.GetDeserializer(type, content);
			object result;
			try
			{
				using (XmlReader xmlReader = this.CreateXmlReader(readStream, content))
				{
					XmlSerializer xmlSerializer = deserializer as XmlSerializer;
					if (xmlSerializer != null)
					{
						result = xmlSerializer.Deserialize(xmlReader);
					}
					else
					{
						XmlObjectSerializer xmlObjectSerializer = deserializer as XmlObjectSerializer;
						if (xmlObjectSerializer == null)
						{
							XmlMediaTypeFormatter.ThrowInvalidSerializerException(deserializer, "GetDeserializer");
						}
						result = xmlObjectSerializer.ReadObject(xmlReader);
					}
				}
			}
			catch (Exception exception)
			{
				if (formatterLogger == null)
				{
					throw;
				}
				formatterLogger.LogError(string.Empty, exception);
				result = MediaTypeFormatter.GetDefaultValueForType(type);
			}
			return result;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000ACCC File Offset: 0x00008ECC
		protected internal virtual object GetDeserializer(Type type, HttpContent content)
		{
			return this.GetSerializerForType(type);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000ACD8 File Offset: 0x00008ED8
		protected internal virtual XmlReader CreateXmlReader(Stream readStream, HttpContent content)
		{
			Encoding encoding = base.SelectCharacterEncoding((content == null) ? null : content.Headers);
			return XmlDictionaryReader.CreateTextReader(new NonClosingDelegatingStream(readStream), encoding, this._readerQuotas, null);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000AD0C File Offset: 0x00008F0C
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
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelpers.Canceled();
			}
			Task result;
			try
			{
				this.WriteToStream(type, value, writeStream, content);
				result = TaskHelpers.Completed();
			}
			catch (Exception exception)
			{
				result = TaskHelpers.FromError(exception);
			}
			return result;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000AD78 File Offset: 0x00008F78
		private void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			bool flag;
			if (this.UseXmlSerializer)
			{
				flag = MediaTypeFormatter.TryGetDelegatingTypeForIEnumerableGenericOrSame(ref type);
			}
			else
			{
				flag = MediaTypeFormatter.TryGetDelegatingTypeForIQueryableGenericOrSame(ref type);
			}
			if (flag && value != null)
			{
				value = MediaTypeFormatter.GetTypeRemappingConstructor(type).Invoke(new object[]
				{
					value
				});
			}
			object serializer = this.GetSerializer(type, value, content);
			using (XmlWriter xmlWriter = this.CreateXmlWriter(writeStream, content))
			{
				XmlSerializer xmlSerializer = serializer as XmlSerializer;
				if (xmlSerializer != null)
				{
					xmlSerializer.Serialize(xmlWriter, value);
				}
				else
				{
					XmlObjectSerializer xmlObjectSerializer = serializer as XmlObjectSerializer;
					if (xmlObjectSerializer == null)
					{
						XmlMediaTypeFormatter.ThrowInvalidSerializerException(serializer, "GetSerializer");
					}
					xmlObjectSerializer.WriteObject(xmlWriter, value);
				}
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000AE28 File Offset: 0x00009028
		protected internal virtual object GetSerializer(Type type, object value, HttpContent content)
		{
			return this.GetSerializerForType(type);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000AE34 File Offset: 0x00009034
		protected internal virtual XmlWriter CreateXmlWriter(Stream writeStream, HttpContent content)
		{
			Encoding encoding = base.SelectCharacterEncoding((content != null) ? content.Headers : null);
			XmlWriterSettings xmlWriterSettings = this.WriterSettings.Clone();
			xmlWriterSettings.Encoding = encoding;
			return XmlWriter.Create(writeStream, xmlWriterSettings);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000AE6E File Offset: 0x0000906E
		public virtual XmlSerializer CreateXmlSerializer(Type type)
		{
			return new XmlSerializer(type);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000AE76 File Offset: 0x00009076
		public virtual DataContractSerializer CreateDataContractSerializer(Type type)
		{
			return new DataContractSerializer(type);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000AE7E File Offset: 0x0000907E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public XmlReader InvokeCreateXmlReader(Stream readStream, HttpContent content)
		{
			return this.CreateXmlReader(readStream, content);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000AE88 File Offset: 0x00009088
		[EditorBrowsable(EditorBrowsableState.Never)]
		public XmlWriter InvokeCreateXmlWriter(Stream writeStream, HttpContent content)
		{
			return this.CreateXmlWriter(writeStream, content);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000AE92 File Offset: 0x00009092
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object InvokeGetDeserializer(Type type, HttpContent content)
		{
			return this.GetDeserializer(type, content);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000AE9C File Offset: 0x0000909C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object InvokeGetSerializer(Type type, object value, HttpContent content)
		{
			return this.GetSerializer(type, value, content);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000AEA8 File Offset: 0x000090A8
		private object CreateDefaultSerializer(Type type, bool throwOnError)
		{
			Exception ex = null;
			object obj = null;
			try
			{
				if (this.UseXmlSerializer)
				{
					obj = this.CreateXmlSerializer(type);
				}
				else
				{
					FormattingUtilities.XsdDataContractExporter.GetRootElementName(type);
					obj = this.CreateDataContractSerializer(type);
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (obj != null || !throwOnError)
			{
				return obj;
			}
			if (ex != null)
			{
				throw Error.InvalidOperation(ex, Resources.SerializerCannotSerializeType, new object[]
				{
					this.UseXmlSerializer ? typeof(XmlSerializer).Name : typeof(DataContractSerializer).Name,
					type.Name
				});
			}
			throw Error.InvalidOperation(Resources.SerializerCannotSerializeType, new object[]
			{
				this.UseXmlSerializer ? typeof(XmlSerializer).Name : typeof(DataContractSerializer).Name,
				type.Name
			});
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000AF9C File Offset: 0x0000919C
		private object GetCachedSerializer(Type type, bool throwOnError)
		{
			object obj;
			if (!this._serializerCache.TryGetValue(type, out obj))
			{
				obj = this.CreateDefaultSerializer(type, throwOnError);
				this._serializerCache.TryAdd(type, obj);
			}
			return obj;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000AFD1 File Offset: 0x000091D1
		private void VerifyAndSetSerializer(Type type, object serializer)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (serializer == null)
			{
				throw Error.ArgumentNull("serializer");
			}
			this.SetSerializerInternal(type, serializer);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B010 File Offset: 0x00009210
		private void SetSerializerInternal(Type type, object serializer)
		{
			this._serializerCache.AddOrUpdate(type, serializer, (Type key, object value) => serializer);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B04C File Offset: 0x0000924C
		private object GetSerializerForType(Type type)
		{
			object cachedSerializer = this.GetCachedSerializer(type, true);
			if (cachedSerializer == null)
			{
				throw Error.InvalidOperation(Resources.SerializerCannotSerializeType, new object[]
				{
					this.UseXmlSerializer ? typeof(XmlSerializer).Name : typeof(DataContractSerializer).Name,
					type.Name
				});
			}
			return cachedSerializer;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B0B0 File Offset: 0x000092B0
		private static void ThrowInvalidSerializerException(object serializer, string getSerializerMethodName)
		{
			if (serializer == null)
			{
				throw Error.InvalidOperation(Resources.XmlMediaTypeFormatter_NullReturnedSerializer, new object[]
				{
					getSerializerMethodName
				});
			}
			throw Error.InvalidOperation(Resources.XmlMediaTypeFormatter_InvalidSerializerType, new object[]
			{
				serializer.GetType().Name,
				getSerializerMethodName
			});
		}

		// Token: 0x040000CF RID: 207
		private ConcurrentDictionary<Type, object> _serializerCache = new ConcurrentDictionary<Type, object>();

		// Token: 0x040000D0 RID: 208
		private XmlDictionaryReaderQuotas _readerQuotas = FormattingUtilities.CreateDefaultReaderQuotas();
	}
}
