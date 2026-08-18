using System;
using System.Drawing;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E5 RID: 1253
	internal abstract class WindowsBrush : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x060051BE RID: 20926
		public abstract object Clone();

		// Token: 0x060051BF RID: 20927
		protected abstract void CreateBrush();

		// Token: 0x060051C0 RID: 20928 RVA: 0x00153C76 File Offset: 0x00151E76
		public WindowsBrush(DeviceContext dc)
		{
			this.dc = dc;
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x00153C90 File Offset: 0x00151E90
		public WindowsBrush(DeviceContext dc, Color color)
		{
			this.dc = dc;
			this.color = color;
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x00153CB4 File Offset: 0x00151EB4
		~WindowsBrush()
		{
			this.Dispose(false);
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x060051C3 RID: 20931 RVA: 0x00153CE4 File Offset: 0x00151EE4
		protected DeviceContext DC
		{
			get
			{
				return this.dc;
			}
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x00153CEC File Offset: 0x00151EEC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x00153CF8 File Offset: 0x00151EF8
		protected virtual void Dispose(bool disposing)
		{
			if (this.dc != null && this.nativeHandle != IntPtr.Zero)
			{
				this.dc.DeleteObject(this.nativeHandle, GdiObjectType.Brush);
				this.nativeHandle = IntPtr.Zero;
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x060051C6 RID: 20934 RVA: 0x00153D45 File Offset: 0x00151F45
		public Color Color
		{
			get
			{
				return this.color;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x060051C7 RID: 20935 RVA: 0x00153D4D File Offset: 0x00151F4D
		// (set) Token: 0x060051C8 RID: 20936 RVA: 0x00153D6D File Offset: 0x00151F6D
		protected IntPtr NativeHandle
		{
			get
			{
				if (this.nativeHandle == IntPtr.Zero)
				{
					this.CreateBrush();
				}
				return this.nativeHandle;
			}
			set
			{
				this.nativeHandle = value;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x060051C9 RID: 20937 RVA: 0x00153D76 File Offset: 0x00151F76
		public IntPtr HBrush
		{
			get
			{
				return this.NativeHandle;
			}
		}

		// Token: 0x040035DB RID: 13787
		private DeviceContext dc;

		// Token: 0x040035DC RID: 13788
		private IntPtr nativeHandle;

		// Token: 0x040035DD RID: 13789
		private Color color = Color.White;
	}
}
