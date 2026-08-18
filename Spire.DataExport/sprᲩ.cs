using System;
using System.Collections;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

// Token: 0x02000036 RID: 54
internal class sprᲩ : sprᲤ
{
	// Token: 0x060001C2 RID: 450 RVA: 0x00010F08 File Offset: 0x0000FF08
	public sprᲩ(spr\u219E A_0) : base(A_0)
	{
		this.ᜀ = new sprᠪ(A_0);
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00010F28 File Offset: 0x0000FF28
	public override void ᜀ(sprḗ A_0, spr\u1F46 A_1)
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
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x00010F64 File Offset: 0x0000FF64
	public override void ᜀ(sprḗ A_0)
	{
		int a_ = 4;
		int num = 2;
		for (;;)
		{
			IEnumerator enumerator;
			switch (num)
			{
			case 0:
				try
				{
					num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_CE;
						case 2:
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							goto IL_8D;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8D;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						}
						IL_73:
						num = 2;
						continue;
						goto IL_73;
						IL_8D:
						spr\u2320 spr_u = (spr\u2320)enumerator.Current;
						spr_u.ᜀ(A_0);
						num = 3;
					}
					IL_CE:
					goto IL_184;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_111;
							case 1:
								if (disposable != null)
								{
									num = 2;
									continue;
								}
								goto IL_113;
							case 2:
								disposable.Dispose();
								num = 0;
								continue;
							}
							break;
						}
					}
					IL_111:
					IL_113:;
				}
				goto IL_114;
			case 1:
				num = 4;
				continue;
			case 3:
				goto IL_164;
			case 4:
				if (base.\u1715() == null)
				{
					num = 3;
					continue;
				}
				goto IL_114;
			}
			if (base.\u1714() != null)
			{
				if (true)
				{
				}
				num = 1;
				continue;
			}
			break;
			IL_114:
			base.\u1714().ᜀ(A_0);
			enumerator = this.ᜀ.ᜇ();
			num = 0;
		}
		IL_164:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("感倡䌣唥眧礩䤫䴭䐯嬱嬳堵瘷唹䠻爽⼿⍁⁃⍅ⱇ", a_)));
		IL_184:
		base.\u1715().ᜀ(A_0);
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00011114 File Offset: 0x00010114
	public sprᠪ ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x04000098 RID: 152
	private new sprᠪ ᜀ;
}
