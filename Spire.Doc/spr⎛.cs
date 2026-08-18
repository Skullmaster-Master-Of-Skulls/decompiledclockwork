using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x02000188 RID: 392
internal class spr\u239B : spr\u171F
{
	// Token: 0x06000DBF RID: 3519 RVA: 0x000E3DEC File Offset: 0x000E2DEC
	internal spr\u239B() : base(EsRecordType.Bse, 2)
	{
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x000E3E08 File Offset: 0x000E2E08
	internal void ᜂ(BinaryReader A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 2:
				num = 3;
				continue;
			case 3:
				if (this.ᜁ == 0)
				{
					num = 5;
					continue;
				}
				goto IL_A1;
			case 4:
				if (this.ᜆ == 0)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5C;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 5:
				goto IL_5C;
			}
			if (this.ᜇ == 4294967295U)
			{
				break;
			}
			if (true)
			{
			}
			num = 0;
		}
		return;
		IL_5C:
		return;
		IL_A1:
		A_0.BaseStream.Position = (long)((ulong)this.ᜇ);
		this.ᜌ = (sprᦫ.ᜀ(A_0, base.ᜄ()) as sprᝋ);
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x000E3EE0 File Offset: 0x000E2EE0
	internal void ᜂ(BinaryWriter A_0)
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
		this.ᜇ = (uint)A_0.BaseStream.Position;
		this.ᜅ = this.ᜌ.ᜃ(A_0);
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x000E3F40 File Offset: 0x000E2F40
	internal bool ᜀ()
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
		return this.\u170D;
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x000E3F84 File Offset: 0x000E2F84
	internal void ᜀ(bool A_0)
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
		this.\u170D = A_0;
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x000E3FC8 File Offset: 0x000E2FC8
	internal sprᝋ ᜁ()
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
		return this.ᜌ;
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x000E400C File Offset: 0x000E300C
	internal void ᜀ(sprᝋ A_0)
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
		this.ᜌ = A_0;
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x000E4050 File Offset: 0x000E3050
	protected override void ᜀ(BinaryReader A_0)
	{
		for (;;)
		{
			int num = (int)(A_0.BaseStream.Position + (long)base.ᜆ().ᜄ());
			this.ᜁ(A_0);
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (true)
					{
					}
					goto IL_4B;
				case 2:
					if (A_0.BaseStream.Position < (long)num)
					{
						this.\u170D = true;
						this.ᜌ = (sprᝋ)sprᦫ.ᜀ(A_0, base.ᜄ());
						num2 = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 3:
					goto IL_4B;
				}
				break;
				IL_4B:
				num2 = 2;
			}
		}
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x000E4120 File Offset: 0x000E3120
	protected override void ᜀ(BinaryWriter A_0)
	{
		for (;;)
		{
			int num = (int)A_0.BaseStream.Position;
			this.ᜁ(A_0);
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					this.ᜅ = this.ᜌ.ᜃ(A_0);
					int num3 = (int)A_0.BaseStream.Position;
					A_0.BaseStream.Position = (long)num;
					this.ᜁ(A_0);
					A_0.BaseStream.Position = (long)num3;
					if (true)
					{
					}
					num2 = 1;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B;
					default:
						goto IL_B8;
					}
					break;
				case 2:
					if (this.\u170D)
					{
						goto IL_3B;
					}
					return;
				}
				break;
				IL_3B:
				num2 = 0;
			}
		}
		IL_B8:
		if (false)
		{
		}
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x000E41F0 File Offset: 0x000E31F0
	private void ᜁ(BinaryReader A_0)
	{
		int a_ = 11;
		this.ᜁ = (int)A_0.ReadByte();
		this.ᜂ = (int)A_0.ReadByte();
		this.ᜃ = A_0.ReadBytes(16);
		this.ᜄ = (int)A_0.ReadInt16();
		this.ᜅ = A_0.ReadInt32();
		this.ᜆ = A_0.ReadInt32();
		this.ᜇ = A_0.ReadUInt32();
		this.ᜈ = (int)A_0.ReadByte();
		this.ᜉ = (int)A_0.ReadByte();
		this.ᜊ = (int)A_0.ReadByte();
		this.ᜋ = (int)A_0.ReadByte();
		if (this.ᜉ > 0)
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
				throw new InvalidOperationException(ClipboardData.b("㝰ᱲt᥶ᵸ孺ᱼ彾쎀쾂첄힆ꦈﲊﮎ戀뎒랖漢爵膠슢쮤쎦覨\udfaa얬욮슰鎲\udcb4쒶馸햺튼쮾뫂ꃄ돆룊료뿎ꇐ볒꟔ꏖ볘뿚", a_));
			}
		}
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x000E42DC File Offset: 0x000E32DC
	private void ᜁ(BinaryWriter A_0)
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
		A_0.Write((byte)this.ᜁ);
		A_0.Write((byte)this.ᜂ);
		A_0.Write(this.ᜃ);
		A_0.Write((short)this.ᜄ);
		A_0.Write(this.ᜅ);
		A_0.Write(this.ᜆ);
		A_0.Write(this.ᜇ);
		A_0.Write((byte)this.ᜈ);
		A_0.Write((byte)this.ᜉ);
		A_0.Write((byte)this.ᜊ);
		A_0.Write((byte)this.ᜋ);
	}

	// Token: 0x04001722 RID: 5922
	internal new const int ᜀ = 36;

	// Token: 0x04001723 RID: 5923
	internal int ᜁ;

	// Token: 0x04001724 RID: 5924
	internal int ᜂ;

	// Token: 0x04001725 RID: 5925
	internal new byte[] ᜃ;

	// Token: 0x04001726 RID: 5926
	internal int ᜄ;

	// Token: 0x04001727 RID: 5927
	internal new int ᜅ;

	// Token: 0x04001728 RID: 5928
	internal int ᜆ;

	// Token: 0x04001729 RID: 5929
	internal uint ᜇ;

	// Token: 0x0400172A RID: 5930
	internal int ᜈ;

	// Token: 0x0400172B RID: 5931
	internal int ᜉ;

	// Token: 0x0400172C RID: 5932
	internal int ᜊ;

	// Token: 0x0400172D RID: 5933
	internal int ᜋ;

	// Token: 0x0400172E RID: 5934
	private sprᝋ ᜌ;

	// Token: 0x0400172F RID: 5935
	private bool \u170D;
}
