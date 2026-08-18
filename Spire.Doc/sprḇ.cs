using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Spire.Doc.Core.Escher;

// Token: 0x020001A7 RID: 423
internal class sprḇ : spr\u23F8
{
	// Token: 0x06001045 RID: 4165 RVA: 0x000F8CB8 File Offset: 0x000F7CB8
	public sprḇ()
	{
		this.ᜀ = new spr\u1D43();
		this.ᜁ = new spr\u1DB8();
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x000F8CE4 File Offset: 0x000F7CE4
	internal spr\u1D43 ᜀ()
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

	// Token: 0x06001047 RID: 4167 RVA: 0x000F8D28 File Offset: 0x000F7D28
	internal void ᜀ(spr\u1D43 A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x000F8D6C File Offset: 0x000F7D6C
	public Image ᜁ()
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

	// Token: 0x06001049 RID: 4169 RVA: 0x000F8DB0 File Offset: 0x000F7DB0
	public void ᜀ(Stream A_0)
	{
		spr\u224B spr_u224B;
		bool a_;
		for (;;)
		{
			this.ᜀ.ᜁ(A_0);
			spr_u224B = new spr\u224B();
			spr_u224B.ᜁ(A_0);
			a_ = false;
			MSOBlipType msoblipType = (MSOBlipType)(spr_u224B.ᜂ() - 61464);
			int num = 18;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_19A;
				case 1:
					if ((spr_u224B.ᜁ() ^ 1760U) == 1U)
					{
						num = 12;
						continue;
					}
					goto IL_12B;
				case 2:
					goto IL_146;
				case 3:
					goto IL_141;
				case 4:
					a_ = true;
					num = 7;
					continue;
				case 5:
					a_ = true;
					num = 2;
					continue;
				case 6:
					num = 20;
					continue;
				case 7:
					goto IL_170;
				case 8:
					goto IL_22B;
				case 9:
					goto IL_15C;
				case 10:
					if ((spr_u224B.ᜁ() ^ 1130U) == 1U)
					{
						num = 15;
						continue;
					}
					goto IL_22B;
				case 11:
					goto IL_226;
				case 12:
					a_ = true;
					num = 26;
					continue;
				case 13:
					a_ = true;
					num = 19;
					continue;
				case 14:
					goto IL_1B0;
				case 15:
					a_ = true;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_136;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 16:
					a_ = true;
					num = 0;
					continue;
				case 17:
					if ((spr_u224B.ᜁ() ^ 1960U) == 1U)
					{
						if (true)
						{
						}
						num = 16;
						continue;
					}
					goto IL_19A;
				case 18:
					switch (msoblipType)
					{
					case MSOBlipType.msoblipEMF:
						num = 22;
						continue;
					case MSOBlipType.msoblipWMF:
						num = 21;
						continue;
					case MSOBlipType.msoblipPICT:
						num = 24;
						continue;
					case MSOBlipType.msoblipJPEG:
						num = 10;
						continue;
					case MSOBlipType.msoblipPNG:
						num = 1;
						continue;
					case MSOBlipType.msoblipDIB:
						num = 17;
						continue;
					default:
						num = 6;
						continue;
					}
					break;
				case 19:
					goto IL_210;
				case 20:
					goto IL_251;
				case 21:
					if ((spr_u224B.ᜁ() ^ 534U) == 1U)
					{
						num = 4;
						continue;
					}
					goto IL_170;
				case 22:
					if ((spr_u224B.ᜁ() ^ 980U) == 1U)
					{
						num = 5;
						continue;
					}
					goto IL_146;
				case 23:
					goto IL_186;
				case 24:
					if ((spr_u224B.ᜁ() ^ 1346U) == 1U)
					{
						num = 13;
						continue;
					}
					goto IL_210;
				case 25:
					goto IL_241;
				case 26:
					goto IL_12B;
				}
				break;
				IL_136:
				num = 3;
				continue;
				IL_12B:
				this.ᜃ = new spr\u1DB8();
				goto IL_136;
				IL_146:
				this.ᜃ = new spr\u21D2();
				num = 9;
				continue;
				IL_170:
				this.ᜃ = new spr\u21D2();
				num = 23;
				continue;
				IL_19A:
				this.ᜃ = new spr\u1DB8();
				num = 14;
				continue;
				IL_210:
				this.ᜃ = new spr\u21D2();
				num = 11;
				continue;
				IL_22B:
				this.ᜃ = new spr\u1DB8();
				num = 25;
			}
		}
		IL_141:
		IL_15C:
		IL_186:
		IL_1B0:
		IL_226:
		IL_241:
		IL_251:
		this.ᜂ = this.ᜃ.ᜀ(A_0, (int)spr_u224B.ᜇ(), a_);
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x000F90EC File Offset: 0x000F80EC
	internal void ᜀ(Stream A_0, MemoryStream A_1, byte[] A_2, Image A_3)
	{
		if (A_3 is Metafile)
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
				this.ᜃ = new spr\u21D2();
				(this.ᜃ as spr\u21D2).ᜀ(A_3 as Metafile);
				this.ᜃ.ᜀ(A_0, A_1, MSOBlipType.msoblipEMF, A_2);
				return;
			}
		}
		this.ᜃ = new spr\u1DB8();
		this.ᜃ.ᜀ(A_0, A_1, MSOBlipType.msoblipPNG, A_2);
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x000F9180 File Offset: 0x000F8180
	internal override void \u170D()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜃ.\u170D();
				num = 5;
				continue;
			case 1:
				if (this.ᜃ != null)
				{
					num = 0;
					continue;
				}
				return;
			case 2:
				goto IL_7D;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A9;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 4:
				this.ᜂ.Dispose();
				this.ᜂ = null;
				goto IL_A9;
			case 5:
				return;
			}
			if (true)
			{
			}
			if (this.ᜂ != null)
			{
				num = 4;
				continue;
			}
			IL_7D:
			num = 1;
			continue;
			IL_A9:
			num = 2;
		}
	}

	// Token: 0x040017AB RID: 6059
	private new spr\u1D43 ᜀ;

	// Token: 0x040017AC RID: 6060
	private new spr\u1DB8 ᜁ;

	// Token: 0x040017AD RID: 6061
	private new Image ᜂ;

	// Token: 0x040017AE RID: 6062
	private new spr\u2096 ᜃ;
}
