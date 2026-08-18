using System;

// Token: 0x020004B6 RID: 1206
internal class spr\u225D : spr\u24A5
{
	// Token: 0x06004AA2 RID: 19106 RVA: 0x002D422C File Offset: 0x002D322C
	public spr\u225D(string A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x06004AA3 RID: 19107 RVA: 0x002D4248 File Offset: 0x002D3248
	public string ᜀ(string A_0)
	{
		string result;
		for (;;)
		{
			IL_1C:
			result = A_0;
			for (;;)
			{
				IL_1E:
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1E;
						default:
							if (false)
							{
							}
							result = A_0.Remove(0, this.ᜀ.Length);
							num = 0;
							continue;
						}
						break;
					case 2:
						if (true)
						{
						}
						if (this.ᜀ != null)
						{
							num = 4;
							continue;
						}
						return result;
					case 3:
						if (A_0.StartsWith(this.ᜀ))
						{
							num = 1;
							continue;
						}
						return result;
					case 4:
						num = 3;
						continue;
					}
					goto IL_1C;
				}
			}
		}
		return result;
	}

	// Token: 0x040021D3 RID: 8659
	private string ᜀ;
}
