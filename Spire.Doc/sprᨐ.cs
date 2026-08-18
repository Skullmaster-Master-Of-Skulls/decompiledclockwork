using System;
using System.IO;

// Token: 0x02000382 RID: 898
internal class sprᨐ : spr\u17BB
{
	// Token: 0x06003246 RID: 12870 RVA: 0x002E5BFC File Offset: 0x002E4BFC
	internal sprᨐ(int A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06003247 RID: 12871 RVA: 0x002E5C14 File Offset: 0x002E4C14
	internal sprᨐ(int A_0, byte[] A_1) : base(A_0, A_1.Length)
	{
		this.ᜀ = A_1;
	}

	// Token: 0x06003248 RID: 12872 RVA: 0x002E5C34 File Offset: 0x002E4C34
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
		this.ᜀ = A_0.ReadBytes(base.ᜊ());
	}

	// Token: 0x06003249 RID: 12873 RVA: 0x002E5C84 File Offset: 0x002E4C84
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
		A_0.Write(this.ᜀ);
	}

	// Token: 0x0600324A RID: 12874 RVA: 0x002E5CCC File Offset: 0x002E4CCC
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

	// Token: 0x0400274C RID: 10060
	private new byte[] ᜀ;
}
