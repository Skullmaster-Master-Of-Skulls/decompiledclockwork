using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000514 RID: 1300
	internal class KnownAssembliesSet
	{
		// Token: 0x060030FD RID: 12541 RVA: 0x000EA950 File Offset: 0x000E8B50
		internal KnownAssembliesSet()
		{
			this._assemblies = new Dictionary<Assembly, KnownAssemblyEntry>();
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000EA963 File Offset: 0x000E8B63
		internal KnownAssembliesSet(KnownAssembliesSet set)
		{
			this._assemblies = new Dictionary<Assembly, KnownAssemblyEntry>(set._assemblies);
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000EA97C File Offset: 0x000E8B7C
		internal virtual bool TryGetKnownAssembly(Assembly assembly, object loaderCookie, EdmItemCollection itemCollection, out KnownAssemblyEntry entry)
		{
			return this._assemblies.TryGetValue(assembly, out entry) && entry.HaveSeenInCompatibleContext(loaderCookie, itemCollection);
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000EA99F File Offset: 0x000E8B9F
		internal IEnumerable<Assembly> Assemblies
		{
			get
			{
				return this._assemblies.Keys;
			}
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000EA9C8 File Offset: 0x000E8BC8
		public IEnumerable<KnownAssemblyEntry> GetEntries(object loaderCookie, EdmItemCollection itemCollection)
		{
			return from e in this._assemblies.Values
			where e.HaveSeenInCompatibleContext(loaderCookie, itemCollection)
			select e;
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000EAA08 File Offset: 0x000E8C08
		internal bool Contains(Assembly assembly, object loaderCookie, EdmItemCollection itemCollection)
		{
			KnownAssemblyEntry knownAssemblyEntry;
			return this.TryGetKnownAssembly(assembly, loaderCookie, itemCollection, out knownAssemblyEntry);
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000EAA20 File Offset: 0x000E8C20
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

		// Token: 0x04001286 RID: 4742
		private readonly Dictionary<Assembly, KnownAssemblyEntry> _assemblies;
	}
}
