using System;
using System.Runtime.InteropServices;

// Token: 0x020001D9 RID: 473
[CLSCompliant(false)]
[StructLayout(LayoutKind.Explicit)]
internal class spr\u22DD : spr\u2562
{
	// Token: 0x0600149F RID: 5279 RVA: 0x001516E4 File Offset: 0x001506E4
	internal override int ᜀ()
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
		return 6;
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x00151720 File Offset: 0x00150720
	internal override void ᜁ(byte[] A_0, int A_1)
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
		this.ᜂ = spr\u2562.ᜄ(A_0, ref A_1);
		this.ᜃ = spr\u2562.ᜄ(A_0, ref A_1);
		this.ᜄ = A_0[A_1];
		A_1++;
		this.ᜅ = A_0[A_1];
		A_1++;
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x00151794 File Offset: 0x00150794
	internal override int ᜀ(byte[] A_0, int A_1)
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
		spr\u2562.ᜀ(A_0, ref A_1, this.ᜂ);
		spr\u2562.ᜀ(A_0, ref A_1, this.ᜃ);
		A_0[A_1] = this.ᜄ;
		A_1++;
		A_0[A_1] = this.ᜅ;
		A_1++;
		return 6;
	}

	// Token: 0x0400192A RID: 6442
	private new const int ᜀ = 6;

	// Token: 0x0400192B RID: 6443
	[FieldOffset(0)]
	internal new short ᜁ;

	// Token: 0x0400192C RID: 6444
	[FieldOffset(0)]
	internal new short ᜂ;

	// Token: 0x0400192D RID: 6445
	[FieldOffset(2)]
	internal new short ᜃ;

	// Token: 0x0400192E RID: 6446
	[FieldOffset(4)]
	internal new byte ᜄ;

	// Token: 0x0400192F RID: 6447
	[FieldOffset(5)]
	internal byte ᜅ;
}
