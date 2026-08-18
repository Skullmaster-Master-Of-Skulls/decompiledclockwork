using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000002 RID: 2
	internal static class ByteExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
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
