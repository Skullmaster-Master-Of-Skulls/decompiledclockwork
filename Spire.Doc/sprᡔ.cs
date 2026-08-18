using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x02000423 RID: 1059
internal class sprᡔ
{
	// Token: 0x06003AD8 RID: 15064 RVA: 0x0036D074 File Offset: 0x0036C074
	[CLSCompliant(false)]
	public static uint ᜀ(byte[] A_0, int A_1, int A_2, uint A_3)
	{
		int a_ = 4;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 1:
					goto IL_BA;
				case 2:
					goto IL_96;
				case 3:
					goto IL_3C;
				case 4:
				{
					if (num2 >= num3)
					{
						num = 1;
						continue;
					}
					uint num4 = sprᡔ.ᜀ[(int)((UIntPtr)((A_3 ^ (uint)A_0[num2]) & 255U))];
					A_3 = (A_3 >> 8 ^ num4);
					num2++;
					num = 5;
					continue;
				}
				case 5:
					goto IL_96;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				A_3 = ~A_3;
				num2 = A_1;
				num3 = A_1 + A_2;
				num = 2;
				continue;
				IL_96:
				num = 4;
			}
			IL_BA:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_D0;
			}
		}
		IL_3C:
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ࡩᥫ࡭ᙯ᝱ٳ", a_));
		IL_D0:
		if (false)
		{
		}
		return ~A_3;
	}

	// Token: 0x06003AD9 RID: 15065 RVA: 0x0036D15C File Offset: 0x0036C15C
	[CLSCompliant(false)]
	public static uint ᜀ(Stream A_0, int A_1)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
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
					uint num3;
					int num4;
					int num5;
					switch (num)
					{
					case 0:
						num = 10;
						continue;
					case 2:
						goto IL_8B;
					case 3:
						goto IL_1C7;
					case 4:
					{
						int num2;
						if (num2 == 0)
						{
							num = 11;
							continue;
						}
						byte[] array;
						num3 = sprᡔ.ᜀ(array, 0, num2, num3);
						num4 -= num2;
						num = 7;
						continue;
					}
					case 5:
					{
						if (num4 <= 0)
						{
							num = 9;
							continue;
						}
						int count = Math.Min(num4, 4096);
						byte[] array;
						int num2 = A_0.Read(array, 0, count);
						num = 4;
						continue;
					}
					case 6:
						if (num5 >= A_1)
						{
							num = 0;
							continue;
						}
						goto IL_194;
					case 7:
						goto IL_176;
					case 8:
						goto IL_CC;
					case 9:
						return num3;
					case 10:
					{
						if (A_1 < 0)
						{
							num = 3;
							continue;
						}
						if (true)
						{
						}
						int num6 = Math.Min(num5, 4096);
						byte[] array = new byte[num6];
						num = 8;
						continue;
					}
					case 11:
						goto IL_171;
					}
					if (A_0 == null)
					{
						num = 2;
						continue;
					}
					num3 = 0U;
					num4 = A_1;
					num5 = (int)(A_0.Length - A_0.Position);
					num = 6;
					continue;
				}
				}
				IL_176:
				num = 5;
				continue;
				IL_CC:
				goto IL_176;
			}
			IL_8B:
			throw new ArgumentNullException(ClipboardData.b("ᱮհŲၴᙶᑸ", a_));
			IL_171:
			throw new sprᥠ(ClipboardData.b("⩮ὰᝲ啴ᡶὸ孺๼୾ꦈ力ﮒ", a_));
			IL_194:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ͮᑰᵲቴͶᅸ", a_));
			IL_1C7:
			goto IL_194;
		}
		}
	}

	// Token: 0x06003ADA RID: 15066 RVA: 0x0036D334 File Offset: 0x0036C334
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
				int num2 = 7;
				for (;;)
				{
					uint num4;
					switch (num2)
					{
					case 0:
						goto IL_93;
					case 1:
					{
						uint num3 = num3 >> 1 ^ A_0;
						num2 = 0;
						continue;
					}
					case 2:
					{
						uint num3;
						if ((num3 & 1U) != 0U)
						{
							num2 = 1;
							continue;
						}
						if (true)
						{
						}
						num3 >>= 1;
						num2 = 11;
						continue;
					}
					case 3:
					{
						if (num >= 256U)
						{
							num2 = 10;
							continue;
						}
						uint num3 = num;
						num4 = 8U;
						num2 = 8;
						continue;
					}
					case 4:
						goto IL_110;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_110;
						default:
						{
							if (false)
							{
							}
							uint num3;
							array[(int)((UIntPtr)num)] = num3;
							num += 1U;
							num2 = 4;
							continue;
						}
						}
						break;
					case 6:
						goto IL_5F;
					case 7:
						goto IL_C4;
					case 8:
						goto IL_5F;
					case 9:
						if (num4 < 1U)
						{
							num2 = 5;
							continue;
						}
						num2 = 2;
						continue;
					case 10:
						return array;
					case 11:
						goto IL_93;
					}
					break;
					IL_5F:
					num2 = 9;
					continue;
					IL_93:
					num4 -= 1U;
					num2 = 6;
					continue;
					IL_C4:
					num2 = 3;
					continue;
					IL_110:
					goto IL_C4;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x06003ADC RID: 15068 RVA: 0x0036D4A8 File Offset: 0x0036C4A8
	// Note: this type is marked as 'beforefieldinit'.
	static sprᡔ()
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
		sprᡔ.ᜀ = new uint[]
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

	// Token: 0x04002B6C RID: 11116
	private static readonly uint[] ᜀ;
}
