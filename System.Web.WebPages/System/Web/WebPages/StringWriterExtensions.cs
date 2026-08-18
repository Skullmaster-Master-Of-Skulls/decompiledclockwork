using System;
using System.IO;
using System.Text;

namespace System.Web.WebPages
{
	// Token: 0x02000051 RID: 81
	internal static class StringWriterExtensions
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00008054 File Offset: 0x00006254
		public static void CopyTo(this StringWriter input, TextWriter output)
		{
			StringBuilder stringBuilder = input.GetStringBuilder();
			int i = stringBuilder.Length;
			int num = Math.Min(stringBuilder.Length, 1024);
			char[] array = new char[num];
			int num2 = 0;
			while (i > 0)
			{
				int num3 = Math.Min(num, i);
				stringBuilder.CopyTo(num2, array, 0, num3);
				output.Write(array, 0, num3);
				num2 += num3;
				i -= num3;
			}
		}

		// Token: 0x040000A1 RID: 161
		public const int BufferSize = 1024;
	}
}
