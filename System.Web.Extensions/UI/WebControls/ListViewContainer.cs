using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000AC RID: 172
	internal class ListViewContainer : Control, INamingContainer
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x000221D4 File Offset: 0x000203D4
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is ListViewCommandEventArgs)
			{
				base.RaiseBubbleEvent(source, e);
				return true;
			}
			if (e is CommandEventArgs)
			{
				ListViewCommandEventArgs args = new ListViewCommandEventArgs(null, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}
	}
}
