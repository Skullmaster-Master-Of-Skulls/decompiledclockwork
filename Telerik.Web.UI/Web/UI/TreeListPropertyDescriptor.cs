using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001271 RID: 4721
	internal class TreeListPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600C470 RID: 50288 RVA: 0x002BF527 File Offset: 0x002BD727
		public TreeListPropertyDescriptor(string propertyName, PropertyDescriptor originalProperty) : base(propertyName, null)
		{
			this.isReadOnly = originalProperty.IsReadOnly;
			this.dataType = originalProperty.PropertyType;
		}

		// Token: 0x0600C471 RID: 50289 RVA: 0x002BF549 File Offset: 0x002BD749
		public void Initialize(TreeListInsertionObject owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600C472 RID: 50290 RVA: 0x002BF552 File Offset: 0x002BD752
		public TreeListPropertyDescriptor(string propertyName, bool ReadOnly, Type PropertyType) : base(propertyName, null)
		{
			this.isReadOnly = ReadOnly;
			this.dataType = PropertyType;
		}

		// Token: 0x0600C473 RID: 50291 RVA: 0x002BF56A File Offset: 0x002BD76A
		public override bool CanResetValue(object component)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x17003F48 RID: 16200
		// (get) Token: 0x0600C474 RID: 50292 RVA: 0x002BF576 File Offset: 0x002BD776
		public override Type ComponentType
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		// Token: 0x0600C475 RID: 50293 RVA: 0x002BF582 File Offset: 0x002BD782
		public override object GetValue(object component)
		{
			if (this._owner != null)
			{
				return this._owner.GetPropertyValue(this.Name);
			}
			return DBNull.Value;
		}

		// Token: 0x17003F49 RID: 16201
		// (get) Token: 0x0600C476 RID: 50294 RVA: 0x002BF5A3 File Offset: 0x002BD7A3
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x17003F4A RID: 16202
		// (get) Token: 0x0600C477 RID: 50295 RVA: 0x002BF5AB File Offset: 0x002BD7AB
		public override Type PropertyType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x0600C478 RID: 50296 RVA: 0x002BF5B3 File Offset: 0x002BD7B3
		public override void ResetValue(object component)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600C479 RID: 50297 RVA: 0x002BF5BF File Offset: 0x002BD7BF
		public override void SetValue(object component, object value)
		{
			throw new Exception("No value can be set to the property");
		}

		// Token: 0x0600C47A RID: 50298 RVA: 0x002BF5CB File Offset: 0x002BD7CB
		public override bool ShouldSerializeValue(object component)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x04003411 RID: 13329
		private bool isReadOnly;

		// Token: 0x04003412 RID: 13330
		private Type dataType;

		// Token: 0x04003413 RID: 13331
		private TreeListInsertionObject _owner;
	}
}
