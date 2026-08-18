using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.EntityClient;
using System.IO;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001FB RID: 507
	internal abstract class MetadataArtifactLoader
	{
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002152 RID: 8530
		public abstract string Path { get; }

		// Token: 0x06002153 RID: 8531
		public abstract void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet);

		// Token: 0x06002154 RID: 8532 RVA: 0x000755BF File Offset: 0x000737BF
		public static MetadataArtifactLoader Create(string path, MetadataArtifactLoader.ExtensionCheck extensionCheck, string validExtension, ICollection<string> uriRegistry)
		{
			return MetadataArtifactLoader.Create(path, extensionCheck, validExtension, uriRegistry, new DefaultAssemblyResolver());
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000755D0 File Offset: 0x000737D0
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
				if (extensionCheck != MetadataArtifactLoader.ExtensionCheck.Specific)
				{
					if (extensionCheck == MetadataArtifactLoader.ExtensionCheck.All)
					{
						if (!MetadataArtifactLoader.IsValidArtifact(text))
						{
							throw EntityUtil.Metadata(Strings.InvalidMetadataPath);
						}
					}
				}
				else
				{
					MetadataArtifactLoader.CheckArtifactExtension(text, validExtension);
				}
				return new MetadataArtifactLoaderFile(text, uriRegistry);
			}
			throw EntityUtil.Metadata(Strings.InvalidMetadataPath);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00075648 File Offset: 0x00073848
		public static MetadataArtifactLoader Create(List<MetadataArtifactLoader> allCollections)
		{
			return new MetadataArtifactLoaderComposite(allCollections);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00075650 File Offset: 0x00073850
		public static MetadataArtifactLoader CreateCompositeFromFilePaths(IEnumerable<string> filePaths, string validExtension)
		{
			return MetadataArtifactLoader.CreateCompositeFromFilePaths(filePaths, validExtension, new DefaultAssemblyResolver());
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x00075660 File Offset: 0x00073860
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
					throw EntityUtil.Metadata(Strings.NotValidInputPath, EntityUtil.CollectionParameterElementIsNullOrEmpty("filePaths"));
				}
				string text2 = text.Trim();
				if (text2.Length > 0)
				{
					list.Add(MetadataArtifactLoader.Create(text2, extensionCheck, validExtension, uriRegistry, resolver));
				}
			}
			return MetadataArtifactLoader.Create(list);
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0007570C File Offset: 0x0007390C
		public static MetadataArtifactLoader CreateCompositeFromXmlReaders(IEnumerable<XmlReader> xmlReaders)
		{
			List<MetadataArtifactLoader> list = new List<MetadataArtifactLoader>();
			foreach (XmlReader xmlReader in xmlReaders)
			{
				if (xmlReader == null)
				{
					throw EntityUtil.CollectionParameterElementIsNull("xmlReaders");
				}
				list.Add(new MetadataArtifactLoaderXmlReaderWrapper(xmlReader));
			}
			return MetadataArtifactLoader.Create(list);
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x00075774 File Offset: 0x00073974
		internal static void CheckArtifactExtension(string path, string validExtension)
		{
			string extension = MetadataArtifactLoader.GetExtension(path);
			if (!extension.Equals(validExtension, StringComparison.OrdinalIgnoreCase))
			{
				throw EntityUtil.Metadata(Strings.InvalidFileExtension(path, extension, validExtension));
			}
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0006C916 File Offset: 0x0006AB16
		public virtual List<string> GetOriginalPaths()
		{
			return new List<string>(new string[]
			{
				this.Path
			});
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000757A0 File Offset: 0x000739A0
		public virtual List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x000173E2 File Offset: 0x000155E2
		public virtual bool IsComposite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600215E RID: 8542
		public abstract List<string> GetPaths();

		// Token: 0x0600215F RID: 8543
		public abstract List<string> GetPaths(DataSpace spaceToGet);

		// Token: 0x06002160 RID: 8544 RVA: 0x000757CE File Offset: 0x000739CE
		public List<XmlReader> GetReaders()
		{
			return this.GetReaders(null);
		}

		// Token: 0x06002161 RID: 8545
		public abstract List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary);

		// Token: 0x06002162 RID: 8546
		public abstract List<XmlReader> CreateReaders(DataSpace spaceToGet);

		// Token: 0x06002163 RID: 8547 RVA: 0x000757D7 File Offset: 0x000739D7
		internal static bool PathStartsWithResPrefix(string path)
		{
			return path.StartsWith(MetadataArtifactLoader.resPathPrefix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000757E8 File Offset: 0x000739E8
		protected static bool IsCSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".csdl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00075818 File Offset: 0x00073A18
		protected static bool IsSSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".ssdl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00075848 File Offset: 0x00073A48
		protected static bool IsCSSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".msl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00075878 File Offset: 0x00073A78
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

		// Token: 0x06002168 RID: 8552 RVA: 0x000758B0 File Offset: 0x00073AB0
		internal static bool IsValidArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && (string.Compare(extension, ".csdl", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(extension, ".ssdl", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(extension, ".msl", StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000758FB File Offset: 0x00073AFB
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

		// Token: 0x0600216A RID: 8554 RVA: 0x00075920 File Offset: 0x00073B20
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
					path += System.IO.Path.DirectorySeparatorChar.ToString();
				}
				else
				{
					string text = DbConnectionOptions.ExpandDataDirectory("metadata", path);
					if (text != null)
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
			catch (ArgumentException inner)
			{
				throw EntityUtil.Metadata(Strings.NotValidInputPath, inner);
			}
			catch (NotSupportedException inner2)
			{
				throw EntityUtil.Metadata(Strings.NotValidInputPath, inner2);
			}
			catch (PathTooLongException)
			{
				throw EntityUtil.Metadata(Strings.NotValidInputPath);
			}
			return path;
		}

		// Token: 0x04000EB8 RID: 3768
		protected static readonly string resPathPrefix = "res://";

		// Token: 0x04000EB9 RID: 3769
		protected static readonly string resPathSeparator = "/";

		// Token: 0x04000EBA RID: 3770
		protected static readonly string altPathSeparator = "\\";

		// Token: 0x04000EBB RID: 3771
		protected static readonly string wildcard = "*";

		// Token: 0x02000525 RID: 1317
		public enum ExtensionCheck
		{
			// Token: 0x04001B5F RID: 7007
			None,
			// Token: 0x04001B60 RID: 7008
			Specific,
			// Token: 0x04001B61 RID: 7009
			All
		}
	}
}
