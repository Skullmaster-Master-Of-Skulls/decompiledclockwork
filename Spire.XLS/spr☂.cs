using System;
using System.Diagnostics;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004B3 RID: 1203
internal class spr\u2602 : IDisposable
{
	// Token: 0x06004A71 RID: 19057 RVA: 0x002D1FB0 File Offset: 0x002D0FB0
	[DebuggerStepThrough]
	internal spr\u1DF5 ᜂ()
	{
		int a_ = 5;
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
			if (!this.ᜁ)
			{
				return this.ᜀ;
			}
			break;
		}
		throw new ObjectDisposedException(RecordTableEnumerator.b("砺刼刾ㅀⱂ⭄≆❈㽊", a_), RecordTableEnumerator.b("漺唼娾慀⁂⩄⩆㥈⑊⍌⩎㽐❒畔㽖㡘⡚絜㵞Ѡ٢୤䝦൨ɪṬὮṰrၴ፶坸", a_));
	}

	// Token: 0x06004A72 RID: 19058 RVA: 0x002D2028 File Offset: 0x002D1028
	public bool ᜀ()
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

	// Token: 0x06004A73 RID: 19059 RVA: 0x002D206C File Offset: 0x002D106C
	public void ᜀ(bool A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06004A74 RID: 19060 RVA: 0x002D20B0 File Offset: 0x002D10B0
	public spr\u2602()
	{
		this.ᜀ = new spr\u2158();
	}

	// Token: 0x06004A75 RID: 19061 RVA: 0x002D20D8 File Offset: 0x002D10D8
	protected virtual void ᜃ()
	{
		try
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜁ();
		}
		finally
		{
			base.Finalize();
		}
		if (true)
		{
		}
	}

	// Token: 0x06004A76 RID: 19062 RVA: 0x002D2134 File Offset: 0x002D1134
	public void ᜁ()
	{
		int num = 5;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
			{
				if (false)
				{
				}
				int num2;
				IWorkbooks workbooks;
				switch (num)
				{
				case 0:
					goto IL_B6;
				case 1:
					if (true)
					{
					}
					goto IL_B6;
				case 2:
					if (num2 < 0)
					{
						num = 4;
						continue;
					}
					(workbooks[num2] as XlsWorkbook).\u1716();
					workbooks[num2].Close();
					num2--;
					num = 0;
					continue;
				case 3:
					return;
				case 4:
					goto IL_D0;
				}
				if (this.ᜁ)
				{
					num = 3;
					break;
				}
				workbooks = this.ᜀ.ᜥ();
				num2 = workbooks.Count - 1;
				num = 1;
				break;
				IL_B6:
				num = 2;
				break;
			}
			}
		}
		return;
		IL_D0:
		this.ᜀ = null;
		this.ᜁ = true;
		GC.SuppressFinalize(this);
	}

	// Token: 0x040021C0 RID: 8640
	private spr\u17FF ᜀ;

	// Token: 0x040021C1 RID: 8641
	private bool ᜁ;

	// Token: 0x040021C2 RID: 8642
	private bool ᜂ = true;
}
