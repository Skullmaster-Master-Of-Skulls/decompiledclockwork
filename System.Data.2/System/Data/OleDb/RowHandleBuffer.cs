using System;
using System.Data.Common;
using System.Data.ProviderBase;

namespace System.Data.OleDb
{
	// Token: 0x02000285 RID: 645
	internal sealed class RowHandleBuffer : DbBuffer
	{
		// Token: 0x06002704 RID: 9988 RVA: 0x00108630 File Offset: 0x00107A30
		internal RowHandleBuffer(IntPtr rowHandleFetchCount) : base((int)rowHandleFetchCount * ADP.PtrSize)
		{
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x00108650 File Offset: 0x00107A50
		internal IntPtr GetRowHandle(int index)
		{
			return base.ReadIntPtr(index * ADP.PtrSize);
		}
	}
}
