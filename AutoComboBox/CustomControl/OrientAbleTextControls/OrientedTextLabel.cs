using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CustomControl.OrientAbleTextControls
{
	// Token: 0x020000C2 RID: 194
	public class OrientedTextLabel : Label
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x0003A98B File Offset: 0x0003998B
		public OrientedTextLabel()
		{
			this.rotationAngle = 0.0;
			this.textOrientation = Orientation.Rotate;
			base.Size = new Size(105, 12);
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0003A9BC File Offset: 0x000399BC
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0003A9D4 File Offset: 0x000399D4
		[Category("Appearance")]
		[Description("Rotation Angle")]
		public double RotationAngle
		{
			get
			{
				return this.rotationAngle;
			}
			set
			{
				this.rotationAngle = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0003A9E8 File Offset: 0x000399E8
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0003AA00 File Offset: 0x00039A00
		[Category("Appearance")]
		[Description("Kind of Text Orientation")]
		public Orientation TextOrientation
		{
			get
			{
				return this.textOrientation;
			}
			set
			{
				this.textOrientation = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0003AA14 File Offset: 0x00039A14
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0003AA2C File Offset: 0x00039A2C
		[Category("Appearance")]
		[Description("Direction of the Text")]
		public Direction TextDirection
		{
			get
			{
				return this.textDirection;
			}
			set
			{
				this.textDirection = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0003AA40 File Offset: 0x00039A40
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0003AA58 File Offset: 0x00039A58
		[Category("Appearance")]
		[Description("Display Text")]
		public override string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
				base.Invalidate();
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0003AA6C File Offset: 0x00039A6C
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.Trimming = StringTrimming.None;
			Brush brush = new SolidBrush(this.ForeColor);
			float width = graphics.MeasureString(this.text, this.Font).Width;
			float height = graphics.MeasureString(this.text, this.Font).Height;
			float num;
			if (base.ClientRectangle.Width < base.ClientRectangle.Height)
			{
				num = (float)base.ClientRectangle.Width * 0.9f / 2f;
			}
			else
			{
				num = (float)base.ClientRectangle.Height * 0.9f / 2f;
			}
			switch (this.textOrientation)
			{
			case Orientation.Circle:
				if (this.textDirection == Direction.Clockwise)
				{
					for (int i = 0; i < this.text.Length; i++)
					{
						graphics.TranslateTransform((float)((double)num * (1.0 - Math.Cos(6.283185307179586 / (double)this.text.Length * (double)i + this.rotationAngle / 180.0 * 3.141592653589793))), (float)((double)num * (1.0 - Math.Sin(6.283185307179586 / (double)this.text.Length * (double)i + this.rotationAngle / 180.0 * 3.141592653589793))));
						graphics.RotateTransform(-90f + (float)this.rotationAngle + (float)(360 / this.text.Length * i));
						graphics.DrawString(this.text[i].ToString(), this.Font, brush, 0f, 0f);
						graphics.ResetTransform();
					}
				}
				else
				{
					for (int i = 0; i < this.text.Length; i++)
					{
						graphics.TranslateTransform((float)((double)num * (1.0 - Math.Cos(6.283185307179586 / (double)this.text.Length * (double)i + this.rotationAngle / 180.0 * 3.141592653589793))), (float)((double)num * (1.0 + Math.Sin(6.283185307179586 / (double)this.text.Length * (double)i + this.rotationAngle / 180.0 * 3.141592653589793))));
						graphics.RotateTransform(-90f - (float)this.rotationAngle - (float)(360 / this.text.Length * i));
						graphics.DrawString(this.text[i].ToString(), this.Font, brush, 0f, 0f);
						graphics.ResetTransform();
					}
				}
				break;
			case Orientation.Arc:
			{
				float num2 = 2f * width / num / (float)this.text.Length;
				if (this.textDirection == Direction.Clockwise)
				{
					for (int i = 0; i < this.text.Length; i++)
					{
						graphics.TranslateTransform((float)((double)num * (1.0 - Math.Cos((double)(num2 * (float)i) + this.rotationAngle / 180.0 * 3.141592653589793))), (float)((double)num * (1.0 - Math.Sin((double)(num2 * (float)i) + this.rotationAngle / 180.0 * 3.141592653589793))));
						graphics.RotateTransform(-90f + (float)this.rotationAngle + 180f * num2 * (float)i / 3.1415927f);
						graphics.DrawString(this.text[i].ToString(), this.Font, brush, 0f, 0f);
						graphics.ResetTransform();
					}
				}
				else
				{
					for (int i = 0; i < this.text.Length; i++)
					{
						graphics.TranslateTransform((float)((double)num * (1.0 - Math.Cos((double)(num2 * (float)i) + this.rotationAngle / 180.0 * 3.141592653589793))), (float)((double)num * (1.0 + Math.Sin((double)(num2 * (float)i) + this.rotationAngle / 180.0 * 3.141592653589793))));
						graphics.RotateTransform(-90f - (float)this.rotationAngle - 180f * num2 * (float)i / 3.1415927f);
						graphics.DrawString(this.text[i].ToString(), this.Font, brush, 0f, 0f);
						graphics.ResetTransform();
					}
				}
				break;
			}
			case Orientation.Rotate:
			{
				double num3 = this.rotationAngle / 180.0 * 3.141592653589793;
				graphics.TranslateTransform(((float)base.ClientRectangle.Width + (float)((double)height * Math.Sin(num3)) - (float)((double)width * Math.Cos(num3))) / 2f, ((float)base.ClientRectangle.Height - (float)((double)height * Math.Cos(num3)) - (float)((double)width * Math.Sin(num3))) / 2f);
				graphics.RotateTransform((float)this.rotationAngle);
				graphics.DrawString(this.text, this.Font, brush, 0f, 0f);
				graphics.ResetTransform();
				break;
			}
			}
		}

		// Token: 0x040005A2 RID: 1442
		private double rotationAngle;

		// Token: 0x040005A3 RID: 1443
		private string text;

		// Token: 0x040005A4 RID: 1444
		private Orientation textOrientation;

		// Token: 0x040005A5 RID: 1445
		private Direction textDirection;
	}
}
