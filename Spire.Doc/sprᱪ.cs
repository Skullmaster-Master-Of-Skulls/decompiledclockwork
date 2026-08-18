using System;
using System.IO;
using Spire.Doc;
using Spire.Doc.Core.Escher;
using Spire.Doc.Fields.Shape;

// Token: 0x0200038D RID: 909
internal class sprᱪ : sprᢘ
{
	// Token: 0x06003298 RID: 12952 RVA: 0x002E8558 File Offset: 0x002E7558
	internal override byte[] ᜁ()
	{
		if (this.ᜅ == null)
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
				if (false)
				{
				}
				return null;
			}
		}
		return this.ᜅ.ᜃ();
	}

	// Token: 0x06003299 RID: 12953 RVA: 0x002E85AC File Offset: 0x002E75AC
	internal override void ᜀ(byte[] A_0)
	{
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜅ = this.ᜁ.Images.ᜂ(A_0);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜅ != null)
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
					{
						sprᠾ sprᠾ = this.ᜅ;
						sprᠾ.ᜂ(sprᠾ.ᜅ() - 1);
						num = 1;
						continue;
					}
					}
					break;
				}
				break;
			}
			}
		}
	}

	// Token: 0x0600329A RID: 12954 RVA: 0x002E864C File Offset: 0x002E764C
	internal override sprᠾ ᜂ()
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

	// Token: 0x0600329B RID: 12955 RVA: 0x002E8690 File Offset: 0x002E7690
	internal override void ᜀ(sprᠾ A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x0600329C RID: 12956 RVA: 0x002E86D4 File Offset: 0x002E76D4
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
		return this.ᜆ;
	}

	// Token: 0x0600329D RID: 12957 RVA: 0x002E8718 File Offset: 0x002E7718
	internal sprᱪ(Document A_0) : base(A_0)
	{
	}

	// Token: 0x0600329E RID: 12958 RVA: 0x002E872C File Offset: 0x002E772C
	internal sprᱪ(sprᠾ A_0, bool A_1, Document A_2) : base(A_2)
	{
		if (A_1)
		{
			base.\u1717().ᜀ(MSOFBT.msofbtBlipPNG);
			base.\u1717().ᜁ(1760);
		}
		else
		{
			base.\u1717().ᜀ(MSOFBT.msofbtBlipJPEG);
			base.\u1717().ᜁ(1130);
		}
		base.ᜀ(Guid.NewGuid());
		base.ᜁ(base.ᜈ());
		this.ᜅ = A_0;
	}

	// Token: 0x0600329F RID: 12959 RVA: 0x002E87A8 File Offset: 0x002E77A8
	protected override void ᜁ(Stream A_0)
	{
		byte[] array;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_A7;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				base.ᜂ(A_0);
				this.ᜆ = (PresetTexture)A_0.ReadByte();
				int num = base.\u1717().ᜇ() - 16 - 1;
				array = new byte[num];
				A_0.Read(array, 0, num);
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (base.ᜆ())
						{
							num2 = 2;
							continue;
						}
						goto IL_A9;
					case 1:
						goto IL_A7;
					case 2:
						array = this.ᜁ(array);
						num2 = 1;
						continue;
					}
					break;
				}
				break;
			}
			}
		}
		IL_A7:
		IL_A9:
		this.ᜅ = this.ᜁ.Images.ᜂ(array);
	}

	// Token: 0x060032A0 RID: 12960 RVA: 0x002E8878 File Offset: 0x002E7878
	protected override void ᜀ(Stream A_0)
	{
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_9B;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				byte[] array = base.ᜈ().ToByteArray();
				A_0.Write(array, 0, array.Length);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9B;
					case 1:
						array = base.ᜉ().ToByteArray();
						A_0.Write(array, 0, array.Length);
						num = 0;
						continue;
					case 2:
						if (base.ᜋ())
						{
							num = 1;
							continue;
						}
						goto IL_9D;
					}
					break;
				}
				break;
			}
			}
		}
		IL_9B:
		IL_9D:
		A_0.WriteByte(byte.MaxValue);
		A_0.Write(this.ᜁ(), 0, this.ᜁ().Length);
	}

	// Token: 0x060032A1 RID: 12961 RVA: 0x002E8944 File Offset: 0x002E7944
	internal virtual spr\u2192 ᜄ()
	{
		sprᱪ sprᱪ;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_7C;
			default:
			{
				if (false)
				{
				}
				sprᱪ = new sprᱪ(this.ᜁ);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7C;
					case 1:
						if (this.ᜅ != null)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_7E;
					case 2:
						sprᱪ.ᜅ = this.ᜅ;
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
			}
		}
		IL_7C:
		IL_7E:
		sprᱪ.ᜀ(base.\u1717().ᜆ());
		sprᱪ.ᜀ(new Guid(base.ᜈ().ToByteArray()));
		sprᱪ.ᜁ(new Guid(base.ᜉ().ToByteArray()));
		sprᱪ.ᜁ = this.ᜁ;
		return sprᱪ;
	}

	// Token: 0x060032A2 RID: 12962 RVA: 0x002E8A20 File Offset: 0x002E7A20
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

	// Token: 0x060032A3 RID: 12963 RVA: 0x002E8A64 File Offset: 0x002E7A64
	private new byte[] ᜁ(byte[] A_0)
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
		uint num = BitConverter.ToUInt32(A_0, 0);
		uint num2 = BitConverter.ToUInt32(A_0, 32);
		int value = A_0.Length + 14;
		MemoryStream memoryStream = new MemoryStream();
		byte[] bytes = BitConverter.GetBytes(value);
		memoryStream.Write(sprᱪ.ᜃ, 0, sprᱪ.ᜃ.Length);
		memoryStream.Write(bytes, 0, bytes.Length);
		memoryStream.Write(sprᱪ.ᜄ, 0, sprᱪ.ᜄ.Length);
		uint value2 = num + 14U + num2 * 4U;
		bytes = BitConverter.GetBytes(value2);
		memoryStream.Write(bytes, 0, bytes.Length);
		memoryStream.Write(A_0, 0, A_0.Length);
		A_0 = memoryStream.ToArray();
		memoryStream.Close();
		return A_0;
	}

	// Token: 0x060032A4 RID: 12964 RVA: 0x002E8B34 File Offset: 0x002E7B34
	// Note: this type is marked as 'beforefieldinit'.
	static sprᱪ()
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
		sprᱪ.ᜃ = new byte[]
		{
			66,
			77
		};
		byte[] array = new byte[4];
		sprᱪ.ᜄ = array;
	}

	// Token: 0x040027F9 RID: 10233
	private new const int ᜀ = 32;

	// Token: 0x040027FA RID: 10234
	private new const int ᜁ = 14;

	// Token: 0x040027FB RID: 10235
	private new const uint ᜂ = 4U;

	// Token: 0x040027FC RID: 10236
	private new static readonly byte[] ᜃ;

	// Token: 0x040027FD RID: 10237
	private new static readonly byte[] ᜄ;

	// Token: 0x040027FE RID: 10238
	private new sprᠾ ᜅ;

	// Token: 0x040027FF RID: 10239
	private new PresetTexture ᜆ;
}
