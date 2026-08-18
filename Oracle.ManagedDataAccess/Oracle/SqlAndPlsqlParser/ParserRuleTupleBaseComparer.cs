using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200027D RID: 637
	internal class ParserRuleTupleBaseComparer : IComparer<ParserRuleTuple>
	{
		// Token: 0x0600190C RID: 6412 RVA: 0x00107DFC File Offset: 0x00105FFC
		protected ParserRuleTupleBaseComparer()
		{
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00107E04 File Offset: 0x00106004
		public int Compare(ParserRuleTuple x, ParserRuleTuple y)
		{
			if (x.ParserGrammar != y.ParserGrammar)
			{
				throw new ParserException(ParserExceptionType.Grammar, ParserExceptionError.DifferentGrammarsTuples);
			}
			int num = x.m_vBaseHead - y.m_vBaseHead;
			if (num == 0)
			{
				int[] vBaseRhs = x.m_vBaseRhs;
				int[] vBaseRhs2 = y.m_vBaseRhs;
				if (vBaseRhs == null)
				{
					if (vBaseRhs2 != null)
					{
						num = -1;
					}
				}
				else if (vBaseRhs2 == null)
				{
					num = 1;
				}
				else
				{
					int num2 = vBaseRhs.Length;
					num = num2 - vBaseRhs2.Length;
					int num3 = 0;
					while (num == 0 && num3 < num2)
					{
						num = vBaseRhs[num3] - vBaseRhs2[num3];
						num3++;
					}
				}
			}
			return num;
		}

		// Token: 0x04001B74 RID: 7028
		public static ParserRuleTupleBaseComparer s_vInstance = new ParserRuleTupleBaseComparer();
	}
}
