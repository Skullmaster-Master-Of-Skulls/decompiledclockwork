using System;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001AD RID: 429
	internal class NavigationPropertyAccessor
	{
		// Token: 0x06001EB3 RID: 7859 RVA: 0x0006C744 File Offset: 0x0006A944
		public NavigationPropertyAccessor(string propertyName)
		{
			this._propertyName = propertyName;
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x0006C753 File Offset: 0x0006A953
		public bool HasProperty
		{
			get
			{
				return this._propertyName != null;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x0006C75E File Offset: 0x0006A95E
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x0006C766 File Offset: 0x0006A966
		// (set) Token: 0x06001EB7 RID: 7863 RVA: 0x0006C76E File Offset: 0x0006A96E
		public Func<object, object> ValueGetter
		{
			get
			{
				return this._memberGetter;
			}
			set
			{
				Interlocked.CompareExchange<Func<object, object>>(ref this._memberGetter, value, null);
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x0006C77E File Offset: 0x0006A97E
		// (set) Token: 0x06001EB9 RID: 7865 RVA: 0x0006C786 File Offset: 0x0006A986
		public Action<object, object> ValueSetter
		{
			get
			{
				return this._memberSetter;
			}
			set
			{
				Interlocked.CompareExchange<Action<object, object>>(ref this._memberSetter, value, null);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x0006C796 File Offset: 0x0006A996
		// (set) Token: 0x06001EBB RID: 7867 RVA: 0x0006C79E File Offset: 0x0006A99E
		public Action<object, object> CollectionAdd
		{
			get
			{
				return this._collectionAdd;
			}
			set
			{
				Interlocked.CompareExchange<Action<object, object>>(ref this._collectionAdd, value, null);
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x0006C7AE File Offset: 0x0006A9AE
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x0006C7B6 File Offset: 0x0006A9B6
		public Func<object, object, bool> CollectionRemove
		{
			get
			{
				return this._collectionRemove;
			}
			set
			{
				Interlocked.CompareExchange<Func<object, object, bool>>(ref this._collectionRemove, value, null);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x0006C7C6 File Offset: 0x0006A9C6
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x0006C7CE File Offset: 0x0006A9CE
		public Func<object> CollectionCreate
		{
			get
			{
				return this._collectionCreate;
			}
			set
			{
				Interlocked.CompareExchange<Func<object>>(ref this._collectionCreate, value, null);
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x0006C7DE File Offset: 0x0006A9DE
		public static NavigationPropertyAccessor NoNavigationProperty
		{
			get
			{
				return new NavigationPropertyAccessor(null);
			}
		}

		// Token: 0x04000CE0 RID: 3296
		private Func<object, object> _memberGetter;

		// Token: 0x04000CE1 RID: 3297
		private Action<object, object> _memberSetter;

		// Token: 0x04000CE2 RID: 3298
		private Action<object, object> _collectionAdd;

		// Token: 0x04000CE3 RID: 3299
		private Func<object, object, bool> _collectionRemove;

		// Token: 0x04000CE4 RID: 3300
		private Func<object> _collectionCreate;

		// Token: 0x04000CE5 RID: 3301
		private readonly string _propertyName;
	}
}
