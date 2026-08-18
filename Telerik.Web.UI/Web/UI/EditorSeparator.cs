using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001083 RID: 4227
	public sealed class EditorSeparator : EditorToolBase
	{
		// Token: 0x17003691 RID: 13969
		// (get) Token: 0x0600A9F8 RID: 43512 RVA: 0x0024DF1F File Offset: 0x0024C11F
		// (set) Token: 0x0600A9F9 RID: 43513 RVA: 0x0024DF22 File Offset: 0x0024C122
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override EditorToolType Type
		{
			get
			{
				return EditorToolType.Separator;
			}
			set
			{
			}
		}

		// Token: 0x0600A9FA RID: 43514 RVA: 0x0024DF24 File Offset: 0x0024C124
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState()
			};
		}

		// Token: 0x0600A9FB RID: 43515 RVA: 0x0024DF42 File Offset: 0x0024C142
		protected override void LoadViewState(object state)
		{
			base.LoadViewState(((object[])state)[0]);
		}

		// Token: 0x0600A9FC RID: 43516 RVA: 0x0024DF52 File Offset: 0x0024C152
		internal static void Render(HtmlTextWriter writer)
		{
			writer.Write("&nbsp;");
		}
	}
}
