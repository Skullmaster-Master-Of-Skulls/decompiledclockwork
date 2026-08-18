using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Practices.Unity
{
	// Token: 0x020000A1 RID: 161
	internal class NamedTypesRegistry
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000957C File Offset: 0x0000777C
		public NamedTypesRegistry() : this(null)
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009585 File Offset: 0x00007785
		public NamedTypesRegistry(NamedTypesRegistry parent)
		{
			this.parent = parent;
			this.registeredKeys = new Dictionary<Type, List<string>>();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000959F File Offset: 0x0000779F
		public void RegisterType(Type t, string name)
		{
			if (!this.registeredKeys.ContainsKey(t))
			{
				this.registeredKeys[t] = new List<string>();
			}
			this.RemoveMatchingKeys(t, name);
			this.registeredKeys[t].Add(name);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000095DC File Offset: 0x000077DC
		public IEnumerable<string> GetKeys(Type t)
		{
			IEnumerable<string> enumerable = Enumerable.Empty<string>();
			if (this.parent != null)
			{
				enumerable = enumerable.Concat(this.parent.GetKeys(t));
			}
			if (this.registeredKeys.ContainsKey(t))
			{
				enumerable = enumerable.Concat(this.registeredKeys[t]);
			}
			return enumerable;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000962C File Offset: 0x0000782C
		public IEnumerable<Type> RegisteredTypes
		{
			get
			{
				return this.registeredKeys.Keys;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009639 File Offset: 0x00007839
		public void Clear()
		{
			this.registeredKeys.Clear();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000965C File Offset: 0x0000785C
		private void RemoveMatchingKeys(Type t, string name)
		{
			IEnumerable<string> source = from registeredName in this.registeredKeys[t]
			where registeredName != name
			select registeredName;
			this.registeredKeys[t] = source.ToList<string>();
		}

		// Token: 0x040000A1 RID: 161
		private readonly Dictionary<Type, List<string>> registeredKeys;

		// Token: 0x040000A2 RID: 162
		private readonly NamedTypesRegistry parent;
	}
}
