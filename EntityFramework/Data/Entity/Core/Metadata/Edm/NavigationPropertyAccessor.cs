using System;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F2 RID: 1266
	internal class NavigationPropertyAccessor
	{
		// Token: 0x06002F1A RID: 12058 RVA: 0x000E0E0B File Offset: 0x000DF00B
		public NavigationPropertyAccessor(string propertyName)
		{
			this._propertyName = propertyName;
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002F1B RID: 12059 RVA: 0x000E0E1A File Offset: 0x000DF01A
		public bool HasProperty
		{
			get
			{
				return this._propertyName != null;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x000E0E28 File Offset: 0x000DF028
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x000E0E30 File Offset: 0x000DF030
		// (set) Token: 0x06002F1E RID: 12062 RVA: 0x000E0E38 File Offset: 0x000DF038
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

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x000E0E48 File Offset: 0x000DF048
		// (set) Token: 0x06002F20 RID: 12064 RVA: 0x000E0E50 File Offset: 0x000DF050
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

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002F21 RID: 12065 RVA: 0x000E0E60 File Offset: 0x000DF060
		// (set) Token: 0x06002F22 RID: 12066 RVA: 0x000E0E68 File Offset: 0x000DF068
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

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000E0E78 File Offset: 0x000DF078
		// (set) Token: 0x06002F24 RID: 12068 RVA: 0x000E0E80 File Offset: 0x000DF080
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

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x000E0E90 File Offset: 0x000DF090
		// (set) Token: 0x06002F26 RID: 12070 RVA: 0x000E0E98 File Offset: 0x000DF098
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

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x000E0EA8 File Offset: 0x000DF0A8
		public static NavigationPropertyAccessor NoNavigationProperty
		{
			get
			{
				return new NavigationPropertyAccessor(null);
			}
		}

		// Token: 0x040011E2 RID: 4578
		private Func<object, object> _memberGetter;

		// Token: 0x040011E3 RID: 4579
		private Action<object, object> _memberSetter;

		// Token: 0x040011E4 RID: 4580
		private Action<object, object> _collectionAdd;

		// Token: 0x040011E5 RID: 4581
		private Func<object, object, bool> _collectionRemove;

		// Token: 0x040011E6 RID: 4582
		private Func<object> _collectionCreate;

		// Token: 0x040011E7 RID: 4583
		private readonly string _propertyName;
	}
}
