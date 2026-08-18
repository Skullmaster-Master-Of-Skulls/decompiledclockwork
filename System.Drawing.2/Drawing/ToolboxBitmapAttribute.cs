using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace System.Drawing
{
	// Token: 0x02000038 RID: 56
	[AttributeUsage(AttributeTargets.Class)]
	public class ToolboxBitmapAttribute : Attribute
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x000182A6 File Offset: 0x000164A6
		public ToolboxBitmapAttribute(string imageFile) : this(ToolboxBitmapAttribute.GetImageFromFile(imageFile, false, true), ToolboxBitmapAttribute.GetImageFromFile(imageFile, true, true))
		{
			this.imageFile = imageFile;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000182C5 File Offset: 0x000164C5
		public ToolboxBitmapAttribute(Type t) : this(ToolboxBitmapAttribute.GetImageFromResource(t, null, false), ToolboxBitmapAttribute.GetImageFromResource(t, null, true))
		{
			this.imageType = t;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000182E4 File Offset: 0x000164E4
		public ToolboxBitmapAttribute(Type t, string name) : this(ToolboxBitmapAttribute.GetImageFromResource(t, name, false), ToolboxBitmapAttribute.GetImageFromResource(t, name, true))
		{
			this.imageType = t;
			this.imageName = name;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001830A File Offset: 0x0001650A
		private ToolboxBitmapAttribute(Image smallImage, Image largeImage)
		{
			this.smallImage = smallImage;
			this.largeImage = largeImage;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00018320 File Offset: 0x00016520
		public override bool Equals(object value)
		{
			if (value == this)
			{
				return true;
			}
			ToolboxBitmapAttribute toolboxBitmapAttribute = value as ToolboxBitmapAttribute;
			return toolboxBitmapAttribute != null && toolboxBitmapAttribute.smallImage == this.smallImage && toolboxBitmapAttribute.largeImage == this.largeImage;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001835D File Offset: 0x0001655D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00018365 File Offset: 0x00016565
		public Image GetImage(object component)
		{
			return this.GetImage(component, true);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001836F File Offset: 0x0001656F
		public Image GetImage(object component, bool large)
		{
			if (component != null)
			{
				return this.GetImage(component.GetType(), large);
			}
			return null;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00018383 File Offset: 0x00016583
		public Image GetImage(Type type)
		{
			return this.GetImage(type, false);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001838D File Offset: 0x0001658D
		public Image GetImage(Type type, bool large)
		{
			return this.GetImage(type, null, large);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00018398 File Offset: 0x00016598
		public Image GetImage(Type type, string imgName, bool large)
		{
			if ((large && this.largeImage == null) || (!large && this.smallImage == null))
			{
				Image image;
				if (large)
				{
					image = this.largeImage;
				}
				else
				{
					image = this.smallImage;
				}
				if (image == null)
				{
					image = ToolboxBitmapAttribute.GetImageFromResource(type, imgName, large);
				}
				if (large && this.largeImage == null && this.smallImage != null)
				{
					image = new Bitmap((Bitmap)this.smallImage, ToolboxBitmapAttribute.largeSize.Width, ToolboxBitmapAttribute.largeSize.Height);
				}
				Bitmap bitmap = image as Bitmap;
				if (bitmap != null)
				{
					ToolboxBitmapAttribute.MakeBackgroundAlphaZero(bitmap);
				}
				if (image == null)
				{
					image = ToolboxBitmapAttribute.DefaultComponent.GetImage(type, large);
				}
				if (large)
				{
					this.largeImage = image;
				}
				else
				{
					this.smallImage = image;
				}
			}
			Image result = large ? this.largeImage : this.smallImage;
			if (this.Equals(ToolboxBitmapAttribute.Default))
			{
				this.largeImage = null;
				this.smallImage = null;
			}
			return result;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00018484 File Offset: 0x00016684
		internal Bitmap GetOriginalBitmap()
		{
			if (this.originalBitmap != null)
			{
				return this.originalBitmap;
			}
			if (this.smallImage == null)
			{
				return null;
			}
			if (!DpiHelper.IsScalingRequired)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(this.imageFile))
			{
				this.originalBitmap = (ToolboxBitmapAttribute.GetImageFromFile(this.imageFile, false, false) as Bitmap);
			}
			else if (this.imageType != null)
			{
				this.originalBitmap = (ToolboxBitmapAttribute.GetImageFromResource(this.imageType, this.imageName, false, false) as Bitmap);
			}
			return this.originalBitmap;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001850C File Offset: 0x0001670C
		private static Image GetIconFromStream(Stream stream, bool large, bool scaled)
		{
			if (stream == null)
			{
				return null;
			}
			Icon original = new Icon(stream);
			Icon icon = new Icon(original, large ? ToolboxBitmapAttribute.largeSize : ToolboxBitmapAttribute.smallSize);
			Bitmap result = icon.ToBitmap();
			if (DpiHelper.IsScalingRequired && scaled)
			{
				DpiHelper.ScaleBitmapLogicalToDevice(ref result, 0);
			}
			return result;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00018554 File Offset: 0x00016754
		private static string GetFileNameFromBitmapSelector(string originalName)
		{
			if (originalName != ToolboxBitmapAttribute.lastOriginalFileName)
			{
				ToolboxBitmapAttribute.lastOriginalFileName = originalName;
				ToolboxBitmapAttribute.lastUpdatedFileName = BitmapSelector.GetFileName(originalName);
			}
			return ToolboxBitmapAttribute.lastUpdatedFileName;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001857C File Offset: 0x0001677C
		private static Image GetImageFromFile(string imageFile, bool large, bool scaled = true)
		{
			Image image = null;
			try
			{
				if (imageFile != null)
				{
					imageFile = ToolboxBitmapAttribute.GetFileNameFromBitmapSelector(imageFile);
					string extension = Path.GetExtension(imageFile);
					if (extension != null && string.Equals(extension, ".ico", StringComparison.OrdinalIgnoreCase))
					{
						FileStream fileStream = File.Open(imageFile, FileMode.Open);
						if (fileStream == null)
						{
							goto IL_64;
						}
						try
						{
							image = ToolboxBitmapAttribute.GetIconFromStream(fileStream, large, scaled);
							goto IL_64;
						}
						finally
						{
							fileStream.Close();
						}
					}
					if (!large)
					{
						image = Image.FromFile(imageFile);
						Bitmap bitmap = image as Bitmap;
						if (DpiHelper.IsScalingRequired && scaled)
						{
							DpiHelper.ScaleBitmapLogicalToDevice(ref bitmap, 0);
						}
					}
				}
				IL_64:;
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			return image;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001861C File Offset: 0x0001681C
		private static Image GetBitmapFromResource(Type t, string bitmapname, bool large, bool scaled)
		{
			if (bitmapname == null)
			{
				return null;
			}
			Image image = null;
			Stream resourceStream = BitmapSelector.GetResourceStream(t, bitmapname);
			if (resourceStream != null)
			{
				Bitmap bitmap = new Bitmap(resourceStream);
				image = bitmap;
				ToolboxBitmapAttribute.MakeBackgroundAlphaZero(bitmap);
				if (large)
				{
					image = new Bitmap(bitmap, ToolboxBitmapAttribute.largeSize.Width, ToolboxBitmapAttribute.largeSize.Height);
				}
				if (DpiHelper.IsScalingRequired && scaled)
				{
					bitmap = (Bitmap)image;
					DpiHelper.ScaleBitmapLogicalToDevice(ref bitmap, 0);
					image = bitmap;
				}
			}
			return image;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00018689 File Offset: 0x00016889
		private static Image GetIconFromResource(Type t, string bitmapname, bool large, bool scaled)
		{
			if (bitmapname == null)
			{
				return null;
			}
			return ToolboxBitmapAttribute.GetIconFromStream(BitmapSelector.GetResourceStream(t, bitmapname), large, scaled);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001869E File Offset: 0x0001689E
		public static Image GetImageFromResource(Type t, string imageName, bool large)
		{
			return ToolboxBitmapAttribute.GetImageFromResource(t, imageName, large, true);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000186AC File Offset: 0x000168AC
		internal static Image GetImageFromResource(Type t, string imageName, bool large, bool scaled)
		{
			Image image = null;
			try
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				if (imageName == null)
				{
					string text4 = t.FullName;
					int num = text4.LastIndexOf('.');
					if (num != -1)
					{
						text4 = text4.Substring(num + 1);
					}
					text = text4 + ".ico";
					text2 = text4 + ".bmp";
				}
				else if (string.Compare(Path.GetExtension(imageName), ".ico", true, CultureInfo.CurrentCulture) == 0)
				{
					text = imageName;
				}
				else if (string.Compare(Path.GetExtension(imageName), ".bmp", true, CultureInfo.CurrentCulture) == 0)
				{
					text2 = imageName;
				}
				else
				{
					text3 = imageName;
					text2 = imageName + ".bmp";
					text = imageName + ".ico";
				}
				if (text3 != null)
				{
					image = ToolboxBitmapAttribute.GetBitmapFromResource(t, text3, large, scaled);
				}
				if (image == null && text2 != null)
				{
					image = ToolboxBitmapAttribute.GetBitmapFromResource(t, text2, large, scaled);
				}
				if (image == null && text != null)
				{
					image = ToolboxBitmapAttribute.GetIconFromResource(t, text, large, scaled);
				}
			}
			catch (Exception ex)
			{
				t == null;
			}
			return image;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000187A4 File Offset: 0x000169A4
		private static void MakeBackgroundAlphaZero(Bitmap img)
		{
			Color pixel = img.GetPixel(0, img.Height - 1);
			img.MakeTransparent();
			Color color = Color.FromArgb(0, pixel);
			img.SetPixel(0, img.Height - 1, color);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000187E0 File Offset: 0x000169E0
		static ToolboxBitmapAttribute()
		{
			SafeNativeMethods.Gdip.DummyFunction();
			Bitmap img = null;
			Stream resourceStream = BitmapSelector.GetResourceStream(typeof(ToolboxBitmapAttribute), "DefaultComponent.bmp");
			if (resourceStream != null)
			{
				img = new Bitmap(resourceStream);
				ToolboxBitmapAttribute.MakeBackgroundAlphaZero(img);
			}
			ToolboxBitmapAttribute.DefaultComponent = new ToolboxBitmapAttribute(img, null);
		}

		// Token: 0x04000329 RID: 809
		private Image smallImage;

		// Token: 0x0400032A RID: 810
		private Image largeImage;

		// Token: 0x0400032B RID: 811
		private Bitmap originalBitmap;

		// Token: 0x0400032C RID: 812
		private string imageFile;

		// Token: 0x0400032D RID: 813
		private Type imageType;

		// Token: 0x0400032E RID: 814
		private string imageName;

		// Token: 0x0400032F RID: 815
		private static readonly Size largeSize = new Size(32, 32);

		// Token: 0x04000330 RID: 816
		private static readonly Size smallSize = new Size(16, 16);

		// Token: 0x04000331 RID: 817
		private static string lastOriginalFileName;

		// Token: 0x04000332 RID: 818
		private static string lastUpdatedFileName;

		// Token: 0x04000333 RID: 819
		public static readonly ToolboxBitmapAttribute Default = new ToolboxBitmapAttribute(null, null);

		// Token: 0x04000334 RID: 820
		private static readonly ToolboxBitmapAttribute DefaultComponent;
	}
}
