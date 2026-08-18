using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001B39 RID: 6969
	public class ContextMenuTagNameTarget : ContextMenuTarget
	{
		// Token: 0x1700522D RID: 21037
		// (get) Token: 0x06010DB0 RID: 69040 RVA: 0x003BD792 File Offset: 0x003BB992
		// (set) Token: 0x06010DB1 RID: 69041 RVA: 0x003BD7B2 File Offset: 0x003BB9B2
		public string TagName
		{
			get
			{
				return ((string)base.ViewState["TagName"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["TagName"] = value;
			}
		}

		// Token: 0x1700522E RID: 21038
		// (get) Token: 0x06010DB2 RID: 69042 RVA: 0x003BD7C5 File Offset: 0x003BB9C5
		internal override ContextMenuTargetType Type
		{
			get
			{
				return ContextMenuTargetType.TagName;
			}
		}
	}
}
