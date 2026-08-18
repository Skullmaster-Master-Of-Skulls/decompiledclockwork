using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.Common
{
	// Token: 0x0200014D RID: 333
	internal sealed class SafeMD5Helper
	{
		// Token: 0x06000D30 RID: 3376 RVA: 0x0002F465 File Offset: 0x0002D665
		private SafeMD5Helper()
		{
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0002F46D File Offset: 0x0002D66D
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public static uint RotateLeft(uint uiNumber, ushort shift)
		{
			return uiNumber >> (int)(32 - shift) | uiNumber << (int)shift;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0002F47F File Offset: 0x0002D67F
		public static uint ReverseByte(uint uiNumber)
		{
			return (uiNumber & 255U) << 24 | uiNumber >> 24 | (uiNumber & 16711680U) >> 8 | (uiNumber & 65280U) << 8;
		}
	}
}
