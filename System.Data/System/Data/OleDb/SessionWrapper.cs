using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000254 RID: 596
	internal sealed class SessionWrapper : WrappedIUnknown
	{
		// Token: 0x06002085 RID: 8325 RVA: 0x00280DA8 File Offset: 0x002801A8
		internal SessionWrapper()
		{
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00280DC8 File Offset: 0x002801C8
		internal void QueryInterfaceIDBCreateCommand(OleDbConnectionString constr)
		{
			if (!constr.HaveQueriedForCreateCommand || constr.DangerousIDBCreateCommandCreateCommand != null)
			{
				IntPtr zero = IntPtr.Zero;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					IntPtr ptr = Marshal.ReadIntPtr(this.handle, 0);
					IntPtr ptr2 = Marshal.ReadIntPtr(ptr, 0);
					UnsafeNativeMethods.IUnknownQueryInterface unknownQueryInterface = (UnsafeNativeMethods.IUnknownQueryInterface)Marshal.GetDelegateForFunctionPointer(ptr2, typeof(UnsafeNativeMethods.IUnknownQueryInterface));
					int num = unknownQueryInterface(this.handle, ref ODB.IID_IDBCreateCommand, ref zero);
					if (0 <= num && IntPtr.Zero != zero)
					{
						ptr = Marshal.ReadIntPtr(zero, 0);
						ptr2 = Marshal.ReadIntPtr(ptr, 3 * IntPtr.Size);
						this.DangerousIDBCreateCommandCreateCommand = (UnsafeNativeMethods.IDBCreateCommandCreateCommand)Marshal.GetDelegateForFunctionPointer(ptr2, typeof(UnsafeNativeMethods.IDBCreateCommandCreateCommand));
						constr.DangerousIDBCreateCommandCreateCommand = this.DangerousIDBCreateCommandCreateCommand;
					}
					constr.HaveQueriedForCreateCommand = true;
				}
				finally
				{
					if (IntPtr.Zero != zero)
					{
						IntPtr handle = this.handle;
						this.handle = zero;
						Marshal.Release(handle);
					}
				}
			}
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00280ED8 File Offset: 0x002802D8
		internal void VerifyIDBCreateCommand(OleDbConnectionString constr)
		{
			IntPtr ptr = Marshal.ReadIntPtr(this.handle, 0);
			IntPtr intPtr = Marshal.ReadIntPtr(ptr, 3 * IntPtr.Size);
			UnsafeNativeMethods.IDBCreateCommandCreateCommand idbcreateCommandCreateCommand = constr.DangerousIDBCreateCommandCreateCommand;
			if (idbcreateCommandCreateCommand == null || intPtr != Marshal.GetFunctionPointerForDelegate(idbcreateCommandCreateCommand))
			{
				idbcreateCommandCreateCommand = (UnsafeNativeMethods.IDBCreateCommandCreateCommand)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(UnsafeNativeMethods.IDBCreateCommandCreateCommand));
				constr.DangerousIDBCreateCommandCreateCommand = idbcreateCommandCreateCommand;
			}
			this.DangerousIDBCreateCommandCreateCommand = idbcreateCommandCreateCommand;
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00280F48 File Offset: 0x00280348
		internal OleDbHResult CreateCommand(ref object icommandText)
		{
			OleDbHResult result = OleDbHResult.E_NOINTERFACE;
			UnsafeNativeMethods.IDBCreateCommandCreateCommand dangerousIDBCreateCommandCreateCommand = this.DangerousIDBCreateCommandCreateCommand;
			if (dangerousIDBCreateCommandCreateCommand != null)
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.DangerousAddRef(ref flag);
					result = dangerousIDBCreateCommandCreateCommand(this.handle, IntPtr.Zero, ref ODB.IID_ICommandText, ref icommandText);
				}
				finally
				{
					if (flag)
					{
						base.DangerousRelease();
					}
				}
			}
			return result;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00280FB8 File Offset: 0x002803B8
		internal IDBSchemaRowsetWrapper IDBSchemaRowset(OleDbConnectionInternal connection)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|session> %d#, IDBSchemaRowset\n", connection.ObjectID);
			return new IDBSchemaRowsetWrapper(base.ComWrapper());
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00280FE8 File Offset: 0x002803E8
		internal IOpenRowsetWrapper IOpenRowset(OleDbConnectionInternal connection)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|session> %d#, IOpenRowset\n", connection.ObjectID);
			return new IOpenRowsetWrapper(base.ComWrapper());
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00281018 File Offset: 0x00280418
		internal ITransactionJoinWrapper ITransactionJoin(OleDbConnectionInternal connection)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|session> %d#, ITransactionJoin\n", connection.ObjectID);
			return new ITransactionJoinWrapper(base.ComWrapper());
		}

		// Token: 0x04001528 RID: 5416
		private UnsafeNativeMethods.IDBCreateCommandCreateCommand DangerousIDBCreateCommandCreateCommand;
	}
}
