using System;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x0200012E RID: 302
	internal class DbConnectionStringBuilderDescriptor : PropertyDescriptor
	{
		// Token: 0x060013E3 RID: 5091 RVA: 0x0023D688 File Offset: 0x0023CA88
		internal DbConnectionStringBuilderDescriptor(string propertyName, Type componentType, Type propertyType, bool isReadOnly, Attribute[] attributes) : base(propertyName, attributes)
		{
			this._componentType = componentType;
			this._propertyType = propertyType;
			this._isReadOnly = isReadOnly;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0023D6B8 File Offset: 0x0023CAB8
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x0023D6D8 File Offset: 0x0023CAD8
		internal bool RefreshOnChange
		{
			get
			{
				return this._refreshOnChange;
			}
			set
			{
				this._refreshOnChange = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0023D6F8 File Offset: 0x0023CAF8
		public override Type ComponentType
		{
			get
			{
				return this._componentType;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0023D718 File Offset: 0x0023CB18
		public override bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0023D738 File Offset: 0x0023CB38
		public override Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0023D758 File Offset: 0x0023CB58
		public override bool CanResetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			return dbConnectionStringBuilder != null && dbConnectionStringBuilder.ShouldSerialize(this.DisplayName);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0023D788 File Offset: 0x0023CB88
		public override object GetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			object result;
			if (dbConnectionStringBuilder != null && dbConnectionStringBuilder.TryGetValue(this.DisplayName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0023D7B8 File Offset: 0x0023CBB8
		public override void ResetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			if (dbConnectionStringBuilder != null)
			{
				dbConnectionStringBuilder.Remove(this.DisplayName);
				if (this.RefreshOnChange)
				{
					dbConnectionStringBuilder.ClearPropertyDescriptors();
				}
			}
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0023D7F8 File Offset: 0x0023CBF8
		public override void SetValue(object component, object value)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			if (dbConnectionStringBuilder != null)
			{
				if (typeof(string) == this.PropertyType && string.Empty.Equals(value))
				{
					value = null;
				}
				dbConnectionStringBuilder[this.DisplayName] = value;
				if (this.RefreshOnChange)
				{
					dbConnectionStringBuilder.ClearPropertyDescriptors();
				}
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0023D858 File Offset: 0x0023CC58
		public override bool ShouldSerializeValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			return dbConnectionStringBuilder != null && dbConnectionStringBuilder.ShouldSerialize(this.DisplayName);
		}

		// Token: 0x04000C34 RID: 3124
		private Type _componentType;

		// Token: 0x04000C35 RID: 3125
		private Type _propertyType;

		// Token: 0x04000C36 RID: 3126
		private bool _isReadOnly;

		// Token: 0x04000C37 RID: 3127
		private bool _refreshOnChange;
	}
}
