using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.IO;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000505 RID: 1285
	internal abstract class MetadataArtifactLoader
	{
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600300B RID: 12299
		public abstract string Path { get; }

		// Token: 0x0600300C RID: 12300 RVA: 0x000E6D40 File Offset: 0x000E4F40
		public static MetadataArtifactLoader Create(string path, MetadataArtifactLoader.ExtensionCheck extensionCheck, string validExtension, ICollection<string> uriRegistry)
		{
			return MetadataArtifactLoader.Create(path, extensionCheck, validExtension, uriRegistry, new DefaultAssemblyResolver());
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000E6D50 File Offset: 0x000E4F50
		internal static MetadataArtifactLoader Create(string path, MetadataArtifactLoader.ExtensionCheck extensionCheck, string validExtension, ICollection<string> uriRegistry, MetadataArtifactAssemblyResolver resolver)
		{
			if (MetadataArtifactLoader.PathStartsWithResPrefix(path))
			{
				return MetadataArtifactLoaderCompositeResource.CreateResourceLoader(path, extensionCheck, validExtension, uriRegistry, resolver);
			}
			string text = MetadataArtifactLoader.NormalizeFilePaths(path);
			if (Directory.Exists(text))
			{
				return new MetadataArtifactLoaderCompositeFile(text, uriRegistry);
			}
			if (File.Exists(text))
			{
				switch (extensionCheck)
				{
				case MetadataArtifactLoader.ExtensionCheck.Specific:
					MetadataArtifactLoader.CheckArtifactExtension(text, validExtension);
					break;
				case MetadataArtifactLoader.ExtensionCheck.All:
					if (!MetadataArtifactLoader.IsValidArtifact(text))
					{
						throw new MetadataException(Strings.InvalidMetadataPath);
					}
					break;
				}
				return new MetadataArtifactLoaderFile(text, uriRegistry);
			}
			throw new MetadataException(Strings.InvalidMetadataPath);
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000E6DD2 File Offset: 0x000E4FD2
		public static MetadataArtifactLoader Create(List<MetadataArtifactLoader> allCollections)
		{
			return new MetadataArtifactLoaderComposite(allCollections);
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000E6DDA File Offset: 0x000E4FDA
		public static MetadataArtifactLoader CreateCompositeFromFilePaths(IEnumerable<string> filePaths, string validExtension)
		{
			return MetadataArtifactLoader.CreateCompositeFromFilePaths(filePaths, validExtension, new DefaultAssemblyResolver());
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000E6DE8 File Offset: 0x000E4FE8
		internal static MetadataArtifactLoader CreateCompositeFromFilePaths(IEnumerable<string> filePaths, string validExtension, MetadataArtifactAssemblyResolver resolver)
		{
			MetadataArtifactLoader.ExtensionCheck extensionCheck;
			if (string.IsNullOrEmpty(validExtension))
			{
				extensionCheck = MetadataArtifactLoader.ExtensionCheck.All;
			}
			else
			{
				extensionCheck = MetadataArtifactLoader.ExtensionCheck.Specific;
			}
			List<MetadataArtifactLoader> list = new List<MetadataArtifactLoader>();
			HashSet<string> uriRegistry = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in filePaths)
			{
				if (string.IsNullOrEmpty(text))
				{
					throw new MetadataException(Strings.NotValidInputPath, new ArgumentException(Strings.ADP_CollectionParameterElementIsNullOrEmpty("filePaths")));
				}
				string text2 = text.Trim();
				if (text2.Length > 0)
				{
					list.Add(MetadataArtifactLoader.Create(text2, extensionCheck, validExtension, uriRegistry, resolver));
				}
			}
			return MetadataArtifactLoader.Create(list);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000E6E98 File Offset: 0x000E5098
		public static MetadataArtifactLoader CreateCompositeFromXmlReaders(IEnumerable<XmlReader> xmlReaders)
		{
			List<MetadataArtifactLoader> list = new List<MetadataArtifactLoader>();
			foreach (XmlReader xmlReader in xmlReaders)
			{
				if (xmlReader == null)
				{
					throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("xmlReaders"));
				}
				list.Add(new MetadataArtifactLoaderXmlReaderWrapper(xmlReader));
			}
			return MetadataArtifactLoader.Create(list);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000E6F04 File Offset: 0x000E5104
		internal static void CheckArtifactExtension(string path, string validExtension)
		{
			string extension = MetadataArtifactLoader.GetExtension(path);
			if (!extension.Equals(validExtension, StringComparison.OrdinalIgnoreCase))
			{
				throw new MetadataException(Strings.InvalidFileExtension(path, extension, validExtension));
			}
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000E6F30 File Offset: 0x000E5130
		public virtual List<string> GetOriginalPaths()
		{
			return new List<string>(new string[]
			{
				this.Path
			});
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000E6F54 File Offset: 0x000E5154
		public virtual List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x000E6F82 File Offset: 0x000E5182
		public virtual bool IsComposite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003016 RID: 12310
		public abstract List<string> GetPaths();

		// Token: 0x06003017 RID: 12311
		public abstract List<string> GetPaths(DataSpace spaceToGet);

		// Token: 0x06003018 RID: 12312 RVA: 0x000E6F85 File Offset: 0x000E5185
		public List<XmlReader> GetReaders()
		{
			return this.GetReaders(null);
		}

		// Token: 0x06003019 RID: 12313
		public abstract List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary);

		// Token: 0x0600301A RID: 12314
		public abstract List<XmlReader> CreateReaders(DataSpace spaceToGet);

		// Token: 0x0600301B RID: 12315 RVA: 0x000E6F8E File Offset: 0x000E518E
		internal static bool PathStartsWithResPrefix(string path)
		{
			return path.StartsWith(MetadataArtifactLoader.resPathPrefix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000E6F9C File Offset: 0x000E519C
		protected static bool IsCSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".csdl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000E6FCC File Offset: 0x000E51CC
		protected static bool IsSSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".ssdl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000E6FFC File Offset: 0x000E51FC
		protected static bool IsCSSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".msl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000E702C File Offset: 0x000E522C
		private static string GetExtension(string resource)
		{
			if (string.IsNullOrEmpty(resource))
			{
				return string.Empty;
			}
			int num = resource.LastIndexOf('.');
			if (num < 0)
			{
				return string.Empty;
			}
			return resource.Substring(num);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000E7064 File Offset: 0x000E5264
		internal static bool IsValidArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && (string.Compare(extension, ".csdl", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(extension, ".ssdl", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(extension, ".msl", StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000E70AF File Offset: 0x000E52AF
		protected static bool IsArtifactOfDataSpace(string resource, DataSpace dataSpace)
		{
			if (dataSpace == DataSpace.CSpace)
			{
				return MetadataArtifactLoader.IsCSpaceArtifact(resource);
			}
			if (dataSpace == DataSpace.SSpace)
			{
				return MetadataArtifactLoader.IsSSpaceArtifact(resource);
			}
			return dataSpace == DataSpace.CSSpace && MetadataArtifactLoader.IsCSSpaceArtifact(resource);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000E70D4 File Offset: 0x000E52D4
		internal static string NormalizeFilePaths(string path)
		{
			bool flag = true;
			if (!string.IsNullOrEmpty(path))
			{
				path = path.Trim();
				if (path.StartsWith("~", StringComparison.Ordinal))
				{
					AspProxy aspProxy = new AspProxy();
					path = aspProxy.MapWebPath(path);
					flag = false;
				}
				if (path.Length == 2 && path[1] == System.IO.Path.VolumeSeparatorChar)
				{
					path += System.IO.Path.DirectorySeparatorChar;
				}
				else
				{
					string text = DbProviderServices.ExpandDataDirectory(path);
					if (!path.Equals(text, StringComparison.Ordinal))
					{
						path = text;
						flag = false;
					}
				}
			}
			try
			{
				if (flag)
				{
					path = System.IO.Path.GetFullPath(path);
				}
			}
			catch (ArgumentException innerException)
			{
				throw new MetadataException(Strings.NotValidInputPath, innerException);
			}
			catch (NotSupportedException innerException2)
			{
				throw new MetadataException(Strings.NotValidInputPath, innerException2);
			}
			catch (PathTooLongException)
			{
				throw new MetadataException(Strings.NotValidInputPath);
			}
			return path;
		}

		// Token: 0x0400124C RID: 4684
		protected static readonly string resPathPrefix = "res://";

		// Token: 0x0400124D RID: 4685
		protected static readonly string resPathSeparator = "/";

		// Token: 0x0400124E RID: 4686
		protected static readonly string altPathSeparator = "\\";

		// Token: 0x0400124F RID: 4687
		protected static readonly string wildcard = "*";

		// Token: 0x02000506 RID: 1286
		public enum ExtensionCheck
		{
			// Token: 0x04001251 RID: 4689
			None,
			// Token: 0x04001252 RID: 4690
			Specific,
			// Token: 0x04001253 RID: 4691
			All
		}
	}
}
