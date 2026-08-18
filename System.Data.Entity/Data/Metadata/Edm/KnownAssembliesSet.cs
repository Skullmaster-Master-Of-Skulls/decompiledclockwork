using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001B1 RID: 433
	internal class KnownAssembliesSet
	{
		// Token: 0x06001ED0 RID: 7888 RVA: 0x0006C98A File Offset: 0x0006AB8A
		internal KnownAssembliesSet()
		{
			this._assemblies = new Dictionary<Assembly, KnownAssemblyEntry>();
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x0006C99D File Offset: 0x0006AB9D
		internal KnownAssembliesSet(KnownAssembliesSet set)
		{
			this._assemblies = new Dictionary<Assembly, KnownAssemblyEntry>(set._assemblies);
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x0006C9B6 File Offset: 0x0006ABB6
		internal bool TryGetKnownAssembly(Assembly assembly, object loaderCookie, EdmItemCollection itemCollection, out KnownAssemblyEntry entry)
		{
			return this._assemblies.TryGetValue(assembly, out entry) && entry.HaveSeenInCompatibleContext(loaderCookie, itemCollection);
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x0006C9D9 File Offset: 0x0006ABD9
		internal IEnumerable<Assembly> Assemblies
		{
			get
			{
				return this._assemblies.Keys;
			}
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0006C9E8 File Offset: 0x0006ABE8
		public IEnumerable<KnownAssemblyEntry> GetEntries(object loaderCookie, EdmItemCollection itemCollection)
		{
			return from e in this._assemblies.Values
			where e.HaveSeenInCompatibleContext(loaderCookie, itemCollection)
			select e;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x0006CA28 File Offset: 0x0006AC28
		internal bool Contains(Assembly assembly, object loaderCookie, EdmItemCollection itemCollection)
		{
			KnownAssemblyEntry knownAssemblyEntry;
			return this.TryGetKnownAssembly(assembly, loaderCookie, itemCollection, out knownAssemblyEntry);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0006CA40 File Offset: 0x0006AC40
		internal void Add(Assembly assembly, KnownAssemblyEntry knownAssemblyEntry)
		{
			KnownAssemblyEntry knownAssemblyEntry2;
			if (this._assemblies.TryGetValue(assembly, out knownAssemblyEntry2))
			{
				this._assemblies[assembly] = knownAssemblyEntry;
				return;
			}
			this._assemblies.Add(assembly, knownAssemblyEntry);
		}

		// Token: 0x04000CE9 RID: 3305
		private Dictionary<Assembly, KnownAssemblyEntry> _assemblies;
	}
}
