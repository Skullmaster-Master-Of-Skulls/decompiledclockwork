using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using Spire.CompoundFile.Doc;

// Token: 0x0200039E RID: 926
[DefaultMember("Item")]
internal class spr\u1D3C
{
	// Token: 0x06003441 RID: 13377 RVA: 0x00300AA0 File Offset: 0x002FFAA0
	internal spr\u1D3C(sprᲨ A_0) : this()
	{
		this.ᜀ.Add(A_0);
	}

	// Token: 0x06003442 RID: 13378 RVA: 0x00300AC0 File Offset: 0x002FFAC0
	internal spr\u1D3C(sprᲨ A_0, sprᲨ A_1) : this()
	{
		this.ᜀ.Add(A_0);
		this.ᜀ.Add(A_1);
	}

	// Token: 0x06003443 RID: 13379 RVA: 0x00300AF0 File Offset: 0x002FFAF0
	internal spr\u1D3C(spr\u1D3C A_0) : this()
	{
		this.ᜀ.AddRange(A_0.ᜀ);
	}

	// Token: 0x06003444 RID: 13380 RVA: 0x00300B14 File Offset: 0x002FFB14
	internal spr\u1D3C()
	{
		this.ᜀ = new ArrayList();
	}

	// Token: 0x06003445 RID: 13381 RVA: 0x00300B34 File Offset: 0x002FFB34
	internal void ᜂ(sprᲨ A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				ArrayList arrayList = (ArrayList)this.ᜀ.Clone();
				bool flag = false;
				int num = 0;
				int num2 = 7;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						spr\u1D3C spr_u1D3C;
						if (spr_u1D3C.ᜀ() == 1)
						{
							num2 = 5;
							continue;
						}
						goto IL_84;
					}
					case 1:
					{
						if (num >= arrayList.Count)
						{
							num2 = 9;
							continue;
						}
						sprᲨ sprᲨ = (sprᲨ)arrayList[num];
						spr\u1D3C spr_u1D3C = spr\u1B69.ᜇ(sprᲨ, A_0);
						num2 = 0;
						continue;
					}
					case 2:
						goto IL_84;
					case 3:
						if (!flag)
						{
							if (true)
							{
							}
							num2 = 6;
							continue;
						}
						return;
					case 4:
						goto IL_156;
					case 5:
					{
						flag = true;
						sprᲨ sprᲨ;
						this.ᜀ.Remove(sprᲨ);
						this.ᜀ.Remove(A_0);
						spr\u1D3C spr_u1D3C;
						A_0 = spr_u1D3C.ᜀ(0);
						this.ᜀ.Add(A_0);
						num2 = 2;
						continue;
					}
					case 6:
						this.ᜀ.Add(A_0);
						goto IL_106;
					case 7:
						goto IL_156;
					case 8:
						return;
					case 9:
						num2 = 3;
						continue;
					}
					break;
					IL_84:
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_106:
						num2 = 8;
						continue;
					}
					if (false)
					{
					}
					num2 = 4;
					continue;
					IL_156:
					num2 = 1;
				}
			}
			return;
		}
	}

	// Token: 0x06003446 RID: 13382 RVA: 0x00300CC0 File Offset: 0x002FFCC0
	internal void ᜀ(spr\u1D3C A_0)
	{
		IEnumerator enumerator = A_0.ᜀ.GetEnumerator();
		try
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6D;
				case 1:
				{
					if (!enumerator.MoveNext())
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					sprᲨ a_ = (sprᲨ)enumerator.Current;
					this.ᜂ(a_);
					num = 0;
					continue;
				}
				case 3:
					goto IL_97;
				case 4:
					goto IL_8D;
				}
				goto IL_32;
				IL_6D:
				num = 1;
				continue;
				IL_32:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_8D:
					num = 3;
					break;
				default:
					if (false)
					{
					}
					goto IL_6D;
				}
			}
			IL_97:;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_D7;
					case 1:
						if (disposable != null)
						{
							num = 2;
							continue;
						}
						goto IL_D9;
					case 2:
						disposable.Dispose();
						num = 0;
						continue;
					}
					break;
				}
			}
			IL_D7:
			IL_D9:;
		}
	}

	// Token: 0x06003447 RID: 13383 RVA: 0x00300DC4 File Offset: 0x002FFDC4
	internal void ᜀ(sprᲨ A_0)
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
		this.ᜀ.Add(A_0);
	}

	// Token: 0x06003448 RID: 13384 RVA: 0x00300E0C File Offset: 0x002FFE0C
	internal void ᜀ(bool A_0)
	{
		for (;;)
		{
			IL_22:
			int num = 0;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_88:
				num2 = 0;
				break;
			default:
				if (false)
				{
				}
				num2 = 1;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_4A;
				case 1:
					goto IL_4A;
				case 2:
					return;
				case 3:
					if (num >= this.ᜀ())
					{
						if (true)
						{
						}
						num2 = 2;
						continue;
					}
					goto IL_6D;
				}
				goto IL_22;
				IL_4A:
				num2 = 3;
			}
			IL_6D:
			((sprᲨ)this.ᜀ[num]).ᜀ(A_0);
			num++;
			goto IL_88;
		}
	}

	// Token: 0x06003449 RID: 13385 RVA: 0x00300EB0 File Offset: 0x002FFEB0
	internal void ᜀ(spr\u25FD A_0)
	{
		IEnumerator enumerator = this.ᜀ.GetEnumerator();
		try
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8D;
				case 1:
					goto IL_97;
				case 3:
					goto IL_75;
				case 4:
				{
					if (!enumerator.MoveNext())
					{
						num = 0;
						continue;
					}
					sprᲨ sprᲨ = (sprᲨ)enumerator.Current;
					sprᲨ.ᜀ(A_0);
					num = 3;
					continue;
				}
				}
				goto IL_32;
				IL_75:
				num = 4;
				continue;
				IL_32:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_8D:
					num = 1;
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					goto IL_75;
				}
			}
			IL_97:;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_D7;
					case 1:
						if (disposable != null)
						{
							num = 2;
							continue;
						}
						goto IL_D9;
					case 2:
						disposable.Dispose();
						num = 0;
						continue;
					}
					break;
				}
			}
			IL_D7:
			IL_D9:;
		}
	}

	// Token: 0x0600344A RID: 13386 RVA: 0x00300FB4 File Offset: 0x002FFFB4
	internal sprᲨ[] ᜂ()
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
		return (sprᲨ[])this.ᜀ.ToArray(typeof(sprᲨ));
	}

	// Token: 0x0600344B RID: 13387 RVA: 0x0030100C File Offset: 0x0030000C
	internal RectangleF ᜁ()
	{
		ArrayList arrayList;
		for (;;)
		{
			arrayList = new ArrayList();
			int num = 0;
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (arrayList.Count != 0)
					{
						num2 = 2;
						continue;
					}
					goto IL_D6;
				case 1:
					num2 = 0;
					continue;
				case 2:
					goto IL_55;
				case 3:
					goto IL_5F;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5F;
					default:
						if (false)
						{
						}
						goto IL_57;
					}
					break;
				case 5:
					if (true)
					{
					}
					goto IL_57;
				}
				break;
				IL_57:
				num2 = 3;
				continue;
				IL_5F:
				if (num >= this.ᜀ())
				{
					num2 = 1;
				}
				else
				{
					arrayList.AddRange(this.ᜀ(num).ᜀ());
					num++;
					num2 = 4;
				}
			}
		}
		IL_55:
		return sprὍ.ᜁ((PointF[])arrayList.ToArray(typeof(PointF)));
		IL_D6:
		return RectangleF.Empty;
	}

	// Token: 0x0600344C RID: 13388 RVA: 0x003010F4 File Offset: 0x003000F4
	internal void ᜁ(sprᲨ A_0)
	{
		ArrayList arrayList;
		for (;;)
		{
			arrayList = new ArrayList(this.ᜀ.Count);
			int num = 0;
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					sprᲨ sprᲨ;
					arrayList.Add(sprᲨ);
					num2 = 6;
					continue;
				}
				case 1:
				{
					sprᲨ sprᲨ;
					if (sprᲨ.ᜅ() != 0)
					{
						num2 = 0;
						continue;
					}
					goto IL_41;
				}
				case 2:
					goto IL_8C;
				case 3:
					goto IL_D9;
				case 4:
				{
					if (num >= this.ᜀ())
					{
						num2 = 3;
						continue;
					}
					sprᲨ sprᲨ = spr\u1B69.ᜆ(A_0, this.ᜀ(num));
					num2 = 1;
					continue;
				}
				case 5:
					goto IL_8C;
				case 6:
					goto IL_41;
				}
				break;
				IL_41:
				num++;
				num2 = 2;
				continue;
				IL_8C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DB;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				num2 = 4;
			}
		}
		IL_D9:
		IL_DB:
		this.ᜀ.Clear();
		this.ᜀ.AddRange(arrayList);
	}

	// Token: 0x0600344D RID: 13389 RVA: 0x003011F4 File Offset: 0x003001F4
	internal int ᜀ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.ᜀ.Count;
	}

	// Token: 0x0600344E RID: 13390 RVA: 0x0030123C File Offset: 0x0030023C
	internal sprᲨ ᜀ(int A_0)
	{
		int a_ = 13;
		for (;;)
		{
			IL_09:
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_09;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 2:
						if (A_0 >= this.ᜀ())
						{
							num = 3;
							continue;
						}
						goto IL_94;
					case 3:
						goto IL_92;
					}
					if (0 > A_0)
					{
						goto IL_65;
					}
					num = 0;
					break;
				}
			}
		}
		IL_65:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᩲ᭴፶ᱸͺ", a_));
		IL_92:
		goto IL_65;
		IL_94:
		return (sprᲨ)this.ᜀ[A_0];
	}

	// Token: 0x0400284F RID: 10319
	private readonly ArrayList ᜀ;
}
