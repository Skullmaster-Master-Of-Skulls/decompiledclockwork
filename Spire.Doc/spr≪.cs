using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200038B RID: 907
[CLSCompliant(false)]
internal class spr\u226A
{
	// Token: 0x06003281 RID: 12929 RVA: 0x002E7B14 File Offset: 0x002E6B14
	internal List<spr\u2105> ᜀ()
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

	// Token: 0x06003282 RID: 12930 RVA: 0x002E7B58 File Offset: 0x002E6B58
	internal string ᜁ()
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
		return this.ᜁ;
	}

	// Token: 0x06003283 RID: 12931 RVA: 0x002E7B9C File Offset: 0x002E6B9C
	internal void ᜀ(string A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06003284 RID: 12932 RVA: 0x002E7BE0 File Offset: 0x002E6BE0
	internal spr\u226A()
	{
	}

	// Token: 0x06003285 RID: 12933 RVA: 0x002E7C0C File Offset: 0x002E6C0C
	internal spr\u226A(Stream A_0)
	{
		byte[] a_ = new byte[4];
		this.ᜂ.ᜀ(A_0, a_);
		int num = this.ᜂ.ᜀ(A_0, a_);
		for (int i = 0; i < num; i++)
		{
			spr\u2105 item = new spr\u2105(A_0);
			this.ᜀ.Add(item);
		}
		this.ᜁ = this.ᜂ.ᜁ(A_0);
	}

	// Token: 0x06003286 RID: 12934 RVA: 0x002E7C90 File Offset: 0x002E6C90
	internal void ᜀ(Stream A_0)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 3;
			long position;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
				{
					if (true)
					{
					}
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					spr\u2105 spr_u = this.ᜀ[num2];
					spr_u.ᜀ(A_0);
					num2++;
					num = 5;
					continue;
				}
				case 1:
					goto IL_112;
				case 2:
					goto IL_EB;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_60;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 4:
					goto IL_73;
				case 5:
					goto IL_EB;
				}
				goto IL_5D;
				IL_60:
				num = 4;
				continue;
				IL_EB:
				num = 0;
				continue;
				IL_5D:
				if (A_0 == null)
				{
					goto IL_60;
				}
				position = A_0.Position;
				A_0.Position += 4L;
				count = this.ᜀ.Count;
				this.ᜂ.ᜀ(A_0, count);
				num2 = 0;
				num = 2;
			}
			IL_73:
			throw new ArgumentNullException(ClipboardData.b("੸ེོ᩾", a_));
			IL_112:
			this.ᜂ.ᜀ(A_0, this.ᜁ);
			long position2 = A_0.Position;
			A_0.Position = position;
			this.ᜂ.ᜀ(A_0, (int)(position2 - position));
			A_0.Position = position2;
			return;
		}
		}
	}

	// Token: 0x040027F0 RID: 10224
	private List<spr\u2105> ᜀ = new List<spr\u2105>();

	// Token: 0x040027F1 RID: 10225
	private string ᜁ;

	// Token: 0x040027F2 RID: 10226
	private spr\u1AED ᜂ = new spr\u1AED();
}
