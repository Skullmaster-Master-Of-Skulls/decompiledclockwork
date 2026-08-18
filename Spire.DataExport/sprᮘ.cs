using System;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;
using Spire.XLS.File;

// Token: 0x0200001B RID: 27
internal class sprᮘ : spr\u1DEE
{
	// Token: 0x06000102 RID: 258 RVA: 0x0000A7E4 File Offset: 0x000097E4
	public sprᮘ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0000A7FC File Offset: 0x000097FC
	protected override BiffCellType ᜂ()
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
		return BiffCellType.Boolean;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000A838 File Offset: 0x00009838
	protected override bool ᜆ()
	{
		if (base.ᜢ()[6] == 0)
		{
			if (true)
			{
			}
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
		return true;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000A884 File Offset: 0x00009884
	protected override void ᜁ(bool A_0)
	{
		base.ᜢ()[7] = 0;
		if (A_0)
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
				base.ᜢ()[6] = 1;
				return;
			}
		}
		base.ᜢ()[6] = 0;
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000A8E0 File Offset: 0x000098E0
	protected override object ᜀ()
	{
		if (base.ᜢ()[7] == 0)
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
				return this.ᜆ();
			}
		}
		return sprᮌ.ᜀ(base.ᜢ()[6]);
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0000A940 File Offset: 0x00009940
	protected override void ᜀ(object A_0)
	{
		int a_ = 16;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				goto IL_104;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_9B;
				}
				break;
			case 2:
				goto IL_FF;
			case 4:
				goto IL_66;
			case 5:
				if (A_0 is string)
				{
					num = 2;
					continue;
				}
				num = 0;
				continue;
			}
			if (A_0 is bool)
			{
				num = 1;
			}
			else
			{
				num = 5;
			}
		}
		IL_66:
		throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("洫尭圯䄱欳电崷嘹倻栽ℿ⹁ㅃ⍅", a_)), string.Empty));
		IL_9B:
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜁ((bool)A_0);
		return;
		IL_FF:
		base.ᜢ()[7] = 1;
		base.ᜢ()[6] = sprᮌ.ᜅ(A_0 as string);
		return;
		IL_104:
		throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("洫尭圯䄱欳电崷嘹倻栽ℿ⹁ㅃ⍅", a_)), A_0 as string));
	}
}
