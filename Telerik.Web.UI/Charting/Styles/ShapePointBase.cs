using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001770 RID: 6000
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	internal class ShapePointBase : Component
	{
		// Token: 0x170046F8 RID: 18168
		// (get) Token: 0x0600EA17 RID: 59927 RVA: 0x00355387 File Offset: 0x00353587
		// (set) Token: 0x0600EA18 RID: 59928 RVA: 0x0035538F File Offset: 0x0035358F
		public float X
		{
			get
			{
				return this.shapePointBaseX;
			}
			set
			{
				this.shapePointBaseX = value;
			}
		}

		// Token: 0x170046F9 RID: 18169
		// (get) Token: 0x0600EA19 RID: 59929 RVA: 0x00355398 File Offset: 0x00353598
		// (set) Token: 0x0600EA1A RID: 59930 RVA: 0x003553A0 File Offset: 0x003535A0
		public float Y
		{
			get
			{
				return this.shapePointBaseY;
			}
			set
			{
				this.shapePointBaseY = value;
			}
		}

		// Token: 0x170046FA RID: 18170
		// (get) Token: 0x0600EA1B RID: 59931 RVA: 0x003553A9 File Offset: 0x003535A9
		// (set) Token: 0x0600EA1C RID: 59932 RVA: 0x003553B1 File Offset: 0x003535B1
		public AnchorStyles Anchor
		{
			get
			{
				return this.anchor;
			}
			set
			{
				this.anchor = value;
			}
		}

		// Token: 0x170046FB RID: 18171
		// (get) Token: 0x0600EA1D RID: 59933 RVA: 0x003553BA File Offset: 0x003535BA
		// (set) Token: 0x0600EA1E RID: 59934 RVA: 0x003553C2 File Offset: 0x003535C2
		public bool Locked
		{
			get
			{
				return this.locked;
			}
			set
			{
				this.locked = value;
			}
		}

		// Token: 0x0600EA1F RID: 59935 RVA: 0x003553CB File Offset: 0x003535CB
		public ShapePointBase()
		{
		}

		// Token: 0x0600EA20 RID: 59936 RVA: 0x003553D3 File Offset: 0x003535D3
		public ShapePointBase(float x, float y)
		{
			this.shapePointBaseX = x;
			this.shapePointBaseY = y;
		}

		// Token: 0x0600EA21 RID: 59937 RVA: 0x003553E9 File Offset: 0x003535E9
		public ShapePointBase(Point point)
		{
			this.shapePointBaseX = (float)point.X;
			this.shapePointBaseY = (float)point.Y;
		}

		// Token: 0x0600EA22 RID: 59938 RVA: 0x0035540D File Offset: 0x0035360D
		public ShapePointBase(ShapePointBase point)
		{
			this.shapePointBaseX = point.X;
			this.shapePointBaseY = point.Y;
			this.anchor = point.Anchor;
			this.locked = point.Locked;
		}

		// Token: 0x0600EA23 RID: 59939 RVA: 0x00355445 File Offset: 0x00353645
		internal void Set(float x, float y)
		{
			this.shapePointBaseX = x;
			this.shapePointBaseY = y;
		}

		// Token: 0x0600EA24 RID: 59940 RVA: 0x00355455 File Offset: 0x00353655
		internal void Set(Point point)
		{
			this.Set((float)point.X, (float)point.Y);
		}

		// Token: 0x0600EA25 RID: 59941 RVA: 0x0035546D File Offset: 0x0035366D
		internal Point GetPoint()
		{
			return new Point((int)this.shapePointBaseX, (int)this.shapePointBaseY);
		}

		// Token: 0x0600EA26 RID: 59942 RVA: 0x00355482 File Offset: 0x00353682
		internal Point GetPoint(Rectangle bounds)
		{
			return new Point(bounds.X + (int)this.shapePointBaseX, bounds.Y + (int)this.shapePointBaseY);
		}

		// Token: 0x0600EA27 RID: 59943 RVA: 0x003554A8 File Offset: 0x003536A8
		internal Point GetPoint(Rectangle src, Rectangle dst)
		{
			double num = (double)(this.shapePointBaseX - (float)src.X);
			double num2 = (double)(this.shapePointBaseY - (float)src.Y);
			double num3 = num / (double)src.Width;
			double num4 = num2 / (double)src.Height;
			double a;
			if ((this.anchor & AnchorStyles.Left) != AnchorStyles.None)
			{
				a = (double)dst.X + num;
			}
			else if ((this.anchor & AnchorStyles.Right) != AnchorStyles.None)
			{
				a = (double)dst.Right - ((double)src.Width - num);
			}
			else
			{
				a = (double)dst.X + num3 * (double)dst.Width;
			}
			double a2;
			if ((this.anchor & AnchorStyles.Top) != AnchorStyles.None)
			{
				a2 = (double)dst.Y + num2;
			}
			else if ((this.anchor & AnchorStyles.Bottom) != AnchorStyles.None)
			{
				a2 = (double)dst.Bottom - ((double)src.Height - num2);
			}
			else
			{
				a2 = (double)dst.Y + num4 * (double)dst.Height;
			}
			return new Point((int)Math.Round(a), (int)Math.Round(a2));
		}

		// Token: 0x0600EA28 RID: 59944 RVA: 0x0035559E File Offset: 0x0035379E
		internal Rectangle GetBounds(int weight)
		{
			return new Rectangle((int)(this.shapePointBaseX - (float)(weight / 4)), (int)(this.shapePointBaseY - (float)(weight / 4)), weight, weight);
		}

		// Token: 0x0600EA29 RID: 59945 RVA: 0x003555C0 File Offset: 0x003537C0
		internal bool IsVisible(int x, int y, int width)
		{
			return this.GetBounds(width).Contains(x, y);
		}

		// Token: 0x0600EA2A RID: 59946 RVA: 0x003555DE File Offset: 0x003537DE
		public override string ToString()
		{
			return string.Format("Point: {0},{1}", this.shapePointBaseX, this.shapePointBaseY);
		}

		// Token: 0x0400436D RID: 17261
		protected float shapePointBaseX;

		// Token: 0x0400436E RID: 17262
		protected float shapePointBaseY;

		// Token: 0x0400436F RID: 17263
		private AnchorStyles anchor;

		// Token: 0x04004370 RID: 17264
		private bool locked;

		// Token: 0x04004371 RID: 17265
		internal bool Selected;
	}
}
