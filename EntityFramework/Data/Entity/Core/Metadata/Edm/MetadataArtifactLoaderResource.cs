using System;
using System.Collections.Generic;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Resources;
using System.IO;
using System.Reflection;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050B RID: 1291
	internal class MetadataArtifactLoaderResource : MetadataArtifactLoader, IComparable
	{
		// Token: 0x0600305B RID: 12379 RVA: 0x000E7ECC File Offset: 0x000E60CC
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

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x000E7F1B File Offset: 0x000E611B
		public override string Path
		{
			get
			{
				return MetadataArtifactLoaderCompositeResource.CreateResPath(this._assembly, this._resourceName);
			}
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000E7F30 File Offset: 0x000E6130
		public int CompareTo(object obj)
		{
			MetadataArtifactLoaderResource metadataArtifactLoaderResource = obj as MetadataArtifactLoaderResource;
			if (metadataArtifactLoaderResource != null)
			{
				return string.Compare(this.Path, metadataArtifactLoaderResource.Path, StringComparison.OrdinalIgnoreCase);
			}
			return -1;
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000E7F5B File Offset: 0x000E615B
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000E7F67 File Offset: 0x000E6167
		public override int GetHashCode()
		{
			return this.Path.GetHashCode();
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000E7F74 File Offset: 0x000E6174
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000E7FAC File Offset: 0x000E61AC
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded)
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000E7FD4 File Offset: 0x000E61D4
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

		// Token: 0x06003063 RID: 12387 RVA: 0x000E800C File Offset: 0x000E620C
		private XmlReader CreateReader()
		{
			Stream input = this.LoadResource();
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.CloseInput = true;
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			return XmlReader.Create(input, xmlReaderSettings);
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000E8040 File Offset: 0x000E6240
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

		// Token: 0x06003065 RID: 12389 RVA: 0x000E8078 File Offset: 0x000E6278
		private Stream LoadResource()
		{
			Stream result;
			if (this.TryCreateResourceStream(out result))
			{
				return result;
			}
			throw new MetadataException(Strings.UnableToLoadResource);
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000E809B File Offset: 0x000E629B
		private bool TryCreateResourceStream(out Stream resourceStream)
		{
			resourceStream = this._assembly.GetManifestResourceStream(this._resourceName);
			return resourceStream != null;
		}

		// Token: 0x0400125E RID: 4702
		private readonly bool _alreadyLoaded;

		// Token: 0x0400125F RID: 4703
		private readonly Assembly _assembly;

		// Token: 0x04001260 RID: 4704
		private readonly string _resourceName;
	}
}
