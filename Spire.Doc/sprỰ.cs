using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x02000189 RID: 393
internal class sprỰ : spr\u17BB
{
	// Token: 0x06000DCA RID: 3530 RVA: 0x000E43A4 File Offset: 0x000E33A4
	internal sprỰ(int A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x000E43BC File Offset: 0x000E33BC
	internal sprỰ(int A_0, byte[] A_1) : base(A_0, A_1.Length + 20)
	{
		this.ᜀ = A_1;
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x000E43E0 File Offset: 0x000E33E0
	internal override void ᜀ(BinaryReader A_0)
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
		new Guid(A_0.ReadBytes(16));
		int count = A_0.ReadInt32();
		this.ᜀ = A_0.ReadBytes(count);
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x000E4440 File Offset: 0x000E3440
	internal override void ᜀ(BinaryWriter A_0)
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
		A_0.Write(sprỰ.ᜁ.ToByteArray());
		A_0.Write(this.ᜀ.Length);
		A_0.Write(this.ᜀ);
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x000E44A8 File Offset: 0x000E34A8
	internal byte[] ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x000E44EC File Offset: 0x000E34EC
	// Note: this type is marked as 'beforefieldinit'.
	static sprỰ()
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		sprỰ.ᜁ = new Guid(ClipboardData.b("๴乶䩸䱺Ṽ乾낂놄ꪆ뢈뺊벌벐ꞒꎔꚖꦘ뚚꒜ﲞ삠関袤욦醨좪캬隮펰ힲힴ芶\uddb8莺躼슾", a_));
	}

	// Token: 0x04001730 RID: 5936
	private new byte[] ᜀ;

	// Token: 0x04001731 RID: 5937
	private static Guid ᜁ;
}
