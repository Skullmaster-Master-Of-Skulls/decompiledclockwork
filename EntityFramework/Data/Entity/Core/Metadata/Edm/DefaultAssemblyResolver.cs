using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B7 RID: 1207
	internal class DefaultAssemblyResolver : MetadataArtifactAssemblyResolver
	{
		// Token: 0x06002C74 RID: 11380 RVA: 0x000D8FFC File Offset: 0x000D71FC
		internal override bool TryResolveAssemblyReference(AssemblyName refernceName, out Assembly assembly)
		{
			assembly = this.ResolveAssembly(refernceName);
			return assembly != null;
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000D900F File Offset: 0x000D720F
		internal override IEnumerable<Assembly> GetWildcardAssemblies()
		{
			return DefaultAssemblyResolver.GetAllDiscoverableAssemblies();
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000D9018 File Offset: 0x000D7218
		internal virtual Assembly ResolveAssembly(AssemblyName referenceName)
		{
			Assembly assembly = null;
			foreach (Assembly assembly2 in DefaultAssemblyResolver.GetAlreadyLoadedNonSystemAssemblies())
			{
				if (AssemblyName.ReferenceMatchesDefinition(referenceName, new AssemblyName(assembly2.FullName)))
				{
					return assembly2;
				}
			}
			if (assembly == null)
			{
				assembly = MetadataAssemblyHelper.SafeLoadReferencedAssembly(referenceName);
				if (assembly != null)
				{
					return assembly;
				}
			}
			DefaultAssemblyResolver.TryFindWildcardAssemblyMatch(referenceName, out assembly);
			return assembly;
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000D90A0 File Offset: 0x000D72A0
		private static bool TryFindWildcardAssemblyMatch(AssemblyName referenceName, out Assembly assembly)
		{
			foreach (Assembly assembly2 in DefaultAssemblyResolver.GetAllDiscoverableAssemblies())
			{
				if (AssemblyName.ReferenceMatchesDefinition(referenceName, new AssemblyName(assembly2.FullName)))
				{
					assembly = assembly2;
					return true;
				}
			}
			assembly = null;
			return false;
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000D9120 File Offset: 0x000D7320
		private static IEnumerable<Assembly> GetAlreadyLoadedNonSystemAssemblies()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			return from a in assemblies
			where a != null && !MetadataAssemblyHelper.ShouldFilterAssembly(a)
			select a;
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000D9164 File Offset: 0x000D7364
		private static IEnumerable<Assembly> GetAllDiscoverableAssemblies()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			HashSet<Assembly> hashSet = new HashSet<Assembly>(DefaultAssemblyResolver.AssemblyComparer.Instance);
			foreach (Assembly item in DefaultAssemblyResolver.GetAlreadyLoadedNonSystemAssemblies())
			{
				hashSet.Add(item);
			}
			AspProxy aspProxy = new AspProxy();
			if (aspProxy.IsAspNetEnvironment())
			{
				if (aspProxy.HasBuildManagerType())
				{
					IEnumerable<Assembly> buildManagerReferencedAssemblies = aspProxy.GetBuildManagerReferencedAssemblies();
					if (buildManagerReferencedAssemblies != null)
					{
						foreach (Assembly assembly in buildManagerReferencedAssemblies)
						{
							if (!MetadataAssemblyHelper.ShouldFilterAssembly(assembly))
							{
								hashSet.Add(assembly);
							}
						}
					}
				}
				return from a in hashSet
				where a != null
				select a;
			}
			if (entryAssembly == null)
			{
				return hashSet;
			}
			hashSet.Add(entryAssembly);
			foreach (Assembly item2 in MetadataAssemblyHelper.GetNonSystemReferencedAssemblies(entryAssembly))
			{
				hashSet.Add(item2);
			}
			return hashSet;
		}

		// Token: 0x020004B8 RID: 1208
		internal sealed class AssemblyComparer : IEqualityComparer<Assembly>
		{
			// Token: 0x06002C7D RID: 11389 RVA: 0x000D92B8 File Offset: 0x000D74B8
			private AssemblyComparer()
			{
			}

			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000D92C0 File Offset: 0x000D74C0
			public static DefaultAssemblyResolver.AssemblyComparer Instance
			{
				get
				{
					return DefaultAssemblyResolver.AssemblyComparer._instance;
				}
			}

			// Token: 0x06002C7F RID: 11391 RVA: 0x000D92C8 File Offset: 0x000D74C8
			public bool Equals(Assembly x, Assembly y)
			{
				AssemblyName assemblyName = new AssemblyName(x.FullName);
				AssemblyName assemblyName2 = new AssemblyName(y.FullName);
				return object.ReferenceEquals(x, y) || (AssemblyName.ReferenceMatchesDefinition(assemblyName, assemblyName2) && AssemblyName.ReferenceMatchesDefinition(assemblyName2, assemblyName));
			}

			// Token: 0x06002C80 RID: 11392 RVA: 0x000D930A File Offset: 0x000D750A
			public int GetHashCode(Assembly assembly)
			{
				return assembly.FullName.GetHashCode();
			}

			// Token: 0x04001068 RID: 4200
			private static readonly DefaultAssemblyResolver.AssemblyComparer _instance = new DefaultAssemblyResolver.AssemblyComparer();
		}
	}
}
