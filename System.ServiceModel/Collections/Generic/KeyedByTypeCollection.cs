using System;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace System.Collections.Generic
{
	// Token: 0x0200001F RID: 31
	[__DynamicallyInvokable]
	public class KeyedByTypeCollection<TItem> : KeyedCollection<Type, TItem>
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x000072D4 File Offset: 0x000054D4
		public KeyedByTypeCollection() : base(null, 4)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000072E0 File Offset: 0x000054E0
		public KeyedByTypeCollection(IEnumerable<TItem> items) : base(null, 4)
		{
			if (items == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("items");
			}
			foreach (TItem item in items)
			{
				base.Add(item);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007344 File Offset: 0x00005544
		public T Find<T>()
		{
			return this.Find<T>(false);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000734D File Offset: 0x0000554D
		public T Remove<T>()
		{
			return this.Find<T>(true);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007358 File Offset: 0x00005558
		private T Find<T>(bool remove)
		{
			for (int i = 0; i < base.Count; i++)
			{
				TItem titem = base[i];
				if (titem is T)
				{
					if (remove)
					{
						base.Remove(titem);
					}
					return (T)((object)titem);
				}
			}
			return default(T);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000073AB File Offset: 0x000055AB
		public Collection<T> FindAll<T>()
		{
			return this.FindAll<T>(false);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000073B4 File Offset: 0x000055B4
		public Collection<T> RemoveAll<T>()
		{
			return this.FindAll<T>(true);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000073C0 File Offset: 0x000055C0
		private Collection<T> FindAll<T>(bool remove)
		{
			Collection<T> collection = new Collection<T>();
			foreach (TItem titem in this)
			{
				if (titem is T)
				{
					collection.Add((T)((object)titem));
				}
			}
			if (remove)
			{
				foreach (T t in collection)
				{
					base.Remove((TItem)((object)t));
				}
			}
			return collection;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007470 File Offset: 0x00005670
		[__DynamicallyInvokable]
		protected override Type GetKeyForItem(TItem item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			return item.GetType();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007498 File Offset: 0x00005698
		[__DynamicallyInvokable]
		protected override void InsertItem(int index, TItem item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			if (base.Contains(item.GetType()))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("item", SR.GetString("DuplicateBehavior1", new object[]
				{
					item.GetType().FullName
				}));
			}
			base.InsertItem(index, item);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000750F File Offset: 0x0000570F
		[__DynamicallyInvokable]
		protected override void SetItem(int index, TItem item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.SetItem(index, item);
		}
	}
}
