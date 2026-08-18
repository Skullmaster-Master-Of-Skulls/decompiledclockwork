using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200006F RID: 111
	internal class XmlWrappingReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x0001452F File Offset: 0x0001352F
		internal XmlWrappingReader(XmlReader baseReader)
		{
			this.Reader = baseReader;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001453E File Offset: 0x0001353E
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.reader.Settings;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0001454B File Offset: 0x0001354B
		public override XmlNodeType NodeType
		{
			get
			{
				return this.reader.NodeType;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00014558 File Offset: 0x00013558
		public override string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00014565 File Offset: 0x00013565
		public override string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00014572 File Offset: 0x00013572
		public override string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0001457F File Offset: 0x0001357F
		public override string Prefix
		{
			get
			{
				return this.reader.Prefix;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0001458C File Offset: 0x0001358C
		public override bool HasValue
		{
			get
			{
				return this.reader.HasValue;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00014599 File Offset: 0x00013599
		public override string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x000145A6 File Offset: 0x000135A6
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x000145B3 File Offset: 0x000135B3
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x000145C0 File Offset: 0x000135C0
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x000145CD File Offset: 0x000135CD
		public override bool IsDefault
		{
			get
			{
				return this.reader.IsDefault;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x000145DA File Offset: 0x000135DA
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000145E7 File Offset: 0x000135E7
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000145F4 File Offset: 0x000135F4
		public override string XmlLang
		{
			get
			{
				return this.reader.XmlLang;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00014601 File Offset: 0x00013601
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.reader.SchemaInfo;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0001460E File Offset: 0x0001360E
		public override Type ValueType
		{
			get
			{
				return this.reader.ValueType;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001461B File Offset: 0x0001361B
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00014628 File Offset: 0x00013628
		public override bool CanResolveEntity
		{
			get
			{
				return this.reader.CanResolveEntity;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00014635 File Offset: 0x00013635
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00014642 File Offset: 0x00013642
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0001464F File Offset: 0x0001364F
		public override bool HasAttributes
		{
			get
			{
				return this.reader.HasAttributes;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0001465C File Offset: 0x0001365C
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00014669 File Offset: 0x00013669
		public override string GetAttribute(string name)
		{
			return this.reader.GetAttribute(name);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00014677 File Offset: 0x00013677
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.reader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00014686 File Offset: 0x00013686
		public override string GetAttribute(int i)
		{
			return this.reader.GetAttribute(i);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00014694 File Offset: 0x00013694
		public override bool MoveToAttribute(string name)
		{
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000146A2 File Offset: 0x000136A2
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000146B1 File Offset: 0x000136B1
		public override void MoveToAttribute(int i)
		{
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000146BF File Offset: 0x000136BF
		public override bool MoveToFirstAttribute()
		{
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000146CC File Offset: 0x000136CC
		public override bool MoveToNextAttribute()
		{
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000146D9 File Offset: 0x000136D9
		public override bool MoveToElement()
		{
			return this.reader.MoveToElement();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000146E6 File Offset: 0x000136E6
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000146F3 File Offset: 0x000136F3
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00014700 File Offset: 0x00013700
		public override void Skip()
		{
			this.reader.Skip();
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001470D File Offset: 0x0001370D
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001471B File Offset: 0x0001371B
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00014728 File Offset: 0x00013728
		public override bool ReadAttributeValue()
		{
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00014735 File Offset: 0x00013735
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.reader).Dispose();
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00014742 File Offset: 0x00013742
		public virtual bool HasLineInfo()
		{
			return this.readerAsIXmlLineInfo != null && this.readerAsIXmlLineInfo.HasLineInfo();
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00014759 File Offset: 0x00013759
		public virtual int LineNumber
		{
			get
			{
				if (this.readerAsIXmlLineInfo != null)
				{
					return this.readerAsIXmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00014770 File Offset: 0x00013770
		public virtual int LinePosition
		{
			get
			{
				if (this.readerAsIXmlLineInfo != null)
				{
					return this.readerAsIXmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00014787 File Offset: 0x00013787
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0001478F File Offset: 0x0001378F
		protected XmlReader Reader
		{
			get
			{
				return this.reader;
			}
			set
			{
				this.reader = value;
				this.readerAsIXmlLineInfo = (value as IXmlLineInfo);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x000147A4 File Offset: 0x000137A4
		internal virtual SchemaInfo DtdSchemaInfo
		{
			get
			{
				return XmlReader.GetDtdSchemaInfo(this.reader);
			}
		}

		// Token: 0x040005E6 RID: 1510
		protected XmlReader reader;

		// Token: 0x040005E7 RID: 1511
		protected IXmlLineInfo readerAsIXmlLineInfo;
	}
}
