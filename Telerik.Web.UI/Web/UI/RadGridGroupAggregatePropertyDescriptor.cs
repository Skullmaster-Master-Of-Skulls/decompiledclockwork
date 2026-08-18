using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000B7C RID: 2940
	internal class RadGridGroupAggregatePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06006F0D RID: 28429 RVA: 0x0019BD0B File Offset: 0x00199F0B
		public RadGridGroupAggregatePropertyDescriptor(string propertyName, PropertyDescriptor originalProperty) : base(propertyName, null)
		{
			this.isReadOnly = originalProperty.IsReadOnly;
			this.dataType = originalProperty.PropertyType;
		}

		// Token: 0x06006F0E RID: 28430 RVA: 0x0019BD2D File Offset: 0x00199F2D
		public void Initialize(GridGroupAggregateObject owner)
		{
			this._owner = owner;
		}

		// Token: 0x06006F0F RID: 28431 RVA: 0x0019BD36 File Offset: 0x00199F36
		public RadGridGroupAggregatePropertyDescriptor(string propertyName, bool ReadOnly, Type PropertyType) : base(propertyName, null)
		{
			this.isReadOnly = ReadOnly;
			this.dataType = PropertyType;
		}

		// Token: 0x06006F10 RID: 28432 RVA: 0x0019BD4E File Offset: 0x00199F4E
		public override bool CanResetValue(object component)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x1700246D RID: 9325
		// (get) Token: 0x06006F11 RID: 28433 RVA: 0x0019BD5A File Offset: 0x00199F5A
		public override Type ComponentType
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		// Token: 0x06006F12 RID: 28434 RVA: 0x0019BD66 File Offset: 0x00199F66
		public override object GetValue(object component)
		{
			if (this._owner != null)
			{
				return this._owner.GetPropertyValue(this.Name);
			}
			return DBNull.Value;
		}

		// Token: 0x1700246E RID: 9326
		// (get) Token: 0x06006F13 RID: 28435 RVA: 0x0019BD87 File Offset: 0x00199F87
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x1700246F RID: 9327
		// (get) Token: 0x06006F14 RID: 28436 RVA: 0x0019BD8F File Offset: 0x00199F8F
		public override Type PropertyType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x06006F15 RID: 28437 RVA: 0x0019BD97 File Offset: 0x00199F97
		public override void ResetValue(object component)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06006F16 RID: 28438 RVA: 0x0019BDA3 File Offset: 0x00199FA3
		public override void SetValue(object component, object value)
		{
			throw new Exception("No value can be set to the property");
		}

		// Token: 0x06006F17 RID: 28439 RVA: 0x0019BDAF File Offset: 0x00199FAF
		public override bool ShouldSerializeValue(object component)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x04001DF5 RID: 7669
		private bool isReadOnly;

		// Token: 0x04001DF6 RID: 7670
		private Type dataType;

		// Token: 0x04001DF7 RID: 7671
		private GridGroupAggregateObject _owner;
	}
}
