using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008E9 RID: 2281
	internal sealed class SetOfValueComparer : IComparer<ReadOnlyMemory<byte>>
	{
		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060052D6 RID: 21206 RVA: 0x0012ABB6 File Offset: 0x00129BB6
		internal static SetOfValueComparer Instance
		{
			get
			{
				return SetOfValueComparer._instance;
			}
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x0012ABBD File Offset: 0x00129BBD
		public int Compare(ReadOnlyMemory<byte> x, ReadOnlyMemory<byte> y)
		{
			return SetOfValueComparer.Compare(x.Span, y.Span);
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x0012ABD4 File Offset: 0x00129BD4
		internal static int Compare(ReadOnlySpan<byte> x, ReadOnlySpan<byte> y)
		{
			int num = Math.Min(x.Length, y.Length);
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)x[i];
				byte b = y[i];
				int num3 = num2 - (int)b;
				if (num3 != 0)
				{
					return num3;
				}
			}
			return x.Length - y.Length;
		}

		// Token: 0x04002AB2 RID: 10930
		private static SetOfValueComparer _instance = new SetOfValueComparer();
	}
}
