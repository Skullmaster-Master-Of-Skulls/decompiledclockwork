using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.ServiceModel
{
	// Token: 0x020000F3 RID: 243
	public sealed class ExtensionCollection<T> : SynchronizedCollection<IExtension<T>>, IExtensionCollection<T>, ICollection<IExtension<T>>, IEnumerable<IExtension<T>>, IEnumerable where T : IExtensibleObject<T>
	{
		// Token: 0x0600051D RID: 1309 RVA: 0x000181A1 File Offset: 0x000163A1
		public ExtensionCollection(T owner)
		{
			if (owner == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("owner");
			}
			this.owner = owner;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000181C8 File Offset: 0x000163C8
		public ExtensionCollection(T owner, object syncRoot) : base(syncRoot)
		{
			if (owner == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("owner");
			}
			this.owner = owner;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000181F0 File Offset: 0x000163F0
		bool ICollection<IExtension<!0>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000181F4 File Offset: 0x000163F4
		protected override void ClearItems()
		{
			object syncRoot = base.SyncRoot;
			lock (syncRoot)
			{
				IExtension<T>[] array = new IExtension<T>[base.Count];
				base.CopyTo(array, 0);
				base.ClearItems();
				foreach (IExtension<T> extension in array)
				{
					extension.Detach(this.owner);
				}
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00018270 File Offset: 0x00016470
		public E Find<E>()
		{
			List<IExtension<T>> items = base.Items;
			object syncRoot = base.SyncRoot;
			lock (syncRoot)
			{
				for (int i = base.Count - 1; i >= 0; i--)
				{
					IExtension<T> extension = items[i];
					if (extension is E)
					{
						return (E)((object)extension);
					}
				}
			}
			return default(E);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000182F0 File Offset: 0x000164F0
		public Collection<E> FindAll<E>()
		{
			Collection<E> collection = new Collection<E>();
			List<IExtension<T>> items = base.Items;
			object syncRoot = base.SyncRoot;
			lock (syncRoot)
			{
				for (int i = 0; i < items.Count; i++)
				{
					IExtension<T> extension = items[i];
					if (extension is E)
					{
						collection.Add((E)((object)extension));
					}
				}
			}
			return collection;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00018370 File Offset: 0x00016570
		protected override void InsertItem(int index, IExtension<T> item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			object syncRoot = base.SyncRoot;
			lock (syncRoot)
			{
				item.Attach(this.owner);
				base.InsertItem(index, item);
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000183D4 File Offset: 0x000165D4
		protected override void RemoveItem(int index)
		{
			object syncRoot = base.SyncRoot;
			lock (syncRoot)
			{
				base.Items[index].Detach(this.owner);
				base.RemoveItem(index);
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001842C File Offset: 0x0001662C
		protected override void SetItem(int index, IExtension<T> item)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCannotSetExtensionsByIndex")));
		}

		// Token: 0x04000A33 RID: 2611
		private T owner;
	}
}
