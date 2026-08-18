using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000284 RID: 644
	internal sealed class DualCoTaskMem : SafeHandle
	{
		// Token: 0x060026FD RID: 9981 RVA: 0x0010844C File Offset: 0x0010784C
		private DualCoTaskMem() : base(IntPtr.Zero, true)
		{
			this.handle2 = IntPtr.Zero;
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x00108470 File Offset: 0x00107870
		internal DualCoTaskMem(UnsafeNativeMethods.IDBInfo dbInfo, int[] literals, out int literalCount, out IntPtr literalInfo, out OleDbHResult hr) : this()
		{
			int cLiterals = (literals != null) ? literals.Length : 0;
			Bid.Trace("<oledb.IDBInfo.GetLiteralInfo|API|OLEDB>\n");
			hr = dbInfo.GetLiteralInfo(cLiterals, literals, out literalCount, out this.handle, out this.handle2);
			literalInfo = this.handle;
			Bid.Trace("<oledb.IDBInfo.GetLiteralInfo|API|OLEDB|RET> %08X{HRESULT}\n", hr);
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x001084C8 File Offset: 0x001078C8
		internal DualCoTaskMem(UnsafeNativeMethods.IColumnsInfo columnsInfo, out IntPtr columnCount, out IntPtr columnInfos, out OleDbHResult hr) : this()
		{
			Bid.Trace("<oledb.IColumnsInfo.GetColumnInfo|API|OLEDB>\n");
			hr = columnsInfo.GetColumnInfo(out columnCount, out this.handle, out this.handle2);
			columnInfos = this.handle;
			Bid.Trace("<oledb.IColumnsInfo.GetColumnInfo|API|OLEDB|RET> %08X{HRESULT}\n", hr);
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x00108510 File Offset: 0x00107910
		internal DualCoTaskMem(UnsafeNativeMethods.IDBSchemaRowset dbSchemaRowset, out int schemaCount, out IntPtr schemaGuids, out IntPtr schemaRestrictions, out OleDbHResult hr) : this()
		{
			Bid.Trace("<oledb.IDBSchemaRowset.GetSchemas|API|OLEDB>\n");
			hr = dbSchemaRowset.GetSchemas(out schemaCount, out this.handle, out this.handle2);
			schemaGuids = this.handle;
			schemaRestrictions = this.handle2;
			Bid.Trace("<oledb.IDBSchemaRowset.GetSchemas|API|OLEDB|RET> %08X{HRESULT}\n", hr);
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x00108564 File Offset: 0x00107964
		internal DualCoTaskMem(UnsafeNativeMethods.IColumnsRowset icolumnsRowset, out IntPtr cOptColumns, out OleDbHResult hr) : base(IntPtr.Zero, true)
		{
			Bid.Trace("<oledb.IColumnsRowset.GetAvailableColumns|API|OLEDB>\n");
			hr = icolumnsRowset.GetAvailableColumns(out cOptColumns, out this.handle);
			Bid.Trace("<oledb.IColumnsRowset.GetAvailableColumns|API|OLEDB|RET> %08X{HRESULT}\n", hr);
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x001085A4 File Offset: 0x001079A4
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle && IntPtr.Zero == this.handle2;
			}
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x001085D8 File Offset: 0x001079D8
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

		// Token: 0x040019EE RID: 6638
		private IntPtr handle2;
	}
}
