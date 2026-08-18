using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000276 RID: 630
	internal sealed class DataSourceWrapper : WrappedIUnknown
	{
		// Token: 0x06002688 RID: 9864 RVA: 0x00104FD0 File Offset: 0x001043D0
		internal DataSourceWrapper()
		{
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x00104FE4 File Offset: 0x001043E4
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

		// Token: 0x0600268A RID: 9866 RVA: 0x001051E0 File Offset: 0x001045E0
		internal IDBInfoWrapper IDBInfo(OleDbConnectionInternal connection)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|datasource> %d#, IDBInfo\n", connection.ObjectID);
			return new IDBInfoWrapper(base.ComWrapper());
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x00105208 File Offset: 0x00104608
		internal IDBPropertiesWrapper IDBProperties(OleDbConnectionInternal connection)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|datasource> %d#, IDBProperties\n", connection.ObjectID);
			return new IDBPropertiesWrapper(base.ComWrapper());
		}
	}
}
