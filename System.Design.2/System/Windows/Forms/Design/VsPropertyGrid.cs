using System;
using System.Drawing;
using System.IO;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200036C RID: 876
	internal class VsPropertyGrid : PropertyGrid
	{
		// Token: 0x060023E6 RID: 9190 RVA: 0x000E029C File Offset: 0x000DE49C
		public VsPropertyGrid(IServiceProvider serviceProvider)
		{
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x000E02A4 File Offset: 0x000DE4A4
		protected override Bitmap SortByPropertyImage
		{
			get
			{
				return this.GetBitmap("PBAlpha", false);
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x000E02B2 File Offset: 0x000DE4B2
		protected override Bitmap SortByCategoryImage
		{
			get
			{
				return this.GetBitmap("PBCatego", true);
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x000E02C0 File Offset: 0x000DE4C0
		protected override Bitmap ShowPropertyPageImage
		{
			get
			{
				return this.GetBitmap("PBPPage", false);
			}
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000E02D0 File Offset: 0x000DE4D0
		private Bitmap GetBitmap(string resourceName, bool setMagentaTransparent = false)
		{
			Stream resourceStream = BitmapSelector.GetResourceStream(typeof(PropertyGrid), resourceName + ".ico");
			Bitmap bitmap;
			if (resourceStream != null)
			{
				if (!VsPropertyGrid.IsScalingInitialized)
				{
					if (DpiHelper.IsScalingRequired)
					{
						VsPropertyGrid.IconSize = DpiHelper.LogicalToDeviceUnits(VsPropertyGrid.ICON_SIZE, 0);
					}
					VsPropertyGrid.IsScalingInitialized = true;
				}
				Icon icon = new Icon(resourceStream, VsPropertyGrid.IconSize);
				bitmap = icon.ToBitmap();
				icon.Dispose();
			}
			else
			{
				bitmap = new Bitmap(BitmapSelector.GetResourceStream(typeof(PropertyGrid), resourceName + ".bmp"));
				if (setMagentaTransparent)
				{
					bitmap.MakeTransparent(Color.Magenta);
				}
			}
			return bitmap;
		}

		// Token: 0x04001A44 RID: 6724
		private static readonly Size ICON_SIZE = new Size(16, 16);

		// Token: 0x04001A45 RID: 6725
		private static Size IconSize = VsPropertyGrid.ICON_SIZE;

		// Token: 0x04001A46 RID: 6726
		private static bool IsScalingInitialized = false;
	}
}
