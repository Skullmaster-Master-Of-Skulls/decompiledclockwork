using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000454 RID: 1108
internal class sprឥ : XlsObject
{
	// Token: 0x060042BF RID: 17087 RVA: 0x00255F3C File Offset: 0x00254F3C
	public sprឥ(spr\u2158 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜀ.Add(new sprᴔ());
		this.ᜀ.Add(new spr\u21F5());
		this.ᜀ.Add(new spr\u1BA6());
		this.ᜀ.Add(new spr\u2381());
		this.ᜀ.Add(new spr\u20A9());
		this.ᜀ.Add(new sprᣌ());
		this.ᜀ.Add(new spr\u1AF4());
		this.ᜀ.Add(new sprᩆ());
		this.ᜀ.Add(new spr\u25F3());
		this.ᜀ.Add(new sprᡥ());
		this.ᜀ.Add(new sprẠ());
		this.ᜀ.Add(new spr\u173F());
		this.ᜀ.Add(new spr\u2309());
		this.ᜀ.Add(new sprᢶ());
		this.ᜀ.Add(new sprṒ());
		this.ᜀ.Add(new spr\u19EE());
		this.ᜀ.Add(new spr\u262F());
		this.ᜀ.Add(new spr\u243D());
		this.ᜀ.Add(new sprᝄ());
		this.ᜀ.Add(new sprẪ());
		this.ᜀ.Add(new spr᱕());
		this.ᜀ.Add(new spr\u1DD8());
		this.ᜀ.Add(new sprỒ());
		this.ᜀ.Add(new spr\u2595());
		this.ᜀ.Add(new spr\u2478());
		this.ᜀ.Add(new spr\u259D());
		this.ᜀ.Add(new spr\u2599());
		this.ᜀ.Add(new sprᲸ());
		this.ᜀ.Add(new sprἄ());
	}

	// Token: 0x060042C0 RID: 17088 RVA: 0x0025612C File Offset: 0x0025512C
	public spr\u2575 ᜀ(string A_0)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 12;
			List<sprἏ> list;
			for (;;)
			{
				int length;
				switch (num)
				{
				case 0:
					goto IL_6D;
				case 1:
					goto IL_160;
				case 2:
					goto IL_F0;
				case 3:
				{
					if (length == 0)
					{
						num = 2;
						continue;
					}
					list = new List<sprἏ>();
					int num2 = 0;
					num = 7;
					continue;
				}
				case 4:
				{
					sprἏ sprἏ = (sprἏ)sprἏ.ᜇ();
					int num3;
					int num2 = num3;
					list.Add(sprἏ);
					num = 1;
					continue;
				}
				case 5:
					goto IL_F5;
				case 6:
				{
					int num2;
					int num3;
					if (num3 > num2)
					{
						num = 4;
						continue;
					}
					int num4;
					num4++;
					num = 5;
					continue;
				}
				case 7:
					goto IL_160;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_109;
					default:
						goto IL_192;
					}
					break;
				case 9:
					goto IL_160;
				case 10:
					goto IL_F5;
				case 11:
				{
					int num2;
					if (num2 >= length)
					{
						num = 8;
						continue;
					}
					int num4 = 0;
					int count = this.ᜀ.Count;
					num = 10;
					continue;
				}
				case 13:
				{
					int num4;
					int count;
					if (num4 >= count)
					{
						goto IL_109;
					}
					sprἏ sprἏ = this.ᜀ[num4];
					int num2;
					int num3 = sprἏ.ᜀ(A_0, num2);
					if (true)
					{
					}
					num = 6;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				length = A_0.Length;
				num = 3;
				continue;
				IL_F5:
				num = 13;
				continue;
				IL_109:
				num = 9;
				continue;
				IL_160:
				num = 11;
			}
			IL_6D:
			throw new ArgumentNullException(RecordTableEnumerator.b("弸吺似刾⁀㝂", a_), RecordTableEnumerator.b("樸伺似嘾⽀⑂敄㝆⡈㥊㹌⩎煐㕒㑔㹖㕘㹚㥜煞", a_));
			IL_F0:
			throw new ArgumentException(RecordTableEnumerator.b("樸伺似嘾⽀⑂敄⑆⡈╊⍌⁎═獒㝔㉖祘㹚ぜ⽞ᕠᩢ䭤", a_), RecordTableEnumerator.b("弸吺似刾⁀㝂", a_));
			IL_192:
			if (false)
			{
			}
			return new spr\u2575((spr\u2158)base.ReservedHandle, this, list);
		}
		}
	}

	// Token: 0x04001D93 RID: 7571
	private List<sprἏ> ᜀ = new List<sprἏ>();
}
