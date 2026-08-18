using System;
using System.Drawing;
using System.IO;

namespace WebGrease.ImageAssemble
{
	// Token: 0x02000100 RID: 256
	internal class BitmapContainer
	{
		// Token: 0x06001064 RID: 4196 RVA: 0x00049CAC File Offset: 0x00047EAC
		internal BitmapContainer(InputImage inputImage)
		{
			this.InputImage = inputImage;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00049CBB File Offset: 0x00047EBB
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x00049CC3 File Offset: 0x00047EC3
		internal InputImage InputImage { get; private set; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00049CCC File Offset: 0x00047ECC
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x00049CD4 File Offset: 0x00047ED4
		internal Bitmap Bitmap
		{
			get
			{
				return this.bitmap;
			}
			set
			{
				this.bitmap = value;
				if (value != null)
				{
					this.Width = value.Width;
					this.Height = value.Height;
					return;
				}
				this.Width = 0;
				this.Height = 0;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00049D07 File Offset: 0x00047F07
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x00049D0F File Offset: 0x00047F0F
		internal int Width { get; private set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00049D18 File Offset: 0x00047F18
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x00049D20 File Offset: 0x00047F20
		internal int Height { get; private set; }

		// Token: 0x0600106D RID: 4205 RVA: 0x00049D4C File Offset: 0x00047F4C
		public void BitmapAction(Action<Bitmap> action)
		{
			Safe.FileLock(new FileInfo(this.InputImage.AbsoluteImagePath), int.MaxValue, delegate()
			{
				action(this.Bitmap);
			});
		}

		// Token: 0x04000662 RID: 1634
		private Bitmap bitmap;
	}
}
