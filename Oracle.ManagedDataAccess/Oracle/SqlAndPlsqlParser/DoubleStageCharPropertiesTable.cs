using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000262 RID: 610
	internal class DoubleStageCharPropertiesTable
	{
		// Token: 0x06001886 RID: 6278 RVA: 0x0010328C File Offset: 0x0010148C
		public DoubleStageCharPropertiesTable(string chars)
		{
			for (int i = 0; i < 256; i++)
			{
				this.m_vFirstTable[i] = -1;
			}
			List<BitVector32[]> list = new List<BitVector32[]>();
			foreach (char c in chars)
			{
				int num = (int)c;
				int num2 = num / 256;
				BitVector32[] array;
				if (this.m_vFirstTable[num2] == -1)
				{
					array = new BitVector32[8];
					for (int k = 0; k < 8; k++)
					{
						array[k] = new BitVector32(0);
					}
					this.m_vFirstTable[num2] = list.Count;
					list.Add(array);
				}
				else
				{
					array = list[num2];
				}
				num %= 256;
				array[num / 32][1 << num % 32] = true;
			}
			this.m_vSecondTables = list.ToArray();
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0010338C File Offset: 0x0010158C
		public bool Contains(char c)
		{
			int num = (int)(c / 'Ā');
			num = this.m_vFirstTable[num];
			if (num == -1)
			{
				return false;
			}
			int num2 = (int)(c % 'Ā');
			return this.m_vSecondTables[num][num2 / 32][1 << num2 % 32];
		}

		// Token: 0x04001AE5 RID: 6885
		private const int c_vFirstTableSize = 256;

		// Token: 0x04001AE6 RID: 6886
		private const int m_vSecondTableSize = 8;

		// Token: 0x04001AE7 RID: 6887
		private int[] m_vFirstTable = new int[256];

		// Token: 0x04001AE8 RID: 6888
		private BitVector32[][] m_vSecondTables;
	}
}
