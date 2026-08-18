using System;
using System.Collections.Generic;
using System.IO;
using Spire.Doc;

// Token: 0x0200023A RID: 570
internal class sprᵲ : List<object>
{
	// Token: 0x06001B39 RID: 6969 RVA: 0x001C65B4 File Offset: 0x001C55B4
	internal sprᵲ(Document A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x06001B3A RID: 6970 RVA: 0x001C65D0 File Offset: 0x001C55D0
	internal int ᜀ(Stream A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_B6;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		long position = A_0.Position;
		using (List<object>.Enumerator enumerator = base.GetEnumerator())
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_A6;
				case 2:
				{
					if (!enumerator.MoveNext())
					{
						num = 0;
						continue;
					}
					spr\u2192 spr_u = (spr\u2192)enumerator.Current;
					spr_u.ᜆ(A_0);
					num = 3;
					continue;
				}
				}
				IL_83:
				num = 2;
				continue;
				goto IL_83;
			}
			IL_A6:;
		}
		IL_B6:
		return (int)(A_0.Position - position);
	}

	// Token: 0x06001B3B RID: 6971 RVA: 0x001C66AC File Offset: 0x001C56AC
	internal void ᜀ(Stream A_0, int A_1)
	{
		for (;;)
		{
			long num = A_0.Position + (long)A_1;
			num = Math.Min(num, A_0.Length);
			int num2 = 5;
			for (;;)
			{
				spr\u2192 spr_u;
				switch (num2)
				{
				case 0:
					if (spr_u != null)
					{
						num2 = 1;
						continue;
					}
					goto IL_49;
				case 1:
					goto IL_B3;
				case 2:
					if (A_0.Position < num)
					{
						if (true)
						{
						}
						num2 = 7;
						continue;
					}
					return;
				case 3:
					return;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B3;
					default:
						if (false)
						{
						}
						if (A_0.Position >= A_0.Length)
						{
							num2 = 3;
							continue;
						}
						spr_u = spr\u1D2F.ᜀ(A_0, this.ᜀ);
						num2 = 0;
						continue;
					}
					break;
				case 5:
					goto IL_49;
				case 6:
					goto IL_49;
				case 7:
					num2 = 4;
					continue;
				}
				break;
				IL_49:
				num2 = 2;
				continue;
				IL_B3:
				base.Add(spr_u);
				num2 = 6;
			}
		}
	}

	// Token: 0x04001EAF RID: 7855
	private Document ᜀ;
}
