using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001218 RID: 4632
	public class TreeListPageSizeChangedEventArgs : TreeListCommandEventArgs
	{
		// Token: 0x0600BF45 RID: 48965 RVA: 0x002A597C File Offset: 0x002A3B7C
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToInt32(System.Object)")]
		public TreeListPageSizeChangedEventArgs(TreeListItem item, object commandSource, object argument) : base(item, commandSource, "ChangePageSize", argument)
		{
			this.NewPageSize = Convert.ToInt32(argument);
		}

		// Token: 0x17003DB7 RID: 15799
		// (get) Token: 0x0600BF46 RID: 48966 RVA: 0x002A5998 File Offset: 0x002A3B98
		// (set) Token: 0x0600BF47 RID: 48967 RVA: 0x002A59A0 File Offset: 0x002A3BA0
		public int NewPageSize { get; internal set; }

		// Token: 0x0600BF48 RID: 48968 RVA: 0x002A59AC File Offset: 0x002A3BAC
		public override void ExecuteCommand(object source)
		{
			RadTreeList radTreeList = source as RadTreeList;
			if (radTreeList != null)
			{
				radTreeList.FirePageSizeChanged(this);
			}
		}
	}
}
