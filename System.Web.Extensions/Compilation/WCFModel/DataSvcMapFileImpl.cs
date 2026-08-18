using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000010 RID: 16
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:xml-dataservicemap", ElementName = "ReferenceGroup")]
	internal class DataSvcMapFileImpl
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000386C File Offset: 0x00001A6C
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000038A0 File Offset: 0x00001AA0
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000038B7 File Offset: 0x00001AB7
		[XmlArray(ElementName = "MetadataSources", Order = 0)]
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000038D2 File Offset: 0x00001AD2
		[XmlArray(ElementName = "Metadata", Order = 1)]
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000038ED File Offset: 0x00001AED
		[XmlArray(ElementName = "Extensions", Order = 2)]
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003908 File Offset: 0x00001B08
		[XmlArray(ElementName = "Parameters", Order = 3)]
		[XmlArrayItem("Parameter", typeof(Parameter))]
		public List<Parameter> Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = new List<Parameter>();
				}
				return this._parameters;
			}
		}

		// Token: 0x04000037 RID: 55
		public const string NamespaceUri = "urn:schemas-microsoft-com:xml-dataservicemap";

		// Token: 0x04000038 RID: 56
		private string _id;

		// Token: 0x04000039 RID: 57
		private List<MetadataSource> _metadataSourceList;

		// Token: 0x0400003A RID: 58
		private List<MetadataFile> _metadataList;

		// Token: 0x0400003B RID: 59
		private List<ExtensionFile> _extensionFileList;

		// Token: 0x0400003C RID: 60
		private List<Parameter> _parameters;
	}
}
