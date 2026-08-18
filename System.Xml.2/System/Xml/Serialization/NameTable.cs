using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000166 RID: 358
	internal class NameTable : INameScope
	{
		// Token: 0x06001823 RID: 6179 RVA: 0x00069418 File Offset: 0x00067618
		internal void Add(XmlQualifiedName qname, object value)
		{
			this.Add(qname.Name, qname.Namespace, value);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00069430 File Offset: 0x00067630
		internal void Add(string name, string ns, object value)
		{
			NameKey key = new NameKey(name, ns);
			this.table.Add(key, value);
		}

		// Token: 0x1700052F RID: 1327
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

		// Token: 0x17000530 RID: 1328
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

		// Token: 0x17000531 RID: 1329
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

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x000694E1 File Offset: 0x000676E1
		internal ICollection Values
		{
			get
			{
				return this.table.Values;
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000694F0 File Offset: 0x000676F0
		internal Array ToArray(Type type)
		{
			Array array = Array.CreateInstance(type, this.table.Count);
			this.table.Values.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04000B2F RID: 2863
		private Hashtable table = new Hashtable();
	}
}
