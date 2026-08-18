using System;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x020002F3 RID: 755
	public abstract class PropertyEntry
	{
		// Token: 0x060022FC RID: 8956 RVA: 0x000030B5 File Offset: 0x000012B5
		internal PropertyEntry()
		{
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x0007208B File Offset: 0x0007028B
		// (set) Token: 0x060022FE RID: 8958 RVA: 0x00072093 File Offset: 0x00070293
		public string Filter
		{
			get
			{
				return this._filter;
			}
			set
			{
				this._filter = value;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x0007209C File Offset: 0x0007029C
		// (set) Token: 0x06002300 RID: 8960 RVA: 0x000720A4 File Offset: 0x000702A4
		internal int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x000720AD File Offset: 0x000702AD
		// (set) Token: 0x06002302 RID: 8962 RVA: 0x000720B5 File Offset: 0x000702B5
		internal int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x000720BE File Offset: 0x000702BE
		// (set) Token: 0x06002304 RID: 8964 RVA: 0x000720C6 File Offset: 0x000702C6
		public PropertyInfo PropertyInfo
		{
			get
			{
				return this._propertyInfo;
			}
			set
			{
				this._propertyInfo = value;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x000720CF File Offset: 0x000702CF
		// (set) Token: 0x06002306 RID: 8966 RVA: 0x000720D7 File Offset: 0x000702D7
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x000720E0 File Offset: 0x000702E0
		// (set) Token: 0x06002308 RID: 8968 RVA: 0x000720E8 File Offset: 0x000702E8
		public Type Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x000720F1 File Offset: 0x000702F1
		public Type DeclaringType
		{
			get
			{
				if (this._propertyInfo == null)
				{
					return null;
				}
				return this._propertyInfo.DeclaringType;
			}
		}

		// Token: 0x04001C93 RID: 7315
		private string _filter;

		// Token: 0x04001C94 RID: 7316
		private PropertyInfo _propertyInfo;

		// Token: 0x04001C95 RID: 7317
		private string _name;

		// Token: 0x04001C96 RID: 7318
		private Type _type;

		// Token: 0x04001C97 RID: 7319
		private int _index;

		// Token: 0x04001C98 RID: 7320
		private int _order;
	}
}
