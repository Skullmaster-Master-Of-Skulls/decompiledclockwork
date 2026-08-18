using System;
using System.Collections.Generic;

// Token: 0x02000421 RID: 1057
internal class sprᦎ
{
	// Token: 0x06003F18 RID: 16152 RVA: 0x00239E24 File Offset: 0x00238E24
	public int ᜀ()
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

	// Token: 0x06003F19 RID: 16153 RVA: 0x00239E68 File Offset: 0x00238E68
	public static int ᜂ(int A_0)
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
		int num = A_0 % 1024;
		return A_0 - num;
	}

	// Token: 0x06003F1A RID: 16154 RVA: 0x00239EB0 File Offset: 0x00238EB0
	public bool ᜆ(int A_0)
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
		int key = sprᦎ.ᜂ(A_0);
		return this.ᜁ.ContainsKey(key);
	}

	// Token: 0x06003F1B RID: 16155 RVA: 0x00239F00 File Offset: 0x00238F00
	public bool ᜃ(int A_0, int A_1)
	{
		int num;
		bool flag;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_78:
			if (num >= A_1)
			{
				return flag;
			}
			num2 = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_46;
		}
		int num3;
		for (;;)
		{
			IL_28:
			switch (num2)
			{
			case 0:
				goto IL_78;
			case 1:
				if (!flag)
				{
					num2 = 3;
					continue;
				}
				if (true)
				{
				}
				flag = !this.ᜁ.ContainsKey(num3);
				num++;
				num3 += 1024;
				num2 = 5;
				continue;
			case 2:
				num2 = 1;
				continue;
			case 3:
				goto IL_6E;
			case 4:
				goto IL_70;
			case 5:
				goto IL_70;
			}
			goto IL_46;
			IL_70:
			num2 = 0;
		}
		IL_6E:
		return flag;
		IL_46:
		num3 = sprᦎ.ᜂ(A_0);
		flag = true;
		num = 0;
		num2 = 4;
		goto IL_28;
	}

	// Token: 0x06003F1C RID: 16156 RVA: 0x00239FC8 File Offset: 0x00238FC8
	public int ᜄ(int A_0)
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
		int num = A_0 % 1024;
		int key = A_0 - num;
		int result;
		this.ᜁ.TryGetValue(key, out result);
		return result;
	}

	// Token: 0x06003F1D RID: 16157 RVA: 0x0023A020 File Offset: 0x00239020
	public bool ᜁ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				int num = A_0;
				A_0 = sprᦎ.ᜂ(A_0);
				this.ᜄ = Math.Max(this.ᜄ, A_1);
				int num2 = sprᦎ.ᜂ(A_1);
				flag = false;
				int num3 = (num2 - A_0) / 1024 + 1;
				int num4 = 2;
				for (;;)
				{
					int num5;
					int num6;
					switch (num4)
					{
					case 0:
						goto IL_27F;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_249;
						default:
							goto IL_1C8;
						}
						break;
					case 2:
						if (this.ᜃ(A_0, num3))
						{
							num4 = 5;
							continue;
						}
						flag = this.ᜀ(A_0, num2, A_2);
						num4 = 1;
						continue;
					case 3:
						if (num5 > A_1)
						{
							goto IL_249;
						}
						this.ᜁ(num5);
						num5++;
						num4 = 6;
						continue;
					case 4:
						goto IL_25A;
					case 5:
						flag = true;
						num4 = 13;
						continue;
					case 6:
						goto IL_235;
					case 7:
						num4 = 10;
						continue;
					case 8:
						this.ᜃ.Add(A_2, new KeyValuePair<int, int>(num3, A_0));
						num4 = 15;
						continue;
					case 9:
						goto IL_25A;
					case 10:
						if (!flag)
						{
							num4 = 16;
							continue;
						}
						this.ᜁ.Add(num6, A_2);
						num6 += 1024;
						num4 = 4;
						continue;
					case 11:
						goto IL_235;
					case 12:
						num4 = 14;
						continue;
					case 13:
					{
						KeyValuePair<int, int> keyValuePair;
						if (!this.ᜃ.TryGetValue(A_2, out keyValuePair))
						{
							num4 = 8;
							continue;
						}
						int key = keyValuePair.Key + (int)Math.Ceiling((double)(num2 - A_0 + 1) / 1024.0);
						KeyValuePair<int, int> value = new KeyValuePair<int, int>(key, keyValuePair.Value);
						this.ᜃ[A_2] = value;
						if (true)
						{
						}
						num4 = 0;
						continue;
					}
					case 14:
						goto IL_15F;
					case 15:
						goto IL_27F;
					case 16:
						goto IL_188;
					case 17:
						if (num6 <= A_1)
						{
							num4 = 7;
							continue;
						}
						goto IL_188;
					}
					break;
					IL_188:
					num5 = num;
					num4 = 11;
					continue;
					IL_235:
					num4 = 3;
					continue;
					IL_249:
					num4 = 12;
					continue;
					IL_25A:
					num4 = 17;
					continue;
					IL_27F:
					num6 = A_0;
					num4 = 9;
				}
			}
			IL_15F:
			return flag;
			IL_1C8:
			if (false)
			{
			}
			return flag;
		}
		}
	}

	// Token: 0x06003F1E RID: 16158 RVA: 0x0023A2C0 File Offset: 0x002392C0
	private void ᜁ(int A_0)
	{
		int key;
		int num2;
		for (;;)
		{
			key = sprᦎ.ᜂ(A_0);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7B;
				case 1:
					if (true)
					{
					}
					if (!this.ᜂ.TryGetValue(key, out num2))
					{
						num2 = 1;
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7B;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_87;
				case 3:
					goto IL_79;
				}
				break;
				IL_7B:
				num2++;
				num = 2;
			}
		}
		IL_79:
		IL_87:
		this.ᜂ[key] = num2;
	}

	// Token: 0x06003F1F RID: 16159 RVA: 0x0023A364 File Offset: 0x00239364
	private bool ᜀ(int A_0, int A_1, int A_2)
	{
		int num;
		bool flag;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_79:
			if (num > A_1)
			{
				return flag;
			}
			num2 = 5;
			break;
		default:
			if (false)
			{
			}
			goto IL_46;
		}
		for (;;)
		{
			IL_28:
			switch (num2)
			{
			case 0:
				goto IL_69;
			case 1:
				if (!flag)
				{
					num2 = 3;
					continue;
				}
				flag = (this.ᜄ(A_0) == A_2);
				num += 1024;
				num2 = 0;
				continue;
			case 2:
				goto IL_79;
			case 3:
				goto IL_67;
			case 4:
				goto IL_69;
			case 5:
				num2 = 1;
				continue;
			}
			goto IL_46;
			IL_69:
			if (true)
			{
			}
			num2 = 2;
		}
		IL_67:
		return flag;
		IL_46:
		flag = true;
		num = A_0;
		num2 = 4;
		goto IL_28;
	}

	// Token: 0x06003F20 RID: 16160 RVA: 0x0023A418 File Offset: 0x00239418
	private void ᜀ(int A_0)
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
		int key = sprᦎ.ᜂ(A_0);
		this.ᜁ.Remove(key);
	}

	// Token: 0x06003F21 RID: 16161 RVA: 0x0023A468 File Offset: 0x00239468
	public void ᜂ(int A_0, int A_1)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜄ(A_0) != A_1)
				{
					num = 1;
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
					this.ᜀ(A_0);
					A_0 += 1024;
					num = 2;
					continue;
				}
				break;
			case 1:
				return;
			}
			IL_22:
			num = 0;
			continue;
			goto IL_22;
		}
	}

	// Token: 0x06003F22 RID: 16162 RVA: 0x0023A4FC File Offset: 0x002394FC
	public void ᜇ(int A_0)
	{
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			KeyValuePair<int, int> keyValuePair;
			switch (num)
			{
			case 0:
				return;
			case 1:
				this.ᜂ(keyValuePair.Value, A_0);
				num = 0;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			if (!this.ᜃ.TryGetValue(A_0, out keyValuePair))
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06003F23 RID: 16163 RVA: 0x0023A588 File Offset: 0x00239588
	public int ᜀ(int A_0, int A_1)
	{
		int num;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_7B:
			if (this.ᜁ(num, num + A_0, A_1))
			{
				int result;
				return result;
			}
			num2 = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_46;
		}
		int a_;
		for (;;)
		{
			IL_1E:
			switch (num2)
			{
			case 0:
				goto IL_90;
			case 1:
				if (this.ᜃ(num, a_))
				{
					num2 = 4;
					continue;
				}
				num += 1024;
				num2 = 3;
				continue;
			case 2:
				goto IL_7B;
			case 3:
				goto IL_92;
			case 4:
			{
				int result = num;
				num2 = 2;
				continue;
			}
			case 5:
				goto IL_92;
			}
			goto IL_46;
			IL_92:
			num2 = 1;
		}
		IL_90:
		throw new InvalidOperationException();
		IL_46:
		num = 1024;
		a_ = (int)Math.Ceiling((double)A_0 / 1024.0);
		if (true)
		{
		}
		num2 = 5;
		goto IL_1E;
	}

	// Token: 0x06003F24 RID: 16164 RVA: 0x0023A664 File Offset: 0x00239664
	public int ᜃ(int A_0)
	{
		KeyValuePair<int, int> keyValuePair;
		if (!this.ᜃ.TryGetValue(A_0, out keyValuePair))
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_42;
			}
			if (false)
			{
			}
			return 0;
		}
		IL_42:
		return keyValuePair.Key * 1024;
	}

	// Token: 0x06003F25 RID: 16165 RVA: 0x0023A6C0 File Offset: 0x002396C0
	public int ᜅ(int A_0)
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
		A_0 = sprᦎ.ᜂ(A_0);
		int result;
		this.ᜂ.TryGetValue(A_0, out result);
		return result;
	}

	// Token: 0x06003F26 RID: 16166 RVA: 0x0023A714 File Offset: 0x00239714
	public void ᜁ(int A_0, int A_1)
	{
		int num = 2;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6E;
			case 1:
				num2 = 0;
				num = 0;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_70;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (this.ᜅ.TryGetValue(A_0, out num2))
				{
					goto IL_70;
				}
				num = 1;
				break;
			}
		}
		IL_6E:
		IL_70:
		this.ᜅ[A_0] = num2 + A_1;
	}

	// Token: 0x06003F27 RID: 16167 RVA: 0x0023A7A0 File Offset: 0x002397A0
	public int ᜈ(int A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		int result;
		this.ᜅ.TryGetValue(A_0, out result);
		return result;
	}

	// Token: 0x04001CB5 RID: 7349
	public const int ᜀ = 1024;

	// Token: 0x04001CB6 RID: 7350
	private Dictionary<int, int> ᜁ = new Dictionary<int, int>();

	// Token: 0x04001CB7 RID: 7351
	private Dictionary<int, int> ᜂ = new Dictionary<int, int>();

	// Token: 0x04001CB8 RID: 7352
	private Dictionary<int, KeyValuePair<int, int>> ᜃ = new Dictionary<int, KeyValuePair<int, int>>();

	// Token: 0x04001CB9 RID: 7353
	private int ᜄ;

	// Token: 0x04001CBA RID: 7354
	private Dictionary<int, int> ᜅ = new Dictionary<int, int>();
}
