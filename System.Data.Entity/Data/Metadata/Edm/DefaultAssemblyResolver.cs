using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000210 RID: 528
	internal class DefaultAssemblyResolver : MetadataArtifactAssemblyResolver
	{
		// Token: 0x060022F2 RID: 8946 RVA: 0x0007C31C File Offset: 0x0007A51C
		internal override bool TryResolveAssemblyReference(AssemblyName refernceName, out Assembly assembly)
		{
			assembly = this.ResolveAssembly(refernceName);
			return assembly != null;
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x0007C32F File Offset: 0x0007A52F
		internal override IEnumerable<Assembly> GetWildcardAssemblies()
		{
			return DefaultAssemblyResolver.GetAllDiscoverableAssemblies();
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x0007C338 File Offset: 0x0007A538
		internal Assembly ResolveAssembly(AssemblyName referenceName)
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
			this.TryFindWildcardAssemblyMatch(referenceName, out assembly);
			return assembly;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x0007C3C0 File Offset: 0x0007A5C0
		private bool TryFindWildcardAssemblyMatch(AssemblyName referenceName, out Assembly assembly)
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

		// Token: 0x060022F6 RID: 8950 RVA: 0x0007C428 File Offset: 0x0007A628
		private static IEnumerable<Assembly> GetAlreadyLoadedNonSystemAssemblies()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			return from a in assemblies
			where a != null && !MetadataAssemblyHelper.ShouldFilterAssembly(a)
			select a;
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x0007C468 File Offset: 0x0007A668
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

		// Token: 0x02000543 RID: 1347
		internal sealed class AssemblyComparer : IEqualityComparer<Assembly>
		{
			// Token: 0x06003ECE RID: 16078 RVA: 0x00002050 File Offset: 0x00000250
			private AssemblyComparer()
			{
			}

			// Token: 0x17000B30 RID: 2864
			// (get) Token: 0x06003ECF RID: 16079 RVA: 0x000E950B File Offset: 0x000E770B
			public static DefaultAssemblyResolver.AssemblyComparer Instance
			{
				get
				{
					return DefaultAssemblyResolver.AssemblyComparer._instance;
				}
			}

			// Token: 0x06003ED0 RID: 16080 RVA: 0x000E9514 File Offset: 0x000E7714
			public bool Equals(Assembly x, Assembly y)
			{
				AssemblyName assemblyName = new AssemblyName(x.FullName);
				AssemblyName assemblyName2 = new AssemblyName(y.FullName);
				return x == y || (AssemblyName.ReferenceMatchesDefinition(assemblyName, assemblyName2) && AssemblyName.ReferenceMatchesDefinition(assemblyName2, assemblyName));
			}

			// Token: 0x06003ED1 RID: 16081 RVA: 0x000E9551 File Offset: 0x000E7751
			public int GetHashCode(Assembly assembly)
			{
				return assembly.FullName.GetHashCode();
			}

			// Token: 0x04001BD3 RID: 7123
			private static DefaultAssemblyResolver.AssemblyComparer _instance = new DefaultAssemblyResolver.AssemblyComparer();
		}
	}
}
