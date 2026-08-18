using System;
using System.Collections.Generic;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200042A RID: 1066
[spr\u2400(FormulaToken.tParentheses, "(")]
internal class sprὪ : spr\u23FA
{
	// Token: 0x06004093 RID: 16531 RVA: 0x00243D44 File Offset: 0x00242D44
	static sprὪ()
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprὪ.ᜀ = new spr\u2400[]
		{
			new spr\u2400(FormulaToken.tParentheses, RecordTableEnumerator.b("湅", a_))
		};
	}

	// Token: 0x06004094 RID: 16532 RVA: 0x00243DB0 File Offset: 0x00242DB0
	public sprὪ()
	{
		int a_ = 12;
		base..ctor(RecordTableEnumerator.b("橁", a_));
		this.TokenCode = FormulaToken.tParentheses;
	}

	// Token: 0x06004095 RID: 16533 RVA: 0x00243DE4 File Offset: 0x00242DE4
	public sprὪ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004096 RID: 16534 RVA: 0x00243DFC File Offset: 0x00242DFC
	public sprὪ(string A_0)
	{
		int a_ = 4;
		base..ctor(RecordTableEnumerator.b("ሹ", a_));
		if (A_0 != RecordTableEnumerator.b("ሹ", a_))
		{
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤹䠻䰽ؿⵁ㙃⭅㵇♉ⵋ", a_));
		}
	}

	// Token: 0x06004097 RID: 16535 RVA: 0x00243E54 File Offset: 0x00242E54
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return RecordTableEnumerator.b("浄湆", a_);
	}

	// Token: 0x06004098 RID: 16536 RVA: 0x00243EA8 File Offset: 0x00242EA8
	public override void ᜀ(FormulaUtil A_0, Stack<object> A_1, bool A_2)
	{
		int a_ = 7;
		object obj;
		object obj2;
		for (;;)
		{
			obj = A_1.Pop();
			obj2 = (obj as sprᯡ);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5C;
				case 1:
					if (obj2 != null)
					{
						goto IL_3A;
					}
					obj2 = string.Empty;
					num = 0;
					continue;
				case 2:
					obj = A_1.Pop();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3A;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					goto IL_94;
				}
				break;
				IL_3A:
				num = 2;
			}
		}
		IL_5C:
		IL_94:
		string item = obj2.ToString() + RecordTableEnumerator.b("ᔼ", a_) + obj.ToString() + RecordTableEnumerator.b("ᐼ", a_);
		A_1.Push(item);
	}

	// Token: 0x06004099 RID: 16537 RVA: 0x00243F80 File Offset: 0x00242F80
	public override string[] ᜀ(string A_0, ref int A_1, FormulaUtil A_2)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		int num = FormulaUtil.FindCorrespondingBracket(A_0, A_1);
		A_1 = num + 1;
		string text = A_0.Substring(1, num - 1);
		return new string[]
		{
			text
		};
	}

	// Token: 0x0600409A RID: 16538 RVA: 0x00243FE0 File Offset: 0x00242FE0
	public override ParseFormulaOptions ᜀ(ParseFormulaOptions A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return A_0;
	}

	// Token: 0x0600409B RID: 16539 RVA: 0x0024401C File Offset: 0x0024301C
	protected override spr\u2400[] ᜀ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return sprὪ.ᜀ;
	}

	// Token: 0x04001CDD RID: 7389
	private new static readonly spr\u2400[] ᜀ;
}
