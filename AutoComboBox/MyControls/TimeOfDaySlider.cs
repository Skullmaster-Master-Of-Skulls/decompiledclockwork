using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000060 RID: 96
	public class TimeOfDaySlider : UserControl
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0001B53C File Offset: 0x0001A53C
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0001B554 File Offset: 0x0001A554
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
				base.Invalidate();
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0001B568 File Offset: 0x0001A568
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0001B580 File Offset: 0x0001A580
		public DateTime EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
				base.Invalidate();
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0001B594 File Offset: 0x0001A594
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0001B5AC File Offset: 0x0001A5AC
		public Color SliderForeColour
		{
			get
			{
				return this.sliderForeColour;
			}
			set
			{
				this.sliderForeColour = value;
				base.Invalidate();
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0001B5C0 File Offset: 0x0001A5C0
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0001B5D8 File Offset: 0x0001A5D8
		public Color SliderBackColour
		{
			get
			{
				return this.sliderBackColour;
			}
			set
			{
				this.sliderBackColour = value;
				base.Invalidate();
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001B5E9 File Offset: 0x0001A5E9
		public TimeOfDaySlider()
		{
			this.InitializeComponent();
			this.sliderForeColour = Color.Green;
			this.sliderBackColour = Color.White;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001B618 File Offset: 0x0001A618
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			using (Brush brush = new SolidBrush(this.sliderBackColour))
			{
				e.Graphics.FillRectangle(brush, base.Bounds);
			}
			if (this.endTime > this.startTime)
			{
				double num = Convert.ToDouble(base.Bounds.Width) / 1440.0;
				if (num > 0.0)
				{
					using (Brush brush = new SolidBrush(this.sliderForeColour))
					{
						int num2 = this.startTime.Hour * 60 + this.startTime.Minute;
						int num3 = this.endTime.Hour * 60 + this.endTime.Minute;
						float x = (float)((double)num2 * num);
						float width = (float)((double)(num3 - num2) * num);
						e.Graphics.FillRectangle(brush, x, 0f, width, (float)base.Bounds.Height);
					}
				}
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001B768 File Offset: 0x0001A768
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001B7A0 File Offset: 0x0001A7A0
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "TimeOfDaySlider";
			base.Size = new Size(376, 19);
			base.ResumeLayout(false);
		}

		// Token: 0x0400034E RID: 846
		private DateTime startTime;

		// Token: 0x0400034F RID: 847
		private DateTime endTime;

		// Token: 0x04000350 RID: 848
		private Color sliderForeColour;

		// Token: 0x04000351 RID: 849
		private Color sliderBackColour;

		// Token: 0x04000352 RID: 850
		private IContainer components = null;
	}
}
