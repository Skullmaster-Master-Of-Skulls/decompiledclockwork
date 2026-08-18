using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000451 RID: 1105
internal class spr\u2028 : sprᡍ
{
	// Token: 0x060042AB RID: 17067 RVA: 0x00255844 File Offset: 0x00254844
	public spr\u2028()
	{
		this.ᜀ = new List<spr\u23E7.ᜀ>();
	}

	// Token: 0x060042AC RID: 17068 RVA: 0x00255864 File Offset: 0x00254864
	[CLSCompliant(false)]
	public spr\u2028(List<spr\u23E7.ᜀ> A_0)
	{
		int a_ = 8;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("刽⤿ㅁぃ", a_));
		}
		this.ᜀ = A_0;
	}

	// Token: 0x060042AD RID: 17069 RVA: 0x002558A0 File Offset: 0x002548A0
	public List<spr\u23E7.ᜀ> ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x060042AE RID: 17070 RVA: 0x002558E4 File Offset: 0x002548E4
	[CLSCompliant(false)]
	public void ᜀ(spr\u23E7.ᜀ A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				int count = this.ᜀ.Count;
				int num2 = count;
				int num3 = 5;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (this.ᜀ[num].ᜈ() < A_0.ᜈ())
						{
							num3 = 4;
							continue;
						}
						goto IL_9A;
					case 1:
					{
						spr\u23E7.ᜀ ᜀ = this.ᜀ[num];
						num3 = 8;
						continue;
					}
					case 2:
						goto IL_9A;
					case 3:
						if (num < count)
						{
							num3 = 1;
							continue;
						}
						goto IL_169;
					case 4:
						num++;
						num3 = 7;
						continue;
					case 5:
						goto IL_148;
					case 6:
						if (num >= num2)
						{
							goto IL_158;
						}
						num3 = 0;
						continue;
					case 7:
						goto IL_148;
					case 8:
					{
						spr\u23E7.ᜀ ᜀ;
						if (ᜀ.ᜈ() == A_0.ᜈ())
						{
							if (true)
							{
							}
							num3 = 9;
							continue;
						}
						goto IL_F6;
					}
					case 9:
						goto IL_95;
					}
					break;
					IL_9A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_158:
						num3 = 2;
						continue;
					default:
						if (false)
						{
						}
						num3 = 3;
						continue;
					}
					IL_148:
					num3 = 6;
				}
			}
			IL_95:
			this.ᜀ[num] = A_0;
			return;
			IL_F6:
			this.ᜀ.Insert(num, A_0);
			return;
			IL_169:
			this.ᜀ.Add(A_0);
			return;
		}
		}
	}

	// Token: 0x060042AF RID: 17071 RVA: 0x00255A68 File Offset: 0x00254A68
	public void ᜀ(int A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int count = this.ᜀ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					spr\u23E7.ᜀ ᜀ;
					if (ᜀ.ᜈ() == (MsoOptions)A_0)
					{
						num2 = 3;
						continue;
					}
					num++;
					num2 = 2;
					continue;
				}
				case 1:
					goto IL_88;
				case 2:
					goto IL_88;
				case 3:
					goto IL_86;
				case 4:
					return;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						if (num >= count)
						{
							num2 = 4;
							continue;
						}
						spr\u23E7.ᜀ ᜀ = this.ᜀ[num];
						num2 = 0;
						continue;
					}
					}
					break;
				}
				break;
				IL_88:
				num2 = 5;
			}
		}
		IL_86:
		this.ᜀ.RemoveAt(num);
	}

	// Token: 0x04001D8F RID: 7567
	private List<spr\u23E7.ᜀ> ᜀ;
}
