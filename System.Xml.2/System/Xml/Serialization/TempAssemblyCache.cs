using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200013E RID: 318
	internal class TempAssemblyCache
	{
		// Token: 0x170004AB RID: 1195
		internal TempAssembly this[string ns, object o]
		{
			get
			{
				return (TempAssembly)this.cache[new TempAssemblyCacheKey(ns, o)];
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00065B2C File Offset: 0x00063D2C
		internal void Add(string ns, object o, TempAssembly assembly)
		{
			TempAssemblyCacheKey key = new TempAssemblyCacheKey(ns, o);
			lock (this)
			{
				if (this.cache[key] != assembly)
				{
					Hashtable hashtable = new Hashtable();
					foreach (object key2 in this.cache.Keys)
					{
						hashtable.Add(key2, this.cache[key2]);
					}
					this.cache = hashtable;
					this.cache[key] = assembly;
				}
			}
		}

		// Token: 0x04000AAC RID: 2732
		private Hashtable cache = new Hashtable();
	}
}
