using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000218 RID: 536
	internal class ComProxy : IDisposable
	{
		// Token: 0x0600104D RID: 4173 RVA: 0x0003B384 File Offset: 0x00039584
		internal static ComProxy Create(IntPtr outer, object obj, IDisposable disp)
		{
			if (outer == IntPtr.Zero)
			{
				throw Fx.AssertAndThrow("Outer cannot be null");
			}
			IntPtr pUnk = IntPtr.Zero;
			pUnk = Marshal.CreateAggregatedObject(outer, obj);
			int num = Marshal.AddRef(pUnk);
			if (3 == num)
			{
				Marshal.Release(pUnk);
			}
			Marshal.Release(pUnk);
			return new ComProxy(pUnk, disp);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0003B3D7 File Offset: 0x000395D7
		internal ComProxy(IntPtr inner, IDisposable disp)
		{
			this.inner = inner;
			this.ccw = disp;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0003B3F0 File Offset: 0x000395F0
		internal void QueryInterface(ref Guid riid, out IntPtr tearoff)
		{
			if (this.inner == IntPtr.Zero)
			{
				throw Fx.AssertAndThrow("Inner should not be Null at this point");
			}
			int num = Marshal.QueryInterface(this.inner, ref riid, out tearoff);
			if (num != HR.S_OK)
			{
				throw Fx.AssertAndThrow("QueryInterface should succeed");
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0003B43B File Offset: 0x0003963B
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0003B444 File Offset: 0x00039644
		private void Dispose(bool disposing)
		{
			if (this.inner == IntPtr.Zero)
			{
				throw Fx.AssertAndThrow("Inner should not be Null at this point");
			}
			Marshal.Release(this.inner);
			if (disposing && this.ccw != null)
			{
				this.ccw.Dispose();
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0003B490 File Offset: 0x00039690
		public ComProxy Clone()
		{
			if (this.inner == IntPtr.Zero)
			{
				throw Fx.AssertAndThrow("Inner should not be Null at this point");
			}
			Marshal.AddRef(this.inner);
			return new ComProxy(this.inner, null);
		}

		// Token: 0x04001871 RID: 6257
		private IntPtr inner;

		// Token: 0x04001872 RID: 6258
		private IDisposable ccw;
	}
}
