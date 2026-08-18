using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000578 RID: 1400
	internal abstract class WebPartActionVerb : WebPartVerb
	{
		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x06004732 RID: 18226 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06004733 RID: 18227 RVA: 0x000EA789 File Offset: 0x000E8989
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
