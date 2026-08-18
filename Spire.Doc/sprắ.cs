using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x02000176 RID: 374
internal class sprắ : spr\u23F8
{
	// Token: 0x06000D25 RID: 3365 RVA: 0x000DB504 File Offset: 0x000DA504
	public void ᜁ(Stream A_0)
	{
		int a_ = 16;
		this.ᜂ = A_0.ReadByte();
		this.ᜃ = A_0.ReadByte();
		this.ᜄ = base.ᜂ(A_0, 16);
		this.ᜅ = (int)spr\u23F8.ᜂ(A_0);
		this.ᜆ = spr\u23F8.ᜁ(A_0);
		this.ᜇ = spr\u23F8.ᜁ(A_0);
		this.ᜈ = spr\u23F8.ᜁ(A_0);
		this.ᜉ = A_0.ReadByte();
		this.ᜊ = A_0.ReadByte();
		this.ᜋ = A_0.ReadByte();
		this.ᜌ = A_0.ReadByte();
		if (this.ᜊ > 0)
		{
			for (;;)
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
					goto IL_B9;
				}
			}
			IL_B9:
			if (false)
			{
			}
			throw new NotImplementedException(ClipboardData.b("㝵塷㡹ほ㝽큿ꊁﲇ겋낏ﲑﮕﶗ몙ﾝ펟芡슣즥\udda7쒩좫肭", a_));
		}
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x000DB5F0 File Offset: 0x000DA5F0
	public void ᜀ(Stream A_0)
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
		A_0.WriteByte((byte)this.ᜂ);
		A_0.WriteByte((byte)this.ᜃ);
		A_0.Write(this.ᜄ, 0, 16);
		spr\u23F8.ᜀ(A_0, (short)this.ᜅ);
		spr\u23F8.ᜁ(A_0, this.ᜆ);
		spr\u23F8.ᜁ(A_0, this.ᜇ);
		spr\u23F8.ᜁ(A_0, this.ᜈ);
		A_0.WriteByte((byte)this.ᜉ);
		A_0.WriteByte((byte)this.ᜊ);
		A_0.WriteByte((byte)this.ᜋ);
		A_0.WriteByte((byte)this.ᜌ);
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x000DB6BC File Offset: 0x000DA6BC
	public sprắ ᜀ()
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
		sprắ sprắ = (sprắ)base.MemberwiseClone();
		sprắ.ᜄ = new byte[this.ᜄ.Length];
		this.ᜄ.CopyTo(sprắ.ᜄ, 0);
		return sprắ;
	}

	// Token: 0x04001460 RID: 5216
	public new const int ᜀ = 16;

	// Token: 0x04001461 RID: 5217
	public new const int ᜁ = 36;

	// Token: 0x04001462 RID: 5218
	internal new int ᜂ;

	// Token: 0x04001463 RID: 5219
	internal new int ᜃ;

	// Token: 0x04001464 RID: 5220
	internal new byte[] ᜄ;

	// Token: 0x04001465 RID: 5221
	internal new int ᜅ;

	// Token: 0x04001466 RID: 5222
	internal int ᜆ;

	// Token: 0x04001467 RID: 5223
	internal int ᜇ;

	// Token: 0x04001468 RID: 5224
	internal int ᜈ;

	// Token: 0x04001469 RID: 5225
	internal int ᜉ;

	// Token: 0x0400146A RID: 5226
	internal int ᜊ;

	// Token: 0x0400146B RID: 5227
	internal int ᜋ;

	// Token: 0x0400146C RID: 5228
	internal int ᜌ;
}
