using System;
using System.Collections.Specialized;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x0200077B RID: 1915
	internal static class EnvironmentBlock
	{
		// Token: 0x06003B42 RID: 15170 RVA: 0x000FC1C0 File Offset: 0x000FB1C0
		public static byte[] ToByteArray(StringDictionary sd, bool unicode)
		{
			string[] array = new string[sd.Count];
			sd.Keys.CopyTo(array, 0);
			string[] array2 = new string[sd.Count];
			sd.Values.CopyTo(array2, 0);
			Array.Sort(array, array2, OrdinalCaseInsensitiveComparer.Default);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < sd.Count; i++)
			{
				stringBuilder.Append(array[i]);
				stringBuilder.Append('=');
				stringBuilder.Append(array2[i]);
				stringBuilder.Append('\0');
			}
			stringBuilder.Append('\0');
			byte[] bytes;
			if (unicode)
			{
				bytes = Encoding.Unicode.GetBytes(stringBuilder.ToString());
			}
			else
			{
				bytes = Encoding.Default.GetBytes(stringBuilder.ToString());
			}
			if (bytes.Length > 65535)
			{
				throw new InvalidOperationException(SR.GetString("EnvironmentBlockTooLong", new object[]
				{
					bytes.Length
				}));
			}
			return bytes;
		}
	}
}
