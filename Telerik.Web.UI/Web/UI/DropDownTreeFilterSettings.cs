using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200046B RID: 1131
	public sealed class DropDownTreeFilterSettings : ObjectWithState
	{
		// Token: 0x06002887 RID: 10375 RVA: 0x000834C6 File Offset: 0x000816C6
		internal DropDownTreeFilterSettings(StateBag ownerViewState) : base("DropDownTreeButtonSettings", ownerViewState)
		{
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06002888 RID: 10376 RVA: 0x000834D4 File Offset: 0x000816D4
		// (set) Token: 0x06002889 RID: 10377 RVA: 0x000834F5 File Offset: 0x000816F5
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether to highlight the matches.")]
		public DropDownTreeHighlight Highlight
		{
			get
			{
				return (DropDownTreeHighlight)(base.ViewState["DropDownTreeHighlight"] ?? DropDownTreeHighlight.None);
			}
			set
			{
				base.ViewState["DropDownTreeHighlight"] = value;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600288A RID: 10378 RVA: 0x0008350D File Offset: 0x0008170D
		// (set) Token: 0x0600288B RID: 10379 RVA: 0x0008352D File Offset: 0x0008172D
		[DefaultValue("")]
		[Description("Gets or sets a value indicating the Empty message of the filter.")]
		public string EmptyMessage
		{
			get
			{
				return (string)(base.ViewState["EmptyMessage"] ?? "");
			}
			set
			{
				base.ViewState["EmptyMessage"] = value;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x0600288C RID: 10380 RVA: 0x00083540 File Offset: 0x00081740
		// (set) Token: 0x0600288D RID: 10381 RVA: 0x00083561 File Offset: 0x00081761
		[DefaultValue(DropDownTreeFilter.StartsWith)]
		[Description("Gets or sets a value indicating the filter criteria.")]
		[Browsable(true)]
		public DropDownTreeFilter Filter
		{
			get
			{
				return (DropDownTreeFilter)(base.ViewState["Filter"] ?? DropDownTreeFilter.StartsWith);
			}
			set
			{
				base.ViewState["Filter"] = value;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x00083579 File Offset: 0x00081779
		// (set) Token: 0x0600288F RID: 10383 RVA: 0x0008359A File Offset: 0x0008179A
		[Browsable(true)]
		[Description("Gets or sets a value indicating the filter criteria when template is applied.")]
		[DefaultValue(DropDownTreeFilterTemplate.ByText)]
		public DropDownTreeFilterTemplate FilterTemplate
		{
			get
			{
				return (DropDownTreeFilterTemplate)(base.ViewState["FilterTemplate"] ?? DropDownTreeFilterTemplate.ByText);
			}
			set
			{
				base.ViewState["FilterTemplate"] = value;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x000835B2 File Offset: 0x000817B2
		// (set) Token: 0x06002891 RID: 10385 RVA: 0x000835D3 File Offset: 0x000817D3
		[DefaultValue(1)]
		[Description("Defines the minimum number of characters that must be typed before a filtering is made.")]
		[Category("Behavior")]
		public int MinFilterLength
		{
			get
			{
				return (int)(base.ViewState["MinFilterLength"] ?? 1);
			}
			set
			{
				base.ViewState["MinFilterLength"] = value;
			}
		}
	}
}
