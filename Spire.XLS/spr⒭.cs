using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x0200039D RID: 925
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ExtSST)]
internal class spr\u24AD : spr\u251F
{
	// Token: 0x0600384B RID: 14411 RVA: 0x001F7228 File Offset: 0x001F6228
	public new ushort ᜀ()
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

	// Token: 0x0600384C RID: 14412 RVA: 0x001F726C File Offset: 0x001F626C
	public new void ᜀ(ushort A_0)
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

	// Token: 0x0600384D RID: 14413 RVA: 0x001F72B0 File Offset: 0x001F62B0
	public spr\u19CA[] ᜄ()
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
		return this.ᜃ;
	}

	// Token: 0x0600384E RID: 14414 RVA: 0x001F72F4 File Offset: 0x001F62F4
	public new void ᜀ(spr\u19CA[] A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x0600384F RID: 14415 RVA: 0x001F7338 File Offset: 0x001F6338
	public virtual int ᜆ()
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
		return 0;
	}

	// Token: 0x06003850 RID: 14416 RVA: 0x001F7374 File Offset: 0x001F6374
	public bool ᜁ()
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

	// Token: 0x06003851 RID: 14417 RVA: 0x001F73B8 File Offset: 0x001F63B8
	public new sprỪ ᜃ()
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
		return this.ᜅ;
	}

	// Token: 0x06003852 RID: 14418 RVA: 0x001F73FC File Offset: 0x001F63FC
	public new void ᜀ(sprỪ A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06003853 RID: 14419 RVA: 0x001F7440 File Offset: 0x001F6440
	public spr\u24AD()
	{
	}

	// Token: 0x06003854 RID: 14420 RVA: 0x001F745C File Offset: 0x001F645C
	public spr\u24AD(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003855 RID: 14421 RVA: 0x001F7478 File Offset: 0x001F6478
	public spr\u24AD(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003856 RID: 14422 RVA: 0x001F7494 File Offset: 0x001F6494
	public override int ᜀ(BinaryWriter A_0, DataProvider A_1, IEncryptor A_2, int A_3)
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
		this.ᜅ();
		return base.ᜀ(A_0, A_1, A_2, A_3);
	}

	// Token: 0x06003857 RID: 14423 RVA: 0x001F74E0 File Offset: 0x001F64E0
	public override void ᜂ()
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num;
			int num6;
			spr\u24E5 spr_u24E;
			for (;;)
			{
				this.ᜂ = base.ᜌ(0);
				num = (this.m_iLength - 2) / 8;
				int num2 = this.m_iLength - 2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_138;
				default:
				{
					if (false)
					{
					}
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_A6;
						case 1:
							goto IL_CE;
						case 2:
							if (num2 % 8 != 0)
							{
								num3 = 3;
								continue;
							}
							goto IL_A6;
						case 3:
							num3 = 4;
							continue;
						case 4:
						{
							if (num2 % 4 != 0)
							{
								if (true)
								{
								}
								num3 = 6;
								continue;
							}
							int num4 = base.ᜑ(this.m_iLength - 4);
							this.ᜄ = (num4 == 10);
							num3 = 0;
							continue;
						}
						case 5:
							if (this.ᜄ)
							{
								num3 = 7;
								continue;
							}
							goto IL_A6;
						case 6:
						{
							int num5 = base.ᜑ(this.m_iLength - 4);
							this.ᜄ = (num5 == 10);
							num3 = 5;
							continue;
						}
						case 7:
							goto IL_138;
						}
						break;
						IL_A6:
						this.ᜃ = new spr\u19CA[num];
						num6 = 2;
						spr_u24E = new spr\u24E5(this.ᜀ);
						num3 = 1;
					}
					break;
				}
				}
			}
			IL_CE:
			try
			{
				for (;;)
				{
					int num7 = 0;
					int num3 = 4;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							num3 = 2;
							continue;
						case 1:
						{
							if (num7 >= num)
							{
								num3 = 0;
								continue;
							}
							spr\u19CA spr_u19CA = (spr\u19CA)spr\u175E.ᜀ(TBIFFRecord.ExtSSTInfoSub);
							spr_u19CA.StreamPos = this.StreamPos + (long)num6;
							spr_u19CA.ParseStructure(spr_u24E, num6, 8, ExcelVersion.Version97to2003);
							this.ᜃ[num7] = spr_u19CA;
							num7++;
							num6 += 8;
							num3 = 3;
							continue;
						}
						case 2:
							goto IL_208;
						case 3:
							goto IL_18F;
						case 4:
							goto IL_18F;
						}
						break;
						IL_18F:
						num3 = 1;
					}
				}
				IL_208:
				return;
			}
			finally
			{
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						((IDisposable)spr_u24E).Dispose();
						num3 = 1;
						continue;
					case 1:
						goto IL_247;
					}
					if (spr_u24E == null)
					{
						break;
					}
					num3 = 0;
				}
				IL_247:;
			}
			IL_138:
			throw new sprῩ(RecordTableEnumerator.b("簸䌺䤼氾ቀᝂᝄ≆⩈⑊㽌⭎癐⁒畔㍖㡘⽚㱜罞በ੢ὤɦ䥨٪ѬŮѰr啴䕶奸ᙺࡼ౾ꎂꦈ年릘連뾞馠趢", a_));
		}
		}
	}

	// Token: 0x06003858 RID: 14424 RVA: 0x001F775C File Offset: 0x001F675C
	public override void ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			this.ᜀ = new byte[this.GetStoreSize(ExcelVersion.Version97to2003)];
			base.ᜀ(0, this.ᜂ);
			this.m_iLength = 2;
			int num;
			int num2;
			int num3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_92:
				num = 0;
				num2 = this.ᜃ.Length;
				num3 = 1;
				break;
			default:
				if (false)
				{
				}
				num3 = 0;
				break;
			}
			for (;;)
			{
				if (true)
				{
				}
				switch (num3)
				{
				case 0:
					if (this.ᜃ != null)
					{
						num3 = 4;
						continue;
					}
					return;
				case 1:
					goto IL_94;
				case 2:
					return;
				case 3:
					if (num >= num2)
					{
						num3 = 2;
						continue;
					}
					this.ᜃ[num].StreamPos = (long)this.m_iLength;
					base.ᜀ(this.m_iLength, this.ᜃ[num].Data, 0, 8);
					num++;
					this.m_iLength += 8;
					num3 = 5;
					continue;
				case 4:
					goto IL_92;
				case 5:
					goto IL_94;
				}
				break;
				IL_94:
				num3 = 3;
			}
		}
	}

	// Token: 0x06003859 RID: 14425 RVA: 0x001F7884 File Offset: 0x001F6884
	public void ᜅ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = (int)this.ᜅ.StreamPos;
				int num2 = (int)this.ᜅ.ᜇ();
				int num3 = 4;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_75;
					case 1:
						goto IL_104;
					case 2:
						goto IL_75;
					case 3:
					{
						int num4;
						if (num4 >= num2)
						{
							goto IL_83;
						}
						int[] array;
						int num5 = array[num4];
						int num6;
						spr\u19CA spr_u19CA = this.ᜃ[num6];
						int[] array2;
						spr_u19CA.ᜀ(num + array2[num4]);
						spr_u19CA.ᜀ((ushort)num5);
						num4 += (int)this.ᜀ();
						num6++;
						num3 = 0;
						continue;
					}
					case 4:
						if (num2 > 0)
						{
							if (true)
							{
							}
							num3 = 5;
							continue;
						}
						goto IL_104;
					case 5:
					{
						int[] array = this.ᜅ.ᜅ();
						int[] array2 = this.ᜅ.ᜆ();
						int num4 = 0;
						int num6 = 0;
						num3 = 2;
						continue;
					}
					}
					break;
					IL_75:
					num3 = 3;
					continue;
					IL_83:
					num3 = 1;
					continue;
					IL_104:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_83;
					default:
						goto IL_11A;
					}
				}
			}
			IL_11A:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x0600385A RID: 14426 RVA: 0x001F79B4 File Offset: 0x001F69B4
	public override int ᜁ(ExcelVersion A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 2:
				goto IL_7A;
			case 3:
				num = 2;
				continue;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (this.ᜃ == null)
				{
					num = 3;
					continue;
				}
				break;
			}
			num = 0;
		}
		IL_5E:
		int num2 = this.ᜃ.Length;
		goto IL_7D;
		IL_7A:
		num2 = 0;
		IL_7D:
		int num3 = num2;
		return 2 + num3 * 8;
	}

	// Token: 0x040018CE RID: 6350
	private new const int ᜀ = 2;

	// Token: 0x040018CF RID: 6351
	private new const int ᜁ = 8;

	// Token: 0x040018D0 RID: 6352
	[spr\u2429(0, 2)]
	private new ushort ᜂ = 8;

	// Token: 0x040018D1 RID: 6353
	private new spr\u19CA[] ᜃ;

	// Token: 0x040018D2 RID: 6354
	private new bool ᜄ;

	// Token: 0x040018D3 RID: 6355
	private new sprỪ ᜅ;
}
