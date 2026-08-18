using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001B36 RID: 6966
	public class ContextMenuControlTarget : ContextMenuTarget
	{
		// Token: 0x17005228 RID: 21032
		// (get) Token: 0x06010DA6 RID: 69030 RVA: 0x003BD70B File Offset: 0x003BB90B
		// (set) Token: 0x06010DA7 RID: 69031 RVA: 0x003BD72B File Offset: 0x003BB92B
		[DefaultValue("")]
		[TypeConverter(typeof(ContextMenuControlTargetControlIDConverter))]
		[IDReferenceProperty]
		public string ControlID
		{
			get
			{
				return ((string)base.ViewState["ControlID"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ControlID"] = value;
			}
		}

		// Token: 0x17005229 RID: 21033
		// (get) Token: 0x06010DA8 RID: 69032 RVA: 0x003BD73E File Offset: 0x003BB93E
		internal override ContextMenuTargetType Type
		{
			get
			{
				return ContextMenuTargetType.Control;
			}
		}
	}
}
