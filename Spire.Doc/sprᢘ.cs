using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Core.Escher;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x020002E5 RID: 741
internal abstract class sprᢘ : spr\u2192
{
	// Token: 0x060028AE RID: 10414 RVA: 0x002875EC File Offset: 0x002865EC
	internal MSOBlipType ᜌ()
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
		return (MSOBlipType)(base.\u1717().ᜅ() - 61464);
	}

	// Token: 0x060028AF RID: 10415
	internal abstract PresetTexture ᜀ();

	// Token: 0x060028B0 RID: 10416
	internal new abstract byte[] ᜁ();

	// Token: 0x060028B1 RID: 10417
	internal abstract void ᜀ(byte[] A_0);

	// Token: 0x060028B2 RID: 10418
	internal abstract sprᠾ ᜂ();

	// Token: 0x060028B3 RID: 10419
	internal abstract void ᜀ(sprᠾ A_0);

	// Token: 0x060028B4 RID: 10420 RVA: 0x00287638 File Offset: 0x00286638
	internal ImageFormat ᜊ()
	{
		int a_ = 7;
		for (;;)
		{
			MSOBlipType msoblipType = this.ᜌ();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_88;
				case 1:
					switch (msoblipType)
					{
					case MSOBlipType.msoblipEMF:
						return ImageFormat.Emf;
					case MSOBlipType.msoblipWMF:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_88;
						default:
							goto IL_80;
						}
						break;
					case MSOBlipType.msoblipPICT:
						goto IL_9B;
					case MSOBlipType.msoblipJPEG:
						return ImageFormat.Jpeg;
					case MSOBlipType.msoblipPNG:
						return ImageFormat.Png;
					case MSOBlipType.msoblipDIB:
						return ImageFormat.Bmp;
					default:
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_93;
				}
				break;
				IL_88:
				num = 2;
			}
		}
		return ImageFormat.Png;
		IL_80:
		if (false)
		{
		}
		return ImageFormat.Wmf;
		IL_93:
		IL_9B:
		throw new Exception(this.ᜌ().ToString() + ClipboardData.b("Ѭᱮ兰ᵲᩴͶ奸ࡺࡼཾ", a_));
	}

	// Token: 0x060028B5 RID: 10421 RVA: 0x00287708 File Offset: 0x00286708
	internal Guid ᜈ()
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
		return this.ᜁ;
	}

	// Token: 0x060028B6 RID: 10422 RVA: 0x0028774C File Offset: 0x0028674C
	internal void ᜀ(Guid A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x060028B7 RID: 10423 RVA: 0x00287790 File Offset: 0x00286790
	internal Guid ᜉ()
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

	// Token: 0x060028B8 RID: 10424 RVA: 0x002877D4 File Offset: 0x002867D4
	internal new void ᜁ(Guid A_0)
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

	// Token: 0x060028B9 RID: 10425 RVA: 0x00287818 File Offset: 0x00286818
	internal bool ᜆ()
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
			if (this.ᜌ() == MSOBlipType.msoblipDIB)
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x060028BA RID: 10426 RVA: 0x00287864 File Offset: 0x00286864
	protected sprᢘ(Document A_0) : base(A_0)
	{
	}

	// Token: 0x060028BB RID: 10427 RVA: 0x00287878 File Offset: 0x00286878
	protected void ᜂ(Stream A_0)
	{
		for (;;)
		{
			byte[] array = new byte[16];
			A_0.Read(array, 0, array.Length);
			this.ᜁ = new Guid(array);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7A;
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7A;
					default:
						if (false)
						{
						}
						if (this.ᜋ())
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						return;
					}
					break;
				}
				break;
				IL_7A:
				array = new byte[16];
				A_0.Read(array, 0, array.Length);
				this.ᜂ = new Guid(array);
				num = 1;
			}
		}
	}

	// Token: 0x060028BC RID: 10428 RVA: 0x00287930 File Offset: 0x00286930
	internal bool ᜋ()
	{
		int num = 24;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (base.\u1717().ᜂ() != 1131)
				{
					num = 6;
					continue;
				}
				return true;
			case 1:
				if (true)
				{
				}
				num = 5;
				continue;
			case 2:
				if (base.\u1717().ᜂ() != 535)
				{
					num = 28;
					continue;
				}
				return true;
			case 3:
				goto IL_172;
			case 4:
				if (base.\u1717().ᜂ() != 1961)
				{
					num = 21;
					continue;
				}
				return true;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2B6;
				default:
					if (false)
					{
					}
					if (base.\u1717().ᜂ() != 1347)
					{
						num = 25;
						continue;
					}
					return true;
				}
				break;
			case 6:
				num = 7;
				continue;
			case 7:
				if (base.\u1717().ᜂ() != 1763)
				{
					num = 12;
					continue;
				}
				return true;
			case 8:
				goto IL_2B6;
			case 9:
				if (base.\u1717().ᜅ() != MSOFBT.msofbtBlipJPEG)
				{
					num = 27;
					continue;
				}
				goto IL_1D1;
			case 10:
				num = 8;
				continue;
			case 11:
				goto IL_1D1;
			case 12:
				goto IL_354;
			case 13:
				if (base.\u1717().ᜅ() == MSOFBT.msofbtBlipDIB)
				{
					num = 22;
					continue;
				}
				goto IL_145;
			case 14:
				if (base.\u1717().ᜅ() == (MSOFBT)61482)
				{
					num = 11;
					continue;
				}
				goto IL_354;
			case 15:
				if (base.\u1717().ᜅ() == MSOFBT.msofbtBlipWMF)
				{
					num = 17;
					continue;
				}
				goto IL_10D;
			case 16:
				if (base.\u1717().ᜂ() != 1761)
				{
					num = 19;
					continue;
				}
				return true;
			case 17:
				num = 2;
				continue;
			case 18:
				if (base.\u1717().ᜅ() == (MSOFBT)61481)
				{
					num = 29;
					continue;
				}
				return false;
			case 19:
				goto IL_B0;
			case 20:
				if (base.\u1717().ᜅ() == MSOFBT.msofbtBlipPNG)
				{
					num = 23;
					continue;
				}
				goto IL_B0;
			case 21:
				goto IL_145;
			case 22:
				num = 4;
				continue;
			case 23:
				num = 16;
				continue;
			case 25:
				goto IL_27E;
			case 26:
				if (base.\u1717().ᜅ() == (MSOFBT)61468)
				{
					num = 1;
					continue;
				}
				goto IL_27E;
			case 27:
				num = 14;
				continue;
			case 28:
				goto IL_10D;
			case 29:
				goto IL_16D;
			}
			if (base.\u1717().ᜅ() == MSOFBT.msofbtBlipEMF)
			{
				num = 10;
				continue;
			}
			goto IL_172;
			IL_B0:
			num = 13;
			continue;
			IL_10D:
			num = 26;
			continue;
			IL_145:
			num = 18;
			continue;
			IL_172:
			num = 15;
			continue;
			IL_1D1:
			num = 0;
			continue;
			IL_27E:
			num = 9;
			continue;
			IL_2B6:
			if (base.\u1717().ᜂ() != 981)
			{
				num = 3;
				continue;
			}
			return true;
			IL_354:
			num = 20;
		}
		IL_16D:
		return base.\u1717().ᜂ() == 1765;
	}

	// Token: 0x060028BD RID: 10429
	internal abstract override spr\u2192 ᜃ();

	// Token: 0x0400236E RID: 9070
	protected new const int ᜀ = 16;

	// Token: 0x0400236F RID: 9071
	private new Guid ᜁ;

	// Token: 0x04002370 RID: 9072
	private new Guid ᜂ;

	// Token: 0x04002371 RID: 9073
	private new byte[] ᜃ;
}
