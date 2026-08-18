using System;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000320 RID: 800
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.SST)]
internal class sprỪ : spr\u23E8
{
	// Token: 0x06003163 RID: 12643 RVA: 0x001C97BC File Offset: 0x001C87BC
	public new uint ᜃ()
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

	// Token: 0x06003164 RID: 12644 RVA: 0x001C9800 File Offset: 0x001C8800
	public new void ᜀ(uint A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06003165 RID: 12645 RVA: 0x001C9844 File Offset: 0x001C8844
	public uint ᜇ()
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

	// Token: 0x06003166 RID: 12646 RVA: 0x001C9888 File Offset: 0x001C8888
	public object[] ᜊ()
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

	// Token: 0x06003167 RID: 12647 RVA: 0x001C98CC File Offset: 0x001C88CC
	public new void ᜀ(object[] A_0)
	{
		int a_ = 16;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (A_0 != null)
			{
				this.ᜃ = A_0;
				this.ᜂ = (uint)this.ᜃ.Length;
				return;
			}
			if (true)
			{
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ぅ⥇♉㥋⭍", a_));
	}

	// Token: 0x06003168 RID: 12648 RVA: 0x001C9940 File Offset: 0x001C8940
	public int[] ᜆ()
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

	// Token: 0x06003169 RID: 12649 RVA: 0x001C9984 File Offset: 0x001C8984
	public int[] ᜅ()
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

	// Token: 0x0600316A RID: 12650 RVA: 0x001C99C8 File Offset: 0x001C89C8
	public bool ᜈ()
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

	// Token: 0x0600316B RID: 12651 RVA: 0x001C9A0C File Offset: 0x001C8A0C
	public new void ᜀ(bool A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x0600316C RID: 12652 RVA: 0x001C9A50 File Offset: 0x001C8A50
	public virtual bool ᜉ()
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

	// Token: 0x0600316E RID: 12654 RVA: 0x001C9AA8 File Offset: 0x001C8AA8
	public override void ᜀ()
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				this.ᜁ = this.ᜀ.ReadUInt32(0);
				this.ᜂ = this.ᜀ.ReadUInt32(4);
				this.ᜃ = new object[this.ᜂ];
				num = 8;
				int count = this.ᜁ.Count;
				int num2 = 0;
				spr\u223A spr_u223A = new spr\u223A(0);
				int num3 = 0;
				int num4 = 1;
				for (;;)
				{
					if (true)
					{
					}
					string text;
					object obj;
					int num5;
					switch (num4)
					{
					case 0:
					{
						byte[] array;
						if (array != null)
						{
							num4 = 9;
							continue;
						}
						goto IL_B9;
					}
					case 1:
						goto IL_10E;
					case 2:
						goto IL_164;
					case 3:
						if ((long)num3 >= (long)((ulong)this.ᜂ))
						{
							num4 = 5;
							continue;
						}
						num4 = 10;
						continue;
					case 4:
						goto IL_10E;
					case 5:
						goto IL_132;
					case 6:
						goto IL_166;
					case 7:
					{
						spr\u223A spr_u223A2 = spr_u223A.\u170D();
						spr_u223A2.ᜁ(text);
						byte[] array;
						spr_u223A2.ᜀ(array);
						obj = spr_u223A2;
						num4 = 11;
						continue;
					}
					case 8:
					{
						byte[] array;
						if (array.Length > 0)
						{
							goto IL_1BA;
						}
						goto IL_B9;
					}
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1BA;
						default:
							if (false)
							{
							}
							num4 = 8;
							continue;
						}
						break;
					case 10:
					{
						if (3 + num > this.m_iLength)
						{
							num4 = 2;
							continue;
						}
						byte[] array;
						byte[] array2;
						text = base.ᜀ(num, this.ᜁ, count, ref num2, out num5, out array, out array2);
						obj = null;
						num4 = 0;
						continue;
					}
					case 11:
						goto IL_166;
					}
					break;
					IL_B9:
					obj = text;
					num4 = 6;
					continue;
					IL_10E:
					num4 = 3;
					continue;
					IL_166:
					this.ᜃ[num3] = obj;
					num += num5;
					num3++;
					num4 = 4;
					continue;
					IL_1BA:
					num4 = 7;
				}
			}
			IL_132:
			this.ᜀ(num);
			this.ᜀ.Clear();
			return;
			IL_164:
			throw new sprῩ(RecordTableEnumerator.b("欷椹栻氽┿⅁⭃㑅ⱇ", a_));
		}
		}
	}

	// Token: 0x0600316F RID: 12655 RVA: 0x001C9CD0 File Offset: 0x001C8CD0
	public override void ᜀ(ExcelVersion A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u1CCE spr_u1CCE;
			for (;;)
			{
				this.ᜁ.Clear();
				this.ᜁ();
				this.ᜁ = this.ᜂ;
				this.ᜀ.WriteUInt32(0, this.ᜁ);
				this.ᜀ.WriteUInt32(4, this.ᜂ);
				this.m_iLength = 8;
				this.ᜄ = new int[this.ᜂ];
				this.ᜅ = new int[this.ᜂ];
				byte[] array = null;
				byte[] array2 = null;
				new spr\u223A(0);
				spr_u1CCE = new spr\u1CCE(this, 4);
				int num = 0;
				int num2 = 12;
				for (;;)
				{
					int num3;
					string text;
					spr\u223A.StringType stringType;
					Encoding encoding;
					switch (num2)
					{
					case 0:
						goto IL_135;
					case 1:
						goto IL_130;
					case 2:
						if (num3 > 0)
						{
							num2 = 8;
							continue;
						}
						goto IL_ED;
					case 3:
						goto IL_24F;
					case 4:
						goto IL_303;
					case 5:
						if (spr\u251F.ᜀ(text))
						{
							num2 = 13;
							continue;
						}
						goto IL_135;
					case 6:
					{
						spr\u223A spr_u223A;
						if (spr_u223A == null)
						{
							num2 = 14;
							continue;
						}
						text = spr_u223A.ᜏ();
						num3 = spr_u223A.ᜌ();
						stringType = spr_u223A.ᜑ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_130;
						default:
							if (false)
							{
							}
							num2 = 4;
							continue;
						}
						break;
					}
					case 7:
						goto IL_ED;
					case 8:
					{
						spr\u223A spr_u223A;
						spr_u223A.ᜁ(array2, 0, false);
						num2 = 7;
						continue;
					}
					case 9:
					{
						if ((long)num >= (long)((ulong)this.ᜂ))
						{
							num2 = 16;
							continue;
						}
						object obj = this.ᜃ[num];
						spr\u223A spr_u223A = obj as spr\u223A;
						num3 = 0;
						stringType = spr\u223A.StringType.Unicode;
						encoding = Encoding.Unicode;
						num2 = 6;
						continue;
					}
					case 10:
						if (spr_u1CCE.ᜀ() < 20)
						{
							num2 = 1;
							continue;
						}
						goto IL_1B8;
					case 11:
						goto IL_303;
					case 12:
						goto IL_24F;
					case 13:
						stringType &= ~spr\u223A.StringType.Unicode;
						stringType = stringType;
						encoding = Encoding.UTF8;
						num2 = 0;
						continue;
					case 14:
					{
						if (true)
						{
						}
						object obj;
						text = (string)obj;
						num2 = 11;
						continue;
					}
					case 15:
						goto IL_1B8;
					case 16:
						goto IL_275;
					}
					break;
					IL_ED:
					this.ᜄ[num] = spr_u1CCE.ᜅ() + 4;
					this.ᜅ[num] = spr_u1CCE.ᜁ();
					num2 = 10;
					continue;
					IL_130:
					spr_u1CCE.ᜃ();
					num2 = 15;
					continue;
					IL_135:
					ushort num4 = (ushort)text.Length;
					int byteCount = encoding.GetByteCount(text);
					int a_ = byteCount + 3;
					sprỪ.ᜀ(ref array, a_);
					sprỪ.ᜀ(ref array2, num3);
					encoding.GetBytes(text, 0, (int)num4, array, 0);
					num2 = 2;
					continue;
					IL_1B8:
					spr_u1CCE.ᜀ(num4);
					this.ᜀ(spr_u1CCE, stringType, num3, byteCount, array);
					this.ᜀ(spr_u1CCE, array2, num3);
					num++;
					num2 = 3;
					continue;
					IL_24F:
					num2 = 9;
					continue;
					IL_303:
					num2 = 5;
				}
			}
			IL_275:
			this.m_iLength = spr_u1CCE.ᜉ();
			this.ᜂ = spr_u1CCE.ᜈ();
			spr_u1CCE.ᜄ();
			return;
		}
		}
	}

	// Token: 0x06003170 RID: 12656 RVA: 0x001CA02C File Offset: 0x001C902C
	private new void ᜀ(spr\u1CCE A_0, spr\u223A.StringType A_1, int A_2, int A_3, byte[] A_4)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 18;
			for (;;)
			{
				int num3;
				switch (num2)
				{
				case 0:
					goto IL_15B;
				case 1:
					num3 = Math.Min(A_0.ᜀ(), A_3 - num) / 2 * 2;
					goto IL_10E;
				case 2:
					A_1 = spr\u223A.StringType.Unicode;
					num2 = 13;
					continue;
				case 3:
					if (num >= A_3)
					{
						num2 = 10;
						continue;
					}
					goto IL_60;
				case 4:
					if (num == 0)
					{
						num2 = 5;
						continue;
					}
					goto IL_7F;
				case 5:
					num2 = 6;
					continue;
				case 6:
					if (A_2 > 0)
					{
						num2 = 11;
						continue;
					}
					goto IL_7F;
				case 7:
					if (true)
					{
					}
					num2 = 9;
					continue;
				case 8:
					if ((byte)(A_1 & spr\u223A.StringType.Unicode) == 0)
					{
						num2 = 7;
						continue;
					}
					num2 = 1;
					continue;
				case 9:
					num3 = Math.Min(A_0.ᜀ(), A_3 - num);
					goto IL_10E;
				case 10:
					return;
				case 11:
					A_0.ᜀ((ushort)(A_2 / 4));
					num2 = 12;
					continue;
				case 12:
					goto IL_7F;
				case 13:
					goto IL_DB;
				case 14:
					goto IL_DB;
				case 15:
					if (num < A_3)
					{
						num2 = 17;
						continue;
					}
					goto IL_15B;
				case 16:
					if ((byte)(A_1 & spr\u223A.StringType.Unicode) != 0)
					{
						num2 = 2;
						continue;
					}
					A_1 = spr\u223A.StringType.NonUnicode;
					num2 = 14;
					continue;
				case 17:
					A_0.ᜃ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DB;
					default:
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 18:
					goto IL_60;
				}
				break;
				IL_60:
				A_0.ᜀ((byte)A_1);
				num2 = 4;
				continue;
				IL_7F:
				num2 = 8;
				continue;
				IL_DB:
				num2 = 3;
				continue;
				IL_10E:
				int num4 = num3;
				A_0.ᜀ(A_4, num, num4);
				num += num4;
				num2 = 15;
				continue;
				IL_15B:
				num2 = 16;
			}
		}
	}

	// Token: 0x06003171 RID: 12657 RVA: 0x001CA224 File Offset: 0x001C9224
	private new void ᜀ(spr\u1CCE A_0, byte[] A_1, int A_2)
	{
		for (;;)
		{
			IL_44:
			int num = 0;
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
					switch (num2)
					{
					case 0:
						if (num >= A_2)
						{
							num2 = 2;
							continue;
						}
						goto IL_81;
					case 1:
						num2 = 5;
						continue;
					case 2:
						return;
					case 3:
						if (true)
						{
						}
						if (A_2 > 0)
						{
							num2 = 1;
							continue;
						}
						return;
					case 4:
						if (num < A_2)
						{
							num2 = 6;
							continue;
						}
						goto IL_D3;
					case 5:
						goto IL_81;
					case 6:
						goto IL_C2;
					case 7:
						goto IL_D3;
					}
					goto IL_44;
					IL_81:
					int num3 = Math.Min(A_0.ᜀ(), A_2 - num) / 4 * 4;
					A_0.ᜀ(A_1, num, num3);
					A_0.ᜂ();
					num += num3;
					num2 = 4;
					continue;
					IL_D3:
					num2 = 0;
					continue;
				}
				}
				IL_C2:
				A_0.ᜃ();
				num2 = 7;
			}
		}
	}

	// Token: 0x06003172 RID: 12658 RVA: 0x001CA320 File Offset: 0x001C9320
	private new void ᜁ()
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 0;
				int num3 = 0;
				for (;;)
				{
					string text;
					switch (num3)
					{
					case 0:
						goto IL_EE;
					case 1:
					{
						spr\u223A spr_u223A;
						if (spr_u223A == null)
						{
							num3 = 4;
							continue;
						}
						text = spr_u223A.ᜏ();
						int num4 = spr_u223A.ᜆ();
						num3 = 5;
						continue;
					}
					case 2:
						goto IL_51;
					case 3:
						goto IL_11B;
					case 4:
						goto IL_11D;
					case 5:
					{
						if (true)
						{
						}
						int num4;
						if (num4 > 0)
						{
							num3 = 8;
							continue;
						}
						goto IL_51;
					}
					case 6:
					{
						if ((long)num2 >= (long)((ulong)this.ᜂ))
						{
							num3 = 3;
							continue;
						}
						object obj = this.ᜃ[num2];
						spr\u223A spr_u223A = obj as spr\u223A;
						num3 = 1;
						continue;
					}
					case 7:
						goto IL_EE;
					case 8:
					{
						spr\u223A spr_u223A;
						num += spr_u223A.ᜆ() * 4 + 2;
						num3 = 2;
						continue;
					}
					case 9:
						goto IL_51;
					}
					break;
					IL_51:
					num += text.Length * 2 + 3;
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_11D:
						object obj;
						text = (string)obj;
						num3 = 9;
						continue;
					}
					default:
						if (false)
						{
						}
						num3 = 7;
						continue;
					}
					IL_EE:
					num3 = 6;
				}
			}
			IL_11B:
			num += num / 1000;
			this.ᜀ.EnsureCapacity(8 + num);
			return;
		}
		}
	}

	// Token: 0x06003173 RID: 12659 RVA: 0x001CA49C File Offset: 0x001C949C
	public new static void ᜀ(ref byte[] A_0, int A_1)
	{
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					if (num2 < A_1)
					{
						num = 6;
						continue;
					}
					return;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					goto IL_6A;
				case 3:
					num3 = 0;
					goto IL_88;
				case 4:
					num3 = A_0.Length;
					goto IL_88;
				case 5:
					num = 4;
					continue;
				case 6:
					A_0 = new byte[A_1];
					num = 2;
					continue;
				}
				if (A_0 != null)
				{
					num = 5;
					continue;
				}
				num = 3;
				continue;
				IL_88:
				num2 = num3;
				num = 0;
			}
			IL_6A:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_80;
			}
		}
		IL_80:
		if (false)
		{
		}
	}

	// Token: 0x06003174 RID: 12660 RVA: 0x001CA55C File Offset: 0x001C955C
	private new void ᜀ(int A_0)
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
	}

	// Token: 0x06003175 RID: 12661 RVA: 0x001CA598 File Offset: 0x001C9598
	public virtual int ᜁ(ExcelVersion A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5C;
			case 1:
				goto IL_72;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5C:
				this.ᜀ(A_0);
				base.NeedInfill = false;
				num = 1;
				break;
			default:
				if (false)
				{
				}
				if (!base.NeedInfill)
				{
					goto IL_74;
				}
				num = 0;
				break;
			}
		}
		IL_72:
		IL_74:
		return this.m_iLength;
	}

	// Token: 0x040015C0 RID: 5568
	private new const int ᜀ = 2;

	// Token: 0x040015C1 RID: 5569
	[spr\u2429(0, 4)]
	private new uint ᜁ;

	// Token: 0x040015C2 RID: 5570
	[spr\u2429(4, 4)]
	private new uint ᜂ;

	// Token: 0x040015C3 RID: 5571
	private new object[] ᜃ;

	// Token: 0x040015C4 RID: 5572
	private int[] ᜄ;

	// Token: 0x040015C5 RID: 5573
	private int[] ᜅ;

	// Token: 0x040015C6 RID: 5574
	private bool ᜆ = true;
}
