using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Xsl.Runtime;

namespace System.Xml
{
	// Token: 0x020000AC RID: 172
	public sealed class XmlWriterSettings
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x0002BF87 File Offset: 0x0002AF87
		public XmlWriterSettings()
		{
			this.Reset();
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0002BFA0 File Offset: 0x0002AFA0
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x0002BFA8 File Offset: 0x0002AFA8
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.CheckReadOnly("Encoding");
				this.encoding = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0002BFBC File Offset: 0x0002AFBC
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0002BFC4 File Offset: 0x0002AFC4
		public bool OmitXmlDeclaration
		{
			get
			{
				return this.omitXmlDecl;
			}
			set
			{
				this.CheckReadOnly("OmitXmlDeclaration");
				this.omitXmlDecl = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0002BFD8 File Offset: 0x0002AFD8
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0002BFE0 File Offset: 0x0002AFE0
		public NewLineHandling NewLineHandling
		{
			get
			{
				return this.newLineHandling;
			}
			set
			{
				this.CheckReadOnly("NewLineHandling");
				if (value > NewLineHandling.None)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.newLineHandling = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0002C003 File Offset: 0x0002B003
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0002C00B File Offset: 0x0002B00B
		public string NewLineChars
		{
			get
			{
				return this.newLineChars;
			}
			set
			{
				this.CheckReadOnly("NewLineChars");
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.newLineChars = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0002C02D File Offset: 0x0002B02D
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x0002C038 File Offset: 0x0002B038
		public bool Indent
		{
			get
			{
				return this.indent == TriState.True;
			}
			set
			{
				this.CheckReadOnly("Indent");
				this.indent = (value ? TriState.True : TriState.False);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0002C052 File Offset: 0x0002B052
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x0002C05A File Offset: 0x0002B05A
		public string IndentChars
		{
			get
			{
				return this.indentChars;
			}
			set
			{
				this.CheckReadOnly("IndentChars");
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.indentChars = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0002C07C File Offset: 0x0002B07C
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x0002C084 File Offset: 0x0002B084
		public bool NewLineOnAttributes
		{
			get
			{
				return this.newLineOnAttributes;
			}
			set
			{
				this.CheckReadOnly("NewLineOnAttributes");
				this.newLineOnAttributes = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0002C098 File Offset: 0x0002B098
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x0002C0A0 File Offset: 0x0002B0A0
		public bool CloseOutput
		{
			get
			{
				return this.closeOutput;
			}
			set
			{
				this.CheckReadOnly("CloseOutput");
				this.closeOutput = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0002C0B4 File Offset: 0x0002B0B4
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x0002C0BC File Offset: 0x0002B0BC
		public ConformanceLevel ConformanceLevel
		{
			get
			{
				return this.conformanceLevel;
			}
			set
			{
				this.CheckReadOnly("ConformanceLevel");
				if (value > ConformanceLevel.Document)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.conformanceLevel = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0002C0DF File Offset: 0x0002B0DF
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x0002C0E7 File Offset: 0x0002B0E7
		public bool CheckCharacters
		{
			get
			{
				return this.checkCharacters;
			}
			set
			{
				this.CheckReadOnly("CheckCharacters");
				this.checkCharacters = value;
			}
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0002C0FC File Offset: 0x0002B0FC
		public void Reset()
		{
			this.encoding = Encoding.UTF8;
			this.omitXmlDecl = false;
			this.newLineHandling = NewLineHandling.Replace;
			this.newLineChars = "\r\n";
			this.indent = TriState.Unknown;
			this.indentChars = "  ";
			this.newLineOnAttributes = false;
			this.closeOutput = false;
			this.conformanceLevel = ConformanceLevel.Document;
			this.checkCharacters = true;
			this.outputMethod = XmlOutputMethod.Xml;
			this.cdataSections.Clear();
			this.mergeCDataSections = false;
			this.mediaType = null;
			this.docTypeSystem = null;
			this.docTypePublic = null;
			this.standalone = XmlStandalone.Omit;
			this.isReadOnly = false;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002C198 File Offset: 0x0002B198
		public XmlWriterSettings Clone()
		{
			XmlWriterSettings xmlWriterSettings = base.MemberwiseClone() as XmlWriterSettings;
			xmlWriterSettings.cdataSections = new List<XmlQualifiedName>(this.cdataSections);
			xmlWriterSettings.isReadOnly = false;
			return xmlWriterSettings;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0002C1CA File Offset: 0x0002B1CA
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0002C1D2 File Offset: 0x0002B1D2
		internal bool ReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0002C1DB File Offset: 0x0002B1DB
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0002C1E3 File Offset: 0x0002B1E3
		public XmlOutputMethod OutputMethod
		{
			get
			{
				return this.outputMethod;
			}
			internal set
			{
				this.outputMethod = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0002C1EC File Offset: 0x0002B1EC
		internal List<XmlQualifiedName> CDataSectionElements
		{
			get
			{
				return this.cdataSections;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0002C1F4 File Offset: 0x0002B1F4
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0002C1FC File Offset: 0x0002B1FC
		public bool DoNotEscapeUriAttributes
		{
			get
			{
				return this.doNotEscapeUriAttributes;
			}
			set
			{
				this.CheckReadOnly("DoNotEscapeUriAttributes");
				this.doNotEscapeUriAttributes = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0002C210 File Offset: 0x0002B210
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x0002C218 File Offset: 0x0002B218
		internal bool MergeCDataSections
		{
			get
			{
				return this.mergeCDataSections;
			}
			set
			{
				this.CheckReadOnly("MergeCDataSections");
				this.mergeCDataSections = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0002C22C File Offset: 0x0002B22C
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0002C234 File Offset: 0x0002B234
		internal string MediaType
		{
			get
			{
				return this.mediaType;
			}
			set
			{
				this.CheckReadOnly("MediaType");
				this.mediaType = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0002C248 File Offset: 0x0002B248
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0002C250 File Offset: 0x0002B250
		internal string DocTypeSystem
		{
			get
			{
				return this.docTypeSystem;
			}
			set
			{
				this.CheckReadOnly("DocTypeSystem");
				this.docTypeSystem = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0002C264 File Offset: 0x0002B264
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0002C26C File Offset: 0x0002B26C
		internal string DocTypePublic
		{
			get
			{
				return this.docTypePublic;
			}
			set
			{
				this.CheckReadOnly("DocTypePublic");
				this.docTypePublic = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0002C280 File Offset: 0x0002B280
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0002C288 File Offset: 0x0002B288
		internal XmlStandalone Standalone
		{
			get
			{
				return this.standalone;
			}
			set
			{
				this.CheckReadOnly("Standalone");
				this.standalone = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0002C29C File Offset: 0x0002B29C
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x0002C2A4 File Offset: 0x0002B2A4
		internal bool AutoXmlDeclaration
		{
			get
			{
				return this.autoXmlDecl;
			}
			set
			{
				this.CheckReadOnly("AutoXmlDeclaration");
				this.autoXmlDecl = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0002C2B8 File Offset: 0x0002B2B8
		internal TriState InternalIndent
		{
			get
			{
				return this.indent;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0002C2C0 File Offset: 0x0002B2C0
		internal bool IsQuerySpecific
		{
			get
			{
				return this.cdataSections.Count != 0 || this.docTypePublic != null || this.docTypeSystem != null || this.standalone == XmlStandalone.Yes;
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0002C2EA File Offset: 0x0002B2EA
		private void CheckReadOnly(string propertyName)
		{
			if (this.isReadOnly)
			{
				throw new XmlException("Xml_ReadOnlyProperty", "XmlWriterSettings." + propertyName);
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002C30C File Offset: 0x0002B30C
		internal void GetObjectData(XmlQueryDataWriter writer)
		{
			writer.Write(this.encoding.CodePage);
			writer.Write(this.omitXmlDecl);
			writer.Write((sbyte)this.newLineHandling);
			writer.WriteStringQ(this.newLineChars);
			writer.Write((sbyte)this.indent);
			writer.WriteStringQ(this.indentChars);
			writer.Write(this.newLineOnAttributes);
			writer.Write(this.closeOutput);
			writer.Write((sbyte)this.conformanceLevel);
			writer.Write(this.checkCharacters);
			writer.Write((sbyte)this.outputMethod);
			writer.Write(this.cdataSections.Count);
			foreach (XmlQualifiedName xmlQualifiedName in this.cdataSections)
			{
				writer.Write(xmlQualifiedName.Name);
				writer.Write(xmlQualifiedName.Namespace);
			}
			writer.Write(this.mergeCDataSections);
			writer.WriteStringQ(this.mediaType);
			writer.WriteStringQ(this.docTypeSystem);
			writer.WriteStringQ(this.docTypePublic);
			writer.Write((sbyte)this.standalone);
			writer.Write(this.autoXmlDecl);
			writer.Write(this.isReadOnly);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002C464 File Offset: 0x0002B464
		internal XmlWriterSettings(XmlQueryDataReader reader)
		{
			this.encoding = Encoding.GetEncoding(reader.ReadInt32());
			this.omitXmlDecl = reader.ReadBoolean();
			this.newLineHandling = (NewLineHandling)reader.ReadSByte(0, 2);
			this.newLineChars = reader.ReadStringQ();
			this.indent = (TriState)reader.ReadSByte(-1, 1);
			this.indentChars = reader.ReadStringQ();
			this.newLineOnAttributes = reader.ReadBoolean();
			this.closeOutput = reader.ReadBoolean();
			this.conformanceLevel = (ConformanceLevel)reader.ReadSByte(0, 2);
			this.checkCharacters = reader.ReadBoolean();
			this.outputMethod = (XmlOutputMethod)reader.ReadSByte(0, 3);
			int num = reader.ReadInt32();
			this.cdataSections = new List<XmlQualifiedName>(num);
			for (int i = 0; i < num; i++)
			{
				this.cdataSections.Add(new XmlQualifiedName(reader.ReadString(), reader.ReadString()));
			}
			this.mergeCDataSections = reader.ReadBoolean();
			this.mediaType = reader.ReadStringQ();
			this.docTypeSystem = reader.ReadStringQ();
			this.docTypePublic = reader.ReadStringQ();
			this.Standalone = (XmlStandalone)reader.ReadSByte(0, 2);
			this.autoXmlDecl = reader.ReadBoolean();
			this.isReadOnly = reader.ReadBoolean();
		}

		// Token: 0x0400083C RID: 2108
		private Encoding encoding;

		// Token: 0x0400083D RID: 2109
		private bool omitXmlDecl;

		// Token: 0x0400083E RID: 2110
		private NewLineHandling newLineHandling;

		// Token: 0x0400083F RID: 2111
		private string newLineChars;

		// Token: 0x04000840 RID: 2112
		private TriState indent;

		// Token: 0x04000841 RID: 2113
		private string indentChars;

		// Token: 0x04000842 RID: 2114
		private bool newLineOnAttributes;

		// Token: 0x04000843 RID: 2115
		private bool closeOutput;

		// Token: 0x04000844 RID: 2116
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000845 RID: 2117
		private bool checkCharacters;

		// Token: 0x04000846 RID: 2118
		private XmlOutputMethod outputMethod;

		// Token: 0x04000847 RID: 2119
		private List<XmlQualifiedName> cdataSections = new List<XmlQualifiedName>();

		// Token: 0x04000848 RID: 2120
		private bool doNotEscapeUriAttributes;

		// Token: 0x04000849 RID: 2121
		private bool mergeCDataSections;

		// Token: 0x0400084A RID: 2122
		private string mediaType;

		// Token: 0x0400084B RID: 2123
		private string docTypeSystem;

		// Token: 0x0400084C RID: 2124
		private string docTypePublic;

		// Token: 0x0400084D RID: 2125
		private XmlStandalone standalone;

		// Token: 0x0400084E RID: 2126
		private bool autoXmlDecl;

		// Token: 0x0400084F RID: 2127
		private bool isReadOnly;
	}
}
