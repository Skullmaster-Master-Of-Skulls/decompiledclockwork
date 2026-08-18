using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000420 RID: 1056
internal class sprᝠ : IColorScale
{
	// Token: 0x06003F13 RID: 16147 RVA: 0x00239A98 File Offset: 0x00238A98
	public IList<IColorConditionValue> ᜀ()
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
		return this.ᜂ;
	}

	// Token: 0x06003F14 RID: 16148 RVA: 0x00239ADC File Offset: 0x00238ADC
	public void ᜁ(int A_0)
	{
		int a_ = 16;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_73;
				default:
					goto IL_95;
				}
				break;
			case 1:
				num = 2;
				continue;
			case 2:
				goto IL_73;
			}
			if (A_0 >= 2)
			{
				if (true)
				{
				}
				num = 1;
				continue;
			}
			break;
			IL_73:
			if (A_0 <= 3)
			{
				goto IL_9D;
			}
			num = 0;
		}
		IL_49:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("╅❇㽉≋㩍", a_), RecordTableEnumerator.b("ॅ♇♉㕋湍扏牑㭓⑕硗楙籛㵝şౡ䑣ѥ൧䩩ᥫᵭᕯᙱ味᝵୷婹ύᅽꚅ曆벑", a_));
		IL_95:
		if (false)
		{
		}
		goto IL_49;
		IL_9D:
		this.ᜀ(A_0);
	}

	// Token: 0x06003F15 RID: 16149 RVA: 0x00239B90 File Offset: 0x00238B90
	public sprᝠ()
	{
		this.ᜁ(2);
	}

	// Token: 0x06003F16 RID: 16150 RVA: 0x00239BB8 File Offset: 0x00238BB8
	private void ᜀ(int A_0)
	{
		int a_ = 7;
		Color[] array2;
		int num2;
		for (;;)
		{
			this.ᜂ.Clear();
			int num = 6;
			for (;;)
			{
				Color[] array;
				switch (num)
				{
				case 0:
					array = sprᝠ.ᜀ;
					goto IL_A9;
				case 1:
					this.ᜂ.Add(new spr\u24B3(ConditionValueType.Percentile, RecordTableEnumerator.b("࠼༾", a_), array2[num2++]));
					num = 3;
					continue;
				case 2:
					if (A_0 == 3)
					{
						num = 1;
						continue;
					}
					goto IL_133;
				case 3:
					goto IL_A4;
				case 4:
					num = 5;
					continue;
				case 5:
					array = sprᝠ.ᜁ;
					goto IL_A9;
				case 6:
					if (A_0 != 2)
					{
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DB;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				break;
				IL_DB:
				num = 2;
				continue;
				IL_A9:
				array2 = array;
				num2 = 0;
				this.ᜂ.Add(new spr\u24B3(ConditionValueType.LowestValue, RecordTableEnumerator.b("഼", a_), array2[num2++]));
				goto IL_DB;
			}
		}
		IL_A4:
		IL_133:
		this.ᜂ.Add(new spr\u24B3(ConditionValueType.HighestValue, RecordTableEnumerator.b("഼", a_), array2[num2++]));
	}

	// Token: 0x06003F17 RID: 16151 RVA: 0x00239D28 File Offset: 0x00238D28
	// Note: this type is marked as 'beforefieldinit'.
	static sprᝠ()
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
		sprᝠ.ᜀ = new Color[]
		{
			Color.FromArgb(255, 255, 113, 40),
			Color.FromArgb(255, 255, 239, 156)
		};
		sprᝠ.ᜁ = new Color[]
		{
			Color.FromArgb(255, 248, 105, 107),
			Color.FromArgb(255, 255, 235, 132),
			Color.FromArgb(255, 99, 190, 123)
		};
	}

	// Token: 0x04001CB2 RID: 7346
	private static readonly Color[] ᜀ;

	// Token: 0x04001CB3 RID: 7347
	private static readonly Color[] ᜁ;

	// Token: 0x04001CB4 RID: 7348
	private IList<IColorConditionValue> ᜂ = new List<IColorConditionValue>(3);
}
