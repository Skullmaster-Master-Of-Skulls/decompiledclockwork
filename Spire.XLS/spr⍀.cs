using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000523 RID: 1315
internal class spr\u2340
{
	// Token: 0x06005069 RID: 20585 RVA: 0x00327258 File Offset: 0x00326258
	public List<spr\u22B9> ᜀ()
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

	// Token: 0x0600506A RID: 20586 RVA: 0x0032729C File Offset: 0x0032629C
	public string ᜁ()
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

	// Token: 0x0600506B RID: 20587 RVA: 0x003272E0 File Offset: 0x003262E0
	public void ᜀ(string A_0)
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

	// Token: 0x0600506C RID: 20588 RVA: 0x00327324 File Offset: 0x00326324
	public spr\u2340()
	{
	}

	// Token: 0x0600506D RID: 20589 RVA: 0x00327344 File Offset: 0x00326344
	public spr\u2340(Stream A_0)
	{
		byte[] a_ = new byte[4];
		sprṯ.ᜀ(A_0, a_);
		int num = sprṯ.ᜀ(A_0, a_);
		for (int i = 0; i < num; i++)
		{
			spr\u22B9 item = new spr\u22B9(A_0);
			this.ᜀ.Add(item);
		}
		this.ᜁ = sprṯ.ᜁ(A_0);
	}

	// Token: 0x0600506E RID: 20590 RVA: 0x003273A8 File Offset: 0x003263A8
	public void ᜀ(Stream A_0)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 2;
			long position;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_C9;
				case 1:
					goto IL_57;
				case 3:
					goto IL_C9;
				case 4:
					goto IL_10C;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (false)
						{
						}
						if (true)
						{
						}
						if (num2 >= count)
						{
							num = 4;
							continue;
						}
						spr\u22B9 spr_u22B = this.ᜀ[num2];
						spr_u22B.ᜀ(A_0);
						num2++;
						break;
					}
					}
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				position = A_0.Position;
				A_0.Position += 4L;
				count = this.ᜀ.Count;
				sprṯ.ᜀ(A_0, count);
				num2 = 0;
				num = 3;
				continue;
				IL_C9:
				num = 5;
			}
			IL_57:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬷丹主嬽ℿ⽁", a_));
			IL_10C:
			sprṯ.ᜁ(A_0, this.ᜁ);
			long position2 = A_0.Position;
			A_0.Position = position;
			sprṯ.ᜀ(A_0, (int)(position2 - position));
			A_0.Position = position2;
			return;
		}
		}
	}

	// Token: 0x04002411 RID: 9233
	private List<spr\u22B9> ᜀ = new List<spr\u22B9>();

	// Token: 0x04002412 RID: 9234
	private string ᜁ;
}
