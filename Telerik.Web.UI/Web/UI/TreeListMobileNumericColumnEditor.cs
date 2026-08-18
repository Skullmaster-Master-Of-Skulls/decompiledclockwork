using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200095D RID: 2397
	[TelerikToolboxCategory("Data")]
	public class TreeListMobileNumericColumnEditor : TreeListMobileColumnEditorBase
	{
		// Token: 0x06005B35 RID: 23349 RVA: 0x001159CD File Offset: 0x00113BCD
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TreeListMobileNumericColumnEditor(TreeListNumericColumn column) : base(column)
		{
			this.InitializeMobileEditor();
		}

		// Token: 0x06005B36 RID: 23350 RVA: 0x001159DC File Offset: 0x00113BDC
		public override void Initialize(TreeListEditableItem editItem, Control container)
		{
			base.MobileEditor.ID = this.GenerateControlID();
			container.Controls.Add(base.MobileEditor);
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x00115A00 File Offset: 0x00113C00
		protected override void InitializeMobileEditor()
		{
			base.MobileEditor = new TextBox();
			base.MobileEditor.Attributes.Add("type", "number");
		}
	}
}
