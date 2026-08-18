using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000010 RID: 16
	public class MyPictureBox : PictureBox
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00003D80 File Offset: 0x00002D80
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003DB7 File Offset: 0x00002DB7
		private void InitializeComponent()
		{
			this.components = new Container();
			this.SizeMode = MyPictureBox.PhotoBoxSizeMode.AutoSize;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003DD0 File Offset: 0x00002DD0
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003DE8 File Offset: 0x00002DE8
		[DefaultValue(MyPictureBox.PhotoBoxSizeMode.ScaleImage)]
		[Category("Behavior")]
		[Description("Controls how the image is drawn within the control.")]
		public new MyPictureBox.PhotoBoxSizeMode SizeMode
		{
			get
			{
				return this._sizeMode;
			}
			set
			{
				this._sizeMode = value;
				base.Invalidate();
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003DFC File Offset: 0x00002DFC
		private Rectangle ScaleToFit(Rectangle targetArea, Image img)
		{
			Rectangle result = new Rectangle(targetArea.Location, targetArea.Size);
			if (result.Height * img.Width > result.Width * img.Height)
			{
				result.Height = result.Width * img.Height / img.Width;
				result.Y += (targetArea.Height - result.Height) / 2;
			}
			else
			{
				result.Width = result.Height * img.Width / img.Height;
				result.X += (targetArea.Width - result.Width) / 2;
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003ECC File Offset: 0x00002ECC
		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.SizeMode == MyPictureBox.PhotoBoxSizeMode.ScaleImage)
			{
				base.SizeMode = PictureBoxSizeMode.Normal;
			}
			else
			{
				base.SizeMode = (PictureBoxSizeMode)this._sizeMode;
			}
			base.OnPaint(e);
			if (this.SizeMode == MyPictureBox.PhotoBoxSizeMode.ScaleImage && base.Image != null)
			{
				e.Graphics.Clear(SystemColors.Control);
				Rectangle rect = this.ScaleToFit(base.ClientRectangle, base.Image);
				e.Graphics.DrawImage(base.Image, rect);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003F5C File Offset: 0x00002F5C
		protected override void OnResize(EventArgs e)
		{
			if (this.SizeMode == MyPictureBox.PhotoBoxSizeMode.ScaleImage)
			{
				base.Invalidate();
			}
			base.OnResize(e);
		}

		// Token: 0x0400006A RID: 106
		private IContainer components = null;

		// Token: 0x0400006B RID: 107
		private MyPictureBox.PhotoBoxSizeMode _sizeMode = MyPictureBox.PhotoBoxSizeMode.ScaleImage;

		// Token: 0x02000011 RID: 17
		public enum PhotoBoxSizeMode
		{
			// Token: 0x0400006D RID: 109
			Normal,
			// Token: 0x0400006E RID: 110
			StretchImage,
			// Token: 0x0400006F RID: 111
			AutoSize,
			// Token: 0x04000070 RID: 112
			CenterImage,
			// Token: 0x04000071 RID: 113
			ScaleImage
		}
	}
}
