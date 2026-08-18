using System;
using System.IO;

// Token: 0x02000269 RID: 617
internal class spr\u2432
{
	// Token: 0x06002065 RID: 8293 RVA: 0x00223484 File Offset: 0x00222484
	internal spr\u2432()
	{
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x00223498 File Offset: 0x00222498
	internal static void ᜀ(BinaryReader A_0, int A_1, spr\u1ACD A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_37:
				int num = A_1 + 1;
				int[] array = new int[num];
				int num2 = 0;
				for (;;)
				{
					IL_4E:
					int num3 = 7;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_AA;
						case 1:
						{
							int num4 = 0;
							num3 = 5;
							continue;
						}
						case 2:
							goto IL_E4;
						case 3:
							return;
						case 4:
							if (num2 >= num)
							{
								num3 = 1;
								continue;
							}
							array[num2] = A_0.ReadInt32();
							num2++;
							num3 = 2;
							continue;
						case 5:
							goto IL_AA;
						case 6:
						{
							int num4;
							if (num4 >= A_1)
							{
								num3 = 3;
								continue;
							}
							A_2(A_0, array[num4], array[num4 + 1]);
							num4++;
							if (true)
							{
							}
							num3 = 0;
							continue;
						}
						case 7:
							goto IL_E4;
						}
						goto IL_37;
						IL_AA:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4E;
						default:
							if (false)
							{
							}
							num3 = 6;
							continue;
						}
						IL_E4:
						num3 = 4;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002067 RID: 8295 RVA: 0x002235A8 File Offset: 0x002225A8
	internal static void ᜀ(BinaryReader A_0, int A_1, int A_2, spr\u1ACD A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_4D:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int a_ = (A_1 - 4) / (4 + A_2);
				spr\u2432.ᜀ(A_0, a_, A_3);
				num = 2;
				continue;
			}
			case 1:
				goto IL_2E;
			case 2:
				return;
			}
			goto IL_4A;
		}
		IL_2E:
		if (true)
		{
		}
		IL_4A:
		if (A_1 != 0)
		{
			goto IL_4D;
		}
	}
}
