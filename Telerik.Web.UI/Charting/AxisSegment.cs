using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001723 RID: 5923
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class AxisSegment : StateManagedObject
	{
		// Token: 0x0600E608 RID: 58888 RVA: 0x00331E68 File Offset: 0x00330068
		public AxisSegment()
		{
			this.axisSegmentPointEnd = default(PointF);
			this.axisSegmentPointStart = default(PointF);
			this.axisSegmentRectangle = default(Rectangle);
			this.axisSegmentPaths = new GraphicsPath[2];
			this.axisSegmentPaths[0] = null;
			this.axisSegmentPaths[1] = null;
		}

		// Token: 0x0600E609 RID: 58889 RVA: 0x00331EC5 File Offset: 0x003300C5
		public AxisSegment(string name) : this()
		{
			this.Name = name;
		}

		// Token: 0x1700460D RID: 17933
		// (get) Token: 0x0600E60A RID: 58890 RVA: 0x00331ED4 File Offset: 0x003300D4
		// (set) Token: 0x0600E60B RID: 58891 RVA: 0x00331EF4 File Offset: 0x003300F4
		[PersistenceMode(PersistenceMode.Attribute)]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x1700460E RID: 17934
		// (get) Token: 0x0600E60C RID: 58892 RVA: 0x00331F07 File Offset: 0x00330107
		// (set) Token: 0x0600E60D RID: 58893 RVA: 0x00331F30 File Offset: 0x00330130
		[DefaultValue(0.0)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[Description("Specifies the minimal value of the axis segment.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public double MinValue
		{
			get
			{
				return (double)(base.ViewState["MinValue"] ?? 0.0);
			}
			set
			{
				base.ViewState["MinValue"] = value;
			}
		}

		// Token: 0x1700460F RID: 17935
		// (get) Token: 0x0600E60E RID: 58894 RVA: 0x00331F48 File Offset: 0x00330148
		// (set) Token: 0x0600E60F RID: 58895 RVA: 0x00331F71 File Offset: 0x00330171
		[Description("Specifies the maximal value of the axis segment.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(100.0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		public double MaxValue
		{
			get
			{
				return (double)(base.ViewState["MaxValue"] ?? 100.0);
			}
			set
			{
				base.ViewState["MaxValue"] = value;
			}
		}

		// Token: 0x17004610 RID: 17936
		// (get) Token: 0x0600E610 RID: 58896 RVA: 0x00331F89 File Offset: 0x00330189
		// (set) Token: 0x0600E611 RID: 58897 RVA: 0x00331FB2 File Offset: 0x003301B2
		[DefaultValue(10.0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Specifies the step at which axis segment values are calculated.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public double Step
		{
			get
			{
				return (double)(base.ViewState["Step"] ?? 10.0);
			}
			set
			{
				base.ViewState["Step"] = value;
			}
		}

		// Token: 0x17004611 RID: 17937
		// (get) Token: 0x0600E612 RID: 58898 RVA: 0x00331FCA File Offset: 0x003301CA
		// (set) Token: 0x0600E613 RID: 58899 RVA: 0x00331FD2 File Offset: 0x003301D2
		internal PointF StartPoint
		{
			get
			{
				return this.axisSegmentPointStart;
			}
			set
			{
				this.axisSegmentPointStart = value;
			}
		}

		// Token: 0x17004612 RID: 17938
		// (get) Token: 0x0600E614 RID: 58900 RVA: 0x00331FDB File Offset: 0x003301DB
		// (set) Token: 0x0600E615 RID: 58901 RVA: 0x00331FE3 File Offset: 0x003301E3
		internal PointF EndPoint
		{
			get
			{
				return this.axisSegmentPointEnd;
			}
			set
			{
				this.axisSegmentPointEnd = value;
			}
		}

		// Token: 0x17004613 RID: 17939
		// (get) Token: 0x0600E616 RID: 58902 RVA: 0x00331FEC File Offset: 0x003301EC
		// (set) Token: 0x0600E617 RID: 58903 RVA: 0x00331FF4 File Offset: 0x003301F4
		internal RectangleF Rectangle
		{
			get
			{
				return this.axisSegmentRectangle;
			}
			set
			{
				this.axisSegmentRectangle = value;
			}
		}

		// Token: 0x17004614 RID: 17940
		// (get) Token: 0x0600E618 RID: 58904 RVA: 0x00332000 File Offset: 0x00330200
		internal virtual double PixelsPerValue
		{
			get
			{
				float num = this.axisSegmentPointStart.X - this.axisSegmentPointEnd.X;
				float num2 = this.axisSegmentPointStart.Y - this.axisSegmentPointEnd.Y;
				double num3 = Math.Abs(this.MaxValue - this.MinValue);
				return Math.Sqrt((double)(num * num + num2 * num2)) / num3;
			}
		}

		// Token: 0x0600E619 RID: 58905 RVA: 0x00332060 File Offset: 0x00330260
		internal float? GetX(double val)
		{
			float? num = new float?((float)((double)this.Rectangle.Left + (val - this.MinValue) * this.PixelsPerValue));
			float? num2 = num;
			float left = this.Rectangle.Left;
			float? num4;
			if (num2.GetValueOrDefault() >= left || num2 == null)
			{
				float? num3 = num;
				float right = this.Rectangle.Right;
				if (num3.GetValueOrDefault() <= right || num3 == null)
				{
					num4 = num;
					goto IL_8A;
				}
			}
			num4 = null;
			IL_8A:
			num = num4;
			if (num == null)
			{
				if (val >= this.MaxValue)
				{
					return new float?(this.Rectangle.Right);
				}
				if (val <= this.MinValue)
				{
					return new float?(this.Rectangle.Left);
				}
			}
			return num;
		}

		// Token: 0x0600E61A RID: 58906 RVA: 0x00332140 File Offset: 0x00330340
		internal float? GetY(double val)
		{
			float? num = new float?((float)((double)this.Rectangle.Bottom - (val - this.MinValue) * this.PixelsPerValue));
			float? num2 = num;
			float top = this.Rectangle.Top;
			float? num4;
			if (num2.GetValueOrDefault() >= top || num2 == null)
			{
				float? num3 = num;
				float bottom = this.Rectangle.Bottom;
				if (num3.GetValueOrDefault() <= bottom || num3 == null)
				{
					num4 = num;
					goto IL_8A;
				}
			}
			num4 = null;
			IL_8A:
			num = num4;
			if (num == null)
			{
				if (val >= this.MaxValue)
				{
					return new float?(this.Rectangle.Top);
				}
				if (val <= this.MinValue)
				{
					return new float?(this.Rectangle.Bottom);
				}
			}
			return num;
		}

		// Token: 0x0600E61B RID: 58907 RVA: 0x00332220 File Offset: 0x00330420
		internal void SetRange(ChartSeriesItemsCollection items, bool isOptimizeMax)
		{
			if (double.IsNaN(this.MaxValue) || double.IsNaN(this.MinValue) || this.axisSegmentItemsCount == 0)
			{
				return;
			}
			double minValue = items.GetMinValue(this.MinValue, this.MaxValue);
			double maxValue = items.GetMaxValue(this.MinValue, this.MaxValue);
			if (isOptimizeMax)
			{
				this.MaxValue = Math.Min((maxValue > 0.0) ? (maxValue * 1.1) : (maxValue * 0.8), this.MaxValue);
			}
			this.MinValue = Math.Min((minValue > 0.0) ? (minValue * 0.8) : (minValue * 1.2), this.MinValue);
			double num = AxisSegment.OptimizeNumber(this.MaxValue, null) - AxisSegment.OptimizeNumber(this.MinValue, new bool?(false));
			num = ((num == 0.0) ? (this.MaxValue - this.MinValue) : num);
			double number = num / (double)this.axisSegmentItemsCount;
			double num2 = AxisSegment.OptimizeNumber(number, null);
			this.Step = ((num2 == 0.0) ? 1.0 : num2);
			double num3;
			double num4;
			if (isOptimizeMax)
			{
				num3 = AxisSegment.OptimizeNumber(this.MinValue, new bool?(false));
				num4 = num3;
				while ((float)num4 < (float)this.MaxValue)
				{
					num4 += this.Step;
				}
			}
			else
			{
				num4 = this.MaxValue;
				num3 = num4;
				while ((float)num3 > (float)this.MinValue)
				{
					num3 -= this.Step;
				}
			}
			switch (this.axisSegmentVisibleValues)
			{
			case ChartAxisVisibleValues.Positive:
				if (num3 < 0.0)
				{
					num3 = 0.0;
				}
				break;
			case ChartAxisVisibleValues.Negative:
				if (num4 > 0.0)
				{
					num4 = 0.0;
				}
				break;
			}
			this.MaxValue = Math.Round(num4, 6);
			this.MinValue = Math.Round(num3, 6);
		}

		// Token: 0x0600E61C RID: 58908 RVA: 0x0033242C File Offset: 0x0033062C
		private static double OptimizeNumber(double number, bool? toLarge)
		{
			if (number == 0.0)
			{
				return number;
			}
			bool flag = number < 0.0;
			if (flag)
			{
				if (toLarge != null)
				{
					toLarge = !toLarge;
				}
				else
				{
					toLarge = new bool?(true);
				}
			}
			number = Math.Abs(number);
			int num = 0;
			double num2 = number;
			while (number < 1.0)
			{
				num++;
				number *= 10.0;
			}
			double num3 = Math.Ceiling(number);
			if (toLarge != null && !toLarge.Value)
			{
				num3 = Math.Floor(number);
			}
			if (num3 >= 10.0 && num3 < 999.0)
			{
				if (toLarge != null && !toLarge.Value)
				{
					num3 = Math.Floor(number / 10.0) * 10.0;
				}
				else
				{
					num3 = Math.Ceiling(number / 10.0) * 10.0;
				}
			}
			if (num3 >= 1000.0 && num3 < 9999.0)
			{
				if (toLarge != null && !toLarge.Value)
				{
					num3 = Math.Floor(number / 100.0) * 100.0;
				}
				else
				{
					num3 = Math.Ceiling(number / 100.0) * 100.0;
				}
			}
			if (num3 >= 10000.0)
			{
				if ((toLarge != null && !toLarge.Value) || flag)
				{
					num3 = Math.Floor(number / 1000.0) * 1000.0;
				}
				else
				{
					num3 = Math.Ceiling(number / 1000.0) * 1000.0;
				}
			}
			double num4 = num3;
			if (num2 < 1.0)
			{
				while (num-- > 0)
				{
					num4 /= 10.0;
				}
			}
			return num4 * (double)(flag ? -1 : 1);
		}

		// Token: 0x0600E61D RID: 58909 RVA: 0x00332634 File Offset: 0x00330834
		internal double GetAxisItems(ChartAxis axis)
		{
			double num = this.MinValue;
			ChartAxisItem chartAxisItem = axis.AddItem(axis.FormatLabel(num));
			chartAxisItem.Value = (decimal)num;
			chartAxisItem.chartAxisItemType = ChartAxisItemType.SegmentStart;
			num += this.Step;
			if (this.axisSegmentPercent > 15)
			{
				while (num < this.MaxValue)
				{
					num = Math.Round(num, 13);
					chartAxisItem = axis.AddItem(axis.FormatLabel(num));
					chartAxisItem.Value = (decimal)num;
					num += this.Step;
				}
			}
			num = this.MaxValue;
			chartAxisItem = axis.AddItem(axis.FormatLabel(num));
			chartAxisItem.Value = (decimal)num;
			chartAxisItem.chartAxisItemType = ChartAxisItemType.SegmentEnd;
			return num;
		}

		// Token: 0x0600E61E RID: 58910 RVA: 0x003326E0 File Offset: 0x003308E0
		internal bool IsIntersection(AxisSegment segment)
		{
			float num = (float)this.MinValue;
			float num2 = (float)this.MaxValue;
			float num3 = (float)segment.MinValue;
			float num4 = (float)segment.MaxValue;
			return (num != 0f || num2 != 0f) && (num3 != 0f || num4 != 0f) && (num <= num3 || num <= num4 || num2 <= num3 || num2 <= num4) && (num >= num3 || num >= num4 || num2 >= num3 || num2 >= num4);
		}

		// Token: 0x0600E61F RID: 58911 RVA: 0x00332758 File Offset: 0x00330958
		internal GraphicsPath GetPath(GraphicsPath linePath, bool startLine, bool endLine, bool isHorizontal)
		{
			this.axisSegmentPaths[0] = null;
			this.axisSegmentPaths[1] = null;
			GraphicsPath graphicsPath = new GraphicsPath();
			GraphicsPath graphicsPath2 = new GraphicsPath();
			PointF pointF = new PointF(this.Rectangle.Left, this.Rectangle.Top);
			PointF pointF2 = new PointF(this.Rectangle.Left, this.Rectangle.Bottom);
			PointF pointF3 = new PointF(this.Rectangle.Right, this.Rectangle.Top);
			PointF pointF4 = new PointF(this.Rectangle.Right, this.Rectangle.Bottom);
			if (isHorizontal)
			{
				float num = (float)((int)this.Rectangle.Height / 2);
				graphicsPath.AddLine(new PointF(pointF2.X, pointF2.Y - num), pointF);
				if (startLine)
				{
					GraphicsPath graphicsPath3 = RenderEngine.MoveTo(linePath, pointF.X, pointF.Y);
					graphicsPath.AddPath(graphicsPath3, false);
					this.axisSegmentPaths[0] = graphicsPath3;
				}
				else
				{
					graphicsPath.AddLine(pointF, pointF3);
				}
				graphicsPath.AddLine(pointF3, new PointF(pointF4.X, pointF4.Y - num));
				graphicsPath.AddLine(new PointF(pointF4.X, pointF4.Y - num), new PointF(pointF2.X, pointF2.Y - num));
				if (endLine)
				{
					GraphicsPath graphicsPath4 = RenderEngine.MoveTo(linePath, pointF2.X, pointF2.Y);
					graphicsPath2.AddPath(graphicsPath4, false);
					this.axisSegmentPaths[1] = graphicsPath4;
				}
				else
				{
					graphicsPath2.AddLine(pointF2, pointF4);
				}
				num += this.Rectangle.Height % 2f;
				graphicsPath2.AddLine(pointF4, new PointF(pointF3.X, pointF3.Y + num));
				graphicsPath2.AddLine(new PointF(pointF3.X, pointF3.Y + num), new PointF(pointF.X, pointF.Y + num));
				graphicsPath2.AddLine(new PointF(pointF.X, pointF.Y + num), pointF2);
				graphicsPath.AddPath(graphicsPath2, true);
			}
			else
			{
				float num2 = (float)((int)(this.Rectangle.Width - this.Rectangle.Width % 2f)) / 2f;
				graphicsPath.AddLine(new PointF(pointF3.X - num2, pointF3.Y), pointF3);
				if (startLine)
				{
					GraphicsPath graphicsPath5 = RenderEngine.MoveTo(linePath, pointF3.X, pointF3.Y);
					graphicsPath.AddPath(graphicsPath5, false);
					this.axisSegmentPaths[0] = graphicsPath5;
				}
				else
				{
					graphicsPath.AddLine(pointF3, pointF4);
				}
				graphicsPath.AddLine(pointF4, new PointF(pointF4.X - num2, pointF4.Y));
				graphicsPath.AddLine(new PointF(pointF4.X - num2, pointF4.Y), new PointF(pointF3.X - num2, pointF3.Y));
				if (endLine)
				{
					GraphicsPath graphicsPath6 = RenderEngine.MoveTo(linePath, pointF.X, pointF.Y);
					graphicsPath2.AddPath(graphicsPath6, false);
					this.axisSegmentPaths[1] = graphicsPath6;
				}
				else
				{
					graphicsPath2.AddLine(pointF, pointF2);
				}
				graphicsPath2.AddLine(pointF2, new PointF(pointF4.X - num2, pointF4.Y));
				graphicsPath2.AddLine(new PointF(pointF4.X - num2, pointF4.Y), new PointF(pointF3.X - num2, pointF3.Y));
				graphicsPath2.AddLine(new PointF(pointF3.X - num2, pointF3.Y), pointF);
				graphicsPath.AddPath(graphicsPath2, true);
			}
			return graphicsPath;
		}

		// Token: 0x0400422E RID: 16942
		protected PointF axisSegmentPointStart;

		// Token: 0x0400422F RID: 16943
		protected PointF axisSegmentPointEnd;

		// Token: 0x04004230 RID: 16944
		protected RectangleF axisSegmentRectangle;

		// Token: 0x04004231 RID: 16945
		internal ChartAxisVisibleValues axisSegmentVisibleValues;

		// Token: 0x04004232 RID: 16946
		internal int axisSegmentItemsCount;

		// Token: 0x04004233 RID: 16947
		internal GraphicsPath[] axisSegmentPaths;

		// Token: 0x04004234 RID: 16948
		internal int axisSegmentPercent;
	}
}
