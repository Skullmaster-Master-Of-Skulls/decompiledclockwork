using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017E3 RID: 6115
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class StyleMarkerEmptyValue : StyleMarker
	{
		// Token: 0x0600EDE3 RID: 60899 RVA: 0x00363660 File Offset: 0x00361860
		public StyleMarkerEmptyValue()
		{
			this.position = new PositionCenter();
		}

		// Token: 0x170047FE RID: 18430
		// (get) Token: 0x0600EDE4 RID: 60900 RVA: 0x00363673 File Offset: 0x00361873
		// (set) Token: 0x0600EDE5 RID: 60901 RVA: 0x00363694 File Offset: 0x00361894
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public override bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x170047FF RID: 18431
		// (get) Token: 0x0600EDE6 RID: 60902 RVA: 0x0036369D File Offset: 0x0036189D
		// (set) Token: 0x0600EDE7 RID: 60903 RVA: 0x003636BD File Offset: 0x003618BD
		[DefaultValue(typeof(string), "Cross")]
		[Description("Specifies the shape of the empty value marker")]
		[SkinnableProperty]
		[Editor(typeof(FiguresEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		public override string Figure
		{
			get
			{
				return (string)(base.ViewState["Figure"] ?? "Cross");
			}
			set
			{
				base.Figure = value;
			}
		}

		// Token: 0x0600EDE8 RID: 60904 RVA: 0x003636C6 File Offset: 0x003618C6
		internal override void Reset()
		{
			base.Reset();
			this.Figure = "Cross";
			this.Visible = true;
			this.position = new PositionCenter();
		}
	}
}
