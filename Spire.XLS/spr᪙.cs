using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200050C RID: 1292
[CLSCompliant(false)]
[sprᵴ(MsoRecords.msofbtClientData)]
internal class spr᪙ : spr\u1D3B
{
	// Token: 0x06004E89 RID: 20105 RVA: 0x002FB4D8 File Offset: 0x002FA4D8
	public spr\u2003 ᜁ()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(this.ᜀ[0] is spr\u2003))
				{
					goto IL_A0;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9E;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 1:
				num = 0;
				continue;
			case 2:
				goto IL_9E;
			}
			if (true)
			{
			}
			if (this.ᜀ.Count <= 0)
			{
				goto IL_A0;
			}
			num = 1;
		}
		IL_9E:
		return (spr\u2003)this.ᜀ[0];
		IL_A0:
		return null;
	}

	// Token: 0x06004E8A RID: 20106 RVA: 0x002FB588 File Offset: 0x002FA588
	public new void ᜀ(spr\u2003 A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x06004E8B RID: 20107 RVA: 0x002FB5C8 File Offset: 0x002FA5C8
	public new BiffRecordRaw[] ᜀ()
	{
		if (this.ᜀ == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			if (true)
			{
			}
			return null;
		}
		return this.ᜀ.ToArray();
	}

	// Token: 0x06004E8C RID: 20108 RVA: 0x002FB61C File Offset: 0x002FA61C
	public spr᪙(spr\u1D3B A_0)
	{
		this.ᜀ = new List<BiffRecordRaw>();
		base..ctor(A_0);
	}

	// Token: 0x06004E8D RID: 20109 RVA: 0x002FB63C File Offset: 0x002FA63C
	public spr᪙(spr\u1D3B A_0, byte[] A_1, int A_2)
	{
		this.ᜀ = new List<BiffRecordRaw>();
		base..ctor(A_0, A_1, A_2);
	}

	// Token: 0x06004E8E RID: 20110 RVA: 0x002FB660 File Offset: 0x002FA660
	public spr᪙(spr\u1D3B A_0, byte[] A_1, int A_2, spr\u24C9 A_3)
	{
		int a_ = 2;
		this.ᜀ = new List<BiffRecordRaw>();
		base..ctor(A_0, A_1, A_2, A_3);
		BiffRecordRaw[] array = A_3();
		if (array == null)
		{
			throw new ArgumentException(RecordTableEnumerator.b("礷帹堻圽㐿⭁⭃⡅⥇♉汋⩍ㅏ♑㕓癕㭗㭙㉛祝ᑟ䉡٣ͥ䡧ѩᥫɭᱯ", a_));
		}
		this.ᜀ.Clear();
		this.ᜀ.AddRange(array);
		for (int i = this.ᜀ.Count - 1; i >= 0; i--)
		{
			if (!(this.ᜀ[i] is spr\u2114))
			{
				return;
			}
			this.ᜀ.RemoveAt(i);
		}
	}

	// Token: 0x06004E8F RID: 20111 RVA: 0x002FB704 File Offset: 0x002FA704
	public override void ᜀ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3)
	{
		for (;;)
		{
			IL_1C:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8F:
				if (true)
				{
				}
				num = 4;
				break;
			default:
				if (false)
				{
				}
				this.m_iLength = 0;
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_81;
				case 1:
					return;
				case 2:
					if (A_2 != null)
					{
						num = 3;
						continue;
					}
					return;
				case 3:
					num = 0;
					continue;
				case 4:
					A_2.Add(this.m_iLength + A_1);
					A_3.Add(this.ᜀ);
					num = 1;
					continue;
				}
				goto IL_1C;
			}
			IL_81:
			if (A_3 != null)
			{
				goto IL_8F;
			}
			break;
		}
	}

	// Token: 0x06004E90 RID: 20112 RVA: 0x002FB7B8 File Offset: 0x002FA7B8
	public override void ᜀ(Stream A_0)
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
	}

	// Token: 0x06004E91 RID: 20113 RVA: 0x002FB7F4 File Offset: 0x002FA7F4
	protected override object ᜅ()
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
		spr᪙ spr᪙ = (spr᪙)base.ᜅ();
		spr᪙.ᜀ = spr\u1CD3.ᜀ(this.ᜀ);
		return spr᪙;
	}

	// Token: 0x06004E92 RID: 20114 RVA: 0x002FB850 File Offset: 0x002FA850
	public override void ᜏ()
	{
		int a_ = 16;
		for (;;)
		{
			BiffRecordRaw[] array = base.\u1716()();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B6;
				case 1:
					if (array == null)
					{
						num = 6;
						continue;
					}
					goto IL_F8;
				case 2:
					return;
				case 3:
					goto IL_B6;
				case 4:
				{
					int num2;
					if (this.ᜀ[num2] is spr\u2114)
					{
						num = 5;
						continue;
					}
					return;
				}
				case 5:
				{
					if (true)
					{
					}
					int num2;
					this.ᜀ.RemoveAt(num2);
					num2--;
					num = 3;
					continue;
				}
				case 6:
					goto IL_53;
				case 7:
				{
					int num2;
					if (num2 < 0)
					{
						num = 2;
						continue;
					}
					num = 4;
					continue;
				}
				}
				break;
				IL_B6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_F8:
					this.ᜀ.Clear();
					this.ᜀ.AddRange(array);
					int num2 = this.ᜀ.Count - 1;
					num = 0;
					break;
				}
				default:
					if (false)
					{
					}
					num = 7;
					break;
				}
			}
		}
		IL_53:
		throw new ArgumentException(RecordTableEnumerator.b("݅ⱇ⹉╋㩍㥏㵑㩓㝕㑗穙㡛㽝ᑟ͡䑣ե१ѩ䭫ᩭ偯ၱᅳ噵ᙷཹၻች", a_));
	}

	// Token: 0x06004E93 RID: 20115 RVA: 0x002FB988 File Offset: 0x002FA988
	public new void ᜀ(BiffRecordRaw A_0)
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
		this.ᜀ.Add(A_0);
	}

	// Token: 0x06004E94 RID: 20116 RVA: 0x002FB9D0 File Offset: 0x002FA9D0
	public new void ᜀ(ICollection<BiffRecordRaw> A_0)
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
		this.ᜀ.AddRange(A_0);
	}

	// Token: 0x06004E95 RID: 20117 RVA: 0x002FBA18 File Offset: 0x002FAA18
	public new void ᜀ(IList A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = A_0.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						this.ᜀ.Add(A_0[num] as BiffRecordRaw);
						num++;
						num2 = 1;
						continue;
					}
					break;
				case 1:
					goto IL_2B;
				case 2:
					goto IL_2B;
				case 3:
					return;
				}
				break;
				IL_2B:
				num2 = 0;
			}
		}
	}

	// Token: 0x04002380 RID: 9088
	private new List<BiffRecordRaw> ᜀ;
}
