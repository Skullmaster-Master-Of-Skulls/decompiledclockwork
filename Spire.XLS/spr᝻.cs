using System;
using System.Drawing;
using Spire.Xls;

// Token: 0x02000350 RID: 848
internal class spr\u177B : spr\u2374
{
	// Token: 0x0600338B RID: 13195 RVA: 0x001DC188 File Offset: 0x001DB188
	public spr\u177B(object[][] A_0, Type[] A_1, OrderBy[] A_2, Color[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x0600338C RID: 13196 RVA: 0x001DC1A0 File Offset: 0x001DB1A0
	public override void ᜄ(int A_0, int A_1, int A_2)
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
		this.ᜌ(A_0, A_1, A_2);
	}

	// Token: 0x0600338D RID: 13197 RVA: 0x001DC1E4 File Offset: 0x001DB1E4
	private void ᜌ(int A_0, int A_1, int A_2)
	{
		if (this.ᜃ[A_2 - 1] != OrderBy.Bottom)
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
				this.ᜋ(A_0, A_1, A_2);
				return;
			}
		}
		if (true)
		{
		}
		this.ᜊ(A_0, A_1, A_2);
	}

	// Token: 0x0600338E RID: 13198 RVA: 0x001DC244 File Offset: 0x001DB244
	private void ᜋ(int A_0, int A_1, int A_2)
	{
		for (;;)
		{
			IL_24:
			int num = this.ᜄ;
			for (;;)
			{
				IL_2B:
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_CF;
					case 1:
						goto IL_38;
					case 2:
						this.ᜀ(num, this.ᜄ);
						this.ᜄ++;
						num2 = 1;
						continue;
					case 3:
						if (num > A_1)
						{
							num2 = 4;
							continue;
						}
						num2 = 6;
						continue;
					case 4:
						return;
					case 5:
						goto IL_CF;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B;
						default:
							if (false)
							{
							}
							if (this.ᜀ(this.ᜆ[A_2 - 1], (Color)this.ᜀ[num][A_2]))
							{
								num2 = 2;
								continue;
							}
							goto IL_38;
						}
						break;
					}
					goto IL_24;
					IL_38:
					num++;
					num2 = 5;
					continue;
					IL_CF:
					if (true)
					{
					}
					num2 = 3;
				}
			}
		}
	}

	// Token: 0x0600338F RID: 13199 RVA: 0x001DC350 File Offset: 0x001DB350
	private new bool ᜀ(Color A_0, Color A_1)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return true;
			case 1:
				num = 2;
				continue;
			case 2:
				if (A_0.B == A_1.B)
				{
					num = 0;
					continue;
				}
				return false;
			case 3:
				goto IL_8C;
			case 4:
				if (true)
				{
				}
				if (A_0.G == A_1.G)
				{
					num = 1;
					continue;
				}
				return false;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8C:
				num = 4;
				break;
			default:
				if (false)
				{
				}
				if (A_0.R != A_1.R)
				{
					return false;
				}
				num = 3;
				break;
			}
		}
		return true;
	}

	// Token: 0x06003390 RID: 13200 RVA: 0x001DC41C File Offset: 0x001DB41C
	private void ᜊ(int A_0, int A_1, int A_2)
	{
		for (;;)
		{
			IL_24:
			int num = this.ᜅ;
			for (;;)
			{
				IL_2B:
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							if (this.ᜀ(this.ᜆ[A_2 - 1], (Color)this.ᜀ[num][A_2]))
							{
								num2 = 6;
								continue;
							}
							goto IL_38;
						}
						break;
					case 1:
						if (num < A_0)
						{
							num2 = 3;
							continue;
						}
						num2 = 0;
						continue;
					case 2:
						goto IL_D7;
					case 3:
						return;
					case 4:
						goto IL_38;
					case 5:
						goto IL_D7;
					case 6:
						this.ᜁ(num, this.ᜅ);
						this.ᜅ--;
						num2 = 4;
						continue;
					}
					goto IL_24;
					IL_38:
					num--;
					num2 = 5;
					continue;
					IL_D7:
					num2 = 1;
				}
			}
		}
	}

	// Token: 0x06003391 RID: 13201 RVA: 0x001DC528 File Offset: 0x001DB528
	internal new void ᜁ(int A_0, int A_1)
	{
		for (;;)
		{
			int num = A_0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= A_1)
					{
						num2 = 1;
						continue;
					}
					base.ᜃ(num, num + 1);
					num++;
					num2 = 2;
					continue;
				case 1:
					return;
				case 2:
					goto IL_48;
				case 3:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_48;
					}
					break;
				}
				break;
				IL_48:
				num2 = 0;
			}
		}
	}

	// Token: 0x06003392 RID: 13202 RVA: 0x001DC5B8 File Offset: 0x001DB5B8
	internal new void ᜀ(int A_0, int A_1)
	{
		for (;;)
		{
			int num = A_0;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_48;
				case 1:
					if (num <= A_1)
					{
						num2 = 3;
						continue;
					}
					base.ᜃ(num, num - 1);
					num--;
					num2 = 0;
					continue;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_48;
					}
					break;
				case 3:
					return;
				}
				break;
				IL_48:
				num2 = 1;
			}
		}
	}

	// Token: 0x06003393 RID: 13203 RVA: 0x001DC648 File Offset: 0x001DB648
	public new void ᜉ(int A_0, int A_1, int A_2)
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

	// Token: 0x06003394 RID: 13204 RVA: 0x001DC688 File Offset: 0x001DB688
	public new void ᜂ(int A_0, int A_1, int A_2)
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

	// Token: 0x06003395 RID: 13205 RVA: 0x001DC6C8 File Offset: 0x001DB6C8
	public new void ᜅ(int A_0, int A_1, int A_2)
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

	// Token: 0x06003396 RID: 13206 RVA: 0x001DC708 File Offset: 0x001DB708
	public new void ᜃ(int A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}

	// Token: 0x06003397 RID: 13207 RVA: 0x001DC748 File Offset: 0x001DB748
	public new void ᜁ(int A_0, int A_1, int A_2)
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

	// Token: 0x06003398 RID: 13208 RVA: 0x001DC788 File Offset: 0x001DB788
	public new void ᜀ(int A_0, int A_1, int A_2)
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

	// Token: 0x06003399 RID: 13209 RVA: 0x001DC7C8 File Offset: 0x001DB7C8
	public new void ᜆ(int A_0, int A_1, int A_2)
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

	// Token: 0x0600339A RID: 13210 RVA: 0x001DC808 File Offset: 0x001DB808
	public new void ᜇ(int A_0, int A_1, int A_2)
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

	// Token: 0x0600339B RID: 13211 RVA: 0x001DC848 File Offset: 0x001DB848
	public new void ᜈ(int A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}
}
