using System;

// Token: 0x0200042B RID: 1067
[CLSCompliant(false)]
internal class spr\u2462 : spr\u23F8
{
	// Token: 0x06003B60 RID: 15200 RVA: 0x0037130C File Offset: 0x0037030C
	internal override int ᜇ()
	{
		int num;
		for (;;)
		{
			num = 4;
			int num2 = 6;
			for (;;)
			{
				int num3;
				switch (num2)
				{
				case 0:
					goto IL_4B;
				case 1:
					if (num3 >= this.ᜄ.Length)
					{
						num2 = 7;
						continue;
					}
					num2 = 2;
					continue;
				case 2:
					if (this.ᜄ[num3] != null)
					{
						num2 = 5;
						continue;
					}
					goto IL_4B;
				case 3:
					return num;
				case 4:
					goto IL_AB;
				case 5:
					num += this.ᜄ[num3].ᜇ();
					num2 = 0;
					continue;
				case 6:
					if (this.ᜄ == null)
					{
						num2 = 3;
						continue;
					}
					num3 = 0;
					num2 = 4;
					continue;
				case 7:
					return num;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4B;
					default:
						if (false)
						{
						}
						goto IL_AB;
					}
					break;
				}
				break;
				IL_4B:
				if (true)
				{
				}
				num3++;
				num2 = 8;
				continue;
				IL_AB:
				num2 = 1;
			}
		}
		return num;
	}

	// Token: 0x06003B61 RID: 15201 RVA: 0x00371418 File Offset: 0x00370418
	internal int ᜀ()
	{
		if (this.ᜄ != null)
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
				return this.ᜄ.Length;
			}
		}
		return 0;
	}

	// Token: 0x06003B62 RID: 15202 RVA: 0x00371468 File Offset: 0x00370468
	internal void ᜀ(int A_0)
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
		this.ᜄ = new spr\u1C2A[A_0];
	}

	// Token: 0x06003B63 RID: 15203 RVA: 0x003714B0 File Offset: 0x003704B0
	internal spr\u1C2A[] ᜁ()
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
		return this.ᜄ;
	}

	// Token: 0x06003B64 RID: 15204 RVA: 0x003714F4 File Offset: 0x003704F4
	internal spr\u2462()
	{
	}

	// Token: 0x06003B65 RID: 15205 RVA: 0x00371514 File Offset: 0x00370514
	internal override void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_14F;
				case 1:
					return;
				case 2:
					goto IL_BF;
				case 3:
					goto IL_95;
				case 4:
					if (this.ᜁ == this.ᜀ)
					{
						num = 3;
						continue;
					}
					goto IL_14F;
				case 6:
					goto IL_BF;
				case 7:
				{
					if (num2 >= (int)this.ᜂ)
					{
						num = 1;
						continue;
					}
					spr\u1C2A spr_u1C2A = this.ᜄ[num2] = new spr\u1C2A();
					spr_u1C2A.ᜀ(A_0, A_1, A_2);
					A_1 += spr_u1C2A.ᜇ();
					A_2 -= spr_u1C2A.ᜇ();
					num2++;
					num = 6;
					continue;
				}
				case 8:
					return;
				}
				if (A_0.Length < 2)
				{
					num = 8;
					continue;
				}
				this.ᜂ = (this.ᜁ = spr\u23F8.ᜃ(A_0, A_1));
				A_1 += 2;
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
					num = 4;
					continue;
				}
				IL_95:
				this.ᜂ = spr\u23F8.ᜀ(A_0, ref A_1);
				num = 0;
				continue;
				IL_BF:
				num = 7;
				continue;
				IL_14F:
				this.ᜃ = spr\u23F8.ᜀ(A_0, ref A_1);
				A_2 = A_0.Length - A_1;
				this.ᜄ = new spr\u1C2A[(int)this.ᜂ];
				num2 = 0;
				num = 2;
			}
			return;
		}
		}
	}

	// Token: 0x06003B66 RID: 15206 RVA: 0x003716AC File Offset: 0x003706AC
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int result;
		for (;;)
		{
			result = 0;
			this.ᜂ = (ushort)this.ᜄ.Length;
			spr\u23F8.ᜀ(A_0, this.ᜂ, ref A_1);
			spr\u23F8.ᜀ(A_0, this.ᜃ, ref A_1);
			int num = 0;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return result;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_7F;
					case 1:
						goto IL_7F;
					case 2:
						return result;
					case 3:
						if (num >= this.ᜄ.Length)
						{
							num2 = 2;
							continue;
						}
						A_1 = this.ᜄ[num].ᜀ(A_0, A_1);
						num++;
						num2 = 0;
						continue;
					}
					break;
					IL_7F:
					num2 = 3;
				}
				break;
			}
			}
		}
		return result;
	}

	// Token: 0x04002B98 RID: 11160
	private new ushort ᜀ = ushort.MaxValue;

	// Token: 0x04002B99 RID: 11161
	private new ushort ᜁ;

	// Token: 0x04002B9A RID: 11162
	private new ushort ᜂ;

	// Token: 0x04002B9B RID: 11163
	private new ushort ᜃ;

	// Token: 0x04002B9C RID: 11164
	private new spr\u1C2A[] ᜄ;
}
