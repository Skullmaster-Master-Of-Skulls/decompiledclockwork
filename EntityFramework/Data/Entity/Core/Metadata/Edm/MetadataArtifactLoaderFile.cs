using System;
using System.Collections.Generic;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050A RID: 1290
	internal class MetadataArtifactLoaderFile : MetadataArtifactLoader, IComparable
	{
		// Token: 0x06003051 RID: 12369 RVA: 0x000E7D53 File Offset: 0x000E5F53
		public MetadataArtifactLoaderFile(string path, ICollection<string> uriRegistry)
		{
			this._path = path;
			this._alreadyLoaded = uriRegistry.Contains(this._path);
			if (!this._alreadyLoaded)
			{
				uriRegistry.Add(this._path);
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x000E7D88 File Offset: 0x000E5F88
		public override string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000E7D90 File Offset: 0x000E5F90
		public int CompareTo(object obj)
		{
			MetadataArtifactLoaderFile metadataArtifactLoaderFile = obj as MetadataArtifactLoaderFile;
			if (metadataArtifactLoaderFile != null)
			{
				return string.Compare(this._path, metadataArtifactLoaderFile._path, StringComparison.OrdinalIgnoreCase);
			}
			return -1;
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000E7DBB File Offset: 0x000E5FBB
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000E7DC7 File Offset: 0x000E5FC7
		public override int GetHashCode()
		{
			return this._path.GetHashCode();
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000E7DD4 File Offset: 0x000E5FD4
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this._path, spaceToGet))
			{
				list.Add(this._path);
			}
			return list;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000E7E0C File Offset: 0x000E600C
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded)
			{
				list.Add(this._path);
			}
			return list;
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000E7E34 File Offset: 0x000E6034
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

		// Token: 0x06003059 RID: 12377 RVA: 0x000E7E6C File Offset: 0x000E606C
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

		// Token: 0x0600305A RID: 12378 RVA: 0x000E7EA4 File Offset: 0x000E60A4
		private XmlReader CreateXmlReader()
		{
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			return XmlReader.Create(this._path, xmlReaderSettings);
		}

		// Token: 0x0400125C RID: 4700
		private readonly bool _alreadyLoaded;

		// Token: 0x0400125D RID: 4701
		private readonly string _path;
	}
}
