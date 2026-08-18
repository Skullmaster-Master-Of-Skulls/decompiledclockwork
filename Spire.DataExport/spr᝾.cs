using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using Spire.DataExport.CollectionEditors;

// Token: 0x02000044 RID: 68
[DefaultMember("Item")]
internal class spr\u177E : IEnumerable
{
	// Token: 0x06000220 RID: 544 RVA: 0x000135A0 File Offset: 0x000125A0
	public IEnumerator ᜃ()
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
		return this.ᜀ.GetEnumerator();
	}

	// Token: 0x06000221 RID: 545 RVA: 0x000135E8 File Offset: 0x000125E8
	public int ᜀ(spr\u2495 A_0)
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
		return this.ᜀ.Add(A_0);
	}

	// Token: 0x06000222 RID: 546 RVA: 0x00013630 File Offset: 0x00012630
	public int ᜀ(Color A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_91:
			if (true)
			{
			}
			num = 4;
			break;
		default:
			if (false)
			{
			}
			goto IL_46;
		}
		int num2;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
			{
				Color color;
				if (color.ToArgb() == A_0.ToArgb())
				{
					num = 2;
					continue;
				}
				num2++;
				num = 1;
				continue;
			}
			case 1:
				goto IL_5E;
			case 2:
				return num2;
			case 3:
				return -1;
			case 4:
			{
				if (num2 >= this.ᜀ.Count)
				{
					num = 3;
					continue;
				}
				Color color = this.ᜀ(num2).ᜄ();
				num = 0;
				continue;
			}
			case 5:
				goto IL_50;
			}
			goto IL_46;
		}
		IL_50:
		IL_5E:
		goto IL_91;
		IL_46:
		num2 = 0;
		num = 5;
		goto IL_28;
	}

	// Token: 0x06000223 RID: 547 RVA: 0x000136FC File Offset: 0x000126FC
	public int ᜂ()
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
		return this.ᜀ.Count - 1;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x00013744 File Offset: 0x00012744
	public spr\u2495 ᜀ(int A_0)
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
		return this.ᜀ[A_0] as spr\u2495;
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00013790 File Offset: 0x00012790
	public void ᜀ(int A_0, spr\u2495 A_1)
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
		this.ᜀ[A_0] = A_1;
	}

	// Token: 0x06000226 RID: 550 RVA: 0x000137D8 File Offset: 0x000127D8
	public int ᜁ()
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
		return this.ᜀ.Count;
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00013820 File Offset: 0x00012820
	public int ᜀ()
	{
		int a_ = 15;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			for (;;)
			{
				num = HyperlinksCollectionEditor.b("倪焬䰮帰弲娴䔶䴸夺儼", a_).Length + 2;
				int num2 = 0;
				int num3 = 1;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						return num;
					case 1:
						goto IL_5F;
					case 2:
						goto IL_5F;
					case 3:
						if (true)
						{
						}
						if (num2 >= this.ᜀ.Count)
						{
							num3 = 0;
							continue;
						}
						num += this.ᜀ(num2).ᜁ();
						num2++;
						num3 = 2;
						continue;
					}
					break;
					IL_5F:
					num3 = 3;
				}
			}
			break;
		}
		return num;
	}

	// Token: 0x040000A4 RID: 164
	private ArrayList ᜀ = new ArrayList();
}
