using System;
using System.Collections.Generic;
using System.Reflection;
using Spire.Doc.Documents;

// Token: 0x020003D6 RID: 982
[DefaultMember("Item")]
internal class spr\u2100
{
	// Token: 0x0600375E RID: 14174 RVA: 0x0033C2CC File Offset: 0x0033B2CC
	internal bool ᜀ(CompatibilityOptions A_0)
	{
		while (!this.ᜀ().ContainsKey(A_0))
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
				return false;
			}
		}
		if (true)
		{
		}
		return this.ᜀ()[A_0];
	}

	// Token: 0x0600375F RID: 14175 RVA: 0x0033C328 File Offset: 0x0033B328
	internal void ᜀ(CompatibilityOptions A_0, bool A_1)
	{
		while (!this.ᜀ().ContainsKey(A_0))
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
				this.ᜀ().Add(A_0, A_1);
				return;
			}
		}
		if (true)
		{
		}
		this.ᜀ()[A_0] = A_1;
	}

	// Token: 0x06003760 RID: 14176 RVA: 0x0033C390 File Offset: 0x0033B390
	internal Dictionary<CompatibilityOptions, bool> ᜀ()
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_6F;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						this.ᜀ = new Dictionary<CompatibilityOptions, bool>();
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				if (this.ᜀ != null)
				{
					goto IL_71;
				}
				num = 2;
			}
		}
		IL_6F:
		IL_71:
		return this.ᜀ;
	}

	// Token: 0x06003761 RID: 14177 RVA: 0x0033C414 File Offset: 0x0033B414
	internal spr\u2100()
	{
	}

	// Token: 0x040029F0 RID: 10736
	private Dictionary<CompatibilityOptions, bool> ᜀ;
}
