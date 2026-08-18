using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020011F6 RID: 4598
	public class TreeListTextBoxColumnEditor : TreeListColumnEditor
	{
		// Token: 0x0600BDBB RID: 48571 RVA: 0x002A07A3 File Offset: 0x0029E9A3
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TreeListTextBoxColumnEditor(TreeListEditableColumn column) : base(column)
		{
			this.InitializeTextBox();
		}

		// Token: 0x0600BDBC RID: 48572 RVA: 0x002A07B2 File Offset: 0x0029E9B2
		public override void Initialize(TreeListEditableItem editItem, Control container)
		{
			this.TextBoxControl.ID = this.GenerateControlID();
			container.Controls.Add(this.TextBoxControl);
		}

		// Token: 0x17003D32 RID: 15666
		// (get) Token: 0x0600BDBD RID: 48573 RVA: 0x002A07D6 File Offset: 0x0029E9D6
		// (set) Token: 0x0600BDBE RID: 48574 RVA: 0x002A07DE File Offset: 0x0029E9DE
		public TextBox TextBoxControl { get; private set; }

		// Token: 0x0600BDBF RID: 48575 RVA: 0x002A07E7 File Offset: 0x0029E9E7
		protected virtual void InitializeTextBox()
		{
			this.TextBoxControl = new TextBox();
			if (base.Column.Owner.ResolvedRenderMode == RenderMode.Mobile)
			{
				this.TextBoxControl.CssClass = "rtlValue";
			}
		}

		// Token: 0x0600BDC0 RID: 48576 RVA: 0x002A0818 File Offset: 0x0029EA18
		public override void SetValues(IEnumerable values)
		{
			object firstValueFromEnumerable = TreeListColumnEditor.GetFirstValueFromEnumerable(values);
			if (string.IsNullOrWhiteSpace(base.Column.DefaultInsertValue) && (this.TextBoxControl.NamingContainer is TreeListEditFormInsertItem || this.TextBoxControl.NamingContainer is TreeListDataInsertItem) && (base.Column.Owner.IsUsingModelBinding || !string.IsNullOrWhiteSpace(base.Column.Owner.ItemType)))
			{
				this.TextBoxControl.Text = string.Empty;
				return;
			}
			this.TextBoxControl.Text = ((firstValueFromEnumerable != null) ? firstValueFromEnumerable.ToString() : string.Empty);
		}

		// Token: 0x0600BDC1 RID: 48577 RVA: 0x002A09EC File Offset: 0x0029EBEC
		public override IEnumerable GetValues()
		{
			string value = this.TextBoxControl.Text;
			TreeListBoundColumn boundColumn = base.Column as TreeListBoundColumn;
			if (string.IsNullOrEmpty(value) && boundColumn != null && boundColumn.ConvertEmptyStringToNull)
			{
				yield return null;
			}
			yield return value;
			yield break;
		}
	}
}
