using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.EntityModel.SchemaObjectModel;
using System.IO;
using System.Reflection;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000200 RID: 512
	internal class MetadataArtifactLoaderResource : MetadataArtifactLoader, IComparable
	{
		// Token: 0x060021A7 RID: 8615 RVA: 0x000767CC File Offset: 0x000749CC
		internal MetadataArtifactLoaderResource(Assembly assembly, string resourceName, ICollection<string> uriRegistry)
		{
			this._assembly = assembly;
			this._resourceName = resourceName;
			string item = MetadataArtifactLoaderCompositeResource.CreateResPath(this._assembly, this._resourceName);
			this._alreadyLoaded = uriRegistry.Contains(item);
			if (!this._alreadyLoaded)
			{
				uriRegistry.Add(item);
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x0007681B File Offset: 0x00074A1B
		public override string Path
		{
			get
			{
				return MetadataArtifactLoaderCompositeResource.CreateResPath(this._assembly, this._resourceName);
			}
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x00076830 File Offset: 0x00074A30
		public int CompareTo(object obj)
		{
			MetadataArtifactLoaderResource metadataArtifactLoaderResource = obj as MetadataArtifactLoaderResource;
			if (metadataArtifactLoaderResource != null)
			{
				return string.Compare(this.Path, metadataArtifactLoaderResource.Path, StringComparison.OrdinalIgnoreCase);
			}
			return -1;
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0007685B File Offset: 0x00074A5B
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x00076867 File Offset: 0x00074A67
		public override int GetHashCode()
		{
			return this.Path.GetHashCode();
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000089D0 File Offset: 0x00006BD0
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x00076874 File Offset: 0x00074A74
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x000768AC File Offset: 0x00074AAC
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded)
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000768D4 File Offset: 0x00074AD4
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (!this._alreadyLoaded)
			{
				XmlReader xmlReader = this.CreateReader();
				list.Add(xmlReader);
				if (sourceDictionary != null)
				{
					sourceDictionary.Add(this, xmlReader);
				}
			}
			return list;
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0007690C File Offset: 0x00074B0C
		private XmlReader CreateReader()
		{
			Stream input = this.LoadResource();
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.CloseInput = true;
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			return XmlReader.Create(input, xmlReaderSettings);
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x00076940 File Offset: 0x00074B40
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				XmlReader item = this.CreateReader();
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x00076978 File Offset: 0x00074B78
		private Stream LoadResource()
		{
			Stream result;
			if (this.TryCreateResourceStream(out result))
			{
				return result;
			}
			throw EntityUtil.Metadata(Strings.UnableToLoadResource);
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x0007699B File Offset: 0x00074B9B
		private bool TryCreateResourceStream(out Stream resourceStream)
		{
			resourceStream = this._assembly.GetManifestResourceStream(this._resourceName);
			return resourceStream != null;
		}

		// Token: 0x04000EC6 RID: 3782
		private readonly bool _alreadyLoaded;

		// Token: 0x04000EC7 RID: 3783
		private readonly Assembly _assembly;

		// Token: 0x04000EC8 RID: 3784
		private readonly string _resourceName;
	}
}
