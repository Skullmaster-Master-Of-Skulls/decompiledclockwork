using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002E0 RID: 736
	internal class NameTable : INameScope
	{
		// Token: 0x0600226C RID: 8812 RVA: 0x000A1054 File Offset: 0x000A0054
		internal void Add(XmlQualifiedName qname, object value)
		{
			this.Add(qname.Name, qname.Namespace, value);
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000A106C File Offset: 0x000A006C
		internal void Add(string name, string ns, object value)
		{
			NameKey key = new NameKey(name, ns);
			this.table.Add(key, value);
		}

		// Token: 0x1700086B RID: 2155
		internal object this[XmlQualifiedName qname]
		{
			get
			{
				return this.table[new NameKey(qname.Name, qname.Namespace)];
			}
			set
			{
				this.table[new NameKey(qname.Name, qname.Namespace)] = value;
			}
		}

		// Token: 0x1700086C RID: 2156
		internal object this[string name, string ns]
		{
			get
			{
				return this.table[new NameKey(name, ns)];
			}
			set
			{
				this.table[new NameKey(name, ns)] = value;
			}
		}

		// Token: 0x1700086D RID: 2157
		object INameScope.this[string name, string ns]
		{
			get
			{
				return this.table[new NameKey(name, ns)];
			}
			set
			{
				this.table[new NameKey(name, ns)] = value;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x000A111D File Offset: 0x000A011D
		internal ICollection Values
		{
			get
			{
				return this.table.Values;
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000A112C File Offset: 0x000A012C
		internal Array ToArray(Type type)
		{
			Array array = Array.CreateInstance(type, this.table.Count);
			this.table.Values.CopyTo(array, 0);
			return array;
		}

		// Token: 0x040014C3 RID: 5315
		private Hashtable table = new Hashtable();
	}
}
