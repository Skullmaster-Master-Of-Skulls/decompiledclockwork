using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020011F3 RID: 4595
	public class TreeListNumericColumnEditor : TreeListColumnEditor
	{
		// Token: 0x0600BDA5 RID: 48549 RVA: 0x002A01C3 File Offset: 0x0029E3C3
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TreeListNumericColumnEditor(TreeListEditableColumn column) : base(column)
		{
			this.InitializeNumericTextBox();
		}

		// Token: 0x0600BDA6 RID: 48550 RVA: 0x002A01D4 File Offset: 0x0029E3D4
		public override void Initialize(TreeListEditableItem editItem, Control container)
		{
			this.NumericTextBoxControl.RenderMode = base.Column.Owner.RenderMode;
			this.NumericTextBoxControl.ID = this.GenerateControlID();
			TreeListNumericColumn treeListNumericColumn = base.Column as TreeListNumericColumn;
			this.NumericTextBoxControl.Type = treeListNumericColumn.NumericType;
			this.NumericTextBoxControl.NumberFormat.AllowRounding = treeListNumericColumn.AllowRounding;
			this.NumericTextBoxControl.NumberFormat.KeepNotRoundedValue = treeListNumericColumn.KeepNotRoundedValue;
			this.NumericTextBoxControl.NumberFormat.DecimalDigits = treeListNumericColumn.DecimalDigits;
			this.NumericTextBoxControl.EnableEmbeddedSkins = treeListNumericColumn.Owner.EnableEmbeddedSkins;
			this.NumericTextBoxControl.EnableAriaSupport = treeListNumericColumn.Owner.EnableAriaSupport;
			this.NumericTextBoxControl.DataType = treeListNumericColumn.DataType;
			container.Controls.Add(this.NumericTextBoxControl);
		}

		// Token: 0x0600BDA7 RID: 48551 RVA: 0x002A02BA File Offset: 0x0029E4BA
		protected virtual void InitializeNumericTextBox()
		{
			this.NumericTextBoxControl = new RadNumericTextBox();
		}

		// Token: 0x17003D2E RID: 15662
		// (get) Token: 0x0600BDA8 RID: 48552 RVA: 0x002A02C7 File Offset: 0x0029E4C7
		// (set) Token: 0x0600BDA9 RID: 48553 RVA: 0x002A02CF File Offset: 0x0029E4CF
		public RadNumericTextBox NumericTextBoxControl { get; private set; }

		// Token: 0x0600BDAA RID: 48554 RVA: 0x002A02D8 File Offset: 0x0029E4D8
		public override void SetValues(IEnumerable values)
		{
			object obj = TreeListColumnEditor.GetFirstValueFromEnumerable(values);
			if (string.IsNullOrWhiteSpace(base.Column.DefaultInsertValue) && (this.NumericTextBoxControl.NamingContainer is TreeListEditFormInsertItem || this.NumericTextBoxControl.NamingContainer is TreeListDataInsertItem) && (base.Column.Owner.IsUsingModelBinding || !string.IsNullOrWhiteSpace(base.Column.Owner.ItemType)))
			{
				obj = null;
			}
			if (obj == null || obj is DBNull)
			{
				this.NumericTextBoxControl.DbValue = obj;
				return;
			}
			if (obj is double?)
			{
				this.NumericTextBoxControl.DbValue = (double)obj;
				return;
			}
			if (obj is IConvertible)
			{
				this.NumericTextBoxControl.DbValue = Convert.ToDouble(obj);
				return;
			}
			this.NumericTextBoxControl.DbValue = double.Parse(obj.ToString());
		}

		// Token: 0x0600BDAB RID: 48555 RVA: 0x002A0494 File Offset: 0x0029E694
		public override IEnumerable GetValues()
		{
			yield return this.NumericTextBoxControl.DbValue;
			yield break;
		}
	}
}
