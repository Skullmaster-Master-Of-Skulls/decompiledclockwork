using System;
using System.IO;
using Spire.Doc;
using Spire.Doc.Core.Escher;
using Spire.Doc.Fields.Shape;

// Token: 0x020002E6 RID: 742
internal class sprᲱ : sprᢘ
{
	// Token: 0x060028BE RID: 10430 RVA: 0x00287CC4 File Offset: 0x00286CC4
	internal override PresetTexture ᜀ()
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
		return PresetTexture.Custom;
	}

	// Token: 0x060028BF RID: 10431 RVA: 0x00287D04 File Offset: 0x00286D04
	internal CompressionMethod ᜅ()
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
		return CompressionMethod.msocompressionZip;
	}

	// Token: 0x060028C0 RID: 10432 RVA: 0x00287D40 File Offset: 0x00286D40
	internal override byte[] ᜁ()
	{
		while (this.ᜈ == null)
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
				if (true)
				{
				}
				return null;
			}
		}
		return this.ᜈ.ᜃ();
	}

	// Token: 0x060028C1 RID: 10433 RVA: 0x00287D94 File Offset: 0x00286D94
	internal override void ᜀ(byte[] A_0)
	{
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_50:
				if (this.ᜈ == null)
				{
					return;
				}
				num = 1;
				break;
			default:
				if (false)
				{
				}
				this.ᜈ = this.ᜁ.Images.ᜀ(A_0, false);
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					sprᠾ sprᠾ = this.ᜈ;
					sprᠾ.ᜂ(sprᠾ.ᜅ() - 1);
					if (true)
					{
					}
					num = 0;
					continue;
				}
				case 2:
					goto IL_50;
				}
				break;
			}
		}
	}

	// Token: 0x060028C2 RID: 10434 RVA: 0x00287E38 File Offset: 0x00286E38
	internal override sprᠾ ᜂ()
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

	// Token: 0x060028C3 RID: 10435 RVA: 0x00287E7C File Offset: 0x00286E7C
	internal override void ᜀ(sprᠾ A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x060028C4 RID: 10436 RVA: 0x00287EC0 File Offset: 0x00286EC0
	internal sprᲱ(Document A_0) : base(A_0)
	{
	}

	// Token: 0x060028C5 RID: 10437 RVA: 0x00287ED4 File Offset: 0x00286ED4
	internal sprᲱ(sprᠾ A_0, Document A_1) : base(A_1)
	{
		if (A_0 != null)
		{
			base.\u1717().ᜀ(MSOFBT.msofbtBlipEMF);
			base.\u1717().ᜁ(980);
			base.ᜀ(Guid.NewGuid());
			base.ᜁ(base.ᜈ());
			this.ᜈ = A_0;
			this.ᜀ = this.ᜈ.ᜆ();
			this.ᜇ = 254;
			this.ᜃ = A_0.ᜁ().Width;
			this.ᜄ = A_0.ᜁ().Height;
			this.ᜅ = A_0.ᜁ().Width * 12700 * 72 / 96;
			this.ᜆ = A_0.ᜁ().Height * 12700 * 72 / 96;
		}
	}

	// Token: 0x060028C6 RID: 10438 RVA: 0x00287FB4 File Offset: 0x00286FB4
	internal virtual spr\u2192 ᜄ()
	{
		sprᲱ sprᲱ;
		for (;;)
		{
			sprᲱ = (sprᲱ)base.MemberwiseClone();
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					for (;;)
					{
						sprᲱ.ᜈ = this.ᜈ;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_6E;
						}
					}
					IL_6E:
					if (false)
					{
					}
					num = 1;
					continue;
				case 1:
					goto IL_7C;
				case 2:
					if (this.ᜈ != null)
					{
						num = 0;
						continue;
					}
					goto IL_7E;
				}
				break;
			}
		}
		IL_7C:
		IL_7E:
		sprᲱ.ᜀ(base.\u1717().ᜆ());
		sprᲱ.ᜀ(new Guid(base.ᜈ().ToByteArray()));
		sprᲱ.ᜁ(new Guid(base.ᜉ().ToByteArray()));
		sprᲱ.ᜁ = this.ᜁ;
		return sprᲱ;
	}

	// Token: 0x060028C7 RID: 10439 RVA: 0x00288090 File Offset: 0x00287090
	internal override void \u170D()
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
		base.\u170D();
	}

	// Token: 0x060028C8 RID: 10440 RVA: 0x002880D4 File Offset: 0x002870D4
	protected override void ᜁ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = (int)A_0.Position;
				base.ᜂ(A_0);
				this.ᜀ = spr\u23F8.ᜁ(A_0);
				this.ᜁ = spr\u23F8.ᜁ(A_0);
				this.ᜂ = spr\u23F8.ᜁ(A_0);
				this.ᜃ = spr\u23F8.ᜁ(A_0);
				this.ᜄ = spr\u23F8.ᜁ(A_0);
				this.ᜅ = spr\u23F8.ᜁ(A_0);
				this.ᜆ = spr\u23F8.ᜁ(A_0);
				int num2 = spr\u23F8.ᜁ(A_0);
				CompressionMethod compressionMethod = (CompressionMethod)A_0.ReadByte();
				this.ᜇ = (byte)A_0.ReadByte();
				byte[] array = new byte[num2];
				A_0.Read(array, 0, num2);
				if (true)
				{
				}
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_11B;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜈ = this.ᜁ.Images.ᜀ(array, true);
							break;
						}
						num3 = 2;
						continue;
					case 2:
						goto IL_14B;
					case 3:
						if (compressionMethod == CompressionMethod.msocompressionZip)
						{
							num3 = 1;
							continue;
						}
						this.ᜈ = this.ᜁ.Images.ᜀ(array, false);
						num3 = 0;
						continue;
					}
					break;
				}
			}
			IL_11B:
			IL_14B:
			A_0.Position = (long)(num + base.\u1717().ᜇ());
			return;
		}
		}
	}

	// Token: 0x060028C9 RID: 10441 RVA: 0x00288244 File Offset: 0x00287244
	protected override void ᜀ(Stream A_0)
	{
		for (;;)
		{
			byte[] array = base.ᜈ().ToByteArray();
			A_0.Write(array, 0, array.Length);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					for (;;)
					{
						array = base.ᜉ().ToByteArray();
						A_0.Write(array, 0, array.Length);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_82;
						}
					}
					IL_82:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 1;
					continue;
				case 1:
					goto IL_9B;
				case 2:
					if (base.ᜋ())
					{
						num = 0;
						continue;
					}
					goto IL_9D;
				}
				break;
			}
		}
		IL_9B:
		IL_9D:
		spr\u23F8.ᜁ(A_0, this.ᜀ);
		spr\u23F8.ᜁ(A_0, this.ᜁ);
		spr\u23F8.ᜁ(A_0, this.ᜂ);
		spr\u23F8.ᜁ(A_0, this.ᜃ);
		spr\u23F8.ᜁ(A_0, this.ᜄ);
		spr\u23F8.ᜁ(A_0, this.ᜅ);
		spr\u23F8.ᜁ(A_0, this.ᜆ);
		spr\u23F8.ᜁ(A_0, this.ᜂ().ᜂ.Length);
		A_0.WriteByte(0);
		A_0.WriteByte(this.ᜇ);
		A_0.Write(this.ᜂ().ᜂ, 0, this.ᜂ().ᜂ.Length);
	}

	// Token: 0x04002372 RID: 9074
	private new int ᜀ;

	// Token: 0x04002373 RID: 9075
	private new int ᜁ;

	// Token: 0x04002374 RID: 9076
	private new int ᜂ;

	// Token: 0x04002375 RID: 9077
	private new int ᜃ;

	// Token: 0x04002376 RID: 9078
	private new int ᜄ;

	// Token: 0x04002377 RID: 9079
	private new int ᜅ;

	// Token: 0x04002378 RID: 9080
	private new int ᜆ;

	// Token: 0x04002379 RID: 9081
	private byte ᜇ;

	// Token: 0x0400237A RID: 9082
	private sprᠾ ᜈ;
}
