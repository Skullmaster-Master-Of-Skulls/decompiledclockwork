using System;
using System.Collections;
using System.Runtime.Serialization;

namespace log4net.Util
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public sealed class PropertiesDictionary : ReadOnlyPropertiesDictionary, ISerializable, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x00018DD1 File Offset: 0x00016FD1
		public PropertiesDictionary()
		{
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00018DD9 File Offset: 0x00016FD9
		public PropertiesDictionary(ReadOnlyPropertiesDictionary propertiesDictionary) : base(propertiesDictionary)
		{
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00018DE2 File Offset: 0x00016FE2
		private PropertiesDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170001AD RID: 429
		public override object this[string key]
		{
			get
			{
				return base.InnerHashtable[key];
			}
			set
			{
				base.InnerHashtable[key] = value;
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00018E09 File Offset: 0x00017009
		public void Remove(string key)
		{
			base.InnerHashtable.Remove(key);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00018E17 File Offset: 0x00017017
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return base.InnerHashtable.GetEnumerator();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00018E24 File Offset: 0x00017024
		void IDictionary.Remove(object key)
		{
			base.InnerHashtable.Remove(key);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00018E32 File Offset: 0x00017032
		bool IDictionary.Contains(object key)
		{
			return base.InnerHashtable.Contains(key);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00018E40 File Offset: 0x00017040
		public override void Clear()
		{
			base.InnerHashtable.Clear();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00018E4D File Offset: 0x0001704D
		void IDictionary.Add(object key, object value)
		{
			if (!(key is string))
			{
				throw new ArgumentException("key must be a string", "key");
			}
			base.InnerHashtable.Add(key, value);
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00018E74 File Offset: 0x00017074
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001AF RID: 431
		object IDictionary.this[object key]
		{
			get
			{
				if (!(key is string))
				{
					throw new ArgumentException("key must be a string", "key");
				}
				return base.InnerHashtable[key];
			}
			set
			{
				if (!(key is string))
				{
					throw new ArgumentException("key must be a string", "key");
				}
				base.InnerHashtable[key] = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x00018EC4 File Offset: 0x000170C4
		ICollection IDictionary.Values
		{
			get
			{
				return base.InnerHashtable.Values;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00018ED1 File Offset: 0x000170D1
		ICollection IDictionary.Keys
		{
			get
			{
				return base.InnerHashtable.Keys;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00018EDE File Offset: 0x000170DE
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00018EE1 File Offset: 0x000170E1
		void ICollection.CopyTo(Array array, int index)
		{
			base.InnerHashtable.CopyTo(array, index);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00018EF0 File Offset: 0x000170F0
		bool ICollection.IsSynchronized
		{
			get
			{
				return base.InnerHashtable.IsSynchronized;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00018EFD File Offset: 0x000170FD
		object ICollection.SyncRoot
		{
			get
			{
				return base.InnerHashtable.SyncRoot;
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00018F0A File Offset: 0x0001710A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)base.InnerHashtable).GetEnumerator();
		}
	}
}
