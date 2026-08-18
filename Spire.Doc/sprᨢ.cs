using System;
using System.Collections;
using System.Reflection;

// Token: 0x02000299 RID: 665
[DefaultMember("Item")]
internal class sprᨢ : ArrayList
{
	// Token: 0x06002357 RID: 9047 RVA: 0x0023EDFC File Offset: 0x0023DDFC
	internal sprᨢ()
	{
	}

	// Token: 0x06002358 RID: 9048 RVA: 0x0023EE10 File Offset: 0x0023DE10
	internal sprᨢ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002359 RID: 9049 RVA: 0x0023EE24 File Offset: 0x0023DE24
	internal sprᨢ(ArrayList A_0) : base(A_0)
	{
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x0023EE38 File Offset: 0x0023DE38
	internal sprᨢ(params spr\u21DA[] A_0) : base(A_0.Length)
	{
		foreach (spr\u21DA value in A_0)
		{
			this.Add(value);
		}
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x0023EE70 File Offset: 0x0023DE70
	public spr\u21DA ᜀ(int A_0)
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
		return (spr\u21DA)base[A_0];
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x0023EEB8 File Offset: 0x0023DEB8
	public void ᜀ(int A_0, spr\u21DA A_1)
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
		base[A_0] = A_1;
	}
}
