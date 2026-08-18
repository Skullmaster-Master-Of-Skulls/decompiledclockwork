using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200053F RID: 1343
[spr\u2593(TBIFFRecord.ChartGelFrame)]
[CLSCompliant(false)]
internal class spr\u216D : spr\u2453
{
	// Token: 0x060051BA RID: 20922 RVA: 0x0032FA0C File Offset: 0x0032EA0C
	public virtual bool ᜄ()
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
		return true;
	}

	// Token: 0x060051BB RID: 20923 RVA: 0x0032FA48 File Offset: 0x0032EA48
	public List<spr\u23E7.ᜀ> ᜅ()
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

	// Token: 0x060051BC RID: 20924 RVA: 0x0032FA8C File Offset: 0x0032EA8C
	public new void ᜀ(List<spr\u23E7.ᜀ> A_0)
	{
		int a_ = 16;
		if (A_0 != null)
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
				this.ᜅ = A_0;
				return;
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ॅ㡇㹉╋⅍㹏ṑ㵓╕ⱗ", a_));
	}

	// Token: 0x060051BD RID: 20925 RVA: 0x0032FAF0 File Offset: 0x0032EAF0
	public spr\u216D()
	{
	}

	// Token: 0x060051BE RID: 20926 RVA: 0x0032FB40 File Offset: 0x0032EB40
	public spr\u216D(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060051BF RID: 20927 RVA: 0x0032FB90 File Offset: 0x0032EB90
	public spr\u216D(int A_0) : base(A_0)
	{
	}

	// Token: 0x060051C0 RID: 20928 RVA: 0x0032FBE0 File Offset: 0x0032EBE0
	public override void ᜂ()
	{
		int num = 0;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 1:
					return;
				case 2:
					base.ᜀ(base.TypeCode);
					base.ᜂ();
					this.ᜁ();
					this.ᜂ.Clear();
					if (true)
					{
					}
					num = 1;
					continue;
				}
				if (this.ᜀ == null)
				{
					return;
				}
				num = 2;
				break;
			}
		}
	}

	// Token: 0x060051C1 RID: 20929 RVA: 0x0032FC78 File Offset: 0x0032EC78
	private void ᜁ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 8;
				uint num2 = BitConverter.ToUInt32(this.ᜀ, 4);
				uint num3 = num2 + (uint)num;
				int num4 = 7;
				for (;;)
				{
					switch (num4)
					{
					case 0:
					{
						if ((long)num >= (long)((ulong)num3))
						{
							num4 = 1;
							continue;
						}
						spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ(this.ᜀ, ref num);
						this.ᜅ.Add(ᜀ);
						num4 = 8;
						continue;
					}
					case 1:
					{
						int num5 = 0;
						int count = this.ᜅ.Count;
						num4 = 5;
						continue;
					}
					case 2:
					{
						int num5;
						int count;
						if (num5 >= count)
						{
							num4 = 6;
							continue;
						}
						spr\u23E7.ᜀ ᜀ2 = this.ᜅ[num5];
						ᜀ2.ᜀ(this.ᜀ, ref num);
						num5++;
						num4 = 3;
						continue;
					}
					case 3:
						goto IL_EB;
					case 4:
						goto IL_8C;
					case 5:
						goto IL_EB;
					case 6:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DD;
						default:
							goto IL_16E;
						}
						break;
					case 7:
						goto IL_8C;
					case 8:
					{
						spr\u23E7.ᜀ ᜀ;
						if (ᜀ.ᜂ())
						{
							goto IL_DD;
						}
						goto IL_8C;
					}
					case 9:
					{
						spr\u23E7.ᜀ ᜀ;
						num3 -= ᜀ.ᜆ();
						num4 = 4;
						continue;
					}
					}
					break;
					IL_8C:
					num4 = 0;
					continue;
					IL_DD:
					num4 = 9;
					continue;
					IL_EB:
					num4 = 2;
				}
			}
			IL_16E:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x060051C2 RID: 20930 RVA: 0x0032FDFC File Offset: 0x0032EDFC
	public override void ᜀ(ExcelVersion A_0)
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
	}

	// Token: 0x060051C3 RID: 20931 RVA: 0x0032FE38 File Offset: 0x0032EE38
	private new void ᜀ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.m_iLength = 0;
				int num = 8;
				int num2 = 0;
				int count = this.ᜅ.Count;
				int num3 = 3;
				for (;;)
				{
					int num5;
					switch (num3)
					{
					case 0:
					{
						if (num2 >= count)
						{
							num3 = 7;
							continue;
						}
						spr\u23E7.ᜀ ᜀ = this.ᜅ[num2];
						this.m_iLength += ᜀ.ᜃ().Length;
						num += ᜀ.ᜃ().Length;
						num3 = 11;
						continue;
					}
					case 1:
						goto IL_23A;
					case 2:
						goto IL_23A;
					case 3:
						goto IL_25D;
					case 4:
					{
						if (true)
						{
						}
						spr\u23E7.ᜀ ᜀ;
						if (ᜀ.ᜄ().Length > 0)
						{
							num3 = 17;
							continue;
						}
						goto IL_308;
					}
					case 5:
						return;
					case 6:
						goto IL_308;
					case 7:
					{
						this.m_iLength += this.ᜄ.Length + 8;
						this.ᜀ = new byte[this.m_iLength];
						this.ᜃ.CopyTo(this.ᜀ, 0);
						this.ᜄ.CopyTo(this.ᜀ, this.m_iLength - this.ᜄ.Length);
						Array.Copy(BitConverter.GetBytes(this.m_iLength - 8 - this.ᜄ.Length), 0, this.ᜀ, 4, 4);
						int num4 = 8;
						num5 = 0;
						int count2 = this.ᜅ.Count;
						num3 = 1;
						continue;
					}
					case 8:
						num3 = 10;
						continue;
					case 9:
					{
						spr\u23E7.ᜀ ᜀ2;
						ᜀ2.ᜄ().CopyTo(this.ᜀ, num);
						num += ᜀ2.ᜄ().Length;
						num3 = 14;
						continue;
					}
					case 10:
					{
						spr\u23E7.ᜀ ᜀ2;
						if (ᜀ2.ᜄ().Length > 0)
						{
							num3 = 9;
							continue;
						}
						goto IL_182;
					}
					case 11:
					{
						spr\u23E7.ᜀ ᜀ;
						if (ᜀ.ᜄ() != null)
						{
							num3 = 13;
							continue;
						}
						goto IL_308;
					}
					case 12:
					{
						int count2;
						if (num5 >= count2)
						{
							num3 = 5;
							continue;
						}
						spr\u23E7.ᜀ ᜀ2 = this.ᜅ[num5];
						int num4;
						ᜀ2.ᜃ().CopyTo(this.ᜀ, num4);
						num4 += ᜀ2.ᜃ().Length;
						num3 = 15;
						continue;
					}
					case 13:
						num3 = 4;
						continue;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_23A;
						default:
							if (false)
							{
							}
							goto IL_182;
						}
						break;
					case 15:
					{
						spr\u23E7.ᜀ ᜀ2;
						if (ᜀ2.ᜄ() != null)
						{
							num3 = 8;
							continue;
						}
						goto IL_182;
					}
					case 16:
						goto IL_25D;
					case 17:
					{
						spr\u23E7.ᜀ ᜀ;
						this.m_iLength += ᜀ.ᜄ().Length;
						num3 = 6;
						continue;
					}
					}
					break;
					IL_182:
					num5++;
					num3 = 2;
					continue;
					IL_23A:
					num3 = 12;
					continue;
					IL_25D:
					num3 = 0;
					continue;
					IL_308:
					num2++;
					num3 = 16;
				}
			}
			return;
		}
	}

	// Token: 0x060051C4 RID: 20932 RVA: 0x00330164 File Offset: 0x0032F164
	public new List<BiffRecordRaw> ᜃ()
	{
		switch (0)
		{
		default:
		{
			List<BiffRecordRaw> list;
			for (;;)
			{
				this.ᜀ();
				list = new List<BiffRecordRaw>();
				int num = this.ᜀ.Length;
				spr\u251F spr_u251F = this;
				int num2 = 0;
				int num3 = 0;
				int num4 = 12;
				for (;;)
				{
					TBIFFRecord tbiffrecord;
					TBIFFRecord tbiffrecord2;
					switch (num4)
					{
					case 0:
						IL_EF:
						if (num3 >= 2)
						{
							num4 = 5;
							continue;
						}
						num4 = 8;
						continue;
					case 1:
						goto IL_17A;
					case 2:
						if (num3 >= 2)
						{
							num4 = 10;
							continue;
						}
						num4 = 15;
						continue;
					case 3:
						num4 = 13;
						continue;
					case 4:
						goto IL_16D;
					case 5:
						num4 = 14;
						continue;
					case 6:
						list.Add(spr_u251F);
						num4 = 9;
						continue;
					case 7:
						if (num <= 8224)
						{
							num4 = 3;
							continue;
						}
						num4 = 0;
						continue;
					case 8:
						tbiffrecord = TBIFFRecord.ChartGelFrame;
						goto IL_1D7;
					case 9:
						return list;
					case 10:
						num4 = 11;
						continue;
					case 11:
						tbiffrecord2 = TBIFFRecord.Continue;
						goto IL_115;
					case 12:
						goto IL_17A;
					case 13:
						if (num2 == 0)
						{
							num4 = 6;
							continue;
						}
						num4 = 2;
						continue;
					case 14:
						tbiffrecord = TBIFFRecord.Continue;
						goto IL_1D7;
					case 15:
						tbiffrecord2 = TBIFFRecord.ChartGelFrame;
						goto IL_115;
					}
					break;
					IL_1D7:
					TBIFFRecord a_ = tbiffrecord;
					spr_u251F = (spr\u251F)spr\u175E.ᜀ(a_);
					byte[] array = new byte[8224];
					Array.Copy(this.ᜀ, num2, array, 0, 8224);
					spr_u251F.ᜂ(array);
					spr_u251F.Length = 8224;
					list.Add(spr_u251F);
					num2 += 8224;
					num3++;
					num -= 8224;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EF;
					default:
						if (false)
						{
						}
						num4 = 1;
						continue;
					}
					IL_115:
					TBIFFRecord a_2 = tbiffrecord2;
					spr_u251F = (spr\u251F)spr\u175E.ᜀ(a_2);
					int num5 = this.ᜀ.Length - num2;
					byte[] array2 = new byte[num5];
					Array.Copy(this.ᜀ, num2, array2, 0, num5);
					spr_u251F.ᜂ(array2);
					spr_u251F.Length = num5;
					list.Add(spr_u251F);
					num4 = 4;
					continue;
					IL_17A:
					num4 = 7;
				}
			}
			IL_16D:
			if (true)
			{
			}
			return list;
		}
		}
	}

	// Token: 0x060051C5 RID: 20933 RVA: 0x003303D8 File Offset: 0x0032F3D8
	public override object ᜇ()
	{
		switch (0)
		{
		default:
		{
			spr\u216D spr_u216D;
			for (;;)
			{
				if (true)
				{
				}
				spr_u216D = (spr\u216D)base.ᜇ();
				spr_u216D.ᜅ = new List<spr\u23E7.ᜀ>();
				int num = 0;
				int count = this.ᜅ.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return spr_u216D;
					case 1:
						IL_D3:
						goto IL_5F;
					case 2:
					{
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						spr\u23E7.ᜀ ᜀ = this.ᜅ[num];
						spr_u216D.ᜅ.Add((spr\u23E7.ᜀ)ᜀ.ᜇ());
						num++;
						num2 = 1;
						continue;
					}
					case 3:
						goto IL_5F;
					}
					break;
					IL_5F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D3;
					default:
						if (false)
						{
						}
						num2 = 2;
						break;
					}
				}
			}
			return spr_u216D;
		}
		}
	}

	// Token: 0x060051C6 RID: 20934 RVA: 0x003304BC File Offset: 0x0032F4BC
	public void ᜈ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_7B:
				this.m_iLength = -1;
				int num = 384;
				int num2 = 17;
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
						int index;
						spr\u23E7.ᜀ ᜀ2;
						switch (num2)
						{
						case 0:
						{
							if (num > 412)
							{
								num2 = 8;
								continue;
							}
							MsoOptions a_ = (MsoOptions)num;
							num2 = 6;
							continue;
						}
						case 1:
							goto IL_159;
						case 2:
							num2 = 14;
							continue;
						case 3:
							if (!this.ᜀ(MsoOptions.NoFillHitTest, out index))
							{
								num2 = 7;
								continue;
							}
							return;
						case 4:
							goto IL_99;
						case 5:
							goto IL_1BA;
						case 6:
						{
							MsoOptions a_;
							if (!this.ᜀ(a_, out index))
							{
								num2 = 15;
								continue;
							}
							goto IL_99;
						}
						case 7:
						{
							spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ();
							ᜀ.ᜀ(MsoOptions.NoFillHitTest);
							this.ᜅ.Insert(index, ᜀ);
							num2 = 11;
							continue;
						}
						case 8:
							num2 = 3;
							continue;
						case 9:
							if (ᜀ2.ᜈ() == MsoOptions.GradientColorType)
							{
								num2 = 10;
								continue;
							}
							goto IL_213;
						case 10:
							ᜀ2.ᜀ(1U);
							num2 = 12;
							continue;
						case 11:
							return;
						case 12:
							goto IL_213;
						case 13:
							goto IL_231;
						case 14:
							if (true)
							{
							}
							if (ᜀ2.ᜈ() == MsoOptions.GradientTransparency)
							{
								goto IL_205;
							}
							goto IL_1BA;
						case 15:
						{
							ᜀ2 = new spr\u23E7.ᜀ();
							MsoOptions a_;
							ᜀ2.ᜀ(a_);
							num2 = 16;
							continue;
						}
						case 16:
							if (ᜀ2.ᜈ() != MsoOptions.Transparency)
							{
								num2 = 2;
								continue;
							}
							goto IL_231;
						case 17:
							goto IL_159;
						}
						goto IL_7B;
						IL_99:
						num++;
						num2 = 1;
						continue;
						IL_159:
						num2 = 0;
						continue;
						IL_1BA:
						num2 = 9;
						continue;
						IL_213:
						this.ᜅ.Insert(index, ᜀ2);
						num2 = 4;
						continue;
						IL_231:
						ᜀ2.ᜀ(65535U);
						num2 = 5;
						continue;
					}
					}
					IL_205:
					num2 = 13;
				}
			}
			return;
		}
	}

	// Token: 0x060051C7 RID: 20935 RVA: 0x00330718 File Offset: 0x0032F718
	private new bool ᜀ(MsoOptions A_0, out int A_1)
	{
		for (;;)
		{
			A_1 = 0;
			int count = this.ᜅ.Count;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_BC:
				A_1++;
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num = 3;
				break;
			}
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_7C;
				case 1:
					goto IL_BC;
				case 2:
					goto IL_67;
				case 3:
					goto IL_67;
				case 4:
					if (this.ᜅ[A_1].ᜈ() != A_0)
					{
						num = 1;
						continue;
					}
					goto IL_BE;
				case 5:
					if (A_1 >= count)
					{
						num = 0;
						continue;
					}
					num = 4;
					continue;
				}
				break;
				IL_67:
				num = 5;
			}
		}
		IL_7C:
		IL_BE:
		return A_1 < this.ᜅ.Count;
	}

	// Token: 0x0400246B RID: 9323
	public new const int ᜀ = 384;

	// Token: 0x0400246C RID: 9324
	public new const int ᜁ = 412;

	// Token: 0x0400246D RID: 9325
	public new const int ᜂ = 8;

	// Token: 0x0400246E RID: 9326
	private new readonly byte[] ᜃ = new byte[]
	{
		227,
		1,
		11,
		240
	};

	// Token: 0x0400246F RID: 9327
	private new readonly byte[] ᜄ = new byte[]
	{
		179,
		0,
		34,
		241,
		66,
		0,
		0,
		0,
		158,
		1,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		159,
		1,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		160,
		1,
		0,
		0,
		0,
		32,
		161,
		193,
		0,
		0,
		0,
		0,
		162,
		1,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		163,
		1,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		164,
		1,
		0,
		0,
		0,
		32,
		165,
		193,
		0,
		0,
		0,
		0,
		166,
		1,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		167,
		1,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		191,
		1,
		0,
		0,
		96,
		0
	};

	// Token: 0x04002470 RID: 9328
	private new List<spr\u23E7.ᜀ> ᜅ = new List<spr\u23E7.ᜀ>();
}
