using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x02000012 RID: 18
	public abstract class Brush : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x0600005B RID: 91
		public abstract object Clone();

		// Token: 0x0600005C RID: 92 RVA: 0x0000372C File Offset: 0x0000192C
		protected internal void SetNativeBrush(IntPtr brush)
		{
			IntSecurity.UnmanagedCode.Demand();
			this.SetNativeBrushInternal(brush);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000373F File Offset: 0x0000193F
		internal void SetNativeBrushInternal(IntPtr brush)
		{
			this.nativeBrush = brush;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003748 File Offset: 0x00001948
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal IntPtr NativeBrush
		{
			get
			{
				return this.nativeBrush;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003750 File Offset: 0x00001950
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003760 File Offset: 0x00001960
		protected virtual void Dispose(bool disposing)
		{
			if (this.nativeBrush != IntPtr.Zero)
			{
				try
				{
					SafeNativeMethods.Gdip.GdipDeleteBrush(new HandleRef(this, this.nativeBrush));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.nativeBrush = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000037C8 File Offset: 0x000019C8
		~Brush()
		{
			this.Dispose(false);
		}

		// Token: 0x0400009F RID: 159
		private IntPtr nativeBrush;
	}
}
