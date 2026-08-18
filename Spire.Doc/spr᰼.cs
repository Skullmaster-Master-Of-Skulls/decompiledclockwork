using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x020001D3 RID: 467
[CLSCompliant(false)]
internal class spr\u1C3C
{
	// Token: 0x0600141E RID: 5150 RVA: 0x0014D1E0 File Offset: 0x0014C1E0
	internal List<string> ᜀ()
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

	// Token: 0x0600141F RID: 5151 RVA: 0x0014D224 File Offset: 0x0014C224
	internal spr\u1C3C()
	{
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x0014D254 File Offset: 0x0014C254
	internal spr\u1C3C(Stream A_0)
	{
		byte[] a_ = new byte[4];
		this.ᜁ = this.ᜃ.ᜀ(A_0, a_);
		int num = this.ᜃ.ᜀ(A_0, a_);
		if (this.ᜁ != 8)
		{
			A_0.Position += (long)(this.ᜁ - 8);
		}
		for (int i = 0; i < num; i++)
		{
			string item = this.ᜃ.ᜁ(A_0);
			this.ᜂ.Add(item);
		}
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x0014D2F8 File Offset: 0x0014C2F8
	internal void ᜀ(Stream A_0)
	{
		for (;;)
		{
			this.ᜃ.ᜀ(A_0, this.ᜁ);
			int count = this.ᜂ.Count;
			this.ᜃ.ᜀ(A_0, count);
			int i = 0;
			int num = 3;
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					goto IL_57;
				case 1:
					return;
				case 2:
				{
					while (i >= count)
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
							num = 1;
							goto IL_02;
						}
					}
					string a_ = this.ᜂ[i];
					this.ᜃ.ᜀ(A_0, a_);
					i++;
					num = 0;
					continue;
				}
				case 3:
					if (true)
					{
					}
					goto IL_57;
				}
				break;
				IL_57:
				num = 2;
			}
		}
	}

	// Token: 0x04001901 RID: 6401
	private const int ᜀ = 8;

	// Token: 0x04001902 RID: 6402
	private int ᜁ = 8;

	// Token: 0x04001903 RID: 6403
	private List<string> ᜂ = new List<string>();

	// Token: 0x04001904 RID: 6404
	private spr\u1AED ᜃ = new spr\u1AED();
}
