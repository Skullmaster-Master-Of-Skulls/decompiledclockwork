using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.ServiceModel
{
	// Token: 0x02000052 RID: 82
	internal static class StringUtil
	{
		// Token: 0x06000229 RID: 553 RVA: 0x0000C6DB File Offset: 0x0000A8DB
		internal static int GetNonRandomizedHashCode(string str)
		{
			if (!StringUtil.randomizedStringHashingEnabled)
			{
				return str.GetHashCode();
			}
			return StringUtil.GetStableHashCode(str);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		[SecuritySafeCritical]
		private unsafe static int GetStableHashCode(string str)
		{
			char* ptr = str;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 352654597;
			int num2 = num;
			int* ptr2 = (int*)ptr;
			int i;
			for (i = str.Length; i > 2; i -= 4)
			{
				num = ((num << 5) + num + (num >> 27) ^ *ptr2);
				num2 = ((num2 << 5) + num2 + (num2 >> 27) ^ ptr2[1]);
				ptr2 += 2;
			}
			if (i > 0)
			{
				num = ((num << 5) + num + (num >> 27) ^ *ptr2);
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x040004AC RID: 1196
		private static readonly bool randomizedStringHashingEnabled = StringComparer.InvariantCultureIgnoreCase.GetHashCode("The quick brown fox jumps over the lazy dog.") != 1883137582;
	}
}
