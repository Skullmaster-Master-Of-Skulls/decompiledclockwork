using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020011F4 RID: 4596
	public class TreeListCheckBoxColumnEditor : TreeListColumnEditor
	{
		// Token: 0x0600BDAC RID: 48556 RVA: 0x002A04B1 File Offset: 0x0029E6B1
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TreeListCheckBoxColumnEditor(TreeListEditableColumn column) : base(column)
		{
			this.InitializeCheckBox();
		}

		// Token: 0x0600BDAD RID: 48557 RVA: 0x002A04C0 File Offset: 0x0029E6C0
		public override void Initialize(TreeListEditableItem editItem, Control container)
		{
			this.CheckBoxControl.ID = this.GenerateControlID();
			container.Controls.Add(this.CheckBoxControl);
		}

		// Token: 0x0600BDAE RID: 48558 RVA: 0x002A04E4 File Offset: 0x0029E6E4
		protected virtual void InitializeCheckBox()
		{
			this.CheckBoxControl = new CheckBox();
		}

		// Token: 0x17003D2F RID: 15663
		// (get) Token: 0x0600BDAF RID: 48559 RVA: 0x002A04F1 File Offset: 0x0029E6F1
		// (set) Token: 0x0600BDB0 RID: 48560 RVA: 0x002A04F9 File Offset: 0x0029E6F9
		public CheckBox CheckBoxControl { get; private set; }

		// Token: 0x0600BDB1 RID: 48561 RVA: 0x002A0504 File Offset: 0x0029E704
		public override void SetValues(IEnumerable values)
		{
			object obj = TreeListColumnEditor.GetFirstValueFromEnumerable(values);
			if (string.IsNullOrWhiteSpace(base.Column.DefaultInsertValue) && (this.CheckBoxControl.NamingContainer is TreeListEditFormInsertItem || this.CheckBoxControl.NamingContainer is TreeListDataInsertItem) && (base.Column.Owner.IsUsingModelBinding || !string.IsNullOrWhiteSpace(base.Column.Owner.ItemType)))
			{
				obj = null;
			}
			if (obj == null)
			{
				this.CheckBoxControl.Checked = false;
				return;
			}
			if (obj is bool)
			{
				this.CheckBoxControl.Checked = (bool)obj;
				return;
			}
			if (obj is IConvertible)
			{
				this.CheckBoxControl.Checked = Convert.ToBoolean(obj);
				return;
			}
			this.CheckBoxControl.Checked = bool.Parse(obj.ToString());
		}

		// Token: 0x0600BDB2 RID: 48562 RVA: 0x002A06AC File Offset: 0x0029E8AC
		public override IEnumerable GetValues()
		{
			yield return this.CheckBoxControl.Checked;
			yield break;
		}
	}
}
