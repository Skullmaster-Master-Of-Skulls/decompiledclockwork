using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200019C RID: 412
internal abstract class spr\u2578 : Stream
{
	// Token: 0x06000FEB RID: 4075 RVA: 0x000F7CAC File Offset: 0x000F6CAC
	public spr\u2578(string A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x000F7CC8 File Offset: 0x000F6CC8
	public virtual void ᜀ(spr\u2578 A_0)
	{
		int a_ = 3;
		int num = 1;
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
					num = 3;
					continue;
				}
				A_0.Write(buffer, 0, count);
				goto IL_6B;
			}
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6B;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				goto IL_AF;
			case 3:
				return;
			case 4:
				goto IL_AF;
			case 5:
				goto IL_60;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			buffer = new byte[32768];
			long position = this.Position;
			num = 2;
			continue;
			IL_6B:
			num = 4;
			continue;
			IL_AF:
			num = 0;
		}
		IL_60:
		throw new ArgumentNullException(ClipboardData.b("ᩨὪὬ੮ၰṲ", a_));
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x000F7DB0 File Offset: 0x000F6DB0
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

	// Token: 0x06000FEE RID: 4078 RVA: 0x000F7DF4 File Offset: 0x000F6DF4
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

	// Token: 0x04001798 RID: 6040
	private string ᜀ;
}
