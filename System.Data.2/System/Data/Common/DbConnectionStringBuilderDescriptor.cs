using System;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x020002E8 RID: 744
	internal class DbConnectionStringBuilderDescriptor : PropertyDescriptor
	{
		// Token: 0x06002F24 RID: 12068 RVA: 0x0012A4E8 File Offset: 0x001298E8
		internal DbConnectionStringBuilderDescriptor(string propertyName, Type componentType, Type propertyType, bool isReadOnly, Attribute[] attributes) : base(propertyName, attributes)
		{
			this._componentType = componentType;
			this._propertyType = propertyType;
			this._isReadOnly = isReadOnly;
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x0012A514 File Offset: 0x00129914
		// (set) Token: 0x06002F26 RID: 12070 RVA: 0x0012A528 File Offset: 0x00129928
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

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x0012A53C File Offset: 0x0012993C
		public override Type ComponentType
		{
			get
			{
				return this._componentType;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002F28 RID: 12072 RVA: 0x0012A550 File Offset: 0x00129950
		public override bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x0012A564 File Offset: 0x00129964
		public override Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x0012A578 File Offset: 0x00129978
		public override bool CanResetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			return dbConnectionStringBuilder != null && dbConnectionStringBuilder.ShouldSerialize(this.DisplayName);
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x0012A5A0 File Offset: 0x001299A0
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

		// Token: 0x06002F2C RID: 12076 RVA: 0x0012A5CC File Offset: 0x001299CC
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

		// Token: 0x06002F2D RID: 12077 RVA: 0x0012A600 File Offset: 0x00129A00
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

		// Token: 0x06002F2E RID: 12078 RVA: 0x0012A65C File Offset: 0x00129A5C
		public override bool ShouldSerializeValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			return dbConnectionStringBuilder != null && dbConnectionStringBuilder.ShouldSerialize(this.DisplayName);
		}

		// Token: 0x04001CE0 RID: 7392
		private Type _componentType;

		// Token: 0x04001CE1 RID: 7393
		private Type _propertyType;

		// Token: 0x04001CE2 RID: 7394
		private bool _isReadOnly;

		// Token: 0x04001CE3 RID: 7395
		private bool _refreshOnChange;
	}
}
