using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x02000390 RID: 912
[CLSCompliant(false)]
internal class spr᭑ : spr\u23F8
{
	// Token: 0x0600338E RID: 13198 RVA: 0x002F55CC File Offset: 0x002F45CC
	internal spr᭑()
	{
	}

	// Token: 0x0600338F RID: 13199 RVA: 0x002F55E0 File Offset: 0x002F45E0
	internal spr᭑(byte[] A_0) : base(A_0)
	{
	}

	// Token: 0x06003390 RID: 13200 RVA: 0x002F55F4 File Offset: 0x002F45F4
	internal spr᭑(byte[] A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06003391 RID: 13201 RVA: 0x002F560C File Offset: 0x002F460C
	internal spr᭑(byte[] A_0, int A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003392 RID: 13202 RVA: 0x002F5624 File Offset: 0x002F4624
	internal spr᭑(Stream A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06003393 RID: 13203 RVA: 0x002F563C File Offset: 0x002F463C
	internal override void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 2;
		for (;;)
		{
			IL_09:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 14;
					continue;
				case 2:
					if (A_1 >= 0)
					{
						num = 3;
						continue;
					}
					goto IL_20D;
				case 3:
					num = 13;
					continue;
				case 4:
					goto IL_208;
				case 5:
					return;
				case 6:
					goto IL_145;
				case 7:
					goto IL_145;
				case 8:
					return;
				case 9:
				{
					if (A_2 == 0)
					{
						num = 8;
						continue;
					}
					int num2 = (A_2 - 4) / 10;
					this.ᜀ = new uint[num2 + 1];
					this.ᜁ = new spr\u2243[num2];
					int num3 = (num2 + 1) * 4;
					Buffer.BlockCopy(A_0, 0, this.ᜀ, 0, num3);
					A_1 += num3;
					int num4 = 0;
					num = 7;
					continue;
				}
				case 10:
					goto IL_71;
				case 11:
				{
					int num2;
					int num4;
					if (num4 >= num2)
					{
						num = 5;
						continue;
					}
					this.ᜁ[num4] = new spr\u2243(A_0, A_1);
					num4++;
					A_1 += 6;
					num = 6;
					continue;
				}
				case 12:
					if (A_2 >= 0)
					{
						num = 0;
						continue;
					}
					goto IL_77;
				case 13:
					if (A_1 <= A_0.Length - 1)
					{
						num = 12;
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
						num = 15;
						continue;
					}
					break;
				case 14:
					if (A_2 + A_1 > A_0.Length)
					{
						num = 4;
						continue;
					}
					this.ᜁ = null;
					this.ᜀ = null;
					num = 9;
					continue;
				case 15:
					goto IL_CD;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 2;
				continue;
				IL_145:
				num = 11;
			}
		}
		IL_71:
		throw new ArgumentNullException(ClipboardData.b("१ᡩṫ⩭ᅯٱᕳ", a_));
		IL_77:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ŧ⥩ͫ᭭ṯٱ", a_));
		IL_CD:
		goto IL_20D;
		IL_208:
		goto IL_77;
		IL_20D:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ŧ╩੫࡭ͯ᝱s", a_), ClipboardData.b("㹧୩k᭭ᕯ剱ᝳ᝵ᙷ婹ቻᅽꊁꢇﶍ늑ﺕ聯벛꺝肟쎡쪣슥袧충\udeab쮭톯욱톳쒵颷캹풻\udfbd꺿ꗃ듅뫇軉귋뫍뇏ﳑ飓돕뛗뷙꣛뛝샟쿡쓣ퟥ", a_));
	}

	// Token: 0x06003394 RID: 13204 RVA: 0x002F5878 File Offset: 0x002F4878
	internal override void ᜀ(Stream A_0, int A_1)
	{
		int a_ = 18;
		int num3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_19C:
			int num;
			int num2;
			if (num >= num2)
			{
				num3 = 10;
			}
			else
			{
				byte[] array;
				A_0.Read(array, 0, 6);
				this.ᜁ[num] = new spr\u2243(array, 0, 6);
				num++;
				num3 = 1;
			}
			break;
		}
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num3 = 3;
				break;
			}
			break;
		}
		for (;;)
		{
			switch (num3)
			{
			case 0:
			{
				int num4;
				if (num4 < 6)
				{
					num3 = 8;
					continue;
				}
				goto IL_8D;
			}
			case 1:
				goto IL_190;
			case 2:
				if (true)
				{
				}
				goto IL_190;
			case 4:
				goto IL_88;
			case 5:
				goto IL_8D;
			case 6:
				if (A_1 < 0)
				{
					num3 = 12;
					continue;
				}
				this.ᜁ = null;
				this.ᜀ = null;
				num3 = 7;
				continue;
			case 7:
			{
				if (A_1 == 0)
				{
					num3 = 9;
					continue;
				}
				int num2 = (A_1 - 4) / 10;
				this.ᜀ = new uint[num2 + 1];
				this.ᜁ = new spr\u2243[num2];
				int num4 = (num2 + 1) * 4;
				byte[] array = new byte[num4];
				A_0.Read(array, 0, num4);
				Buffer.BlockCopy(array, 0, this.ᜀ, 0, num4);
				num3 = 0;
				continue;
			}
			case 8:
			{
				byte[] array = new byte[6];
				num3 = 5;
				continue;
			}
			case 9:
				return;
			case 10:
				return;
			case 11:
				goto IL_19C;
			case 12:
				goto IL_133;
			}
			if (A_0 == null)
			{
				num3 = 4;
				continue;
			}
			num3 = 6;
			continue;
			IL_8D:
			int num = 0;
			num3 = 2;
			continue;
			IL_190:
			num3 = 11;
		}
		IL_88:
		throw new ArgumentNullException(ClipboardData.b("୷๹๻᭽", a_));
		IL_133:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᅷ㥹፻୽", a_));
	}

	// Token: 0x06003395 RID: 13205 RVA: 0x002F5A78 File Offset: 0x002F4A78
	internal uint[] ᜁ()
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

	// Token: 0x06003396 RID: 13206 RVA: 0x002F5ABC File Offset: 0x002F4ABC
	internal spr\u2243[] ᜀ()
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

	// Token: 0x06003397 RID: 13207 RVA: 0x002F5B00 File Offset: 0x002F4B00
	internal override int ᜇ()
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return num;
				case 1:
					num += 6 * this.ᜁ.Length;
					num2 = 0;
					continue;
				case 2:
					num += this.ᜀ.Length + 4;
					num2 = 3;
					continue;
				case 3:
					goto IL_5D;
				case 4:
					if (this.ᜀ != null)
					{
						num2 = 2;
						continue;
					}
					goto IL_5D;
				case 5:
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
						if (this.ᜁ != null)
						{
							num2 = 1;
							continue;
						}
						return num;
					}
					break;
				}
				break;
				IL_5D:
				num2 = 5;
			}
		}
		return num;
	}

	// Token: 0x0400280C RID: 10252
	private new uint[] ᜀ;

	// Token: 0x0400280D RID: 10253
	private new spr\u2243[] ᜁ;
}
