using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000005 RID: 5
	internal static class ByteExtensions
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002334 File Offset: 0x00000534
		public static string ToHexString(this IEnumerable<byte> bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}
	}
}
