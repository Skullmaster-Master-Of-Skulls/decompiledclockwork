using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x0200031C RID: 796
internal class spr\u1BA5 : sprᝋ
{
	// Token: 0x06002B4B RID: 11083 RVA: 0x002A6B7C File Offset: 0x002A5B7C
	internal spr\u1BA5()
	{
	}

	// Token: 0x06002B4C RID: 11084 RVA: 0x002A6B90 File Offset: 0x002A5B90
	internal spr\u1BA5(Guid A_0, byte[] A_1, PresetTexture A_2)
	{
		int a_ = 19;
		base..ctor(A_0);
		this.ᜂ = A_2;
		base.ᜂ(spr\u1D5F.ᜁ(spr\u2075.\u171B(A_1)));
		switch (base.ᜂ())
		{
		case ImageType.Jpeg:
		case ImageType.Png:
		case ImageType.Bitmap:
			this.ᜁ = A_1;
			return;
		}
		throw new InvalidOperationException(ClipboardData.b("㝸ᑺॼ彾ﮂﾊ놐ﲒﾖﲘ붜철슢스슦覨춪슬\uddae\udcb0튲솴쒶馸펺\ud8bc춾꓀", a_));
	}

	// Token: 0x06002B4D RID: 11085 RVA: 0x002A6C04 File Offset: 0x002A5C04
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
		this.ᜂ = (PresetTexture)A_0.ReadByte();
		int num2 = (int)A_0.BaseStream.Position - num;
		int a_ = base.ᜆ().ᜄ() - num2;
		this.ᜀ = new spr\u1BA5.ᜀ(A_0, (int)A_0.BaseStream.Position, a_, base.ᜂ());
		A_0.BaseStream.Position = (long)(num + base.ᜆ().ᜄ());
	}

	// Token: 0x06002B4E RID: 11086 RVA: 0x002A6CB4 File Offset: 0x002A5CB4
	protected override void ᜀ(BinaryWriter A_0)
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
			base.ᜁ(A_0);
			A_0.Write((byte)this.ᜂ);
			if (base.ᜂ() != ImageType.Bitmap)
			{
				A_0.Write(this.ᜁ);
				return;
			}
			break;
		}
		A_0.Write(this.ᜁ, 14, this.ᜁ.Length - 14);
	}

	// Token: 0x06002B4F RID: 11087 RVA: 0x002A6D34 File Offset: 0x002A5D34
	internal override byte[] ᜀ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_52:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 1;
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
				if (this.ᜀ != null)
				{
					num = 4;
					continue;
				}
				goto IL_99;
			case 2:
				num = 0;
				continue;
			case 3:
				goto IL_75;
			case 4:
				this.ᜁ = this.ᜀ.ᜀ();
				num = 3;
				continue;
			}
			break;
		}
		if (this.ᜁ == null)
		{
			goto IL_52;
		}
		IL_75:
		IL_99:
		return this.ᜁ;
	}

	// Token: 0x06002B50 RID: 11088 RVA: 0x002A6DE0 File Offset: 0x002A5DE0
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
		return this.ᜂ;
	}

	// Token: 0x04002550 RID: 9552
	private new spr\u1BA5.ᜀ ᜀ;

	// Token: 0x04002551 RID: 9553
	private new byte[] ᜁ;

	// Token: 0x04002552 RID: 9554
	private new PresetTexture ᜂ;

	// Token: 0x0200031D RID: 797
	private new class ᜀ
	{
		// Token: 0x06002B51 RID: 11089 RVA: 0x002A6E24 File Offset: 0x002A5E24
		internal ᜀ(BinaryReader A_0, int A_1, int A_2, ImageType A_3)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
			this.ᜂ = A_2;
			this.ᜃ = A_3;
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x002A6E54 File Offset: 0x002A5E54
		internal byte[] ᜀ()
		{
			this.ᜀ.BaseStream.Position = (long)this.ᜁ;
			if (this.ᜃ != ImageType.Bitmap)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_20;
				}
				if (false)
				{
				}
				return this.ᜀ.ReadBytes(this.ᜂ);
			}
			IL_20:
			if (true)
			{
			}
			return spr\u2075.ᜀ(this.ᜀ, this.ᜂ);
		}

		// Token: 0x04002553 RID: 9555
		private readonly BinaryReader ᜀ;

		// Token: 0x04002554 RID: 9556
		private readonly int ᜁ;

		// Token: 0x04002555 RID: 9557
		private readonly int ᜂ;

		// Token: 0x04002556 RID: 9558
		private readonly ImageType ᜃ;
	}
}
