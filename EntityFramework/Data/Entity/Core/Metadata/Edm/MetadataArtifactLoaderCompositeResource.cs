using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Resources;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000509 RID: 1289
	internal class MetadataArtifactLoaderCompositeResource : MetadataArtifactLoader
	{
		// Token: 0x0600303E RID: 12350 RVA: 0x000E78A3 File Offset: 0x000E5AA3
		internal MetadataArtifactLoaderCompositeResource(string originalPath, string assemblyName, string resourceName, ICollection<string> uriRegistry, MetadataArtifactAssemblyResolver resolver)
		{
			this._originalPath = originalPath;
			this._children = new ReadOnlyCollection<MetadataArtifactLoaderResource>(MetadataArtifactLoaderCompositeResource.LoadResources(assemblyName, resourceName, uriRegistry, resolver));
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x000E78C8 File Offset: 0x000E5AC8
		public override string Path
		{
			get
			{
				return this._originalPath;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x000E78D0 File Offset: 0x000E5AD0
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000E78D3 File Offset: 0x000E5AD3
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			return this.GetOriginalPaths();
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000E78DC File Offset: 0x000E5ADC
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000E7938 File Offset: 0x000E5B38
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetPaths());
			}
			return list;
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000E7994 File Offset: 0x000E5B94
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetReaders(sourceDictionary));
			}
			return list;
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000E79F0 File Offset: 0x000E5BF0
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.CreateReaders(spaceToGet));
			}
			return list;
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000E7A4C File Offset: 0x000E5C4C
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
							MetadataArtifactLoaderCompositeResource.LoadResourcesFromAssembly(assembly, resourceName, uriRegistry, list);
						}
					}
					goto IL_60;
				}
			}
			Assembly assembly2 = MetadataArtifactLoaderCompositeResource.ResolveAssemblyName(assemblyName, resolver);
			MetadataArtifactLoaderCompositeResource.LoadResourcesFromAssembly(assembly2, resourceName, uriRegistry, list);
			IL_60:
			if (resourceName != null && list.Count == 0)
			{
				throw new MetadataException(Strings.UnableToLoadResource);
			}
			return list;
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000E7AE0 File Offset: 0x000E5CE0
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

		// Token: 0x06003048 RID: 12360 RVA: 0x000E7B28 File Offset: 0x000E5D28
		private static void LoadResourcesFromAssembly(Assembly assembly, string resourceName, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders)
		{
			if (resourceName == null)
			{
				MetadataArtifactLoaderCompositeResource.LoadAllResourcesFromAssembly(assembly, uriRegistry, loaders);
				return;
			}
			if (MetadataArtifactLoaderCompositeResource.AssemblyContainsResource(assembly, ref resourceName))
			{
				MetadataArtifactLoaderCompositeResource.CreateAndAddSingleResourceLoader(assembly, resourceName, uriRegistry, loaders);
				return;
			}
			throw new MetadataException(Strings.UnableToLoadResource);
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000E7B54 File Offset: 0x000E5D54
		private static void LoadAllResourcesFromAssembly(Assembly assembly, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders)
		{
			string[] manifestResourceNamesForAssembly = MetadataArtifactLoaderCompositeResource.GetManifestResourceNamesForAssembly(assembly);
			foreach (string resourceName in manifestResourceNamesForAssembly)
			{
				MetadataArtifactLoaderCompositeResource.CreateAndAddSingleResourceLoader(assembly, resourceName, uriRegistry, loaders);
			}
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000E7B88 File Offset: 0x000E5D88
		private static void CreateAndAddSingleResourceLoader(Assembly assembly, string resourceName, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders)
		{
			string item = MetadataArtifactLoaderCompositeResource.CreateResPath(assembly, resourceName);
			if (!uriRegistry.Contains(item))
			{
				loaders.Add(new MetadataArtifactLoaderResource(assembly, resourceName, uriRegistry));
			}
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000E7BB4 File Offset: 0x000E5DB4
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

		// Token: 0x0600304C RID: 12364 RVA: 0x000E7BF7 File Offset: 0x000E5DF7
		internal static string[] GetManifestResourceNamesForAssembly(Assembly assembly)
		{
			if (assembly.IsDynamic)
			{
				return new string[0];
			}
			return assembly.GetManifestResourceNames();
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000E7C10 File Offset: 0x000E5E10
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

		// Token: 0x0600304E RID: 12366 RVA: 0x000E7C3C File Offset: 0x000E5E3C
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

		// Token: 0x0600304F RID: 12367 RVA: 0x000E7CA0 File Offset: 0x000E5EA0
		private static void ValidateExtension(MetadataArtifactLoader.ExtensionCheck extensionCheck, string validExtension, string resourceName)
		{
			if (resourceName == null)
			{
				return;
			}
			switch (extensionCheck)
			{
			case MetadataArtifactLoader.ExtensionCheck.Specific:
				MetadataArtifactLoader.CheckArtifactExtension(resourceName, validExtension);
				return;
			case MetadataArtifactLoader.ExtensionCheck.All:
				if (!MetadataArtifactLoader.IsValidArtifact(resourceName))
				{
					throw new MetadataException(Strings.InvalidMetadataPath);
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000E7CE0 File Offset: 0x000E5EE0
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
				throw new MetadataException(Strings.InvalidMetadataPath);
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

		// Token: 0x0400125A RID: 4698
		private readonly ReadOnlyCollection<MetadataArtifactLoaderResource> _children;

		// Token: 0x0400125B RID: 4699
		private readonly string _originalPath;
	}
}
