using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Drawing.Internal;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Drawing
{
	// Token: 0x0200000E RID: 14
	[Editor("System.Drawing.Design.BitmapEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ComVisible(true)]
	[Serializable]
	public sealed class Bitmap : Image
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002A98 File Offset: 0x00000C98
		public Bitmap(string filename)
		{
			IntSecurity.DemandReadFileIO(filename);
			filename = Path.GetFullPath(filename);
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateBitmapFromFile(filename, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			num = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, zero));
			if (num != 0)
			{
				SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, zero));
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeImage(zero);
			Image.EnsureSave(this, filename, null);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B08 File Offset: 0x00000D08
		public Bitmap(string filename, bool useIcm)
		{
			IntSecurity.DemandReadFileIO(filename);
			filename = Path.GetFullPath(filename);
			IntPtr zero = IntPtr.Zero;
			int num;
			if (useIcm)
			{
				num = SafeNativeMethods.Gdip.GdipCreateBitmapFromFileICM(filename, out zero);
			}
			else
			{
				num = SafeNativeMethods.Gdip.GdipCreateBitmapFromFile(filename, out zero);
			}
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			num = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, zero));
			if (num != 0)
			{
				SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, zero));
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeImage(zero);
			Image.EnsureSave(this, filename, null);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B84 File Offset: 0x00000D84
		public Bitmap(Type type, string resource)
		{
			Stream manifestResourceStream = type.Module.Assembly.GetManifestResourceStream(type, resource);
			if (manifestResourceStream == null)
			{
				throw new ArgumentException(SR.GetString("ResourceNotFound", new object[]
				{
					type,
					resource
				}));
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateBitmapFromStream(new GPStream(manifestResourceStream), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			num = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, zero));
			if (num != 0)
			{
				SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, zero));
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeImage(zero);
			Image.EnsureSave(this, null, manifestResourceStream);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002C1C File Offset: 0x00000E1C
		public Bitmap(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"stream",
					"null"
				}));
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateBitmapFromStream(new GPStream(stream), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			num = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, zero));
			if (num != 0)
			{
				SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, zero));
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeImage(zero);
			Image.EnsureSave(this, null, stream);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002CAC File Offset: 0x00000EAC
		public Bitmap(Stream stream, bool useIcm)
		{
			if (stream == null)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"stream",
					"null"
				}));
			}
			IntPtr zero = IntPtr.Zero;
			int num;
			if (useIcm)
			{
				num = SafeNativeMethods.Gdip.GdipCreateBitmapFromStreamICM(new GPStream(stream), out zero);
			}
			else
			{
				num = SafeNativeMethods.Gdip.GdipCreateBitmapFromStream(new GPStream(stream), out zero);
			}
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			num = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, zero));
			if (num != 0)
			{
				SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, zero));
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeImage(zero);
			Image.EnsureSave(this, null, stream);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002D4C File Offset: 0x00000F4C
		public Bitmap(int width, int height, int stride, PixelFormat format, IntPtr scan0)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateBitmapFromScan0(width, height, stride, (int)format, new HandleRef(null, scan0), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeImage(zero);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002D98 File Offset: 0x00000F98
		public Bitmap(int width, int height, PixelFormat format)
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateBitmapFromScan0(width, height, 0, (int)format, NativeMethods.NullHandleRef, out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeImage(zero);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002DD3 File Offset: 0x00000FD3
		public Bitmap(int width, int height) : this(width, height, PixelFormat.Format32bppArgb)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public Bitmap(int width, int height, Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException(SR.GetString("InvalidArgument", new object[]
				{
					"g",
					"null"
				}));
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateBitmapFromGraphics(width, height, new HandleRef(g, g.NativeGraphics), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeImage(zero);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002E4D File Offset: 0x0000104D
		public Bitmap(Image original) : this(original, original.Width, original.Height)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002E64 File Offset: 0x00001064
		public Bitmap(Image original, int width, int height) : this(width, height)
		{
			Graphics graphics = null;
			try
			{
				graphics = Graphics.FromImage(this);
				graphics.Clear(Color.Transparent);
				graphics.DrawImage(original, 0, 0, width, height);
			}
			finally
			{
				if (graphics != null)
				{
					graphics.Dispose();
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002EB4 File Offset: 0x000010B4
		private Bitmap(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002EC0 File Offset: 0x000010C0
		public static Bitmap FromHicon(IntPtr hicon)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateBitmapFromHICON(new HandleRef(null, hicon), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return Bitmap.FromGDIplus(zero);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002EFC File Offset: 0x000010FC
		public static Bitmap FromResource(IntPtr hinstance, string bitmapName)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			IntPtr intPtr = Marshal.StringToHGlobalUni(bitmapName);
			IntPtr handle;
			int num = SafeNativeMethods.Gdip.GdipCreateBitmapFromResource(new HandleRef(null, hinstance), new HandleRef(null, intPtr), out handle);
			Marshal.FreeHGlobal(intPtr);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return Bitmap.FromGDIplus(handle);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002F46 File Offset: 0x00001146
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public IntPtr GetHbitmap()
		{
			return this.GetHbitmap(Color.LightGray);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F54 File Offset: 0x00001154
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public IntPtr GetHbitmap(Color background)
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateHBITMAPFromBitmap(new HandleRef(this, this.nativeImage), out zero, ColorTranslator.ToWin32(background));
			if (num == 2 && (base.Width >= 32767 || base.Height >= 32767))
			{
				throw new ArgumentException(SR.GetString("GdiplusInvalidSize"));
			}
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return zero;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002FBC File Offset: 0x000011BC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public IntPtr GetHicon()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateHICONFromBitmap(new HandleRef(this, this.nativeImage), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return zero;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002FEE File Offset: 0x000011EE
		public Bitmap(Image original, Size newSize) : this(original, (newSize != null) ? newSize.Width : 0, (newSize != null) ? newSize.Height : 0)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000301B File Offset: 0x0000121B
		private Bitmap()
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003024 File Offset: 0x00001224
		internal static Bitmap FromGDIplus(IntPtr handle)
		{
			Bitmap bitmap = new Bitmap();
			bitmap.SetNativeImage(handle);
			return bitmap;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003040 File Offset: 0x00001240
		public Bitmap Clone(Rectangle rect, PixelFormat format)
		{
			if (rect.Width == 0 || rect.Height == 0)
			{
				throw new ArgumentException(SR.GetString("GdiplusInvalidRectangle", new object[]
				{
					rect.ToString()
				}));
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneBitmapAreaI(rect.X, rect.Y, rect.Width, rect.Height, (int)format, new HandleRef(this, this.nativeImage), out zero);
			if (num != 0 || zero == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return Bitmap.FromGDIplus(zero);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000030DC File Offset: 0x000012DC
		public Bitmap Clone(RectangleF rect, PixelFormat format)
		{
			if (rect.Width == 0f || rect.Height == 0f)
			{
				throw new ArgumentException(SR.GetString("GdiplusInvalidRectangle", new object[]
				{
					rect.ToString()
				}));
			}
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneBitmapArea(rect.X, rect.Y, rect.Width, rect.Height, (int)format, new HandleRef(this, this.nativeImage), out zero);
			if (num != 0 || zero == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return Bitmap.FromGDIplus(zero);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003180 File Offset: 0x00001380
		public void MakeTransparent()
		{
			Color pixel = Bitmap.defaultTransparentColor;
			if (base.Height > 0 && base.Width > 0)
			{
				pixel = this.GetPixel(0, base.Size.Height - 1);
			}
			if (pixel.A < 255)
			{
				return;
			}
			this.MakeTransparent(pixel);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000031D4 File Offset: 0x000013D4
		public void MakeTransparent(Color transparentColor)
		{
			if (base.RawFormat.Guid == ImageFormat.Icon.Guid)
			{
				throw new InvalidOperationException(SR.GetString("CantMakeIconTransparent"));
			}
			Size size = base.Size;
			Bitmap bitmap = null;
			Graphics graphics = null;
			try
			{
				bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
				try
				{
					graphics = Graphics.FromImage(bitmap);
					graphics.Clear(Color.Transparent);
					Rectangle destRect = new Rectangle(0, 0, size.Width, size.Height);
					ImageAttributes imageAttributes = null;
					try
					{
						imageAttributes = new ImageAttributes();
						imageAttributes.SetColorKey(transparentColor, transparentColor);
						graphics.DrawImage(this, destRect, 0, 0, size.Width, size.Height, GraphicsUnit.Pixel, imageAttributes, null, IntPtr.Zero);
					}
					finally
					{
						if (imageAttributes != null)
						{
							imageAttributes.Dispose();
						}
					}
				}
				finally
				{
					if (graphics != null)
					{
						graphics.Dispose();
					}
				}
				IntPtr nativeImage = this.nativeImage;
				this.nativeImage = bitmap.nativeImage;
				bitmap.nativeImage = nativeImage;
			}
			finally
			{
				if (bitmap != null)
				{
					bitmap.Dispose();
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000032F8 File Offset: 0x000014F8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
		{
			BitmapData bitmapData = new BitmapData();
			return this.LockBits(rect, flags, format, bitmapData);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003318 File Offset: 0x00001518
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format, BitmapData bitmapData)
		{
			GPRECT gprect = new GPRECT(rect);
			int num = SafeNativeMethods.Gdip.GdipBitmapLockBits(new HandleRef(this, this.nativeImage), ref gprect, flags, format, bitmapData);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return bitmapData;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003354 File Offset: 0x00001554
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void UnlockBits(BitmapData bitmapdata)
		{
			int num = SafeNativeMethods.Gdip.GdipBitmapUnlockBits(new HandleRef(this, this.nativeImage), bitmapdata);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003380 File Offset: 0x00001580
		public Color GetPixel(int x, int y)
		{
			int argb = 0;
			if (x < 0 || x >= base.Width)
			{
				throw new ArgumentOutOfRangeException("x", SR.GetString("ValidRangeX"));
			}
			if (y < 0 || y >= base.Height)
			{
				throw new ArgumentOutOfRangeException("y", SR.GetString("ValidRangeY"));
			}
			int num = SafeNativeMethods.Gdip.GdipBitmapGetPixel(new HandleRef(this, this.nativeImage), x, y, out argb);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return Color.FromArgb(argb);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000033FC File Offset: 0x000015FC
		public void SetPixel(int x, int y, Color color)
		{
			if ((base.PixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
			{
				throw new InvalidOperationException(SR.GetString("GdiplusCannotSetPixelFromIndexedPixelFormat"));
			}
			if (x < 0 || x >= base.Width)
			{
				throw new ArgumentOutOfRangeException("x", SR.GetString("ValidRangeX"));
			}
			if (y < 0 || y >= base.Height)
			{
				throw new ArgumentOutOfRangeException("y", SR.GetString("ValidRangeY"));
			}
			int num = SafeNativeMethods.Gdip.GdipBitmapSetPixel(new HandleRef(this, this.nativeImage), x, y, color.ToArgb());
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003490 File Offset: 0x00001690
		public void SetResolution(float xDpi, float yDpi)
		{
			int num = SafeNativeMethods.Gdip.GdipBitmapSetResolution(new HandleRef(this, this.nativeImage), xDpi, yDpi);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x0400009D RID: 157
		private static Color defaultTransparentColor = Color.LightGray;
	}
}
