using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x020004C0 RID: 1216
internal class spr\u234F
{
	// Token: 0x06004ADB RID: 19163 RVA: 0x002D6F78 File Offset: 0x002D5F78
	public List<string> ᜀ()
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

	// Token: 0x06004ADC RID: 19164 RVA: 0x002D6FBC File Offset: 0x002D5FBC
	public spr\u234F()
	{
	}

	// Token: 0x06004ADD RID: 19165 RVA: 0x002D6FE4 File Offset: 0x002D5FE4
	public spr\u234F(Stream A_0)
	{
		byte[] a_ = new byte[4];
		this.ᜁ = sprṯ.ᜀ(A_0, a_);
		int num = sprṯ.ᜀ(A_0, a_);
		if (this.ᜁ != 8)
		{
			A_0.Position += (long)(this.ᜁ - 8);
		}
		for (int i = 0; i < num; i++)
		{
			string item = sprṯ.ᜁ(A_0);
			this.ᜂ.Add(item);
		}
	}

	// Token: 0x06004ADE RID: 19166 RVA: 0x002D706C File Offset: 0x002D606C
	public void ᜀ(Stream A_0)
	{
		for (;;)
		{
			sprṯ.ᜀ(A_0, this.ᜁ);
			int count = this.ᜂ.Count;
			sprṯ.ᜀ(A_0, count);
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					goto IL_4B;
				case 1:
					return;
				case 2:
				{
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					string a_ = this.ᜂ[num];
					sprṯ.ᜁ(A_0, a_);
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					}
					if (false)
					{
					}
					num2 = 3;
					continue;
				}
				case 3:
					goto IL_4B;
				}
				break;
				IL_4B:
				num2 = 2;
			}
		}
	}

	// Token: 0x040021FE RID: 8702
	private const int ᜀ = 8;

	// Token: 0x040021FF RID: 8703
	private int ᜁ = 8;

	// Token: 0x04002200 RID: 8704
	private List<string> ᜂ = new List<string>();
}
