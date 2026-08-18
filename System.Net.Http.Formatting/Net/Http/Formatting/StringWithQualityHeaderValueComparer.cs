using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200002E RID: 46
	internal class StringWithQualityHeaderValueComparer : IComparer<StringWithQualityHeaderValue>
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00006656 File Offset: 0x00004856
		private StringWithQualityHeaderValueComparer()
		{
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000665E File Offset: 0x0000485E
		public static StringWithQualityHeaderValueComparer QualityComparer
		{
			get
			{
				return StringWithQualityHeaderValueComparer._qualityComparer;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006668 File Offset: 0x00004868
		public int Compare(StringWithQualityHeaderValue stringWithQuality1, StringWithQualityHeaderValue stringWithQuality2)
		{
			double? quality = stringWithQuality1.Quality;
			double num = (quality != null) ? quality.GetValueOrDefault() : 1.0;
			double? quality2 = stringWithQuality2.Quality;
			double num2 = (quality2 != null) ? quality2.GetValueOrDefault() : 1.0;
			double num3 = num - num2;
			if (num3 < 0.0)
			{
				return -1;
			}
			if (num3 > 0.0)
			{
				return 1;
			}
			if (!string.Equals(stringWithQuality1.Value, stringWithQuality2.Value, StringComparison.OrdinalIgnoreCase))
			{
				if (string.Equals(stringWithQuality1.Value, "*", StringComparison.OrdinalIgnoreCase))
				{
					return -1;
				}
				if (string.Equals(stringWithQuality2.Value, "*", StringComparison.OrdinalIgnoreCase))
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x04000066 RID: 102
		private static readonly StringWithQualityHeaderValueComparer _qualityComparer = new StringWithQualityHeaderValueComparer();
	}
}
