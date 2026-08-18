using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200009C RID: 156
	[Obsolete("Use XmlReader created by XmlReader.Create() method using appropriate XmlReaderSettings instead. http://go.microsoft.com/fwlink/?linkid=14202")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class XmlValidatingReader : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x0002791E File Offset: 0x0002691E
		public XmlValidatingReader(XmlReader reader)
		{
			this.impl = new XmlValidatingReaderImpl(reader);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002793E File Offset: 0x0002693E
		public XmlValidatingReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.impl = new XmlValidatingReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00027960 File Offset: 0x00026960
		public XmlValidatingReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.impl = new XmlValidatingReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00027982 File Offset: 0x00026982
		public override XmlReaderSettings Settings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00027985 File Offset: 0x00026985
		public override XmlNodeType NodeType
		{
			get
			{
				return this.impl.NodeType;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00027992 File Offset: 0x00026992
		public override string Name
		{
			get
			{
				return this.impl.Name;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0002799F File Offset: 0x0002699F
		public override string LocalName
		{
			get
			{
				return this.impl.LocalName;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x000279AC File Offset: 0x000269AC
		public override string NamespaceURI
		{
			get
			{
				return this.impl.NamespaceURI;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x000279B9 File Offset: 0x000269B9
		public override string Prefix
		{
			get
			{
				return this.impl.Prefix;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x000279C6 File Offset: 0x000269C6
		public override bool HasValue
		{
			get
			{
				return this.impl.HasValue;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x000279D3 File Offset: 0x000269D3
		public override string Value
		{
			get
			{
				return this.impl.Value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x000279E0 File Offset: 0x000269E0
		public override int Depth
		{
			get
			{
				return this.impl.Depth;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x000279ED File Offset: 0x000269ED
		public override string BaseURI
		{
			get
			{
				return this.impl.BaseURI;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x000279FA File Offset: 0x000269FA
		public override bool IsEmptyElement
		{
			get
			{
				return this.impl.IsEmptyElement;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00027A07 File Offset: 0x00026A07
		public override bool IsDefault
		{
			get
			{
				return this.impl.IsDefault;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00027A14 File Offset: 0x00026A14
		public override char QuoteChar
		{
			get
			{
				return this.impl.QuoteChar;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00027A21 File Offset: 0x00026A21
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.impl.XmlSpace;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00027A2E File Offset: 0x00026A2E
		public override string XmlLang
		{
			get
			{
				return this.impl.XmlLang;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00027A3B File Offset: 0x00026A3B
		public override int AttributeCount
		{
			get
			{
				return this.impl.AttributeCount;
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00027A48 File Offset: 0x00026A48
		public override string GetAttribute(string name)
		{
			return this.impl.GetAttribute(name);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00027A56 File Offset: 0x00026A56
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.impl.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00027A65 File Offset: 0x00026A65
		public override string GetAttribute(int i)
		{
			return this.impl.GetAttribute(i);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00027A73 File Offset: 0x00026A73
		public override bool MoveToAttribute(string name)
		{
			return this.impl.MoveToAttribute(name);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00027A81 File Offset: 0x00026A81
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.impl.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00027A90 File Offset: 0x00026A90
		public override void MoveToAttribute(int i)
		{
			this.impl.MoveToAttribute(i);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00027A9E File Offset: 0x00026A9E
		public override bool MoveToFirstAttribute()
		{
			return this.impl.MoveToFirstAttribute();
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00027AAB File Offset: 0x00026AAB
		public override bool MoveToNextAttribute()
		{
			return this.impl.MoveToNextAttribute();
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00027AB8 File Offset: 0x00026AB8
		public override bool MoveToElement()
		{
			return this.impl.MoveToElement();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00027AC5 File Offset: 0x00026AC5
		public override bool ReadAttributeValue()
		{
			return this.impl.ReadAttributeValue();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00027AD2 File Offset: 0x00026AD2
		public override bool Read()
		{
			return this.impl.Read();
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00027ADF File Offset: 0x00026ADF
		public override bool EOF
		{
			get
			{
				return this.impl.EOF;
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00027AEC File Offset: 0x00026AEC
		public override void Close()
		{
			this.impl.Close();
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00027AF9 File Offset: 0x00026AF9
		public override ReadState ReadState
		{
			get
			{
				return this.impl.ReadState;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00027B06 File Offset: 0x00026B06
		public override XmlNameTable NameTable
		{
			get
			{
				return this.impl.NameTable;
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00027B14 File Offset: 0x00026B14
		public override string LookupNamespace(string prefix)
		{
			string text = this.impl.LookupNamespace(prefix);
			if (text != null && text.Length == 0)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00027B3C File Offset: 0x00026B3C
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00027B3F File Offset: 0x00026B3F
		public override void ResolveEntity()
		{
			this.impl.ResolveEntity();
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00027B4C File Offset: 0x00026B4C
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00027B4F File Offset: 0x00026B4F
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00027B5F File Offset: 0x00026B5F
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00027B6F File Offset: 0x00026B6F
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00027B7F File Offset: 0x00026B7F
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00027B8F File Offset: 0x00026B8F
		public override string ReadString()
		{
			this.impl.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00027BA2 File Offset: 0x00026BA2
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00027BA5 File Offset: 0x00026BA5
		public int LineNumber
		{
			get
			{
				return this.impl.LineNumber;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00027BB2 File Offset: 0x00026BB2
		public int LinePosition
		{
			get
			{
				return this.impl.LinePosition;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00027BBF File Offset: 0x00026BBF
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00027BCD File Offset: 0x00026BCD
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.impl.LookupNamespace(prefix);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00027BDB File Offset: 0x00026BDB
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.impl.LookupPrefix(namespaceName);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060008A0 RID: 2208 RVA: 0x00027BE9 File Offset: 0x00026BE9
		// (remove) Token: 0x060008A1 RID: 2209 RVA: 0x00027BF7 File Offset: 0x00026BF7
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.impl.ValidationEventHandler += value;
			}
			remove
			{
				this.impl.ValidationEventHandler -= value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00027C05 File Offset: 0x00026C05
		public object SchemaType
		{
			get
			{
				return this.impl.SchemaType;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00027C12 File Offset: 0x00026C12
		public XmlReader Reader
		{
			get
			{
				return this.impl.Reader;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00027C1F File Offset: 0x00026C1F
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x00027C2C File Offset: 0x00026C2C
		public ValidationType ValidationType
		{
			get
			{
				return this.impl.ValidationType;
			}
			set
			{
				this.impl.ValidationType = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00027C3A File Offset: 0x00026C3A
		public XmlSchemaCollection Schemas
		{
			get
			{
				return this.impl.Schemas;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00027C47 File Offset: 0x00026C47
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x00027C54 File Offset: 0x00026C54
		public EntityHandling EntityHandling
		{
			get
			{
				return this.impl.EntityHandling;
			}
			set
			{
				this.impl.EntityHandling = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00027C62 File Offset: 0x00026C62
		public XmlResolver XmlResolver
		{
			set
			{
				this.impl.XmlResolver = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00027C70 File Offset: 0x00026C70
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x00027C7D File Offset: 0x00026C7D
		public bool Namespaces
		{
			get
			{
				return this.impl.Namespaces;
			}
			set
			{
				this.impl.Namespaces = value;
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00027C8B File Offset: 0x00026C8B
		public object ReadTypedValue()
		{
			return this.impl.ReadTypedValue();
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00027C98 File Offset: 0x00026C98
		public Encoding Encoding
		{
			get
			{
				return this.impl.Encoding;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00027CA5 File Offset: 0x00026CA5
		internal XmlValidatingReaderImpl Impl
		{
			get
			{
				return this.impl;
			}
		}

		// Token: 0x040007A3 RID: 1955
		private XmlValidatingReaderImpl impl;
	}
}
