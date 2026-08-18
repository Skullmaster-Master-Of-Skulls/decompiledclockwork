using System;
using System.IdentityModel;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Permissions;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200023A RID: 570
	internal class PersistHelper
	{
		// Token: 0x060010F8 RID: 4344 RVA: 0x0003E198 File Offset: 0x0003C398
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		internal static byte[] ConvertHGlobalToByteArray(SafeHGlobalHandle hGlobal)
		{
			int num = SafeNativeMethods.GlobalSize(hGlobal).ToInt32();
			if (num <= 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			IntPtr intPtr = SafeNativeMethods.GlobalLock(hGlobal);
			if (IntPtr.Zero == intPtr)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new OutOfMemoryException());
			}
			try
			{
				Marshal.Copy(intPtr, array, 0, num);
			}
			finally
			{
				SafeNativeMethods.GlobalUnlock(hGlobal);
			}
			return array;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0003E20C File Offset: 0x0003C40C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		internal static byte[] PersistIPersistStreamToByteArray(IPersistStream persistableObject)
		{
			IStream stream = SafeNativeMethods.CreateStreamOnHGlobal(SafeHGlobalHandle.InvalidHandle, false);
			byte[] result;
			try
			{
				persistableObject.Save(stream, true);
				SafeHGlobalHandle hglobalFromStream = SafeNativeMethods.GetHGlobalFromStream(stream);
				if (hglobalFromStream == null || IntPtr.Zero == hglobalFromStream.DangerousGetHandle())
				{
					throw Fx.AssertAndThrow("HGlobal returned from  GetHGlobalFromStream is NULL");
				}
				result = PersistHelper.ConvertHGlobalToByteArray(hglobalFromStream);
			}
			finally
			{
				Marshal.ReleaseComObject(stream);
			}
			return result;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0003E278 File Offset: 0x0003C478
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		internal static void LoadIntoObjectFromByteArray(IPersistStream persistableObject, byte[] byteStream)
		{
			SafeHGlobalHandle hGlobal = SafeHGlobalHandle.AllocHGlobal(byteStream.Length);
			IntPtr intPtr = SafeNativeMethods.GlobalLock(hGlobal);
			if (IntPtr.Zero == intPtr)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new OutOfMemoryException());
			}
			try
			{
				Marshal.Copy(byteStream, 0, intPtr, byteStream.Length);
				IStream stream = SafeNativeMethods.CreateStreamOnHGlobal(hGlobal, false);
				try
				{
					persistableObject.Load(stream);
				}
				finally
				{
					Marshal.ReleaseComObject(stream);
				}
			}
			finally
			{
				SafeNativeMethods.GlobalUnlock(hGlobal);
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0003E2FC File Offset: 0x0003C4FC
		internal static object ActivateAndLoadFromByteStream(Guid clsid, byte[] byteStream)
		{
			IPersistStream persistStream = SafeNativeMethods.CoCreateInstance(clsid, null, CLSCTX.INPROC_SERVER, typeof(IPersistStream).GUID) as IPersistStream;
			if (persistStream != null)
			{
				PersistHelper.LoadIntoObjectFromByteArray(persistStream, byteStream);
				return persistStream;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CLSIDDoesNotSupportIPersistStream", new object[]
			{
				clsid.ToString("B")
			})));
		}
	}
}
