using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x0200095B RID: 2395
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	public class TreeListMobileColumnEditorBase : TreeListColumnEditor
	{
		// Token: 0x06005B2A RID: 23338 RVA: 0x00115528 File Offset: 0x00113728
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TreeListMobileColumnEditorBase(TreeListEditableColumn column) : base(column)
		{
			this.InitializeMobileEditor();
		}

		// Token: 0x06005B2B RID: 23339 RVA: 0x00115537 File Offset: 0x00113737
		public override void Initialize(TreeListEditableItem editItem, Control container)
		{
			this.MobileEditor.ID = this.GenerateControlID();
			container.Controls.Add(this.MobileEditor);
		}

		// Token: 0x06005B2C RID: 23340 RVA: 0x0011555B File Offset: 0x0011375B
		protected virtual void InitializeMobileEditor()
		{
			this.MobileEditor = new TextBox();
		}

		// Token: 0x17001E13 RID: 7699
		// (get) Token: 0x06005B2D RID: 23341 RVA: 0x00115568 File Offset: 0x00113768
		// (set) Token: 0x06005B2E RID: 23342 RVA: 0x00115570 File Offset: 0x00113770
		public TextBox MobileEditor { get; set; }

		// Token: 0x06005B2F RID: 23343 RVA: 0x0011557C File Offset: 0x0011377C
		public override void SetValues(IEnumerable values)
		{
			object obj = TreeListColumnEditor.GetFirstValueFromEnumerable(values);
			if (string.IsNullOrWhiteSpace(base.Column.DefaultInsertValue) && (this.MobileEditor.NamingContainer is TreeListEditFormInsertItem || this.MobileEditor.NamingContainer is TreeListDataInsertItem) && (base.Column.Owner.IsUsingModelBinding || !string.IsNullOrWhiteSpace(base.Column.Owner.ItemType)))
			{
				obj = null;
			}
			if (obj != null && !(obj is DBNull))
			{
				this.MobileEditor.Text = obj.ToString();
				return;
			}
			this.MobileEditor.Text = string.Empty;
		}

		// Token: 0x06005B30 RID: 23344 RVA: 0x001156F4 File Offset: 0x001138F4
		public override IEnumerable GetValues()
		{
			yield return this.MobileEditor.Text;
			yield break;
		}
	}
}
