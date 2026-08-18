using System;
using System.IO;
using System.Runtime.InteropServices;

// Token: 0x020001AA RID: 426
[CLSCompliant(false)]
[StructLayout(LayoutKind.Sequential)]
internal class spr\u17CB : spr\u2562
{
	// Token: 0x060010B2 RID: 4274 RVA: 0x000FC474 File Offset: 0x000FB474
	internal spr\u17CB()
	{
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x000FC494 File Offset: 0x000FB494
	internal byte ᜂ()
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

	// Token: 0x060010B4 RID: 4276 RVA: 0x000FC4D8 File Offset: 0x000FB4D8
	internal void ᜀ(byte A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x000FC51C File Offset: 0x000FB51C
	internal spr\u2445 ᜁ()
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

	// Token: 0x060010B6 RID: 4278 RVA: 0x000FC560 File Offset: 0x000FB560
	internal override int ᜀ()
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
		return 13;
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x000FC5A0 File Offset: 0x000FB5A0
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
		this.ᜁ = A_0[A_1];
		A_1++;
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x000FC5EC File Offset: 0x000FB5EC
	internal override int ᜀ(byte[] A_0, int A_1)
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
		A_0[A_1] = this.ᜁ;
		A_1++;
		return this.ᜂ.ᜀ(A_0, A_1) + 1;
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x000FC644 File Offset: 0x000FB644
	internal void ᜀ(BinaryWriter A_0)
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
		A_0.Write(this.ᜁ);
		spr\u1D61 spr_u1D = this.ᜂ.ᜄ();
		A_0.Write(spr_u1D.ᜁ);
		A_0.Write(spr_u1D.ᜂ);
		A_0.Write(spr_u1D.ᜃ);
	}

	// Token: 0x040017CF RID: 6095
	internal new const int ᜀ = 13;

	// Token: 0x040017D0 RID: 6096
	private new byte ᜁ;

	// Token: 0x040017D1 RID: 6097
	private new spr\u2445 ᜂ = new spr\u2445();
}
