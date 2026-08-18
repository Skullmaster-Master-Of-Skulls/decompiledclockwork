using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000275 RID: 629
	internal sealed class OleDbServicesWrapper : WrappedIUnknown
	{
		// Token: 0x06002686 RID: 9862 RVA: 0x00104E98 File Offset: 0x00104298
		internal OleDbServicesWrapper(object unknown)
		{
			if (unknown != null)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this.handle = Marshal.GetComInterfaceForObject(unknown, typeof(UnsafeNativeMethods.IDataInitialize));
				}
				IntPtr ptr = Marshal.ReadIntPtr(this.handle, 0);
				IntPtr ptr2 = Marshal.ReadIntPtr(ptr, 3 * IntPtr.Size);
				this.DangerousIDataInitializeGetDataSource = (UnsafeNativeMethods.IDataInitializeGetDataSource)Marshal.GetDelegateForFunctionPointer(ptr2, typeof(UnsafeNativeMethods.IDataInitializeGetDataSource));
			}
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x00104F20 File Offset: 0x00104320
		internal void GetDataSource(OleDbConnectionString constr, ref DataSourceWrapper datasrcWrapper)
		{
			UnsafeNativeMethods.IDataInitializeGetDataSource dangerousIDataInitializeGetDataSource = this.DangerousIDataInitializeGetDataSource;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			OleDbHResult oleDbHResult;
			try
			{
				base.DangerousAddRef(ref flag);
				string actualConnectionString = constr.ActualConnectionString;
				oleDbHResult = dangerousIDataInitializeGetDataSource(this.handle, IntPtr.Zero, 23, actualConnectionString, ref ODB.IID_IDBInitialize, ref datasrcWrapper);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			if (oleDbHResult < OleDbHResult.S_OK)
			{
				if (OleDbHResult.REGDB_E_CLASSNOTREG == oleDbHResult)
				{
					throw ODB.ProviderUnavailable(constr.Provider, null);
				}
				Exception ex = OleDbConnection.ProcessResults(oleDbHResult, null, null);
				throw ex;
			}
			else
			{
				if (datasrcWrapper.IsInvalid)
				{
					SafeNativeMethods.Wrapper.ClearErrorInfo();
					throw ODB.ProviderUnavailable(constr.Provider, null);
				}
				return;
			}
		}

		// Token: 0x04001838 RID: 6200
		private UnsafeNativeMethods.IDataInitializeGetDataSource DangerousIDataInitializeGetDataSource;
	}
}
