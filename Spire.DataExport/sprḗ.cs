using System;
using System.IO;

// Token: 0x02000003 RID: 3
internal abstract class sprḗ : Stream
{
	// Token: 0x06000005 RID: 5 RVA: 0x000032D4 File Offset: 0x000022D4
	public sprḗ()
	{
	}

	// Token: 0x06000006 RID: 6
	public abstract int ᜀ(byte[] A_0, int A_1);

	// Token: 0x06000007 RID: 7
	public abstract int ᜁ(byte[] A_0, int A_1);

	// Token: 0x06000008 RID: 8 RVA: 0x000032E8 File Offset: 0x000022E8
	public virtual long ᜀ(long A_0, SeekOrigin A_1)
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
		return 0L;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00003328 File Offset: 0x00002328
	public long ᜀ(Stream A_0, long A_1)
	{
		switch (0)
		{
		default:
		{
			long result;
			for (;;)
			{
				int num = 0;
				int num2 = 0;
				byte[] buffer = null;
				int num3 = 12;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_D2;
					case 1:
						goto IL_194;
					case 2:
						goto IL_127;
					case 3:
						if (A_1 == 0L)
						{
							num3 = 9;
							continue;
						}
						num3 = 6;
						continue;
					case 4:
						if (A_1 > 61440L)
						{
							num3 = 13;
							continue;
						}
						num = (int)A_1;
						num3 = 1;
						continue;
					case 5:
						goto IL_A7;
					case 6:
						if (A_1 > (long)num)
						{
							num3 = 10;
							continue;
						}
						num2 = (int)A_1;
						num3 = 7;
						continue;
					case 7:
						goto IL_D2;
					case 8:
						goto IL_127;
					case 9:
						return result;
					case 10:
						if (true)
						{
						}
						num2 = num;
						num3 = 0;
						continue;
					case 11:
						A_0.Position = 0L;
						A_1 = A_0.Length;
						num3 = 5;
						continue;
					case 12:
						if (A_1 == 0L)
						{
							num3 = 11;
							continue;
						}
						goto IL_A7;
					case 13:
						num = 61440;
						goto IL_16A;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_16A;
						default:
							if (false)
							{
							}
							goto IL_194;
						}
						break;
					}
					break;
					IL_A7:
					result = A_1;
					num3 = 4;
					continue;
					IL_D2:
					A_0.Read(buffer, 0, num2);
					this.Write(buffer, 0, num2);
					A_1 -= (long)num2;
					num3 = 2;
					continue;
					IL_127:
					num3 = 3;
					continue;
					IL_16A:
					num3 = 14;
					continue;
					IL_194:
					buffer = new byte[num];
					num3 = 8;
				}
			}
			return result;
		}
		}
	}
}
