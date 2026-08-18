using System;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003A9 RID: 937
internal abstract class sprᭅ : sprἏ
{
	// Token: 0x060038DD RID: 14557 RVA: 0x001FB1CC File Offset: 0x001FA1CC
	public sprᭅ()
	{
	}

	// Token: 0x060038DE RID: 14558 RVA: 0x001FB1E0 File Offset: 0x001FA1E0
	public override int ᜀ(string A_0, int A_1)
	{
		int a_ = 3;
		int num = 1;
		int num2;
		int num3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0[A_1] != '[')
				{
					num = 11;
					continue;
				}
				A_1++;
				num2 = A_0.IndexOf(']', A_1);
				num = 5;
				continue;
			case 2:
				if (A_1 >= 0)
				{
					num = 6;
					continue;
				}
				goto IL_5B;
			case 3:
				goto IL_54;
			case 4:
				goto IL_B2;
			case 5:
				if (num2 < A_1)
				{
					num = 10;
					continue;
				}
				goto IL_1A1;
			case 6:
				num = 8;
				continue;
			case 7:
			{
				if (true)
				{
				}
				int length;
				if (length == 0)
				{
					num = 4;
					continue;
				}
				num = 2;
				continue;
			}
			case 8:
			{
				int length;
				if (A_1 > length - 1)
				{
					num = 9;
					continue;
				}
				num3 = A_1;
				num = 0;
				continue;
			}
			case 9:
				goto IL_19C;
			case 10:
				return num3;
			case 11:
				return A_1;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				int length = A_0.Length;
				num = 7;
			}
		}
		for (;;)
		{
			IL_54:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_FB;
			}
		}
		IL_FB:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("弸吺似刾⁀㝂", a_));
		IL_5B:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倸唺夼娾㥀", a_), RecordTableEnumerator.b("瀸唺夼娾㥀捂⥄≆㩈㡊浌㭎㥐㉒㭔睖楘筚㉜ⵞ䅠ѢᝤɦࡨὪ࡬ᵮ兰ݲᵴᙶ᝸孺ᅼ᩾ꞈ", a_));
		IL_B2:
		throw new ArgumentException(RecordTableEnumerator.b("樸伺似嘾⽀⑂敄⑆⡈╊⍌⁎═獒㝔㉖祘㹚ぜ⽞ᕠᩢ䭤", a_), RecordTableEnumerator.b("弸吺似刾⁀㝂", a_));
		IL_19C:
		goto IL_5B;
		IL_1A1:
		return this.ᜀ(A_0, num3, A_1, num2);
	}

	// Token: 0x060038DF RID: 14559
	public new abstract int ᜀ(string A_0, int A_1, int A_2, int A_3);

	// Token: 0x04001901 RID: 6401
	private new const char ᜀ = '[';

	// Token: 0x04001902 RID: 6402
	private new const char ᜁ = ']';
}
