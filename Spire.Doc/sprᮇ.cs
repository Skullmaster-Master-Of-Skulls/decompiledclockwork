using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x02000200 RID: 512
internal class sprᮇ
{
	// Token: 0x06001662 RID: 5730 RVA: 0x0016A2E4 File Offset: 0x001692E4
	public int ᜅ()
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
		return this.ᜁ;
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x0016A328 File Offset: 0x00169328
	public void ᜀ(int A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x0016A36C File Offset: 0x0016936C
	public Guid ᜃ()
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

	// Token: 0x06001665 RID: 5733 RVA: 0x0016A3B0 File Offset: 0x001693B0
	public void ᜀ(Guid A_0)
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

	// Token: 0x06001666 RID: 5734 RVA: 0x0016A3F4 File Offset: 0x001693F4
	public int ᜁ()
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

	// Token: 0x06001667 RID: 5735 RVA: 0x0016A438 File Offset: 0x00169438
	public void ᜁ(int A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x0016A47C File Offset: 0x0016947C
	public int ᜂ()
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
		return this.ᜄ.Count;
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x0016A4C4 File Offset: 0x001694C4
	public List<spr\u1ADE> ᜄ()
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
		return this.ᜄ;
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x0016A508 File Offset: 0x00169508
	public sprᮇ(Guid A_0, int A_1)
	{
		this.ᜂ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x0016A534 File Offset: 0x00169534
	public void ᜂ(Stream A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte[] a_ = new byte[4];
				A_0.Position = (long)this.ᜁ;
				this.ᜃ = sprữ.ᜁ(A_0, a_);
				int num = sprữ.ᜁ(A_0, a_);
				List<int> list = new List<int>();
				int num2 = 0;
				int num3 = 14;
				for (;;)
				{
					int num5;
					switch (num3)
					{
					case 0:
					{
						if (true)
						{
						}
						Dictionary<int, string> dictionary;
						if (dictionary != null)
						{
							num3 = 15;
							continue;
						}
						goto IL_108;
					}
					case 1:
					{
						Dictionary<int, string> dictionary;
						spr\u1ADE spr_u1ADE;
						string a_2;
						if (dictionary.TryGetValue(spr_u1ADE.ᜅ(), out a_2))
						{
							num3 = 6;
							continue;
						}
						goto IL_108;
					}
					case 2:
					{
						Dictionary<int, string> dictionary;
						spr\u1ADE spr_u1ADE;
						int num4;
						this.ᜀ(spr_u1ADE, A_0, num4, ref dictionary);
						this.ᜄ.RemoveAt(num5);
						list.RemoveAt(num5);
						num--;
						num5--;
						num3 = 8;
						continue;
					}
					case 3:
						goto IL_252;
					case 4:
					{
						list.Add((int)A_0.Length);
						Dictionary<int, string> dictionary = null;
						num5 = 0;
						num3 = 3;
						continue;
					}
					case 5:
						goto IL_108;
					case 6:
					{
						spr\u1ADE spr_u1ADE;
						string a_2;
						spr_u1ADE.ᜀ(a_2);
						num3 = 5;
						continue;
					}
					case 7:
						goto IL_189;
					case 8:
						goto IL_108;
					case 9:
					{
						if (num2 >= num)
						{
							num3 = 4;
							continue;
						}
						int a_3 = sprữ.ᜁ(A_0, a_);
						int item = sprữ.ᜁ(A_0, a_);
						this.ᜄ.Add(new spr\u1ADE(a_3));
						list.Add(item);
						num2++;
						num3 = 13;
						continue;
					}
					case 10:
						if (num5 >= num)
						{
							num3 = 11;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_252;
						default:
						{
							if (false)
							{
							}
							spr\u1ADE spr_u1ADE = this.ᜄ[num5];
							int num6 = list[num5];
							int num7 = list[num5 + 1];
							A_0.Position = (long)(this.ᜁ + list[num5]);
							int num4 = num7 - num6;
							num3 = 12;
							continue;
						}
						}
						break;
					case 11:
						return;
					case 12:
					{
						spr\u1ADE spr_u1ADE;
						if (spr_u1ADE.ᜅ() < 2)
						{
							num3 = 2;
							continue;
						}
						int num4;
						spr_u1ADE.ᜂ(A_0, num4);
						num3 = 0;
						continue;
					}
					case 13:
						goto IL_E7;
					case 14:
						goto IL_E7;
					case 15:
						num3 = 1;
						continue;
					}
					break;
					IL_E7:
					num3 = 9;
					continue;
					IL_108:
					num5++;
					num3 = 7;
					continue;
					IL_189:
					num3 = 10;
					continue;
					IL_252:
					goto IL_189;
				}
			}
			return;
		}
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x0016A7D8 File Offset: 0x001697D8
	private void ᜀ(spr\u1ADE A_0, Stream A_1, int A_2, ref Dictionary<int, string> A_3)
	{
		for (;;)
		{
			int num = A_0.ᜅ();
			if (num != 0)
			{
				goto IL_46;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_23;
			}
		}
		IL_23:
		if (false)
		{
		}
		if (true)
		{
		}
		A_3 = this.ᜀ(A_1);
		return;
		IL_46:
		A_0.ᜂ(A_1, A_2);
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x0016A834 File Offset: 0x00169834
	private Dictionary<int, string> ᜀ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			Dictionary<int, string> dictionary;
			for (;;)
			{
				byte[] a_ = new byte[4];
				int num = sprữ.ᜁ(A_0, a_);
				dictionary = new Dictionary<int, string>();
				int num2 = 0;
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_5B;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_87;
						}
						break;
					case 2:
					{
						if (num2 >= num)
						{
							num3 = 1;
							continue;
						}
						int key = sprữ.ᜁ(A_0, a_);
						string value = sprữ.ᜁ(A_0, -1);
						dictionary.Add(key, value);
						num2++;
						num3 = 0;
						continue;
					}
					case 3:
						goto IL_5B;
					}
					break;
					IL_5B:
					num3 = 2;
				}
			}
			IL_87:
			if (false)
			{
			}
			return dictionary;
		}
		}
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x0016A900 File Offset: 0x00169900
	public void ᜁ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			long position;
			for (;;)
			{
				for (;;)
				{
					this.ᜁ = (int)A_0.Position;
					sprữ.ᜂ(A_0, 0);
					Dictionary<int, string> dictionary = this.ᜀ();
					spr\u1ADE spr_u1ADE = new spr\u1ADE(1);
					spr_u1ADE.ᜄ = PropertyType.Int16;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_1A3;
						case 1:
						{
							spr_u1ADE.ᜀ((dictionary.Count == 0) ? 1251 : -535);
							int num2 = this.ᜄ.Count - 1;
							num = 8;
							continue;
						}
						case 2:
						{
							int num2;
							if (num2 < 0)
							{
								num = 3;
								continue;
							}
							spr\u1ADE spr_u1ADE2 = this.ᜄ[num2];
							num2--;
							num = 11;
							continue;
						}
						case 3:
						{
							int count = this.ᜄ.Count;
							sprữ.ᜂ(A_0, count);
							A_0.Position += (long)(count * 4 * 2);
							List<int> list = new List<int>();
							int num3 = 0;
							num = 10;
							continue;
						}
						case 4:
							goto IL_8C;
						case 5:
						{
							position = A_0.Position;
							A_0.Position = (long)(this.ᜁ + 8);
							int num4 = 0;
							List<int> list;
							int count2 = list.Count;
							num = 7;
							continue;
						}
						case 6:
							goto IL_182;
						case 7:
							goto IL_182;
						case 8:
							goto IL_1A8;
						case 9:
						{
							int count;
							int num3;
							if (num3 >= count)
							{
								num = 5;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								spr\u1ADE spr_u1ADE3 = this.ᜄ[num3];
								List<int> list;
								list.Add((int)A_0.Position);
								spr_u1ADE3.ᜀ(A_0);
								num3++;
								num = 4;
								continue;
							}
							}
							break;
						}
						case 10:
							if (true)
							{
							}
							goto IL_8C;
						case 11:
							goto IL_1A8;
						case 12:
						{
							int num4;
							int count2;
							if (num4 >= count2)
							{
								num = 0;
								continue;
							}
							List<int> list;
							int a_ = list[num4] - this.ᜁ;
							sprữ.ᜂ(A_0, this.ᜄ[num4].ᜅ());
							sprữ.ᜂ(A_0, a_);
							num4++;
							num = 6;
							continue;
						}
						}
						break;
						IL_8C:
						num = 9;
						continue;
						IL_182:
						num = 12;
						continue;
						IL_1A8:
						num = 2;
					}
				}
			}
			IL_1A3:
			this.ᜃ = (int)(position - (long)this.ᜁ);
			A_0.Position = (long)this.ᜁ;
			sprữ.ᜂ(A_0, this.ᜃ);
			A_0.Position = position;
			return;
		}
		}
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x0016ABB8 File Offset: 0x00169BB8
	private Dictionary<int, string> ᜀ()
	{
		switch (0)
		{
		default:
		{
			Dictionary<int, string> dictionary;
			for (;;)
			{
				dictionary = new Dictionary<int, string>();
				int num = 0;
				int count = this.ᜄ.Count;
				int num2 = 9;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_89;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (num < count)
							{
								spr\u1ADE spr_u1ADE = this.ᜄ[num];
								num2 = 7;
								continue;
							}
							break;
						}
						num2 = 3;
						continue;
					case 2:
					{
						spr\u1ADE spr_u1ADE2 = new spr\u1ADE(0);
						spr_u1ADE2.ᜀ(dictionary);
						this.ᜄ.Insert(0, spr_u1ADE2);
						num2 = 5;
						continue;
					}
					case 3:
						num2 = 6;
						continue;
					case 4:
					{
						spr\u1ADE spr_u1ADE;
						dictionary.Add(spr_u1ADE.ᜅ(), spr_u1ADE.ᜀ());
						num2 = 0;
						continue;
					}
					case 5:
						return dictionary;
					case 6:
						if (dictionary.Count > 0)
						{
							if (true)
							{
							}
							num2 = 2;
							continue;
						}
						return dictionary;
					case 7:
					{
						spr\u1ADE spr_u1ADE;
						if (spr_u1ADE.ᜀ() != null)
						{
							num2 = 4;
							continue;
						}
						goto IL_89;
					}
					case 8:
						goto IL_126;
					case 9:
						goto IL_126;
					}
					break;
					IL_89:
					num++;
					num2 = 8;
					continue;
					IL_126:
					num2 = 1;
				}
			}
			return dictionary;
		}
		}
	}

	// Token: 0x04001A29 RID: 6697
	private const int ᜀ = -2147483648;

	// Token: 0x04001A2A RID: 6698
	private int ᜁ;

	// Token: 0x04001A2B RID: 6699
	private Guid ᜂ;

	// Token: 0x04001A2C RID: 6700
	private int ᜃ;

	// Token: 0x04001A2D RID: 6701
	private List<spr\u1ADE> ᜄ = new List<spr\u1ADE>();
}
