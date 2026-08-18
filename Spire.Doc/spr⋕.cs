using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;
using Spire.Pdf.General.Paper.Drawing.Ps;

// Token: 0x020002C5 RID: 709
internal class spr\u22D5 : sprᝋ
{
	// Token: 0x06002695 RID: 9877 RVA: 0x00261E20 File Offset: 0x00260E20
	internal spr\u22D5()
	{
	}

	// Token: 0x06002696 RID: 9878 RVA: 0x00261E34 File Offset: 0x00260E34
	internal spr\u22D5(Guid A_0, byte[] A_1)
	{
		int a_ = 11;
		base..ctor(A_0);
		base.ᜂ(spr\u1D5F.ᜁ(spr\u2075.\u171B(A_1)));
		switch (base.ᜂ())
		{
		case ImageType.Emf:
		case ImageType.Pict:
		case ImageType.Metafile:
			this.ᜂ = A_1;
			return;
		}
		throw new InvalidOperationException(ClipboardData.b("㽰ᱲŴ坶ᱸͺർ᩾ꮊ歷떔ﺖ漢煮爵膠얢쪤햦쒨쪪\ud9ac辮\ud9b0횲잴튶鞸", a_));
	}

	// Token: 0x06002697 RID: 9879 RVA: 0x00261EA4 File Offset: 0x00260EA4
	protected override void ᜀ(BinaryReader A_0)
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
		int num = (int)A_0.BaseStream.Position;
		base.ᜂ(A_0);
		this.ᜁ = new spr\u22D5.ᜀ(A_0, (int)A_0.BaseStream.Position);
		A_0.BaseStream.Position = (long)(num + base.ᜆ().ᜄ());
	}

	// Token: 0x06002698 RID: 9880 RVA: 0x00261F24 File Offset: 0x00260F24
	protected override void ᜀ(BinaryWriter A_0)
	{
		byte[] array = spr\u2075.ᜐ(this.ᜂ);
		base.ᜁ(A_0);
		A_0.Write(array.Length);
		spr\u2481 spr_u = spr\u2075.\u171A(this.ᜂ);
		A_0.Write(spr_u.ᜌ());
		A_0.Write(spr_u.ᜉ());
		A_0.Write(spr_u.ᜐ());
		A_0.Write(spr_u.ᜂ());
		A_0.Write(spr_u.\u170D());
		A_0.Write(spr_u.ᜋ());
		if (array.Length <= 16384)
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
				A_0.Write(array.Length);
				A_0.Write(254);
				A_0.Write(254);
				A_0.Write(array);
				return;
			}
		}
		if (true)
		{
		}
		MemoryStream memoryStream = new MemoryStream(array);
		MemoryStream memoryStream2 = new MemoryStream();
		spr\u258F.ᜀ(memoryStream, memoryStream2, PsZipMethod.Zlib);
		A_0.Write((int)memoryStream2.Length);
		A_0.Write(0);
		A_0.Write(254);
		A_0.Write(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length);
		memoryStream2.Close();
		memoryStream.Close();
	}

	// Token: 0x06002699 RID: 9881 RVA: 0x00262058 File Offset: 0x00261058
	internal override byte[] ᜀ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				case 1:
					goto IL_89;
				default:
					goto IL_89;
				}
				IL_36:
				this.ᜂ = this.ᜁ.ᜀ();
				num = 4;
				continue;
				IL_89:
				if (true)
				{
				}
				if (false)
				{
				}
				goto IL_36;
			case 2:
				num = 3;
				continue;
			case 3:
				if (this.ᜁ != null)
				{
					num = 0;
					continue;
				}
				goto IL_99;
			case 4:
				goto IL_4F;
			}
			if (this.ᜂ != null)
			{
				break;
			}
			num = 2;
		}
		IL_4F:
		IL_99:
		return this.ᜂ;
	}

	// Token: 0x0600269A RID: 9882 RVA: 0x00262104 File Offset: 0x00261104
	internal override PresetTexture ᜁ()
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
		return PresetTexture.Custom;
	}

	// Token: 0x04002265 RID: 8805
	private new const int ᜀ = 16384;

	// Token: 0x04002266 RID: 8806
	private new spr\u22D5.ᜀ ᜁ;

	// Token: 0x04002267 RID: 8807
	private new byte[] ᜂ;

	// Token: 0x020002C6 RID: 710
	private new class ᜀ
	{
		// Token: 0x0600269B RID: 9883 RVA: 0x00262144 File Offset: 0x00261144
		internal ᜀ(BinaryReader A_0, int A_1)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x00262168 File Offset: 0x00261168
		internal byte[] ᜀ()
		{
			int a_ = 18;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_209:
				num = 9;
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					goto IL_64;
				}
				break;
			}
			byte[] array;
			int a_2;
			int num2;
			spr\u2481 a_3;
			for (;;)
			{
				IL_35:
				EsBlipCompression esBlipCompression;
				switch (num)
				{
				case 0:
					if (esBlipCompression != EsBlipCompression.Deflate)
					{
						num = 1;
						continue;
					}
					try
					{
						array = spr\u258F.ᜀ(array, a_2, PsZipMethod.Zlib);
						goto IL_18F;
					}
					catch (Exception)
					{
						array = spr\u1CC6.ᜁ();
						goto IL_18F;
					}
					goto IL_144;
				case 1:
					num = 7;
					continue;
				case 2:
					if (num2 <= 0)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					goto IL_144;
				case 3:
					array = spr\u2075.ᜀ(array, a_3);
					num = 5;
					continue;
				case 4:
					goto IL_114;
				case 5:
					goto IL_1CB;
				case 6:
					goto IL_209;
				case 7:
					if (esBlipCompression != EsBlipCompression.None)
					{
						num = 6;
						continue;
					}
					goto IL_18F;
				case 8:
					if (spr\u2075.ᜆ(array))
					{
						num = 3;
						continue;
					}
					return array;
				case 9:
					goto IL_1D9;
				}
				goto IL_64;
				IL_144:
				EsBlipCompression esBlipCompression2 = (EsBlipCompression)this.ᜀ.ReadByte();
				this.ᜀ.ReadByte();
				array = this.ᜀ.ReadBytes(num2);
				esBlipCompression = esBlipCompression2;
				num = 0;
				continue;
				IL_18F:
				num = 8;
			}
			IL_114:
			return null;
			IL_1CB:
			return array;
			IL_1D9:
			throw new InvalidOperationException(ClipboardData.b("ㅷᑹ੻ώꚅﮑ뢗蓮킟킡솣향\udba7쎩쎫삭邯\udfb1톳습킷햹\ud8bb邽", a_));
			IL_64:
			this.ᜀ.BaseStream.Position = (long)this.ᜁ;
			a_2 = this.ᜀ.ReadInt32();
			int a_4 = this.ᜀ.ReadInt32();
			int a_5 = this.ᜀ.ReadInt32();
			int a_6 = this.ᜀ.ReadInt32();
			int a_7 = this.ᜀ.ReadInt32();
			int a_8 = this.ᜀ.ReadInt32();
			int a_9 = this.ᜀ.ReadInt32();
			a_3 = spr\u2481.ᜀ(a_4, a_5, a_6, a_7, a_8, a_9);
			num2 = this.ᜀ.ReadInt32();
			num = 2;
			goto IL_35;
		}

		// Token: 0x04002268 RID: 8808
		private readonly BinaryReader ᜀ;

		// Token: 0x04002269 RID: 8809
		private readonly int ᜁ;
	}
}
