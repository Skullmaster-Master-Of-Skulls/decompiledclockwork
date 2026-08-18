using System;

namespace System.Data.Entity
{
	// Token: 0x02000127 RID: 295
	internal static class Error
	{
		// Token: 0x06001562 RID: 5474 RVA: 0x00003403 File Offset: 0x00001603
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0000340B File Offset: 0x0000160B
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x000488CA File Offset: 0x00046ACA
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00003487 File Offset: 0x00001687
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
