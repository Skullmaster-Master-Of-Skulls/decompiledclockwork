using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000D3 RID: 211
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	[__DynamicallyInvokable]
	public abstract class XmlReader : IDisposable
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x000208A1 File Offset: 0x0001EAA1
		[__DynamicallyInvokable]
		public virtual XmlReaderSettings Settings
		{
			[__DynamicallyInvokable]
			get
			{
				return null;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600095D RID: 2397
		[__DynamicallyInvokable]
		public abstract XmlNodeType NodeType { [__DynamicallyInvokable] get; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x000208A4 File Offset: 0x0001EAA4
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.Prefix.Length == 0)
				{
					return this.LocalName;
				}
				return this.NameTable.Add(this.Prefix + ":" + this.LocalName);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600095F RID: 2399
		[__DynamicallyInvokable]
		public abstract string LocalName { [__DynamicallyInvokable] get; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000960 RID: 2400
		[__DynamicallyInvokable]
		public abstract string NamespaceURI { [__DynamicallyInvokable] get; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000961 RID: 2401
		[__DynamicallyInvokable]
		public abstract string Prefix { [__DynamicallyInvokable] get; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x000208DB File Offset: 0x0001EADB
		[__DynamicallyInvokable]
		public virtual bool HasValue
		{
			[__DynamicallyInvokable]
			get
			{
				return XmlReader.HasValueInternal(this.NodeType);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000963 RID: 2403
		[__DynamicallyInvokable]
		public abstract string Value { [__DynamicallyInvokable] get; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000964 RID: 2404
		[__DynamicallyInvokable]
		public abstract int Depth { [__DynamicallyInvokable] get; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000965 RID: 2405
		[__DynamicallyInvokable]
		public abstract string BaseURI { [__DynamicallyInvokable] get; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000966 RID: 2406
		[__DynamicallyInvokable]
		public abstract bool IsEmptyElement { [__DynamicallyInvokable] get; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x000208E8 File Offset: 0x0001EAE8
		[__DynamicallyInvokable]
		public virtual bool IsDefault
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x000208EB File Offset: 0x0001EAEB
		public virtual char QuoteChar
		{
			get
			{
				return '"';
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x000208EF File Offset: 0x0001EAEF
		[__DynamicallyInvokable]
		public virtual XmlSpace XmlSpace
		{
			[__DynamicallyInvokable]
			get
			{
				return XmlSpace.None;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x000208F2 File Offset: 0x0001EAF2
		[__DynamicallyInvokable]
		public virtual string XmlLang
		{
			[__DynamicallyInvokable]
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x000208F9 File Offset: 0x0001EAF9
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this as IXmlSchemaInfo;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00020901 File Offset: 0x0001EB01
		[__DynamicallyInvokable]
		public virtual Type ValueType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002090D File Offset: 0x0001EB0D
		[__DynamicallyInvokable]
		public virtual object ReadContentAsObject()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsObject");
			}
			return this.InternalReadContentAsString();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0002092C File Offset: 0x0001EB2C
		[__DynamicallyInvokable]
		public virtual bool ReadContentAsBoolean()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsBoolean");
			}
			bool result;
			try
			{
				result = XmlConvert.ToBoolean(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Boolean", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00020988 File Offset: 0x0001EB88
		public virtual DateTime ReadContentAsDateTime()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsDateTime");
			}
			DateTime result;
			try
			{
				result = XmlConvert.ToDateTime(this.InternalReadContentAsString(), XmlDateTimeSerializationMode.RoundtripKind);
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "DateTime", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000209E4 File Offset: 0x0001EBE4
		[__DynamicallyInvokable]
		public virtual DateTimeOffset ReadContentAsDateTimeOffset()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsDateTimeOffset");
			}
			DateTimeOffset result;
			try
			{
				result = XmlConvert.ToDateTimeOffset(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "DateTimeOffset", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00020A40 File Offset: 0x0001EC40
		[__DynamicallyInvokable]
		public virtual double ReadContentAsDouble()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsDouble");
			}
			double result;
			try
			{
				result = XmlConvert.ToDouble(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Double", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00020A9C File Offset: 0x0001EC9C
		[__DynamicallyInvokable]
		public virtual float ReadContentAsFloat()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsFloat");
			}
			float result;
			try
			{
				result = XmlConvert.ToSingle(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Float", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00020AF8 File Offset: 0x0001ECF8
		[__DynamicallyInvokable]
		public virtual decimal ReadContentAsDecimal()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsDecimal");
			}
			decimal result;
			try
			{
				result = XmlConvert.ToDecimal(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Decimal", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00020B54 File Offset: 0x0001ED54
		[__DynamicallyInvokable]
		public virtual int ReadContentAsInt()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsInt");
			}
			int result;
			try
			{
				result = XmlConvert.ToInt32(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Int", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00020BB0 File Offset: 0x0001EDB0
		[__DynamicallyInvokable]
		public virtual long ReadContentAsLong()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsLong");
			}
			long result;
			try
			{
				result = XmlConvert.ToInt64(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", "Long", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00020C0C File Offset: 0x0001EE0C
		[__DynamicallyInvokable]
		public virtual string ReadContentAsString()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsString");
			}
			return this.InternalReadContentAsString();
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00020C28 File Offset: 0x0001EE28
		[__DynamicallyInvokable]
		public virtual object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAs");
			}
			string text = this.InternalReadContentAsString();
			if (returnType == typeof(string))
			{
				return text;
			}
			object result;
			try
			{
				result = XmlUntypedConverter.Untyped.ChangeType(text, returnType, (namespaceResolver == null) ? (this as IXmlNamespaceResolver) : namespaceResolver);
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", returnType.ToString(), innerException, this as IXmlLineInfo);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Xml_ReadContentAsFormatException", returnType.ToString(), innerException2, this as IXmlLineInfo);
			}
			return result;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00020CD0 File Offset: 0x0001EED0
		[__DynamicallyInvokable]
		public virtual object ReadElementContentAsObject()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsObject"))
			{
				object result = this.ReadContentAsObject();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return string.Empty;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00020CFE File Offset: 0x0001EEFE
		[__DynamicallyInvokable]
		public virtual object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsObject();
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00020D10 File Offset: 0x0001EF10
		[__DynamicallyInvokable]
		public virtual bool ReadElementContentAsBoolean()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsBoolean"))
			{
				bool result = this.ReadContentAsBoolean();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToBoolean(string.Empty);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00020D43 File Offset: 0x0001EF43
		[__DynamicallyInvokable]
		public virtual bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsBoolean();
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00020D54 File Offset: 0x0001EF54
		public virtual DateTime ReadElementContentAsDateTime()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsDateTime"))
			{
				DateTime result = this.ReadContentAsDateTime();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToDateTime(string.Empty, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00020D88 File Offset: 0x0001EF88
		public virtual DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsDateTime();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00020D98 File Offset: 0x0001EF98
		[__DynamicallyInvokable]
		public virtual double ReadElementContentAsDouble()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsDouble"))
			{
				double result = this.ReadContentAsDouble();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToDouble(string.Empty);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00020DCB File Offset: 0x0001EFCB
		[__DynamicallyInvokable]
		public virtual double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsDouble();
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00020DDC File Offset: 0x0001EFDC
		[__DynamicallyInvokable]
		public virtual float ReadElementContentAsFloat()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsFloat"))
			{
				float result = this.ReadContentAsFloat();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToSingle(string.Empty);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00020E0F File Offset: 0x0001F00F
		[__DynamicallyInvokable]
		public virtual float ReadElementContentAsFloat(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsFloat();
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00020E20 File Offset: 0x0001F020
		[__DynamicallyInvokable]
		public virtual decimal ReadElementContentAsDecimal()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsDecimal"))
			{
				decimal result = this.ReadContentAsDecimal();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToDecimal(string.Empty);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00020E53 File Offset: 0x0001F053
		[__DynamicallyInvokable]
		public virtual decimal ReadElementContentAsDecimal(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsDecimal();
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00020E64 File Offset: 0x0001F064
		[__DynamicallyInvokable]
		public virtual int ReadElementContentAsInt()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsInt"))
			{
				int result = this.ReadContentAsInt();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToInt32(string.Empty);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00020E97 File Offset: 0x0001F097
		[__DynamicallyInvokable]
		public virtual int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsInt();
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00020EA8 File Offset: 0x0001F0A8
		[__DynamicallyInvokable]
		public virtual long ReadElementContentAsLong()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsLong"))
			{
				long result = this.ReadContentAsLong();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToInt64(string.Empty);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00020EDB File Offset: 0x0001F0DB
		[__DynamicallyInvokable]
		public virtual long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsLong();
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00020EEC File Offset: 0x0001F0EC
		[__DynamicallyInvokable]
		public virtual string ReadElementContentAsString()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsString"))
			{
				string result = this.ReadContentAsString();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return string.Empty;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00020F1A File Offset: 0x0001F11A
		[__DynamicallyInvokable]
		public virtual string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsString();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00020F2C File Offset: 0x0001F12C
		[__DynamicallyInvokable]
		public virtual object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAs"))
			{
				object result = this.ReadContentAs(returnType, namespaceResolver);
				this.FinishReadElementContentAsXxx();
				return result;
			}
			if (!(returnType == typeof(string)))
			{
				return XmlUntypedConverter.Untyped.ChangeType(string.Empty, returnType, namespaceResolver);
			}
			return string.Empty;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00020F80 File Offset: 0x0001F180
		[__DynamicallyInvokable]
		public virtual object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600098C RID: 2444
		[__DynamicallyInvokable]
		public abstract int AttributeCount { [__DynamicallyInvokable] get; }

		// Token: 0x0600098D RID: 2445
		[__DynamicallyInvokable]
		public abstract string GetAttribute(string name);

		// Token: 0x0600098E RID: 2446
		[__DynamicallyInvokable]
		public abstract string GetAttribute(string name, string namespaceURI);

		// Token: 0x0600098F RID: 2447
		[__DynamicallyInvokable]
		public abstract string GetAttribute(int i);

		// Token: 0x170001B4 RID: 436
		[__DynamicallyInvokable]
		public virtual string this[int i]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetAttribute(i);
			}
		}

		// Token: 0x170001B5 RID: 437
		[__DynamicallyInvokable]
		public virtual string this[string name]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x170001B6 RID: 438
		[__DynamicallyInvokable]
		public virtual string this[string name, string namespaceURI]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetAttribute(name, namespaceURI);
			}
		}

		// Token: 0x06000993 RID: 2451
		[__DynamicallyInvokable]
		public abstract bool MoveToAttribute(string name);

		// Token: 0x06000994 RID: 2452
		[__DynamicallyInvokable]
		public abstract bool MoveToAttribute(string name, string ns);

		// Token: 0x06000995 RID: 2453 RVA: 0x00020FB0 File Offset: 0x0001F1B0
		[__DynamicallyInvokable]
		public virtual void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.AttributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.MoveToElement();
			this.MoveToFirstAttribute();
			for (int j = 0; j < i; j++)
			{
				this.MoveToNextAttribute();
			}
		}

		// Token: 0x06000996 RID: 2454
		[__DynamicallyInvokable]
		public abstract bool MoveToFirstAttribute();

		// Token: 0x06000997 RID: 2455
		[__DynamicallyInvokable]
		public abstract bool MoveToNextAttribute();

		// Token: 0x06000998 RID: 2456
		[__DynamicallyInvokable]
		public abstract bool MoveToElement();

		// Token: 0x06000999 RID: 2457
		[__DynamicallyInvokable]
		public abstract bool ReadAttributeValue();

		// Token: 0x0600099A RID: 2458
		[__DynamicallyInvokable]
		public abstract bool Read();

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600099B RID: 2459
		[__DynamicallyInvokable]
		public abstract bool EOF { [__DynamicallyInvokable] get; }

		// Token: 0x0600099C RID: 2460 RVA: 0x00020FF6 File Offset: 0x0001F1F6
		public virtual void Close()
		{
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600099D RID: 2461
		[__DynamicallyInvokable]
		public abstract ReadState ReadState { [__DynamicallyInvokable] get; }

		// Token: 0x0600099E RID: 2462 RVA: 0x00020FF8 File Offset: 0x0001F1F8
		[__DynamicallyInvokable]
		public virtual void Skip()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return;
			}
			this.SkipSubtree();
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600099F RID: 2463
		[__DynamicallyInvokable]
		public abstract XmlNameTable NameTable { [__DynamicallyInvokable] get; }

		// Token: 0x060009A0 RID: 2464
		[__DynamicallyInvokable]
		public abstract string LookupNamespace(string prefix);

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0002100B File Offset: 0x0001F20B
		[__DynamicallyInvokable]
		public virtual bool CanResolveEntity
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x060009A2 RID: 2466
		[__DynamicallyInvokable]
		public abstract void ResolveEntity();

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0002100E File Offset: 0x0001F20E
		[__DynamicallyInvokable]
		public virtual bool CanReadBinaryContent
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00021011 File Offset: 0x0001F211
		[__DynamicallyInvokable]
		public virtual int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("Xml_ReadBinaryContentNotSupported", new object[]
			{
				"ReadContentAsBase64"
			}));
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00021030 File Offset: 0x0001F230
		[__DynamicallyInvokable]
		public virtual int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("Xml_ReadBinaryContentNotSupported", new object[]
			{
				"ReadElementContentAsBase64"
			}));
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0002104F File Offset: 0x0001F24F
		[__DynamicallyInvokable]
		public virtual int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("Xml_ReadBinaryContentNotSupported", new object[]
			{
				"ReadContentAsBinHex"
			}));
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002106E File Offset: 0x0001F26E
		[__DynamicallyInvokable]
		public virtual int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("Xml_ReadBinaryContentNotSupported", new object[]
			{
				"ReadElementContentAsBinHex"
			}));
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0002108D File Offset: 0x0001F28D
		[__DynamicallyInvokable]
		public virtual bool CanReadValueChunk
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00021090 File Offset: 0x0001F290
		[__DynamicallyInvokable]
		public virtual int ReadValueChunk(char[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("Xml_ReadValueChunkNotSupported"));
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000210A4 File Offset: 0x0001F2A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadString()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			this.MoveToElement();
			if (this.NodeType == XmlNodeType.Element)
			{
				if (this.IsEmptyElement)
				{
					return string.Empty;
				}
				if (!this.Read())
				{
					throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
				}
				if (this.NodeType == XmlNodeType.EndElement)
				{
					return string.Empty;
				}
			}
			string text = string.Empty;
			while (XmlReader.IsTextualNode(this.NodeType))
			{
				text += this.Value;
				if (!this.Read())
				{
					break;
				}
			}
			return text;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00021134 File Offset: 0x0001F334
		[__DynamicallyInvokable]
		public virtual XmlNodeType MoveToContent()
		{
			for (;;)
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.EntityReference:
					goto IL_33;
				case XmlNodeType.Attribute:
					goto IL_2C;
				default:
					if (nodeType - XmlNodeType.EndElement <= 1)
					{
						goto IL_33;
					}
					if (!this.Read())
					{
						goto Block_2;
					}
					break;
				}
			}
			IL_2C:
			this.MoveToElement();
			IL_33:
			return this.NodeType;
			Block_2:
			return this.NodeType;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002118C File Offset: 0x0001F38C
		[__DynamicallyInvokable]
		public virtual void ReadStartElement()
		{
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			this.Read();
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000211D0 File Offset: 0x0001F3D0
		[__DynamicallyInvokable]
		public virtual void ReadStartElement(string name)
		{
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.Name == name)
			{
				this.Read();
				return;
			}
			throw new XmlException("Xml_ElementNotFound", name, this as IXmlLineInfo);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00021234 File Offset: 0x0001F434
		[__DynamicallyInvokable]
		public virtual void ReadStartElement(string localname, string ns)
		{
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.LocalName == localname && this.NamespaceURI == ns)
			{
				this.Read();
				return;
			}
			throw new XmlException("Xml_ElementNotFoundNs", new string[]
			{
				localname,
				ns
			}, this as IXmlLineInfo);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000212B4 File Offset: 0x0001F4B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadElementString()
		{
			string result = string.Empty;
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (!this.IsEmptyElement)
			{
				this.Read();
				result = this.ReadString();
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("Xml_UnexpectedNodeInSimpleContent", new string[]
					{
						this.NodeType.ToString(),
						"ReadElementString"
					}, this as IXmlLineInfo);
				}
				this.Read();
			}
			else
			{
				this.Read();
			}
			return result;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002135C File Offset: 0x0001F55C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadElementString(string name)
		{
			string result = string.Empty;
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.Name != name)
			{
				throw new XmlException("Xml_ElementNotFound", name, this as IXmlLineInfo);
			}
			if (!this.IsEmptyElement)
			{
				result = this.ReadString();
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString(), this as IXmlLineInfo);
				}
				this.Read();
			}
			else
			{
				this.Read();
			}
			return result;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002140C File Offset: 0x0001F60C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadElementString(string localname, string ns)
		{
			string result = string.Empty;
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.LocalName != localname || this.NamespaceURI != ns)
			{
				throw new XmlException("Xml_ElementNotFoundNs", new string[]
				{
					localname,
					ns
				}, this as IXmlLineInfo);
			}
			if (!this.IsEmptyElement)
			{
				result = this.ReadString();
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString(), this as IXmlLineInfo);
				}
				this.Read();
			}
			else
			{
				this.Read();
			}
			return result;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000214D8 File Offset: 0x0001F6D8
		[__DynamicallyInvokable]
		public virtual void ReadEndElement()
		{
			if (this.MoveToContent() != XmlNodeType.EndElement)
			{
				throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			this.Read();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002151B File Offset: 0x0001F71B
		[__DynamicallyInvokable]
		public virtual bool IsStartElement()
		{
			return this.MoveToContent() == XmlNodeType.Element;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00021526 File Offset: 0x0001F726
		[__DynamicallyInvokable]
		public virtual bool IsStartElement(string name)
		{
			return this.MoveToContent() == XmlNodeType.Element && this.Name == name;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002153F File Offset: 0x0001F73F
		[__DynamicallyInvokable]
		public virtual bool IsStartElement(string localname, string ns)
		{
			return this.MoveToContent() == XmlNodeType.Element && this.LocalName == localname && this.NamespaceURI == ns;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00021568 File Offset: 0x0001F768
		[__DynamicallyInvokable]
		public virtual bool ReadToFollowing(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(name, "name");
			}
			name = this.NameTable.Add(name);
			while (this.Read())
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(name, this.Name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000215C0 File Offset: 0x0001F7C0
		[__DynamicallyInvokable]
		public virtual bool ReadToFollowing(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			localName = this.NameTable.Add(localName);
			namespaceURI = this.NameTable.Add(namespaceURI);
			while (this.Read())
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(localName, this.LocalName) && Ref.Equal(namespaceURI, this.NamespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00021640 File Offset: 0x0001F840
		[__DynamicallyInvokable]
		public virtual bool ReadToDescendant(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(name, "name");
			}
			int num = this.Depth;
			if (this.NodeType != XmlNodeType.Element)
			{
				if (this.ReadState != ReadState.Initial)
				{
					return false;
				}
				num--;
			}
			else if (this.IsEmptyElement)
			{
				return false;
			}
			name = this.NameTable.Add(name);
			while (this.Read() && this.Depth > num)
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(name, this.Name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000216CC File Offset: 0x0001F8CC
		[__DynamicallyInvokable]
		public virtual bool ReadToDescendant(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			int num = this.Depth;
			if (this.NodeType != XmlNodeType.Element)
			{
				if (this.ReadState != ReadState.Initial)
				{
					return false;
				}
				num--;
			}
			else if (this.IsEmptyElement)
			{
				return false;
			}
			localName = this.NameTable.Add(localName);
			namespaceURI = this.NameTable.Add(namespaceURI);
			while (this.Read() && this.Depth > num)
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(localName, this.LocalName) && Ref.Equal(namespaceURI, this.NamespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00021780 File Offset: 0x0001F980
		[__DynamicallyInvokable]
		public virtual bool ReadToNextSibling(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(name, "name");
			}
			name = this.NameTable.Add(name);
			while (this.SkipSubtree())
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element && Ref.Equal(name, this.Name))
				{
					return true;
				}
				if (nodeType == XmlNodeType.EndElement || this.EOF)
				{
					break;
				}
			}
			return false;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000217E4 File Offset: 0x0001F9E4
		[__DynamicallyInvokable]
		public virtual bool ReadToNextSibling(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			localName = this.NameTable.Add(localName);
			namespaceURI = this.NameTable.Add(namespaceURI);
			while (this.SkipSubtree())
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element && Ref.Equal(localName, this.LocalName) && Ref.Equal(namespaceURI, this.NamespaceURI))
				{
					return true;
				}
				if (nodeType == XmlNodeType.EndElement || this.EOF)
				{
					break;
				}
			}
			return false;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00021871 File Offset: 0x0001FA71
		[__DynamicallyInvokable]
		public static bool IsName(string str)
		{
			if (str == null)
			{
				throw new NullReferenceException();
			}
			return ValidateNames.IsNameNoNamespaces(str);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00021882 File Offset: 0x0001FA82
		[__DynamicallyInvokable]
		public static bool IsNameToken(string str)
		{
			if (str == null)
			{
				throw new NullReferenceException();
			}
			return ValidateNames.IsNmtokenNoNamespaces(str);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00021894 File Offset: 0x0001FA94
		[__DynamicallyInvokable]
		public virtual string ReadInnerXml()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			if (this.NodeType != XmlNodeType.Attribute && this.NodeType != XmlNodeType.Element)
			{
				this.Read();
				return string.Empty;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriter xmlWriter = this.CreateWriterForInnerOuterXml(stringWriter);
			try
			{
				if (this.NodeType == XmlNodeType.Attribute)
				{
					((XmlTextWriter)xmlWriter).QuoteChar = this.QuoteChar;
					this.WriteAttributeValue(xmlWriter);
				}
				if (this.NodeType == XmlNodeType.Element)
				{
					this.WriteNode(xmlWriter, false);
				}
			}
			finally
			{
				xmlWriter.Close();
			}
			return stringWriter.ToString();
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00021934 File Offset: 0x0001FB34
		private void WriteNode(XmlWriter xtw, bool defattr)
		{
			int num = (this.NodeType == XmlNodeType.None) ? -1 : this.Depth;
			while (this.Read() && num < this.Depth)
			{
				switch (this.NodeType)
				{
				case XmlNodeType.Element:
					xtw.WriteStartElement(this.Prefix, this.LocalName, this.NamespaceURI);
					((XmlTextWriter)xtw).QuoteChar = this.QuoteChar;
					xtw.WriteAttributes(this, defattr);
					if (this.IsEmptyElement)
					{
						xtw.WriteEndElement();
					}
					break;
				case XmlNodeType.Text:
					xtw.WriteString(this.Value);
					break;
				case XmlNodeType.CDATA:
					xtw.WriteCData(this.Value);
					break;
				case XmlNodeType.EntityReference:
					xtw.WriteEntityRef(this.Name);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.XmlDeclaration:
					xtw.WriteProcessingInstruction(this.Name, this.Value);
					break;
				case XmlNodeType.Comment:
					xtw.WriteComment(this.Value);
					break;
				case XmlNodeType.DocumentType:
					xtw.WriteDocType(this.Name, this.GetAttribute("PUBLIC"), this.GetAttribute("SYSTEM"), this.Value);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					xtw.WriteWhitespace(this.Value);
					break;
				case XmlNodeType.EndElement:
					xtw.WriteFullEndElement();
					break;
				}
			}
			if (num == this.Depth && this.NodeType == XmlNodeType.EndElement)
			{
				this.Read();
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00021AB0 File Offset: 0x0001FCB0
		private void WriteAttributeValue(XmlWriter xtw)
		{
			string name = this.Name;
			while (this.ReadAttributeValue())
			{
				if (this.NodeType == XmlNodeType.EntityReference)
				{
					xtw.WriteEntityRef(this.Name);
				}
				else
				{
					xtw.WriteString(this.Value);
				}
			}
			this.MoveToAttribute(name);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00021AFC File Offset: 0x0001FCFC
		[__DynamicallyInvokable]
		public virtual string ReadOuterXml()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			if (this.NodeType != XmlNodeType.Attribute && this.NodeType != XmlNodeType.Element)
			{
				this.Read();
				return string.Empty;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriter xmlWriter = this.CreateWriterForInnerOuterXml(stringWriter);
			try
			{
				if (this.NodeType == XmlNodeType.Attribute)
				{
					xmlWriter.WriteStartAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
					this.WriteAttributeValue(xmlWriter);
					xmlWriter.WriteEndAttribute();
				}
				else
				{
					xmlWriter.WriteNode(this, false);
				}
			}
			finally
			{
				xmlWriter.Close();
			}
			return stringWriter.ToString();
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00021BA4 File Offset: 0x0001FDA4
		private XmlWriter CreateWriterForInnerOuterXml(StringWriter sw)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(sw);
			this.SetNamespacesFlag(xmlTextWriter);
			return xmlTextWriter;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00021BC0 File Offset: 0x0001FDC0
		private void SetNamespacesFlag(XmlTextWriter xtw)
		{
			XmlTextReader xmlTextReader = this as XmlTextReader;
			if (xmlTextReader != null)
			{
				xtw.Namespaces = xmlTextReader.Namespaces;
				return;
			}
			XmlValidatingReader xmlValidatingReader = this as XmlValidatingReader;
			if (xmlValidatingReader != null)
			{
				xtw.Namespaces = xmlValidatingReader.Namespaces;
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00021BFA File Offset: 0x0001FDFA
		[__DynamicallyInvokable]
		public virtual XmlReader ReadSubtree()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw new InvalidOperationException(Res.GetString("Xml_ReadSubtreeNotOnElement"));
			}
			return new XmlSubtreeReader(this);
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00021C1B File Offset: 0x0001FE1B
		[__DynamicallyInvokable]
		public virtual bool HasAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.AttributeCount > 0;
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00021C26 File Offset: 0x0001FE26
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00021C2F File Offset: 0x0001FE2F
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.ReadState != ReadState.Closed)
			{
				this.Close();
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00021C43 File Offset: 0x0001FE43
		internal virtual XmlNamespaceManager NamespaceManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00021C46 File Offset: 0x0001FE46
		internal static bool IsTextualNode(XmlNodeType nodeType)
		{
			return ((ulong)XmlReader.IsTextualNodeBitmap & (ulong)(1L << (int)(nodeType & (XmlNodeType)31))) > 0UL;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00021C5A File Offset: 0x0001FE5A
		internal static bool CanReadContentAs(XmlNodeType nodeType)
		{
			return ((ulong)XmlReader.CanReadContentAsBitmap & (ulong)(1L << (int)(nodeType & (XmlNodeType)31))) > 0UL;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00021C6E File Offset: 0x0001FE6E
		internal static bool HasValueInternal(XmlNodeType nodeType)
		{
			return ((ulong)XmlReader.HasValueBitmap & (ulong)(1L << (int)(nodeType & (XmlNodeType)31))) > 0UL;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00021C84 File Offset: 0x0001FE84
		private bool SkipSubtree()
		{
			this.MoveToElement();
			if (this.NodeType == XmlNodeType.Element && !this.IsEmptyElement)
			{
				int depth = this.Depth;
				while (this.Read() && depth < this.Depth)
				{
				}
				return this.NodeType == XmlNodeType.EndElement && this.Read();
			}
			return this.Read();
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00021CDC File Offset: 0x0001FEDC
		internal void CheckElement(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			if (this.NodeType != XmlNodeType.Element)
			{
				throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.LocalName != localName || this.NamespaceURI != namespaceURI)
			{
				throw new XmlException("Xml_ElementNotFoundNs", new string[]
				{
					localName,
					namespaceURI
				}, this as IXmlLineInfo);
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00021D77 File Offset: 0x0001FF77
		internal Exception CreateReadContentAsException(string methodName)
		{
			return XmlReader.CreateReadContentAsException(methodName, this.NodeType, this as IXmlLineInfo);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00021D8B File Offset: 0x0001FF8B
		internal Exception CreateReadElementContentAsException(string methodName)
		{
			return XmlReader.CreateReadElementContentAsException(methodName, this.NodeType, this as IXmlLineInfo);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00021D9F File Offset: 0x0001FF9F
		internal bool CanReadContentAs()
		{
			return XmlReader.CanReadContentAs(this.NodeType);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00021DAC File Offset: 0x0001FFAC
		internal static Exception CreateReadContentAsException(string methodName, XmlNodeType nodeType, IXmlLineInfo lineInfo)
		{
			string name = "Xml_InvalidReadContentAs";
			object[] args = new string[]
			{
				methodName,
				nodeType.ToString()
			};
			return new InvalidOperationException(XmlReader.AddLineInfo(Res.GetString(name, args), lineInfo));
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00021DEC File Offset: 0x0001FFEC
		internal static Exception CreateReadElementContentAsException(string methodName, XmlNodeType nodeType, IXmlLineInfo lineInfo)
		{
			string name = "Xml_InvalidReadElementContentAs";
			object[] args = new string[]
			{
				methodName,
				nodeType.ToString()
			};
			return new InvalidOperationException(XmlReader.AddLineInfo(Res.GetString(name, args), lineInfo));
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00021E2C File Offset: 0x0002002C
		private static string AddLineInfo(string message, IXmlLineInfo lineInfo)
		{
			if (lineInfo != null)
			{
				string[] array = new string[]
				{
					lineInfo.LineNumber.ToString(CultureInfo.InvariantCulture),
					lineInfo.LinePosition.ToString(CultureInfo.InvariantCulture)
				};
				string str = message;
				string str2 = " ";
				string name = "Xml_ErrorPosition";
				object[] args = array;
				message = str + str2 + Res.GetString(name, args);
			}
			return message;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00021E8C File Offset: 0x0002008C
		internal string InternalReadContentAsString()
		{
			string text = string.Empty;
			StringBuilder stringBuilder = null;
			do
			{
				switch (this.NodeType)
				{
				case XmlNodeType.Attribute:
					goto IL_55;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if (text.Length == 0)
					{
						text = this.Value;
						goto IL_9B;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
						stringBuilder.Append(text);
					}
					stringBuilder.Append(this.Value);
					goto IL_9B;
				case XmlNodeType.EntityReference:
					if (this.CanResolveEntity)
					{
						this.ResolveEntity();
						goto IL_9B;
					}
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.EndEntity:
					goto IL_9B;
				}
				break;
				IL_9B:;
			}
			while ((this.AttributeCount != 0) ? this.ReadAttributeValue() : this.Read());
			goto IL_B6;
			IL_55:
			return this.Value;
			IL_B6:
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00021F5C File Offset: 0x0002015C
		private bool SetupReadElementContentAsXxx(string methodName)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw this.CreateReadElementContentAsException(methodName);
			}
			bool isEmptyElement = this.IsEmptyElement;
			this.Read();
			if (isEmptyElement)
			{
				return false;
			}
			XmlNodeType nodeType = this.NodeType;
			if (nodeType == XmlNodeType.EndElement)
			{
				this.Read();
				return false;
			}
			if (nodeType == XmlNodeType.Element)
			{
				throw new XmlException("Xml_MixedReadElementContentAs", string.Empty, this as IXmlLineInfo);
			}
			return true;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00021FC0 File Offset: 0x000201C0
		private void FinishReadElementContentAsXxx()
		{
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString());
			}
			this.Read();
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00022000 File Offset: 0x00020200
		internal bool IsDefaultInternal
		{
			get
			{
				if (this.IsDefault)
				{
					return true;
				}
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				return schemaInfo != null && schemaInfo.IsDefault;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0002202C File Offset: 0x0002022C
		internal virtual IDtdInfo DtdInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00022030 File Offset: 0x00020230
		internal static Encoding GetEncoding(XmlReader reader)
		{
			XmlTextReaderImpl xmlTextReaderImpl = XmlReader.GetXmlTextReaderImpl(reader);
			if (xmlTextReaderImpl == null)
			{
				return null;
			}
			return xmlTextReaderImpl.Encoding;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00022050 File Offset: 0x00020250
		internal static ConformanceLevel GetV1ConformanceLevel(XmlReader reader)
		{
			XmlTextReaderImpl xmlTextReaderImpl = XmlReader.GetXmlTextReaderImpl(reader);
			if (xmlTextReaderImpl == null)
			{
				return ConformanceLevel.Document;
			}
			return xmlTextReaderImpl.V1ComformanceLevel;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00022070 File Offset: 0x00020270
		private static XmlTextReaderImpl GetXmlTextReaderImpl(XmlReader reader)
		{
			XmlTextReaderImpl xmlTextReaderImpl = reader as XmlTextReaderImpl;
			if (xmlTextReaderImpl != null)
			{
				return xmlTextReaderImpl;
			}
			XmlTextReader xmlTextReader = reader as XmlTextReader;
			if (xmlTextReader != null)
			{
				return xmlTextReader.Impl;
			}
			XmlValidatingReaderImpl xmlValidatingReaderImpl = reader as XmlValidatingReaderImpl;
			if (xmlValidatingReaderImpl != null)
			{
				return xmlValidatingReaderImpl.ReaderImpl;
			}
			XmlValidatingReader xmlValidatingReader = reader as XmlValidatingReader;
			if (xmlValidatingReader != null)
			{
				return xmlValidatingReader.Impl.ReaderImpl;
			}
			return null;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000220C2 File Offset: 0x000202C2
		[__DynamicallyInvokable]
		public static XmlReader Create(string inputUri)
		{
			return XmlReader.Create(inputUri, null, null);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000220CC File Offset: 0x000202CC
		[__DynamicallyInvokable]
		public static XmlReader Create(string inputUri, XmlReaderSettings settings)
		{
			return XmlReader.Create(inputUri, settings, null);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000220D6 File Offset: 0x000202D6
		public static XmlReader Create(string inputUri, XmlReaderSettings settings, XmlParserContext inputContext)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(inputUri, inputContext);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000220EA File Offset: 0x000202EA
		[__DynamicallyInvokable]
		public static XmlReader Create(Stream input)
		{
			return XmlReader.Create(input, null, string.Empty);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000220F8 File Offset: 0x000202F8
		[__DynamicallyInvokable]
		public static XmlReader Create(Stream input, XmlReaderSettings settings)
		{
			return XmlReader.Create(input, settings, string.Empty);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00022106 File Offset: 0x00020306
		public static XmlReader Create(Stream input, XmlReaderSettings settings, string baseUri)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(input, null, baseUri, null);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002211C File Offset: 0x0002031C
		[__DynamicallyInvokable]
		public static XmlReader Create(Stream input, XmlReaderSettings settings, XmlParserContext inputContext)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(input, null, string.Empty, inputContext);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00022136 File Offset: 0x00020336
		[__DynamicallyInvokable]
		public static XmlReader Create(TextReader input)
		{
			return XmlReader.Create(input, null, string.Empty);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00022144 File Offset: 0x00020344
		[__DynamicallyInvokable]
		public static XmlReader Create(TextReader input, XmlReaderSettings settings)
		{
			return XmlReader.Create(input, settings, string.Empty);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00022152 File Offset: 0x00020352
		public static XmlReader Create(TextReader input, XmlReaderSettings settings, string baseUri)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(input, baseUri, null);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00022167 File Offset: 0x00020367
		[__DynamicallyInvokable]
		public static XmlReader Create(TextReader input, XmlReaderSettings settings, XmlParserContext inputContext)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(input, string.Empty, inputContext);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00022180 File Offset: 0x00020380
		[__DynamicallyInvokable]
		public static XmlReader Create(XmlReader reader, XmlReaderSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(reader);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00022194 File Offset: 0x00020394
		internal static XmlReader CreateSqlReader(Stream input, XmlReaderSettings settings, XmlParserContext inputContext)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			byte[] array = new byte[XmlReader.CalcBufferSize(input)];
			int num = 0;
			int num2;
			do
			{
				num2 = input.Read(array, num, array.Length - num);
				num += num2;
			}
			while (num2 > 0 && num < 2);
			XmlReader xmlReader;
			if (num >= 2 && array[0] == 223 && array[1] == 255)
			{
				if (inputContext != null)
				{
					throw new ArgumentException(Res.GetString("XmlBinary_NoParserContext"), "inputContext");
				}
				xmlReader = new XmlSqlBinaryReader(input, array, num, string.Empty, settings.CloseInput, settings);
			}
			else
			{
				xmlReader = new XmlTextReaderImpl(input, array, num, settings, null, string.Empty, inputContext, settings.CloseInput);
			}
			if (settings.ValidationType != ValidationType.None)
			{
				xmlReader = settings.AddValidation(xmlReader);
			}
			if (settings.Async)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00022260 File Offset: 0x00020460
		internal static int CalcBufferSize(Stream input)
		{
			int num = 4096;
			if (input.CanSeek)
			{
				long length = input.Length;
				if (length < (long)num)
				{
					num = checked((int)length);
				}
				else if (length > 65536L)
				{
					num = 8192;
				}
			}
			return num;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002229C File Offset: 0x0002049C
		private object debuggerDisplayProxy
		{
			get
			{
				return new XmlReader.XmlReaderDebuggerDisplayProxy(this);
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000222A9 File Offset: 0x000204A9
		[__DynamicallyInvokable]
		public virtual Task<string> GetValueAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000222B0 File Offset: 0x000204B0
		[__DynamicallyInvokable]
		public virtual Task<object> ReadContentAsObjectAsync()
		{
			XmlReader.<ReadContentAsObjectAsync>d__184 <ReadContentAsObjectAsync>d__;
			<ReadContentAsObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadContentAsObjectAsync>d__.<>4__this = this;
			<ReadContentAsObjectAsync>d__.<>1__state = -1;
			<ReadContentAsObjectAsync>d__.<>t__builder.Start<XmlReader.<ReadContentAsObjectAsync>d__184>(ref <ReadContentAsObjectAsync>d__);
			return <ReadContentAsObjectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000222F3 File Offset: 0x000204F3
		[__DynamicallyInvokable]
		public virtual Task<string> ReadContentAsStringAsync()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsString");
			}
			return this.InternalReadContentAsStringAsync();
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00022310 File Offset: 0x00020510
		[__DynamicallyInvokable]
		public virtual Task<object> ReadContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			XmlReader.<ReadContentAsAsync>d__186 <ReadContentAsAsync>d__;
			<ReadContentAsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadContentAsAsync>d__.<>4__this = this;
			<ReadContentAsAsync>d__.returnType = returnType;
			<ReadContentAsAsync>d__.namespaceResolver = namespaceResolver;
			<ReadContentAsAsync>d__.<>1__state = -1;
			<ReadContentAsAsync>d__.<>t__builder.Start<XmlReader.<ReadContentAsAsync>d__186>(ref <ReadContentAsAsync>d__);
			return <ReadContentAsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00022364 File Offset: 0x00020564
		[__DynamicallyInvokable]
		public virtual Task<object> ReadElementContentAsObjectAsync()
		{
			XmlReader.<ReadElementContentAsObjectAsync>d__187 <ReadElementContentAsObjectAsync>d__;
			<ReadElementContentAsObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadElementContentAsObjectAsync>d__.<>4__this = this;
			<ReadElementContentAsObjectAsync>d__.<>1__state = -1;
			<ReadElementContentAsObjectAsync>d__.<>t__builder.Start<XmlReader.<ReadElementContentAsObjectAsync>d__187>(ref <ReadElementContentAsObjectAsync>d__);
			return <ReadElementContentAsObjectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000223A8 File Offset: 0x000205A8
		[__DynamicallyInvokable]
		public virtual Task<string> ReadElementContentAsStringAsync()
		{
			XmlReader.<ReadElementContentAsStringAsync>d__188 <ReadElementContentAsStringAsync>d__;
			<ReadElementContentAsStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadElementContentAsStringAsync>d__.<>4__this = this;
			<ReadElementContentAsStringAsync>d__.<>1__state = -1;
			<ReadElementContentAsStringAsync>d__.<>t__builder.Start<XmlReader.<ReadElementContentAsStringAsync>d__188>(ref <ReadElementContentAsStringAsync>d__);
			return <ReadElementContentAsStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000223EC File Offset: 0x000205EC
		[__DynamicallyInvokable]
		public virtual Task<object> ReadElementContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			XmlReader.<ReadElementContentAsAsync>d__189 <ReadElementContentAsAsync>d__;
			<ReadElementContentAsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadElementContentAsAsync>d__.<>4__this = this;
			<ReadElementContentAsAsync>d__.returnType = returnType;
			<ReadElementContentAsAsync>d__.namespaceResolver = namespaceResolver;
			<ReadElementContentAsAsync>d__.<>1__state = -1;
			<ReadElementContentAsAsync>d__.<>t__builder.Start<XmlReader.<ReadElementContentAsAsync>d__189>(ref <ReadElementContentAsAsync>d__);
			return <ReadElementContentAsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002243F File Offset: 0x0002063F
		[__DynamicallyInvokable]
		public virtual Task<bool> ReadAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00022446 File Offset: 0x00020646
		[__DynamicallyInvokable]
		public virtual Task SkipAsync()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return AsyncHelper.DoneTask;
			}
			return this.SkipSubtreeAsync();
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002245D File Offset: 0x0002065D
		[__DynamicallyInvokable]
		public virtual Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("Xml_ReadBinaryContentNotSupported", new object[]
			{
				"ReadContentAsBase64"
			}));
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002247C File Offset: 0x0002067C
		[__DynamicallyInvokable]
		public virtual Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("Xml_ReadBinaryContentNotSupported", new object[]
			{
				"ReadElementContentAsBase64"
			}));
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002249B File Offset: 0x0002069B
		[__DynamicallyInvokable]
		public virtual Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("Xml_ReadBinaryContentNotSupported", new object[]
			{
				"ReadContentAsBinHex"
			}));
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000224BA File Offset: 0x000206BA
		[__DynamicallyInvokable]
		public virtual Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("Xml_ReadBinaryContentNotSupported", new object[]
			{
				"ReadElementContentAsBinHex"
			}));
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000224D9 File Offset: 0x000206D9
		[__DynamicallyInvokable]
		public virtual Task<int> ReadValueChunkAsync(char[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("Xml_ReadValueChunkNotSupported"));
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000224EC File Offset: 0x000206EC
		[__DynamicallyInvokable]
		public virtual Task<XmlNodeType> MoveToContentAsync()
		{
			XmlReader.<MoveToContentAsync>d__197 <MoveToContentAsync>d__;
			<MoveToContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<XmlNodeType>.Create();
			<MoveToContentAsync>d__.<>4__this = this;
			<MoveToContentAsync>d__.<>1__state = -1;
			<MoveToContentAsync>d__.<>t__builder.Start<XmlReader.<MoveToContentAsync>d__197>(ref <MoveToContentAsync>d__);
			return <MoveToContentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00022530 File Offset: 0x00020730
		[__DynamicallyInvokable]
		public virtual Task<string> ReadInnerXmlAsync()
		{
			XmlReader.<ReadInnerXmlAsync>d__198 <ReadInnerXmlAsync>d__;
			<ReadInnerXmlAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadInnerXmlAsync>d__.<>4__this = this;
			<ReadInnerXmlAsync>d__.<>1__state = -1;
			<ReadInnerXmlAsync>d__.<>t__builder.Start<XmlReader.<ReadInnerXmlAsync>d__198>(ref <ReadInnerXmlAsync>d__);
			return <ReadInnerXmlAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00022574 File Offset: 0x00020774
		private Task WriteNodeAsync(XmlWriter xtw, bool defattr)
		{
			XmlReader.<WriteNodeAsync>d__199 <WriteNodeAsync>d__;
			<WriteNodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNodeAsync>d__.<>4__this = this;
			<WriteNodeAsync>d__.xtw = xtw;
			<WriteNodeAsync>d__.defattr = defattr;
			<WriteNodeAsync>d__.<>1__state = -1;
			<WriteNodeAsync>d__.<>t__builder.Start<XmlReader.<WriteNodeAsync>d__199>(ref <WriteNodeAsync>d__);
			return <WriteNodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000225C8 File Offset: 0x000207C8
		[__DynamicallyInvokable]
		public virtual Task<string> ReadOuterXmlAsync()
		{
			XmlReader.<ReadOuterXmlAsync>d__200 <ReadOuterXmlAsync>d__;
			<ReadOuterXmlAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadOuterXmlAsync>d__.<>4__this = this;
			<ReadOuterXmlAsync>d__.<>1__state = -1;
			<ReadOuterXmlAsync>d__.<>t__builder.Start<XmlReader.<ReadOuterXmlAsync>d__200>(ref <ReadOuterXmlAsync>d__);
			return <ReadOuterXmlAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002260C File Offset: 0x0002080C
		private Task<bool> SkipSubtreeAsync()
		{
			XmlReader.<SkipSubtreeAsync>d__201 <SkipSubtreeAsync>d__;
			<SkipSubtreeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<SkipSubtreeAsync>d__.<>4__this = this;
			<SkipSubtreeAsync>d__.<>1__state = -1;
			<SkipSubtreeAsync>d__.<>t__builder.Start<XmlReader.<SkipSubtreeAsync>d__201>(ref <SkipSubtreeAsync>d__);
			return <SkipSubtreeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00022650 File Offset: 0x00020850
		internal Task<string> InternalReadContentAsStringAsync()
		{
			XmlReader.<InternalReadContentAsStringAsync>d__202 <InternalReadContentAsStringAsync>d__;
			<InternalReadContentAsStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<InternalReadContentAsStringAsync>d__.<>4__this = this;
			<InternalReadContentAsStringAsync>d__.<>1__state = -1;
			<InternalReadContentAsStringAsync>d__.<>t__builder.Start<XmlReader.<InternalReadContentAsStringAsync>d__202>(ref <InternalReadContentAsStringAsync>d__);
			return <InternalReadContentAsStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00022694 File Offset: 0x00020894
		private Task<bool> SetupReadElementContentAsXxxAsync(string methodName)
		{
			XmlReader.<SetupReadElementContentAsXxxAsync>d__203 <SetupReadElementContentAsXxxAsync>d__;
			<SetupReadElementContentAsXxxAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<SetupReadElementContentAsXxxAsync>d__.<>4__this = this;
			<SetupReadElementContentAsXxxAsync>d__.methodName = methodName;
			<SetupReadElementContentAsXxxAsync>d__.<>1__state = -1;
			<SetupReadElementContentAsXxxAsync>d__.<>t__builder.Start<XmlReader.<SetupReadElementContentAsXxxAsync>d__203>(ref <SetupReadElementContentAsXxxAsync>d__);
			return <SetupReadElementContentAsXxxAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000226E0 File Offset: 0x000208E0
		private Task FinishReadElementContentAsXxxAsync()
		{
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("Xml_InvalidNodeType", this.NodeType.ToString());
			}
			return this.ReadAsync();
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002271C File Offset: 0x0002091C
		[__DynamicallyInvokable]
		protected XmlReader()
		{
		}

		// Token: 0x04000334 RID: 820
		private static uint IsTextualNodeBitmap = 24600U;

		// Token: 0x04000335 RID: 821
		private static uint CanReadContentAsBitmap = 123324U;

		// Token: 0x04000336 RID: 822
		private static uint HasValueBitmap = 157084U;

		// Token: 0x04000337 RID: 823
		internal const int DefaultBufferSize = 4096;

		// Token: 0x04000338 RID: 824
		internal const int BiggerBufferSize = 8192;

		// Token: 0x04000339 RID: 825
		internal const int MaxStreamLengthForDefaultBufferSize = 65536;

		// Token: 0x0400033A RID: 826
		internal const int AsyncBufferSize = 65536;

		// Token: 0x0200034F RID: 847
		[DebuggerDisplay("{ToString()}")]
		private struct XmlReaderDebuggerDisplayProxy
		{
			// Token: 0x06002E3A RID: 11834 RVA: 0x000F50EA File Offset: 0x000F32EA
			internal XmlReaderDebuggerDisplayProxy(XmlReader reader)
			{
				this.reader = reader;
			}

			// Token: 0x06002E3B RID: 11835 RVA: 0x000F50F4 File Offset: 0x000F32F4
			public override string ToString()
			{
				XmlNodeType nodeType = this.reader.NodeType;
				string text = nodeType.ToString();
				switch (nodeType)
				{
				case XmlNodeType.Element:
				case XmlNodeType.EntityReference:
				case XmlNodeType.EndElement:
				case XmlNodeType.EndEntity:
					text = text + ", Name=\"" + this.reader.Name + "\"";
					break;
				case XmlNodeType.Attribute:
				case XmlNodeType.ProcessingInstruction:
					text = string.Concat(new string[]
					{
						text,
						", Name=\"",
						this.reader.Name,
						"\", Value=\"",
						XmlConvert.EscapeValueForDebuggerDisplay(this.reader.Value),
						"\""
					});
					break;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
				case XmlNodeType.XmlDeclaration:
					text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.reader.Value) + "\"";
					break;
				case XmlNodeType.DocumentType:
					text = text + ", Name=\"" + this.reader.Name + "'";
					text = text + ", SYSTEM=\"" + this.reader.GetAttribute("SYSTEM") + "\"";
					text = text + ", PUBLIC=\"" + this.reader.GetAttribute("PUBLIC") + "\"";
					text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.reader.Value) + "\"";
					break;
				}
				return text;
			}

			// Token: 0x04001611 RID: 5649
			private XmlReader reader;
		}
	}
}
