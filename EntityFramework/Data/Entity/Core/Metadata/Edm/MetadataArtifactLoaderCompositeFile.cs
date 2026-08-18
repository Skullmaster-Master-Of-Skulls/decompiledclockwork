using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000508 RID: 1288
	internal class MetadataArtifactLoaderCompositeFile : MetadataArtifactLoader
	{
		// Token: 0x06003030 RID: 12336 RVA: 0x000E744E File Offset: 0x000E564E
		public MetadataArtifactLoaderCompositeFile(string path, ICollection<string> uriRegistry)
		{
			this._path = path;
			this._uriRegistry = uriRegistry;
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x000E7464 File Offset: 0x000E5664
		public override string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x000E746C File Offset: 0x000E566C
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x000E746F File Offset: 0x000E566F
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> CsdlChildren
		{
			get
			{
				this.LoadCollections();
				return this._csdlChildren;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x000E747D File Offset: 0x000E567D
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> SsdlChildren
		{
			get
			{
				this.LoadCollections();
				return this._ssdlChildren;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x000E748B File Offset: 0x000E568B
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> MslChildren
		{
			get
			{
				this.LoadCollections();
				return this._mslChildren;
			}
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000E749C File Offset: 0x000E569C
		private void LoadCollections()
		{
			if (this._csdlChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> value = new ReadOnlyCollection<MetadataArtifactLoaderFile>(MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".csdl", this._uriRegistry));
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._csdlChildren, value, null);
			}
			if (this._ssdlChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> value2 = new ReadOnlyCollection<MetadataArtifactLoaderFile>(MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".ssdl", this._uriRegistry));
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._ssdlChildren, value2, null);
			}
			if (this._mslChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> value3 = new ReadOnlyCollection<MetadataArtifactLoaderFile>(MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".msl", this._uriRegistry));
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._mslChildren, value3, null);
			}
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x000E753F File Offset: 0x000E573F
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			return this.GetOriginalPaths();
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000E7548 File Offset: 0x000E5748
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			IList<MetadataArtifactLoaderFile> list2;
			if (!this.TryGetListForSpace(spaceToGet, out list2))
			{
				return list;
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile in list2)
			{
				list.AddRange(metadataArtifactLoaderFile.GetPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000E75AC File Offset: 0x000E57AC
		private bool TryGetListForSpace(DataSpace spaceToGet, out IList<MetadataArtifactLoaderFile> files)
		{
			switch (spaceToGet)
			{
			case DataSpace.CSpace:
				files = this.CsdlChildren;
				return true;
			case DataSpace.SSpace:
				files = this.SsdlChildren;
				return true;
			case DataSpace.CSSpace:
				files = this.MslChildren;
				return true;
			}
			files = null;
			return false;
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000E75F8 File Offset: 0x000E57F8
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile in this.CsdlChildren)
			{
				list.AddRange(metadataArtifactLoaderFile.GetPaths());
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile2 in this.SsdlChildren)
			{
				list.AddRange(metadataArtifactLoaderFile2.GetPaths());
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile3 in this.MslChildren)
			{
				list.AddRange(metadataArtifactLoaderFile3.GetPaths());
			}
			return list;
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000E76E4 File Offset: 0x000E58E4
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile in this.CsdlChildren)
			{
				list.AddRange(metadataArtifactLoaderFile.GetReaders(sourceDictionary));
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile2 in this.SsdlChildren)
			{
				list.AddRange(metadataArtifactLoaderFile2.GetReaders(sourceDictionary));
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile3 in this.MslChildren)
			{
				list.AddRange(metadataArtifactLoaderFile3.GetReaders(sourceDictionary));
			}
			return list;
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000E77D4 File Offset: 0x000E59D4
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			IList<MetadataArtifactLoaderFile> list2;
			if (!this.TryGetListForSpace(spaceToGet, out list2))
			{
				return list;
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile in list2)
			{
				list.AddRange(metadataArtifactLoaderFile.CreateReaders(spaceToGet));
			}
			return list;
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000E7838 File Offset: 0x000E5A38
		private static List<MetadataArtifactLoaderFile> GetArtifactsInDirectory(string directory, string extension, ICollection<string> uriRegistry)
		{
			List<MetadataArtifactLoaderFile> list = new List<MetadataArtifactLoaderFile>();
			string[] files = Directory.GetFiles(directory, MetadataArtifactLoader.wildcard + extension, SearchOption.TopDirectoryOnly);
			foreach (string text in files)
			{
				string text2 = System.IO.Path.Combine(directory, text);
				if (!uriRegistry.Contains(text2) && text.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(new MetadataArtifactLoaderFile(text2, uriRegistry));
				}
			}
			return list;
		}

		// Token: 0x04001255 RID: 4693
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _csdlChildren;

		// Token: 0x04001256 RID: 4694
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _ssdlChildren;

		// Token: 0x04001257 RID: 4695
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _mslChildren;

		// Token: 0x04001258 RID: 4696
		private readonly string _path;

		// Token: 0x04001259 RID: 4697
		private readonly ICollection<string> _uriRegistry;
	}
}
