using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001847 RID: 6215
	public class ReorderClientOperation<T> : ClientOperation<T> where T : ControlItem
	{
		// Token: 0x170048E7 RID: 18663
		// (get) Token: 0x0600F155 RID: 61781 RVA: 0x0036DB9E File Offset: 0x0036BD9E
		// (set) Token: 0x0600F156 RID: 61782 RVA: 0x0036DBA6 File Offset: 0x0036BDA6
		public int NewIndex { get; internal set; }

		// Token: 0x170048E8 RID: 18664
		// (get) Token: 0x0600F157 RID: 61783 RVA: 0x0036DBAF File Offset: 0x0036BDAF
		// (set) Token: 0x0600F158 RID: 61784 RVA: 0x0036DBB7 File Offset: 0x0036BDB7
		public int OldIndex { get; internal set; }
	}
}
