using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200040E RID: 1038
internal class spr\u193B
{
	// Token: 0x06003E6F RID: 15983 RVA: 0x00229D54 File Offset: 0x00228D54
	[CLSCompliant(false)]
	public static uint ᜀ(byte[] A_0, int A_1, int A_2, uint A_3)
	{
		int a_ = 0;
		int num = 1;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				goto IL_3C;
			case 2:
				goto IL_96;
			case 3:
			{
				if (num2 >= num3)
				{
					num = 4;
					continue;
				}
				uint num4 = spr\u193B.ᜀ[(int)((UIntPtr)((A_3 ^ (uint)A_0[num2]) & 255U))];
				A_3 = (A_3 >> 8 ^ num4);
				num2++;
				num = 5;
				continue;
			}
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_D0;
				}
				break;
			case 5:
				goto IL_96;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			A_3 = ~A_3;
			num2 = A_1;
			num3 = A_1 + A_2;
			num = 2;
			continue;
			IL_96:
			num = 3;
		}
		IL_3C:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("吵䴷尹娻嬽㈿", a_));
		IL_D0:
		if (false)
		{
		}
		return ~A_3;
	}

	// Token: 0x06003E70 RID: 15984 RVA: 0x00229E3C File Offset: 0x00228E3C
	[CLSCompliant(false)]
	public static uint ᜀ(Stream A_0, int A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 10;
			for (;;)
			{
				int num2;
				uint num4;
				int num6;
				switch (num)
				{
				case 0:
					goto IL_157;
				case 1:
				{
					if (num2 <= 0)
					{
						num = 9;
						continue;
					}
					int count = Math.Min(num2, 4096);
					byte[] array;
					int num3 = A_0.Read(array, 0, count);
					num = 6;
					continue;
				}
				case 2:
					goto IL_157;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5C;
					default:
						goto IL_1BE;
					}
					break;
				case 4:
					num = 7;
					continue;
				case 5:
					goto IL_152;
				case 6:
				{
					int num3;
					if (num3 == 0)
					{
						num = 5;
						continue;
					}
					byte[] array;
					num4 = spr\u193B.ᜀ(array, 0, num3, num4);
					num2 -= num3;
					num = 2;
					continue;
				}
				case 7:
				{
					if (A_1 < 0)
					{
						num = 3;
						continue;
					}
					int num5 = Math.Min(num6, 4096);
					byte[] array = new byte[num5];
					num = 0;
					continue;
				}
				case 8:
					if (num6 >= A_1)
					{
						num = 4;
						continue;
					}
					goto IL_175;
				case 9:
					return num4;
				case 11:
					goto IL_65;
				}
				goto IL_59;
				IL_5C:
				num = 11;
				continue;
				IL_59:
				if (A_0 == null)
				{
					goto IL_5C;
				}
				if (true)
				{
				}
				num4 = 0U;
				num2 = A_1;
				num6 = (int)(A_0.Length - A_0.Position);
				num = 8;
				continue;
				IL_157:
				num = 1;
			}
			IL_65:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬷丹主嬽ℿ⽁", a_));
			IL_152:
			throw new sprớ(RecordTableEnumerator.b("紷吹堻ḽ⼿⑁摃㕅㱇㡉⥋⽍㵏牑♓㍕㥗㥙㑛㭝џ", a_));
			IL_175:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("吷弹刻夽㐿⩁", a_));
			IL_1BE:
			if (false)
			{
			}
			goto IL_175;
		}
		}
	}

	// Token: 0x06003E71 RID: 15985 RVA: 0x0022A010 File Offset: 0x00229010
	[CLSCompliant(false)]
	public static uint[] ᜀ(uint A_0)
	{
		switch (0)
		{
		default:
		{
			uint[] array;
			for (;;)
			{
				array = new uint[256];
				uint num = 0U;
				int num2 = 11;
				for (;;)
				{
					uint num4;
					switch (num2)
					{
					case 0:
						goto IL_81;
					case 1:
						goto IL_81;
					case 2:
						goto IL_69;
					case 3:
					{
						if (true)
						{
						}
						uint num3 = num3 >> 1 ^ A_0;
						num2 = 0;
						continue;
					}
					case 4:
					{
						if (num >= 256U)
						{
							num2 = 7;
							continue;
						}
						uint num3 = num;
						num4 = 8U;
						num2 = 2;
						continue;
					}
					case 5:
					{
						uint num3;
						array[(int)((UIntPtr)num)] = num3;
						num += 1U;
						num2 = 9;
						continue;
					}
					case 6:
					{
						uint num3;
						if ((num3 & 1U) != 0U)
						{
							num2 = 3;
							continue;
						}
						num3 >>= 1;
						num2 = 1;
						continue;
					}
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_67;
						default:
							goto IL_144;
						}
						break;
					case 8:
						goto IL_69;
					case 9:
						goto IL_AF;
					case 10:
						if (num4 < 1U)
						{
							num2 = 5;
							continue;
						}
						num2 = 6;
						continue;
					case 11:
						goto IL_67;
					}
					break;
					IL_69:
					num2 = 10;
					continue;
					IL_81:
					num4 -= 1U;
					num2 = 8;
					continue;
					IL_AF:
					num2 = 4;
					continue;
					IL_67:
					goto IL_AF;
				}
			}
			IL_144:
			if (false)
			{
			}
			return array;
		}
		}
	}

	// Token: 0x06003E73 RID: 15987 RVA: 0x0022A17C File Offset: 0x0022917C
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u193B()
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
		spr\u193B.ᜀ = new uint[]
		{
			0U,
			1996959894U,
			3993919788U,
			2567524794U,
			124634137U,
			1886057615U,
			3915621685U,
			2657392035U,
			249268274U,
			2044508324U,
			3772115230U,
			2547177864U,
			162941995U,
			2125561021U,
			3887607047U,
			2428444049U,
			498536548U,
			1789927666U,
			4089016648U,
			2227061214U,
			450548861U,
			1843258603U,
			4107580753U,
			2211677639U,
			325883990U,
			1684777152U,
			4251122042U,
			2321926636U,
			335633487U,
			1661365465U,
			4195302755U,
			2366115317U,
			997073096U,
			1281953886U,
			3579855332U,
			2724688242U,
			1006888145U,
			1258607687U,
			3524101629U,
			2768942443U,
			901097722U,
			1119000684U,
			3686517206U,
			2898065728U,
			853044451U,
			1172266101U,
			3705015759U,
			2882616665U,
			651767980U,
			1373503546U,
			3369554304U,
			3218104598U,
			565507253U,
			1454621731U,
			3485111705U,
			3099436303U,
			671266974U,
			1594198024U,
			3322730930U,
			2970347812U,
			795835527U,
			1483230225U,
			3244367275U,
			3060149565U,
			1994146192U,
			31158534U,
			2563907772U,
			4023717930U,
			1907459465U,
			112637215U,
			2680153253U,
			3904427059U,
			2013776290U,
			251722036U,
			2517215374U,
			3775830040U,
			2137656763U,
			141376813U,
			2439277719U,
			3865271297U,
			1802195444U,
			476864866U,
			2238001368U,
			4066508878U,
			1812370925U,
			453092731U,
			2181625025U,
			4111451223U,
			1706088902U,
			314042704U,
			2344532202U,
			4240017532U,
			1658658271U,
			366619977U,
			2362670323U,
			4224994405U,
			1303535960U,
			984961486U,
			2747007092U,
			3569037538U,
			1256170817U,
			1037604311U,
			2765210733U,
			3554079995U,
			1131014506U,
			879679996U,
			2909243462U,
			3663771856U,
			1141124467U,
			855842277U,
			2852801631U,
			3708648649U,
			1342533948U,
			654459306U,
			3188396048U,
			3373015174U,
			1466479909U,
			544179635U,
			3110523913U,
			3462522015U,
			1591671054U,
			702138776U,
			2966460450U,
			3352799412U,
			1504918807U,
			783551873U,
			3082640443U,
			3233442989U,
			3988292384U,
			2596254646U,
			62317068U,
			1957810842U,
			3939845945U,
			2647816111U,
			81470997U,
			1943803523U,
			3814918930U,
			2489596804U,
			225274430U,
			2053790376U,
			3826175755U,
			2466906013U,
			167816743U,
			2097651377U,
			4027552580U,
			2265490386U,
			503444072U,
			1762050814U,
			4150417245U,
			2154129355U,
			426522225U,
			1852507879U,
			4275313526U,
			2312317920U,
			282753626U,
			1742555852U,
			4189708143U,
			2394877945U,
			397917763U,
			1622183637U,
			3604390888U,
			2714866558U,
			953729732U,
			1340076626U,
			3518719985U,
			2797360999U,
			1068828381U,
			1219638859U,
			3624741850U,
			2936675148U,
			906185462U,
			1090812512U,
			3747672003U,
			2825379669U,
			829329135U,
			1181335161U,
			3412177804U,
			3160834842U,
			628085408U,
			1382605366U,
			3423369109U,
			3138078467U,
			570562233U,
			1426400815U,
			3317316542U,
			2998733608U,
			733239954U,
			1555261956U,
			3268935591U,
			3050360625U,
			752459403U,
			1541320221U,
			2607071920U,
			3965973030U,
			1969922972U,
			40735498U,
			2617837225U,
			3943577151U,
			1913087877U,
			83908371U,
			2512341634U,
			3803740692U,
			2075208622U,
			213261112U,
			2463272603U,
			3855990285U,
			2094854071U,
			198958881U,
			2262029012U,
			4057260610U,
			1759359992U,
			534414190U,
			2176718541U,
			4139329115U,
			1873836001U,
			414664567U,
			2282248934U,
			4279200368U,
			1711684554U,
			285281116U,
			2405801727U,
			4167216745U,
			1634467795U,
			376229701U,
			2685067896U,
			3608007406U,
			1308918612U,
			956543938U,
			2808555105U,
			3495958263U,
			1231636301U,
			1047427035U,
			2932959818U,
			3654703836U,
			1088359270U,
			936918000U,
			2847714899U,
			3736837829U,
			1202900863U,
			817233897U,
			3183342108U,
			3401237130U,
			1404277552U,
			615818150U,
			3134207493U,
			3453421203U,
			1423857449U,
			601450431U,
			3009837614U,
			3294710456U,
			1567103746U,
			711928724U,
			3020668471U,
			3272380065U,
			1510334235U,
			755167117U
		};
	}

	// Token: 0x04001ABF RID: 6847
	private static readonly uint[] ᜀ;
}
