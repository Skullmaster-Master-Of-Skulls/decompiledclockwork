using System;
using System.IO;
using Spire.Doc.Fields.Shape;

// Token: 0x020001E9 RID: 489
internal class sprᦫ
{
	// Token: 0x06001557 RID: 5463 RVA: 0x0015CA84 File Offset: 0x0015BA84
	internal static spr\u171F ᜀ(BinaryReader A_0, sprά A_1)
	{
		spr\u2410 spr_u;
		for (;;)
		{
			spr_u = new spr\u2410(A_0);
			Stream baseStream = A_0.BaseStream;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (baseStream.Position + (long)spr_u.ᜄ() > baseStream.Length)
					{
						goto IL_B5;
					}
					goto IL_C2;
				case 1:
					num = 0;
					continue;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B5;
					default:
						if (false)
						{
						}
						if (spr_u.ᜄ() >= 0)
						{
							num = 1;
							continue;
						}
						goto IL_73;
					}
					break;
				case 3:
					goto IL_92;
				case 4:
					goto IL_73;
				}
				break;
				IL_73:
				spr_u.ᜀ((int)(baseStream.Length - baseStream.Position));
				num = 3;
				continue;
				IL_B5:
				num = 4;
			}
		}
		IL_92:
		IL_C2:
		spr\u171F spr_u171F = sprᦫ.ᜀ(spr_u, A_1);
		spr_u171F.ᜀ(spr_u, A_0, A_1);
		return spr_u171F;
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x0015CB68 File Offset: 0x0015BB68
	private static spr\u171F ᜀ(spr\u2410 A_0, sprά A_1)
	{
		for (;;)
		{
			EsRecordType esRecordType = A_0.ᜅ();
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5E;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EB;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 2:
					num = 4;
					continue;
				case 3:
					num = 0;
					continue;
				case 4:
					switch (esRecordType)
					{
					case EsRecordType.BlipEmf:
					case EsRecordType.BlipPict:
					case EsRecordType.BlipWmf:
						goto IL_48;
					case EsRecordType.BlipJpeg:
					case EsRecordType.BlipPng:
					case EsRecordType.BlipDib:
						goto IL_C2;
					default:
						num = 1;
						continue;
					}
					break;
				case 5:
					if (esRecordType != EsRecordType.Bse)
					{
						num = 2;
						continue;
					}
					goto IL_EB;
				case 6:
					if (esRecordType != EsRecordType.BlipJpeg2)
					{
						num = 3;
						continue;
					}
					goto IL_C2;
				}
				break;
			}
		}
		IL_48:
		if (true)
		{
		}
		return new spr\u22D5();
		IL_5E:
		return null;
		IL_C2:
		return new spr\u1BA5();
		IL_EB:
		return new spr\u239B();
	}
}
