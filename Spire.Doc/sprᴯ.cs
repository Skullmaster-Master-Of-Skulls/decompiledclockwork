using System;
using System.IO;
using Spire.Doc;
using Spire.Doc.Core.Escher;

// Token: 0x0200038C RID: 908
internal class spr\u1D2F : spr\u23F8
{
	// Token: 0x06003287 RID: 12935 RVA: 0x002E7DEC File Offset: 0x002E6DEC
	internal int ᜂ()
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

	// Token: 0x06003288 RID: 12936 RVA: 0x002E7E30 File Offset: 0x002E6E30
	internal void ᜁ(int A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06003289 RID: 12937 RVA: 0x002E7E74 File Offset: 0x002E6E74
	internal bool ᜀ()
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
		return this.ᜁ == 15;
	}

	// Token: 0x0600328A RID: 12938 RVA: 0x002E7EBC File Offset: 0x002E6EBC
	internal void ᜀ(bool A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜁ = 15;
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
					num = 2;
					continue;
				}
				break;
			case 2:
				return;
			}
			IL_1C:
			if (A_0)
			{
				num = 1;
				continue;
			}
			break;
			goto IL_1C;
		}
	}

	// Token: 0x0600328B RID: 12939 RVA: 0x002E7F34 File Offset: 0x002E6F34
	internal new int ᜇ()
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

	// Token: 0x0600328C RID: 12940 RVA: 0x002E7F78 File Offset: 0x002E6F78
	internal void ᜀ(int A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x0600328D RID: 12941 RVA: 0x002E7FBC File Offset: 0x002E6FBC
	internal MSOFBT ᜅ()
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
		return (MSOFBT)this.ᜃ;
	}

	// Token: 0x0600328E RID: 12942 RVA: 0x002E8000 File Offset: 0x002E7000
	internal void ᜀ(MSOFBT A_0)
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
		this.ᜃ = (int)A_0;
	}

	// Token: 0x0600328F RID: 12943 RVA: 0x002E8044 File Offset: 0x002E7044
	internal int ᜁ()
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

	// Token: 0x06003290 RID: 12944 RVA: 0x002E8088 File Offset: 0x002E7088
	internal void ᜂ(int A_0)
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

	// Token: 0x06003291 RID: 12945 RVA: 0x002E80CC File Offset: 0x002E70CC
	internal spr\u1D2F(Document A_0)
	{
		this.ᜅ = A_0;
	}

	// Token: 0x06003292 RID: 12946 RVA: 0x002E80E8 File Offset: 0x002E70E8
	internal spr\u1D2F(Stream A_0, Document A_1)
	{
		this.ᜅ = A_1;
		this.ᜁ(A_0);
	}

	// Token: 0x06003293 RID: 12947 RVA: 0x002E810C File Offset: 0x002E710C
	internal void ᜁ(Stream A_0)
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
		int num = spr\u23F8.ᜁ(A_0);
		this.ᜁ = (num & 15);
		this.ᜂ = (num & 65520) >> 4;
		this.ᜃ = (int)(((long)num & (long)((ulong)-65536)) >> 16);
		this.ᜄ = spr\u23F8.ᜁ(A_0);
	}

	// Token: 0x06003294 RID: 12948 RVA: 0x002E8188 File Offset: 0x002E7188
	internal void ᜀ(Stream A_0)
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
		num |= this.ᜁ;
		num |= this.ᜂ << 4;
		num |= this.ᜃ << 16;
		spr\u23F8.ᜁ(A_0, num);
		spr\u23F8.ᜁ(A_0, this.ᜄ);
	}

	// Token: 0x06003295 RID: 12949 RVA: 0x002E81F8 File Offset: 0x002E71F8
	internal spr\u2192 ᜄ()
	{
		for (;;)
		{
			MSOFBT msofbt = this.ᜅ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (msofbt)
					{
					case MSOFBT.msofbtDggContainer:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_261;
						default:
							goto IL_1D2;
						}
						break;
					case MSOFBT.msofbtBstoreContainer:
						goto IL_1B0;
					case MSOFBT.msofbtDgContainer:
						goto IL_EF;
					case MSOFBT.msofbtSpgrContainer:
						goto IL_15A;
					case MSOFBT.msofbtSpContainer:
						goto IL_236;
					case MSOFBT.msofbtSolverContainer:
						goto IL_1E4;
					case MSOFBT.msofbtDgg:
						goto IL_17E;
					case MSOFBT.msofbtBSE:
						goto IL_214;
					case MSOFBT.msofbtDg:
						goto IL_208;
					case MSOFBT.msofbtSpgr:
						goto IL_27D;
					case MSOFBT.msofbtSp:
						goto IL_14E;
					case MSOFBT.msofbtOPT:
						goto IL_E3;
					case MSOFBT.msofbtTextbox:
					case MSOFBT.msofbtAnchor:
					case MSOFBT.msofbtConnectorRule:
					case MSOFBT.msofbtAlignRule:
					case MSOFBT.msofbtArcRule:
					case MSOFBT.msofbtClientRule:
					case MSOFBT.msofbtCLSID:
					case MSOFBT.msofbtCalloutRule:
					case MSOFBT.msofbtBlipFirst:
					case (MSOFBT)61465:
					case (MSOFBT)61468:
						goto IL_12B;
					case MSOFBT.msofbtClientTextbox:
						goto IL_220;
					case MSOFBT.msofbtChildAnchor:
						goto IL_271;
					case MSOFBT.msofbtClientAnchor:
						goto IL_289;
					case MSOFBT.msofbtClientData:
						goto IL_11F;
					case MSOFBT.msofbtBlipEMF:
					case MSOFBT.msofbtBlipWMF:
						goto IL_D7;
					case MSOFBT.msofbtBlipJPEG:
					case MSOFBT.msofbtBlipPNG:
					case MSOFBT.msofbtBlipDIB:
						goto IL_1FC;
					default:
						num = 3;
						continue;
					}
					break;
				case 1:
					if (msofbt != MSOFBT.msofbtREGROUPItems)
					{
						num = 2;
						continue;
					}
					goto IL_1F0;
				case 2:
					num = 8;
					continue;
				case 3:
					num = 1;
					continue;
				case 4:
					num = 5;
					continue;
				case 5:
					if (true)
					{
					}
					goto IL_12B;
				case 6:
					if (this.ᜀ())
					{
						num = 7;
						continue;
					}
					goto IL_295;
				case 7:
					goto IL_14C;
				case 8:
					switch (msofbt)
					{
					case MSOFBT.msofbtSecondaryFOPT:
						goto IL_FB;
					case MSOFBT.msofbtTertiaryFOPT:
						goto IL_166;
					}
					goto IL_261;
				}
				break;
				IL_12B:
				num = 6;
				continue;
				IL_261:
				num = 4;
			}
		}
		IL_D7:
		return new sprᲱ(this.ᜅ);
		IL_E3:
		return new spr\u22B7(this.ᜅ);
		IL_EF:
		return new spr\u1DB9(this.ᜅ);
		IL_FB:
		return new spr\u257B(this.ᜅ);
		IL_11F:
		return new spr\u20AA(this.ᜅ);
		IL_14C:
		return new spr\u2542(this.ᜅ);
		IL_14E:
		return new spr\u2402(this.ᜅ);
		IL_15A:
		return new sprᥙ(this.ᜅ);
		IL_166:
		return new spr\u228C(this.ᜅ);
		IL_17E:
		return new spr\u1BEE(this.ᜅ);
		IL_1B0:
		return new spr\u2568(this.ᜅ);
		IL_1D2:
		if (false)
		{
		}
		return new spr\u2403(this.ᜅ);
		IL_1E4:
		return new spr\u22A2(this.ᜅ);
		IL_1F0:
		return new sprᩉ(this.ᜅ);
		IL_1FC:
		return new sprᱪ(this.ᜅ);
		IL_208:
		return new spr\u2365(this.ᜅ);
		IL_214:
		return new sprΏ(this.ᜅ);
		IL_220:
		return new sprᥥ(this.ᜅ);
		IL_236:
		return new spr\u2459(this.ᜅ);
		IL_271:
		return new sprᧁ(this.ᜅ);
		IL_27D:
		return new spr\u2379(this.ᜅ);
		IL_289:
		return new sprᣯ(this.ᜅ);
		IL_295:
		return new sprᩉ(this.ᜅ);
	}

	// Token: 0x06003296 RID: 12950 RVA: 0x002E84A8 File Offset: 0x002E74A8
	internal spr\u1D2F ᜆ()
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
		spr\u1D2F spr_u1D2F = (spr\u1D2F)base.MemberwiseClone();
		spr_u1D2F.ᜅ = this.ᜅ;
		return spr_u1D2F;
	}

	// Token: 0x06003297 RID: 12951 RVA: 0x002E84FC File Offset: 0x002E74FC
	internal static spr\u2192 ᜀ(Stream A_0, Document A_1)
	{
		spr\u2192 spr_u;
		for (;;)
		{
			spr\u1D2F spr_u1D2F = new spr\u1D2F(A_0, A_1);
			spr_u = spr_u1D2F.ᜄ();
			if (!spr_u.ᜀ(spr_u1D2F, A_0))
			{
				break;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			goto Block_1;
		}
		return null;
		Block_1:
		if (false)
		{
		}
		return spr_u;
	}

	// Token: 0x040027F3 RID: 10227
	internal new const int ᜀ = 15;

	// Token: 0x040027F4 RID: 10228
	private new int ᜁ;

	// Token: 0x040027F5 RID: 10229
	private new int ᜂ;

	// Token: 0x040027F6 RID: 10230
	private new int ᜃ;

	// Token: 0x040027F7 RID: 10231
	private new int ᜄ;

	// Token: 0x040027F8 RID: 10232
	internal new Document ᜅ;
}
