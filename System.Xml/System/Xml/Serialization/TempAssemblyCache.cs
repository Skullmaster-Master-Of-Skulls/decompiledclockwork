using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002B7 RID: 695
	internal class TempAssemblyCache
	{
		// Token: 0x170007EE RID: 2030
		internal TempAssembly this[string ns, object o]
		{
			get
			{
				return (TempAssembly)this.cache[new TempAssemblyCacheKey(ns, o)];
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0009D8CC File Offset: 0x0009C8CC
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

		// Token: 0x04001447 RID: 5191
		private Hashtable cache = new Hashtable();
	}
}
