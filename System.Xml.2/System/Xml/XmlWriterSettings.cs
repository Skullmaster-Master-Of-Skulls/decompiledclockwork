using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml.Xsl.Runtime;

namespace System.Xml
{
	// Token: 0x020000E9 RID: 233
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public sealed class XmlWriterSettings
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x00040CC3 File Offset: 0x0003EEC3
		[__DynamicallyInvokable]
		public XmlWriterSettings()
		{
			this.Initialize();
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00040CDC File Offset: 0x0003EEDC
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x00040CE4 File Offset: 0x0003EEE4
		[__DynamicallyInvokable]
		public bool Async
		{
			[__DynamicallyInvokable]
			get
			{
				return this.useAsync;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("Async");
				this.useAsync = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00040CF8 File Offset: 0x0003EEF8
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x00040D00 File Offset: 0x0003EF00
		[__DynamicallyInvokable]
		public Encoding Encoding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.encoding;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("Encoding");
				this.encoding = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00040D14 File Offset: 0x0003EF14
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x00040D1C File Offset: 0x0003EF1C
		[__DynamicallyInvokable]
		public bool OmitXmlDeclaration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.omitXmlDecl;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("OmitXmlDeclaration");
				this.omitXmlDecl = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00040D30 File Offset: 0x0003EF30
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x00040D38 File Offset: 0x0003EF38
		[__DynamicallyInvokable]
		public NewLineHandling NewLineHandling
		{
			[__DynamicallyInvokable]
			get
			{
				return this.newLineHandling;
			}
			[__DynamicallyInvokable]
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

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00040D5B File Offset: 0x0003EF5B
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00040D63 File Offset: 0x0003EF63
		[__DynamicallyInvokable]
		public string NewLineChars
		{
			[__DynamicallyInvokable]
			get
			{
				return this.newLineChars;
			}
			[__DynamicallyInvokable]
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

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00040D85 File Offset: 0x0003EF85
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x00040D90 File Offset: 0x0003EF90
		[__DynamicallyInvokable]
		public bool Indent
		{
			[__DynamicallyInvokable]
			get
			{
				return this.indent == TriState.True;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("Indent");
				this.indent = (value ? TriState.True : TriState.False);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00040DAA File Offset: 0x0003EFAA
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x00040DB2 File Offset: 0x0003EFB2
		[__DynamicallyInvokable]
		public string IndentChars
		{
			[__DynamicallyInvokable]
			get
			{
				return this.indentChars;
			}
			[__DynamicallyInvokable]
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

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00040DD4 File Offset: 0x0003EFD4
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x00040DDC File Offset: 0x0003EFDC
		[__DynamicallyInvokable]
		public bool NewLineOnAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.newLineOnAttributes;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("NewLineOnAttributes");
				this.newLineOnAttributes = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x00040DF0 File Offset: 0x0003EFF0
		// (set) Token: 0x06000F83 RID: 3971 RVA: 0x00040DF8 File Offset: 0x0003EFF8
		[__DynamicallyInvokable]
		public bool CloseOutput
		{
			[__DynamicallyInvokable]
			get
			{
				return this.closeOutput;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("CloseOutput");
				this.closeOutput = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00040E0C File Offset: 0x0003F00C
		// (set) Token: 0x06000F85 RID: 3973 RVA: 0x00040E14 File Offset: 0x0003F014
		[__DynamicallyInvokable]
		public ConformanceLevel ConformanceLevel
		{
			[__DynamicallyInvokable]
			get
			{
				return this.conformanceLevel;
			}
			[__DynamicallyInvokable]
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

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00040E37 File Offset: 0x0003F037
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x00040E3F File Offset: 0x0003F03F
		[__DynamicallyInvokable]
		public bool CheckCharacters
		{
			[__DynamicallyInvokable]
			get
			{
				return this.checkCharacters;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("CheckCharacters");
				this.checkCharacters = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00040E53 File Offset: 0x0003F053
		// (set) Token: 0x06000F89 RID: 3977 RVA: 0x00040E5B File Offset: 0x0003F05B
		[__DynamicallyInvokable]
		public NamespaceHandling NamespaceHandling
		{
			[__DynamicallyInvokable]
			get
			{
				return this.namespaceHandling;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("NamespaceHandling");
				if (value > NamespaceHandling.OmitDuplicates)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.namespaceHandling = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00040E7E File Offset: 0x0003F07E
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x00040E86 File Offset: 0x0003F086
		[__DynamicallyInvokable]
		public bool WriteEndDocumentOnClose
		{
			[__DynamicallyInvokable]
			get
			{
				return this.writeEndDocumentOnClose;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("WriteEndDocumentOnClose");
				this.writeEndDocumentOnClose = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00040E9A File Offset: 0x0003F09A
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x00040EA2 File Offset: 0x0003F0A2
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

		// Token: 0x06000F8E RID: 3982 RVA: 0x00040EAB File Offset: 0x0003F0AB
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.CheckReadOnly("Reset");
			this.Initialize();
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00040EC0 File Offset: 0x0003F0C0
		[__DynamicallyInvokable]
		public XmlWriterSettings Clone()
		{
			XmlWriterSettings xmlWriterSettings = base.MemberwiseClone() as XmlWriterSettings;
			xmlWriterSettings.cdataSections = new List<XmlQualifiedName>(this.cdataSections);
			xmlWriterSettings.isReadOnly = false;
			return xmlWriterSettings;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00040EF2 File Offset: 0x0003F0F2
		internal List<XmlQualifiedName> CDataSectionElements
		{
			get
			{
				return this.cdataSections;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00040EFA File Offset: 0x0003F0FA
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x00040F02 File Offset: 0x0003F102
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

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00040F16 File Offset: 0x0003F116
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x00040F1E File Offset: 0x0003F11E
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

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00040F32 File Offset: 0x0003F132
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x00040F3A File Offset: 0x0003F13A
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

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00040F4E File Offset: 0x0003F14E
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x00040F56 File Offset: 0x0003F156
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

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00040F6A File Offset: 0x0003F16A
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x00040F72 File Offset: 0x0003F172
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

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00040F86 File Offset: 0x0003F186
		// (set) Token: 0x06000F9C RID: 3996 RVA: 0x00040F8E File Offset: 0x0003F18E
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

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00040FA2 File Offset: 0x0003F1A2
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x00040FAA File Offset: 0x0003F1AA
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

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00040FBE File Offset: 0x0003F1BE
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x00040FC6 File Offset: 0x0003F1C6
		internal TriState IndentInternal
		{
			get
			{
				return this.indent;
			}
			set
			{
				this.indent = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00040FCF File Offset: 0x0003F1CF
		internal bool IsQuerySpecific
		{
			get
			{
				return this.cdataSections.Count != 0 || this.docTypePublic != null || this.docTypeSystem != null || this.standalone == XmlStandalone.Yes;
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00040FFC File Offset: 0x0003F1FC
		internal XmlWriter CreateWriter(string outputFileName)
		{
			if (outputFileName == null)
			{
				throw new ArgumentNullException("outputFileName");
			}
			XmlWriterSettings xmlWriterSettings = this;
			if (!xmlWriterSettings.CloseOutput)
			{
				xmlWriterSettings = xmlWriterSettings.Clone();
				xmlWriterSettings.CloseOutput = true;
			}
			FileStream fileStream = null;
			XmlWriter result;
			try
			{
				fileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, this.useAsync);
				result = xmlWriterSettings.CreateWriter(fileStream);
			}
			catch
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00041070 File Offset: 0x0003F270
		internal XmlWriter CreateWriter(Stream output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			XmlWriter xmlWriter;
			if (this.Encoding.WebName == "utf-8")
			{
				switch (this.OutputMethod)
				{
				case XmlOutputMethod.Xml:
					if (this.Indent)
					{
						xmlWriter = new XmlUtf8RawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new XmlUtf8RawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Html:
					if (this.Indent)
					{
						xmlWriter = new HtmlUtf8RawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new HtmlUtf8RawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Text:
					xmlWriter = new TextUtf8RawTextWriter(output, this);
					break;
				case XmlOutputMethod.AutoDetect:
					xmlWriter = new XmlAutoDetectWriter(output, this);
					break;
				default:
					return null;
				}
			}
			else
			{
				switch (this.OutputMethod)
				{
				case XmlOutputMethod.Xml:
					if (this.Indent)
					{
						xmlWriter = new XmlEncodedRawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new XmlEncodedRawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Html:
					if (this.Indent)
					{
						xmlWriter = new HtmlEncodedRawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new HtmlEncodedRawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Text:
					xmlWriter = new TextEncodedRawTextWriter(output, this);
					break;
				case XmlOutputMethod.AutoDetect:
					xmlWriter = new XmlAutoDetectWriter(output, this);
					break;
				default:
					return null;
				}
			}
			if (this.OutputMethod != XmlOutputMethod.AutoDetect && this.IsQuerySpecific)
			{
				xmlWriter = new QueryOutputWriter((XmlRawWriter)xmlWriter, this);
			}
			xmlWriter = new XmlWellFormedWriter(xmlWriter, this);
			if (this.useAsync)
			{
				xmlWriter = new XmlAsyncCheckWriter(xmlWriter);
			}
			return xmlWriter;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000411C0 File Offset: 0x0003F3C0
		internal XmlWriter CreateWriter(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			XmlWriter xmlWriter;
			switch (this.OutputMethod)
			{
			case XmlOutputMethod.Xml:
				if (this.Indent)
				{
					xmlWriter = new XmlEncodedRawTextWriterIndent(output, this);
				}
				else
				{
					xmlWriter = new XmlEncodedRawTextWriter(output, this);
				}
				break;
			case XmlOutputMethod.Html:
				if (this.Indent)
				{
					xmlWriter = new HtmlEncodedRawTextWriterIndent(output, this);
				}
				else
				{
					xmlWriter = new HtmlEncodedRawTextWriter(output, this);
				}
				break;
			case XmlOutputMethod.Text:
				xmlWriter = new TextEncodedRawTextWriter(output, this);
				break;
			case XmlOutputMethod.AutoDetect:
				xmlWriter = new XmlAutoDetectWriter(output, this);
				break;
			default:
				return null;
			}
			if (this.OutputMethod != XmlOutputMethod.AutoDetect && this.IsQuerySpecific)
			{
				xmlWriter = new QueryOutputWriter((XmlRawWriter)xmlWriter, this);
			}
			xmlWriter = new XmlWellFormedWriter(xmlWriter, this);
			if (this.useAsync)
			{
				xmlWriter = new XmlAsyncCheckWriter(xmlWriter);
			}
			return xmlWriter;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0004127E File Offset: 0x0003F47E
		internal XmlWriter CreateWriter(XmlWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			return this.AddConformanceWrapper(output);
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00041295 File Offset: 0x0003F495
		// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x0004129D File Offset: 0x0003F49D
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

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000412A6 File Offset: 0x0003F4A6
		private void CheckReadOnly(string propertyName)
		{
			if (this.isReadOnly)
			{
				throw new XmlException("Xml_ReadOnlyProperty", base.GetType().Name + "." + propertyName);
			}
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000412D4 File Offset: 0x0003F4D4
		private void Initialize()
		{
			this.encoding = Encoding.UTF8;
			this.omitXmlDecl = false;
			this.newLineHandling = NewLineHandling.Replace;
			this.newLineChars = Environment.NewLine;
			this.indent = TriState.Unknown;
			this.indentChars = "  ";
			this.newLineOnAttributes = false;
			this.closeOutput = false;
			this.namespaceHandling = NamespaceHandling.Default;
			this.conformanceLevel = ConformanceLevel.Document;
			this.checkCharacters = true;
			this.writeEndDocumentOnClose = true;
			this.outputMethod = XmlOutputMethod.Xml;
			this.cdataSections.Clear();
			this.mergeCDataSections = false;
			this.mediaType = null;
			this.docTypeSystem = null;
			this.docTypePublic = null;
			this.standalone = XmlStandalone.Omit;
			this.doNotEscapeUriAttributes = false;
			this.useAsync = false;
			this.isReadOnly = false;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0004138C File Offset: 0x0003F58C
		private XmlWriter AddConformanceWrapper(XmlWriter baseWriter)
		{
			ConformanceLevel conformanceLevel = ConformanceLevel.Auto;
			XmlWriterSettings settings = baseWriter.Settings;
			bool flag = false;
			bool checkNames = false;
			bool flag2 = false;
			bool flag3 = false;
			if (settings == null)
			{
				if (this.newLineHandling == NewLineHandling.Replace)
				{
					flag2 = true;
					flag3 = true;
				}
				if (this.checkCharacters)
				{
					flag = true;
					flag3 = true;
				}
			}
			else
			{
				if (this.conformanceLevel != settings.ConformanceLevel)
				{
					conformanceLevel = this.ConformanceLevel;
					flag3 = true;
				}
				if (this.checkCharacters && !settings.CheckCharacters)
				{
					flag = true;
					checkNames = (conformanceLevel == ConformanceLevel.Auto);
					flag3 = true;
				}
				if (this.newLineHandling == NewLineHandling.Replace && settings.NewLineHandling == NewLineHandling.None)
				{
					flag2 = true;
					flag3 = true;
				}
			}
			XmlWriter xmlWriter = baseWriter;
			if (flag3)
			{
				if (conformanceLevel != ConformanceLevel.Auto)
				{
					xmlWriter = new XmlWellFormedWriter(xmlWriter, this);
				}
				if (flag || flag2)
				{
					xmlWriter = new XmlCharCheckingWriter(xmlWriter, flag, checkNames, flag2, this.NewLineChars);
				}
			}
			if (this.IsQuerySpecific && (settings == null || !settings.IsQuerySpecific))
			{
				xmlWriter = new QueryOutputWriterV1(xmlWriter, this);
			}
			return xmlWriter;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00041464 File Offset: 0x0003F664
		internal void GetObjectData(XmlQueryDataWriter writer)
		{
			writer.Write(this.Encoding.CodePage);
			writer.Write(this.OmitXmlDeclaration);
			writer.Write((sbyte)this.NewLineHandling);
			writer.WriteStringQ(this.NewLineChars);
			writer.Write((sbyte)this.IndentInternal);
			writer.WriteStringQ(this.IndentChars);
			writer.Write(this.NewLineOnAttributes);
			writer.Write(this.CloseOutput);
			writer.Write((sbyte)this.ConformanceLevel);
			writer.Write(this.CheckCharacters);
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
			writer.Write(this.ReadOnly);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000415BC File Offset: 0x0003F7BC
		internal XmlWriterSettings(XmlQueryDataReader reader)
		{
			this.Encoding = Encoding.GetEncoding(reader.ReadInt32());
			this.OmitXmlDeclaration = reader.ReadBoolean();
			this.NewLineHandling = (NewLineHandling)reader.ReadSByte(0, 2);
			this.NewLineChars = reader.ReadStringQ();
			this.IndentInternal = (TriState)reader.ReadSByte(-1, 1);
			this.IndentChars = reader.ReadStringQ();
			this.NewLineOnAttributes = reader.ReadBoolean();
			this.CloseOutput = reader.ReadBoolean();
			this.ConformanceLevel = (ConformanceLevel)reader.ReadSByte(0, 2);
			this.CheckCharacters = reader.ReadBoolean();
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
			this.ReadOnly = reader.ReadBoolean();
		}

		// Token: 0x04000458 RID: 1112
		private bool useAsync;

		// Token: 0x04000459 RID: 1113
		private Encoding encoding;

		// Token: 0x0400045A RID: 1114
		private bool omitXmlDecl;

		// Token: 0x0400045B RID: 1115
		private NewLineHandling newLineHandling;

		// Token: 0x0400045C RID: 1116
		private string newLineChars;

		// Token: 0x0400045D RID: 1117
		private TriState indent;

		// Token: 0x0400045E RID: 1118
		private string indentChars;

		// Token: 0x0400045F RID: 1119
		private bool newLineOnAttributes;

		// Token: 0x04000460 RID: 1120
		private bool closeOutput;

		// Token: 0x04000461 RID: 1121
		private NamespaceHandling namespaceHandling;

		// Token: 0x04000462 RID: 1122
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000463 RID: 1123
		private bool checkCharacters;

		// Token: 0x04000464 RID: 1124
		private bool writeEndDocumentOnClose;

		// Token: 0x04000465 RID: 1125
		private XmlOutputMethod outputMethod;

		// Token: 0x04000466 RID: 1126
		private List<XmlQualifiedName> cdataSections = new List<XmlQualifiedName>();

		// Token: 0x04000467 RID: 1127
		private bool doNotEscapeUriAttributes;

		// Token: 0x04000468 RID: 1128
		private bool mergeCDataSections;

		// Token: 0x04000469 RID: 1129
		private string mediaType;

		// Token: 0x0400046A RID: 1130
		private string docTypeSystem;

		// Token: 0x0400046B RID: 1131
		private string docTypePublic;

		// Token: 0x0400046C RID: 1132
		private XmlStandalone standalone;

		// Token: 0x0400046D RID: 1133
		private bool autoXmlDecl;

		// Token: 0x0400046E RID: 1134
		private bool isReadOnly;
	}
}
