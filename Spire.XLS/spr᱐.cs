using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020005B6 RID: 1462
internal class spr᱐
{
	// Token: 0x06005864 RID: 22628 RVA: 0x003825FC File Offset: 0x003815FC
	public List<spr\u1DAB> ᜁ()
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

	// Token: 0x06005865 RID: 22629 RVA: 0x00382640 File Offset: 0x00381640
	public spr᱐()
	{
		this.ᜀ = new List<spr\u1DAB>();
		base..ctor();
	}

	// Token: 0x06005866 RID: 22630 RVA: 0x00382660 File Offset: 0x00381660
	public spr᱐(byte[] A_0)
	{
		int a_ = 2;
		this.ᜀ = new List<spr\u1DAB>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("尷嬹䠻弽", a_));
		}
		int i = 0;
		int num = A_0.Length;
		this.ᜀ = new List<spr\u1DAB>();
		int num2 = 0;
		while (i < num)
		{
			this.ᜀ.Add(new spr\u1DAB(A_0, i, num2));
			i += 128;
			num2++;
		}
	}

	// Token: 0x06005867 RID: 22631 RVA: 0x003826DC File Offset: 0x003816DC
	public int ᜀ()
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				result = -1;
				int num = 0;
				int count = this.ᜀ.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return result;
					case 1:
					{
						spr\u1DAB spr_u1DAB;
						if (spr_u1DAB.ᜄ() == spr\u1DAB.EntryType.Invalid)
						{
							num2 = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_83;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num++;
							num2 = 4;
							continue;
						}
						break;
					}
					case 2:
						goto IL_C7;
					case 3:
						result = num;
						goto IL_83;
					case 4:
						goto IL_C7;
					case 5:
					{
						if (num >= count)
						{
							num2 = 6;
							continue;
						}
						spr\u1DAB spr_u1DAB = this.ᜀ[num];
						num2 = 1;
						continue;
					}
					case 6:
						return result;
					}
					break;
					IL_83:
					num2 = 0;
					continue;
					IL_C7:
					num2 = 5;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06005868 RID: 22632 RVA: 0x003827D0 File Offset: 0x003817D0
	internal void ᜀ(spr\u1DAB A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			num = this.ᜀ();
			if (num < 0)
			{
				this.ᜀ.Add(A_0);
				return;
			}
			break;
		}
		if (true)
		{
		}
		this.ᜀ[num] = A_0;
		A_0.ᜃ(num);
	}

	// Token: 0x06005869 RID: 22633 RVA: 0x0038283C File Offset: 0x0038183C
	public void ᜀ(Stream A_0)
	{
		int a_ = 7;
		int num = 0;
		for (;;)
		{
			int num2;
			int count;
			switch (num)
			{
			case 1:
				if (true)
				{
				}
				if (num2 >= count)
				{
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
				{
					if (false)
					{
					}
					spr\u1DAB spr_u1DAB = this.ᜀ[num2];
					spr_u1DAB.ᜀ(A_0);
					num2++;
					num = 3;
					continue;
				}
				}
				break;
			case 2:
				goto IL_B5;
			case 3:
				goto IL_B5;
			case 4:
				goto IL_3C;
			case 5:
				return;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num2 = 0;
			count = this.ᜀ.Count;
			num = 2;
			continue;
			IL_B5:
			num = 1;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("丼䬾㍀♂⑄⩆", a_));
	}

	// Token: 0x04002A09 RID: 10761
	private List<spr\u1DAB> ᜀ;
}
