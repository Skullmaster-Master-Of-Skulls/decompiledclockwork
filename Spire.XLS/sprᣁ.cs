using System;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000369 RID: 873
[CLSCompliant(false)]
internal class sprᣁ : spr\u1A58
{
	// Token: 0x06003564 RID: 13668 RVA: 0x001E7DB4 File Offset: 0x001E6DB4
	public sprᣁ(spr\u2453 A_0) : base(A_0)
	{
		base.ᜁ(TBIFFRecord.HeaderFooterImage);
		base.ᜀ(TBIFFRecord.HeaderFooterImage);
	}

	// Token: 0x06003565 RID: 13669 RVA: 0x001E7DE0 File Offset: 0x001E6DE0
	public override int ᜀ()
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
		return 8224 - spr\u1976.ᜃ;
	}

	// Token: 0x06003566 RID: 13670 RVA: 0x001E7E28 File Offset: 0x001E6E28
	public override int ᜀ(byte[] A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				IL_6D:
				num = 0;
				int num2 = 3;
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
						int num4;
						int num5;
						switch (num2)
						{
						case 0:
							goto IL_19F;
						case 1:
							goto IL_11D;
						case 2:
							num2 = 11;
							continue;
						case 3:
							goto IL_78;
						case 4:
						{
							int num3;
							if (num3 - num4 >= this.ᜅ)
							{
								num2 = 2;
								continue;
							}
							num2 = 5;
							continue;
						}
						case 5:
						{
							int num3;
							num5 = num3 - num4;
							goto IL_16C;
						}
						case 6:
						{
							if (true)
							{
							}
							int num3 = A_1 + A_2;
							num4 = A_1;
							num2 = 7;
							continue;
						}
						case 7:
							goto IL_19F;
						case 8:
							goto IL_12E;
						case 9:
						{
							int num3;
							if (num4 >= num3)
							{
								num2 = 10;
								continue;
							}
							base.ᜂ();
							base.ᜇ();
							num++;
							this.ᜀ.ᜀ(this.ᜁ, spr\u1976.ᜂ, 0, spr\u1976.ᜃ);
							base.ᜁ(spr\u1976.ᜃ);
							num2 = 4;
							continue;
						}
						case 10:
							num2 = 8;
							continue;
						case 11:
							num5 = this.ᜅ;
							goto IL_16C;
						}
						goto IL_6D;
						IL_16C:
						int num6 = num5;
						this.ᜀ.ᜀ(this.ᜁ, A_0, num4, num6);
						base.ᜁ(num6);
						num4 += this.ᜅ;
						num2 = 0;
						continue;
						IL_19F:
						num2 = 9;
						continue;
					}
					}
					IL_78:
					if (base.ᜀ(A_2))
					{
						num2 = 6;
					}
					else
					{
						this.ᜀ.ᜀ(this.ᜁ, A_0, A_1, A_2);
						base.ᜁ(A_2);
						num2 = 1;
					}
				}
			}
			IL_11D:
			IL_12E:
			base.ᜂ();
			return num;
		}
		}
	}
}
