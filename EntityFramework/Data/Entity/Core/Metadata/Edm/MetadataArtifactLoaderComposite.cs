using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000507 RID: 1287
	internal class MetadataArtifactLoaderComposite : MetadataArtifactLoader, IEnumerable<MetadataArtifactLoader>, IEnumerable
	{
		// Token: 0x06003025 RID: 12325 RVA: 0x000E71E6 File Offset: 0x000E53E6
		public MetadataArtifactLoaderComposite(List<MetadataArtifactLoader> children)
		{
			this._children = new ReadOnlyCollection<MetadataArtifactLoader>(new List<MetadataArtifactLoader>(children));
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06003026 RID: 12326 RVA: 0x000E71FF File Offset: 0x000E53FF
		public override string Path
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x000E7206 File Offset: 0x000E5406
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000E720C File Offset: 0x000E540C
		public override List<string> GetOriginalPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetOriginalPaths());
			}
			return list;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000E7268 File Offset: 0x000E5468
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetOriginalPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000E72C4 File Offset: 0x000E54C4
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000E7320 File Offset: 0x000E5520
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetPaths());
			}
			return list;
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000E737C File Offset: 0x000E557C
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetReaders(sourceDictionary));
			}
			return list;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000E73D8 File Offset: 0x000E55D8
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.CreateReaders(spaceToGet));
			}
			return list;
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000E7434 File Offset: 0x000E5634
		public IEnumerator<MetadataArtifactLoader> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000E7441 File Offset: 0x000E5641
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x04001254 RID: 4692
		private readonly ReadOnlyCollection<MetadataArtifactLoader> _children;
	}
}
