using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001FD RID: 509
	internal class MetadataArtifactLoaderCompositeFile : MetadataArtifactLoader
	{
		// Token: 0x06002179 RID: 8569 RVA: 0x00075CCD File Offset: 0x00073ECD
		public MetadataArtifactLoaderCompositeFile(string path, ICollection<string> uriRegistry)
		{
			this._path = path;
			this._uriRegistry = uriRegistry;
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x00075CE3 File Offset: 0x00073EE3
		public override string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00075CEC File Offset: 0x00073EEC
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
			IList<MetadataArtifactLoaderFile> list;
			if (this.TryGetListForSpace(spaceToGet, out list))
			{
				foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile in list)
				{
					metadataArtifactLoaderFile.CollectFilePermissionPaths(paths, spaceToGet);
				}
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x00017938 File Offset: 0x00015B38
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x00075D40 File Offset: 0x00073F40
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> CsdlChildren
		{
			get
			{
				this.LoadCollections();
				return this._csdlChildren;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x00075D4E File Offset: 0x00073F4E
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> SsdlChildren
		{
			get
			{
				this.LoadCollections();
				return this._ssdlChildren;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x00075D5C File Offset: 0x00073F5C
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> MslChildren
		{
			get
			{
				this.LoadCollections();
				return this._mslChildren;
			}
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x00075D6C File Offset: 0x00073F6C
		private void LoadCollections()
		{
			if (this._csdlChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> value = MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".csdl", this._uriRegistry).AsReadOnly();
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._csdlChildren, value, null);
			}
			if (this._ssdlChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> value2 = MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".ssdl", this._uriRegistry).AsReadOnly();
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._ssdlChildren, value2, null);
			}
			if (this._mslChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> value3 = MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".msl", this._uriRegistry).AsReadOnly();
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._mslChildren, value3, null);
			}
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00075E0F File Offset: 0x0007400F
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			return this.GetOriginalPaths();
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x00075E18 File Offset: 0x00074018
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

		// Token: 0x06002183 RID: 8579 RVA: 0x00075E7C File Offset: 0x0007407C
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

		// Token: 0x06002184 RID: 8580 RVA: 0x00075EBC File Offset: 0x000740BC
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

		// Token: 0x06002185 RID: 8581 RVA: 0x00075FA0 File Offset: 0x000741A0
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

		// Token: 0x06002186 RID: 8582 RVA: 0x00076088 File Offset: 0x00074288
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

		// Token: 0x06002187 RID: 8583 RVA: 0x000760EC File Offset: 0x000742EC
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

		// Token: 0x04000EBD RID: 3773
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _csdlChildren;

		// Token: 0x04000EBE RID: 3774
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _ssdlChildren;

		// Token: 0x04000EBF RID: 3775
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _mslChildren;

		// Token: 0x04000EC0 RID: 3776
		private readonly string _path;

		// Token: 0x04000EC1 RID: 3777
		private readonly ICollection<string> _uriRegistry;
	}
}
