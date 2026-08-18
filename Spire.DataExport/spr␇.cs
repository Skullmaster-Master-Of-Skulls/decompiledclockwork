using System;
using Spire.DataExport.XLS.Formula;

// Token: 0x02000064 RID: 100
internal class spr\u2407 : sprạ
{
	// Token: 0x06000339 RID: 825 RVA: 0x0001EC78 File Offset: 0x0001DC78
	public ushort ᜃ()
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

	// Token: 0x0600033A RID: 826 RVA: 0x0001ECBC File Offset: 0x0001DCBC
	public ushort ᜂ()
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

	// Token: 0x0600033B RID: 827 RVA: 0x0001ED00 File Offset: 0x0001DD00
	public override int ᜄ()
	{
		if (base.\u170D() == 25)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return 5;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return 4;
		}
		return 5;
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0001ED4C File Offset: 0x0001DD4C
	public spr\u2407(FormulaTokenCode A_0) : base(A_0, 5, FormulaTokenType.Control)
	{
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0001ED64 File Offset: 0x0001DD64
	public override void ᜀ(byte[] A_0, int A_1)
	{
		if (base.\u170D() != 25)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_0A;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜁ = BitConverter.ToUInt16(A_0, A_1);
			this.ᜀ = BitConverter.ToUInt16(A_0, A_1 + 2);
			return;
		}
		IL_0A:
		this.ᜂ = new byte[]
		{
			A_0[A_1],
			A_0[A_1 + 1],
			A_0[A_1 + 2]
		};
	}

	// Token: 0x0600033E RID: 830 RVA: 0x0001EDEC File Offset: 0x0001DDEC
	public override byte[] ᜁ()
	{
		byte[] array;
		for (;;)
		{
			array = base.ᜁ();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8E;
				case 1:
					if (this.ᜂ == null)
					{
						BitConverter.GetBytes(this.ᜁ).CopyTo(array, 1);
						BitConverter.GetBytes(this.ᜃ()).CopyTo(array, 3);
						if (true)
						{
						}
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8E;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					return array;
				case 3:
					return array;
				}
				break;
				IL_8E:
				this.ᜂ.CopyTo(array, 1);
				num = 3;
			}
		}
		return array;
	}

	// Token: 0x04000258 RID: 600
	private new ushort ᜀ;

	// Token: 0x04000259 RID: 601
	private new ushort ᜁ;

	// Token: 0x0400025A RID: 602
	private byte[] ᜂ;
}
