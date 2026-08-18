using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace a.b
{
	// Token: 0x02000347 RID: 839
	internal class a6 : en
	{
		// Token: 0x06001E54 RID: 7764 RVA: 0x00081E12 File Offset: 0x00080E12
		public a6() : this(new i0())
		{
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00081E1F File Offset: 0x00080E1F
		public a6(i0 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.b = A_0;
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x00081E47 File Offset: 0x00080E47
		public i0 a()
		{
			return this.b;
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00081E4F File Offset: 0x00080E4F
		public go b()
		{
			return this.a;
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x00081E57 File Offset: 0x00080E57
		protected override void db(eq A_0)
		{
			base.db(A_0);
			this.a.a();
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x00081E6C File Offset: 0x00080E6C
		protected override void dc(eq A_0, de A_1, int A_2, int A_3, int A_4, int A_5, int A_6, int A_7, string A_8)
		{
			int a_ = this.a.Count + 1;
			string text = this.b.a(a_, A_1);
			this.a(text);
			byte[] array = gt.a(A_8);
			ImageFormat a_2;
			Size size;
			if (this.b.f().gf() == null)
			{
				using (Image image = Image.FromStream(new MemoryStream(array)))
				{
					a_2 = image.RawFormat;
					size = image.Size;
				}
				using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(text, FileMode.Create)))
				{
					binaryWriter.Write(array);
					goto IL_F6;
				}
			}
			a_2 = this.b.f().gf();
			if (this.b.a())
			{
				size = new Size(this.b.f().gk(A_1, A_2, A_4, A_6), this.b.f().gl(A_1, A_3, A_5, A_7));
			}
			else
			{
				size = new Size(A_2, A_3);
			}
			this.a(array, A_1, text, size);
			IL_F6:
			this.a.a(new am(text, a_2, size));
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00081FA0 File Offset: 0x00080FA0
		protected virtual void a(byte[] A_0, de A_1, string A_2, Size A_3)
		{
			ImageFormat format = this.b.f().gf();
			float num = this.b.e();
			float num2 = this.b.c();
			using (Image image = Image.FromStream(new MemoryStream(A_0, 0, A_0.Length)))
			{
				Bitmap bitmap = new Bitmap(new Bitmap(A_3.Width, A_3.Height, image.PixelFormat));
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				RectangleF rect = new RectangleF(num, num, (float)A_3.Width + num2, (float)A_3.Height + num2);
				if (this.b.d() != null)
				{
					graphics.Clear(this.b.d().Value);
				}
				graphics.DrawImage(image, rect);
				bitmap.Save(A_2, format);
			}
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x000820A0 File Offset: 0x000810A0
		protected virtual void a(string A_0)
		{
			FileInfo fileInfo = new FileInfo(A_0);
			if (!string.IsNullOrEmpty(fileInfo.DirectoryName) && !Directory.Exists(fileInfo.DirectoryName))
			{
				Directory.CreateDirectory(fileInfo.DirectoryName);
			}
		}

		// Token: 0x040013D4 RID: 5076
		private readonly go a = new go();

		// Token: 0x040013D5 RID: 5077
		private readonly i0 b;
	}
}
