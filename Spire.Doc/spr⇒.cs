using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Spire.Doc.Core.Escher;

// Token: 0x020003F8 RID: 1016
internal class spr\u21D2 : spr\u2096
{
	// Token: 0x060038DD RID: 14557 RVA: 0x003522A4 File Offset: 0x003512A4
	internal Metafile ᜀ()
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
		return this.ᜐ;
	}

	// Token: 0x060038DE RID: 14558 RVA: 0x003522E8 File Offset: 0x003512E8
	internal void ᜀ(Metafile A_0)
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
		this.ᜐ = A_0;
	}

	// Token: 0x060038DF RID: 14559 RVA: 0x0035232C File Offset: 0x0035132C
	public spr\u21D2()
	{
		this.ᜀ = new byte[16];
		this.ᜁ = new byte[16];
	}

	// Token: 0x060038E0 RID: 14560 RVA: 0x0035235C File Offset: 0x0035135C
	public override Image ᜀ(Stream A_0, int A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			MemoryStream memoryStream;
			MemoryStream memoryStream2;
			for (;;)
			{
				IL_49:
				int num = 0;
				for (;;)
				{
					IL_4B:
					int num2 = 1;
					for (;;)
					{
						byte[] array;
						int num3;
						sprᢹ sprᢹ;
						switch (num2)
						{
						case 0:
							memoryStream.Write(array, 0, num3);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4B;
							default:
								if (false)
								{
								}
								num2 = 5;
								continue;
							}
							break;
						case 1:
							goto IL_A5;
						case 2:
							sprᢹ = new sprᢹ(memoryStream2);
							memoryStream = new MemoryStream();
							array = new byte[4096];
							num2 = 4;
							continue;
						case 3:
							if (num3 > 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_C4;
						case 4:
							goto IL_56;
						case 5:
							if (true)
							{
							}
							goto IL_56;
						case 6:
							if (num >= 16)
							{
								num2 = 7;
								continue;
							}
							this.ᜀ[num] = (byte)A_0.ReadByte();
							num++;
							num2 = 8;
							continue;
						case 7:
							this.ᜅ = spr\u23F8.ᜁ(A_0);
							this.ᜆ = spr\u23F8.ᜁ(A_0);
							this.ᜇ = spr\u23F8.ᜁ(A_0);
							this.ᜈ = spr\u23F8.ᜁ(A_0);
							this.ᜉ = spr\u23F8.ᜁ(A_0);
							this.ᜊ = spr\u23F8.ᜁ(A_0);
							this.ᜋ = spr\u23F8.ᜁ(A_0);
							this.ᜂ = spr\u23F8.ᜃ(A_0);
							this.ᜃ = (byte)A_0.ReadByte();
							this.\u170D = (byte)A_0.ReadByte();
							this.ᜄ = new byte[this.ᜂ];
							A_0.Read(this.ᜄ, 0, this.ᜄ.Length);
							memoryStream2 = new MemoryStream(this.ᜄ);
							num2 = 9;
							continue;
						case 8:
							goto IL_A5;
						case 9:
							if (this.ᜃ == 0)
							{
								num2 = 2;
								continue;
							}
							goto IL_207;
						}
						goto IL_49;
						IL_56:
						num3 = sprᢹ.ᜀ(array, 0, array.Length);
						num2 = 3;
						continue;
						IL_A5:
						num2 = 6;
					}
				}
			}
			IL_C4:
			memoryStream.Position = 0L;
			return new Metafile(memoryStream);
			IL_207:
			return new Metafile(memoryStream2);
		}
		}
	}

	// Token: 0x060038E1 RID: 14561 RVA: 0x00352578 File Offset: 0x00351578
	internal override void ᜀ(Stream A_0, MemoryStream A_1, MSOBlipType A_2, byte[] A_3)
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
		this.ᜏ = A_1.ToArray();
		this.ᜅ = this.ᜏ.Length;
		this.ᜎ = this.ᜏ;
		Rectangle bounds = this.ᜐ.GetMetafileHeader().Bounds;
		this.ᜆ = bounds.Left;
		this.ᜇ = bounds.Top;
		this.ᜈ = bounds.Right;
		this.ᜉ = bounds.Bottom;
		this.ᜊ = bounds.Width * 12700;
		this.ᜋ = bounds.Height * 12700;
		this.ᜌ = CompressionMethod.msocompressionNone;
		this.\u170D = 254;
		this.ᜀ(A_0, (long)this.ᜅ, A_3);
		A_0.Write(A_3, 0, A_3.Length);
		spr\u23F8.ᜁ(A_0, this.ᜅ);
		spr\u23F8.ᜁ(A_0, this.ᜆ);
		spr\u23F8.ᜁ(A_0, this.ᜇ);
		spr\u23F8.ᜁ(A_0, this.ᜈ);
		spr\u23F8.ᜁ(A_0, this.ᜉ);
		spr\u23F8.ᜁ(A_0, this.ᜊ);
		spr\u23F8.ᜁ(A_0, this.ᜋ);
		spr\u23F8.ᜁ(A_0, this.ᜎ.Length);
		A_0.WriteByte((byte)this.ᜌ);
		A_0.WriteByte(this.\u170D);
		A_0.Write(this.ᜎ, 0, this.ᜎ.Length);
	}

	// Token: 0x060038E2 RID: 14562 RVA: 0x00352708 File Offset: 0x00351708
	private void ᜀ(Stream A_0, long A_1, byte[] A_2)
	{
		spr\u224B spr_u224B;
		spr\u1D43 spr_u1D;
		for (;;)
		{
			spr_u224B = new spr\u224B();
			spr_u224B.ᜀ(MSOFBT.msofbtBSE);
			spr_u224B.ᜁ(2U);
			spr_u224B.ᜂ(2U);
			spr_u224B.ᜀ((uint)(A_1 + 94L));
			spr_u224B.ᜀ(A_0);
			spr_u1D = new spr\u1D43();
			spr_u1D.ᜀ(MSOBlipType.msoblipEMF);
			spr_u1D.ᜁ(MSOBlipType.msoblipPICT);
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_6A;
				case 1:
					if (num >= 16)
					{
						num2 = 2;
						continue;
					}
					spr_u1D.ᜀ()[num] = A_2[num];
					num++;
					if (true)
					{
					}
					num2 = 3;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_C8;
					}
					break;
				case 3:
					goto IL_6A;
				}
				break;
				IL_6A:
				num2 = 1;
			}
		}
		IL_C8:
		if (false)
		{
		}
		spr_u1D.ᜀ(MSOBlipUsage.msoblipUsageDefault);
		spr_u1D.ᜂ(0);
		spr_u1D.ᜂ((uint)(A_1 + 58L));
		spr_u1D.ᜀ(68U);
		spr_u1D.ᜁ(1U);
		spr_u1D.ᜀ(255);
		spr_u1D.ᜀ(0);
		spr_u1D.ᜁ(0);
		spr_u1D.ᜀ(A_0);
		spr_u224B = new spr\u224B();
		spr_u224B.ᜀ((uint)A_1 + 50U);
		spr_u224B.ᜀ(MSOFBT.msofbtBlipEMF);
		spr_u224B.ᜁ(980U);
		spr_u224B.ᜂ(0U);
		spr_u224B.ᜀ(A_0);
	}

	// Token: 0x060038E3 RID: 14563 RVA: 0x00352864 File Offset: 0x00351864
	internal override void \u170D()
	{
		for (;;)
		{
			IL_14:
			base.\u170D();
			this.ᜀ = null;
			this.ᜁ = null;
			this.ᜎ = null;
			this.ᜏ = null;
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜐ.Dispose();
					this.ᜐ = null;
					num = 1;
					continue;
				case 1:
					goto IL_72;
				case 2:
					if (this.ᜐ != null)
					{
						num = 0;
						continue;
					}
					goto IL_74;
				}
				goto IL_14;
			}
			IL_74:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				goto IL_8A;
			}
			IL_72:
			goto IL_74;
		}
		IL_8A:
		if (false)
		{
		}
	}

	// Token: 0x04002A74 RID: 10868
	private new byte[] ᜀ;

	// Token: 0x04002A75 RID: 10869
	private new byte[] ᜁ;

	// Token: 0x04002A76 RID: 10870
	private new uint ᜂ;

	// Token: 0x04002A77 RID: 10871
	private new byte ᜃ;

	// Token: 0x04002A78 RID: 10872
	private new byte[] ᜄ;

	// Token: 0x04002A79 RID: 10873
	private new int ᜅ;

	// Token: 0x04002A7A RID: 10874
	private int ᜆ;

	// Token: 0x04002A7B RID: 10875
	private int ᜇ;

	// Token: 0x04002A7C RID: 10876
	private int ᜈ;

	// Token: 0x04002A7D RID: 10877
	private int ᜉ;

	// Token: 0x04002A7E RID: 10878
	private int ᜊ;

	// Token: 0x04002A7F RID: 10879
	private int ᜋ;

	// Token: 0x04002A80 RID: 10880
	private CompressionMethod ᜌ;

	// Token: 0x04002A81 RID: 10881
	private new byte \u170D;

	// Token: 0x04002A82 RID: 10882
	private byte[] ᜎ;

	// Token: 0x04002A83 RID: 10883
	private byte[] ᜏ;

	// Token: 0x04002A84 RID: 10884
	private Metafile ᜐ;
}
