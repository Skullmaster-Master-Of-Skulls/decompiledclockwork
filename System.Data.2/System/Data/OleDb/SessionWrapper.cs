using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000277 RID: 631
	internal sealed class SessionWrapper : WrappedIUnknown
	{
		// Token: 0x0600268C RID: 9868 RVA: 0x00105230 File Offset: 0x00104630
		internal SessionWrapper()
		{
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x00105244 File Offset: 0x00104644
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

		// Token: 0x0600268E RID: 9870 RVA: 0x00105348 File Offset: 0x00104748
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

		// Token: 0x0600268F RID: 9871 RVA: 0x001053AC File Offset: 0x001047AC
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

		// Token: 0x06002690 RID: 9872 RVA: 0x0010541C File Offset: 0x0010481C
		internal IDBSchemaRowsetWrapper IDBSchemaRowset(OleDbConnectionInternal connection)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|session> %d#, IDBSchemaRowset\n", connection.ObjectID);
			return new IDBSchemaRowsetWrapper(base.ComWrapper());
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x00105444 File Offset: 0x00104844
		internal IOpenRowsetWrapper IOpenRowset(OleDbConnectionInternal connection)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|session> %d#, IOpenRowset\n", connection.ObjectID);
			return new IOpenRowsetWrapper(base.ComWrapper());
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x0010546C File Offset: 0x0010486C
		internal ITransactionJoinWrapper ITransactionJoin(OleDbConnectionInternal connection)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|session> %d#, ITransactionJoin\n", connection.ObjectID);
			return new ITransactionJoinWrapper(base.ComWrapper());
		}

		// Token: 0x04001839 RID: 6201
		private UnsafeNativeMethods.IDBCreateCommandCreateCommand DangerousIDBCreateCommandCreateCommand;
	}
}
