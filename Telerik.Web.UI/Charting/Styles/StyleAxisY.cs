using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017C9 RID: 6089
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	public class StyleAxisY : StyleAxis
	{
		// Token: 0x0600ECF9 RID: 60665 RVA: 0x003612F6 File Offset: 0x0035F4F6
		public StyleAxisY(ChartYAxis axis) : base(axis)
		{
			this.styleAxisMinorGridLines = new StyleGridLine();
		}

		// Token: 0x170047B6 RID: 18358
		// (get) Token: 0x0600ECFA RID: 60666 RVA: 0x0036130A File Offset: 0x0035F50A
		// (set) Token: 0x0600ECFB RID: 60667 RVA: 0x00361312 File Offset: 0x0035F512
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(typeof(Orientation), "Vertical")]
		internal override Orientation Orientation
		{
			get
			{
				return this.styleAxisOrientation;
			}
			set
			{
				this.styleAxisOrientation = value;
			}
		}

		// Token: 0x0600ECFC RID: 60668 RVA: 0x0036131B File Offset: 0x0035F51B
		internal override void Reset()
		{
			base.Reset();
			this.styleAxisMinorGridLines = new StyleGridLine();
			this.styleAxisOrientation = Orientation.Vertical;
		}
	}
}
