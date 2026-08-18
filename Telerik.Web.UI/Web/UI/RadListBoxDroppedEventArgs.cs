using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001927 RID: 6439
	public class RadListBoxDroppedEventArgs : RadListBoxDropEventArgs
	{
		// Token: 0x0600F975 RID: 63861 RVA: 0x0038504E File Offset: 0x0038324E
		public RadListBoxDroppedEventArgs(RadListBoxDropEventArgs droppingEventArgs) : base(droppingEventArgs.HtmlElementID, droppingEventArgs.SourceDragItems)
		{
		}
	}
}
