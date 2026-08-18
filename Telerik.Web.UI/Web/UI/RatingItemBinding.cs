using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000F28 RID: 3880
	public class RatingItemBinding : StateManager
	{
		// Token: 0x17002ECF RID: 11983
		// (get) Token: 0x060093F9 RID: 37881 RVA: 0x00212E90 File Offset: 0x00211090
		// (set) Token: 0x060093FA RID: 37882 RVA: 0x00212EB0 File Offset: 0x002110B0
		[Category("Data")]
		[DefaultValue("")]
		[Description("Gets/Sets the field of the data source that provides the value content (Value property of the Rating item) of the Rating items.")]
		public string ValueField
		{
			get
			{
				return (string)(base.ViewState["ValueField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ValueField"] = value;
			}
		}

		// Token: 0x17002ED0 RID: 11984
		// (get) Token: 0x060093FB RID: 37883 RVA: 0x00212EC3 File Offset: 0x002110C3
		// (set) Token: 0x060093FC RID: 37884 RVA: 0x00212EE3 File Offset: 0x002110E3
		[Category("Data")]
		[DefaultValue("")]
		[Description("Gets/Sets the field of the data source that provides the ToolTip content (ToolTip property of the Rating item) of the Rating items.")]
		public string ToolTipField
		{
			get
			{
				return (string)(base.ViewState["ToolTipField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ToolTipField"] = value;
			}
		}

		// Token: 0x17002ED1 RID: 11985
		// (get) Token: 0x060093FD RID: 37885 RVA: 0x00212EF6 File Offset: 0x002110F6
		// (set) Token: 0x060093FE RID: 37886 RVA: 0x00212F16 File Offset: 0x00211116
		[DefaultValue("")]
		[Category("Data")]
		[Description("Gets/Sets the formatting string used to control how data bound to the RatingItem's ToolTip is displayed.")]
		public string ToolTipFormatString
		{
			get
			{
				return (string)(base.ViewState["ToolTipFormatString"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ToolTipFormatString"] = value;
			}
		}
	}
}
