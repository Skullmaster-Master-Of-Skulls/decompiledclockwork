using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000177 RID: 375
	[TypeConverter(typeof(CursorConverter))]
	[Editor("System.Drawing.Design.CursorEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Serializable]
	public sealed class Cursor : IDisposable, ISerializable
	{
		// Token: 0x060013DA RID: 5082 RVA: 0x0004299C File Offset: 0x00040B9C
		internal Cursor(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			if (enumerator == null)
			{
				return;
			}
			while (enumerator.MoveNext())
			{
				if (string.Equals(enumerator.Name, "CursorData", StringComparison.OrdinalIgnoreCase))
				{
					this.cursorData = (byte[])enumerator.Value;
					if (this.cursorData != null)
					{
						this.LoadPicture(new UnsafeNativeMethods.ComStreamFromDataStream(new MemoryStream(this.cursorData)));
					}
				}
				else if (string.Compare(enumerator.Name, "CursorResourceId", true, CultureInfo.InvariantCulture) == 0)
				{
					this.LoadFromResourceId((int)enumerator.Value);
				}
			}
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00042A41 File Offset: 0x00040C41
		internal Cursor(int nResourceId, int dummy)
		{
			this.LoadFromResourceId(nResourceId);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00042A64 File Offset: 0x00040C64
		internal Cursor(string resource, int dummy)
		{
			Stream manifestResourceStream = typeof(Cursor).Module.Assembly.GetManifestResourceStream(typeof(Cursor), resource);
			this.cursorData = new byte[manifestResourceStream.Length];
			manifestResourceStream.Read(this.cursorData, 0, Convert.ToInt32(manifestResourceStream.Length));
			this.LoadPicture(new UnsafeNativeMethods.ComStreamFromDataStream(new MemoryStream(this.cursorData)));
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00042AF0 File Offset: 0x00040CF0
		public Cursor(IntPtr handle)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("InvalidGDIHandle", new object[]
				{
					typeof(Cursor).Name
				}));
			}
			this.handle = handle;
			this.ownHandle = false;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00042B64 File Offset: 0x00040D64
		public Cursor(string fileName)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			try
			{
				this.cursorData = new byte[fileStream.Length];
				fileStream.Read(this.cursorData, 0, Convert.ToInt32(fileStream.Length));
			}
			finally
			{
				fileStream.Close();
			}
			this.LoadPicture(new UnsafeNativeMethods.ComStreamFromDataStream(new MemoryStream(this.cursorData)));
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00042BF0 File Offset: 0x00040DF0
		public Cursor(Type type, string resource) : this(type.Module.Assembly.GetManifestResourceStream(type, resource))
		{
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00042C0C File Offset: 0x00040E0C
		public Cursor(Stream stream)
		{
			this.cursorData = new byte[stream.Length];
			stream.Read(this.cursorData, 0, Convert.ToInt32(stream.Length));
			this.LoadPicture(new UnsafeNativeMethods.ComStreamFromDataStream(new MemoryStream(this.cursorData)));
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00042C72 File Offset: 0x00040E72
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x00042C79 File Offset: 0x00040E79
		public static Rectangle Clip
		{
			get
			{
				return Cursor.ClipInternal;
			}
			set
			{
				if (!value.IsEmpty)
				{
					IntSecurity.AdjustCursorClip.Demand();
				}
				Cursor.ClipInternal = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00042C94 File Offset: 0x00040E94
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x00042CD0 File Offset: 0x00040ED0
		internal static Rectangle ClipInternal
		{
			get
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				SafeNativeMethods.GetClipCursor(ref rect);
				return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
			set
			{
				if (value.IsEmpty)
				{
					UnsafeNativeMethods.ClipCursor(null);
					return;
				}
				NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(value.X, value.Y, value.Width, value.Height);
				UnsafeNativeMethods.ClipCursor(ref rect);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x00042D18 File Offset: 0x00040F18
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x00042D1F File Offset: 0x00040F1F
		public static Cursor Current
		{
			get
			{
				return Cursor.CurrentInternal;
			}
			set
			{
				IntSecurity.ModifyCursor.Demand();
				Cursor.CurrentInternal = value;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00042D34 File Offset: 0x00040F34
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x00042D58 File Offset: 0x00040F58
		internal static Cursor CurrentInternal
		{
			get
			{
				IntPtr cursor = SafeNativeMethods.GetCursor();
				IntSecurity.UnmanagedCode.Assert();
				return Cursors.KnownCursorFromHCursor(cursor);
			}
			set
			{
				IntPtr intPtr = (value == null) ? IntPtr.Zero : value.handle;
				UnsafeNativeMethods.SetCursor(new HandleRef(value, intPtr));
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00042D89 File Offset: 0x00040F89
		public IntPtr Handle
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					throw new ObjectDisposedException(SR.GetString("ObjectDisposed", new object[]
					{
						base.GetType().Name
					}));
				}
				return this.handle;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00042DC8 File Offset: 0x00040FC8
		public Point HotSpot
		{
			get
			{
				Point result = Point.Empty;
				NativeMethods.ICONINFO iconinfo = new NativeMethods.ICONINFO();
				Icon icon = null;
				IntSecurity.ObjectFromWin32Handle.Assert();
				try
				{
					icon = Icon.FromHandle(this.Handle);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				try
				{
					SafeNativeMethods.GetIconInfo(new HandleRef(this, icon.Handle), iconinfo);
					result = new Point(iconinfo.xHotspot, iconinfo.yHotspot);
				}
				finally
				{
					if (iconinfo.hbmMask != IntPtr.Zero)
					{
						SafeNativeMethods.ExternalDeleteObject(new HandleRef(null, iconinfo.hbmMask));
						iconinfo.hbmMask = IntPtr.Zero;
					}
					if (iconinfo.hbmColor != IntPtr.Zero)
					{
						SafeNativeMethods.ExternalDeleteObject(new HandleRef(null, iconinfo.hbmColor));
						iconinfo.hbmColor = IntPtr.Zero;
					}
					icon.Dispose();
				}
				return result;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x00042EAC File Offset: 0x000410AC
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x00042ED7 File Offset: 0x000410D7
		public static Point Position
		{
			get
			{
				NativeMethods.POINT point = new NativeMethods.POINT();
				UnsafeNativeMethods.GetCursorPos(point);
				return new Point(point.x, point.y);
			}
			set
			{
				IntSecurity.AdjustCursorPosition.Demand();
				UnsafeNativeMethods.SetCursorPos(value.X, value.Y);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x00042EF7 File Offset: 0x000410F7
		public Size Size
		{
			get
			{
				if (Cursor.cursorSize.IsEmpty)
				{
					Cursor.cursorSize = new Size(UnsafeNativeMethods.GetSystemMetrics(13), UnsafeNativeMethods.GetSystemMetrics(14));
				}
				return Cursor.cursorSize;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00042F22 File Offset: 0x00041122
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x00042F2A File Offset: 0x0004112A
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

		// Token: 0x060013F0 RID: 5104 RVA: 0x00042F34 File Offset: 0x00041134
		public IntPtr CopyHandle()
		{
			Size size = this.Size;
			return SafeNativeMethods.CopyImage(new HandleRef(this, this.Handle), 2, size.Width, size.Height, 0);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00042F69 File Offset: 0x00041169
		private void DestroyHandle()
		{
			if (this.ownHandle)
			{
				UnsafeNativeMethods.DestroyCursor(new HandleRef(this, this.handle));
			}
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00042F85 File Offset: 0x00041185
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00042F94 File Offset: 0x00041194
		private void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				this.DestroyHandle();
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00042FBC File Offset: 0x000411BC
		private void DrawImageCore(Graphics graphics, Rectangle imageRect, Rectangle targetRect, bool stretch)
		{
			targetRect.X += (int)graphics.Transform.OffsetX;
			targetRect.Y += (int)graphics.Transform.OffsetY;
			int num = 13369376;
			IntPtr hdc = graphics.GetHdc();
			try
			{
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				Size size = this.Size;
				int width;
				int height;
				if (!imageRect.IsEmpty)
				{
					num2 = imageRect.X;
					num3 = imageRect.Y;
					width = imageRect.Width;
					height = imageRect.Height;
				}
				else
				{
					width = size.Width;
					height = size.Height;
				}
				int width2;
				int height2;
				if (!targetRect.IsEmpty)
				{
					num4 = targetRect.X;
					num5 = targetRect.Y;
					width2 = targetRect.Width;
					height2 = targetRect.Height;
				}
				else
				{
					width2 = size.Width;
					height2 = size.Height;
				}
				int width3;
				int height3;
				int num6;
				int num7;
				if (stretch)
				{
					if (width2 == width && height2 == height && num2 == 0 && num3 == 0 && num == 13369376 && width == size.Width && height == size.Height)
					{
						SafeNativeMethods.DrawIcon(new HandleRef(graphics, hdc), num4, num5, new HandleRef(this, this.handle));
						return;
					}
					width3 = size.Width * width2 / width;
					height3 = size.Height * height2 / height;
					num6 = width2;
					num7 = height2;
				}
				else
				{
					if (num2 == 0 && num3 == 0 && num == 13369376 && size.Width <= width2 && size.Height <= height2 && size.Width == width && size.Height == height)
					{
						SafeNativeMethods.DrawIcon(new HandleRef(graphics, hdc), num4, num5, new HandleRef(this, this.handle));
						return;
					}
					width3 = size.Width;
					height3 = size.Height;
					num6 = ((width2 < width) ? width2 : width);
					num7 = ((height2 < height) ? height2 : height);
				}
				if (num == 13369376)
				{
					SafeNativeMethods.IntersectClipRect(new HandleRef(this, this.Handle), num4, num5, num4 + num6, num5 + num7);
					SafeNativeMethods.DrawIconEx(new HandleRef(graphics, hdc), num4 - num2, num5 - num3, new HandleRef(this, this.handle), width3, height3, 0, NativeMethods.NullHandleRef, 3);
				}
			}
			finally
			{
				graphics.ReleaseHdcInternal(hdc);
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00043228 File Offset: 0x00041428
		public void Draw(Graphics g, Rectangle targetRect)
		{
			this.DrawImageCore(g, Rectangle.Empty, targetRect, false);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00043238 File Offset: 0x00041438
		public void DrawStretched(Graphics g, Rectangle targetRect)
		{
			this.DrawImageCore(g, Rectangle.Empty, targetRect, true);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00043248 File Offset: 0x00041448
		~Cursor()
		{
			this.Dispose(false);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00043278 File Offset: 0x00041478
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			if (this.cursorData != null)
			{
				si.AddValue("CursorData", this.cursorData, typeof(byte[]));
				return;
			}
			if (this.resourceId != 0)
			{
				si.AddValue("CursorResourceId", this.resourceId, typeof(int));
				return;
			}
			throw new SerializationException(SR.GetString("CursorNonSerializableHandle"));
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x000432E1 File Offset: 0x000414E1
		public static void Hide()
		{
			IntSecurity.AdjustCursorClip.Demand();
			UnsafeNativeMethods.ShowCursor(false);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x000432F4 File Offset: 0x000414F4
		private void LoadFromResourceId(int nResourceId)
		{
			this.ownHandle = false;
			try
			{
				this.resourceId = nResourceId;
				this.handle = SafeNativeMethods.LoadCursor(NativeMethods.NullHandleRef, nResourceId);
			}
			catch (Exception ex)
			{
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00043340 File Offset: 0x00041540
		private Size GetIconSize(IntPtr iconHandle)
		{
			Size size = this.Size;
			NativeMethods.ICONINFO iconinfo = new NativeMethods.ICONINFO();
			SafeNativeMethods.GetIconInfo(new HandleRef(this, iconHandle), iconinfo);
			NativeMethods.BITMAP bitmap = new NativeMethods.BITMAP();
			if (iconinfo.hbmColor != IntPtr.Zero)
			{
				UnsafeNativeMethods.GetObject(new HandleRef(null, iconinfo.hbmColor), Marshal.SizeOf(typeof(NativeMethods.BITMAP)), bitmap);
				SafeNativeMethods.IntDeleteObject(new HandleRef(null, iconinfo.hbmColor));
				size = new Size(bitmap.bmWidth, bitmap.bmHeight);
			}
			else if (iconinfo.hbmMask != IntPtr.Zero)
			{
				UnsafeNativeMethods.GetObject(new HandleRef(null, iconinfo.hbmMask), Marshal.SizeOf(typeof(NativeMethods.BITMAP)), bitmap);
				size = new Size(bitmap.bmWidth, bitmap.bmHeight / 2);
			}
			if (iconinfo.hbmMask != IntPtr.Zero)
			{
				SafeNativeMethods.IntDeleteObject(new HandleRef(null, iconinfo.hbmMask));
			}
			return size;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00043438 File Offset: 0x00041638
		private void LoadPicture(UnsafeNativeMethods.IStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			try
			{
				Guid guid = typeof(UnsafeNativeMethods.IPicture).GUID;
				UnsafeNativeMethods.IPicture picture = null;
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					picture = UnsafeNativeMethods.OleCreateIPictureIndirect(null, ref guid, true);
					UnsafeNativeMethods.IPersistStream persistStream = (UnsafeNativeMethods.IPersistStream)picture;
					persistStream.Load(stream);
					if (picture == null || picture.GetPictureType() != 3)
					{
						throw new ArgumentException(SR.GetString("InvalidPictureType", new object[]
						{
							"picture",
							"Cursor"
						}), "picture");
					}
					IntPtr iconHandle = picture.GetHandle();
					Size logicalSize = this.GetIconSize(iconHandle);
					if (DpiHelper.IsScalingRequired)
					{
						logicalSize = DpiHelper.LogicalToDeviceUnits(logicalSize, 0);
					}
					this.handle = SafeNativeMethods.CopyImageAsCursor(new HandleRef(this, iconHandle), 2, logicalSize.Width, logicalSize.Height, 0);
					this.ownHandle = true;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
					if (picture != null)
					{
						Marshal.ReleaseComObject(picture);
					}
				}
			}
			catch (COMException innerException)
			{
				throw new ArgumentException(SR.GetString("InvalidPictureFormat"), "stream", innerException);
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00043558 File Offset: 0x00041758
		internal void SavePicture(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this.resourceId != 0)
			{
				throw new FormatException(SR.GetString("CursorCannotCovertToBytes"));
			}
			try
			{
				stream.Write(this.cursorData, 0, this.cursorData.Length);
			}
			catch (SecurityException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(SR.GetString("InvalidPictureFormat"));
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x000435D4 File Offset: 0x000417D4
		public static void Show()
		{
			UnsafeNativeMethods.ShowCursor(true);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x000435E0 File Offset: 0x000417E0
		public override string ToString()
		{
			string str;
			if (!this.ownHandle)
			{
				str = TypeDescriptor.GetConverter(typeof(Cursor)).ConvertToString(this);
			}
			else
			{
				str = base.ToString();
			}
			return "[Cursor: " + str + "]";
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00043626 File Offset: 0x00041826
		public static bool operator ==(Cursor left, Cursor right)
		{
			return left == null == (right == null) && (left == null || left.handle == right.handle);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0004364A File Offset: 0x0004184A
		public static bool operator !=(Cursor left, Cursor right)
		{
			return !(left == right);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00043656 File Offset: 0x00041856
		public override int GetHashCode()
		{
			return (int)this.handle;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00043663 File Offset: 0x00041863
		public override bool Equals(object obj)
		{
			return obj is Cursor && this == (Cursor)obj;
		}

		// Token: 0x04000956 RID: 2390
		private static Size cursorSize = Size.Empty;

		// Token: 0x04000957 RID: 2391
		private byte[] cursorData;

		// Token: 0x04000958 RID: 2392
		private IntPtr handle = IntPtr.Zero;

		// Token: 0x04000959 RID: 2393
		private bool ownHandle = true;

		// Token: 0x0400095A RID: 2394
		private int resourceId;

		// Token: 0x0400095B RID: 2395
		private object userData;
	}
}
