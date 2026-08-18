using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050C RID: 1292
	internal class MetadataArtifactLoaderXmlReaderWrapper : MetadataArtifactLoader, IComparable
	{
		// Token: 0x06003067 RID: 12391 RVA: 0x000E80B8 File Offset: 0x000E62B8
		public MetadataArtifactLoaderXmlReaderWrapper(XmlReader xmlReader)
		{
			this._reader = xmlReader;
			this._resourceUri = xmlReader.BaseURI;
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000E80D3 File Offset: 0x000E62D3
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

		// Token: 0x06003069 RID: 12393 RVA: 0x000E80F0 File Offset: 0x000E62F0
		public int CompareTo(object obj)
		{
			MetadataArtifactLoaderXmlReaderWrapper metadataArtifactLoaderXmlReaderWrapper = obj as MetadataArtifactLoaderXmlReaderWrapper;
			if (metadataArtifactLoaderXmlReaderWrapper == null)
			{
				return -1;
			}
			if (object.ReferenceEquals(this._reader, metadataArtifactLoaderXmlReaderWrapper._reader))
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000E811F File Offset: 0x000E631F
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000E812B File Offset: 0x000E632B
		public override int GetHashCode()
		{
			return this._reader.GetHashCode();
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000E8138 File Offset: 0x000E6338
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000E8168 File Offset: 0x000E6368
		public override List<string> GetPaths()
		{
			return new List<string>(new string[]
			{
				this.Path
			});
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000E818C File Offset: 0x000E638C
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

		// Token: 0x0600306F RID: 12399 RVA: 0x000E81BC File Offset: 0x000E63BC
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this._reader);
			}
			return list;
		}

		// Token: 0x04001261 RID: 4705
		private readonly XmlReader _reader;

		// Token: 0x04001262 RID: 4706
		private readonly string _resourceUri;
	}
}
