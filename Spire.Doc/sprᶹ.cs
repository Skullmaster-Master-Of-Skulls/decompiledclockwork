using System;
using Spire.Doc;
using Spire.Doc.Core.DataStreamParser.Escher;
using Spire.Doc.Core.Escher;

// Token: 0x02000263 RID: 611
internal class spr\u1DB9 : spr\u2542
{
	// Token: 0x06002009 RID: 8201 RVA: 0x0021F54C File Offset: 0x0021E54C
	internal spr\u2365 ᜀ()
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
		return base.ᜀ(typeof(spr\u2365)) as spr\u2365;
	}

	// Token: 0x0600200A RID: 8202 RVA: 0x0021F59C File Offset: 0x0021E59C
	internal new sprᥙ ᜁ()
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
		return base.ᜀ(typeof(sprᥙ)) as sprᥙ;
	}

	// Token: 0x0600200B RID: 8203 RVA: 0x0021F5EC File Offset: 0x0021E5EC
	internal ShapeDocType ᜄ()
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

	// Token: 0x0600200C RID: 8204 RVA: 0x0021F630 File Offset: 0x0021E630
	internal void ᜀ(ShapeDocType A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600200D RID: 8205 RVA: 0x0021F674 File Offset: 0x0021E674
	internal spr\u1DB9(Document A_0) : base(MSOFBT.msofbtDgContainer, A_0)
	{
	}

	// Token: 0x0600200E RID: 8206 RVA: 0x0021F690 File Offset: 0x0021E690
	private static void ᜀ(spr\u2542 A_0, ref int A_1, ref int A_2)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				spr\u2192 spr_u;
				switch (num2)
				{
				case 0:
					goto IL_62;
				case 1:
					goto IL_4A;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_62;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						if (num >= A_0.\u1714().Count)
						{
							num2 = 6;
							continue;
						}
						spr_u = (A_0.\u1714()[num] as spr\u2192);
						num2 = 4;
						continue;
					}
					break;
				case 3:
					goto IL_96;
				case 4:
					if (spr_u is spr\u2402)
					{
						num2 = 5;
						continue;
					}
					goto IL_4A;
				case 5:
				{
					spr\u2402 spr_u2 = spr_u as spr\u2402;
					A_2 = Math.Max(A_2, spr_u2.ᜀ());
					A_1++;
					num2 = 1;
					continue;
				}
				case 6:
					return;
				case 7:
					if (spr_u is spr\u2542)
					{
						num2 = 0;
						continue;
					}
					goto IL_3C;
				case 8:
					goto IL_3C;
				case 9:
					goto IL_96;
				}
				break;
				IL_3C:
				num++;
				num2 = 9;
				continue;
				IL_4A:
				num2 = 7;
				continue;
				IL_62:
				spr\u1DB9.ᜀ(spr_u as spr\u2542, ref A_1, ref A_2);
				num2 = 8;
				continue;
				IL_96:
				num2 = 2;
			}
		}
	}

	// Token: 0x0600200F RID: 8207 RVA: 0x0021F7D0 File Offset: 0x0021E7D0
	internal void ᜅ()
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
		int a_ = 0;
		int a_2 = 0;
		spr\u1DB9.ᜀ(this.ᜁ(), ref a_, ref a_2);
		this.ᜀ().ᜂ(a_);
		this.ᜀ().ᜁ(a_2);
	}

	// Token: 0x06002010 RID: 8208 RVA: 0x0021F838 File Offset: 0x0021E838
	internal override spr\u2192 ᜂ()
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
		spr\u1DB9 spr_u1DB = (spr\u1DB9)base.ᜂ();
		spr_u1DB.ᜀ = this.ᜀ;
		spr_u1DB.ᜁ = this.ᜁ;
		return spr_u1DB;
	}

	// Token: 0x04002016 RID: 8214
	private new ShapeDocType ᜀ;
}
