using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017CA RID: 6090
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleAxisX : StyleAxis
	{
		// Token: 0x0600ECFD RID: 60669 RVA: 0x00361335 File Offset: 0x0035F535
		public StyleAxisX(ChartXAxis axis) : base(axis)
		{
			this.styleAxisMajorGridLines = new StyleGridLineMajorXAxis();
		}

		// Token: 0x170047B7 RID: 18359
		// (get) Token: 0x0600ECFE RID: 60670 RVA: 0x00361349 File Offset: 0x0035F549
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SkinnableProperty]
		[Browsable(false)]
		public override StyleTickMinor MinorTick
		{
			get
			{
				return base.MinorTick;
			}
		}

		// Token: 0x170047B8 RID: 18360
		// (get) Token: 0x0600ECFF RID: 60671 RVA: 0x00361351 File Offset: 0x0035F551
		[SkinnableProperty]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override StyleGridLine MinorGridLines
		{
			get
			{
				return base.MinorGridLines;
			}
		}

		// Token: 0x170047B9 RID: 18361
		// (get) Token: 0x0600ED00 RID: 60672 RVA: 0x00361359 File Offset: 0x0035F559
		// (set) Token: 0x0600ED01 RID: 60673 RVA: 0x00361361 File Offset: 0x0035F561
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(typeof(Orientation), "Horizontal")]
		[NotifyParentProperty(true)]
		[Browsable(false)]
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

		// Token: 0x0600ED02 RID: 60674 RVA: 0x0036136A File Offset: 0x0035F56A
		internal override void Reset()
		{
			base.Reset();
			this.styleAxisMajorGridLines = new StyleGridLineMajorXAxis();
			this.styleAxisOrientation = Orientation.Horizontal;
			base.Visible = ChartAxisVisibility.Auto;
		}
	}
}
