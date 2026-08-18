using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;
using MS.Internal.Xml.Linq.ComponentModel;

namespace System.Xml.Linq
{
	// Token: 0x0200001F RID: 31
	[XmlSchemaProvider(null, IsAny = true)]
	[TypeDescriptionProvider(typeof(XTypeDescriptionProvider<XElement>))]
	[__DynamicallyInvokable]
	public class XElement : XContainer, IXmlSerializable
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006808 File Offset: 0x00004A08
		[__DynamicallyInvokable]
		public static IEnumerable<XElement> EmptySequence
		{
			[__DynamicallyInvokable]
			get
			{
				if (XElement.emptySequence == null)
				{
					XElement.emptySequence = new XElement[0];
				}
				return XElement.emptySequence;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006821 File Offset: 0x00004A21
		[__DynamicallyInvokable]
		public XElement(XName name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006844 File Offset: 0x00004A44
		[__DynamicallyInvokable]
		public XElement(XName name, object content) : this(name)
		{
			base.AddContentSkipNotify(content);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006854 File Offset: 0x00004A54
		[__DynamicallyInvokable]
		public XElement(XName name, params object[] content) : this(name, content)
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006860 File Offset: 0x00004A60
		[__DynamicallyInvokable]
		public XElement(XElement other) : base(other)
		{
			this.name = other.name;
			XAttribute next = other.lastAttr;
			if (next != null)
			{
				do
				{
					next = next.next;
					this.AppendAttributeSkipNotify(new XAttribute(next));
				}
				while (next != other.lastAttr);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000068A6 File Offset: 0x00004AA6
		[__DynamicallyInvokable]
		public XElement(XStreamingElement other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.name = other.name;
			base.AddContentSkipNotify(other.content);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000068D4 File Offset: 0x00004AD4
		internal XElement() : this("default")
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000068E6 File Offset: 0x00004AE6
		internal XElement(XmlReader r) : this(r, LoadOptions.None)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000068F0 File Offset: 0x00004AF0
		internal XElement(XmlReader r, LoadOptions o)
		{
			this.ReadElementFrom(r, o);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006900 File Offset: 0x00004B00
		[__DynamicallyInvokable]
		public XAttribute FirstAttribute
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.lastAttr == null)
				{
					return null;
				}
				return this.lastAttr.next;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006917 File Offset: 0x00004B17
		[__DynamicallyInvokable]
		public bool HasAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.lastAttr != null;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006924 File Offset: 0x00004B24
		[__DynamicallyInvokable]
		public bool HasElements
		{
			[__DynamicallyInvokable]
			get
			{
				XNode xnode = this.content as XNode;
				if (xnode != null)
				{
					while (!(xnode is XElement))
					{
						xnode = xnode.next;
						if (xnode == this.content)
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000695B File Offset: 0x00004B5B
		[__DynamicallyInvokable]
		public bool IsEmpty
		{
			[__DynamicallyInvokable]
			get
			{
				return this.content == null;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006966 File Offset: 0x00004B66
		[__DynamicallyInvokable]
		public XAttribute LastAttribute
		{
			[__DynamicallyInvokable]
			get
			{
				return this.lastAttr;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000696E File Offset: 0x00004B6E
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00006978 File Offset: 0x00004B78
		[__DynamicallyInvokable]
		public XName Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Name);
				this.name = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Name);
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000069BD File Offset: 0x00004BBD
		[__DynamicallyInvokable]
		public override XmlNodeType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return XmlNodeType.Element;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000069C0 File Offset: 0x00004BC0
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000069FF File Offset: 0x00004BFF
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.content == null)
				{
					return string.Empty;
				}
				string text = this.content as string;
				if (text != null)
				{
					return text;
				}
				StringBuilder stringBuilder = new StringBuilder();
				this.AppendText(stringBuilder);
				return stringBuilder.ToString();
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.RemoveNodes();
				base.Add(value);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006A1C File Offset: 0x00004C1C
		[__DynamicallyInvokable]
		public IEnumerable<XElement> AncestorsAndSelf()
		{
			return base.GetAncestors(null, true);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006A26 File Offset: 0x00004C26
		[__DynamicallyInvokable]
		public IEnumerable<XElement> AncestorsAndSelf(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return base.GetAncestors(name, true);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006A40 File Offset: 0x00004C40
		[__DynamicallyInvokable]
		public XAttribute Attribute(XName name)
		{
			XAttribute next = this.lastAttr;
			if (next != null)
			{
				for (;;)
				{
					next = next.next;
					if (next.name == name)
					{
						break;
					}
					if (next == this.lastAttr)
					{
						goto IL_2A;
					}
				}
				return next;
			}
			IL_2A:
			return null;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006A78 File Offset: 0x00004C78
		[__DynamicallyInvokable]
		public IEnumerable<XAttribute> Attributes()
		{
			return this.GetAttributes(null);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006A81 File Offset: 0x00004C81
		[__DynamicallyInvokable]
		public IEnumerable<XAttribute> Attributes(XName name)
		{
			if (!(name != null))
			{
				return XAttribute.EmptySequence;
			}
			return this.GetAttributes(name);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006A99 File Offset: 0x00004C99
		[__DynamicallyInvokable]
		public IEnumerable<XNode> DescendantNodesAndSelf()
		{
			return base.GetDescendantNodes(true);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006AA2 File Offset: 0x00004CA2
		[__DynamicallyInvokable]
		public IEnumerable<XElement> DescendantsAndSelf()
		{
			return base.GetDescendants(null, true);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006AAC File Offset: 0x00004CAC
		[__DynamicallyInvokable]
		public IEnumerable<XElement> DescendantsAndSelf(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return base.GetDescendants(name, true);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006AC8 File Offset: 0x00004CC8
		[__DynamicallyInvokable]
		public XNamespace GetDefaultNamespace()
		{
			string namespaceOfPrefixInScope = this.GetNamespaceOfPrefixInScope("xmlns", null);
			if (namespaceOfPrefixInScope == null)
			{
				return XNamespace.None;
			}
			return XNamespace.Get(namespaceOfPrefixInScope);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006AF4 File Offset: 0x00004CF4
		[__DynamicallyInvokable]
		public XNamespace GetNamespaceOfPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (prefix.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Argument_InvalidPrefix", new object[]
				{
					prefix
				}));
			}
			if (prefix == "xmlns")
			{
				return XNamespace.Xmlns;
			}
			string namespaceOfPrefixInScope = this.GetNamespaceOfPrefixInScope(prefix, null);
			if (namespaceOfPrefixInScope != null)
			{
				return XNamespace.Get(namespaceOfPrefixInScope);
			}
			if (prefix == "xml")
			{
				return XNamespace.Xml;
			}
			return null;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006B6C File Offset: 0x00004D6C
		[__DynamicallyInvokable]
		public string GetPrefixOfNamespace(XNamespace ns)
		{
			if (ns == null)
			{
				throw new ArgumentNullException("ns");
			}
			string namespaceName = ns.NamespaceName;
			bool flag = false;
			XElement xelement = this;
			XAttribute next;
			for (;;)
			{
				next = xelement.lastAttr;
				if (next != null)
				{
					bool flag2 = false;
					do
					{
						next = next.next;
						if (next.IsNamespaceDeclaration)
						{
							if (next.Value == namespaceName && next.Name.NamespaceName.Length != 0 && (!flag || this.GetNamespaceOfPrefixInScope(next.Name.LocalName, xelement) == null))
							{
								goto IL_72;
							}
							flag2 = true;
						}
					}
					while (next != xelement.lastAttr);
					flag = (flag || flag2);
				}
				xelement = (xelement.parent as XElement);
				if (xelement == null)
				{
					goto Block_8;
				}
			}
			IL_72:
			return next.Name.LocalName;
			Block_8:
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				if (!flag || this.GetNamespaceOfPrefixInScope("xml", null) == null)
				{
					return "xml";
				}
			}
			else if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006C45 File Offset: 0x00004E45
		[__DynamicallyInvokable]
		public static XElement Load(string uri)
		{
			return XElement.Load(uri, LoadOptions.None);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006C50 File Offset: 0x00004E50
		[__DynamicallyInvokable]
		public static XElement Load(string uri, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			XElement result;
			using (XmlReader xmlReader = XmlReader.Create(uri, xmlReaderSettings))
			{
				result = XElement.Load(xmlReader, options);
			}
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006C94 File Offset: 0x00004E94
		[__DynamicallyInvokable]
		public static XElement Load(Stream stream)
		{
			return XElement.Load(stream, LoadOptions.None);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006CA0 File Offset: 0x00004EA0
		[__DynamicallyInvokable]
		public static XElement Load(Stream stream, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			XElement result;
			using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
			{
				result = XElement.Load(xmlReader, options);
			}
			return result;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006CE4 File Offset: 0x00004EE4
		[__DynamicallyInvokable]
		public static XElement Load(TextReader textReader)
		{
			return XElement.Load(textReader, LoadOptions.None);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006CF0 File Offset: 0x00004EF0
		[__DynamicallyInvokable]
		public static XElement Load(TextReader textReader, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			XElement result;
			using (XmlReader xmlReader = XmlReader.Create(textReader, xmlReaderSettings))
			{
				result = XElement.Load(xmlReader, options);
			}
			return result;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006D34 File Offset: 0x00004F34
		[__DynamicallyInvokable]
		public static XElement Load(XmlReader reader)
		{
			return XElement.Load(reader, LoadOptions.None);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006D40 File Offset: 0x00004F40
		[__DynamicallyInvokable]
		public static XElement Load(XmlReader reader, LoadOptions options)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.MoveToContent() != XmlNodeType.Element)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExpectedNodeType", new object[]
				{
					XmlNodeType.Element,
					reader.NodeType
				}));
			}
			XElement result = new XElement(reader, options);
			reader.MoveToContent();
			if (!reader.EOF)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExpectedEndOfFile"));
			}
			return result;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006DB9 File Offset: 0x00004FB9
		[__DynamicallyInvokable]
		public static XElement Parse(string text)
		{
			return XElement.Parse(text, LoadOptions.None);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006DC4 File Offset: 0x00004FC4
		[__DynamicallyInvokable]
		public static XElement Parse(string text, LoadOptions options)
		{
			XElement result;
			using (StringReader stringReader = new StringReader(text))
			{
				XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
				using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
				{
					result = XElement.Load(xmlReader, options);
				}
			}
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006E24 File Offset: 0x00005024
		[__DynamicallyInvokable]
		public void RemoveAll()
		{
			this.RemoveAttributes();
			base.RemoveNodes();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006E34 File Offset: 0x00005034
		[__DynamicallyInvokable]
		public void RemoveAttributes()
		{
			if (base.SkipNotify())
			{
				this.RemoveAttributesSkipNotify();
				return;
			}
			while (this.lastAttr != null)
			{
				XAttribute next = this.lastAttr.next;
				base.NotifyChanging(next, XObjectChangeEventArgs.Remove);
				if (this.lastAttr == null || next != this.lastAttr.next)
				{
					throw new InvalidOperationException(Res.GetString("InvalidOperation_ExternalCode"));
				}
				if (next != this.lastAttr)
				{
					this.lastAttr.next = next.next;
				}
				else
				{
					this.lastAttr = null;
				}
				next.parent = null;
				next.next = null;
				base.NotifyChanged(next, XObjectChangeEventArgs.Remove);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006EDB File Offset: 0x000050DB
		[__DynamicallyInvokable]
		public void ReplaceAll(object content)
		{
			content = XContainer.GetContentSnapshot(content);
			this.RemoveAll();
			base.Add(content);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006EF2 File Offset: 0x000050F2
		[__DynamicallyInvokable]
		public void ReplaceAll(params object[] content)
		{
			this.ReplaceAll(content);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006EFB File Offset: 0x000050FB
		[__DynamicallyInvokable]
		public void ReplaceAttributes(object content)
		{
			content = XContainer.GetContentSnapshot(content);
			this.RemoveAttributes();
			base.Add(content);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006F12 File Offset: 0x00005112
		[__DynamicallyInvokable]
		public void ReplaceAttributes(params object[] content)
		{
			this.ReplaceAttributes(content);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006F1B File Offset: 0x0000511B
		public void Save(string fileName)
		{
			this.Save(fileName, base.GetSaveOptionsFromAnnotations());
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006F2C File Offset: 0x0000512C
		public void Save(string fileName, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using (XmlWriter xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006F6C File Offset: 0x0000516C
		[__DynamicallyInvokable]
		public void Save(Stream stream)
		{
			this.Save(stream, base.GetSaveOptionsFromAnnotations());
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006F7C File Offset: 0x0000517C
		[__DynamicallyInvokable]
		public void Save(Stream stream, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006FBC File Offset: 0x000051BC
		[__DynamicallyInvokable]
		public void Save(TextWriter textWriter)
		{
			this.Save(textWriter, base.GetSaveOptionsFromAnnotations());
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006FCC File Offset: 0x000051CC
		[__DynamicallyInvokable]
		public void Save(TextWriter textWriter, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000700C File Offset: 0x0000520C
		[__DynamicallyInvokable]
		public void Save(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteStartDocument();
			this.WriteTo(writer);
			writer.WriteEndDocument();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007030 File Offset: 0x00005230
		[__DynamicallyInvokable]
		public void SetAttributeValue(XName name, object value)
		{
			XAttribute xattribute = this.Attribute(name);
			if (value == null)
			{
				if (xattribute != null)
				{
					this.RemoveAttribute(xattribute);
					return;
				}
			}
			else
			{
				if (xattribute != null)
				{
					xattribute.Value = XContainer.GetStringValue(value);
					return;
				}
				this.AppendAttribute(new XAttribute(name, value));
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007070 File Offset: 0x00005270
		[__DynamicallyInvokable]
		public void SetElementValue(XName name, object value)
		{
			XElement xelement = base.Element(name);
			if (value == null)
			{
				if (xelement != null)
				{
					base.RemoveNode(xelement);
					return;
				}
			}
			else
			{
				if (xelement != null)
				{
					xelement.Value = XContainer.GetStringValue(value);
					return;
				}
				base.AddNode(new XElement(name, XContainer.GetStringValue(value)));
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000070B5 File Offset: 0x000052B5
		[__DynamicallyInvokable]
		public void SetValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Value = XContainer.GetStringValue(value);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000070D4 File Offset: 0x000052D4
		[__DynamicallyInvokable]
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			new ElementWriter(writer).WriteElement(this);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000070FE File Offset: 0x000052FE
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator string(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return element.Value;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000710B File Offset: 0x0000530B
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator bool(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToBoolean(element.Value.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007130 File Offset: 0x00005330
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator bool?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new bool?(XmlConvert.ToBoolean(element.Value.ToLower(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007164 File Offset: 0x00005364
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator int(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToInt32(element.Value);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007180 File Offset: 0x00005380
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator int?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new int?(XmlConvert.ToInt32(element.Value));
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000071AA File Offset: 0x000053AA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator uint(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToUInt32(element.Value);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000071C8 File Offset: 0x000053C8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator uint?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new uint?(XmlConvert.ToUInt32(element.Value));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000071F2 File Offset: 0x000053F2
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator long(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToInt64(element.Value);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007210 File Offset: 0x00005410
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator long?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new long?(XmlConvert.ToInt64(element.Value));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000723A File Offset: 0x0000543A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator ulong(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToUInt64(element.Value);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007258 File Offset: 0x00005458
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator ulong?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new ulong?(XmlConvert.ToUInt64(element.Value));
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007282 File Offset: 0x00005482
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator float(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToSingle(element.Value);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000072A0 File Offset: 0x000054A0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator float?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new float?(XmlConvert.ToSingle(element.Value));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000072CA File Offset: 0x000054CA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator double(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToDouble(element.Value);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000072E8 File Offset: 0x000054E8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator double?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new double?(XmlConvert.ToDouble(element.Value));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007312 File Offset: 0x00005512
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator decimal(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToDecimal(element.Value);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007330 File Offset: 0x00005530
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator decimal?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new decimal?(XmlConvert.ToDecimal(element.Value));
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000735A File Offset: 0x0000555A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator DateTime(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return DateTime.Parse(element.Value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007380 File Offset: 0x00005580
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator DateTime?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new DateTime?(DateTime.Parse(element.Value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind));
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000073B4 File Offset: 0x000055B4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator DateTimeOffset(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToDateTimeOffset(element.Value);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000073D0 File Offset: 0x000055D0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator DateTimeOffset?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new DateTimeOffset?(XmlConvert.ToDateTimeOffset(element.Value));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000073FA File Offset: 0x000055FA
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator TimeSpan(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToTimeSpan(element.Value);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007418 File Offset: 0x00005618
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator TimeSpan?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new TimeSpan?(XmlConvert.ToTimeSpan(element.Value));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007442 File Offset: 0x00005642
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator Guid(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToGuid(element.Value);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007460 File Offset: 0x00005660
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator Guid?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new Guid?(XmlConvert.ToGuid(element.Value));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000748A File Offset: 0x0000568A
		[__DynamicallyInvokable]
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007490 File Offset: 0x00005690
		[__DynamicallyInvokable]
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this.parent != null || this.annotations != null || this.content != null || this.lastAttr != null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_DeserializeInstance"));
			}
			if (reader.MoveToContent() != XmlNodeType.Element)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExpectedNodeType", new object[]
				{
					XmlNodeType.Element,
					reader.NodeType
				}));
			}
			this.ReadElementFrom(reader, LoadOptions.None);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007519 File Offset: 0x00005719
		[__DynamicallyInvokable]
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteTo(writer);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007522 File Offset: 0x00005722
		internal override void AddAttribute(XAttribute a)
		{
			if (this.Attribute(a.Name) != null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_DuplicateAttribute"));
			}
			if (a.parent != null)
			{
				a = new XAttribute(a);
			}
			this.AppendAttribute(a);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007559 File Offset: 0x00005759
		internal override void AddAttributeSkipNotify(XAttribute a)
		{
			if (this.Attribute(a.Name) != null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_DuplicateAttribute"));
			}
			if (a.parent != null)
			{
				a = new XAttribute(a);
			}
			this.AppendAttributeSkipNotify(a);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007590 File Offset: 0x00005790
		internal void AppendAttribute(XAttribute a)
		{
			bool flag = base.NotifyChanging(a, XObjectChangeEventArgs.Add);
			if (a.parent != null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExternalCode"));
			}
			this.AppendAttributeSkipNotify(a);
			if (flag)
			{
				base.NotifyChanged(a, XObjectChangeEventArgs.Add);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000075D9 File Offset: 0x000057D9
		internal void AppendAttributeSkipNotify(XAttribute a)
		{
			a.parent = this;
			if (this.lastAttr == null)
			{
				a.next = a;
			}
			else
			{
				a.next = this.lastAttr.next;
				this.lastAttr.next = a;
			}
			this.lastAttr = a;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007618 File Offset: 0x00005818
		private bool AttributesEqual(XElement e)
		{
			XAttribute next = this.lastAttr;
			XAttribute next2 = e.lastAttr;
			if (next != null && next2 != null)
			{
				for (;;)
				{
					next = next.next;
					next2 = next2.next;
					if (next.name != next2.name || next.value != next2.value)
					{
						break;
					}
					if (next == this.lastAttr)
					{
						goto Block_3;
					}
				}
				return false;
				Block_3:
				return next2 == e.lastAttr;
			}
			return next == null && next2 == null;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000768B File Offset: 0x0000588B
		internal override XNode CloneNode()
		{
			return new XElement(this);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007694 File Offset: 0x00005894
		internal override bool DeepEquals(XNode node)
		{
			XElement xelement = node as XElement;
			return xelement != null && this.name == xelement.name && base.ContentsEqual(xelement) && this.AttributesEqual(xelement);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000076D0 File Offset: 0x000058D0
		private IEnumerable<XAttribute> GetAttributes(XName name)
		{
			XAttribute a = this.lastAttr;
			if (a != null)
			{
				do
				{
					a = a.next;
					if (name == null || a.name == name)
					{
						yield return a;
					}
				}
				while (a.parent == this && a != this.lastAttr);
			}
			yield break;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000076E8 File Offset: 0x000058E8
		private string GetNamespaceOfPrefixInScope(string prefix, XElement outOfScope)
		{
			for (XElement xelement = this; xelement != outOfScope; xelement = (xelement.parent as XElement))
			{
				XAttribute next = xelement.lastAttr;
				if (next != null)
				{
					for (;;)
					{
						next = next.next;
						if (next.IsNamespaceDeclaration && next.Name.LocalName == prefix)
						{
							break;
						}
						if (next == xelement.lastAttr)
						{
							goto IL_40;
						}
					}
					return next.Value;
				}
				IL_40:;
			}
			return null;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007748 File Offset: 0x00005948
		internal override int GetDeepHashCode()
		{
			int num = this.name.GetHashCode();
			num ^= base.ContentsHashCode();
			XAttribute next = this.lastAttr;
			if (next != null)
			{
				do
				{
					next = next.next;
					num ^= next.GetDeepHashCode();
				}
				while (next != this.lastAttr);
			}
			return num;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007790 File Offset: 0x00005990
		private void ReadElementFrom(XmlReader r, LoadOptions o)
		{
			if (r.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExpectedInteractive"));
			}
			this.name = XNamespace.Get(r.NamespaceURI).GetName(r.LocalName);
			if ((o & LoadOptions.SetBaseUri) != LoadOptions.None)
			{
				string baseURI = r.BaseURI;
				if (baseURI != null && baseURI.Length != 0)
				{
					base.SetBaseUri(baseURI);
				}
			}
			IXmlLineInfo xmlLineInfo = null;
			if ((o & LoadOptions.SetLineInfo) != LoadOptions.None)
			{
				xmlLineInfo = (r as IXmlLineInfo);
				if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
				{
					base.SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
				}
			}
			if (r.MoveToFirstAttribute())
			{
				do
				{
					XAttribute xattribute = new XAttribute(XNamespace.Get((r.Prefix.Length == 0) ? string.Empty : r.NamespaceURI).GetName(r.LocalName), r.Value);
					if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
					{
						xattribute.SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
					}
					this.AppendAttributeSkipNotify(xattribute);
				}
				while (r.MoveToNextAttribute());
				r.MoveToElement();
			}
			if (!r.IsEmptyElement)
			{
				r.Read();
				base.ReadContentFrom(r, o);
			}
			r.Read();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000078AC File Offset: 0x00005AAC
		internal void RemoveAttribute(XAttribute a)
		{
			bool flag = base.NotifyChanging(a, XObjectChangeEventArgs.Remove);
			if (a.parent != this)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExternalCode"));
			}
			XAttribute xattribute = this.lastAttr;
			XAttribute next;
			while ((next = xattribute.next) != a)
			{
				xattribute = next;
			}
			if (xattribute == a)
			{
				this.lastAttr = null;
			}
			else
			{
				if (this.lastAttr == a)
				{
					this.lastAttr = xattribute;
				}
				xattribute.next = a.next;
			}
			a.parent = null;
			a.next = null;
			if (flag)
			{
				base.NotifyChanged(a, XObjectChangeEventArgs.Remove);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000793C File Offset: 0x00005B3C
		private void RemoveAttributesSkipNotify()
		{
			if (this.lastAttr != null)
			{
				XAttribute xattribute = this.lastAttr;
				do
				{
					XAttribute next = xattribute.next;
					xattribute.parent = null;
					xattribute.next = null;
					xattribute = next;
				}
				while (xattribute != this.lastAttr);
				this.lastAttr = null;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000797F File Offset: 0x00005B7F
		internal void SetEndElementLineInfo(int lineNumber, int linePosition)
		{
			base.AddAnnotation(new LineInfoEndElementAnnotation(lineNumber, linePosition));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007990 File Offset: 0x00005B90
		internal override void ValidateNode(XNode node, XNode previous)
		{
			if (node is XDocument)
			{
				throw new ArgumentException(Res.GetString("Argument_AddNode", new object[]
				{
					XmlNodeType.Document
				}));
			}
			if (node is XDocumentType)
			{
				throw new ArgumentException(Res.GetString("Argument_AddNode", new object[]
				{
					XmlNodeType.DocumentType
				}));
			}
		}

		// Token: 0x0400008D RID: 141
		private static IEnumerable<XElement> emptySequence;

		// Token: 0x0400008E RID: 142
		internal XName name;

		// Token: 0x0400008F RID: 143
		internal XAttribute lastAttr;
	}
}
