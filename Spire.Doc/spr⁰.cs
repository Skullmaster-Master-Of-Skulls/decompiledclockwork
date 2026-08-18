using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200042A RID: 1066
[CLSCompliant(false)]
internal class spr\u2070 : spr\u23F8
{
	// Token: 0x06003B56 RID: 15190 RVA: 0x00370C8C File Offset: 0x0036FC8C
	internal spr\u2070()
	{
	}

	// Token: 0x06003B57 RID: 15191 RVA: 0x00370CA0 File Offset: 0x0036FCA0
	internal spr\u2070(byte[] A_0) : base(A_0)
	{
	}

	// Token: 0x06003B58 RID: 15192 RVA: 0x00370CB4 File Offset: 0x0036FCB4
	internal spr\u2070(Stream A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06003B59 RID: 15193 RVA: 0x00370CCC File Offset: 0x0036FCCC
	internal override void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_19B:
				num = 0;
				break;
			case 1:
				goto IL_37;
			default:
				goto IL_37;
			}
			for (;;)
			{
				IL_48:
				switch (num)
				{
				case 0:
					if (A_2 < 0)
					{
						num = 2;
						continue;
					}
					num = 5;
					continue;
				case 1:
					return;
				case 2:
					goto IL_1BA;
				case 3:
					goto IL_129;
				case 4:
					goto IL_17D;
				case 5:
				{
					if (A_1 + A_2 > A_0.Length)
					{
						num = 7;
						continue;
					}
					if (true)
					{
					}
					int num2 = A_0.Length;
					int num3 = 16;
					int num4 = num2 / num3;
					this.ᜀ = new int[num4 + 1];
					this.ᜁ = new spr\u2572[num4];
					A_1 = (num4 + 1) * 4;
					Buffer.BlockCopy(A_0, 0, this.ᜀ, 0, A_1);
					int num5 = 0;
					num = 4;
					continue;
				}
				case 6:
					goto IL_17D;
				case 7:
					goto IL_109;
				case 8:
					goto IL_8E;
				case 9:
				{
					int num4;
					int num5;
					if (num5 >= num4)
					{
						num = 1;
						continue;
					}
					this.ᜁ[num5] = new spr\u2572(A_0, A_1);
					num5++;
					A_1 += 12;
					num = 6;
					continue;
				}
				case 10:
					if (A_1 != 0)
					{
						num = 3;
						continue;
					}
					goto IL_19B;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 10;
				continue;
				IL_17D:
				num = 9;
			}
			IL_8E:
			throw new ArgumentNullException(ClipboardData.b("፱ٳѵ㱷᭹ࡻώ", a_));
			IL_109:
			throw new ArgumentOutOfRangeException(ClipboardData.b("᭱㭳ၵṷॹ᥻੽ꁿꦁꒃ쮇曆", a_));
			IL_129:
			throw new ArgumentOutOfRangeException(ClipboardData.b("᭱㭳ၵṷॹ᥻੽", a_));
			IL_1BA:
			throw new ArgumentOutOfRangeException(ClipboardData.b("᭱㝳᥵൷ᑹࡻ", a_));
			IL_37:
			if (false)
			{
			}
			num = 11;
			goto IL_48;
		}
		}
	}

	// Token: 0x06003B5A RID: 15194 RVA: 0x00370EC0 File Offset: 0x0036FEC0
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 7;
			int num3;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
				{
					if (A_1 + num2 > A_0.Length)
					{
						num = 3;
						continue;
					}
					num3 = A_1;
					int num4 = this.ᜀ.Length * 4;
					Buffer.BlockCopy(this.ᜀ, 0, A_0, A_1, num4);
					A_1 += num4;
					int num5 = 0;
					int num6 = this.ᜁ.Length;
					num = 5;
					continue;
				}
				case 1:
					num = 0;
					continue;
				case 2:
				{
					int num5;
					int num6;
					if (num5 >= num6)
					{
						num = 4;
						continue;
					}
					this.ᜁ[num5].ᜀ(A_0, A_1);
					A_1 += this.ᜁ[num5].ᜇ();
					num5++;
					num = 6;
					continue;
				}
				case 3:
					goto IL_190;
				case 4:
					goto IL_121;
				case 5:
					if (true)
					{
					}
					goto IL_E8;
				case 6:
					goto IL_E8;
				case 8:
					goto IL_67;
				case 9:
					if (A_1 >= 0)
					{
						num = 1;
						continue;
					}
					goto IL_AC;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				IL_C0:
				num2 = this.ᜇ();
				num = 9;
				continue;
				IL_E8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C0;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
			IL_67:
			throw new ArgumentNullException(ClipboardData.b("୩ṫᱭ㑯፱s᝵", a_));
			IL_AC:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ͩ⍫࡭ᙯűᅳɵ", a_));
			IL_121:
			return A_1 - num3;
			IL_190:
			goto IL_AC;
		}
		}
	}

	// Token: 0x06003B5B RID: 15195 RVA: 0x00371068 File Offset: 0x00370068
	internal int[] ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x06003B5C RID: 15196 RVA: 0x003710AC File Offset: 0x003700AC
	internal spr\u2572[] ᜁ()
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

	// Token: 0x06003B5D RID: 15197 RVA: 0x003710F0 File Offset: 0x003700F0
	internal override int ᜇ()
	{
		int num;
		for (;;)
		{
			num = this.ᜀ.Length * 4;
			int num2 = 0;
			int num3 = this.ᜁ.Length;
			int num4 = 2;
			for (;;)
			{
				switch (num4)
				{
				case 0:
					if (num2 >= num3)
					{
						if (true)
						{
						}
						num4 = 1;
						continue;
					}
					num += this.ᜁ[num2].ᜇ();
					num2++;
					num4 = 3;
					continue;
				case 1:
					return num;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9B;
					default:
						if (false)
						{
						}
						goto IL_5E;
					}
					break;
				case 3:
					goto IL_9B;
				}
				break;
				IL_5E:
				num4 = 0;
				continue;
				IL_9B:
				goto IL_5E;
			}
		}
		return num;
	}

	// Token: 0x06003B5E RID: 15198 RVA: 0x0037119C File Offset: 0x0037019C
	internal int ᜂ()
	{
		if (this.ᜁ == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			}
			if (false)
			{
			}
			return 0;
		}
		IL_08:
		if (true)
		{
		}
		return this.ᜁ.Length;
	}

	// Token: 0x06003B5F RID: 15199 RVA: 0x003711EC File Offset: 0x003701EC
	internal void ᜀ(int A_0)
	{
		int a_ = 14;
		for (;;)
		{
			IL_09:
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_C6;
				case 1:
					return;
				case 2:
					if (A_0 != this.ᜂ())
					{
						num = 4;
						continue;
					}
					return;
				case 4:
				{
					this.ᜁ = new spr\u2572[A_0];
					this.ᜀ = new int[A_0 + 1];
					int num2 = 0;
					num = 0;
					continue;
				}
				case 5:
					goto IL_C6;
				case 6:
					goto IL_6C;
				case 7:
				{
					int num2;
					if (num2 >= A_0)
					{
						num = 1;
						continue;
					}
					this.ᜁ[num2] = new spr\u2572();
					num2++;
					num = 5;
					continue;
				}
				}
				if (A_0 >= 0)
				{
					num = 2;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_09;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 6;
					continue;
				}
				IL_C6:
				num = 7;
			}
		}
		IL_6C:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ㅳᡵ౷ࡹᕻ᭽솁ﺉ", a_));
	}

	// Token: 0x04002B96 RID: 11158
	private new int[] ᜀ;

	// Token: 0x04002B97 RID: 11159
	private new spr\u2572[] ᜁ;
}
