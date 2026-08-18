using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001FC RID: 508
	internal class MetadataArtifactLoaderComposite : MetadataArtifactLoader, IEnumerable<MetadataArtifactLoader>, IEnumerable
	{
		// Token: 0x0600216D RID: 8557 RVA: 0x00075A2E File Offset: 0x00073C2E
		public MetadataArtifactLoaderComposite(List<MetadataArtifactLoader> children)
		{
			this._children = new List<MetadataArtifactLoader>(children).AsReadOnly();
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x000406A4 File Offset: 0x0003E8A4
		public override string Path
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x00075A48 File Offset: 0x00073C48
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				metadataArtifactLoader.CollectFilePermissionPaths(paths, spaceToGet);
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x00017938 File Offset: 0x00015B38
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x00075A98 File Offset: 0x00073C98
		public override List<string> GetOriginalPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetOriginalPaths());
			}
			return list;
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x00075AF4 File Offset: 0x00073CF4
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetOriginalPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x00075B50 File Offset: 0x00073D50
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x00075BAC File Offset: 0x00073DAC
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetPaths());
			}
			return list;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x00075C08 File Offset: 0x00073E08
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetReaders(sourceDictionary));
			}
			return list;
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x00075C64 File Offset: 0x00073E64
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.CreateReaders(spaceToGet));
			}
			return list;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x00075CC0 File Offset: 0x00073EC0
		public IEnumerator<MetadataArtifactLoader> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x00075CC0 File Offset: 0x00073EC0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x04000EBC RID: 3772
		private readonly ReadOnlyCollection<MetadataArtifactLoader> _children;
	}
}
