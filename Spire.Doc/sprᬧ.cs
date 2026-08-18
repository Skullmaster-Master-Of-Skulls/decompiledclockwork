using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Core.Escher;

// Token: 0x02000204 RID: 516
internal class sprᬧ : spr\u23F8
{
	// Token: 0x0600183D RID: 6205 RVA: 0x001755FC File Offset: 0x001745FC
	public sprᬧ()
	{
		this.ᜅ = new List<sprἾ>();
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x0017561C File Offset: 0x0017461C
	public spr\u1ADA ᜂ()
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
		return this.ᜆ;
	}

	// Token: 0x0600183F RID: 6207 RVA: 0x00175660 File Offset: 0x00174660
	public new int ᜇ()
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
		int num = 0;
		num += 16;
		num += this.ᜁ();
		return num + 12;
	}

	// Token: 0x06001840 RID: 6208 RVA: 0x001756B4 File Offset: 0x001746B4
	public void ᜀ(Stream A_0, uint A_1)
	{
		for (;;)
		{
			IL_4C:
			this.ᜇ = new List<spr\u224B>();
			uint num = (uint)A_0.Position + A_1;
			int num2 = 0;
			for (;;)
			{
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
					spr\u224B spr_u224B;
					switch (num2)
					{
					case 0:
						goto IL_B7;
					case 1:
					{
						if (A_0.Position >= (long)((ulong)num))
						{
							num2 = 5;
							continue;
						}
						spr_u224B = new spr\u224B();
						spr_u224B.ᜁ(A_0);
						this.ᜇ.Add(spr_u224B);
						MSOFBT msofbt = spr_u224B.ᜂ();
						num2 = 2;
						continue;
					}
					case 2:
					{
						MSOFBT msofbt;
						switch (msofbt)
						{
						case MSOFBT.msofbtSp:
							this.ᜄ = new spr\u19DA();
							this.ᜄ.ᜁ(A_0);
							num2 = 6;
							continue;
						case MSOFBT.msofbtOPT:
							this.ᜀ(A_0, spr_u224B);
							num2 = 4;
							continue;
						case MSOFBT.msofbtTextbox:
						case MSOFBT.msofbtClientTextbox:
							goto IL_142;
						case MSOFBT.msofbtAnchor:
						case MSOFBT.msofbtChildAnchor:
						case MSOFBT.msofbtClientAnchor:
							this.ᜆ = new spr\u1ADA();
							this.ᜆ.ᜁ(A_0);
							num2 = 7;
							continue;
						default:
							num2 = 9;
							continue;
						}
						break;
					}
					case 3:
						goto IL_B7;
					case 4:
						goto IL_B7;
					case 5:
						return;
					case 6:
						goto IL_19A;
					case 7:
						goto IL_B7;
					case 8:
						goto IL_142;
					case 9:
						num2 = 8;
						continue;
					}
					goto IL_4C;
					IL_142:
					A_0.Position += (long)((ulong)spr_u224B.ᜇ());
					if (true)
					{
					}
					num2 = 3;
					continue;
				}
				}
				IL_B7:
				num2 = 1;
				continue;
				IL_19A:
				goto IL_B7;
			}
		}
	}

	// Token: 0x06001841 RID: 6209 RVA: 0x00175860 File Offset: 0x00174860
	public void ᜂ(Stream A_0)
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
		this.ᜀ();
		spr\u224B spr_u224B = new spr\u224B();
		spr_u224B.ᜀ(MSOFBT.msofbtSpContainer);
		spr_u224B.ᜁ(0U);
		spr_u224B.ᜀ((uint)this.ᜇ());
		spr_u224B.ᜂ(15U);
		spr_u224B.ᜀ(A_0);
		this.ᜀ(A_0);
		this.ᜁ(A_0);
		this.ᜆ = new spr\u1ADA();
		this.ᜆ.ᜂ(120);
		spr_u224B = new spr\u224B();
		spr_u224B.ᜀ(MSOFBT.msofbtClientAnchor);
		spr_u224B.ᜀ(4U);
		spr_u224B.ᜁ(0U);
		spr_u224B.ᜂ(0U);
		spr_u224B.ᜀ(A_0);
		this.ᜆ.ᜀ(A_0);
	}

	// Token: 0x06001842 RID: 6210 RVA: 0x00175934 File Offset: 0x00174934
	private void ᜀ(Stream A_0, spr\u224B A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_4B:
				int num;
				int num2;
				int num3;
				int num4;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_C3:
					goto IL_182;
				default:
					if (false)
					{
					}
					num = (int)(A_1.ᜇ() / 6U);
					num2 = 0;
					num3 = 0;
					num4 = 2;
					break;
				}
				int num5;
				for (;;)
				{
					IL_10:
					switch (num4)
					{
					case 0:
						if (num5 >= this.ᜅ.Count)
						{
							num4 = 5;
							continue;
						}
						num4 = 1;
						continue;
					case 1:
						if (this.ᜅ[num5].ᜀ())
						{
							num4 = 6;
							continue;
						}
						goto IL_182;
					case 2:
						goto IL_F9;
					case 3:
						num4 = 4;
						continue;
					case 4:
					{
						if ((long)num2 >= (long)((ulong)A_1.ᜇ()))
						{
							num4 = 12;
							continue;
						}
						sprἾ sprἾ = new sprἾ();
						num2 += sprἾ.ᜁ(A_0);
						this.ᜅ.Add(sprἾ);
						num3++;
						num4 = 7;
						continue;
					}
					case 5:
						return;
					case 6:
						A_0.Read(this.ᜅ[num5].ᜄ(), 0, this.ᜅ[num5].ᜄ().Length);
						num4 = 11;
						continue;
					case 7:
						if (true)
						{
						}
						goto IL_F9;
					case 8:
						if (num3 < num)
						{
							num4 = 3;
							continue;
						}
						goto IL_196;
					case 9:
						goto IL_11D;
					case 10:
						goto IL_11D;
					case 11:
						goto IL_C3;
					case 12:
						goto IL_196;
					}
					goto IL_4B;
					IL_F9:
					num4 = 8;
					continue;
					IL_11D:
					num4 = 0;
					continue;
					IL_196:
					num5 = 0;
					num4 = 9;
				}
				IL_182:
				num5++;
				num4 = 10;
				goto IL_10;
			}
			return;
		}
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x00175B10 File Offset: 0x00174B10
	private void ᜁ(Stream A_0)
	{
		for (;;)
		{
			spr\u224B spr_u224B = new spr\u224B();
			spr_u224B.ᜀ(MSOFBT.msofbtOPT);
			spr_u224B.ᜀ((uint)this.ᜁ());
			spr_u224B.ᜁ(4U);
			spr_u224B.ᜂ(3U);
			spr_u224B.ᜀ(A_0);
			int num = 0;
			int num2 = 10;
			for (;;)
			{
				int num3;
				switch (num2)
				{
				case 0:
					if (num3 >= this.ᜅ.Count)
					{
						num2 = 7;
						continue;
					}
					num2 = 9;
					continue;
				case 1:
					goto IL_E1;
				case 2:
					goto IL_E3;
				case 3:
					goto IL_113;
				case 4:
					goto IL_113;
				case 5:
					num3 = 0;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E1;
					default:
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				case 6:
					goto IL_15C;
				case 7:
					return;
				case 8:
					if (num >= this.ᜅ.Count)
					{
						num2 = 5;
						continue;
					}
					this.ᜅ[num].ᜀ(A_0);
					num++;
					num2 = 2;
					continue;
				case 9:
					if (this.ᜅ[num3].ᜀ())
					{
						if (true)
						{
						}
						num2 = 1;
						continue;
					}
					goto IL_15C;
				case 10:
					goto IL_E3;
				}
				break;
				IL_E1:
				A_0.Write(this.ᜅ[num3].ᜄ(), 0, this.ᜅ[num3].ᜄ().Length);
				num2 = 6;
				continue;
				IL_E3:
				num2 = 8;
				continue;
				IL_113:
				num2 = 0;
				continue;
				IL_15C:
				num3++;
				num2 = 4;
			}
		}
	}

	// Token: 0x06001844 RID: 6212 RVA: 0x00175CB8 File Offset: 0x00174CB8
	private int ᜁ()
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 1;
			for (;;)
			{
				int num3;
				switch (num2)
				{
				case 0:
					if (this.ᜅ[num3].ᜀ())
					{
						num2 = 4;
						continue;
					}
					num += 6;
					num2 = 9;
					continue;
				case 1:
					if (this.ᜅ != null)
					{
						num2 = 3;
						continue;
					}
					return num;
				case 2:
					goto IL_73;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7F;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num += 8;
						num3 = 0;
						num2 = 5;
						continue;
					}
					break;
				case 4:
					num += (int)(this.ᜅ[num3].ᜂ() + 6U);
					num2 = 2;
					continue;
				case 5:
					goto IL_C1;
				case 6:
					return num;
				case 7:
					if (num3 >= this.ᜅ.Count)
					{
						num2 = 6;
						continue;
					}
					num2 = 0;
					continue;
				case 8:
					goto IL_7F;
				case 9:
					goto IL_73;
				}
				break;
				IL_73:
				num3++;
				num2 = 8;
				continue;
				IL_C1:
				num2 = 7;
				continue;
				IL_7F:
				goto IL_C1;
			}
		}
		return num;
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x00175DF0 File Offset: 0x00174DF0
	private void ᜀ()
	{
		int a_ = 5;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜅ = new List<sprἾ>();
		sprἾ sprἾ = new sprἾ();
		sprἾ.ᜀ(true);
		sprἾ.ᜁ(false);
		sprἾ.ᜀ(260);
		sprἾ.ᜀ(1U);
		this.ᜅ.Add(sprἾ);
		sprἾ = new sprἾ();
		sprἾ.ᜀ(true);
		sprἾ.ᜁ(true);
		sprἾ.ᜀ(261);
		sprἾ.ᜀ(Encoding.Unicode.GetBytes(ClipboardData.b("੪ᡬ᭮ṰѲᑴ᭶ᕸࡺ≼ൾ\udc82뒄낆覈", a_)));
		sprἾ.ᜀ((uint)sprἾ.ᜄ().Length);
		this.ᜅ.Add(sprἾ);
		sprἾ = new sprἾ();
		sprἾ.ᜀ(false);
		sprἾ.ᜁ(false);
		sprἾ.ᜀ(262);
		sprἾ.ᜀ(2U);
		this.ᜅ.Add(sprἾ);
		sprἾ = new sprἾ();
		sprἾ.ᜀ(false);
		sprἾ.ᜁ(false);
		sprἾ.ᜀ(511);
		sprἾ.ᜀ(524288U);
		this.ᜅ.Add(sprἾ);
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x00175F30 File Offset: 0x00174F30
	private void ᜀ(Stream A_0)
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
		spr\u224B spr_u224B = new spr\u224B();
		spr_u224B.ᜀ(MSOFBT.msofbtSp);
		spr_u224B.ᜁ(75U);
		spr_u224B.ᜂ(2U);
		spr_u224B.ᜀ(8U);
		spr_u224B.ᜀ(A_0);
		spr\u19DA spr_u19DA = new spr\u19DA();
		spr_u19DA.ᜁ(1026U);
		spr_u19DA.ᜀ(2560U);
		spr_u19DA.ᜀ(A_0);
	}

	// Token: 0x04001B47 RID: 6983
	private new const int ᜀ = 8;

	// Token: 0x04001B48 RID: 6984
	private new const int ᜁ = 8;

	// Token: 0x04001B49 RID: 6985
	private new const int ᜂ = 6;

	// Token: 0x04001B4A RID: 6986
	private new const int ᜃ = 4;

	// Token: 0x04001B4B RID: 6987
	private new spr\u19DA ᜄ;

	// Token: 0x04001B4C RID: 6988
	private new List<sprἾ> ᜅ;

	// Token: 0x04001B4D RID: 6989
	private spr\u1ADA ᜆ;

	// Token: 0x04001B4E RID: 6990
	private List<spr\u224B> ᜇ;
}
