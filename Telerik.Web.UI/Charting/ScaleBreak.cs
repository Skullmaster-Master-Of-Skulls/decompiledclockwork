using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Web.UI;
using Telerik.Charting.Styles;
using Telerik.Web.UI.Common;

namespace Telerik.Charting
{
	// Token: 0x02001732 RID: 5938
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("Segments")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class ScaleBreak : StateManagedObject
	{
		// Token: 0x0600E70A RID: 59146 RVA: 0x0033AC24 File Offset: 0x00338E24
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.scaleBreakLine).TrackViewState();
			((IChartingStateManager)this.scaleBreakSegments).TrackViewState();
		}

		// Token: 0x0600E70B RID: 59147 RVA: 0x0033AC44 File Offset: 0x00338E44
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.scaleBreakLine).LoadViewState(array[1]);
				((IChartingStateManager)this.scaleBreakSegments).LoadViewState(array[2]);
			}
		}

		// Token: 0x0600E70C RID: 59148 RVA: 0x0033AC80 File Offset: 0x00338E80
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.scaleBreakLine).SaveViewState(),
				((IChartingStateManager)this.scaleBreakSegments).SaveViewState()
			}.ToArray();
		}

		// Token: 0x1700464E RID: 17998
		// (get) Token: 0x0600E70D RID: 59149 RVA: 0x0033ACCA File Offset: 0x00338ECA
		// (set) Token: 0x0600E70E RID: 59150 RVA: 0x0033ACEB File Offset: 0x00338EEB
		[Browsable(true)]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool Enabled
		{
			get
			{
				return (bool)(base.ViewState["Enabled"] ?? false);
			}
			set
			{
				base.ViewState["Enabled"] = value;
				this.scaleBreakParent.Items.Clear();
			}
		}

		// Token: 0x1700464F RID: 17999
		// (get) Token: 0x0600E70F RID: 59151 RVA: 0x0033AD13 File Offset: 0x00338F13
		// (set) Token: 0x0600E710 RID: 59152 RVA: 0x0033AD34 File Offset: 0x00338F34
		[Browsable(true)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(ScaleBreakLineType.Sinusoid)]
		public ScaleBreakLineType LineStyle
		{
			get
			{
				return (ScaleBreakLineType)(base.ViewState["LineStyle"] ?? ScaleBreakLineType.Sinusoid);
			}
			set
			{
				base.ViewState["LineStyle"] = value;
			}
		}

		// Token: 0x17004650 RID: 18000
		// (get) Token: 0x0600E711 RID: 59153 RVA: 0x0033AD4C File Offset: 0x00338F4C
		// (set) Token: 0x0600E712 RID: 59154 RVA: 0x0033AD6D File Offset: 0x00338F6D
		[DefaultValue(1)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public int MaxCount
		{
			get
			{
				return (int)(base.ViewState["MaxCount"] ?? 1);
			}
			set
			{
				if (value <= 0)
				{
					throw new ChartException("Scale break's MaxCount should be great that zero.");
				}
				base.ViewState["MaxCount"] = value;
			}
		}

		// Token: 0x17004651 RID: 18001
		// (get) Token: 0x0600E713 RID: 59155 RVA: 0x0033AD94 File Offset: 0x00338F94
		// (set) Token: 0x0600E714 RID: 59156 RVA: 0x0033ADB6 File Offset: 0x00338FB6
		[Browsable(true)]
		[DefaultValue(25)]
		[NotifyParentProperty(true)]
		public byte ValueTolerance
		{
			get
			{
				return (byte)(base.ViewState["ValueTolerance"] ?? 25);
			}
			set
			{
				if (value > 100)
				{
					throw new ChartException("Scale break's ValueTolerance should be from 0 to 100");
				}
				base.ViewState["ValueTolerance"] = value;
			}
		}

		// Token: 0x17004652 RID: 18002
		// (get) Token: 0x0600E715 RID: 59157 RVA: 0x0033ADDE File Offset: 0x00338FDE
		// (set) Token: 0x0600E716 RID: 59158 RVA: 0x0033ADFF File Offset: 0x00338FFF
		[Browsable(true)]
		[DefaultValue(4)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		public int Width
		{
			get
			{
				return (int)(base.ViewState["Width"] ?? 4);
			}
			set
			{
				if (value <= 0)
				{
					throw new ChartException("Scale break's Width should be great that zero.");
				}
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x17004653 RID: 18003
		// (get) Token: 0x0600E717 RID: 59159 RVA: 0x0033AE26 File Offset: 0x00339026
		// (set) Token: 0x0600E718 RID: 59160 RVA: 0x0033AE2E File Offset: 0x0033902E
		[Browsable(true)]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public LineStyle Line
		{
			get
			{
				return this.scaleBreakLine;
			}
			set
			{
				this.scaleBreakLine = value;
			}
		}

		// Token: 0x17004654 RID: 18004
		// (get) Token: 0x0600E719 RID: 59161 RVA: 0x0033AE37 File Offset: 0x00339037
		[Description("Segments collection.")]
		[Editor(typeof(AxisSegmentsCollectionEditor), typeof(UITypeEditor))]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AxisSegmentCollection Segments
		{
			get
			{
				return this.scaleBreakSegments;
			}
		}

		// Token: 0x17004655 RID: 18005
		// (get) Token: 0x0600E71A RID: 59162 RVA: 0x0033AE3F File Offset: 0x0033903F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[NotifyParentProperty(true)]
		public ChartAxis Parent
		{
			get
			{
				return this.scaleBreakParent;
			}
		}

		// Token: 0x0600E71B RID: 59163 RVA: 0x0033AE47 File Offset: 0x00339047
		public ScaleBreak(ChartAxis parent)
		{
			this.scaleBreakParent = parent;
			this.scaleBreakLine = new ScaleBreaksLineStyle();
			this.scaleBreakSegments = new AxisSegmentCollection();
		}

		// Token: 0x0600E71C RID: 59164 RVA: 0x0033AE6C File Offset: 0x0033906C
		internal GraphicsPath CreateScaleBreakLine(double length, bool isHorizontal)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			length = Math.Ceiling(length);
			if (this.LineStyle == ScaleBreakLineType.Straight)
			{
				if (isHorizontal)
				{
					graphicsPath.AddLine(new Point(0, 0), new Point((int)length, 0));
				}
				else
				{
					graphicsPath.AddLine(new Point(0, 0), new Point(0, (int)length));
				}
				return graphicsPath;
			}
			TelerikRandom telerikRandom = new TelerikRandom();
			int num = (int)length / (int)(telerikRandom.GetDouble() * 20.0 + 10.0);
			List<Point> list = new List<Point>();
			float num2 = (float)(length / (double)num);
			float num3 = num2 / 8f;
			int num4 = 0;
			if (this.LineStyle == ScaleBreakLineType.Sinusoid)
			{
				bool flag = true;
				list.Add(new Point(0, 0));
				num4 += (int)num2;
				if (isHorizontal)
				{
					while (length - (double)num4 >= (double)num2)
					{
						list.Add(new Point(num4, (int)num3 * (flag ? -1 : 1)));
						num4 += (int)num2;
						flag = !flag;
					}
					list.Add(new Point((int)length, 0));
				}
				else
				{
					while (length - (double)num4 >= (double)num2)
					{
						list.Add(new Point((int)num3 * (flag ? -1 : 1), num4));
						num4 += (int)num2;
						flag = !flag;
					}
					list.Add(new Point(0, (int)length));
				}
				graphicsPath.AddCurve(list.ToArray());
				return graphicsPath;
			}
			num3 = num2 / (float)(telerikRandom.GetDouble() * 16.0 + 4.0);
			num4 = (int)(telerikRandom.GetDouble() * (double)num2 + (double)num3);
			list.Add(new Point(0, 0));
			if (isHorizontal)
			{
				while (length - (double)num4 >= (double)num2)
				{
					list.Add(new Point(num4, (int)((double)((telerikRandom.GetDouble() < 0.5) ? -1 : 1) * (telerikRandom.GetDouble() * (double)num3))));
					num4 += (int)(telerikRandom.GetDouble() * (double)num2 + (double)num3);
				}
				list.Add(new Point((int)length, 0));
			}
			else
			{
				while (length - (double)num4 >= (double)num2)
				{
					list.Add(new Point((int)((double)((telerikRandom.GetDouble() < 0.5) ? -1 : 1) * (telerikRandom.GetDouble() * (double)num3 * 0.5)), num4));
					num4 += (int)(telerikRandom.GetDouble() * (double)num2 + (double)num3);
				}
				list.Add(new Point(0, (int)length));
			}
			if (this.LineStyle == ScaleBreakLineType.JaggedCurve)
			{
				graphicsPath.AddLines(list.ToArray());
			}
			else
			{
				graphicsPath.AddCurve(list.ToArray());
			}
			return graphicsPath;
		}

		// Token: 0x0400426B RID: 17003
		private LineStyle scaleBreakLine;

		// Token: 0x0400426C RID: 17004
		protected ChartAxis scaleBreakParent;

		// Token: 0x0400426D RID: 17005
		internal AxisSegmentCollection scaleBreakSegments;
	}
}
