using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200044D RID: 1101
[CLSCompliant(false)]
internal class spr៣ : spr\u1D3B, IDisposable, sprẫ
{
	// Token: 0x0600422E RID: 16942 RVA: 0x002515D0 File Offset: 0x002505D0
	public spr៣(spr\u1D3B A_0) : base(A_0)
	{
	}

	// Token: 0x0600422F RID: 16943 RVA: 0x002515FC File Offset: 0x002505FC
	public spr៣(spr\u1D3B A_0, byte[] A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004230 RID: 16944 RVA: 0x0025162C File Offset: 0x0025062C
	public spr៣(spr\u1D3B A_0, Stream A_1) : base(A_0, A_1, null)
	{
	}

	// Token: 0x06004231 RID: 16945 RVA: 0x0025165C File Offset: 0x0025065C
	public Image ᜇ()
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
		return this.ᜈ;
	}

	// Token: 0x06004232 RID: 16946 RVA: 0x002516A0 File Offset: 0x002506A0
	public new void ᜀ(Image A_0)
	{
		int a_ = 5;
		if (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				this.ᜈ = A_0;
				this.ᜀ();
				return;
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䴺尼匾㑀♂", a_));
	}

	// Token: 0x06004233 RID: 16947 RVA: 0x0025170C File Offset: 0x0025070C
	public new byte[] ᜄ()
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
		return this.ᜅ;
	}

