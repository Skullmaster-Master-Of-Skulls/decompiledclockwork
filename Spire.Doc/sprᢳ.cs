using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x020001D2 RID: 466
[CLSCompliant(false)]
internal class sprᢳ
{
	// Token: 0x0600141A RID: 5146 RVA: 0x0014CF7C File Offset: 0x0014BF7C
	internal List<spr\u226A> ᜀ()
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
		return this.ᜂ;
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x0014CFC0 File Offset: 0x0014BFC0
	internal sprᢳ()
	{
		this.ᜁ = 8;
		this.ᜂ = new List<spr\u226A>();
		this.ᜃ = new spr\u1AED();
		base..ctor();
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x0014CFF0 File Offset: 0x0014BFF0
	internal sprᢳ(Stream A_0)
	{
		int a_ = 8;
		this.ᜁ = 8;
		this.ᜂ = new List<spr\u226A>();
		this.ᜃ = new spr\u1AED();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ᵭѯqᅳ᝵ᕷ", a_));
		}
		byte[] a_2 = new byte[4];
		this.ᜁ = this.ᜃ.ᜀ(A_0, a_2);
		int num = this.ᜃ.ᜀ(A_0, a_2);
		if (this.ᜂ.Capacity < num)
		{
			this.ᜂ.Capacity = num;
		}
		if (this.ᜁ != 8)
		{
			A_0.Position += (long)(this.ᜁ - 8);
		}
		for (int i = 0; i < num; i++)
		{
			spr\u226A item = new spr\u226A(A_0);
			this.ᜂ.Add(item);
		}
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x0014D0D4 File Offset: 0x0014C0D4
	internal void ᜀ(Stream A_0)
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			int num2;
			int count;
			switch (num)
			{
			case 1:
				goto IL_DF;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DF;
				default:
					if (false)
					{
					}
					goto IL_DF;
				}
				break;
			case 3:
			{
				if (num2 >= count)
				{
					num = 5;
					continue;
				}
				if (true)
				{
				}
				spr\u226A spr_u226A = this.ᜂ[num2];
				spr_u226A.ᜀ(A_0);
				num2++;
				num = 1;
				continue;
			}
			case 4:
				goto IL_3C;
			case 5:
				return;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			this.ᜃ.ᜀ(A_0, this.ᜁ);
			count = this.ᜂ.Count;
			this.ᜃ.ᜀ(A_0, count);
			num2 = 0;
			num = 2;
			continue;
			IL_DF:
			num = 3;
		}
		IL_3C:
		throw new ArgumentNullException(ClipboardData.b("୷๹๻᭽", a_));
	}

	// Token: 0x040018FD RID: 6397
	private const int ᜀ = 8;

	// Token: 0x040018FE RID: 6398
	private int ᜁ;

	// Token: 0x040018FF RID: 6399
	private List<spr\u226A> ᜂ;

	// Token: 0x04001900 RID: 6400
	private spr\u1AED ᜃ;
}
