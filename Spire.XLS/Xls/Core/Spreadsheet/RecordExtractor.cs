using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x020005FB RID: 1531
	public class RecordExtractor
	{
		// Token: 0x06005A0F RID: 23055 RVA: 0x00386898 File Offset: 0x00385898
		public RecordExtractor()
		{
			this.ᜀ = new Dictionary<int, BiffRecordRaw>();
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x003868B8 File Offset: 0x003858B8
		internal BiffRecordRaw ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
		{
			int a_ = 8;
			if (A_0 == null)
			{
				for (;;)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_36;
					}
				}
				IL_36:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("丽㈿ⵁ㉃⽅ⱇ⽉㹋", a_));
			}
			int a_2 = (int)A_0.ReadInt16(A_1);
			A_1 += 2;
			BiffRecordRaw biffRecordRaw = this.ᜀ(a_2);
			int num = (int)A_0.ReadInt16(A_1);
			biffRecordRaw.Length = num;
			A_1 += 2;
			biffRecordRaw.ParseStructure(A_0, A_1, num, A_2);
			return biffRecordRaw;
		}

		// Token: 0x06005A11 RID: 23057 RVA: 0x0038694C File Offset: 0x0038594C
		internal BiffRecordRaw ᜀ(int A_0)
		{
			int num = 0;
			BiffRecordRaw biffRecordRaw;
			for (;;)
			{
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						goto IL_70;
					}
					break;
				case 2:
					biffRecordRaw = spr\u175E.ᜀ(A_0);
					this.ᜀ.Add(A_0, biffRecordRaw);
					goto IL_52;
				}
				if (true)
				{
				}
				if (!this.ᜀ.TryGetValue(A_0, out biffRecordRaw))
				{
					num = 2;
					continue;
				}
				return biffRecordRaw;
				IL_52:
				num = 1;
			}
			IL_70:
			if (false)
			{
			}
			return biffRecordRaw;
		}

		// Token: 0x04002C4E RID: 11342
		private byte \u25D9\u00A7\u0082\u0098;

		// Token: 0x04002C4F RID: 11343
		private long \u2593\u00AC\u009E\u009A;

		// Token: 0x04002C50 RID: 11344
		private int[] \u25D9\u0097\u008C\u009F;

		// Token: 0x04002C51 RID: 11345
		private Dictionary<int, BiffRecordRaw> ᜀ;
	}
}
