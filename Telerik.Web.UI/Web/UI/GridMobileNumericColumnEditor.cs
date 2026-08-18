using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200036B RID: 875
	[TelerikToolboxCategory("Data")]
	public class GridMobileNumericColumnEditor : GridMobileColumnEditorBase
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x0005DBFB File Offset: 0x0005BDFB
		public GridMobileNumericColumnEditor()
		{
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x0005DC03 File Offset: 0x0005BE03
		public GridMobileNumericColumnEditor(GridNumericColumn owner) : base(owner)
		{
			this.owner = owner;
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0005DC13 File Offset: 0x0005BE13
		public override void SetOwner(IGridEditableColumn owner)
		{
			this.owner = (owner as GridNumericColumn);
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0005DC21 File Offset: 0x0005BE21
		protected override void CreateControls()
		{
			base.CreateControls();
			base.TextBoxControl.Attributes.Add("type", "number");
		}

		// Token: 0x04000771 RID: 1905
		private GridNumericColumn owner;
	}
}
