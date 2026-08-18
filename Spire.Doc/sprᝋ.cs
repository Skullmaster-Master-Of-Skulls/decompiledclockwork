using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x020002C4 RID: 708
internal abstract class sprᝋ : spr\u171F
{
	// Token: 0x06002688 RID: 9864 RVA: 0x002619FC File Offset: 0x002609FC
	internal sprᝋ()
	{
	}

	// Token: 0x06002689 RID: 9865 RVA: 0x00261A28 File Offset: 0x00260A28
	internal sprᝋ(Guid A_0)
	{
		this.ᜁ = A_0;
	}

	// Token: 0x0600268A RID: 9866 RVA: 0x00261A58 File Offset: 0x00260A58
	internal new Guid ᜃ()
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

	// Token: 0x0600268B RID: 9867 RVA: 0x00261A9C File Offset: 0x00260A9C
	internal ImageType ᜂ()
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
		return sprᝋ.ᜀ(base.ᜆ().ᜅ());
	}

	// Token: 0x0600268C RID: 9868 RVA: 0x00261AE8 File Offset: 0x00260AE8
	internal void ᜂ(ImageType A_0)
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
		base.ᜆ().ᜀ(sprᝋ.ᜁ(A_0));
		base.ᜆ().ᜁ((int)sprᝋ.ᜀ(A_0));
	}

	// Token: 0x0600268D RID: 9869 RVA: 0x00261B48 File Offset: 0x00260B48
	private static ImageType ᜀ(EsRecordType A_0)
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
		return (ImageType)(A_0 - 61464);
	}

	// Token: 0x0600268E RID: 9870 RVA: 0x00261B8C File Offset: 0x00260B8C
	private static EsRecordType ᜁ(ImageType A_0)
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
		return (EsRecordType)(A_0 + 61464);
	}

	// Token: 0x0600268F RID: 9871 RVA: 0x00261BD0 File Offset: 0x00260BD0
	private static EsBlipInstance ᜀ(ImageType A_0)
	{
		int a_ = 2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return EsBlipInstance.Emf;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A2;
					case 1:
						num = 0;
						continue;
					case 2:
						switch (A_0)
						{
						case ImageType.Emf:
							return EsBlipInstance.Emf;
						case ImageType.Pict:
							return EsBlipInstance.Pict;
						case ImageType.Jpeg:
							return EsBlipInstance.Jpeg;
						case ImageType.Png:
							return EsBlipInstance.Png;
						case ImageType.Metafile:
							return EsBlipInstance.Wmf;
						case ImageType.Bitmap:
							return EsBlipInstance.Bmp;
						default:
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
				break;
			}
			}
		}
		return EsBlipInstance.Jpeg;
		IL_A2:
		throw new InvalidOperationException(ClipboardData.b("㵧ѩݫmὯձᩳ噵ᅷ᝹ᵻ᥽ꊁ慎揄뺏", a_));
	}

	// Token: 0x06002690 RID: 9872
	internal abstract byte[] ᜀ();

	// Token: 0x06002691 RID: 9873
	internal abstract PresetTexture ᜁ();

	// Token: 0x06002692 RID: 9874 RVA: 0x00261CA0 File Offset: 0x00260CA0
	protected void ᜂ(BinaryReader A_0)
	{
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜁ = sprᝋ.ᜁ(A_0);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if ((base.ᜆ().ᜂ() & 1) != 0)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					return;
				case 2:
					this.ᜂ = sprᝋ.ᜁ(A_0);
					num = 1;
					continue;
				}
				break;
			}
		}
	}

	// Token: 0x06002693 RID: 9875 RVA: 0x00261D38 File Offset: 0x00260D38
	protected void ᜁ(BinaryWriter A_0)
	{
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			A_0.Write(this.ᜁ.ToByteArray());
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_0.Write(this.ᜂ.ToByteArray());
					num = 2;
					continue;
				case 1:
					if ((base.ᜆ().ᜂ() & 1) != 0)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					return;
				case 2:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06002694 RID: 9876 RVA: 0x00261DD8 File Offset: 0x00260DD8
	private static Guid ᜁ(BinaryReader A_0)
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
		return new Guid(A_0.ReadBytes(16));
	}

	// Token: 0x04002262 RID: 8802
	protected new const int ᜀ = 16;

	// Token: 0x04002263 RID: 8803
	private Guid ᜁ = Guid.Empty;

	// Token: 0x04002264 RID: 8804
	private Guid ᜂ = Guid.Empty;
}
