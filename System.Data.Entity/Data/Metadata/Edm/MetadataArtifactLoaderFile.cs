using System;
using System.Collections.Generic;
using System.Data.EntityModel.SchemaObjectModel;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001FF RID: 511
	internal class MetadataArtifactLoaderFile : MetadataArtifactLoader, IComparable
	{
		// Token: 0x0600219C RID: 8604 RVA: 0x0007662C File Offset: 0x0007482C
		public MetadataArtifactLoaderFile(string path, ICollection<string> uriRegistry)
		{
			this._path = path;
			this._alreadyLoaded = uriRegistry.Contains(this._path);
			if (!this._alreadyLoaded)
			{
				uriRegistry.Add(this._path);
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x00076661 File Offset: 0x00074861
		public override string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0007666C File Offset: 0x0007486C
		public int CompareTo(object obj)
		{
			MetadataArtifactLoaderFile metadataArtifactLoaderFile = obj as MetadataArtifactLoaderFile;
			if (metadataArtifactLoaderFile != null)
			{
				return string.Compare(this._path, metadataArtifactLoaderFile._path, StringComparison.OrdinalIgnoreCase);
			}
			return -1;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00076697 File Offset: 0x00074897
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000766A3 File Offset: 0x000748A3
		public override int GetHashCode()
		{
			return this._path.GetHashCode();
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000766B0 File Offset: 0x000748B0
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this._path, spaceToGet))
			{
				paths.Add(this._path);
			}
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000766D4 File Offset: 0x000748D4
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this._path, spaceToGet))
			{
				list.Add(this._path);
			}
			return list;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x0007670C File Offset: 0x0007490C
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded)
			{
				list.Add(this._path);
			}
			return list;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00076734 File Offset: 0x00074934
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (!this._alreadyLoaded)
			{
				XmlReader xmlReader = this.CreateXmlReader();
				list.Add(xmlReader);
				if (sourceDictionary != null)
				{
					sourceDictionary.Add(this, xmlReader);
				}
			}
			return list;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0007676C File Offset: 0x0007496C
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this._path, spaceToGet))
			{
				XmlReader item = this.CreateXmlReader();
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000767A4 File Offset: 0x000749A4
		private XmlReader CreateXmlReader()
		{
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			return XmlReader.Create(this._path, xmlReaderSettings);
		}

		// Token: 0x04000EC4 RID: 3780
		private readonly bool _alreadyLoaded;

		// Token: 0x04000EC5 RID: 3781
		private readonly string _path;
	}
}
