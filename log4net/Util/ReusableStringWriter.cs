using System;
using System.IO;
using System.Text;

namespace log4net.Util
{
	// Token: 0x02000115 RID: 277
	public class ReusableStringWriter : StringWriter
	{
		// Token: 0x06000816 RID: 2070 RVA: 0x00019025 File Offset: 0x00017225
		public ReusableStringWriter(IFormatProvider formatProvider) : base(formatProvider)
		{
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001902E File Offset: 0x0001722E
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00019030 File Offset: 0x00017230
		public void Reset(int maxCapacity, int defaultSize)
		{
			StringBuilder stringBuilder = this.GetStringBuilder();
			stringBuilder.Length = 0;
			if (stringBuilder.Capacity > maxCapacity)
			{
				stringBuilder.Capacity = defaultSize;
			}
		}
	}
}
