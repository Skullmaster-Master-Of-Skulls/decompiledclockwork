using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000482 RID: 1154
	public class MultiViewControlBuilder : ControlBuilder
	{
		// Token: 0x06003939 RID: 14649 RVA: 0x000BA469 File Offset: 0x000B8669
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			if (subBuilder is CodeBlockBuilder)
			{
				throw new Exception(SR.GetString("Multiview_rendering_block_not_allowed"));
			}
			base.AppendSubBuilder(subBuilder);
		}
	}
}
