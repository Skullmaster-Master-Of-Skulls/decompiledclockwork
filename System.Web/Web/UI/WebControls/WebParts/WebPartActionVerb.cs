using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000704 RID: 1796
	internal abstract class WebPartActionVerb : WebPartVerb
	{
		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x060057A5 RID: 22437 RVA: 0x00161439 File Offset: 0x00160439
		// (set) Token: 0x060057A6 RID: 22438 RVA: 0x0016143C File Offset: 0x0016043C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		[Browsable(false)]
		public override bool Checked
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("WebPartActionVerb_CantSetChecked"));
			}
		}
	}
}
