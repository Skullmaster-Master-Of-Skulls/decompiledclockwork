using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001776 RID: 6006
	[ToolboxItem(false)]
	[ComVisible(false)]
	internal class RoundRectShape : ElementShape
	{
		// Token: 0x170046FF RID: 18175
		// (get) Token: 0x0600EA46 RID: 59974 RVA: 0x00356384 File Offset: 0x00354584
		// (set) Token: 0x0600EA47 RID: 59975 RVA: 0x0035638C File Offset: 0x0035458C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Radius
		{
			get
			{
				return this.radius;
			}
			set
			{
				this.radius = value;
			}
		}

		// Token: 0x0600EA48 RID: 59976 RVA: 0x00356395 File Offset: 0x00354595
		public RoundRectShape()
		{
			this.radius = 5;
		}

		// Token: 0x0600EA49 RID: 59977 RVA: 0x003563C0 File Offset: 0x003545C0
		public RoundRectShape(int radius)
		{
			this.radius = radius;
		}

		// Token: 0x0600EA4A RID: 59978 RVA: 0x003563EC File Offset: 0x003545EC
		internal override GraphicsPath CreatePath(Rectangle bounds)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			if ((float)this.Radius <= 0f)
			{
				graphicsPath.AddRectangle(bounds);
				graphicsPath.CloseFigure();
				return graphicsPath;
			}
			float num;
			RectangleF rect;
			if ((double)this.Radius >= (double)Math.Min(bounds.Width, bounds.Height) / 2.0)
			{
				try
				{
					if (bounds.Width > bounds.Height)
					{
						num = (float)bounds.Height;
						rect = new RectangleF((float)bounds.Location.X, (float)bounds.Location.Y, num, num);
						if (rect.Size != SizeF.Empty)
						{
							graphicsPath.AddArc(rect, 90f, 180f);
							rect.X = (float)bounds.Right - num;
							graphicsPath.AddArc(rect, 270f, 180f);
						}
						else
						{
							graphicsPath.AddEllipse(bounds);
						}
					}
					else if (bounds.Width < bounds.Height)
					{
						num = (float)bounds.Width;
						rect = new RectangleF((float)bounds.Location.X, (float)bounds.Location.Y, num, num);
						if (rect.Size != SizeF.Empty)
						{
							graphicsPath.AddArc(rect, 180f, 180f);
							rect.Y = (float)bounds.Bottom - num;
							graphicsPath.AddArc(rect, 0f, 180f);
						}
						else
						{
							graphicsPath.AddEllipse(bounds);
						}
					}
					else
					{
						graphicsPath.AddEllipse(bounds);
					}
				}
				catch (Exception)
				{
					graphicsPath.AddEllipse(bounds);
				}
				finally
				{
					graphicsPath.CloseFigure();
				}
				return graphicsPath;
			}
			num = (float)this.Radius * 2f;
			rect = new RectangleF((float)bounds.Location.X, (float)bounds.Location.Y, num, num);
			float num2 = 2f;
			RectangleF rect2 = new RectangleF((float)bounds.Location.X, (float)bounds.Location.Y, num2, num2);
			if (this.isRightTopRound)
			{
				graphicsPath.AddArc(rect, 180f, 90f);
			}
			else
			{
				graphicsPath.AddArc(rect2, 180f, 90f);
			}
			rect.X = (float)bounds.Right - num;
			rect2.X = (float)bounds.Right - num2;
			if (this.isRightBottomRound)
			{
				graphicsPath.AddArc(rect, 270f, 90f);
			}
			else
			{
				graphicsPath.AddArc(rect2, 270f, 90f);
			}
			rect.Y = (float)bounds.Bottom - num;
			rect2.Y = (float)bounds.Bottom - num2;
			if (this.isLeftBottomRound)
			{
				graphicsPath.AddArc(rect, 0f, 90f);
			}
			else
			{
				graphicsPath.AddArc(rect2, 0f, 90f);
			}
			rect.X = (float)bounds.Left;
			rect2.X = (float)bounds.Left;
			if (this.isLeftTopRound)
			{
				graphicsPath.AddArc(rect, 90f, 90f);
			}
			else
			{
				graphicsPath.AddArc(rect2, 90f, 90f);
			}
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x0600EA4B RID: 59979 RVA: 0x00356754 File Offset: 0x00354954
		internal override string SerializeProperties()
		{
			string text = this.radius.ToString();
			if (!this.isLeftTopRound || !this.isRightTopRound || !this.isLeftBottomRound || !this.isRightBottomRound)
			{
				text = text + ", " + this.isLeftTopRound;
				text = text + ", " + this.isRightTopRound;
				text = text + ", " + this.isLeftBottomRound;
				text = text + ", " + this.isRightBottomRound;
			}
			return text;
		}

		// Token: 0x0600EA4C RID: 59980 RVA: 0x003567EC File Offset: 0x003549EC
		internal override void DeserializeProperties(string propertiesString)
		{
			if (string.IsNullOrEmpty(propertiesString))
			{
				return;
			}
			string[] array = propertiesString.Split(new char[]
			{
				','
			});
			if (array.Length > 0)
			{
				this.radius = int.Parse(array[0]);
			}
			if (array.Length > 1)
			{
				this.isLeftTopRound = bool.Parse(array[1]);
			}
			if (array.Length > 2)
			{
				this.isRightTopRound = bool.Parse(array[2]);
			}
			if (array.Length > 3)
			{
				this.isLeftBottomRound = bool.Parse(array[3]);
			}
			if (array.Length > 4)
			{
				this.isRightBottomRound = bool.Parse(array[4]);
			}
		}

		// Token: 0x04004386 RID: 17286
		private int radius;

		// Token: 0x04004387 RID: 17287
		private bool isLeftTopRound = true;

		// Token: 0x04004388 RID: 17288
		private bool isRightTopRound = true;

		// Token: 0x04004389 RID: 17289
		private bool isLeftBottomRound = true;

		// Token: 0x0400438A RID: 17290
		private bool isRightBottomRound = true;
	}
}
