using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001B0 RID: 432
	internal class MetadataArtifactLoaderXmlReaderWrapper : MetadataArtifactLoader, IComparable
	{
		// Token: 0x06001EC6 RID: 7878 RVA: 0x0006C86E File Offset: 0x0006AA6E
		public MetadataArtifactLoaderXmlReaderWrapper(XmlReader xmlReader)
		{
			this._reader = xmlReader;
			this._resourceUri = xmlReader.BaseURI;
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0006C889 File Offset: 0x0006AA89
		public override string Path
		{
			get
			{
				if (string.IsNullOrEmpty(this._resourceUri))
				{
					return string.Empty;
				}
				return this._resourceUri;
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x0006C8A4 File Offset: 0x0006AAA4
		public int CompareTo(object obj)
		{
			MetadataArtifactLoaderXmlReaderWrapper metadataArtifactLoaderXmlReaderWrapper = obj as MetadataArtifactLoaderXmlReaderWrapper;
			if (metadataArtifactLoaderXmlReaderWrapper == null)
			{
				return -1;
			}
			if (this._reader == metadataArtifactLoaderXmlReaderWrapper._reader)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x0006C8CE File Offset: 0x0006AACE
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x0006C8DA File Offset: 0x0006AADA
		public override int GetHashCode()
		{
			return this._reader.GetHashCode();
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x000089D0 File Offset: 0x00006BD0
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0006C8E8 File Offset: 0x0006AAE8
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0006C916 File Offset: 0x0006AB16
		public override List<string> GetPaths()
		{
			return new List<string>(new string[]
			{
				this.Path
			});
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x0006C92C File Offset: 0x0006AB2C
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			list.Add(this._reader);
			if (sourceDictionary != null)
			{
				sourceDictionary.Add(this, this._reader);
			}
			return list;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x0006C95C File Offset: 0x0006AB5C
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this._reader);
			}
			return list;
		}

		// Token: 0x04000CE7 RID: 3303
		private readonly XmlReader _reader;

		// Token: 0x04000CE8 RID: 3304
		private readonly string _resourceUri;
	}
}
