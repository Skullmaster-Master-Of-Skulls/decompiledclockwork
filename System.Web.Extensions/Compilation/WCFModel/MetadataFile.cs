using System;
using System.IO;
using System.ServiceModel.Description;
using System.Web.Resources;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x0200001A RID: 26
	internal class MetadataFile : ExternalFile
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x000041F0 File Offset: 0x000023F0
		public MetadataFile()
		{
			this.m_ID = Guid.NewGuid().ToString();
			this.m_BinaryContent = new byte[0];
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004228 File Offset: 0x00002428
		public MetadataFile(string name, string url, string content) : base(name)
		{
			this.m_ID = Guid.NewGuid().ToString();
			this.m_SourceUrl = url;
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this.LoadContent(content);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004274 File Offset: 0x00002474
		public MetadataFile(string name, string url, byte[] byteContent) : base(name)
		{
			this.m_ID = Guid.NewGuid().ToString();
			this.m_SourceUrl = url;
			if (byteContent == null)
			{
				throw new ArgumentNullException("byteContent");
			}
			this.LoadContent(byteContent);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000042BD File Offset: 0x000024BD
		public byte[] BinaryContent
		{
			get
			{
				return this.m_BinaryContent;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000042C5 File Offset: 0x000024C5
		private MetadataFile.MetadataContent CachedMetadata
		{
			get
			{
				if (this.m_CachedMetadata == null)
				{
					this.m_CachedMetadata = this.LoadMetadataContent(this.m_MetadataType);
				}
				return this.m_CachedMetadata;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000042E8 File Offset: 0x000024E8
		public string Content
		{
			get
			{
				StreamReader streamReader = new StreamReader(new MemoryStream(this.m_BinaryContent));
				return streamReader.ReadToEnd();
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000430C File Offset: 0x0000250C
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00004314 File Offset: 0x00002514
		[XmlAttribute("MetadataType")]
		public MetadataFile.MetadataType FileType
		{
			get
			{
				return this.m_MetadataType;
			}
			set
			{
				this.m_MetadataType = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000431D File Offset: 0x0000251D
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00004325 File Offset: 0x00002525
		[XmlAttribute]
		public string ID
		{
			get
			{
				return this.m_ID;
			}
			set
			{
				this.m_ID = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000432E File Offset: 0x0000252E
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00004336 File Offset: 0x00002536
		[XmlAttribute]
		public bool Ignore
		{
			get
			{
				return this.m_Ignore;
			}
			set
			{
				this.m_Ignore = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000432E File Offset: 0x0000252E
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000433F File Offset: 0x0000253F
		[XmlIgnore]
		public bool IgnoreSpecified
		{
			get
			{
				return this.m_Ignore;
			}
			set
			{
				if (!value)
				{
					this.m_Ignore = false;
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000434B File Offset: 0x0000254B
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00004353 File Offset: 0x00002553
		[XmlAttribute]
		public bool IsMergeResult
		{
			get
			{
				return this.m_IsMergeResult;
			}
			set
			{
				this.m_IsMergeResult = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000434B File Offset: 0x0000254B
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000435C File Offset: 0x0000255C
		[XmlIgnore]
		public bool IsMergeResultSpecified
		{
			get
			{
				return this.m_IsMergeResult;
			}
			set
			{
				if (!value)
				{
					this.m_IsMergeResult = false;
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004368 File Offset: 0x00002568
		public DiscoveryDocument MetadataDiscoveryDocument
		{
			get
			{
				return this.CachedMetadata.MetadataDiscoveryDocument;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004375 File Offset: 0x00002575
		[XmlIgnore]
		public Exception MetadataFormatError
		{
			get
			{
				return this.CachedMetadata.MetadataFormatError;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004382 File Offset: 0x00002582
		public System.Web.Services.Description.ServiceDescription MetadataServiceDescription
		{
			get
			{
				return this.CachedMetadata.MetadataServiceDescription;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000438F File Offset: 0x0000258F
		public XmlSchema MetadataXmlSchema
		{
			get
			{
				return this.CachedMetadata.MetadataXmlSchema;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000439C File Offset: 0x0000259C
		public XmlDocument MetadataXmlDocument
		{
			get
			{
				return this.CachedMetadata.MetadataXmlDocument;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000043A9 File Offset: 0x000025A9
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000043B1 File Offset: 0x000025B1
		[XmlAttribute]
		public int SourceId
		{
			get
			{
				return this.m_SourceId;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(WCFModelStrings.ReferenceGroup_InvalidSourceId);
				}
				this.m_SourceId = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000043C9 File Offset: 0x000025C9
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000043DC File Offset: 0x000025DC
		[XmlIgnore]
		public bool SourceIdSpecified
		{
			get
			{
				return this.m_SourceId != this.SOURCE_ID_NOT_SPECIFIED;
			}
			set
			{
				if (!value)
				{
					this.m_SourceId = this.SOURCE_ID_NOT_SPECIFIED;
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000043ED File Offset: 0x000025ED
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000043F5 File Offset: 0x000025F5
		[XmlAttribute]
		public string SourceUrl
		{
			get
			{
				return this.m_SourceUrl;
			}
			set
			{
				this.m_SourceUrl = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000043FE File Offset: 0x000025FE
		public string TargetNamespace
		{
			get
			{
				return this.CachedMetadata.TargetNamespace;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000440C File Offset: 0x0000260C
		private MetadataFile.MetadataType DetermineFileType(XmlReader reader)
		{
			MetadataFile.MetadataType result;
			try
			{
				if (reader.IsStartElement("definitions", "http://schemas.xmlsoap.org/wsdl/"))
				{
					result = MetadataFile.MetadataType.Wsdl;
				}
				else if (reader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
				{
					result = MetadataFile.MetadataType.Schema;
				}
				else if (reader.IsStartElement("Policy", "http://schemas.xmlsoap.org/ws/2004/09/policy") || reader.IsStartElement("Policy", "http://www.w3.org/ns/ws-policy"))
				{
					result = MetadataFile.MetadataType.Policy;
				}
				else if (reader.IsStartElement("discovery", "http://schemas.xmlsoap.org/disco/"))
				{
					result = MetadataFile.MetadataType.Disco;
				}
				else if (reader.IsStartElement("Edmx", "http://schemas.microsoft.com/ado/2007/06/edmx"))
				{
					result = MetadataFile.MetadataType.Edmx;
				}
				else
				{
					result = MetadataFile.MetadataType.Xml;
				}
			}
			catch (XmlException)
			{
				result = MetadataFile.MetadataType.Unknown;
			}
			return result;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000044B4 File Offset: 0x000026B4
		public string GetDefaultExtension()
		{
			switch (this.m_MetadataType)
			{
			case MetadataFile.MetadataType.Disco:
				return "disco";
			case MetadataFile.MetadataType.Wsdl:
				return "wsdl";
			case MetadataFile.MetadataType.Schema:
				return "xsd";
			case MetadataFile.MetadataType.Policy:
				return "xml";
			case MetadataFile.MetadataType.Xml:
				return "xml";
			case MetadataFile.MetadataType.Edmx:
				return "edmx";
			default:
				return "data";
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004514 File Offset: 0x00002714
		public string GetDefaultFileName()
		{
			if (!string.IsNullOrEmpty(this.TargetNamespace))
			{
				string text = this.TargetNamespace;
				if (!text.EndsWith("/", StringComparison.Ordinal))
				{
					int num = text.LastIndexOfAny(Path.GetInvalidFileNameChars());
					if (num >= 0)
					{
						text = text.Substring(num + 1);
					}
					string text2 = "." + this.GetDefaultExtension();
					if (text.Length > text2.Length && text.EndsWith(text2, StringComparison.OrdinalIgnoreCase))
					{
						text = text.Substring(0, text.Length - text2.Length);
					}
					if (text.Length > 0)
					{
						return text;
					}
				}
			}
			return "service";
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000045AB File Offset: 0x000027AB
		internal void LoadContent(byte[] byteContent)
		{
			this.m_BinaryContent = byteContent;
			this.LoadContentFromTextReader(new StreamReader(new MemoryStream(byteContent)));
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000045C8 File Offset: 0x000027C8
		internal void LoadContent(string content)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(content);
			streamWriter.Flush();
			this.m_BinaryContent = memoryStream.ToArray();
			this.LoadContentFromTextReader(new StringReader(content));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004608 File Offset: 0x00002808
		private void LoadContentFromTextReader(TextReader contentReader)
		{
			if (contentReader == null)
			{
				throw new ArgumentNullException("contentReader");
			}
			base.ErrorInLoading = null;
			this.m_CachedMetadata = null;
			using (XmlTextReader xmlTextReader = new XmlTextReader(contentReader))
			{
				if (this.m_MetadataType == MetadataFile.MetadataType.Unknown)
				{
					MetadataFile.MetadataType metadataType = this.DetermineFileType(xmlTextReader);
					this.m_CachedMetadata = this.LoadMetadataContent(metadataType, xmlTextReader);
					if (this.m_CachedMetadata.MetadataFormatError == null)
					{
						this.m_MetadataType = metadataType;
					}
				}
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004688 File Offset: 0x00002888
		internal void CleanUpContent()
		{
			base.ErrorInLoading = null;
			this.m_BinaryContent = new byte[0];
			this.m_CachedMetadata = null;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000046A4 File Offset: 0x000028A4
		private MetadataFile.MetadataContent LoadMetadataContent(MetadataFile.MetadataType fileType)
		{
			if (base.ErrorInLoading != null)
			{
				return new MetadataFile.MetadataContent(base.ErrorInLoading);
			}
			MetadataFile.MetadataContent result;
			using (XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(new MemoryStream(this.m_BinaryContent))))
			{
				result = this.LoadMetadataContent(fileType, xmlTextReader);
			}
			return result;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004704 File Offset: 0x00002904
		private MetadataFile.MetadataContent LoadMetadataContent(MetadataFile.MetadataType fileType, XmlTextReader xmlReader)
		{
			MetadataFile.MetadataContent metadataContent = new MetadataFile.MetadataContent();
			try
			{
				switch (fileType)
				{
				case MetadataFile.MetadataType.Unknown:
					break;
				case MetadataFile.MetadataType.Disco:
					metadataContent = new MetadataFile.MetadataContent(DiscoveryDocument.Read(xmlReader));
					break;
				case MetadataFile.MetadataType.Wsdl:
					metadataContent = new MetadataFile.MetadataContent(System.Web.Services.Description.ServiceDescription.Read(xmlReader));
					metadataContent.MetadataServiceDescription.RetrievalUrl = this.GetMetadataSourceUrl();
					break;
				case MetadataFile.MetadataType.Schema:
					metadataContent = new MetadataFile.MetadataContent(XmlSchema.Read(xmlReader, null));
					metadataContent.MetadataXmlSchema.SourceUri = this.GetMetadataSourceUrl();
					break;
				default:
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(xmlReader);
					metadataContent = new MetadataFile.MetadataContent(xmlDocument);
					break;
				}
				}
			}
			catch (Exception metadataFormatError)
			{
				metadataContent = new MetadataFile.MetadataContent(metadataFormatError);
			}
			return metadataContent;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000047B0 File Offset: 0x000029B0
		internal MetadataSection CreateMetadataSection()
		{
			MetadataFile.MetadataContent metadataContent = this.LoadMetadataContent(this.m_MetadataType);
			if (metadataContent.MetadataFormatError != null)
			{
				throw metadataContent.MetadataFormatError;
			}
			MetadataSection result = null;
			switch (this.FileType)
			{
			case MetadataFile.MetadataType.Disco:
				if (metadataContent.MetadataServiceDescription != null)
				{
					result = MetadataSection.CreateFromServiceDescription(metadataContent.MetadataServiceDescription);
				}
				break;
			case MetadataFile.MetadataType.Wsdl:
			{
				System.Web.Services.Description.ServiceDescription metadataServiceDescription = metadataContent.MetadataServiceDescription;
				if (metadataServiceDescription != null)
				{
					result = MetadataSection.CreateFromServiceDescription(metadataServiceDescription);
				}
				break;
			}
			case MetadataFile.MetadataType.Schema:
				if (metadataContent.MetadataXmlSchema != null)
				{
					result = MetadataSection.CreateFromSchema(metadataContent.MetadataXmlSchema);
				}
				break;
			case MetadataFile.MetadataType.Policy:
				if (metadataContent.MetadataXmlDocument != null)
				{
					result = MetadataSection.CreateFromPolicy(metadataContent.MetadataXmlDocument.DocumentElement, null);
				}
				break;
			case MetadataFile.MetadataType.Xml:
			case MetadataFile.MetadataType.Edmx:
				if (metadataContent.MetadataXmlDocument != null)
				{
					result = new MetadataSection(null, null, metadataContent.MetadataXmlDocument.DocumentElement);
				}
				break;
			}
			return result;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000487D File Offset: 0x00002A7D
		internal string GetMetadataSourceUrl()
		{
			if (string.IsNullOrEmpty(this.SourceUrl))
			{
				return base.FileName;
			}
			return this.SourceUrl;
		}

		// Token: 0x0400004B RID: 75
		public const string DEFAULT_FILE_NAME = "service";

		// Token: 0x0400004C RID: 76
		private MetadataFile.MetadataType m_MetadataType;

		// Token: 0x0400004D RID: 77
		private string m_SourceUrl;

		// Token: 0x0400004E RID: 78
		private string m_ID;

		// Token: 0x0400004F RID: 79
		private int m_SourceId;

		// Token: 0x04000050 RID: 80
		private bool m_Ignore;

		// Token: 0x04000051 RID: 81
		private bool m_IsMergeResult;

		// Token: 0x04000052 RID: 82
		private int SOURCE_ID_NOT_SPECIFIED;

		// Token: 0x04000053 RID: 83
		private MetadataFile.MetadataContent m_CachedMetadata;

		// Token: 0x04000054 RID: 84
		private byte[] m_BinaryContent;

		// Token: 0x0200012C RID: 300
		public enum MetadataType
		{
			// Token: 0x04000460 RID: 1120
			[XmlEnum(Name = "Unknown")]
			Unknown,
			// Token: 0x04000461 RID: 1121
			[XmlEnum(Name = "Disco")]
			Disco,
			// Token: 0x04000462 RID: 1122
			[XmlEnum(Name = "Wsdl")]
			Wsdl,
			// Token: 0x04000463 RID: 1123
			[XmlEnum(Name = "Schema")]
			Schema,
			// Token: 0x04000464 RID: 1124
			[XmlEnum(Name = "Policy")]
			Policy,
			// Token: 0x04000465 RID: 1125
			[XmlEnum(Name = "Xml")]
			Xml,
			// Token: 0x04000466 RID: 1126
			[XmlEnum(Name = "Edmx")]
			Edmx
		}

		// Token: 0x0200012D RID: 301
		private class MetadataContent
		{
			// Token: 0x06000F4E RID: 3918 RVA: 0x00037086 File Offset: 0x00035286
			internal MetadataContent()
			{
				this.m_TargetNamespace = string.Empty;
			}

			// Token: 0x06000F4F RID: 3919 RVA: 0x00037099 File Offset: 0x00035299
			internal MetadataContent(DiscoveryDocument discoveryDocument)
			{
				this.m_MetadataDiscoveryDocument = discoveryDocument;
				this.m_TargetNamespace = string.Empty;
			}

			// Token: 0x06000F50 RID: 3920 RVA: 0x000370B3 File Offset: 0x000352B3
			internal MetadataContent(System.Web.Services.Description.ServiceDescription serviceDescription)
			{
				this.m_MetadataServiceDescription = serviceDescription;
				this.m_TargetNamespace = serviceDescription.TargetNamespace;
			}

			// Token: 0x06000F51 RID: 3921 RVA: 0x000370CE File Offset: 0x000352CE
			internal MetadataContent(XmlSchema schema)
			{
				this.m_MetadataXmlSchema = schema;
				this.m_TargetNamespace = schema.TargetNamespace;
			}

			// Token: 0x06000F52 RID: 3922 RVA: 0x000370E9 File Offset: 0x000352E9
			internal MetadataContent(XmlDocument document)
			{
				this.m_MetadataXmlDocument = document;
				this.m_TargetNamespace = string.Empty;
			}

			// Token: 0x06000F53 RID: 3923 RVA: 0x00037103 File Offset: 0x00035303
			internal MetadataContent(Exception metadataFormatError)
			{
				this.m_MetadataFormatError = metadataFormatError;
			}

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00037112 File Offset: 0x00035312
			public DiscoveryDocument MetadataDiscoveryDocument
			{
				get
				{
					return this.m_MetadataDiscoveryDocument;
				}
			}

			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0003711A File Offset: 0x0003531A
			public Exception MetadataFormatError
			{
				get
				{
					return this.m_MetadataFormatError;
				}
			}

			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00037122 File Offset: 0x00035322
			public System.Web.Services.Description.ServiceDescription MetadataServiceDescription
			{
				get
				{
					return this.m_MetadataServiceDescription;
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003712A File Offset: 0x0003532A
			public XmlSchema MetadataXmlSchema
			{
				get
				{
					return this.m_MetadataXmlSchema;
				}
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00037132 File Offset: 0x00035332
			public XmlDocument MetadataXmlDocument
			{
				get
				{
					return this.m_MetadataXmlDocument;
				}
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0003713A File Offset: 0x0003533A
			public string TargetNamespace
			{
				get
				{
					return this.m_TargetNamespace;
				}
			}

			// Token: 0x04000467 RID: 1127
			private DiscoveryDocument m_MetadataDiscoveryDocument;

			// Token: 0x04000468 RID: 1128
			private System.Web.Services.Description.ServiceDescription m_MetadataServiceDescription;

			// Token: 0x04000469 RID: 1129
			private XmlSchema m_MetadataXmlSchema;

			// Token: 0x0400046A RID: 1130
			private XmlDocument m_MetadataXmlDocument;

			// Token: 0x0400046B RID: 1131
			private Exception m_MetadataFormatError;

			// Token: 0x0400046C RID: 1132
			private string m_TargetNamespace;
		}
	}
}
