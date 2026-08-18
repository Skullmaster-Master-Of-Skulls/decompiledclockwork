using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000245 RID: 581
internal abstract class spr\u22EA : ICloneable
{
	// Token: 0x06002335 RID: 9013 RVA: 0x001462C0 File Offset: 0x001452C0
	public spr\u22EA()
	{
	}

	// Token: 0x06002336 RID: 9014 RVA: 0x001462D4 File Offset: 0x001452D4
	public virtual spr\u22EA ᜀ(string A_0)
	{
		int num = 3;
		Match match;
		for (;;)
		{
			bool flag;
			bool flag2;
			switch (num)
			{
			case 0:
				flag = (match.Length == A_0.Length);
				goto IL_B0;
			case 1:
				goto IL_D4;
			case 2:
				if (match.Success)
				{
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_43;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 4:
				num = 0;
				continue;
			case 5:
				if (!flag2)
				{
					num = 1;
					continue;
				}
				goto IL_109;
			case 6:
				goto IL_43;
			case 7:
				flag = false;
				goto IL_B0;
			case 8:
				goto IL_AE;
			case 9:
				if (A_0.Length == 0)
				{
					num = 8;
					continue;
				}
				match = this.ᜄ().Match(A_0);
				num = 2;
				continue;
			}
			if (A_0 != null)
			{
				num = 6;
				continue;
			}
			break;
			IL_43:
			num = 9;
			continue;
			IL_B0:
			flag2 = flag;
			num = 5;
		}
		IL_8C:
		return null;
		IL_AE:
		goto IL_8C;
		IL_D4:
		return null;
		IL_109:
		return this.ᜀ(match);
	}

	// Token: 0x06002337 RID: 9015 RVA: 0x001463F4 File Offset: 0x001453F4
	protected virtual spr\u22EA ᜀ(Match A_0)
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

	// Token: 0x06002338 RID: 9016 RVA: 0x00146434 File Offset: 0x00145434
	public virtual void ᜀ(IWorksheet A_0, Point A_1, ref int A_2, ref int A_3, IList<long> A_4, spr\u2064 A_5)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002339 RID: 9017 RVA: 0x00146474 File Offset: 0x00145474
	public virtual void ᜀ(spr\u2064 A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x0600233A RID: 9018 RVA: 0x001464B4 File Offset: 0x001454B4
	public virtual object ᜅ()
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
		return base.MemberwiseClone();
	}

	// Token: 0x0600233B RID: 9019 RVA: 0x001464F8 File Offset: 0x001454F8
	protected virtual Regex ᜄ()
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

	// Token: 0x0600233C RID: 9020 RVA: 0x00146538 File Offset: 0x00145538
	public virtual int ᜀ()
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
		return int.MaxValue;
	}

	// Token: 0x0600233D RID: 9021 RVA: 0x00146578 File Offset: 0x00145578
	public virtual bool ᜂ()
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
		return false;
	}

	// Token: 0x0600233E RID: 9022 RVA: 0x001465B4 File Offset: 0x001455B4
	public virtual bool ᜁ()
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
		return false;
	}

	// Token: 0x0600233F RID: 9023 RVA: 0x001465F0 File Offset: 0x001455F0
	public virtual bool ᜃ()
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
		return false;
	}

	// Token: 0x06002340 RID: 9024 RVA: 0x0014662C File Offset: 0x0014562C
	protected static void ᜁ(IList<long> A_0, int A_1, int A_2)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				int count;
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 >= A_2)
					{
						num = 3;
						continue;
					}
					goto IL_147;
				}
				case 1:
					goto IL_1A7;
				case 2:
					if (A_1 > count - 1)
					{
						num = 1;
						continue;
					}
					goto IL_F6;
				case 3:
				{
					int num2;
					num2++;
					long num3;
					int a_2 = sprṔ.ᜀ(num3);
					num3 = sprṔ.ᜀ(a_2, num2);
					A_0[A_1] = num3;
					num = 8;
					continue;
				}
				case 4:
					num = 2;
					continue;
				case 5:
					IL_162:
					goto IL_F6;
				case 6:
					return;
				case 7:
					if (true)
					{
					}
					break;
				case 8:
					goto IL_147;
				case 9:
					goto IL_6D;
				case 10:
				{
					if (A_1 >= count)
					{
						num = 6;
						continue;
					}
					long num3 = A_0[A_1];
					int num2 = sprṔ.ᜁ(num3);
					num = 0;
					continue;
				}
				case 11:
					if (A_1 >= 0)
					{
						num = 4;
						continue;
					}
					goto IL_164;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				count = A_0.Count;
				num = 11;
				continue;
				IL_F6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_162;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				IL_147:
				A_1++;
				num = 5;
			}
			IL_6D:
			throw new ArgumentNullException(RecordTableEnumerator.b("弽㈿ぁ݃⍅⑇♉㽋", a_));
			IL_164:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("圽", a_), RecordTableEnumerator.b("栽ℿ⹁ㅃ⍅桇⥉ⵋ⁍㹏㵑⁓癕㩗㽙籛㉝՟ᅡᝣ䙥ᱧɩ൫m偯䉱味᝵ᙷṹ屻᥽겋揄望뚕ﮗ풟財", a_));
			IL_1A7:
			goto IL_164;
		}
		}
	}

	// Token: 0x06002341 RID: 9025 RVA: 0x001467E4 File Offset: 0x001457E4
	protected static void ᜀ(IList<long> A_0, int A_1, int A_2)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				int count;
				long num2;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_19B;
				case 1:
					if (A_1 >= 0)
					{
						num = 2;
						continue;
					}
					goto IL_135;
				case 2:
					num = 11;
					continue;
				case 3:
					goto IL_122;
				case 4:
					return;
				case 6:
					goto IL_65;
				case 7:
					goto IL_C6;
				case 8:
					goto IL_ED;
				case 9:
					if (A_1 >= count)
					{
						num = 4;
						continue;
					}
					num2 = A_0[A_1];
					num3 = sprṔ.ᜀ(num2);
					if (true)
					{
					}
					num = 10;
					continue;
				case 10:
					if (num3 >= A_2)
					{
						num = 7;
						continue;
					}
					goto IL_122;
				case 11:
					if (A_1 <= count - 1)
					{
						goto IL_ED;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C6;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				count = A_0.Count;
				num = 1;
				continue;
				IL_C6:
				num3++;
				int a_2 = sprṔ.ᜁ(num2);
				num2 = sprṔ.ᜀ(num3, a_2);
				A_0[A_1] = num2;
				num = 3;
				continue;
				IL_ED:
				num = 9;
				continue;
				IL_122:
				A_1++;
				num = 8;
			}
			IL_65:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽̿❁⡃⩅㭇", a_));
			IL_135:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("匹", a_), RecordTableEnumerator.b("氹崻刽㔿❁摃╅⥇⑉≋⅍⑏牑㙓㍕硗㙙㥛ⵝ፟䉡ၣ๥१ѩ䱫幭偯፱ᩳት塷ᵹ๻᭽ꢇﺉﺏ늑秊낝", a_));
			IL_19B:
			goto IL_135;
		}
		}
	}

	// Token: 0x04001211 RID: 4625
	protected const char ᜀ = ':';
}
