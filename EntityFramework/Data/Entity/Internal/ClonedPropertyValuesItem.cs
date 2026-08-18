using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000771 RID: 1905
	internal class ClonedPropertyValuesItem : IPropertyValuesItem
	{
		// Token: 0x0600565C RID: 22108 RVA: 0x001763B6 File Offset: 0x001745B6
		public ClonedPropertyValuesItem(string name, object value, Type type, bool isComplex)
		{
			this._name = name;
			this._type = type;
			this._isComplex = isComplex;
			this.Value = value;
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x0600565D RID: 22109 RVA: 0x001763DB File Offset: 0x001745DB
		// (set) Token: 0x0600565E RID: 22110 RVA: 0x001763E3 File Offset: 0x001745E3
		public object Value { get; set; }

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x0600565F RID: 22111 RVA: 0x001763EC File Offset: 0x001745EC
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06005660 RID: 22112 RVA: 0x001763F4 File Offset: 0x001745F4
		public bool IsComplex
		{
			get
			{
				return this._isComplex;
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06005661 RID: 22113 RVA: 0x001763FC File Offset: 0x001745FC
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x040022F7 RID: 8951
		private readonly string _name;

		// Token: 0x040022F8 RID: 8952
		private readonly bool _isComplex;

		// Token: 0x040022F9 RID: 8953
		private readonly Type _type;
	}
}
