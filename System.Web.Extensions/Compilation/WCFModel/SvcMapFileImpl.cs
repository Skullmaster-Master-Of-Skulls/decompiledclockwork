using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000024 RID: 36
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:xml-wcfservicemap", ElementName = "ReferenceGroup")]
	internal class SvcMapFileImpl
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005908 File Offset: 0x00003B08
		// (set) Token: 0x06000167 RID: 359 RVA: 0x0000593C File Offset: 0x00003B3C
		[XmlAttribute]
		public string ID
		{
			get
			{
				if (this._id == null)
				{
					this._id = Guid.NewGuid().ToString();
				}
				return this._id;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._id = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00005953 File Offset: 0x00003B53
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000596E File Offset: 0x00003B6E
		[XmlElement(ElementName = "ClientOptions", Order = 0)]
		public ClientOptions ClientOptions
		{
			get
			{
				if (this._clientOptions == null)
				{
					this._clientOptions = new ClientOptions();
				}
				return this._clientOptions;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._clientOptions = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00005985 File Offset: 0x00003B85
		[XmlArray(ElementName = "MetadataSources", Order = 1)]
		[XmlArrayItem("MetadataSource", typeof(MetadataSource))]
		public List<MetadataSource> MetadataSourceList
		{
			get
			{
				if (this._metadataSourceList == null)
				{
					this._metadataSourceList = new List<MetadataSource>();
				}
				return this._metadataSourceList;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000059A0 File Offset: 0x00003BA0
		[XmlArray(ElementName = "Metadata", Order = 2)]
		[XmlArrayItem("MetadataFile", typeof(MetadataFile))]
		public List<MetadataFile> MetadataList
		{
			get
			{
				if (this._metadataList == null)
				{
					this._metadataList = new List<MetadataFile>();
				}
				return this._metadataList;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000059BB File Offset: 0x00003BBB
		[XmlArray(ElementName = "Extensions", Order = 3)]
		[XmlArrayItem("ExtensionFile", typeof(ExtensionFile))]
		public List<ExtensionFile> Extensions
		{
			get
			{
				if (this._extensionFileList == null)
				{
					this._extensionFileList = new List<ExtensionFile>();
				}
				return this._extensionFileList;
			}
		}

		// Token: 0x0400006C RID: 108
		public const string NamespaceUri = "urn:schemas-microsoft-com:xml-wcfservicemap";

		// Token: 0x0400006D RID: 109
		private string _id;

		// Token: 0x0400006E RID: 110
		private ClientOptions _clientOptions;

		// Token: 0x0400006F RID: 111
		private List<MetadataSource> _metadataSourceList;

		// Token: 0x04000070 RID: 112
		private List<MetadataFile> _metadataList;

		// Token: 0x04000071 RID: 113
		private List<ExtensionFile> _extensionFileList;
	}
}
