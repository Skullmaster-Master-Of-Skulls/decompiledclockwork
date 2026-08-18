using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200025F RID: 607
	internal sealed class DualCoTaskMem : SafeHandle
	{
		// Token: 0x060020BE RID: 8382 RVA: 0x002822C8 File Offset: 0x002816C8
		private DualCoTaskMem() : base(IntPtr.Zero, true)
		{
			this.handle2 = IntPtr.Zero;
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x002822F8 File Offset: 0x002816F8
		internal DualCoTaskMem(UnsafeNativeMethods.IDBInfo dbInfo, int[] literals, out int literalCount, out IntPtr literalInfo, out OleDbHResult hr) : this()
		{
			int cLiterals = (literals != null) ? literals.Length : 0;
			Bid.Trace("<oledb.IDBInfo.GetLiteralInfo|API|OLEDB>\n");
			hr = dbInfo.GetLiteralInfo(cLiterals, literals, out literalCount, out this.handle, out this.handle2);
			literalInfo = this.handle;
			Bid.Trace("<oledb.IDBInfo.GetLiteralInfo|API|OLEDB|RET> %08X{HRESULT}\n", hr);
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00282358 File Offset: 0x00281758
		internal DualCoTaskMem(UnsafeNativeMethods.IColumnsInfo columnsInfo, out IntPtr columnCount, out IntPtr columnInfos, out OleDbHResult hr) : this()
		{
			Bid.Trace("<oledb.IColumnsInfo.GetColumnInfo|API|OLEDB>\n");
			hr = columnsInfo.GetColumnInfo(out columnCount, out this.handle, out this.handle2);
			columnInfos = this.handle;
			Bid.Trace("<oledb.IColumnsInfo.GetColumnInfo|API|OLEDB|RET> %08X{HRESULT}\n", hr);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x002823A8 File Offset: 0x002817A8
		internal DualCoTaskMem(UnsafeNativeMethods.IDBSchemaRowset dbSchemaRowset, out int schemaCount, out IntPtr schemaGuids, out IntPtr schemaRestrictions, out OleDbHResult hr) : this()
		{
			Bid.Trace("<oledb.IDBSchemaRowset.GetSchemas|API|OLEDB>\n");
			hr = dbSchemaRowset.GetSchemas(out schemaCount, out this.handle, out this.handle2);
			schemaGuids = this.handle;
			schemaRestrictions = this.handle2;
			Bid.Trace("<oledb.IDBSchemaRowset.GetSchemas|API|OLEDB|RET> %08X{HRESULT}\n", hr);
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x00282408 File Offset: 0x00281808
		internal DualCoTaskMem(UnsafeNativeMethods.IColumnsRowset icolumnsRowset, out IntPtr cOptColumns, out OleDbHResult hr) : base(IntPtr.Zero, true)
		{
			Bid.Trace("<oledb.IColumnsRowset.GetAvailableColumns|API|OLEDB>\n");
			hr = icolumnsRowset.GetAvailableColumns(out cOptColumns, out this.handle);
			Bid.Trace("<oledb.IColumnsRowset.GetAvailableColumns|API|OLEDB|RET> %08X{HRESULT}\n", hr);
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x00282448 File Offset: 0x00281848
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle && IntPtr.Zero == this.handle2;
			}
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x00282488 File Offset: 0x00281888
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				SafeNativeMethods.CoTaskMemFree(handle);
			}
			handle = this.handle2;
			this.handle2 = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				SafeNativeMethods.CoTaskMemFree(handle);
			}
			return true;
		}

		// Token: 0x0400154F RID: 5455
		private IntPtr handle2;
	}
}
