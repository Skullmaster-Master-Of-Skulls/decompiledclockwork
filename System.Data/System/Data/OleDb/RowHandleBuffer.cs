using System;
using System.Data.Common;
using System.Data.ProviderBase;

namespace System.Data.OleDb
{
	// Token: 0x02000260 RID: 608
	internal sealed class RowHandleBuffer : DbBuffer
	{
		// Token: 0x060020C5 RID: 8389 RVA: 0x002824E8 File Offset: 0x002818E8
		internal RowHandleBuffer(IntPtr rowHandleFetchCount) : base((int)rowHandleFetchCount * ADP.PtrSize)
		{
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x00282508 File Offset: 0x00281908
		internal IntPtr GetRowHandle(int index)
		{
			return base.ReadIntPtr(index * ADP.PtrSize);
		}
	}
}
