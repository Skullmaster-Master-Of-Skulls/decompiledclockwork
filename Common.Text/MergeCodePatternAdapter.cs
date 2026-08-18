using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoPro.Common.Text
{
	// Token: 0x02000002 RID: 2
	public static class MergeCodePatternAdapter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string ReplaceCodes(this string pattern, IDictionary<string, string> codeValues)
		{
			StringBuilder stringBuilder = new StringBuilder(pattern);
			foreach (KeyValuePair<string, string> keyValuePair in codeValues)
			{
				stringBuilder = stringBuilder.Replace(string.Format("{{{0}}}", keyValuePair.Key), keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}
	}
}
