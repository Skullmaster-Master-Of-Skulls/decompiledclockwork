using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001B38 RID: 6968
	public class ContextMenuElementTarget : ContextMenuTarget
	{
		// Token: 0x1700522B RID: 21035
		// (get) Token: 0x06010DAC RID: 69036 RVA: 0x003BD754 File Offset: 0x003BB954
		// (set) Token: 0x06010DAD RID: 69037 RVA: 0x003BD774 File Offset: 0x003BB974
		public string ElementID
		{
			get
			{
				return ((string)base.ViewState["ElementID"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ElementID"] = value;
			}
		}

		// Token: 0x1700522C RID: 21036
		// (get) Token: 0x06010DAE RID: 69038 RVA: 0x003BD787 File Offset: 0x003BB987
		internal override ContextMenuTargetType Type
		{
			get
			{
				return ContextMenuTargetType.Element;
			}
		}
	}
}
