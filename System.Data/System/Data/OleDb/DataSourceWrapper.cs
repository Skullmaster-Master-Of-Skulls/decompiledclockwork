using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000253 RID: 595
	internal sealed class DataSourceWrapper : WrappedIUnknown
	{
		// Token: 0x06002081 RID: 8321 RVA: 0x00280B28 File Offset: 0x0027FF28
		internal DataSourceWrapper()
		{
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00280B48 File Offset: 0x0027FF48
		internal OleDbHResult InitializeAndCreateSession(OleDbConnectionString constr, ref SessionWrapper sessionWrapper)
		{
			bool flag = false;
			IntPtr zero = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			OleDbHResult oleDbHResult;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = Marshal.ReadIntPtr(this.handle, 0);
				IntPtr intPtr = Marshal.ReadIntPtr(ptr, 0);
				UnsafeNativeMethods.IUnknownQueryInterface unknownQueryInterface = constr.DangerousDataSourceIUnknownQueryInterface;
				if (unknownQueryInterface == null || intPtr != Marshal.GetFunctionPointerForDelegate(unknownQueryInterface))
				{
					unknownQueryInterface = (UnsafeNativeMethods.IUnknownQueryInterface)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(UnsafeNativeMethods.IUnknownQueryInterface));
					constr.DangerousDataSourceIUnknownQueryInterface = unknownQueryInterface;
				}
				ptr = Marshal.ReadIntPtr(this.handle, 0);
				intPtr = Marshal.ReadIntPtr(ptr, 3 * IntPtr.Size);
				UnsafeNativeMethods.IDBInitializeInitialize idbinitializeInitialize = constr.DangerousIDBInitializeInitialize;
				if (idbinitializeInitialize == null || intPtr != Marshal.GetFunctionPointerForDelegate(idbinitializeInitialize))
				{
					idbinitializeInitialize = (UnsafeNativeMethods.IDBInitializeInitialize)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(UnsafeNativeMethods.IDBInitializeInitialize));
					constr.DangerousIDBInitializeInitialize = idbinitializeInitialize;
				}
				oleDbHResult = idbinitializeInitialize(this.handle);
				if (OleDbHResult.S_OK <= oleDbHResult || OleDbHResult.DB_E_ALREADYINITIALIZED == oleDbHResult)
				{
					oleDbHResult = (OleDbHResult)unknownQueryInterface(this.handle, ref ODB.IID_IDBCreateSession, ref zero);
					if (OleDbHResult.S_OK <= oleDbHResult && IntPtr.Zero != zero)
					{
						ptr = Marshal.ReadIntPtr(zero, 0);
						intPtr = Marshal.ReadIntPtr(ptr, 3 * IntPtr.Size);
						UnsafeNativeMethods.IDBCreateSessionCreateSession idbcreateSessionCreateSession = constr.DangerousIDBCreateSessionCreateSession;
						if (idbcreateSessionCreateSession == null || intPtr != Marshal.GetFunctionPointerForDelegate(idbcreateSessionCreateSession))
						{
							idbcreateSessionCreateSession = (UnsafeNativeMethods.IDBCreateSessionCreateSession)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(UnsafeNativeMethods.IDBCreateSessionCreateSession));
							constr.DangerousIDBCreateSessionCreateSession = idbcreateSessionCreateSession;
						}
						if (constr.DangerousIDBCreateCommandCreateCommand != null)
						{
							oleDbHResult = idbcreateSessionCreateSession(zero, IntPtr.Zero, ref ODB.IID_IDBCreateCommand, ref sessionWrapper);
							if (OleDbHResult.S_OK <= oleDbHResult && !sessionWrapper.IsInvalid)
							{
								sessionWrapper.VerifyIDBCreateCommand(constr);
							}
						}
						else
						{
							oleDbHResult = idbcreateSessionCreateSession(zero, IntPtr.Zero, ref ODB.IID_IUnknown, ref sessionWrapper);
							if (OleDbHResult.S_OK <= oleDbHResult && !sessionWrapper.IsInvalid)
							{
								sessionWrapper.QueryInterfaceIDBCreateCommand(constr);
							}
						}
					}
				}
			}
			finally
			{
				if (IntPtr.Zero != zero)
				{
					Marshal.Release(zero);
				}
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return oleDbHResult;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x00280D48 File Offset: 0x00280148
		internal IDBInfoWrapper IDBInfo(OleDbConnectionInternal connection)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|datasource> %d#, IDBInfo\n", connection.ObjectID);
			return new IDBInfoWrapper(base.ComWrapper());
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00280D78 File Offset: 0x00280178
		internal IDBPropertiesWrapper IDBProperties(OleDbConnectionInternal connection)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|datasource> %d#, IDBProperties\n", connection.ObjectID);
			return new IDBPropertiesWrapper(base.ComWrapper());
		}
	}
}
