using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	// Token: 0x020003DC RID: 988
	[ComVisible(true)]
	public class StandardOleMarshalObject : MarshalByRefObject, UnsafeNativeMethods.IMarshal
	{
		// Token: 0x06002603 RID: 9731 RVA: 0x000B08AB File Offset: 0x000AEAAB
		protected StandardOleMarshalObject()
		{
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000B08B4 File Offset: 0x000AEAB4
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private IntPtr GetStdMarshaler(ref Guid riid, int dwDestContext, int mshlflags)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this);
			if (iunknownForObject != IntPtr.Zero)
			{
				try
				{
					if (UnsafeNativeMethods.CoGetStandardMarshal(ref riid, iunknownForObject, dwDestContext, IntPtr.Zero, mshlflags, out zero) == 0)
					{
						return zero;
					}
				}
				finally
				{
					Marshal.Release(iunknownForObject);
				}
			}
			throw new InvalidOperationException(SR.GetString("StandardOleMarshalObjectGetMarshalerFailed", new object[]
			{
				riid.ToString()
			}));
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000B0934 File Offset: 0x000AEB34
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int UnsafeNativeMethods.IMarshal.GetUnmarshalClass(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out Guid pCid)
		{
			pCid = StandardOleMarshalObject.CLSID_StdMarshal;
			return 0;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x000B0944 File Offset: 0x000AEB44
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		unsafe int UnsafeNativeMethods.IMarshal.GetMarshalSizeMax(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out int pSize)
		{
			IntPtr stdMarshaler = this.GetStdMarshaler(ref riid, dwDestContext, mshlflags);
			int result;
			try
			{
				IntPtr intPtr = *(IntPtr*)stdMarshaler.ToPointer();
				IntPtr ptr = *(IntPtr*)((byte*)intPtr.ToPointer() + (IntPtr)4 * (IntPtr)sizeof(IntPtr));
				StandardOleMarshalObject.GetMarshalSizeMax_Delegate getMarshalSizeMax_Delegate = (StandardOleMarshalObject.GetMarshalSizeMax_Delegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(StandardOleMarshalObject.GetMarshalSizeMax_Delegate));
				result = getMarshalSizeMax_Delegate(stdMarshaler, ref riid, pv, dwDestContext, pvDestContext, mshlflags, out pSize);
			}
			finally
			{
				Marshal.Release(stdMarshaler);
			}
			return result;
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000B09BC File Offset: 0x000AEBBC
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		unsafe int UnsafeNativeMethods.IMarshal.MarshalInterface(IntPtr pStm, ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags)
		{
			IntPtr stdMarshaler = this.GetStdMarshaler(ref riid, dwDestContext, mshlflags);
			int result;
			try
			{
				IntPtr intPtr = *(IntPtr*)stdMarshaler.ToPointer();
				IntPtr ptr = *(IntPtr*)((byte*)intPtr.ToPointer() + (IntPtr)5 * (IntPtr)sizeof(IntPtr));
				StandardOleMarshalObject.MarshalInterface_Delegate marshalInterface_Delegate = (StandardOleMarshalObject.MarshalInterface_Delegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(StandardOleMarshalObject.MarshalInterface_Delegate));
				result = marshalInterface_Delegate(stdMarshaler, pStm, ref riid, pv, dwDestContext, pvDestContext, mshlflags);
			}
			finally
			{
				Marshal.Release(stdMarshaler);
			}
			return result;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000B0A38 File Offset: 0x000AEC38
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int UnsafeNativeMethods.IMarshal.UnmarshalInterface(IntPtr pStm, ref Guid riid, out IntPtr ppv)
		{
			ppv = IntPtr.Zero;
			return -2147467263;
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000B0A46 File Offset: 0x000AEC46
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int UnsafeNativeMethods.IMarshal.ReleaseMarshalData(IntPtr pStm)
		{
			return -2147467263;
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000B0A4D File Offset: 0x000AEC4D
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int UnsafeNativeMethods.IMarshal.DisconnectObject(int dwReserved)
		{
			return -2147467263;
		}

		// Token: 0x04002088 RID: 8328
		private static readonly Guid CLSID_StdMarshal = new Guid("00000017-0000-0000-c000-000000000046");

		// Token: 0x0200080F RID: 2063
		// (Invoke) Token: 0x060044F2 RID: 17650
		[SuppressUnmanagedCodeSecurity]
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int GetMarshalSizeMax_Delegate(IntPtr _this, ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out int pSize);

		// Token: 0x02000810 RID: 2064
		// (Invoke) Token: 0x060044F6 RID: 17654
		[SuppressUnmanagedCodeSecurity]
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int MarshalInterface_Delegate(IntPtr _this, IntPtr pStm, ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags);
	}
}
