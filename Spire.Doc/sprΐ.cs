using System;

// Token: 0x02000344 RID: 836
[CLSCompliant(false)]
internal class sprΐ
{
	// Token: 0x06002C9C RID: 11420 RVA: 0x002AF63C File Offset: 0x002AE63C
	internal byte ᜋ()
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

	// Token: 0x06002C9D RID: 11421 RVA: 0x002AF680 File Offset: 0x002AE680
	internal short[] ᜈ()
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

	// Token: 0x06002C9E RID: 11422 RVA: 0x002AF6C4 File Offset: 0x002AE6C4
	internal short[] ᜉ()
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

	// Token: 0x06002C9F RID: 11423 RVA: 0x002AF708 File Offset: 0x002AE708
	internal void ᜀ(short[] A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06002CA0 RID: 11424 RVA: 0x002AF74C File Offset: 0x002AE74C
	internal sprᡖ[] ᜊ()
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

	// Token: 0x06002CA1 RID: 11425 RVA: 0x002AF790 File Offset: 0x002AE790
	internal sprΐ(byte A_0)
	{
		this.ᜃ = A_0;
		this.ᜀ = new short[(int)A_0];
		this.ᜂ = new sprᡖ[(int)A_0];
	}

	// Token: 0x06002CA2 RID: 11426 RVA: 0x002AF7E8 File Offset: 0x002AE7E8
	internal sprΐ(sprḍ A_0, int A_1)
	{
		this.ᜀ(A_0.ᜇ(A_1));
	}

	// Token: 0x06002CA3 RID: 11427 RVA: 0x002AF82C File Offset: 0x002AE82C
	internal sprΐ(spr\u1CC1 A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002CA4 RID: 11428 RVA: 0x002AF86C File Offset: 0x002AE86C
	internal void ᜀ(sprḍ A_0, int A_1)
	{
		for (;;)
		{
			bool flag = this.ᜃ();
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_59;
				case 1:
					this.ᜁ();
					this.ᜀ();
					num = 0;
					continue;
				case 2:
					return;
				case 3:
					if (this.ᜄ == null)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_59;
					default:
						if (false)
						{
						}
						this.ᜂ();
						num = 4;
						continue;
					}
					break;
				case 4:
					if (flag)
					{
						num = 1;
						continue;
					}
					goto IL_A3;
				}
				break;
			}
		}
		return;
		IL_59:
		IL_A3:
		A_0.ᜀ(A_1, this.ᜄ);
	}

	// Token: 0x06002CA5 RID: 11429 RVA: 0x002AF92C File Offset: 0x002AE92C
	private void ᜀ(spr\u1CC1 A_0)
	{
		this.ᜄ = A_0.ᜅ();
		if (this.ᜄ == null)
		{
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
				break;
			}
			return;
		}
		this.ᜇ();
		this.ᜆ();
		this.ᜅ();
		this.ᜄ();
	}

	// Token: 0x06002CA6 RID: 11430 RVA: 0x002AF998 File Offset: 0x002AE998
	private void ᜇ()
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
		this.ᜅ = this.ᜄ[0];
		this.ᜆ = 0;
	}

	// Token: 0x06002CA7 RID: 11431 RVA: 0x002AF9E8 File Offset: 0x002AE9E8
	private void ᜆ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜆ = (int)(this.ᜅ * 2);
				this.ᜁ = new short[(int)this.ᜅ];
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_67;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 1:
				return;
			case 3:
				if (this.ᜄ.Length > this.ᜆ)
				{
					num = 4;
					continue;
				}
				return;
			case 4:
				Buffer.BlockCopy(this.ᜄ, 1, this.ᜁ, 0, (int)(this.ᜅ * 2));
				goto IL_67;
			}
			if (this.ᜅ > 0)
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			break;
			IL_67:
			num = 1;
		}
	}

	// Token: 0x06002CA8 RID: 11432 RVA: 0x002AFACC File Offset: 0x002AEACC
	private void ᜅ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
				if (this.ᜄ.Length > this.ᜆ + (int)(this.ᜃ * 2) + 1)
				{
					num = 5;
					continue;
				}
				return;
			case 3:
				goto IL_115;
			case 4:
				if (true)
				{
				}
				this.ᜃ = this.ᜄ[this.ᜆ + 1];
				this.ᜀ = new short[(int)this.ᜃ];
				this.ᜂ = new sprᡖ[(int)this.ᜃ];
				num = 3;
				continue;
			case 5:
				Buffer.BlockCopy(this.ᜄ, this.ᜆ + 2, this.ᜀ, 0, (int)(this.ᜃ * 2));
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_115;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			if (this.ᜄ.Length > this.ᜆ + 1)
			{
				num = 4;
				continue;
			}
			IL_95:
			num = 2;
			continue;
			IL_115:
			goto IL_95;
		}
	}

	// Token: 0x06002CA9 RID: 11433 RVA: 0x002AFC00 File Offset: 0x002AEC00
	private void ᜄ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_58;
			case 1:
			{
				int num2 = (int)((this.ᜃ + 1) * 2);
				int num3 = 0;
				num = 4;
				continue;
			}
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D8;
				default:
				{
					if (false)
					{
					}
					int num3;
					if (num3 >= (int)this.ᜃ)
					{
						num = 5;
						continue;
					}
					int num2;
					this.ᜂ[num3] = new sprᡖ(this.ᜄ[num2 + this.ᜆ]);
					num2++;
					num3++;
					num = 0;
					continue;
				}
				}
				break;
			case 4:
				goto IL_D8;
			case 5:
				return;
			}
			if (this.ᜄ.Length > this.ᜆ + (int)((this.ᜃ + 1) * 2))
			{
				num = 1;
				continue;
			}
			break;
			IL_58:
			num = 3;
			continue;
			IL_D8:
			goto IL_58;
		}
	}

	// Token: 0x06002CAA RID: 11434 RVA: 0x002AFCF4 File Offset: 0x002AECF4
	private bool ᜃ()
	{
		int num = 1;
		int num2;
		for (;;)
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
				switch (num)
				{
				case 0:
					if (num2 == 0)
					{
						goto IL_95;
					}
					goto IL_AC;
				case 2:
					return false;
				}
				if (true)
				{
				}
				this.ᜅ = (byte)((this.ᜁ != null) ? this.ᜁ.Length : 0);
				this.ᜆ = (int)(this.ᜅ * 2);
				num2 = (int)((this.ᜅ + 1) * 2);
				num2 += (int)this.ᜃ * (2 + sprᡖ.ᜀ);
				num = 0;
				continue;
			}
			IL_95:
			num = 2;
		}
		return false;
		IL_AC:
		this.ᜄ = new byte[num2];
		return true;
	}

	// Token: 0x06002CAB RID: 11435 RVA: 0x002AFDBC File Offset: 0x002AEDBC
	private void ᜂ()
	{
		int num = 3;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				this.ᜄ[0] = this.ᜅ;
				Buffer.BlockCopy(this.ᜁ, 0, this.ᜄ, 1, this.ᜆ);
				num = 1;
				continue;
			case 1:
				return;
			case 2:
				if (this.ᜅ > 0)
				{
					num = 0;
					continue;
				}
				return;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9D;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 4:
				goto IL_9D;
			}
			if (this.ᜄ.Length > this.ᜆ)
			{
				num = 4;
				continue;
			}
			break;
			IL_9D:
			num = 2;
		}
	}

	// Token: 0x06002CAC RID: 11436 RVA: 0x002AFE84 File Offset: 0x002AEE84
	private void ᜁ()
	{
		int num = 2;
		for (;;)
		{
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
				switch (num)
				{
				case 0:
					return;
				case 1:
					this.ᜄ[this.ᜆ + 1] = this.ᜃ;
					Buffer.BlockCopy(this.ᜀ, 0, this.ᜄ, this.ᜆ + 2, (int)(this.ᜃ * 2));
					goto IL_A6;
				}
				if (this.ᜄ.Length > this.ᜆ + (int)(this.ᜃ * 2) + 1)
				{
					num = 1;
					continue;
				}
				return;
			}
			IL_A6:
			num = 0;
		}
	}

	// Token: 0x06002CAD RID: 11437 RVA: 0x002AFF44 File Offset: 0x002AEF44
	private void ᜀ()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_56;
			case 1:
			{
				int num2 = (int)((this.ᜃ + 1) * 2);
				int num3 = 0;
				num = 5;
				continue;
			}
			case 2:
				return;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E5;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					int num3;
					if (num3 >= (int)this.ᜃ)
					{
						num = 2;
						continue;
					}
					int num2;
					this.ᜄ[this.ᜆ + num2] = this.ᜂ[num3].ᜀ();
					num2 += sprᡖ.ᜀ;
					num3++;
					num = 0;
					continue;
				}
				}
				break;
			case 5:
				goto IL_E5;
			}
			if (this.ᜄ.Length > this.ᜆ + (int)this.ᜃ * (2 + sprᡖ.ᜀ) + 1)
			{
				num = 1;
				continue;
			}
			break;
			IL_56:
			num = 4;
			continue;
			IL_E5:
			goto IL_56;
		}
	}

	// Token: 0x04002642 RID: 9794
	private short[] ᜀ = new short[0];

	// Token: 0x04002643 RID: 9795
	private short[] ᜁ = new short[0];

	// Token: 0x04002644 RID: 9796
	private sprᡖ[] ᜂ = new sprᡖ[0];

	// Token: 0x04002645 RID: 9797
	private byte ᜃ;

	// Token: 0x04002646 RID: 9798
	private byte[] ᜄ;

	// Token: 0x04002647 RID: 9799
	private byte ᜅ;

	// Token: 0x04002648 RID: 9800
	private int ᜆ;
}
