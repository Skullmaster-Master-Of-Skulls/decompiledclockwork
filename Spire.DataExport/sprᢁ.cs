using System;
using System.Collections;
using System.Reflection;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

// Token: 0x020000FD RID: 253
[DefaultMember("Item")]
internal class sprᢁ : IEnumerable
{
	// Token: 0x0600055C RID: 1372 RVA: 0x00033F88 File Offset: 0x00032F88
	public IEnumerator ᜀ()
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

	// Token: 0x0600055D RID: 1373 RVA: 0x00033FD0 File Offset: 0x00032FD0
	public int ᜁ(spr\u17ED A_0)
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

	// Token: 0x0600055E RID: 1374 RVA: 0x00034018 File Offset: 0x00033018
	public int ᜀ(spr\u17ED A_0)
	{
		int num2;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6D:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				num2 = 0;
				num = 0;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_91;
				case 1:
					goto IL_91;
				case 2:
					goto IL_8F;
				case 3:
					if (this.ᜀ(num2).ᜀ(A_0))
					{
						num = 2;
						continue;
					}
					num2++;
					num = 1;
					continue;
				case 4:
					if (true)
					{
					}
					if (num2 >= this.ᜁ())
					{
						num = 5;
						continue;
					}
					goto IL_6D;
				case 5:
					return -1;
				}
				break;
				IL_91:
				num = 4;
			}
		}
		IL_8F:
		return (int)this.ᜀ(num2).ᜀ();
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x000340E0 File Offset: 0x000330E0
	public int ᜀ(ushort A_0)
	{
		int num2;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_62:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num2 = 0;
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					return -1;
				case 1:
					goto IL_83;
				case 2:
					if (this.ᜀ(num2).ᜀ() == A_0)
					{
						num = 4;
						continue;
					}
					num2++;
					num = 3;
					continue;
				case 3:
					goto IL_83;
				case 4:
					return num2;
				case 5:
					if (true)
					{
					}
					if (num2 >= this.ᜁ())
					{
						num = 0;
						continue;
					}
					goto IL_62;
				}
				break;
				IL_83:
				num = 5;
			}
		}
		return num2;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0003419C File Offset: 0x0003319C
	public spr\u17ED ᜁ(ushort A_0)
	{
		int num2;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_68:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				num2 = 0;
				num = 4;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B3;
				case 1:
					if (this.ᜀ(num2).ᜀ() == A_0)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					num2++;
					num = 5;
					continue;
				case 2:
					goto IL_92;
				case 3:
					if (num2 >= this.ᜁ())
					{
						num = 0;
						continue;
					}
					goto IL_68;
				case 4:
					goto IL_94;
				case 5:
					goto IL_94;
				}
				break;
				IL_94:
				num = 3;
			}
		}
		IL_92:
		return this.ᜀ(num2);
		IL_B3:
		return null;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00034260 File Offset: 0x00033260
	public spr\u17ED ᜀ(int A_0)
	{
		int a_ = 1;
		int num = 2;
		for (;;)
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
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_AF;
				case 1:
					if (A_0 >= this.ᜀ.Count)
					{
						num = 0;
						continue;
					}
					goto IL_B1;
				case 3:
					goto IL_8E;
				}
				if (A_0 >= 0)
				{
					num = 3;
					continue;
				}
				goto IL_65;
			}
			IL_8E:
			num = 1;
		}
		IL_65:
		throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("吜焞圠䈢䤤並䴨搪崬䨮䌰刲䄴帶嘸唺戼瘾⽀❂⁄㽆و㹊㥌N㝐ᅒ㩔≖㝘㽚⹜", a_)), A_0));
		IL_AF:
		goto IL_65;
		IL_B1:
		return this.ᜀ[A_0] as spr\u17ED;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00034330 File Offset: 0x00033330
	public void ᜀ(int A_0, spr\u17ED A_1)
	{
		int a_ = 12;
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					if (A_0 >= this.ᜀ.Count)
					{
						num = 2;
						continue;
					}
					goto IL_B1;
				case 2:
					goto IL_AF;
				case 3:
					goto IL_8E;
				}
				if (A_0 >= 0)
				{
					num = 3;
					continue;
				}
				goto IL_65;
			}
			IL_8E:
			num = 0;
		}
		IL_65:
		throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("愧䐩娫伭尯嬱倳礵䠷弹主弽㐿⭁⭃⡅ᝇ͉≋⩍㕏⩑᭓⍕ⱗᕙ㩛ᱝཟᝡ੣ɥ᭧", a_)), A_0));
		IL_AF:
		goto IL_65;
		IL_B1:
		this.ᜀ[A_0] = A_1;
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x000343FC File Offset: 0x000333FC
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

	// Token: 0x0400057F RID: 1407
	private ArrayList ᜀ = new ArrayList();
}
