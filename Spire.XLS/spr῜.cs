using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003AB RID: 939
internal abstract class spr\u1FDC : Stream
{
	// Token: 0x060038E8 RID: 14568 RVA: 0x001FBAEC File Offset: 0x001FAAEC
	public spr\u1FDC(string A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x060038E9 RID: 14569 RVA: 0x001FBB08 File Offset: 0x001FAB08
	public virtual void ᜀ(spr\u1FDC A_0)
	{
		int a_ = 6;
		int num = 5;
		for (;;)
		{
			byte[] buffer;
			switch (num)
			{
			case 0:
			{
				int count;
				if ((count = this.Read(buffer, 0, 32768)) <= 0)
				{
					num = 4;
					continue;
				}
				A_0.Write(buffer, 0, count);
				goto IL_4F;
			}
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4F;
				default:
					if (false)
					{
					}
					goto IL_AF;
				}
				break;
			case 2:
				goto IL_3C;
			case 3:
				goto IL_AF;
			case 4:
				return;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			buffer = new byte[32768];
			long position = this.Position;
			num = 1;
			continue;
			IL_4F:
			num = 3;
			continue;
			IL_AF:
			num = 0;
		}
		IL_3C:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("伻䨽㈿❁╃⭅", a_));
	}

	// Token: 0x060038EA RID: 14570 RVA: 0x001FBBF4 File Offset: 0x001FABF4
	public string ᜋ()
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

	// Token: 0x060038EB RID: 14571 RVA: 0x001FBC38 File Offset: 0x001FAC38
	protected void ᜀ(string A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0400190A RID: 6410
	private string ᜀ;
}
