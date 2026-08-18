using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001FE RID: 510
	internal class MetadataArtifactLoaderCompositeResource : MetadataArtifactLoader
	{
		// Token: 0x06002188 RID: 8584 RVA: 0x00076155 File Offset: 0x00074355
		internal MetadataArtifactLoaderCompositeResource(string originalPath, string assemblyName, string resourceName, ICollection<string> uriRegistry, MetadataArtifactAssemblyResolver resolver)
		{
			this._originalPath = originalPath;
			this._children = MetadataArtifactLoaderCompositeResource.LoadResources(assemblyName, resourceName, uriRegistry, resolver).AsReadOnly();
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x0007617A File Offset: 0x0007437A
		public override string Path
		{
			get
			{
				return this._originalPath;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x00017938 File Offset: 0x00015B38
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x00076184 File Offset: 0x00074384
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				metadataArtifactLoaderResource.CollectFilePermissionPaths(paths, spaceToGet);
			}
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x00075E0F File Offset: 0x0007400F
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			return this.GetOriginalPaths();
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000761D4 File Offset: 0x000743D4
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00076230 File Offset: 0x00074430
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetPaths());
			}
			return list;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0007628C File Offset: 0x0007448C
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetReaders(sourceDictionary));
			}
			return list;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000762E8 File Offset: 0x000744E8
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.CreateReaders(spaceToGet));
			}
			return list;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00076344 File Offset: 0x00074544
		private static List<MetadataArtifactLoaderResource> LoadResources(string assemblyName, string resourceName, ICollection<string> uriRegistry, MetadataArtifactAssemblyResolver resolver)
		{
			List<MetadataArtifactLoaderResource> list = new List<MetadataArtifactLoaderResource>();
			if (assemblyName == MetadataArtifactLoader.wildcard)
			{
				using (IEnumerator<Assembly> enumerator = resolver.GetWildcardAssemblies().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Assembly assembly = enumerator.Current;
						if (MetadataArtifactLoaderCompositeResource.AssemblyContainsResource(assembly, ref resourceName))
						{
							MetadataArtifactLoaderCompositeResource.LoadResourcesFromAssembly(assembly, resourceName, uriRegistry, list, resolver);
						}
					}
					goto IL_62;
				}
			}
			Assembly assembly2 = MetadataArtifactLoaderCompositeResource.ResolveAssemblyName(assemblyName, resolver);
			MetadataArtifactLoaderCompositeResource.LoadResourcesFromAssembly(assembly2, resourceName, uriRegistry, list, resolver);
			IL_62:
			if (resourceName != null && list.Count == 0)
			{
				throw EntityUtil.Metadata(Strings.UnableToLoadResource);
			}
			return list;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000763DC File Offset: 0x000745DC
		private static bool AssemblyContainsResource(Assembly assembly, ref string resourceName)
		{
			if (resourceName == null)
			{
				return true;
			}
			string[] manifestResourceNamesForAssembly = MetadataArtifactLoaderCompositeResource.GetManifestResourceNamesForAssembly(assembly);
			foreach (string text in manifestResourceNamesForAssembly)
			{
				if (string.Equals(resourceName, text, StringComparison.OrdinalIgnoreCase))
				{
					resourceName = text;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0007641B File Offset: 0x0007461B
		private static void LoadResourcesFromAssembly(Assembly assembly, string resourceName, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders, MetadataArtifactAssemblyResolver resolver)
		{
			if (resourceName == null)
			{
				MetadataArtifactLoaderCompositeResource.LoadAllResourcesFromAssembly(assembly, uriRegistry, loaders, resolver);
				return;
			}
			if (MetadataArtifactLoaderCompositeResource.AssemblyContainsResource(assembly, ref resourceName))
			{
				MetadataArtifactLoaderCompositeResource.CreateAndAddSingleResourceLoader(assembly, resourceName, uriRegistry, loaders);
				return;
			}
			throw EntityUtil.Metadata(Strings.UnableToLoadResource);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0007644C File Offset: 0x0007464C
		private static void LoadAllResourcesFromAssembly(Assembly assembly, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders, MetadataArtifactAssemblyResolver resolver)
		{
			string[] manifestResourceNamesForAssembly = MetadataArtifactLoaderCompositeResource.GetManifestResourceNamesForAssembly(assembly);
			foreach (string resourceName in manifestResourceNamesForAssembly)
			{
				MetadataArtifactLoaderCompositeResource.CreateAndAddSingleResourceLoader(assembly, resourceName, uriRegistry, loaders);
			}
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x00076480 File Offset: 0x00074680
		private static void CreateAndAddSingleResourceLoader(Assembly assembly, string resourceName, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders)
		{
			string item = MetadataArtifactLoaderCompositeResource.CreateResPath(assembly, resourceName);
			if (!uriRegistry.Contains(item))
			{
				loaders.Add(new MetadataArtifactLoaderResource(assembly, resourceName, uriRegistry));
			}
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000764AC File Offset: 0x000746AC
		internal static string CreateResPath(Assembly assembly, string resourceName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", new object[]
			{
				MetadataArtifactLoader.resPathPrefix,
				assembly.FullName,
				MetadataArtifactLoader.resPathSeparator,
				resourceName
			});
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000764ED File Offset: 0x000746ED
		internal static string[] GetManifestResourceNamesForAssembly(Assembly assembly)
		{
			if (assembly.IsDynamic)
			{
				return new string[0];
			}
			return assembly.GetManifestResourceNames();
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00076504 File Offset: 0x00074704
		private static Assembly ResolveAssemblyName(string assemblyName, MetadataArtifactAssemblyResolver resolver)
		{
			AssemblyName refernceName = new AssemblyName(assemblyName);
			Assembly result;
			if (!resolver.TryResolveAssemblyReference(refernceName, out result))
			{
				throw new FileNotFoundException(Strings.UnableToResolveAssembly(assemblyName));
			}
			return result;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x00076530 File Offset: 0x00074730
		internal static MetadataArtifactLoader CreateResourceLoader(string path, MetadataArtifactLoader.ExtensionCheck extensionCheck, string validExtension, ICollection<string> uriRegistry, MetadataArtifactAssemblyResolver resolver)
		{
			string text = null;
			string text2 = null;
			MetadataArtifactLoaderCompositeResource.ParseResourcePath(path, out text, out text2);
			bool flag = text != null && (text2 == null || text.Trim() == MetadataArtifactLoader.wildcard);
			MetadataArtifactLoaderCompositeResource.ValidateExtension(extensionCheck, validExtension, text2);
			if (flag)
			{
				return new MetadataArtifactLoaderCompositeResource(path, text, text2, uriRegistry, resolver);
			}
			Assembly assembly = MetadataArtifactLoaderCompositeResource.ResolveAssemblyName(text, resolver);
			return new MetadataArtifactLoaderResource(assembly, text2, uriRegistry);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x00076592 File Offset: 0x00074792
		private static void ValidateExtension(MetadataArtifactLoader.ExtensionCheck extensionCheck, string validExtension, string resourceName)
		{
			if (resourceName == null)
			{
				return;
			}
			if (extensionCheck == MetadataArtifactLoader.ExtensionCheck.Specific)
			{
				MetadataArtifactLoader.CheckArtifactExtension(resourceName, validExtension);
				return;
			}
			if (extensionCheck != MetadataArtifactLoader.ExtensionCheck.All)
			{
				return;
			}
			if (!MetadataArtifactLoader.IsValidArtifact(resourceName))
			{
				throw EntityUtil.Metadata(Strings.InvalidMetadataPath);
			}
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000765BC File Offset: 0x000747BC
		private static void ParseResourcePath(string path, out string assemblyName, out string resourceName)
		{
			int length = MetadataArtifactLoader.resPathPrefix.Length;
			string[] array = path.Substring(length).Split(new string[]
			{
				MetadataArtifactLoader.resPathSeparator,
				MetadataArtifactLoader.altPathSeparator
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 0 || array.Length > 2)
			{
				throw EntityUtil.Metadata(Strings.InvalidMetadataPath);
			}
			if (array.Length >= 1)
			{
				assemblyName = array[0];
			}
			else
			{
				assemblyName = null;
			}
			if (array.Length == 2)
			{
				resourceName = array[1];
				return;
			}
			resourceName = null;
		}

		// Token: 0x04000EC2 RID: 3778
		private readonly ReadOnlyCollection<MetadataArtifactLoaderResource> _children;

		// Token: 0x04000EC3 RID: 3779
		private readonly string _originalPath;
	}
}