	// Token: 0x06004234 RID: 16948 RVA: 0x00251750 File Offset: 0x00250750
	public new void ᜀ(byte[] A_0)
	{
		int a_ = 11;
		for (;;)
		{
			IL_09:
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Length != this.ᜅ.Length)
					{
						num = 2;
						continue;
					}
					goto IL_AB;
				case 1:
					goto IL_5A;
				case 2:
					goto IL_8D;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_09;
				default:
					if (false)
					{
					}
					if (A_0 == null)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
					break;
				}
			}
		}
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝀≂⥄㉆ⱈ", a_));
		IL_8D:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㝀≂⥄㉆ⱈ敊Ō⩎㽐㑒⅔㽖", a_));
		IL_AB:
		this.ᜅ = A_0;
	}

	// Token: 0x06004235 RID: 16949 RVA: 0x00251810 File Offset: 0x00250810
	public byte ᜁ()
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

	// Token: 0x06004236 RID: 16950 RVA: 0x00251854 File Offset: 0x00250854
	public new void ᜀ(byte A_0)
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

	// Token: 0x06004237 RID: 16951 RVA: 0x00251898 File Offset: 0x00250898
	public int ᜆ()
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
		return this.ᜇ;
	}

	// Token: 0x06004238 RID: 16952 RVA: 0x002518DC File Offset: 0x002508DC
	public new void ᜀ(int A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x06004239 RID: 16953 RVA: 0x00251920 File Offset: 0x00250920
	public new bool ᜃ()
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
		sprᜪ sprᜪ = base.\u1718() as sprᜪ;
		return sprᜪ.ᜉ() == MsoBlipType.msoblipDIB;
	}

	// Token: 0x0600423A RID: 16954 RVA: 0x00251970 File Offset: 0x00250970
	public override void ᜀ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			int num2;
			int num5;
			for (;;)
			{
				MemoryStream memoryStream;
				byte[] buffer;
				int num4;
				switch (num)
				{
				case 0:
					goto IL_D8;
				case 2:
					if (!this.ᜃ())
					{
						num = 8;
						continue;
					}
					num = 5;
					continue;
				case 3:
				{
					if (A_1 >= num2)
					{
						num = 9;
						continue;
					}
					int num3 = memoryStream.Read(buffer, 0, 10240);
					A_0.Write(buffer, 0, num3);
					A_1 += num3;
					num = 0;
					continue;
				}
				case 4:
					if (true)
					{
					}
					base.ᜀ((MsoRecords)61470);
					num = 7;
					continue;
				case 5:
					num4 = 14;
					goto IL_176;
				case 6:
					num4 = 0;
					goto IL_176;
				case 7:
					goto IL_62;
				case 8:
					num = 6;
					continue;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_54;
					default:
						goto IL_10A;
					}
					break;
				case 10:
					goto IL_D8;
				}
				goto IL_4C;
				IL_54:
				num = 4;
				continue;
				IL_4C:
				if (base.\u1717() == (MsoRecords)0)
				{
					goto IL_54;
				}
				IL_62:
				this.m_iLength = 0;
				A_0.Write(this.ᜅ, 0, 16);
				this.m_iLength += 16;
				A_0.WriteByte(this.ᜆ);
				this.m_iLength++;
				num = 2;
				continue;
				IL_D8:
				num = 3;
				continue;
				IL_176:
				num5 = num4;
				memoryStream = new MemoryStream();
				this.ᜈ.Save(memoryStream, sprᜪ.ᜀ((base.\u1718() as sprᜪ).ᜉ()));
				memoryStream.Position = 0L;
				num2 = (int)memoryStream.Length;
				buffer = new byte[10240];
				memoryStream.Position = (long)num5;
				A_1 = num5;
				num = 10;
			}
			IL_10A:
			if (false)
			{
			}
			this.m_iLength += num2 - num5;
			return;
		}
		}
	}

	// Token: 0x0600423B RID: 16955 RVA: 0x00251B6C File Offset: 0x00250B6C
	public override void ᜀ(Stream A_0)
	{
		int a_ = 10;
		if (true)
		{
		}
		if (A_0 != null)
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
				A_0.Read(this.ᜅ, 0, 16);
				int num = this.ᜁ(A_0) + 16;
				this.ᜆ = (byte)A_0.ReadByte();
				num++;
				this.ᜇ = num;
				this.ᜁ(A_0, num);
				this.ᜈ = spr\u17FF.ᜀ(this.ᜉ);
				return;
			}
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㌿㙁㙃⍅⥇❉", a_));
	}

	// Token: 0x0600423C RID: 16956 RVA: 0x00251C18 File Offset: 0x00250C18
	private int ᜁ(Stream A_0)
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

	// Token: 0x0600423D RID: 16957 RVA: 0x00251C58 File Offset: 0x00250C58
	protected override object ᜅ()
	{
		spr៣ spr៣;
		for (;;)
		{
			spr៣ = (spr៣)base.ᜅ();
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					goto IL_EF;
				case 2:
					spr៣.ᜅ = spr\u1CD3.ᜀ(this.ᜅ);
					num = 8;
					continue;
				case 3:
					return spr៣;
				case 4:
					spr៣.ᜉ = UtilityMethods.ᜀ(this.ᜉ);
					num = 1;
					continue;
				case 5:
					spr៣.ᜈ = ((this.ᜉ != null) ? spr\u17FF.ᜀ(spr៣.ᜉ) : ((Image)this.ᜈ.Clone()));
					num = 3;
					continue;
				case 6:
					if (this.ᜉ != null)
					{
						num = 4;
						continue;
					}
					goto IL_EF;
				case 7:
					if (this.ᜈ != null)
					{
						num = 0;
						continue;
					}
					return spr៣;
				case 8:
					goto IL_93;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return spr៣;
					default:
						if (false)
						{
						}
						if (this.ᜅ != null)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_93;
					}
					break;
				}
				break;
				IL_93:
				num = 6;
				continue;
				IL_EF:
				num = 7;
			}
		}
		return spr៣;
	}

	// Token: 0x0600423E RID: 16958 RVA: 0x00251DB4 File Offset: 0x00250DB4
	private void ᜁ(Stream A_0, int A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				bool flag;
				byte[] buffer;
				int num4;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_76;
					default:
						if (false)
						{
						}
						if (this.ᜉ != null)
						{
							num = 13;
							continue;
						}
						goto IL_8A;
					}
					break;
				case 1:
					if (flag)
					{
						num = 2;
						continue;
					}
					goto IL_158;
				case 2:
				{
					uint a_2 = spr\u1D3B.ᜃ(A_0);
					A_0.Position -= 4L;
					int a_3;
					spr៣.ᜀ(this.ᜉ, a_3, a_2, this.ᜀ(A_0, A_1));
					num = 4;
					continue;
				}
				case 3:
				{
					int num3;
					int num2;
					if ((num2 = A_0.Read(buffer, 0, Math.Min(10240, num3))) > 0)
					{
						num = 10;
						continue;
					}
					goto IL_22E;
				}
				case 4:
					goto IL_158;
				case 5:
					goto IL_F9;
				case 6:
					goto IL_1F7;
				case 7:
					goto IL_74;
				case 8:
					if (true)
					{
					}
					goto IL_8A;
				case 9:
				{
					int num3;
					if (num3 <= 0)
					{
						num = 5;
						continue;
					}
					int num2;
					this.ᜉ.Write(buffer, 0, num2);
					num3 -= num2;
					num = 11;
					continue;
				}
				case 10:
					num = 9;
					continue;
				case 11:
					goto IL_1F7;
				case 12:
				{
					int a_3 = num4 + (flag ? 14 : 0);
					this.ᜉ = new MemoryStream(num4 + 14);
					int num3 = num4;
					num = 1;
					continue;
				}
				case 13:
					this.ᜉ.Close();
					num = 8;
					continue;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 0;
				continue;
				IL_8A:
				num4 = this.m_iLength - A_1;
				flag = this.ᜃ();
				num = 12;
				continue;
				IL_158:
				buffer = new byte[10240];
				num = 6;
				continue;
				IL_1F7:
				num = 3;
			}
			IL_74:
			IL_76:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅁぃ㑅ⵇ⭉⅋", a_));
			IL_F9:
			IL_22E:
			this.ᜉ.Position = 0L;
			return;
		}
		}
	}

	// Token: 0x0600423F RID: 16959 RVA: 0x00251FFC File Offset: 0x00250FFC
	private new uint ᜀ(Stream A_0, int A_1)
	{
		int a_ = 14;
		if (A_0 != null)
		{
			if (true)
			{
			}
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
				byte[] array = new byte[4];
				A_0.Position += 32L;
				A_0.Read(array, 0, 4);
				A_0.Position -= 36L;
				return BitConverter.ToUInt32(array, 0);
			}
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㝃㉅㩇⽉ⵋ⍍", a_));
	}

	// Token: 0x06004240 RID: 16960 RVA: 0x00252094 File Offset: 0x00251094
	private new void ᜀ()
	{
		for (;;)
		{
			if (true)
			{
			}
			MemoryStream memoryStream = new MemoryStream();
			this.ᜈ.Save(memoryStream, sprᜪ.ᜀ((base.\u1718() as sprᜪ).ᜉ()));
			memoryStream.Position = 0L;
			try
			{
				new MD5CryptoServiceProvider().ComputeHash(memoryStream).CopyTo(this.ᜅ, 0);
			}
			catch (InvalidOperationException)
			{
				new MACTripleDES().ComputeHash(memoryStream).CopyTo(this.ᜅ, 0);
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_8C;
			}
		}
		IL_8C:
		if (false)
		{
		}
	}

	// Token: 0x06004241 RID: 16961 RVA: 0x00252144 File Offset: 0x00251144
	public new static void ᜀ(MemoryStream A_0, int A_1, uint A_2, uint A_3)
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
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(spr៣.ᜃ, 0, spr៣.ᜃ.Length);
		A_0.Write(bytes, 0, bytes.Length);
		A_0.Write(spr៣.ᜄ, 0, spr៣.ᜄ.Length);
		uint value = A_2 + 14U + A_3 * 4U;
		bytes = BitConverter.GetBytes(value);
		A_0.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x06004242 RID: 16962 RVA: 0x002521D4 File Offset: 0x002511D4
	protected override void \u171F()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_50:
			this.ᜉ.Close();
			this.ᜉ = null;
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				goto IL_50;
			case 2:
				return;
			}
			if (this.ᜉ == null)
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06004243 RID: 16963 RVA: 0x0025225C File Offset: 0x0025125C
	// Note: this type is marked as 'beforefieldinit'.
	static spr៣()
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
		spr៣.ᜃ = new byte[]
		{
			66,
			77
		};
		byte[] array = new byte[4];
		spr៣.ᜄ = array;
	}

	// Token: 0x04001D48 RID: 7496
	public new const int ᜀ = 14;

	// Token: 0x04001D49 RID: 7497
	public new const int ᜁ = 32;

	// Token: 0x04001D4A RID: 7498
	private new const uint ᜂ = 4U;

	// Token: 0x04001D4B RID: 7499
	private new static readonly byte[] ᜃ;

	// Token: 0x04001D4C RID: 7500
	private new static readonly byte[] ᜄ;

	// Token: 0x04001D4D RID: 7501
	private new byte[] ᜅ = new byte[16];

	// Token: 0x04001D4E RID: 7502
	private new byte ᜆ = byte.MaxValue;

	// Token: 0x04001D4F RID: 7503
	private int ᜇ;

	// Token: 0x04001D50 RID: 7504
	private Image ᜈ;

	// Token: 0x04001D51 RID: 7505
	private MemoryStream ᜉ;
}
