using System;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200032A RID: 810
internal class spr\u1DD2
{
	// Token: 0x060031FF RID: 12799 RVA: 0x001CD9DC File Offset: 0x001CC9DC
	private spr\u1DD2()
	{
	}

	// Token: 0x06003200 RID: 12800 RVA: 0x001CD9F0 File Offset: 0x001CC9F0
	public spr\u1DD2(byte[] A_0)
	{
		int a_ = 0;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("圵䨷䠹縻䬽☿⑁⅃㑅", a_));
		}
		this.ᜀ = A_0;
		this.ᜀ();
	}

	// Token: 0x06003201 RID: 12801 RVA: 0x001CDA34 File Offset: 0x001CCA34
	public virtual bool ᜀ(object A_0)
	{
		int num = 2;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_A8;
			case 1:
				if (array == null)
				{
					num = 3;
					continue;
				}
				goto IL_DC;
			case 3:
				goto IL_C1;
			case 4:
				if (A_0 is spr\u1DD2)
				{
					num = 9;
					continue;
				}
				num = 8;
				continue;
			case 5:
				return false;
			case 6:
				if (true)
				{
				}
				goto IL_A8;
			case 7:
				array = (byte[])A_0;
				num = 6;
				continue;
			case 8:
				if (A_0 is byte[])
				{
					num = 7;
					continue;
				}
				goto IL_A8;
			case 9:
				array = ((spr\u1DD2)A_0).ᜀ;
				num = 0;
				continue;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			array = null;
			num = 4;
			continue;
			IL_A8:
			num = 1;
		}
		return false;
		IL_C1:
		return false;
		IL_DC:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return false;
		default:
		{
			if (false)
			{
			}
			byte[] array;
			return BiffRecordRaw.CompareArrays(this.ᜀ, array);
		}
		}
	}

	// Token: 0x06003202 RID: 12802 RVA: 0x001CDB48 File Offset: 0x001CCB48
	public virtual int ᜁ()
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

	// Token: 0x06003203 RID: 12803 RVA: 0x001CDB8C File Offset: 0x001CCB8C
	private void ᜀ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = this.ᜀ.Length;
				int num2 = num / 4;
				this.ᜁ = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 1;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8D;
						default:
							goto IL_B9;
						}
						break;
					case 1:
						goto IL_4A;
					case 2:
						if (num3 >= num2)
						{
							num5 = 0;
							continue;
						}
						this.ᜁ |= BitConverter.ToInt32(this.ᜀ, num4);
						num3++;
						num4 += 4;
						goto IL_8D;
					case 3:
						goto IL_4A;
					}
					break;
					IL_4A:
					num5 = 2;
					continue;
					IL_8D:
					num5 = 3;
				}
			}
			IL_B9:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x040015F1 RID: 5617
	private byte[] ᜀ;

	// Token: 0x040015F2 RID: 5618
	private int ᜁ;
}
