using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002C8 RID: 712
internal class sprណ
{
	// Token: 0x06002B2C RID: 11052 RVA: 0x00180A10 File Offset: 0x0017FA10
	public List<spr\u22A9> ᜀ()
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

	// Token: 0x06002B2D RID: 11053 RVA: 0x00180A54 File Offset: 0x0017FA54
	public sprណ()
	{
		this.ᜂ = -1;
		this.ᜃ = new List<spr\u22A9>();
		base..ctor();
	}

	// Token: 0x06002B2E RID: 11054 RVA: 0x00180A7C File Offset: 0x0017FA7C
	public sprណ(Stream A_0)
	{
		int a_ = 9;
		this.ᜂ = -1;
		this.ᜃ = new List<spr\u22A9>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾㕀ㅂ⁄♆⑈", a_));
		}
		A_0.Position = 0L;
		this.ᜂ(A_0);
		this.ᜃ(A_0);
	}

	// Token: 0x06002B2F RID: 11055 RVA: 0x00180ADC File Offset: 0x0017FADC
	private void ᜃ(Stream A_0)
	{
		for (;;)
		{
			for (;;)
			{
				int num = 0;
				int count = this.ᜃ.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						spr\u22A9 spr_u22A = this.ᜃ[num];
						spr_u22A.ᜂ(A_0);
						num++;
						num2 = 2;
						continue;
					}
					case 1:
						goto IL_4C;
					case 2:
						goto IL_38;
					case 3:
						if (true)
						{
						}
						goto IL_38;
					}
					break;
					IL_38:
					num2 = 0;
				}
			}
			IL_4C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_90;
			}
		}
		IL_90:
		if (false)
		{
		}
	}

	// Token: 0x06002B30 RID: 11056 RVA: 0x00180B80 File Offset: 0x0017FB80
	private void ᜂ(Stream A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte[] array = new byte[16];
				A_0.Read(array, 0, 4);
				int num = BitConverter.ToInt32(array, 0);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int num3;
						int num4;
						if (num3 >= num4)
						{
							num2 = 3;
							continue;
						}
						A_0.Read(array, 0, 16);
						Guid a_ = new Guid(array);
						int a_2 = spr\u23D6.ᜁ(A_0, array);
						this.ᜃ.Add(new spr\u22A9(a_, a_2));
						num3++;
						num2 = 4;
						continue;
					}
					case 1:
						goto IL_6D;
					case 2:
					{
						if (num != 65534)
						{
							num2 = 1;
							continue;
						}
						A_0.Read(array, 0, 2);
						A_0.Read(array, 0, 2);
						A_0.Read(array, 0, 16);
						A_0.Read(array, 0, 4);
						int num4 = BitConverter.ToInt32(array, 0);
						int num3 = 0;
						num2 = 5;
						continue;
					}
					case 3:
						return;
					case 4:
						goto IL_117;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6D;
						default:
							if (false)
							{
							}
							goto IL_117;
						}
						break;
					}
					break;
					IL_117:
					if (true)
					{
					}
					num2 = 0;
				}
			}
			IL_6D:
			throw new IOException();
		}
	}

	// Token: 0x06002B31 RID: 11057 RVA: 0x00180CD0 File Offset: 0x0017FCD0
	private void ᜁ(Stream A_0)
	{
		for (;;)
		{
			for (;;)
			{
				int num = 0;
				int count = this.ᜃ.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_30;
					case 1:
						goto IL_30;
					case 2:
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						this.ᜃ[num].ᜁ(A_0);
						num++;
						num2 = 0;
						continue;
					case 3:
						goto IL_44;
					}
					break;
					IL_30:
					num2 = 2;
				}
			}
			IL_44:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_86;
			}
		}
		IL_86:
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x06002B32 RID: 11058 RVA: 0x00180D74 File Offset: 0x0017FD74
	private void ᜀ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			int num4;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_8F:
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_C6;
					case 1:
					{
						int count = this.ᜃ.Count;
						spr\u23D6.ᜂ(A_0, count);
						List<long> list = new List<long>();
						int num2 = 0;
						num = 11;
						continue;
					}
					case 2:
					{
						int num3 = 0;
						num = 6;
						continue;
					}
					case 3:
						goto IL_C6;
					case 4:
					{
						int count;
						int num3;
						if (num3 >= count)
						{
							num = 5;
							continue;
						}
						spr\u22A9 spr_u22A = this.ᜃ[num3];
						long position = A_0.Position;
						List<long> list;
						A_0.Position = list[num3];
						spr\u23D6.ᜂ(A_0, (int)position);
						A_0.Position = position;
						spr_u22A.ᜁ(A_0);
						num3++;
						num = 10;
						continue;
					}
					case 5:
						return;
					case 6:
						goto IL_E8;
					case 7:
						goto IL_A8;
					case 8:
						if (num4 >= 16)
						{
							num = 1;
							continue;
						}
						A_0.WriteByte(0);
						num4++;
						if (true)
						{
						}
						num = 0;
						continue;
					case 9:
					{
						int count;
						int num2;
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						spr\u22A9 spr_u22A2 = this.ᜃ[num2];
						byte[] array = spr_u22A2.ᜃ().ToByteArray();
						A_0.Write(array, 0, array.Length);
						List<long> list;
						list.Add(A_0.Position);
						spr\u23D6.ᜂ(A_0, 0);
						num2++;
						num = 7;
						continue;
					}
					case 10:
						goto IL_E8;
					case 11:
						goto IL_A8;
					}
					goto IL_6D;
					IL_A8:
					num = 9;
					continue;
					IL_C6:
					num = 8;
					continue;
					IL_E8:
					num = 4;
				}
				return;
			}
			default:
				if (false)
				{
				}
				break;
			}
			IL_6D:
			spr\u23D6.ᜂ(A_0, 65534);
			spr\u23D6.ᜀ(A_0, 261);
			spr\u23D6.ᜀ(A_0, 2);
			num4 = 0;
			goto IL_8F;
		}
		}
	}

	// Token: 0x06002B33 RID: 11059 RVA: 0x00180F88 File Offset: 0x0017FF88
	public void ᜄ(Stream A_0)
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
		this.ᜀ(A_0);
	}

	// Token: 0x06002B34 RID: 11060 RVA: 0x00180FCC File Offset: 0x0017FFCC
	// Note: this type is marked as 'beforefieldinit'.
	static sprណ()
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprណ.ᜁ = new Guid(RecordTableEnumerator.b("❀煂籄ⅆ煈繊⡌罎籐杒㍔ㅖ恘癚汜潞坠孢䡤٦୨剪屬䉮䅰䭲䕴䝶䭸᥺佼䡾낂뺆", a_));
	}

	// Token: 0x04001439 RID: 5177
	private const int ᜀ = 65534;

	// Token: 0x0400143A RID: 5178
	private static readonly Guid ᜁ;

	// Token: 0x0400143B RID: 5179
	private int ᜂ;

	// Token: 0x0400143C RID: 5180
	private List<spr\u22A9> ᜃ;
}
