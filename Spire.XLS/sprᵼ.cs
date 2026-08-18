using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.TemplateMarkers;

// Token: 0x02000522 RID: 1314
[sprᦱ]
internal class sprᵼ : spr\u22EA
{
	// Token: 0x06005064 RID: 20580 RVA: 0x00327034 File Offset: 0x00326034
	static sprᵼ()
	{
		int a_ = 8;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᵼ.ᜀ = new SortedList<string, DataMarkerDirection>(2);
		sprᵼ.ᜀ.Add(RecordTableEnumerator.b("䠽┿ぁぃ⽅⭇⭉⁋", a_), DataMarkerDirection.Vertical);
		sprᵼ.ᜀ.Add(RecordTableEnumerator.b("嘽⼿ぁⵃ㱅❇⑉㡋⽍㱏", a_), DataMarkerDirection.Horizontal);
	}

	// Token: 0x06005066 RID: 20582 RVA: 0x003270CC File Offset: 0x003260CC
	public override spr\u22EA ᜀ(string A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B2;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 3:
			{
				sprᵼ sprᵼ = (sprᵼ)this.ᜅ();
				DataMarkerDirection dataMarkerDirection;
				sprᵼ.ᜁ = dataMarkerDirection;
				num = 5;
				continue;
			}
			case 4:
			{
				if (A_0.Length == 0)
				{
					num = 0;
					continue;
				}
				sprᵼ sprᵼ = null;
				num = 6;
				continue;
			}
			case 5:
			{
				sprᵼ sprᵼ;
				return sprᵼ;
			}
			case 6:
			{
				DataMarkerDirection dataMarkerDirection;
				if (sprᵼ.ᜀ.TryGetValue(A_0.ToLower(), out dataMarkerDirection))
				{
					num = 3;
					continue;
				}
				sprᵼ sprᵼ;
				return sprᵼ;
			}
			}
			if (A_0 == null)
			{
				break;
			}
			num = 1;
		}
		IL_92:
		return null;
		IL_B2:
		goto IL_92;
	}

	// Token: 0x06005067 RID: 20583 RVA: 0x003271B0 File Offset: 0x003261B0
	public override void ᜀ(spr\u2064 A_0)
	{
		int a_ = 1;
		if (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				A_0.ᜀ(this.ᜁ);
				return;
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("堶䤸伺吼倾⽀あ", a_));
	}

	// Token: 0x06005068 RID: 20584 RVA: 0x0032721C File Offset: 0x0032621C
	public override bool ᜂ()
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
		return true;
	}

	// Token: 0x0400240F RID: 9231
	private new static readonly SortedList<string, DataMarkerDirection> ᜀ;

	// Token: 0x04002410 RID: 9232
	private new DataMarkerDirection ᜁ;
}
