using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020002FB RID: 763
	public class GanttResourceColumn : GanttBoundColumn
	{
		// Token: 0x06001A29 RID: 6697 RVA: 0x00055172 File Offset: 0x00053372
		public GanttResourceColumn()
		{
			this.DataField = "resources";
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x00055185 File Offset: 0x00053385
		// (set) Token: 0x06001A2B RID: 6699 RVA: 0x000551A5 File Offset: 0x000533A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Description("The data field in the underlying datasource that this column represents.")]
		[Category("Behavior")]
		[DefaultValue("resources")]
		public new string DataField
		{
			get
			{
				return (string)(base.ViewState["DataField"] ?? string.Empty);
			}
			protected set
			{
				base.ViewState["DataField"] = value;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x000551B8 File Offset: 0x000533B8
		[Description("Gets the string that specifies the display format for items in the column.")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Category("Behavior")]
		[DefaultValue("")]
		public new string DataFormatString
		{
			get
			{
				return base.DataFormatString;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x000551C0 File Offset: 0x000533C0
		[DefaultValue(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Category("Behavior")]
		[Description("Value indicating whether sorting is enabled for this column")]
		public new bool AllowSorting
		{
			get
			{
				return false;
			}
		}
	}
}
