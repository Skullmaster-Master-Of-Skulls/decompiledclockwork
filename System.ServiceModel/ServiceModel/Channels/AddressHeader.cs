using System;
using System.Runtime.Serialization;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009AF RID: 2479
	[__DynamicallyInvokable]
	public abstract class AddressHeader
	{
		// Token: 0x06006136 RID: 24886 RVA: 0x0016A9E3 File Offset: 0x00168BE3
		[__DynamicallyInvokable]
		protected AddressHeader()
		{
		}

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06006137 RID: 24887 RVA: 0x0016A9EC File Offset: 0x00168BEC
		internal bool IsReferenceProperty
		{
			get
			{
				BufferedAddressHeader bufferedAddressHeader = this as BufferedAddressHeader;
				return bufferedAddressHeader != null && bufferedAddressHeader.IsReferencePropertyHeader;
			}
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06006138 RID: 24888
		[__DynamicallyInvokable]
		public abstract string Name { [__DynamicallyInvokable] get; }

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x06006139 RID: 24889
		[__DynamicallyInvokable]
		public abstract string Namespace { [__DynamicallyInvokable] get; }

		// Token: 0x0600613A RID: 24890 RVA: 0x0016AA0C File Offset: 0x00168C0C
		public static AddressHeader CreateAddressHeader(object value)
		{
			Type objectType = AddressHeader.GetObjectType(value);
			return AddressHeader.CreateAddressHeader(value, DataContractSerializerDefaults.CreateSerializer(objectType, int.MaxValue));
		}

		// Token: 0x0600613B RID: 24891 RVA: 0x0016AA31 File Offset: 0x00168C31
		public static AddressHeader CreateAddressHeader(object value, XmlObjectSerializer serializer)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serializer"));
			}
			return new AddressHeader.XmlObjectSerializerAddressHeader(value, serializer);
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x0016AA52 File Offset: 0x00168C52
		[__DynamicallyInvokable]
		public static AddressHeader CreateAddressHeader(string name, string ns, object value)
		{
			return AddressHeader.CreateAddressHeader(name, ns, value, DataContractSerializerDefaults.CreateSerializer(AddressHeader.GetObjectType(value), name, ns, int.MaxValue));
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x0016AA6E File Offset: 0x00168C6E
		internal static AddressHeader CreateAddressHeader(XmlDictionaryString name, XmlDictionaryString ns, object value)
		{
			return new AddressHeader.DictionaryAddressHeader(name, ns, value);
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x0016AA78 File Offset: 0x00168C78
		[__DynamicallyInvokable]
		public static AddressHeader CreateAddressHeader(string name, string ns, object value, XmlObjectSerializer serializer)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serializer"));
			}
			return new AddressHeader.XmlObjectSerializerAddressHeader(name, ns, value, serializer);
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x0016AA9B File Offset: 0x00168C9B
		private static Type GetObjectType(object value)
		{
			if (value != null)
			{
				return value.GetType();
			}
			return typeof(object);
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x0016AAB4 File Offset: 0x00168CB4
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			AddressHeader addressHeader = obj as AddressHeader;
			if (addressHeader == null)
			{
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string comparableForm = this.GetComparableForm(stringBuilder);
			stringBuilder.Remove(0, stringBuilder.Length);
			string comparableForm2 = addressHeader.GetComparableForm(stringBuilder);
			return comparableForm.Length == comparableForm2.Length && string.CompareOrdinal(comparableForm, comparableForm2) == 0;
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x0016AB0D File Offset: 0x00168D0D
		internal string GetComparableForm()
		{
			return this.GetComparableForm(new StringBuilder());
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x0016AB1A File Offset: 0x00168D1A
		internal string GetComparableForm(StringBuilder builder)
		{
			return EndpointAddressProcessor.GetComparableForm(builder, this.GetComparableReader());
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x0016AB28 File Offset: 0x00168D28
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.GetComparableForm().GetHashCode();
		}

		// Token: 0x06006144 RID: 24900 RVA: 0x0016AB35 File Offset: 0x00168D35
		[__DynamicallyInvokable]
		public T GetValue<T>()
		{
			return this.GetValue<T>(DataContractSerializerDefaults.CreateSerializer(typeof(T), this.Name, this.Namespace, int.MaxValue));
		}

		// Token: 0x06006145 RID: 24901 RVA: 0x0016AB60 File Offset: 0x00168D60
		[__DynamicallyInvokable]
		public T GetValue<T>(XmlObjectSerializer serializer)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serializer"));
			}
			T result;
			using (XmlDictionaryReader addressHeaderReader = this.GetAddressHeaderReader())
			{
				if (!serializer.IsStartObject(addressHeaderReader))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ExpectedElementMissing", new object[]
					{
						this.Name,
						this.Namespace
					})));
				}
				result = (T)((object)serializer.ReadObject(addressHeaderReader));
			}
			return result;
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x0016ABF4 File Offset: 0x00168DF4
		[__DynamicallyInvokable]
		public virtual XmlDictionaryReader GetAddressHeaderReader()
		{
			XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter writer = xmlBuffer.OpenSection(XmlDictionaryReaderQuotas.Max);
			this.WriteAddressHeader(writer);
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			return xmlBuffer.GetReader(0);
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x0016AC34 File Offset: 0x00168E34
		private XmlDictionaryReader GetComparableReader()
		{
			XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(XmlDictionaryReaderQuotas.Max);
			AddressHeader.ParameterHeader.WriteStartHeader(xmlDictionaryWriter, this, AddressingVersion.WSAddressingAugust2004);
			AddressHeader.ParameterHeader.WriteHeaderContents(xmlDictionaryWriter, this);
			xmlDictionaryWriter.WriteEndElement();
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			return xmlBuffer.GetReader(0);
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x0016AC84 File Offset: 0x00168E84
		[__DynamicallyInvokable]
		protected virtual void OnWriteStartAddressHeader(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement(this.Name, this.Namespace);
		}

		// Token: 0x06006149 RID: 24905
		[__DynamicallyInvokable]
		protected abstract void OnWriteAddressHeaderContents(XmlDictionaryWriter writer);

		// Token: 0x0600614A RID: 24906 RVA: 0x0016AC98 File Offset: 0x00168E98
		[__DynamicallyInvokable]
		public MessageHeader ToMessageHeader()
		{
			if (this.header == null)
			{
				this.header = new AddressHeader.ParameterHeader(this);
			}
			return this.header;
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x0016ACB4 File Offset: 0x00168EB4
		[__DynamicallyInvokable]
		public void WriteAddressHeader(XmlWriter writer)
		{
			this.WriteAddressHeader(XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x0016ACC2 File Offset: 0x00168EC2
		[__DynamicallyInvokable]
		public void WriteAddressHeader(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			this.WriteStartAddressHeader(writer);
			this.WriteAddressHeaderContents(writer);
			writer.WriteEndElement();
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x0016ACF0 File Offset: 0x00168EF0
		[__DynamicallyInvokable]
		public void WriteStartAddressHeader(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			this.OnWriteStartAddressHeader(writer);
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x0016AD11 File Offset: 0x00168F11
		[__DynamicallyInvokable]
		public void WriteAddressHeaderContents(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			this.OnWriteAddressHeaderContents(writer);
		}

		// Token: 0x040038CE RID: 14542
		private AddressHeader.ParameterHeader header;

		// Token: 0x02000E3D RID: 3645
		private class ParameterHeader : MessageHeader
		{
			// Token: 0x17001CDE RID: 7390
			// (get) Token: 0x060082AC RID: 33452 RVA: 0x001E3232 File Offset: 0x001E1432
			public override bool IsReferenceParameter
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001CDF RID: 7391
			// (get) Token: 0x060082AD RID: 33453 RVA: 0x001E3235 File Offset: 0x001E1435
			public override string Name
			{
				get
				{
					return this.parameter.Name;
				}
			}

			// Token: 0x17001CE0 RID: 7392
			// (get) Token: 0x060082AE RID: 33454 RVA: 0x001E3242 File Offset: 0x001E1442
			public override string Namespace
			{
				get
				{
					return this.parameter.Namespace;
				}
			}

			// Token: 0x060082AF RID: 33455 RVA: 0x001E324F File Offset: 0x001E144F
			public ParameterHeader(AddressHeader parameter)
			{
				this.parameter = parameter;
			}

			// Token: 0x060082B0 RID: 33456 RVA: 0x001E325E File Offset: 0x001E145E
			protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				if (messageVersion == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("messageVersion"));
				}
				AddressHeader.ParameterHeader.WriteStartHeader(writer, this.parameter, messageVersion.Addressing);
			}

			// Token: 0x060082B1 RID: 33457 RVA: 0x001E328A File Offset: 0x001E148A
			protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				AddressHeader.ParameterHeader.WriteHeaderContents(writer, this.parameter);
			}

			// Token: 0x060082B2 RID: 33458 RVA: 0x001E3298 File Offset: 0x001E1498
			internal static void WriteStartHeader(XmlDictionaryWriter writer, AddressHeader parameter, AddressingVersion addressingVersion)
			{
				parameter.WriteStartAddressHeader(writer);
				if (addressingVersion == AddressingVersion.WSAddressing10)
				{
					writer.WriteAttributeString(XD.AddressingDictionary.IsReferenceParameter, XD.Addressing10Dictionary.Namespace, "true");
				}
			}

			// Token: 0x060082B3 RID: 33459 RVA: 0x001E32C8 File Offset: 0x001E14C8
			internal static void WriteHeaderContents(XmlDictionaryWriter writer, AddressHeader parameter)
			{
				parameter.WriteAddressHeaderContents(writer);
			}

			// Token: 0x04004A31 RID: 18993
			private AddressHeader parameter;
		}

		// Token: 0x02000E3E RID: 3646
		private class XmlObjectSerializerAddressHeader : AddressHeader
		{
			// Token: 0x060082B4 RID: 33460 RVA: 0x001E32D4 File Offset: 0x001E14D4
			public XmlObjectSerializerAddressHeader(object objectToSerialize, XmlObjectSerializer serializer)
			{
				this.serializer = serializer;
				this.objectToSerialize = objectToSerialize;
				Type type = (objectToSerialize == null) ? typeof(object) : objectToSerialize.GetType();
				XmlQualifiedName rootElementName = new XsdDataContractExporter().GetRootElementName(type);
				this.name = rootElementName.Name;
				this.ns = rootElementName.Namespace;
			}

			// Token: 0x060082B5 RID: 33461 RVA: 0x001E3330 File Offset: 0x001E1530
			public XmlObjectSerializerAddressHeader(string name, string ns, object objectToSerialize, XmlObjectSerializer serializer)
			{
				if (name == null || name.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
				}
				this.serializer = serializer;
				this.objectToSerialize = objectToSerialize;
				this.name = name;
				this.ns = ns;
			}

			// Token: 0x17001CE1 RID: 7393
			// (get) Token: 0x060082B6 RID: 33462 RVA: 0x001E3380 File Offset: 0x001E1580
			public override string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17001CE2 RID: 7394
			// (get) Token: 0x060082B7 RID: 33463 RVA: 0x001E3388 File Offset: 0x001E1588
			public override string Namespace
			{
				get
				{
					return this.ns;
				}
			}

			// Token: 0x17001CE3 RID: 7395
			// (get) Token: 0x060082B8 RID: 33464 RVA: 0x001E3390 File Offset: 0x001E1590
			private object ThisLock
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060082B9 RID: 33465 RVA: 0x001E3394 File Offset: 0x001E1594
			protected override void OnWriteAddressHeaderContents(XmlDictionaryWriter writer)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.serializer.WriteObjectContent(writer, this.objectToSerialize);
				}
			}

			// Token: 0x04004A32 RID: 18994
			private XmlObjectSerializer serializer;

			// Token: 0x04004A33 RID: 18995
			private object objectToSerialize;

			// Token: 0x04004A34 RID: 18996
			private string name;

			// Token: 0x04004A35 RID: 18997
			private string ns;
		}

		// Token: 0x02000E3F RID: 3647
		private class DictionaryAddressHeader : AddressHeader.XmlObjectSerializerAddressHeader
		{
			// Token: 0x060082BA RID: 33466 RVA: 0x001E33E0 File Offset: 0x001E15E0
			public DictionaryAddressHeader(XmlDictionaryString name, XmlDictionaryString ns, object value) : base(name.Value, ns.Value, value, DataContractSerializerDefaults.CreateSerializer(AddressHeader.GetObjectType(value), name, ns, int.MaxValue))
			{
				this.name = name;
				this.ns = ns;
			}

			// Token: 0x060082BB RID: 33467 RVA: 0x001E3415 File Offset: 0x001E1615
			protected override void OnWriteStartAddressHeader(XmlDictionaryWriter writer)
			{
				writer.WriteStartElement(this.name, this.ns);
			}

			// Token: 0x04004A36 RID: 18998
			private XmlDictionaryString name;

			// Token: 0x04004A37 RID: 18999
			private XmlDictionaryString ns;
		}
	}
}
