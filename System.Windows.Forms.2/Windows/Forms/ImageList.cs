using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms
{
	// Token: 0x02000295 RID: 661
	[Designer("System.Windows.Forms.Design.ImageListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItemFilter("System.Windows.Forms")]
	[DefaultProperty("Images")]
	[TypeConverter(typeof(ImageListConverter))]
	[DesignerSerializer("System.Windows.Forms.Design.ImageListCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionImageList")]
	public sealed class ImageList : Component
	{
		// Token: 0x060029CD RID: 10701 RVA: 0x000BE274 File Offset: 0x000BC474
		public ImageList()
		{
			if (!ImageList.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					ImageList.maxImageWidth = DpiHelper.LogicalToDeviceUnitsX(256);
					ImageList.maxImageHeight = DpiHelper.LogicalToDeviceUnitsY(256);
				}
				ImageList.isScalingInitialized = true;
			}
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000BE2E1 File Offset: 0x000BC4E1
		public ImageList(IContainer container) : this()
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060029CF RID: 10703 RVA: 0x000BE2FE File Offset: 0x000BC4FE
		// (set) Token: 0x060029D0 RID: 10704 RVA: 0x000BE308 File Offset: 0x000BC508
		[SRCategory("CatAppearance")]
		[SRDescription("ImageListColorDepthDescr")]
		public ColorDepth ColorDepth
		{
			get
			{
				return this.colorDepth;
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[]
				{
					4,
					8,
					16,
					24,
					32
				}))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ColorDepth));
				}
				if (this.colorDepth != value)
				{
					this.colorDepth = value;
					this.PerformRecreateHandle("ColorDepth");
				}
			}
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000BE365 File Offset: 0x000BC565
		private bool ShouldSerializeColorDepth()
		{
			return this.Images.Count == 0;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000BE375 File Offset: 0x000BC575
		private void ResetColorDepth()
		{
			this.ColorDepth = ColorDepth.Depth8Bit;
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060029D3 RID: 10707 RVA: 0x000BE37E File Offset: 0x000BC57E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ImageListHandleDescr")]
		public IntPtr Handle
		{
			get
			{
				if (this.nativeImageList == null)
				{
					this.CreateHandle();
				}
				return this.nativeImageList.Handle;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x000BE399 File Offset: 0x000BC599
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ImageListHandleCreatedDescr")]
		public bool HandleCreated
		{
			get
			{
				return this.nativeImageList != null;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x000BE3A4 File Offset: 0x000BC5A4
		[SRCategory("CatAppearance")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ImageListImagesDescr")]
		[MergableProperty(false)]
		public ImageList.ImageCollection Images
		{
			get
			{
				if (this.imageCollection == null)
				{
					this.imageCollection = new ImageList.ImageCollection(this);
				}
				return this.imageCollection;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x000BE3C0 File Offset: 0x000BC5C0
		// (set) Token: 0x060029D7 RID: 10711 RVA: 0x000BE3C8 File Offset: 0x000BC5C8
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ImageListSizeDescr")]
		public Size ImageSize
		{
			get
			{
				return this.imageSize;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"ImageSize",
						"Size.Empty"
					}));
				}
				if (value.Width <= 0 || value.Width > ImageList.maxImageWidth)
				{
					throw new ArgumentOutOfRangeException("ImageSize", SR.GetString("InvalidBoundArgument", new object[]
					{
						"ImageSize.Width",
						value.Width.ToString(CultureInfo.CurrentCulture),
						1.ToString(CultureInfo.CurrentCulture),
						ImageList.maxImageWidth.ToString()
					}));
				}
				if (value.Height <= 0 || value.Height > ImageList.maxImageHeight)
				{
					throw new ArgumentOutOfRangeException("ImageSize", SR.GetString("InvalidBoundArgument", new object[]
					{
						"ImageSize.Height",
						value.Height.ToString(CultureInfo.CurrentCulture),
						1.ToString(CultureInfo.CurrentCulture),
						ImageList.maxImageHeight.ToString()
					}));
				}
				if (this.imageSize.Width != value.Width || this.imageSize.Height != value.Height)
				{
					this.imageSize = new Size(value.Width, value.Height);
					this.PerformRecreateHandle("ImageSize");
				}
			}
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000BE365 File Offset: 0x000BC565
		private bool ShouldSerializeImageSize()
		{
			return this.Images.Count == 0;
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x000BE530 File Offset: 0x000BC730
		// (set) Token: 0x060029DA RID: 10714 RVA: 0x000BE548 File Offset: 0x000BC748
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue(null)]
		[SRDescription("ImageListImageStreamDescr")]
		public ImageListStreamer ImageStream
		{
			get
			{
				if (this.Images.Empty)
				{
					return null;
				}
				return new ImageListStreamer(this);
			}
			set
			{
				if (value != null)
				{
					ImageList.NativeImageList nativeImageList = value.GetNativeImageList();
					if (nativeImageList != null && nativeImageList != this.nativeImageList)
					{
						bool handleCreated = this.HandleCreated;
						this.DestroyHandle();
						this.originals = null;
						this.nativeImageList = new ImageList.NativeImageList(SafeNativeMethods.ImageList_Duplicate(new HandleRef(nativeImageList, nativeImageList.Handle)));
						int width;
						int height;
						if (SafeNativeMethods.ImageList_GetIconSize(new HandleRef(this, this.nativeImageList.Handle), out width, out height))
						{
							this.imageSize = new Size(width, height);
						}
						NativeMethods.IMAGEINFO imageinfo = new NativeMethods.IMAGEINFO();
						if (SafeNativeMethods.ImageList_GetImageInfo(new HandleRef(this, this.nativeImageList.Handle), 0, imageinfo))
						{
							NativeMethods.BITMAP bitmap = new NativeMethods.BITMAP();
							UnsafeNativeMethods.GetObject(new HandleRef(null, imageinfo.hbmImage), Marshal.SizeOf(bitmap), bitmap);
							short bmBitsPixel = bitmap.bmBitsPixel;
							if (bmBitsPixel <= 8)
							{
								if (bmBitsPixel != 4)
								{
									if (bmBitsPixel == 8)
									{
										this.colorDepth = ColorDepth.Depth8Bit;
									}
								}
								else
								{
									this.colorDepth = ColorDepth.Depth4Bit;
								}
							}
							else if (bmBitsPixel != 16)
							{
								if (bmBitsPixel != 24)
								{
									if (bmBitsPixel == 32)
									{
										this.colorDepth = ColorDepth.Depth32Bit;
									}
								}
								else
								{
									this.colorDepth = ColorDepth.Depth24Bit;
								}
							}
							else
							{
								this.colorDepth = ColorDepth.Depth16Bit;
							}
						}
						this.Images.ResetKeys();
						if (handleCreated)
						{
							this.OnRecreateHandle(new EventArgs());
							return;
						}
					}
				}
				else
				{
					this.DestroyHandle();
					this.Images.Clear();
				}
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x000BE699 File Offset: 0x000BC899
		// (set) Token: 0x060029DC RID: 10716 RVA: 0x000BE6A1 File Offset: 0x000BC8A1
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x000BE6AA File Offset: 0x000BC8AA
		// (set) Token: 0x060029DE RID: 10718 RVA: 0x000BE6B2 File Offset: 0x000BC8B2
		[SRCategory("CatBehavior")]
		[SRDescription("ImageListTransparentColorDescr")]
		public Color TransparentColor
		{
			get
			{
				return this.transparentColor;
			}
			set
			{
				this.transparentColor = value;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x000BE6BC File Offset: 0x000BC8BC
		private bool UseTransparentColor
		{
			get
			{
				return this.TransparentColor.A > 0;
			}
		}

		// Token: 0x140001E9 RID: 489
		// (add) Token: 0x060029E0 RID: 10720 RVA: 0x000BE6DA File Offset: 0x000BC8DA
		// (remove) Token: 0x060029E1 RID: 10721 RVA: 0x000BE6F3 File Offset: 0x000BC8F3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ImageListOnRecreateHandleDescr")]
		public event EventHandler RecreateHandle
		{
			add
			{
				this.recreateHandler = (EventHandler)Delegate.Combine(this.recreateHandler, value);
			}
			remove
			{
				this.recreateHandler = (EventHandler)Delegate.Remove(this.recreateHandler, value);
			}
		}

		// Token: 0x140001EA RID: 490
		// (add) Token: 0x060029E2 RID: 10722 RVA: 0x000BE70C File Offset: 0x000BC90C
		// (remove) Token: 0x060029E3 RID: 10723 RVA: 0x000BE725 File Offset: 0x000BC925
		internal event EventHandler ChangeHandle
		{
			add
			{
				this.changeHandler = (EventHandler)Delegate.Combine(this.changeHandler, value);
			}
			remove
			{
				this.changeHandler = (EventHandler)Delegate.Remove(this.changeHandler, value);
			}
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000BE740 File Offset: 0x000BC940
		private Bitmap CreateBitmap(ImageList.Original original, out bool ownsBitmap)
		{
			Color customTransparentColor = this.transparentColor;
			ownsBitmap = false;
			if ((original.options & ImageList.OriginalOptions.CustomTransparentColor) != ImageList.OriginalOptions.Default)
			{
				customTransparentColor = original.customTransparentColor;
			}
			Bitmap bitmap;
			if (original.image is Bitmap)
			{
				bitmap = (Bitmap)original.image;
			}
			else if (original.image is Icon)
			{
				bitmap = ((Icon)original.image).ToBitmap();
				ownsBitmap = true;
			}
			else
			{
				bitmap = new Bitmap((Image)original.image);
				ownsBitmap = true;
			}
			if (customTransparentColor.A > 0)
			{
				Bitmap bitmap2 = bitmap;
				bitmap = (Bitmap)bitmap.Clone();
				bitmap.MakeTransparent(customTransparentColor);
				if (ownsBitmap)
				{
					bitmap2.Dispose();
				}
				ownsBitmap = true;
			}
			Size size = bitmap.Size;
			if ((original.options & ImageList.OriginalOptions.ImageStrip) != ImageList.OriginalOptions.Default)
			{
				if (size.Width == 0 || size.Width % this.imageSize.Width != 0)
				{
					throw new ArgumentException(SR.GetString("ImageListStripBadWidth"), "original");
				}
				if (size.Height != this.imageSize.Height)
				{
					throw new ArgumentException(SR.GetString("ImageListImageTooShort"), "original");
				}
			}
			else if (!size.Equals(this.ImageSize))
			{
				Bitmap bitmap3 = bitmap;
				bitmap = new Bitmap(bitmap3, this.ImageSize);
				if (ownsBitmap)
				{
					bitmap3.Dispose();
				}
				ownsBitmap = true;
			}
			return bitmap;
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000BE890 File Offset: 0x000BCA90
		private int AddIconToHandle(ImageList.Original original, Icon icon)
		{
			int result;
			try
			{
				int num = SafeNativeMethods.ImageList_ReplaceIcon(new HandleRef(this, this.Handle), -1, new HandleRef(icon, icon.Handle));
				if (num == -1)
				{
					throw new InvalidOperationException(SR.GetString("ImageListAddFailed"));
				}
				result = num;
			}
			finally
			{
				if ((original.options & ImageList.OriginalOptions.OwnsImage) != ImageList.OriginalOptions.Default)
				{
					icon.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000BE8F8 File Offset: 0x000BCAF8
		private int AddToHandle(ImageList.Original original, Bitmap bitmap)
		{
			IntPtr intPtr = ControlPaint.CreateHBitmapTransparencyMask(bitmap);
			IntPtr handle = ControlPaint.CreateHBitmapColorMask(bitmap, intPtr);
			int num = SafeNativeMethods.ImageList_Add(new HandleRef(this, this.Handle), new HandleRef(null, handle), new HandleRef(null, intPtr));
			SafeNativeMethods.DeleteObject(new HandleRef(null, handle));
			SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
			if (num == -1)
			{
				throw new InvalidOperationException(SR.GetString("ImageListAddFailed"));
			}
			return num;
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000BE964 File Offset: 0x000BCB64
		private void CreateHandle()
		{
			int num = 1;
			ColorDepth colorDepth = this.colorDepth;
			if (colorDepth <= ColorDepth.Depth8Bit)
			{
				if (colorDepth != ColorDepth.Depth4Bit)
				{
					if (colorDepth == ColorDepth.Depth8Bit)
					{
						num |= 8;
					}
				}
				else
				{
					num |= 4;
				}
			}
			else if (colorDepth != ColorDepth.Depth16Bit)
			{
				if (colorDepth != ColorDepth.Depth24Bit)
				{
					if (colorDepth == ColorDepth.Depth32Bit)
					{
						num |= 32;
					}
				}
				else
				{
					num |= 24;
				}
			}
			else
			{
				num |= 16;
			}
			IntPtr userCookie = UnsafeNativeMethods.ThemingScope.Activate();
			try
			{
				SafeNativeMethods.InitCommonControls();
				this.nativeImageList = new ImageList.NativeImageList(SafeNativeMethods.ImageList_Create(this.imageSize.Width, this.imageSize.Height, num, 4, 4));
			}
			finally
			{
				UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
			}
			if (this.Handle == IntPtr.Zero)
			{
				throw new InvalidOperationException(SR.GetString("ImageListCreateFailed"));
			}
			SafeNativeMethods.ImageList_SetBkColor(new HandleRef(this, this.Handle), -1);
			for (int i = 0; i < this.originals.Count; i++)
			{
				ImageList.Original original = (ImageList.Original)this.originals[i];
				if (original.image is Icon)
				{
					this.AddIconToHandle(original, (Icon)original.image);
				}
				else
				{
					bool flag = false;
					Bitmap bitmap = this.CreateBitmap(original, out flag);
					this.AddToHandle(original, bitmap);
					if (flag)
					{
						bitmap.Dispose();
					}
				}
			}
			this.originals = null;
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000BEAB8 File Offset: 0x000BCCB8
		private void DestroyHandle()
		{
			if (this.HandleCreated)
			{
				this.nativeImageList.Dispose();
				this.nativeImageList = null;
				this.originals = new ArrayList();
			}
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000BEAE0 File Offset: 0x000BCCE0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.originals != null)
				{
					foreach (object obj in this.originals)
					{
						ImageList.Original original = (ImageList.Original)obj;
						if ((original.options & ImageList.OriginalOptions.OwnsImage) != ImageList.OriginalOptions.Default)
						{
							((IDisposable)original.image).Dispose();
						}
					}
				}
				this.DestroyHandle();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000BEB64 File Offset: 0x000BCD64
		public void Draw(Graphics g, Point pt, int index)
		{
			this.Draw(g, pt.X, pt.Y, index);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000BEB7C File Offset: 0x000BCD7C
		public void Draw(Graphics g, int x, int y, int index)
		{
			this.Draw(g, x, y, this.imageSize.Width, this.imageSize.Height, index);
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000BEBA0 File Offset: 0x000BCDA0
		public void Draw(Graphics g, int x, int y, int width, int height, int index)
		{
			if (index < 0 || index >= this.Images.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			IntPtr hdc = g.GetHdc();
			try
			{
				SafeNativeMethods.ImageList_DrawEx(new HandleRef(this, this.Handle), index, new HandleRef(g, hdc), x, y, width, height, -1, -1, 1);
			}
			finally
			{
				g.ReleaseHdcInternal(hdc);
			}
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000BEC38 File Offset: 0x000BCE38
		private void CopyBitmapData(BitmapData sourceData, BitmapData targetData)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < targetData.Height; i++)
			{
				IntPtr handle;
				IntPtr handle2;
				if (IntPtr.Size == 4)
				{
					handle = new IntPtr(sourceData.Scan0.ToInt32() + num);
					handle2 = new IntPtr(targetData.Scan0.ToInt32() + num2);
				}
				else
				{
					handle = new IntPtr(sourceData.Scan0.ToInt64() + (long)num);
					handle2 = new IntPtr(targetData.Scan0.ToInt64() + (long)num2);
				}
				UnsafeNativeMethods.CopyMemory(new HandleRef(this, handle2), new HandleRef(this, handle), Math.Abs(targetData.Stride));
				num += sourceData.Stride;
				num2 += targetData.Stride;
			}
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000BED00 File Offset: 0x000BCF00
		private unsafe static bool BitmapHasAlpha(BitmapData bmpData)
		{
			if (bmpData.PixelFormat != PixelFormat.Format32bppArgb && bmpData.PixelFormat != PixelFormat.Format32bppRgb)
			{
				return false;
			}
			bool result = false;
			for (int i = 0; i < bmpData.Height; i++)
			{
				int num = i * bmpData.Stride;
				for (int j = 3; j < bmpData.Width * 4; j += 4)
				{
					byte* ptr = (byte*)((byte*)bmpData.Scan0.ToPointer() + num) + j;
					if (*ptr != 0)
					{
						return true;
					}
				}
			}
			return result;
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000BED78 File Offset: 0x000BCF78
		private Bitmap GetBitmap(int index)
		{
			if (index < 0 || index >= this.Images.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			Bitmap bitmap = null;
			if (this.ColorDepth == ColorDepth.Depth32Bit)
			{
				NativeMethods.IMAGEINFO imageinfo = new NativeMethods.IMAGEINFO();
				if (SafeNativeMethods.ImageList_GetImageInfo(new HandleRef(this, this.Handle), index, imageinfo))
				{
					Bitmap bitmap2 = null;
					BitmapData bitmapData = null;
					BitmapData bitmapData2 = null;
					IntSecurity.ObjectFromWin32Handle.Assert();
					try
					{
						bitmap2 = Image.FromHbitmap(imageinfo.hbmImage);
						bitmapData = bitmap2.LockBits(new Rectangle(imageinfo.rcImage_left, imageinfo.rcImage_top, imageinfo.rcImage_right - imageinfo.rcImage_left, imageinfo.rcImage_bottom - imageinfo.rcImage_top), ImageLockMode.ReadOnly, bitmap2.PixelFormat);
						int num = bitmapData.Stride * this.imageSize.Height * index;
						if (ImageList.BitmapHasAlpha(bitmapData))
						{
							bitmap = new Bitmap(this.imageSize.Width, this.imageSize.Height, PixelFormat.Format32bppArgb);
							bitmapData2 = bitmap.LockBits(new Rectangle(0, 0, this.imageSize.Width, this.imageSize.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
							this.CopyBitmapData(bitmapData, bitmapData2);
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
						if (bitmap2 != null)
						{
							if (bitmapData != null)
							{
								bitmap2.UnlockBits(bitmapData);
							}
							bitmap2.Dispose();
						}
						if (bitmap != null && bitmapData2 != null)
						{
							bitmap.UnlockBits(bitmapData2);
						}
					}
				}
			}
			if (bitmap == null)
			{
				bitmap = new Bitmap(this.imageSize.Width, this.imageSize.Height);
				Graphics graphics = Graphics.FromImage(bitmap);
				try
				{
					IntPtr hdc = graphics.GetHdc();
					try
					{
						SafeNativeMethods.ImageList_DrawEx(new HandleRef(this, this.Handle), index, new HandleRef(graphics, hdc), 0, 0, this.imageSize.Width, this.imageSize.Height, -1, -1, 1);
					}
					finally
					{
						graphics.ReleaseHdcInternal(hdc);
					}
				}
				finally
				{
					graphics.Dispose();
				}
			}
			bitmap.MakeTransparent(ImageList.fakeTransparencyColor);
			return bitmap;
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000BEF98 File Offset: 0x000BD198
		private void OnRecreateHandle(EventArgs eventargs)
		{
			if (this.recreateHandler != null)
			{
				this.recreateHandler(this, eventargs);
			}
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000BEFAF File Offset: 0x000BD1AF
		private void OnChangeHandle(EventArgs eventargs)
		{
			if (this.changeHandler != null)
			{
				this.changeHandler(this, eventargs);
			}
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000BEFC8 File Offset: 0x000BD1C8
		private void PerformRecreateHandle(string reason)
		{
			if (!this.HandleCreated)
			{
				return;
			}
			if (this.originals == null || this.Images.Empty)
			{
				this.originals = new ArrayList();
			}
			if (this.originals == null)
			{
				throw new InvalidOperationException(SR.GetString("ImageListCantRecreate", new object[]
				{
					reason
				}));
			}
			this.DestroyHandle();
			this.CreateHandle();
			this.OnRecreateHandle(new EventArgs());
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000BF037 File Offset: 0x000BD237
		private void ResetImageSize()
		{
			this.ImageSize = ImageList.DefaultImageSize;
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000BF044 File Offset: 0x000BD244
		private void ResetTransparentColor()
		{
			this.TransparentColor = Color.LightGray;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000BF054 File Offset: 0x000BD254
		private bool ShouldSerializeTransparentColor()
		{
			return !this.TransparentColor.Equals(Color.LightGray);
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000BF084 File Offset: 0x000BD284
		public override string ToString()
		{
			string text = base.ToString();
			if (this.Images != null)
			{
				return string.Concat(new string[]
				{
					text,
					" Images.Count: ",
					this.Images.Count.ToString(CultureInfo.CurrentCulture),
					", ImageSize: ",
					this.ImageSize.ToString()
				});
			}
			return text;
		}

		// Token: 0x040010F8 RID: 4344
		private static Color fakeTransparencyColor = Color.FromArgb(13, 11, 12);

		// Token: 0x040010F9 RID: 4345
		private static Size DefaultImageSize = new Size(16, 16);

		// Token: 0x040010FA RID: 4346
		private const int INITIAL_CAPACITY = 4;

		// Token: 0x040010FB RID: 4347
		private const int GROWBY = 4;

		// Token: 0x040010FC RID: 4348
		private const int MAX_DIMENSION = 256;

		// Token: 0x040010FD RID: 4349
		private static int maxImageWidth = 256;

		// Token: 0x040010FE RID: 4350
		private static int maxImageHeight = 256;

		// Token: 0x040010FF RID: 4351
		private static bool isScalingInitialized;

		// Token: 0x04001100 RID: 4352
		private ImageList.NativeImageList nativeImageList;

		// Token: 0x04001101 RID: 4353
		private ColorDepth colorDepth = ColorDepth.Depth8Bit;

		// Token: 0x04001102 RID: 4354
		private Color transparentColor = Color.Transparent;

		// Token: 0x04001103 RID: 4355
		private Size imageSize = ImageList.DefaultImageSize;

		// Token: 0x04001104 RID: 4356
		private ImageList.ImageCollection imageCollection;

		// Token: 0x04001105 RID: 4357
		private object userData;

		// Token: 0x04001106 RID: 4358
		private IList originals = new ArrayList();

		// Token: 0x04001107 RID: 4359
		private EventHandler recreateHandler;

		// Token: 0x04001108 RID: 4360
		private EventHandler changeHandler;

		// Token: 0x04001109 RID: 4361
		private bool inAddRange;

		// Token: 0x020006AC RID: 1708
		internal class Indexer
		{
			// Token: 0x17001694 RID: 5780
			// (get) Token: 0x06006895 RID: 26773 RVA: 0x001854AF File Offset: 0x001836AF
			// (set) Token: 0x06006896 RID: 26774 RVA: 0x001854B7 File Offset: 0x001836B7
			public virtual ImageList ImageList
			{
				get
				{
					return this.imageList;
				}
				set
				{
					this.imageList = value;
				}
			}

			// Token: 0x17001695 RID: 5781
			// (get) Token: 0x06006897 RID: 26775 RVA: 0x001854C0 File Offset: 0x001836C0
			// (set) Token: 0x06006898 RID: 26776 RVA: 0x001854C8 File Offset: 0x001836C8
			public virtual string Key
			{
				get
				{
					return this.key;
				}
				set
				{
					this.index = -1;
					this.key = ((value == null) ? string.Empty : value);
					this.useIntegerIndex = false;
				}
			}

			// Token: 0x17001696 RID: 5782
			// (get) Token: 0x06006899 RID: 26777 RVA: 0x001854E9 File Offset: 0x001836E9
			// (set) Token: 0x0600689A RID: 26778 RVA: 0x001854F1 File Offset: 0x001836F1
			public virtual int Index
			{
				get
				{
					return this.index;
				}
				set
				{
					this.key = string.Empty;
					this.index = value;
					this.useIntegerIndex = true;
				}
			}

			// Token: 0x17001697 RID: 5783
			// (get) Token: 0x0600689B RID: 26779 RVA: 0x0018550C File Offset: 0x0018370C
			public virtual int ActualIndex
			{
				get
				{
					if (this.useIntegerIndex)
					{
						return this.Index;
					}
					if (this.ImageList != null)
					{
						return this.ImageList.Images.IndexOfKey(this.Key);
					}
					return -1;
				}
			}

			// Token: 0x04003AF4 RID: 15092
			private string key = string.Empty;

			// Token: 0x04003AF5 RID: 15093
			private int index = -1;

			// Token: 0x04003AF6 RID: 15094
			private bool useIntegerIndex = true;

			// Token: 0x04003AF7 RID: 15095
			private ImageList imageList;
		}

		// Token: 0x020006AD RID: 1709
		internal class NativeImageList : IDisposable
		{
			// Token: 0x0600689D RID: 26781 RVA: 0x0018555E File Offset: 0x0018375E
			internal NativeImageList(IntPtr himl)
			{
				this.himl = himl;
			}

			// Token: 0x17001698 RID: 5784
			// (get) Token: 0x0600689E RID: 26782 RVA: 0x0018556D File Offset: 0x0018376D
			internal IntPtr Handle
			{
				get
				{
					return this.himl;
				}
			}

			// Token: 0x0600689F RID: 26783 RVA: 0x00185575 File Offset: 0x00183775
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x060068A0 RID: 26784 RVA: 0x00185584 File Offset: 0x00183784
			public void Dispose(bool disposing)
			{
				if (this.himl != IntPtr.Zero)
				{
					SafeNativeMethods.ImageList_Destroy(new HandleRef(null, this.himl));
					this.himl = IntPtr.Zero;
				}
			}

			// Token: 0x060068A1 RID: 26785 RVA: 0x001855B8 File Offset: 0x001837B8
			~NativeImageList()
			{
				this.Dispose(false);
			}

			// Token: 0x04003AF8 RID: 15096
			private IntPtr himl;
		}

		// Token: 0x020006AE RID: 1710
		private class Original
		{
			// Token: 0x060068A2 RID: 26786 RVA: 0x001855E8 File Offset: 0x001837E8
			internal Original(object image, ImageList.OriginalOptions options) : this(image, options, Color.Transparent)
			{
			}

			// Token: 0x060068A3 RID: 26787 RVA: 0x001855F7 File Offset: 0x001837F7
			internal Original(object image, ImageList.OriginalOptions options, int nImages) : this(image, options, Color.Transparent)
			{
				this.nImages = nImages;
			}

			// Token: 0x060068A4 RID: 26788 RVA: 0x00185610 File Offset: 0x00183810
			internal Original(object image, ImageList.OriginalOptions options, Color customTransparentColor)
			{
				if (!(image is Icon) && !(image is Image))
				{
					throw new InvalidOperationException(SR.GetString("ImageListEntryType"));
				}
				this.image = image;
				this.options = options;
				this.customTransparentColor = customTransparentColor;
				ImageList.OriginalOptions originalOptions = options & ImageList.OriginalOptions.CustomTransparentColor;
			}

			// Token: 0x04003AF9 RID: 15097
			internal object image;

			// Token: 0x04003AFA RID: 15098
			internal ImageList.OriginalOptions options;

			// Token: 0x04003AFB RID: 15099
			internal Color customTransparentColor = Color.Transparent;

			// Token: 0x04003AFC RID: 15100
			internal int nImages = 1;
		}

		// Token: 0x020006AF RID: 1711
		[Flags]
		private enum OriginalOptions
		{
			// Token: 0x04003AFE RID: 15102
			Default = 0,
			// Token: 0x04003AFF RID: 15103
			ImageStrip = 1,
			// Token: 0x04003B00 RID: 15104
			CustomTransparentColor = 2,
			// Token: 0x04003B01 RID: 15105
			OwnsImage = 4
		}

		// Token: 0x020006B0 RID: 1712
		[Editor("System.Windows.Forms.Design.ImageCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public sealed class ImageCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x17001699 RID: 5785
			// (get) Token: 0x060068A5 RID: 26789 RVA: 0x00185670 File Offset: 0x00183870
			public StringCollection Keys
			{
				get
				{
					StringCollection stringCollection = new StringCollection();
					for (int i = 0; i < this.imageInfoCollection.Count; i++)
					{
						ImageList.ImageCollection.ImageInfo imageInfo = this.imageInfoCollection[i] as ImageList.ImageCollection.ImageInfo;
						if (imageInfo != null && imageInfo.Name != null && imageInfo.Name.Length != 0)
						{
							stringCollection.Add(imageInfo.Name);
						}
						else
						{
							stringCollection.Add(string.Empty);
						}
					}
					return stringCollection;
				}
			}

			// Token: 0x060068A6 RID: 26790 RVA: 0x001856DF File Offset: 0x001838DF
			internal ImageCollection(ImageList owner)
			{
				this.owner = owner;
			}

			// Token: 0x060068A7 RID: 26791 RVA: 0x00185700 File Offset: 0x00183900
			internal void ResetKeys()
			{
				if (this.imageInfoCollection != null)
				{
					this.imageInfoCollection.Clear();
				}
				for (int i = 0; i < this.Count; i++)
				{
					this.imageInfoCollection.Add(new ImageList.ImageCollection.ImageInfo());
				}
			}

			// Token: 0x060068A8 RID: 26792 RVA: 0x000072B6 File Offset: 0x000054B6
			[Conditional("DEBUG")]
			private void AssertInvariant()
			{
			}

			// Token: 0x1700169A RID: 5786
			// (get) Token: 0x060068A9 RID: 26793 RVA: 0x00185744 File Offset: 0x00183944
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner.HandleCreated)
					{
						return SafeNativeMethods.ImageList_GetImageCount(new HandleRef(this.owner, this.owner.Handle));
					}
					int num = 0;
					foreach (object obj in this.owner.originals)
					{
						ImageList.Original original = (ImageList.Original)obj;
						if (original != null)
						{
							num += original.nImages;
						}
					}
					return num;
				}
			}

			// Token: 0x1700169B RID: 5787
			// (get) Token: 0x060068AA RID: 26794 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700169C RID: 5788
			// (get) Token: 0x060068AB RID: 26795 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700169D RID: 5789
			// (get) Token: 0x060068AC RID: 26796 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700169E RID: 5790
			// (get) Token: 0x060068AD RID: 26797 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700169F RID: 5791
			// (get) Token: 0x060068AE RID: 26798 RVA: 0x001857D4 File Offset: 0x001839D4
			public bool Empty
			{
				get
				{
					return this.Count == 0;
				}
			}

			// Token: 0x170016A0 RID: 5792
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public Image this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.owner.GetBitmap(index);
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					if (!(value is Bitmap))
					{
						throw new ArgumentException(SR.GetString("ImageListBitmap"));
					}
					Bitmap bitmap = (Bitmap)value;
					bool flag = false;
					if (this.owner.UseTransparentColor)
					{
						bitmap = (Bitmap)bitmap.Clone();
						bitmap.MakeTransparent(this.owner.transparentColor);
						flag = true;
					}
					try
					{
						IntPtr intPtr = ControlPaint.CreateHBitmapTransparencyMask(bitmap);
						IntPtr handle = ControlPaint.CreateHBitmapColorMask(bitmap, intPtr);
						bool flag2 = SafeNativeMethods.ImageList_Replace(new HandleRef(this.owner, this.owner.Handle), index, new HandleRef(null, handle), new HandleRef(null, intPtr));
						SafeNativeMethods.DeleteObject(new HandleRef(null, handle));
						SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
						if (!flag2)
						{
							throw new InvalidOperationException(SR.GetString("ImageListReplaceFailed"));
						}
					}
					finally
					{
						if (flag)
						{
							bitmap.Dispose();
						}
					}
				}
			}

			// Token: 0x170016A1 RID: 5793
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is Image)
					{
						this[index] = (Image)value;
						return;
					}
					throw new ArgumentException(SR.GetString("ImageListBadImage"), "value");
				}
			}

			// Token: 0x170016A2 RID: 5794
			public Image this[string key]
			{
				get
				{
					if (key == null || key.Length == 0)
					{
						return null;
					}
					int index = this.IndexOfKey(key);
					if (this.IsValidIndex(index))
					{
						return this[index];
					}
					return null;
				}
			}

			// Token: 0x060068B4 RID: 26804 RVA: 0x001859D8 File Offset: 0x00183BD8
			public void Add(string key, Image image)
			{
				ImageList.ImageCollection.ImageInfo imageInfo = new ImageList.ImageCollection.ImageInfo();
				imageInfo.Name = key;
				ImageList.Original original = new ImageList.Original(image, ImageList.OriginalOptions.Default);
				this.Add(original, imageInfo);
			}

			// Token: 0x060068B5 RID: 26805 RVA: 0x00185A04 File Offset: 0x00183C04
			public void Add(string key, Icon icon)
			{
				ImageList.ImageCollection.ImageInfo imageInfo = new ImageList.ImageCollection.ImageInfo();
				imageInfo.Name = key;
				ImageList.Original original = new ImageList.Original(icon, ImageList.OriginalOptions.Default);
				this.Add(original, imageInfo);
			}

			// Token: 0x060068B6 RID: 26806 RVA: 0x00185A2F File Offset: 0x00183C2F
			int IList.Add(object value)
			{
				if (value is Image)
				{
					this.Add((Image)value);
					return this.Count - 1;
				}
				throw new ArgumentException(SR.GetString("ImageListBadImage"), "value");
			}

			// Token: 0x060068B7 RID: 26807 RVA: 0x00185A62 File Offset: 0x00183C62
			public void Add(Icon value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.Add(new ImageList.Original(value.Clone(), ImageList.OriginalOptions.OwnsImage), null);
			}

			// Token: 0x060068B8 RID: 26808 RVA: 0x00185A88 File Offset: 0x00183C88
			public void Add(Image value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ImageList.Original original = new ImageList.Original(value, ImageList.OriginalOptions.Default);
				this.Add(original, null);
			}

			// Token: 0x060068B9 RID: 26809 RVA: 0x00185AB4 File Offset: 0x00183CB4
			public int Add(Image value, Color transparentColor)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ImageList.Original original = new ImageList.Original(value, ImageList.OriginalOptions.CustomTransparentColor, transparentColor);
				return this.Add(original, null);
			}

			// Token: 0x060068BA RID: 26810 RVA: 0x00185AE0 File Offset: 0x00183CE0
			private int Add(ImageList.Original original, ImageList.ImageCollection.ImageInfo imageInfo)
			{
				if (original == null || original.image == null)
				{
					throw new ArgumentNullException("original");
				}
				int result = -1;
				if (original.image is Bitmap)
				{
					if (this.owner.originals != null)
					{
						result = this.owner.originals.Add(original);
					}
					if (this.owner.HandleCreated)
					{
						bool flag = false;
						Bitmap bitmap = this.owner.CreateBitmap(original, out flag);
						result = this.owner.AddToHandle(original, bitmap);
						if (flag)
						{
							bitmap.Dispose();
						}
					}
				}
				else
				{
					if (!(original.image is Icon))
					{
						throw new ArgumentException(SR.GetString("ImageListBitmap"));
					}
					if (this.owner.originals != null)
					{
						result = this.owner.originals.Add(original);
					}
					if (this.owner.HandleCreated)
					{
						result = this.owner.AddIconToHandle(original, (Icon)original.image);
					}
				}
				if ((original.options & ImageList.OriginalOptions.ImageStrip) != ImageList.OriginalOptions.Default)
				{
					for (int i = 0; i < original.nImages; i++)
					{
						this.imageInfoCollection.Add(new ImageList.ImageCollection.ImageInfo());
					}
				}
				else
				{
					if (imageInfo == null)
					{
						imageInfo = new ImageList.ImageCollection.ImageInfo();
					}
					this.imageInfoCollection.Add(imageInfo);
				}
				if (!this.owner.inAddRange)
				{
					this.owner.OnChangeHandle(new EventArgs());
				}
				return result;
			}

			// Token: 0x060068BB RID: 26811 RVA: 0x00185C34 File Offset: 0x00183E34
			public void AddRange(Image[] images)
			{
				if (images == null)
				{
					throw new ArgumentNullException("images");
				}
				this.owner.inAddRange = true;
				foreach (Image value in images)
				{
					this.Add(value);
				}
				this.owner.inAddRange = false;
				this.owner.OnChangeHandle(new EventArgs());
			}

			// Token: 0x060068BC RID: 26812 RVA: 0x00185C94 File Offset: 0x00183E94
			public int AddStrip(Image value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Width == 0 || value.Width % this.owner.ImageSize.Width != 0)
				{
					throw new ArgumentException(SR.GetString("ImageListStripBadWidth"), "value");
				}
				if (value.Height != this.owner.ImageSize.Height)
				{
					throw new ArgumentException(SR.GetString("ImageListImageTooShort"), "value");
				}
				int nImages = value.Width / this.owner.ImageSize.Width;
				ImageList.Original original = new ImageList.Original(value, ImageList.OriginalOptions.ImageStrip, nImages);
				return this.Add(original, null);
			}

			// Token: 0x060068BD RID: 26813 RVA: 0x00185D44 File Offset: 0x00183F44
			public void Clear()
			{
				if (this.owner.originals != null)
				{
					this.owner.originals.Clear();
				}
				this.imageInfoCollection.Clear();
				if (this.owner.HandleCreated)
				{
					SafeNativeMethods.ImageList_Remove(new HandleRef(this.owner, this.owner.Handle), -1);
				}
				this.owner.OnChangeHandle(new EventArgs());
			}

			// Token: 0x060068BE RID: 26814 RVA: 0x0000A547 File Offset: 0x00008747
			[EditorBrowsable(EditorBrowsableState.Never)]
			public bool Contains(Image image)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060068BF RID: 26815 RVA: 0x00185DB3 File Offset: 0x00183FB3
			bool IList.Contains(object image)
			{
				return image is Image && this.Contains((Image)image);
			}

			// Token: 0x060068C0 RID: 26816 RVA: 0x00185DCB File Offset: 0x00183FCB
			public bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x060068C1 RID: 26817 RVA: 0x0000A547 File Offset: 0x00008747
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int IndexOf(Image image)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060068C2 RID: 26818 RVA: 0x00185DDA File Offset: 0x00183FDA
			int IList.IndexOf(object image)
			{
				if (image is Image)
				{
					return this.IndexOf((Image)image);
				}
				return -1;
			}

			// Token: 0x060068C3 RID: 26819 RVA: 0x00185DF4 File Offset: 0x00183FF4
			public int IndexOfKey(string key)
			{
				if (key == null || key.Length == 0)
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && this.imageInfoCollection[this.lastAccessedIndex] != null && WindowsFormsUtils.SafeCompareStrings(((ImageList.ImageCollection.ImageInfo)this.imageInfoCollection[this.lastAccessedIndex]).Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (this.imageInfoCollection[i] != null && WindowsFormsUtils.SafeCompareStrings(((ImageList.ImageCollection.ImageInfo)this.imageInfoCollection[i]).Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			// Token: 0x060068C4 RID: 26820 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060068C5 RID: 26821 RVA: 0x00185EA9 File Offset: 0x001840A9
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x060068C6 RID: 26822 RVA: 0x00185EBC File Offset: 0x001840BC
			void ICollection.CopyTo(Array dest, int index)
			{
				for (int i = 0; i < this.Count; i++)
				{
					dest.SetValue(this.owner.GetBitmap(i), index++);
				}
			}

			// Token: 0x060068C7 RID: 26823 RVA: 0x00185EF4 File Offset: 0x001840F4
			public IEnumerator GetEnumerator()
			{
				Image[] array = new Image[this.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.owner.GetBitmap(i);
				}
				return array.GetEnumerator();
			}

			// Token: 0x060068C8 RID: 26824 RVA: 0x0000A547 File Offset: 0x00008747
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void Remove(Image image)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060068C9 RID: 26825 RVA: 0x00185F30 File Offset: 0x00184130
			void IList.Remove(object image)
			{
				if (image is Image)
				{
					this.Remove((Image)image);
					this.owner.OnChangeHandle(new EventArgs());
				}
			}

			// Token: 0x060068CA RID: 26826 RVA: 0x00185F58 File Offset: 0x00184158
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (!SafeNativeMethods.ImageList_Remove(new HandleRef(this.owner, this.owner.Handle), index))
				{
					throw new InvalidOperationException(SR.GetString("ImageListRemoveFailed"));
				}
				if (this.imageInfoCollection != null && index >= 0 && index < this.imageInfoCollection.Count)
				{
					this.imageInfoCollection.RemoveAt(index);
					this.owner.OnChangeHandle(new EventArgs());
				}
			}

			// Token: 0x060068CB RID: 26827 RVA: 0x0018600C File Offset: 0x0018420C
			public void RemoveByKey(string key)
			{
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					this.RemoveAt(index);
				}
			}

			// Token: 0x060068CC RID: 26828 RVA: 0x00186034 File Offset: 0x00184234
			public void SetKeyName(int index, string name)
			{
				if (!this.IsValidIndex(index))
				{
					throw new IndexOutOfRangeException();
				}
				if (this.imageInfoCollection[index] == null)
				{
					this.imageInfoCollection[index] = new ImageList.ImageCollection.ImageInfo();
				}
				((ImageList.ImageCollection.ImageInfo)this.imageInfoCollection[index]).Name = name;
			}

			// Token: 0x04003B02 RID: 15106
			private ImageList owner;

			// Token: 0x04003B03 RID: 15107
			private ArrayList imageInfoCollection = new ArrayList();

			// Token: 0x04003B04 RID: 15108
			private int lastAccessedIndex = -1;

			// Token: 0x020008BE RID: 2238
			internal class ImageInfo
			{
				// Token: 0x17001934 RID: 6452
				// (get) Token: 0x060072E3 RID: 29411 RVA: 0x001A4A13 File Offset: 0x001A2C13
				// (set) Token: 0x060072E4 RID: 29412 RVA: 0x001A4A1B File Offset: 0x001A2C1B
				public string Name
				{
					get
					{
						return this.name;
					}
					set
					{
						this.name = value;
					}
				}

				// Token: 0x04004537 RID: 17719
				private string name;
			}
		}
	}
}
