using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x020002F4 RID: 756
[CLSCompliant(false)]
internal abstract class spr\u2453 : spr\u251F
{
	// Token: 0x06002EC2 RID: 11970 RVA: 0x001A21F8 File Offset: 0x001A11F8
	protected spr\u1A58 ᜎ()
	{
		int a_ = 17;
		if (this.ᜁ == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("Ն㱈≊⅌⭎㑐⅒", a_), RecordTableEnumerator.b("ц╈⩊㹌㱎煐㝒㩔㉖⩘筚㍜ぞᕠ䍢٤٦ըݪ䵬ὮၰŲၴ᥶൸孺ၼ᩾ꦈ슊ﾒ璉\ude96쾠슢즤좨\udfaa첬膮", a_));
			}
		}
		return this.ᜁ;
	}

	// Token: 0x06002EC3 RID: 11971 RVA: 0x001A2270 File Offset: 0x001A1270
	protected spr\u2453()
	{
	}

	// Token: 0x06002EC4 RID: 11972 RVA: 0x001A2298 File Offset: 0x001A1298
	protected spr\u2453(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002EC5 RID: 11973 RVA: 0x001A22C0 File Offset: 0x001A12C0
	protected spr\u2453(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002EC6 RID: 11974 RVA: 0x001A22E8 File Offset: 0x001A12E8
	public override void ᜂ()
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
		this.ᜏ();
	}

	// Token: 0x06002EC7 RID: 11975 RVA: 0x001A232C File Offset: 0x001A132C
	public override void ᜀ(ExcelVersion A_0)
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
		this.AutoGrowData = true;
		this.ᜁ = this.ᜆ();
	}

	// Token: 0x06002EC8 RID: 11976 RVA: 0x001A237C File Offset: 0x001A137C
	protected virtual spr\u1A58 ᜆ()
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
		spr\u1A58 spr_u1A = new spr\u1A58(this);
		spr_u1A.ᜁ(new EventHandler(this.ᜀ));
		return spr_u1A;
	}

	// Token: 0x06002EC9 RID: 11977 RVA: 0x001A23D4 File Offset: 0x001A13D4
	public override int ᜀ(BinaryReader A_0, DataProvider A_1, IDecryptor A_2, byte[] A_3)
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
		this.ᜂ = new List<int>();
		this.ᜀ = new sprᡑ(A_0, A_2, A_1);
		return base.FillRecord(A_0, A_1, A_2, A_3);
	}

	// Token: 0x06002ECA RID: 11978 RVA: 0x001A2434 File Offset: 0x001A1434
	public override int ᜀ(BinaryWriter A_0, IEncryptor A_1, int A_2)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 16;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_1D3;
				case 1:
				{
					this.ᜂ.Add(this.m_iLength);
					int num2;
					num2++;
					int num3 = this.StartDecodingOffset;
					int num4 = 0;
					num = 7;
					continue;
				}
				case 2:
					goto IL_146;
				case 3:
					if (this.ᜀ.Length < this.m_iLength)
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				case 4:
					goto IL_203;
				case 5:
					if (A_1 != null)
					{
						num = 19;
						continue;
					}
					goto IL_305;
				case 6:
					if (this.m_iLength < 0)
					{
						num = 2;
						continue;
					}
					A_0.Write((ushort)this.m_iCode);
					num = 9;
					continue;
				case 7:
					goto IL_EE;
				case 8:
					goto IL_1CE;
				case 9:
					if (this.ᜃ < 0)
					{
						num = 15;
						continue;
					}
					A_0.Write((ushort)this.ᜃ);
					num = 10;
					continue;
				case 10:
					goto IL_1D3;
				case 11:
					goto IL_247;
				case 12:
				{
					int num2;
					int num4;
					if (num4 >= num2)
					{
						num = 17;
						continue;
					}
					int num5 = this.ᜂ[num4];
					int num3;
					int num6 = num5 - num3;
					spr\u24E5 provider;
					A_1.Encrypt(provider, num3, num6, (long)A_2);
					A_2 += num6 + 4;
					num3 = num5 + 4;
					num4++;
					num = 13;
					continue;
				}
				case 13:
					goto IL_EE;
				case 14:
					goto IL_88;
				case 15:
					A_0.Write((ushort)this.m_iLength);
					num = 0;
					continue;
				case 17:
					if (true)
					{
					}
					num = 11;
					continue;
				case 18:
				{
					int num2;
					if (num2 > 0)
					{
						num = 1;
						continue;
					}
					spr\u24E5 provider;
					int startDecodingOffset;
					A_1.Encrypt(provider, startDecodingOffset, this.m_iLength - startDecodingOffset, (long)(A_2 + startDecodingOffset));
					num = 8;
					continue;
				}
				case 19:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11B;
					default:
					{
						if (false)
						{
						}
						int startDecodingOffset = this.StartDecodingOffset;
						int num2 = this.ᜂ.Count;
						spr\u24E5 provider = new spr\u24E5(this.ᜀ);
						num = 18;
						continue;
					}
					}
					break;
				}
				if (A_0 == null)
				{
					num = 14;
					continue;
				}
				goto IL_11B;
				IL_EE:
				num = 12;
				continue;
				IL_11B:
				this.ᜀ(ExcelVersion.Version97to2003);
				num = 6;
				continue;
				IL_1D3:
				A_2 += 4;
				num = 3;
			}
			IL_88:
			throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
			IL_146:
			throw new ApplicationException(RecordTableEnumerator.b("ቄ㕆♈╊⩌潎͐㙒㙔㡖⭘㽚絜㭞`ᝢѤ䝦hժ୬ٮᵰὲ孴", a_));
			IL_1CE:
			goto IL_305;
			IL_203:
			throw new ApplicationException(RecordTableEnumerator.b("ॄ≆❈ⱊ㥌❎煐㱒㍔睖㵘㩚⥜㹞䅠੢ᙤ䝦๨ᥪ࡬๮հᙲݴ坶൸፺ᱼᅾꆀ力﶐뎒ﲜ쒠莢즤슦잨첪\ud9ac잮龰ﲲힴ\uddb6\udcb8\ud8ba즼龾闀뫂뗄ꋆꋊ뻌", a_) + base.GetType().Name);
			IL_247:
			IL_305:
			A_0.Write(this.ᜀ, 0, this.m_iLength);
			return this.m_iLength + 4;
		}
		}
	}

	// Token: 0x06002ECB RID: 11979 RVA: 0x001A2764 File Offset: 0x001A1764
	protected virtual bool ᜏ()
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 8;
			int count;
			for (;;)
			{
				List<byte[]> list;
				int num5;
				switch (num)
				{
				case 0:
					goto IL_F4;
				case 1:
					goto IL_63;
				case 2:
				{
					if (true)
					{
					}
					int num2;
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					byte[] array = list[num2];
					int num3 = array.Length;
					byte[] dst;
					int num4;
					Buffer.BlockCopy(array, 0, dst, num4, num3);
					num4 += num3;
					num2++;
					num = 5;
					continue;
				}
				case 3:
				{
					byte[] dst;
					this.ᜀ = dst;
					num = 0;
					continue;
				}
				case 4:
					if (count > 0)
					{
						num = 7;
						continue;
					}
					goto IL_1BC;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BF;
					default:
						if (false)
						{
						}
						goto IL_63;
					}
					break;
				case 6:
					goto IL_61;
				case 7:
				{
					byte[] dst = new byte[num5 + this.m_iLength];
					Buffer.BlockCopy(this.ᜀ, 0, dst, 0, this.m_iLength);
					int num4 = this.m_iLength;
					int num2 = 0;
					goto IL_BF;
				}
				}
				if (this.ᜀ == null)
				{
					num = 6;
					continue;
				}
				this.ᜀ.ᜂ();
				int item = this.ᜀ.Length;
				this.ᜂ.Clear();
				this.ᜂ.Add(item);
				((IEnumerator)this.ᜀ).Reset();
				list = this.ᜀ(out num5, ref item);
				count = list.Count;
				num = 4;
				continue;
				IL_63:
				num = 2;
				continue;
				IL_BF:
				num = 1;
			}
			IL_61:
			throw new ArgumentNullException(RecordTableEnumerator.b("⥃᥅ⵇ㉉㡋㱍ㅏㅑ⁓㥕⩗", a_));
			IL_F4:
			IL_1BC:
			return count > 0;
		}
		}
	}

	// Token: 0x06002ECC RID: 11980 RVA: 0x001A2934 File Offset: 0x001A1934
	protected new List<byte[]> ᜀ(out int A_0, ref int A_1)
	{
		List<byte[]> list;
		for (;;)
		{
			((IEnumerator)this.ᜀ).Reset();
			list = new List<byte[]>();
			A_0 = 0;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_40;
				case 1:
					goto IL_40;
				case 2:
					return list;
				case 3:
				{
					if (!((IEnumerator)this.ᜀ).MoveNext())
					{
						num = 2;
						continue;
					}
					if (true)
					{
					}
					int num2 = this.ᜀ(list, this.ᜀ.ᜀ());
					A_1 += num2;
					A_0 += num2;
					this.ᜂ.Add(A_1);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return list;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				}
				break;
				IL_40:
				num = 3;
			}
		}
		return list;
	}

	// Token: 0x06002ECD RID: 11981 RVA: 0x001A2A00 File Offset: 0x001A1A00
	protected new virtual int ᜀ(List<byte[]> A_0, BiffRecordRaw A_1)
	{
		int a_ = 4;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_46;
			case 1:
				goto IL_6F;
			case 3:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 3;
			}
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽ሿ❁❃⥅㩇⹉㽋", a_));
		IL_6F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_A1:
			byte[] data = A_1.Data;
			A_0.Add(data);
			return data.Length;
		}
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻崽⼿ぁ⁃", a_));
		}
	}

	// Token: 0x06002ECE RID: 11982 RVA: 0x001A2AC0 File Offset: 0x001A1AC0
	protected new virtual void ᜀ(object A_0, EventArgs A_1)
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
		spr\u1A58 spr_u1A = (spr\u1A58)A_0;
		spr_u1A.ᜀ(new EventHandler(this.ᜀ));
		this.ᜃ = spr_u1A.ᜃ();
	}

	// Token: 0x06002ECF RID: 11983 RVA: 0x001A2B24 File Offset: 0x001A1B24
	protected new void ᜀ(TBIFFRecord A_0)
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
		this.ᜀ.ᜀ(A_0);
	}

	// Token: 0x06002ED0 RID: 11984 RVA: 0x001A2B6C File Offset: 0x001A1B6C
	public virtual object ᜇ()
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
		spr\u2453 spr_u = (spr\u2453)base.Clone();
		spr_u.ᜂ = spr\u1CD3.ᜀ<int>(this.ᜂ);
		return spr_u;
	}

	// Token: 0x04001502 RID: 5378
	protected new sprᡑ ᜀ;

	// Token: 0x04001503 RID: 5379
	private new spr\u1A58 ᜁ;

	// Token: 0x04001504 RID: 5380
	protected internal new List<int> ᜂ = new List<int>();

	// Token: 0x04001505 RID: 5381
	private new int ᜃ = -1;
}
