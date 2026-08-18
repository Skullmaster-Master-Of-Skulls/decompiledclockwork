using System;
using Spire.Doc.Fields.Shape;

// Token: 0x02000298 RID: 664
internal class sprὔ
{
	// Token: 0x06002353 RID: 9043 RVA: 0x0023EC74 File Offset: 0x0023DC74
	public static byte[] ᜀ(byte[] A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= A_0.Length)
					{
						num2 = 1;
						continue;
					}
					goto IL_77;
				case 1:
					goto IL_30;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7F;
					default:
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 3:
					goto IL_7F;
				case 4:
					goto IL_77;
				case 5:
					goto IL_53;
				case 6:
					if (num > 0)
					{
						num2 = 5;
						continue;
					}
					return A_0;
				}
				break;
				IL_30:
				num--;
				if (true)
				{
				}
				num2 = 6;
				continue;
				IL_77:
				num2 = 3;
				continue;
				IL_7F:
				if (A_0[num++] != 0)
				{
					goto IL_30;
				}
				num2 = 2;
			}
		}
		IL_53:
		byte[] array = new byte[A_0.Length - num];
		Buffer.BlockCopy(A_0, num, array, 0, array.Length);
		return array;
	}

	// Token: 0x06002354 RID: 9044 RVA: 0x0023ED4C File Offset: 0x0023DD4C
	public static byte[] ᜀ(byte[] A_0, int A_1)
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
		byte[] array = new byte[A_1];
		Buffer.BlockCopy(A_0, 0, array, array.Length - A_0.Length, A_0.Length);
		return array;
	}

	// Token: 0x06002355 RID: 9045 RVA: 0x0023EDA4 File Offset: 0x0023DDA4
	public static byte[] ᜀ(Rsa A_0, byte[] A_1)
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
		return A_0.Encrypt(A_1);
	}
}
