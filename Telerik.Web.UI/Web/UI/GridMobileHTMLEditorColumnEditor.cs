using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000369 RID: 873
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	public class GridMobileHTMLEditorColumnEditor : GridMobileColumnEditorBase
	{
		// Token: 0x06001E0C RID: 7692 RVA: 0x0005D9D8 File Offset: 0x0005BBD8
		public GridMobileHTMLEditorColumnEditor()
		{
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x0005D9E0 File Offset: 0x0005BBE0
		public GridMobileHTMLEditorColumnEditor(GridHTMLEditorColumn owner) : base(owner)
		{
			this.owner = owner;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x0005D9F0 File Offset: 0x0005BBF0
		public override void SetOwner(IGridEditableColumn owner)
		{
			this.owner = (owner as GridHTMLEditorColumn);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x0005D9FE File Offset: 0x0005BBFE
		protected override void CreateControls()
		{
			base.CreateControls();
			base.TextBoxControl.TextMode = TextBoxMode.MultiLine;
		}

		// Token: 0x0400076D RID: 1901
		private GridHTMLEditorColumn owner;
	}
}
