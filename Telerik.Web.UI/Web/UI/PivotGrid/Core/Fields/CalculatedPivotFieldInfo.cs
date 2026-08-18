using System;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x020006B5 RID: 1717
	internal class CalculatedPivotFieldInfo : PropertyFieldInfo
	{
		// Token: 0x06003DD4 RID: 15828 RVA: 0x000C70A0 File Offset: 0x000C52A0
		public CalculatedPivotFieldInfo(CalculatedField calculatedField)
		{
			if (calculatedField == null)
			{
				throw new ArgumentNullException("calculatedField");
			}
			base.Name = calculatedField.Name;
			base.DataType = typeof(object);
			base.PreferredRole = FieldRoles.Value;
			base.AllowedRoles = FieldRoles.Value;
			base.AutoGenerateField = true;
			this.CalculatedField = calculatedField;
			base.DataType = typeof(object);
			if (string.IsNullOrEmpty(calculatedField.DisplayName))
			{
				base.DisplayName = calculatedField.Name;
				return;
			}
			base.DisplayName = calculatedField.DisplayName;
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x000C712F File Offset: 0x000C532F
		// (set) Token: 0x06003DD6 RID: 15830 RVA: 0x000C7137 File Offset: 0x000C5337
		internal CalculatedField CalculatedField { get; private set; }

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000C7140 File Offset: 0x000C5340
		public override object GetValue(object item)
		{
			return item;
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x000C7143 File Offset: 0x000C5343
		public override void SetValue(object item, object fieldValue)
		{
			throw new NotImplementedException();
		}
	}
}
