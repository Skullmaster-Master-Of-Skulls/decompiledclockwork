using System;
using System.ComponentModel;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000368 RID: 872
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	public class GridMobileMaskedColumnEditor : GridMobileColumnEditorBase
	{
		// Token: 0x06001E09 RID: 7689 RVA: 0x0005D9B2 File Offset: 0x0005BBB2
		public GridMobileMaskedColumnEditor()
		{
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x0005D9BA File Offset: 0x0005BBBA
		public GridMobileMaskedColumnEditor(GridMaskedColumn owner) : base(owner)
		{
			this.owner = owner;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0005D9CA File Offset: 0x0005BBCA
		public override void SetOwner(IGridEditableColumn owner)
		{
			this.owner = (owner as GridMaskedColumn);
		}

		// Token: 0x0400076C RID: 1900
		private GridMaskedColumn owner;
	}
}
