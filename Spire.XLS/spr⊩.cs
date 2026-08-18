using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.XLS;

// Token: 0x0200027C RID: 636
internal class spr\u22A9
{
	// Token: 0x06002651 RID: 9809 RVA: 0x0015F578 File Offset: 0x0015E578
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

	// Token: 0x06002652 RID: 9810 RVA: 0x0015F5BC File Offset: 0x0015E5BC
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

	// Token: 0x06002653 RID: 9811 RVA: 0x0015F600 File Offset: 0x0015E600
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

	// Token: 0x06002654 RID: 9812 RVA: 0x0015F644 File Offset: 0x0015E644
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

	// Token: 0x06002655 RID: 9813 RVA: 0x0015F688 File Offset: 0x0015E688
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

	// Token: 0x06002656 RID: 9814 RVA: 0x0015F6CC File Offset: 0x0015E6CC
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

	// Token: 0x06002657 RID: 9815 RVA: 0x0015F710 File Offset: 0x0015E710
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

	// Token: 0x06002658 RID: 9816 RVA: 0x0015F758 File Offset: 0x0015E758
	public List<spr\u2129> ᜄ()
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

	// Token: 0x06002659 RID: 9817 RVA: 0x0015F79C File Offset: 0x0015E79C
	public spr\u22A9(Guid A_0, int A_1)
	{
		this.ᜂ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x0600265A RID: 9818 RVA: 0x0015F7C8 File Offset: 0x0015E7C8
	public void ᜂ(Stream A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte[] a_ = new byte[4];
				A_0.Position = (long)this.ᜁ;
				this.ᜃ = spr\u23D6.ᜁ(A_0, a_);
				int num = spr\u23D6.ᜁ(A_0, a_);
				List<int> list = new List<int>();
				int num2 = 0;
				int num3 = 0;
				for (;;)
				{
					spr\u2129 spr_u;
					string a_2;
					int num5;
					switch (num3)
					{
					case 0:
						goto IL_E0;
					case 1:
						return;
					case 2:
					{
						Dictionary<int, string> dictionary;
						if (dictionary.TryGetValue(spr_u.ᜅ(), out a_2))
						{
							num3 = 10;
							continue;
						}
						goto IL_127;
					}
					case 3:
					{
						if (spr_u.ᜅ() < 2)
						{
							num3 = 13;
							continue;
						}
						int num4;
						spr_u.ᜂ(A_0, num4);
						num3 = 4;
						continue;
					}
					case 4:
					{
						if (true)
						{
						}
						Dictionary<int, string> dictionary;
						if (dictionary != null)
						{
							num3 = 8;
							continue;
						}
						goto IL_127;
					}
					case 5:
					{
						if (num2 >= num)
						{
							num3 = 15;
							continue;
						}
						int a_3 = spr\u23D6.ᜁ(A_0, a_);
						int item = spr\u23D6.ᜁ(A_0, a_);
						this.ᜄ.Add(new spr\u2129(a_3));
						list.Add(item);
						num2++;
						num3 = 9;
						continue;
					}
					case 6:
						goto IL_127;
					case 7:
						goto IL_1AB;
					case 8:
						num3 = 2;
						continue;
					case 9:
						goto IL_E0;
					case 10:
						goto IL_96;
					case 11:
						goto IL_127;
					case 12:
						goto IL_1AB;
					case 13:
					{
						Dictionary<int, string> dictionary;
						int num4;
						this.ᜀ(spr_u, A_0, num4, ref dictionary);
						this.ᜄ.RemoveAt(num5);
						list.RemoveAt(num5);
						num--;
						num5--;
						num3 = 6;
						continue;
					}
					case 14:
					{
						if (num5 >= num)
						{
							num3 = 1;
							continue;
						}
						spr_u = this.ᜄ[num5];
						int num6 = list[num5];
						int num7 = list[num5 + 1];
						A_0.Position = (long)(this.ᜁ + list[num5]);
						int num4 = num7 - num6;
						num3 = 3;
						continue;
					}
					case 15:
					{
						list.Add((int)A_0.Length);
						Dictionary<int, string> dictionary = null;
						num5 = 0;
						num3 = 7;
						continue;
					}
					}
					break;
					IL_96:
					spr_u.ᜀ(a_2);
					num3 = 11;
					continue;
					IL_E0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_96;
					default:
						if (false)
						{
						}
						num3 = 5;
						continue;
					}
					IL_127:
					num5++;
					num3 = 12;
					continue;
					IL_1AB:
					num3 = 14;
				}
			}
			return;
		}
	}

	// Token: 0x0600265B RID: 9819 RVA: 0x0015FA70 File Offset: 0x0015EA70
	private void ᜀ(spr\u2129 A_0, Stream A_1, int A_2, ref Dictionary<int, string> A_3)
	{
		int num = A_0.ᜅ();
		if (num == 0)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_2D;
				}
			}
			IL_2D:
			if (false)
			{
			}
			if (true)
			{
			}
			A_3 = this.ᜀ(A_1);
			return;
		}
		A_0.ᜂ(A_1, A_2);
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x0015FACC File Offset: 0x0015EACC
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
				IL_2F:
				byte[] a_ = new byte[4];
				int num = spr\u23D6.ᜁ(A_0, a_);
				dictionary = new Dictionary<int, string>();
				int num2 = 0;
				for (;;)
				{
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_67;
						case 1:
							goto IL_51;
						case 2:
							goto IL_51;
						case 3:
						{
							if (num2 >= num)
							{
								num3 = 0;
								continue;
							}
							int key = spr\u23D6.ᜁ(A_0, a_);
							string value = spr\u23D6.ᜁ(A_0, -1);
							dictionary.Add(key, value);
							num2++;
							num3 = 2;
							continue;
						}
						}
						goto IL_2F;
						IL_51:
						num3 = 3;
					}
					IL_67:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					goto Block_2;
				}
			}
			Block_2:
			if (false)
			{
			}
			return dictionary;
		}
		}
	}

	// Token: 0x0600265D RID: 9821 RVA: 0x0015FB98 File Offset: 0x0015EB98
	public void ᜁ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			long position;
			for (;;)
			{
				this.ᜁ = (int)A_0.Position;
				spr\u23D6.ᜂ(A_0, 0);
				Dictionary<int, string> dictionary = this.ᜀ();
				spr\u2129 spr_u = new spr\u2129(1);
				spr_u.ᜄ = PropertyType.Int16;
				int num = 8;
				for (;;)
				{
					List<int> list;
					int num3;
					int count2;
					switch (num)
					{
					case 0:
						goto IL_1F0;
					case 1:
						goto IL_8F;
					case 2:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 9;
							continue;
						}
						int a_ = list[num2] - this.ᜁ;
						spr\u23D6.ᜂ(A_0, this.ᜄ[num2].ᜅ());
						spr\u23D6.ᜂ(A_0, a_);
						num2++;
						num = 12;
						continue;
					}
					case 3:
					{
						if (num3 >= count2)
						{
							num = 5;
							continue;
						}
						spr\u2129 spr_u2 = this.ᜄ[num3];
						list.Add((int)A_0.Position);
						spr_u2.ᜀ(A_0);
						num3++;
						num = 1;
						continue;
					}
					case 4:
						goto IL_1AE;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F0;
						default:
						{
							if (false)
							{
							}
							if (true)
							{
							}
							position = A_0.Position;
							A_0.Position = (long)(this.ᜁ + 8);
							int num2 = 0;
							int count = list.Count;
							num = 4;
							continue;
						}
						}
						break;
					case 6:
					{
						int num4;
						if (num4 < 0)
						{
							num = 0;
							continue;
						}
						spr\u2129 spr_u3 = this.ᜄ[num4];
						num4--;
						num = 11;
						continue;
					}
					case 7:
						goto IL_1D4;
					case 8:
					{
						spr_u.ᜀ((dictionary.Count == 0) ? 1251 : -535);
						int num4 = this.ᜄ.Count - 1;
						num = 7;
						continue;
					}
					case 9:
						goto IL_1CF;
					case 10:
						goto IL_8F;
					case 11:
						goto IL_1D4;
					case 12:
						goto IL_1AE;
					}
					break;
					IL_8F:
					num = 3;
					continue;
					IL_1AE:
					num = 2;
					continue;
					IL_1D4:
					num = 6;
					continue;
					IL_1F0:
					count2 = this.ᜄ.Count;
					spr\u23D6.ᜂ(A_0, count2);
					A_0.Position += (long)(count2 * 4 * 2);
					list = new List<int>();
					num3 = 0;
					num = 10;
				}
			}
			IL_1CF:
			this.ᜃ = (int)(position - (long)this.ᜁ);
			A_0.Position = (long)this.ᜁ;
			spr\u23D6.ᜂ(A_0, this.ᜃ);
			A_0.Position = position;
			return;
		}
		}
	}

	// Token: 0x0600265E RID: 9822 RVA: 0x0015FE54 File Offset: 0x0015EE54
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
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						spr\u2129 spr_u = new spr\u2129(0);
						spr_u.ᜀ(dictionary);
						this.ᜄ.Insert(0, spr_u);
						num2 = 9;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_61;
						default:
						{
							if (false)
							{
							}
							spr\u2129 spr_u2;
							dictionary.Add(spr_u2.ᜅ(), spr_u2.ᜀ());
							num2 = 7;
							continue;
						}
						}
						break;
					case 2:
					{
						spr\u2129 spr_u2;
						if (spr_u2.ᜀ() != null)
						{
							num2 = 1;
							continue;
						}
						goto IL_89;
					}
					case 3:
						goto IL_61;
					case 4:
						goto IL_142;
					case 5:
					{
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						spr\u2129 spr_u2 = this.ᜄ[num];
						num2 = 2;
						continue;
					}
					case 6:
						if (dictionary.Count > 0)
						{
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						return dictionary;
					case 7:
						goto IL_89;
					case 8:
						goto IL_142;
					case 9:
						return dictionary;
					}
					break;
					IL_61:
					num2 = 6;
					continue;
					IL_89:
					num++;
					num2 = 8;
					continue;
					IL_142:
					num2 = 5;
				}
			}
			return dictionary;
		}
		}
	}

	// Token: 0x040012F2 RID: 4850
	private const int ᜀ = -2147483648;

	// Token: 0x040012F3 RID: 4851
	private int ᜁ;

	// Token: 0x040012F4 RID: 4852
	private Guid ᜂ;

	// Token: 0x040012F5 RID: 4853
	private int ᜃ;

	// Token: 0x040012F6 RID: 4854
	private List<spr\u2129> ᜄ = new List<spr\u2129>();
}
