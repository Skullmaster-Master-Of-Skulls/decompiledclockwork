using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001162 RID: 4450
	internal class GridPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600B575 RID: 46453 RVA: 0x0027FA92 File Offset: 0x0027DC92
		public GridPropertyDescriptor(string propertyName, PropertyDescriptor originalProperty) : base(propertyName, null)
		{
			this.isReadOnly = originalProperty.IsReadOnly;
			this.dataType = originalProperty.PropertyType;
		}

		// Token: 0x0600B576 RID: 46454 RVA: 0x0027FAB4 File Offset: 0x0027DCB4
		public void Initialize(GridInsertionObject owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600B577 RID: 46455 RVA: 0x0027FABD File Offset: 0x0027DCBD
		public GridPropertyDescriptor(string propertyName, bool ReadOnly, Type PropertyType) : base(propertyName, null)
		{
			this.isReadOnly = ReadOnly;
			this.dataType = PropertyType;
		}

		// Token: 0x0600B578 RID: 46456 RVA: 0x0027FAD5 File Offset: 0x0027DCD5
		public override bool CanResetValue(object component)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x17003AAF RID: 15023
		// (get) Token: 0x0600B579 RID: 46457 RVA: 0x0027FAE1 File Offset: 0x0027DCE1
		public override Type ComponentType
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		// Token: 0x0600B57A RID: 46458 RVA: 0x0027FAED File Offset: 0x0027DCED
		public override object GetValue(object component)
		{
			if (this._owner != null)
			{
				return this._owner.GetPropertyValue(this.Name);
			}
			return DBNull.Value;
		}

		// Token: 0x17003AB0 RID: 15024
		// (get) Token: 0x0600B57B RID: 46459 RVA: 0x0027FB0E File Offset: 0x0027DD0E
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x17003AB1 RID: 15025
		// (get) Token: 0x0600B57C RID: 46460 RVA: 0x0027FB16 File Offset: 0x0027DD16
		public override Type PropertyType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x0600B57D RID: 46461 RVA: 0x0027FB1E File Offset: 0x0027DD1E
		public override void ResetValue(object component)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600B57E RID: 46462 RVA: 0x0027FB2A File Offset: 0x0027DD2A
		public override void SetValue(object component, object value)
		{
			throw new GridException("No value can be set to the property");
		}

		// Token: 0x0600B57F RID: 46463 RVA: 0x0027FB36 File Offset: 0x0027DD36
		public override bool ShouldSerializeValue(object component)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x04002FE5 RID: 12261
		private bool isReadOnly;

		// Token: 0x04002FE6 RID: 12262
		private Type dataType;

		// Token: 0x04002FE7 RID: 12263
		private GridInsertionObject _owner;
	}
}
