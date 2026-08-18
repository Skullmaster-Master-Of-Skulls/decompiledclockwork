using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006F1 RID: 1777
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public abstract class BaseChannelObjectWithProperties : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x000D872D File Offset: 0x000D772D
		public virtual IDictionary Properties
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
			get
			{
				return this;
			}
		}

		// Token: 0x17000AAF RID: 2735
		public virtual object this[object key]
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x000D873A File Offset: 0x000D773A
		public virtual ICollection Keys
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06003F6D RID: 16237 RVA: 0x000D8740 File Offset: 0x000D7740
		public virtual ICollection Values
		{
			get
			{
				ICollection keys = this.Keys;
				if (keys == null)
				{
					return null;
				}
				ArrayList arrayList = new ArrayList();
				foreach (object key in keys)
				{
					arrayList.Add(this[key]);
				}
				return arrayList;
			}
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x000D87AC File Offset: 0x000D77AC
		public virtual bool Contains(object key)
		{
			if (key == null)
			{
				return false;
			}
			ICollection keys = this.Keys;
			if (keys == null)
			{
				return false;
			}
			string text = key as string;
			foreach (object obj in keys)
			{
				if (text != null)
				{
					string text2 = obj as string;
					if (text2 != null)
					{
						if (string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return true;
						}
						continue;
					}
				}
				if (key.Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x000D8844 File Offset: 0x000D7844
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x000D8847 File Offset: 0x000D7847
		public virtual bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x000D884A File Offset: 0x000D784A
		public virtual void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x000D8851 File Offset: 0x000D7851
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x000D8858 File Offset: 0x000D7858
		public virtual void Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x000D885F File Offset: 0x000D785F
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x000D8867 File Offset: 0x000D7867
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06003F76 RID: 16246 RVA: 0x000D8870 File Offset: 0x000D7870
		public virtual int Count
		{
			get
			{
				ICollection keys = this.Keys;
				if (keys == null)
				{
					return 0;
				}
				return keys.Count;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x000D888F File Offset: 0x000D788F
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x000D8892 File Offset: 0x000D7892
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x000D8895 File Offset: 0x000D7895
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}
	}
}
