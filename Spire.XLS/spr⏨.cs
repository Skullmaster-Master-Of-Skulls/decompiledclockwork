using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x0200025E RID: 606
[CLSCompliant(false)]
internal abstract class spr\u23E8 : sprῄ
{
	// Token: 0x06002478 RID: 9336 RVA: 0x00153F64 File Offset: 0x00152F64
	public spr\u23E8()
	{
	}

	// Token: 0x06002479 RID: 9337 RVA: 0x00153F94 File Offset: 0x00152F94
	public virtual TBIFFRecord ᜌ()
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
		return TBIFFRecord.Continue;
	}

	// Token: 0x0600247A RID: 9338 RVA: 0x00153FD4 File Offset: 0x00152FD4
	protected virtual bool ᜄ()
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
		return false;
	}

	// Token: 0x0600247B RID: 9339 RVA: 0x00154010 File Offset: 0x00153010
	public override int ᜀ(BinaryReader A_0, DataProvider A_1, IDecryptor A_2, byte[] A_3)
	{
		switch (0)
		{
		default:
		{
			Stream baseStream;
			long position;
			int num4;
			for (;;)
			{
				this.ᜁ = new List<int>();
				baseStream = A_0.BaseStream;
				position = baseStream.Position;
				A_1.Read(A_0, 0, 4, A_3);
				int num = A_1.ReadInt32(0);
				this.m_iCode = (num & this.ᜀ);
				num >>= 16;
				this.m_iLength = (num & this.ᜀ);
				int num2 = this.m_iLength;
				int num3 = 0;
				num4 = 0;
				int num5 = 0;
				int num6 = (int)this.ᜌ();
				int num7 = 4;
				for (;;)
				{
					switch (num7)
					{
					case 0:
						if (num3 != 60)
						{
							num7 = 21;
							continue;
						}
						goto IL_321;
					case 1:
						if (this.ᜄ())
						{
							num7 = 7;
							continue;
						}
						goto IL_1F7;
					case 2:
						if (A_2 != null)
						{
							num7 = 6;
							continue;
						}
						goto IL_38D;
					case 3:
					{
						if (this.ᜄ())
						{
							num7 = 12;
							continue;
						}
						int num8 = 0;
						num7 = 17;
						continue;
					}
					case 4:
						goto IL_321;
					case 5:
						goto IL_38D;
					case 6:
					{
						int num9 = 0;
						long num10 = position + 4L;
						int num11 = 0;
						num7 = 13;
						continue;
					}
					case 7:
						num4 += 4;
						num7 = 22;
						continue;
					case 8:
						num7 = 1;
						continue;
					case 9:
					{
						int num11;
						if (num11 >= num5)
						{
							num7 = 19;
							continue;
						}
						int num12 = this.ᜁ[num11];
						int num9;
						int num13 = num12 - num9;
						long num10;
						A_2.Decrypt(this.ᜀ, num9, num13, num10);
						num10 += (long)(num13 + 4);
						num9 = num12 + 4;
						num11++;
						num7 = 15;
						continue;
					}
					case 10:
						if (num3 != num6)
						{
							num7 = 14;
							continue;
						}
						goto IL_321;
					case 11:
					{
						int num8;
						if (num8 >= num5)
						{
							num7 = 20;
							continue;
						}
						A_1.Read(A_0, 0, 4, A_3);
						num2 = (int)A_1.ReadInt16(2);
						int num14;
						this.ᜀ.Read(A_0, num14, num2, A_3, A_2);
						num14 += num2;
						num8++;
						num7 = 18;
						continue;
					}
					case 12:
						baseStream.Position += 4L;
						this.ᜀ.Read(A_0, 0, num4, A_3);
						num7 = 2;
						continue;
					case 13:
						goto IL_E9;
					case 14:
					{
						this.ᜀ.EnsureCapacity(num4);
						baseStream.Position = position;
						int num14 = 0;
						num7 = 3;
						continue;
					}
					case 15:
						goto IL_E9;
					case 16:
						if (true)
						{
						}
						if (num5 > 0)
						{
							num7 = 8;
							continue;
						}
						goto IL_1F7;
					case 17:
						goto IL_184;
					case 18:
						goto IL_184;
					case 19:
						num7 = 5;
						continue;
					case 20:
						goto IL_38D;
					case 21:
						num7 = 10;
						continue;
					case 22:
						goto IL_1F7;
					}
					break;
					IL_E9:
					num7 = 9;
					continue;
					IL_184:
					num7 = 11;
					continue;
					IL_1F7:
					baseStream.Position += (long)num2;
					num4 += num2;
					num5++;
					this.ᜁ.Add(num4);
					A_1.Read(A_0, 0, 4, A_3);
					num = A_1.ReadInt32(0);
					num3 = (num & this.ᜀ);
					num >>= 16;
					num2 = (num & this.ᜀ);
					num7 = 0;
					continue;
					IL_321:
					num7 = 16;
					continue;
					IL_38D:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_321;
					default:
						goto IL_3A3;
					}
				}
			}
			IL_3A3:
			if (false)
			{
			}
			this.m_iLength = num4;
			this.ᜀ();
			return (int)(baseStream.Position - position);
		}
		}
	}

	// Token: 0x0600247C RID: 9340 RVA: 0x001543E0 File Offset: 0x001533E0
	public override int ᜀ(BinaryWriter A_0, DataProvider A_1, IEncryptor A_2, int A_3)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				int num2;
				int num3;
				int count;
				int num6;
				switch (num)
				{
				case 0:
					if (A_2 != null)
					{
						num = 8;
						continue;
					}
					goto IL_1A1;
				case 1:
					goto IL_315;
				case 3:
					if (this.m_iLength < 0)
					{
						num = 25;
						continue;
					}
					A_0.Write((ushort)this.m_iCode);
					A_3 += 2;
					num = 18;
					continue;
				case 4:
				{
					if (this.ᜁ.Count > 0)
					{
						num = 26;
						continue;
					}
					int startDecodingOffset;
					A_2.Encrypt(this.ᜀ, startDecodingOffset, this.m_iLength - startDecodingOffset, (long)(A_3 + startDecodingOffset));
					num = 23;
					continue;
				}
				case 5:
					if (!this.NeedDataArray)
					{
						num = 10;
						continue;
					}
					goto IL_3FE;
				case 6:
					if (this.ᜁ[num2 - 1] != this.m_iLength)
					{
						num = 7;
						continue;
					}
					goto IL_17A;
				case 7:
					this.ᜁ.Add(this.m_iLength);
					num2++;
					num = 13;
					continue;
				case 8:
				{
					int startDecodingOffset = this.StartDecodingOffset;
					num = 4;
					continue;
				}
				case 9:
					if (!this.ᜄ())
					{
						num = 28;
						continue;
					}
					goto IL_17A;
				case 10:
					this.ᜀ.Clear();
					num = 19;
					continue;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21B;
					default:
					{
						if (false)
						{
						}
						if (num3 >= count)
						{
							if (true)
							{
							}
							num = 22;
							continue;
						}
						int num4 = this.ᜁ[num3];
						int num5 = num4 - num6;
						A_2.Encrypt(this.ᜀ, num6, num5, (long)A_3);
						A_3 += num5 + 4;
						num6 = num4 + 4;
						num3++;
						num = 16;
						continue;
					}
					}
					break;
				case 12:
					goto IL_12E;
				case 13:
					goto IL_17A;
				case 14:
					goto IL_AF;
				case 15:
					goto IL_33E;
				case 16:
					goto IL_33E;
				case 17:
					goto IL_1A1;
				case 18:
					if (this.ᜂ < 0)
					{
						num = 24;
						continue;
					}
					A_0.Write((ushort)this.ᜂ);
					num = 1;
					continue;
				case 19:
					goto IL_280;
				case 20:
					this.ᜀ(ExcelVersion.Version97to2003);
					num = 12;
					continue;
				case 21:
					if (base.NeedInfill)
					{
						num = 20;
						continue;
					}
					goto IL_12E;
				case 22:
					num = 17;
					continue;
				case 23:
					goto IL_1A1;
				case 24:
					A_0.Write((ushort)this.m_iLength);
					num = 27;
					continue;
				case 25:
					goto IL_152;
				case 26:
					goto IL_21B;
				case 27:
					goto IL_315;
				case 28:
					num = 6;
					continue;
				}
				if (A_0 == null)
				{
					num = 14;
					continue;
				}
				num = 21;
				continue;
				IL_12E:
				num = 3;
				continue;
				IL_17A:
				num6 = this.StartDecodingOffset;
				num3 = 0;
				count = this.ᜁ.Count;
				num = 15;
				continue;
				IL_1A1:
				byte[] arrBuffer = ((spr\u24E5)A_1).ᜅ();
				this.ᜀ.WriteInto(A_0, 0, this.m_iLength, arrBuffer);
				num = 5;
				continue;
				IL_21B:
				num2 = this.ᜁ.Count;
				num = 9;
				continue;
				IL_315:
				A_3 += 2;
				num = 0;
				continue;
				IL_33E:
				num = 11;
			}
			IL_AF:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
			IL_152:
			throw new ApplicationException(RecordTableEnumerator.b("ፃ㑅❇⑉⭋湍ɏ㝑㝓㥕⩗㹙籛㩝şᙡգ䙥ŧѩ੫ݭᱯṱ婳", a_));
			IL_280:
			IL_3FE:
			base.NeedInfill = true;
			return this.m_iLength + 4;
		}
		}
	}

	// Token: 0x0600247D RID: 9341 RVA: 0x001547FC File Offset: 0x001537FC
	public override object ᜂ()
	{
		spr\u23E8 spr_u23E;
		for (;;)
		{
			IL_14:
			spr_u23E = (spr\u23E8)base.ᜂ();
			spr_u23E.ᜁ = spr\u1CD3.ᜀ<int>(this.ᜁ);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜀ != null)
					{
						num = 2;
						continue;
					}
					goto IL_A9;
				case 1:
					goto IL_95;
				case 2:
					spr_u23E.ᜀ = spr\u17FF.ᜀ();
					spr_u23E.ᜀ.EnsureCapacity(this.ᜀ.Capacity);
					this.ᜀ.CopyTo(0, spr_u23E.ᜀ, 0, this.ᜀ.Capacity);
					num = 1;
					continue;
				}
				goto IL_14;
			}
			IL_A9:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				goto IL_BF;
			}
			IL_95:
			if (true)
			{
			}
			goto IL_A9;
		}
		IL_BF:
		if (false)
		{
		}
		return spr_u23E;
	}

	// Token: 0x04001279 RID: 4729
	private new int ᜀ = 65535;

	// Token: 0x0400127A RID: 4730
	internal List<int> ᜁ = new List<int>();

	// Token: 0x0400127B RID: 4731
	protected new int ᜂ = -1;
}
