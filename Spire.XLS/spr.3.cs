using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;

// Token: 0x020004A1 RID: 1185
[spr\u2593(TBIFFRecord.OBJ)]
[CLSCompliant(false)]
internal class spr\u2003 : BiffRecordRaw, ICloneable
{
	// Token: 0x0600491F RID: 18719 RVA: 0x002C7640 File Offset: 0x002C6640
	public spr\u25AD[] ᜀ()
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
		return this.ᜀ.ToArray();
	}

	// Token: 0x06004920 RID: 18720 RVA: 0x002C7688 File Offset: 0x002C6688
	public new List<spr\u25AD> ᜃ()
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

	// Token: 0x06004921 RID: 18721 RVA: 0x002C76CC File Offset: 0x002C66CC
	public virtual bool ᜂ()
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
		return true;
	}

	// Token: 0x06004922 RID: 18722 RVA: 0x002C7708 File Offset: 0x002C6708
	public spr\u2003()
	{
	}

	// Token: 0x06004923 RID: 18723 RVA: 0x002C7728 File Offset: 0x002C6728
	public spr\u2003(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004924 RID: 18724 RVA: 0x002C7748 File Offset: 0x002C6748
	public spr\u2003(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004925 RID: 18725 RVA: 0x002C7768 File Offset: 0x002C6768
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int a_ = A_1;
				int num = A_1 + this.m_iLength;
				TObjType a_2 = TObjType.otGroup;
				int num2 = 0;
				for (;;)
				{
					spr\u25AD spr_u25AD;
					switch (num2)
					{
					case 0:
						goto IL_90;
					case 1:
						if (A_1 >= num)
						{
							num2 = 5;
							continue;
						}
						goto IL_90;
					case 2:
					{
						spr\u2223 spr_u = (spr\u2223)spr_u25AD;
						a_2 = spr_u.ᜄ();
						num2 = 3;
						continue;
					}
					case 3:
						goto IL_47;
					case 4:
						if (true)
						{
						}
						if (spr_u25AD.ᜏ() == TObjSubRecordType.ftCmo)
						{
							num2 = 2;
							continue;
						}
						goto IL_47;
					case 5:
						return;
					}
					break;
					IL_53:
					num2 = 1;
					continue;
					IL_47:
					A_1 += (int)(spr_u25AD.ᜎ() + 4);
					goto IL_53;
					IL_90:
					spr_u25AD = this.ᜀ(A_0, A_1, a_, a_2);
					this.ᜀ.Add(spr_u25AD);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_53;
					default:
						if (false)
						{
						}
						num2 = 4;
						break;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06004926 RID: 18726 RVA: 0x002C7868 File Offset: 0x002C6868
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_27:
				this.m_iLength = 0;
				int num = 0;
				int count = this.ᜀ.Count;
				for (;;)
				{
					IL_3C:
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_63;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_3C;
							default:
								if (false)
								{
								}
								goto IL_63;
							}
							break;
						case 3:
						{
							if (num >= count)
							{
								if (true)
								{
								}
								num2 = 0;
								continue;
							}
							spr\u25AD spr_u25AD = this.ᜀ[num];
							spr_u25AD.ᜀ(A_0, A_1);
							int num3 = spr_u25AD.ᜀ(A_2);
							this.m_iLength += num3;
							A_1 += num3;
							num++;
							num2 = 1;
							continue;
						}
						}
						goto IL_27;
						IL_63:
						num2 = 3;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06004927 RID: 18727 RVA: 0x002C7944 File Offset: 0x002C6944
	public virtual int ᜀ(ExcelVersion A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				IL_27:
				num = 0;
				int num2 = 0;
				int count = this.ᜀ.Count;
				for (;;)
				{
					IL_37:
					if (true)
					{
					}
					int num3 = 3;
					for (;;)
					{
						switch (num3)
						{
						case 0:
						{
							if (num2 >= count)
							{
								num3 = 2;
								continue;
							}
							spr\u25AD spr_u25AD = this.ᜀ[num2];
							num += spr_u25AD.ᜀ(A_0);
							num2++;
							num3 = 1;
							continue;
						}
						case 1:
							goto IL_70;
						case 2:
							return num;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_37;
							default:
								if (false)
								{
								}
								goto IL_70;
							}
							break;
						}
						goto IL_27;
						IL_70:
						num3 = 0;
					}
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06004928 RID: 18728 RVA: 0x002C7A04 File Offset: 0x002C6A04
	protected spr\u25AD ᜀ(DataProvider A_0, int A_1, int A_2, TObjType A_3)
	{
		switch (0)
		{
		default:
		{
			TObjSubRecordType tobjSubRecordType;
			ushort num;
			byte[] array;
			for (;;)
			{
				IL_4F:
				tobjSubRecordType = (TObjSubRecordType)A_0.ReadInt16(A_1);
				num = A_0.ReadUInt16(A_1 + 2);
				int num2 = 13;
				for (;;)
				{
					int iOffset;
					TObjSubRecordType tobjSubRecordType2;
					int num3;
					switch (num2)
					{
					case 0:
						if (A_0.ReadInt32(iOffset) == 0)
						{
							num2 = 12;
							continue;
						}
						goto IL_16E;
					case 1:
						goto IL_229;
					case 2:
						goto IL_18D;
					case 3:
						switch (tobjSubRecordType2)
						{
						case TObjSubRecordType.ftEnd:
							goto IL_241;
						case TObjSubRecordType.Reserved0:
						case TObjSubRecordType.Reserved1:
						case TObjSubRecordType.Reserved2:
						case TObjSubRecordType.ftButton:
						case TObjSubRecordType.ftGmo:
						case TObjSubRecordType.ftPictFmla:
						case TObjSubRecordType.ftGboData:
						case TObjSubRecordType.ftEdoData:
							goto IL_29F;
						case TObjSubRecordType.ftMacro:
							goto IL_22E;
						case TObjSubRecordType.ftCf:
							goto IL_E4;
						case TObjSubRecordType.ftPioGrbit:
							goto IL_12D;
						case TObjSubRecordType.ftCbls:
							goto IL_DA;
						case TObjSubRecordType.ftRbo:
							goto IL_B4;
						case TObjSubRecordType.ftSbs:
							goto IL_24B;
						case TObjSubRecordType.ftNts:
							goto IL_10A;
						case TObjSubRecordType.ftSbsFormula:
							goto IL_8A;
						case TObjSubRecordType.ftRboData:
							goto IL_124;
						case TObjSubRecordType.ftCblsData:
							goto IL_80;
						case TObjSubRecordType.ftLbsData:
							goto IL_255;
						case TObjSubRecordType.ftCblsFmla:
							goto IL_237;
						case TObjSubRecordType.ftCmo:
							goto IL_137;
						default:
							num2 = 9;
							continue;
						}
						break;
					case 4:
						if ((int)num + A_1 + 4 > base.Length)
						{
							num2 = 7;
							continue;
						}
						goto IL_94;
					case 5:
						if (tobjSubRecordType == TObjSubRecordType.ftEnd)
						{
							num2 = 6;
							continue;
						}
						goto IL_18D;
					case 6:
						num = 0;
						num2 = 2;
						continue;
					case 7:
						goto IL_261;
					case 8:
						goto IL_16E;
					case 9:
						num2 = 1;
						continue;
					case 10:
						num2 = 4;
						continue;
					case 11:
						goto IL_94;
					case 12:
						if (true)
						{
						}
						num3 += 4;
						num2 = 8;
						continue;
					case 13:
						if (tobjSubRecordType != TObjSubRecordType.ftLbsData)
						{
							num2 = 10;
							continue;
						}
						goto IL_261;
					}
					goto IL_4F;
					IL_94:
					num2 = 5;
					continue;
					IL_16E:
					num = (ushort)(base.Length - A_1 - num3 + A_2);
					num2 = 11;
					continue;
					IL_18D:
					array = new byte[(int)num];
					A_0.ReadArray(A_1 + 4, array);
					tobjSubRecordType2 = tobjSubRecordType;
					num2 = 3;
					continue;
					IL_261:
					num3 = 4;
					iOffset = base.Length - 4;
					num2 = 0;
				}
				IL_E4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_FA;
				}
			}
			IL_80:
			return new sprἨ(tobjSubRecordType, num, array);
			IL_8A:
			return new sprḵ(tobjSubRecordType, num, array);
			IL_B4:
			return new sprᾑ(num, array);
			IL_DA:
			return new sprᯄ(tobjSubRecordType, num, array);
			IL_FA:
			if (false)
			{
			}
			return new spr\u21EA(TObjSubRecordType.ftCf, num, array);
			IL_10A:
			return new spr\u2474(tobjSubRecordType, num, array);
			IL_124:
			return new sprᯋ(num, array);
			IL_12D:
			return new spr᮴(TObjSubRecordType.ftPioGrbit, num, array);
			IL_137:
			return new spr\u2223(tobjSubRecordType, num, array);
			IL_229:
			goto IL_29F;
			IL_22E:
			return new sprᥰ(num, array);
			IL_237:
			return new spr᧗(tobjSubRecordType, num, array);
			IL_241:
			return new sprទ(tobjSubRecordType, num, array);
			IL_24B:
			return new sprᢛ(tobjSubRecordType, num, array);
			IL_255:
			return new spr\u2471(tobjSubRecordType, num, array, A_3);
			IL_29F:
			return new spr\u2437(tobjSubRecordType, num, array);
		}
		}
	}

	// Token: 0x06004929 RID: 18729 RVA: 0x002C7CBC File Offset: 0x002C6CBC
	public void ᜀ(spr\u25AD A_0)
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
		this.ᜀ.Add(A_0);
	}

	// Token: 0x0600492A RID: 18730 RVA: 0x002C7D04 File Offset: 0x002C6D04
	public spr\u25AD ᜀ(TObjSubRecordType A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (true)
			{
			}
			if (false)
			{
			}
			int num = this.ᜁ(A_0);
			if (num >= 0)
			{
				return this.ᜀ[num];
			}
			break;
		}
		}
		return null;
	}

	// Token: 0x0600492B RID: 18731 RVA: 0x002C7D5C File Offset: 0x002C6D5C
	public int ᜁ(TObjSubRecordType A_0)
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				result = -1;
				int num = 0;
				int count = this.ᜀ.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						result = num;
						num2 = 5;
						continue;
					case 1:
						goto IL_C0;
					case 2:
					{
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						spr\u25AD spr_u25AD = this.ᜀ[num];
						num2 = 6;
						continue;
					}
					case 3:
						return result;
					case 4:
						goto IL_C0;
					case 5:
						return result;
					case 6:
					{
						spr\u25AD spr_u25AD;
						if (spr_u25AD.ᜏ() == A_0)
						{
							num2 = 0;
							continue;
						}
						for (;;)
						{
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_68;
							}
						}
						IL_68:
						if (false)
						{
						}
						num2 = 4;
						continue;
					}
					}
					break;
					IL_C0:
					if (true)
					{
					}
					num2 = 2;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x0600492C RID: 18732 RVA: 0x002C7E50 File Offset: 0x002C6E50
	public object ᜁ()
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
		spr\u2003 spr_u = (spr\u2003)base.Clone();
		spr_u.ᜀ = spr\u1CD3.ᜀ<spr\u25AD>(this.ᜀ);
		return spr_u;
	}

	// Token: 0x04002135 RID: 8501
	private new List<spr\u25AD> ᜀ = new List<spr\u25AD>();
}
