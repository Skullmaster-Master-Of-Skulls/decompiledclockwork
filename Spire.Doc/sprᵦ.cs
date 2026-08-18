using System;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;

// Token: 0x020001F9 RID: 505
internal class spr\u1D66
{
	// Token: 0x06001619 RID: 5657 RVA: 0x001656CC File Offset: 0x001646CC
	public spr\u1D66(string A_0, bool A_1, bool A_2, sprᠭ A_3, string[] A_4, string[] A_5)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = A_2;
		this.ᜃ = A_3;
		this.ᜄ = A_4;
		this.ᜅ = A_5;
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x0016570C File Offset: 0x0016470C
	public string ᜃ()
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
		return this.ᜀ;
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x00165750 File Offset: 0x00164750
	public sprᠭ ᜀ()
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
		return this.ᜃ;
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x00165794 File Offset: 0x00164794
	public bool ᜁ()
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
		return this.ᜂ;
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x001657D8 File Offset: 0x001647D8
	public bool ᜂ()
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
		return this.ᜁ;
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x0016581C File Offset: 0x0016481C
	public sprᜏ ᜀ(string A_0)
	{
		int a_ = 1;
		if (this.ᜆ == null)
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
				if (true)
				{
				}
				break;
			}
			throw new InvalidOperationException(ClipboardData.b("㍦Ũ๪䵬๮հݲݴṶ᭸๺ॼ᩾ꆀﶈꮊ뎒ﾖﲘ뮚쒠캢삤즦\udda8讪즬쪮튰\udfb2풴얶\ud8b8쾺풼킾꿀귄ꛆ뫈ꏌꃎꗐ럔닖볘뗚﷜뛞迠諢釤軦裨蟪蓬鳮铰韲\udbf4", a_));
		}
		sprᜏ result;
		this.ᜆ.TryGetValue(A_0.ToUpperInvariant(), out result);
		return result;
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x00165894 File Offset: 0x00164894
	public void ᜀ(Dictionary<string, sprᜏ> A_0)
	{
		int a_ = 11;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_171;
			case 1:
				goto IL_158;
			case 3:
				goto IL_45;
			case 4:
			{
				if (this.ᜆ == null)
				{
					num = 1;
					continue;
				}
				Dictionary<string, sprᜏ>.ValueCollection.Enumerator enumerator = A_0.Values.GetEnumerator();
				num = 0;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 4;
			}
		}
		IL_45:
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ᵰᩲٴͶ", a_));
		IL_11E:
		this.ᜆ = A_0;
		return;
		IL_158:
		goto IL_11E;
		IL_171:
		try
		{
			sprᜏ sprᜏ;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_A4:
				Dictionary<string, sprᜏ>.ValueCollection.Enumerator enumerator;
				if (!enumerator.MoveNext())
				{
					num = 0;
				}
				else
				{
					sprᜏ = enumerator.Current;
					num = 6;
				}
				break;
			}
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 2:
					this.ᜆ.Add(sprᜏ.ᜂ(), sprᜏ);
					num = 4;
					continue;
				case 3:
					goto IL_10E;
				case 5:
					goto IL_A4;
				case 6:
					if (!this.ᜆ.ContainsKey(sprᜏ.ᜂ()))
					{
						num = 2;
						continue;
					}
					break;
				}
				IL_9C:
				num = 5;
				continue;
				goto IL_9C;
			}
			IL_10E:
			return;
		}
		finally
		{
			Dictionary<string, sprᜏ>.ValueCollection.Enumerator enumerator;
			((IDisposable)enumerator).Dispose();
		}
		goto IL_11E;
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x00165A28 File Offset: 0x00164A28
	public bool ᜀ(string A_0, spr\u2057 A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			bool result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜄ != null)
					{
						num = 6;
						continue;
					}
					goto IL_140;
				case 1:
					goto IL_C5;
				case 3:
					goto IL_14E;
				case 4:
					goto IL_C5;
				case 5:
					goto IL_19E;
				case 6:
				{
					string[] array = this.ᜄ;
					int num2 = 0;
					num = 4;
					continue;
				}
				case 7:
					goto IL_8C;
				case 8:
				{
					string a;
					if (string.Equals(a, A_0, StringComparison.OrdinalIgnoreCase))
					{
						num = 16;
						continue;
					}
					int num2;
					num2++;
					num = 1;
					continue;
				}
				case 9:
				{
					int num3;
					string[] array2;
					if (num3 >= array2.Length)
					{
						num = 5;
						continue;
					}
					string a2 = array2[num3];
					num = 14;
					continue;
				}
				case 10:
					goto IL_14E;
				case 11:
					result = false;
					if (true)
					{
					}
					num = 13;
					continue;
				case 12:
					goto IL_E5;
				case 13:
					goto IL_C0;
				case 14:
				{
					string a2;
					if (string.Equals(a2, A_0, StringComparison.OrdinalIgnoreCase))
					{
						num = 11;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E5;
					default:
					{
						if (false)
						{
						}
						int num3;
						num3++;
						num = 3;
						continue;
					}
					}
					break;
				}
				case 15:
				{
					string[] array;
					int num2;
					if (num2 >= array.Length)
					{
						num = 12;
						continue;
					}
					string a = array[num2];
					num = 8;
					continue;
				}
				case 16:
					result = true;
					num = 7;
					continue;
				case 17:
				{
					string[] array2 = this.ᜅ;
					int num3 = 0;
					num = 10;
					continue;
				}
				}
				if (this.ᜅ != null)
				{
					num = 17;
					continue;
				}
				goto IL_19E;
				IL_C5:
				num = 15;
				continue;
				IL_14E:
				num = 9;
				continue;
				IL_19E:
				num = 0;
			}
			IL_8C:
			IL_C0:
			return result;
			IL_E5:
			IL_140:
			return this.ᜃ.ᜀ(A_0, A_1);
		}
		}
	}

	// Token: 0x04001A07 RID: 6663
	private string ᜀ;

	// Token: 0x04001A08 RID: 6664
	private bool ᜁ;

	// Token: 0x04001A09 RID: 6665
	private bool ᜂ;

	// Token: 0x04001A0A RID: 6666
	private sprᠭ ᜃ;

	// Token: 0x04001A0B RID: 6667
	private string[] ᜄ;

	// Token: 0x04001A0C RID: 6668
	private string[] ᜅ;

	// Token: 0x04001A0D RID: 6669
	private Dictionary<string, sprᜏ> ᜆ;
}
