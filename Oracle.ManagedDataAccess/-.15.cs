using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace \u0008
{
	// Token: 0x0200036E RID: 878
	internal static class \u0004
	{
		// Token: 0x06001E4F RID: 7759 RVA: 0x001267F4 File Offset: 0x001249F4
		private static bool \u0001(Assembly \u0002, Assembly \u0003)
		{
			byte[] publicKey = \u0002.GetName().GetPublicKey();
			byte[] publicKey2 = \u0003.GetName().GetPublicKey();
			if (publicKey2 == null != (publicKey == null))
			{
				return false;
			}
			if (publicKey2 != null)
			{
				for (int i = 0; i < publicKey2.Length; i++)
				{
					if (publicKey2[i] != publicKey[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00126844 File Offset: 0x00124A44
		private static ICryptoTransform \u0001(byte[] \u0002, byte[] \u0003, bool \u0004)
		{
			ICryptoTransform result;
			using (SymmetricAlgorithm symmetricAlgorithm = new RijndaelManaged())
			{
				result = (\u0004 ? symmetricAlgorithm.CreateDecryptor(\u0002, \u0003) : symmetricAlgorithm.CreateEncryptor(\u0002, \u0003));
			}
			return result;
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0012688C File Offset: 0x00124A8C
		private static ICryptoTransform \u0002(byte[] \u0002, byte[] \u0003, bool \u0004)
		{
			ICryptoTransform result;
			using (DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider())
			{
				result = (\u0004 ? descryptoServiceProvider.CreateDecryptor(\u0002, \u0003) : descryptoServiceProvider.CreateEncryptor(\u0002, \u0003));
			}
			return result;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x001268D4 File Offset: 0x00124AD4
		public static byte[] \u0001(byte[] \u0002)
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (callingAssembly != executingAssembly && !global::\u0008.\u0004.\u0001(executingAssembly, callingAssembly))
			{
				return null;
			}
			global::\u0008.\u0004.\u000F u000F = new global::\u0008.\u0004.\u000F(\u0002);
			byte[] array = new byte[0];
			int num = u000F.\u0002();
			if (num != 67324752)
			{
				int num2 = num >> 24;
				num -= num2 << 24;
				if (num == 8223355)
				{
					if (num2 == 1)
					{
						int num3 = u000F.\u0002();
						array = new byte[num3];
						int num5;
						for (int i = 0; i < num3; i += num5)
						{
							int num4 = u000F.\u0002();
							num5 = u000F.\u0002();
							byte[] array2 = new byte[num4];
							u000F.Read(array2, 0, array2.Length);
							global::\u0008.\u0004.\u0001 u = new global::\u0008.\u0004.\u0001(array2);
							u.\u0001(array, i, num5);
						}
					}
					if (num2 == 2)
					{
						byte[] u2 = new byte[]
						{
							84,
							130,
							34,
							208,
							177,
							150,
							228,
							115
						};
						byte[] u3 = new byte[]
						{
							0,
							203,
							241,
							172,
							5,
							53,
							71,
							6
						};
						using (ICryptoTransform cryptoTransform = global::\u0008.\u0004.\u0002(u2, u3, true))
						{
							byte[] u4 = cryptoTransform.TransformFinalBlock(\u0002, 4, \u0002.Length - 4);
							array = global::\u0008.\u0004.\u0001(u4);
						}
					}
					if (num2 != 3)
					{
						goto IL_26B;
					}
					byte[] u5 = new byte[]
					{
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1
					};
					byte[] u6 = new byte[]
					{
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2
					};
					using (ICryptoTransform cryptoTransform2 = global::\u0008.\u0004.\u0001(u5, u6, true))
					{
						byte[] u7 = cryptoTransform2.TransformFinalBlock(\u0002, 4, \u0002.Length - 4);
						array = global::\u0008.\u0004.\u0001(u7);
						goto IL_26B;
					}
				}
				throw new FormatException("Unknown Header");
			}
			short num6 = (short)u000F.\u0001();
			int num7 = u000F.\u0001();
			int num8 = u000F.\u0001();
			if (num != 67324752 || num6 != 20 || num7 != 0 || num8 != 8)
			{
				throw new FormatException("Wrong Header Signature");
			}
			u000F.\u0002();
			u000F.\u0002();
			u000F.\u0002();
			int num9 = u000F.\u0002();
			int num10 = u000F.\u0001();
			int num11 = u000F.\u0001();
			if (num10 > 0)
			{
				byte[] buffer = new byte[num10];
				u000F.Read(buffer, 0, num10);
			}
			if (num11 > 0)
			{
				byte[] buffer2 = new byte[num11];
				u000F.Read(buffer2, 0, num11);
			}
			byte[] array3 = new byte[u000F.Length - u000F.Position];
			u000F.Read(array3, 0, array3.Length);
			global::\u0008.\u0004.\u0001 u8 = new global::\u0008.\u0004.\u0001(array3);
			array = new byte[num9];
			u8.\u0001(array, 0, array.Length);
			IL_26B:
			u000F.Close();
			u000F = null;
			return array;
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x00126B74 File Offset: 0x00124D74
		public static byte[] \u0002(byte[] \u0002)
		{
			return global::\u0008.\u0004.\u0001(\u0002, 1, null, null);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x00126B80 File Offset: 0x00124D80
		public static byte[] \u0001(byte[] \u0002, byte[] \u0003, byte[] \u0004)
		{
			return global::\u0008.\u0004.\u0001(\u0002, 2, \u0003, \u0004);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00126B8C File Offset: 0x00124D8C
		public static byte[] \u0002(byte[] \u0002, byte[] \u0003, byte[] \u0004)
		{
			return global::\u0008.\u0004.\u0001(\u0002, 3, \u0003, \u0004);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x00126B98 File Offset: 0x00124D98
		private static byte[] \u0001(byte[] \u0002, int \u0003, byte[] \u0004, byte[] \u0005)
		{
			byte[] result;
			try
			{
				global::\u0008.\u0004.\u000F u000F = new global::\u0008.\u0004.\u000F();
				if (\u0003 == 0)
				{
					global::\u0008.\u0004.\u0006 u = new global::\u0008.\u0004.\u0006();
					DateTime now = DateTime.Now;
					long num = (long)((ulong)((now.Year - 1980 & 127) << 25 | now.Month << 21 | now.Day << 16 | now.Hour << 11 | now.Minute << 5 | (int)((uint)now.Second >> 1)));
					uint[] array = new uint[]
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
					uint maxValue = uint.MaxValue;
					uint num2 = maxValue;
					int num3 = 0;
					int num4 = \u0002.Length;
					while (--num4 >= 0)
					{
						num2 = (array[(int)((UIntPtr)((num2 ^ (uint)\u0002[num3++]) & 255U))] ^ num2 >> 8);
					}
					num2 ^= maxValue;
					u000F.\u0002(67324752);
					u000F.\u0001(20);
					u000F.\u0001(0);
					u000F.\u0001(8);
					u000F.\u0002((int)num);
					u000F.\u0002((int)num2);
					long position = u000F.Position;
					u000F.\u0002(0);
					u000F.\u0002(\u0002.Length);
					byte[] bytes = Encoding.UTF8.GetBytes("{data}");
					u000F.\u0001(bytes.Length);
					u000F.\u0001(0);
					u000F.Write(bytes, 0, bytes.Length);
					u.\u0001(\u0002);
					while (!u.IsNeedingInput)
					{
						byte[] array2 = new byte[512];
						int num5 = u.\u0001(array2);
						if (num5 <= 0)
						{
							break;
						}
						u000F.Write(array2, 0, num5);
					}
					u.\u0001();
					while (!u.IsFinished)
					{
						byte[] array3 = new byte[512];
						int num6 = u.\u0001(array3);
						if (num6 <= 0)
						{
							break;
						}
						u000F.Write(array3, 0, num6);
					}
					long num7 = u.TotalOut;
					u000F.\u0002(33639248);
					u000F.\u0001(20);
					u000F.\u0001(20);
					u000F.\u0001(0);
					u000F.\u0001(8);
					u000F.\u0002((int)num);
					u000F.\u0002((int)num2);
					u000F.\u0002((int)num7);
					u000F.\u0002(\u0002.Length);
					u000F.\u0001(bytes.Length);
					u000F.\u0001(0);
					u000F.\u0001(0);
					u000F.\u0001(0);
					u000F.\u0001(0);
					u000F.\u0002(0);
					u000F.\u0002(0);
					u000F.Write(bytes, 0, bytes.Length);
					u000F.\u0002(101010256);
					u000F.\u0001(0);
					u000F.\u0001(0);
					u000F.\u0001(1);
					u000F.\u0001(1);
					u000F.\u0002(46 + bytes.Length);
					u000F.\u0002((int)((long)(30 + bytes.Length) + num7));
					u000F.\u0001(0);
					u000F.Seek(position, SeekOrigin.Begin);
					u000F.\u0002((int)num7);
				}
				else if (\u0003 == 1)
				{
					u000F.\u0002(25000571);
					u000F.\u0002(\u0002.Length);
					byte[] array4;
					for (int i = 0; i < \u0002.Length; i += array4.Length)
					{
						array4 = new byte[Math.Min(2097151, \u0002.Length - i)];
						Buffer.BlockCopy(\u0002, i, array4, 0, array4.Length);
						long position2 = u000F.Position;
						u000F.\u0002(0);
						u000F.\u0002(array4.Length);
						global::\u0008.\u0004.\u0006 u2 = new global::\u0008.\u0004.\u0006();
						u2.\u0001(array4);
						while (!u2.IsNeedingInput)
						{
							byte[] array5 = new byte[512];
							int num8 = u2.\u0001(array5);
							if (num8 <= 0)
							{
								break;
							}
							u000F.Write(array5, 0, num8);
						}
						u2.\u0001();
						while (!u2.IsFinished)
						{
							byte[] array6 = new byte[512];
							int num9 = u2.\u0001(array6);
							if (num9 <= 0)
							{
								break;
							}
							u000F.Write(array6, 0, num9);
						}
						long position3 = u000F.Position;
						u000F.Position = position2;
						u000F.\u0002((int)u2.TotalOut);
						u000F.Position = position3;
					}
				}
				else
				{
					if (\u0003 == 2)
					{
						u000F.\u0002(41777787);
						byte[] array7 = global::\u0008.\u0004.\u0001(\u0002, 1, null, null);
						using (ICryptoTransform cryptoTransform = global::\u0008.\u0004.\u0002(\u0004, \u0005, false))
						{
							byte[] array8 = cryptoTransform.TransformFinalBlock(array7, 0, array7.Length);
							u000F.Write(array8, 0, array8.Length);
							goto IL_44F;
						}
					}
					if (\u0003 == 3)
					{
						u000F.\u0002(58555003);
						byte[] array9 = global::\u0008.\u0004.\u0001(\u0002, 1, null, null);
						using (ICryptoTransform cryptoTransform2 = global::\u0008.\u0004.\u0001(\u0004, \u0005, false))
						{
							byte[] array10 = cryptoTransform2.TransformFinalBlock(array9, 0, array9.Length);
							u000F.Write(array10, 0, array10.Length);
						}
					}
				}
				IL_44F:
				u000F.Flush();
				u000F.Close();
				result = u000F.ToArray();
			}
			catch (Exception ex)
			{
				global::\u0008.\u0004.\u0001 = "ERR 2003: " + ex.Message;
				throw;
			}
			return result;
		}

		// Token: 0x040020A5 RID: 8357
		public static string \u0001;

		// Token: 0x0200036F RID: 879
		internal class \u0001
		{
			// Token: 0x06001E57 RID: 7767 RVA: 0x00127074 File Offset: 0x00125274
			public \u0001(byte[] bytes)
			{
				this.\u001D = new global::\u0008.\u0004.\u0002();
				this.\u001E = new global::\u0008.\u0004.\u0003();
				this.\u0017 = 2;
				this.\u001D.\u0001(bytes, 0, bytes.Length);
			}

			// Token: 0x06001E58 RID: 7768 RVA: 0x001270AC File Offset: 0x001252AC
			private bool \u0001()
			{
				int i = this.\u001E.\u0001();
				while (i >= 258)
				{
					int num;
					switch (this.\u0017)
					{
					case 7:
						while (((num = this.\u007F.\u0001(this.\u001D)) & -256) == 0)
						{
							this.\u001E.\u0001(num);
							if (--i < 258)
							{
								return true;
							}
						}
						if (num >= 257)
						{
							this.\u0019 = global::\u0008.\u0004.\u0001.\u0013[num - 257];
							this.\u0018 = global::\u0008.\u0004.\u0001.\u0014[num - 257];
							goto IL_B7;
						}
						if (num < 0)
						{
							return false;
						}
						this.\u0080 = null;
						this.\u007F = null;
						this.\u0017 = 2;
						return true;
					case 8:
						goto IL_B7;
					case 9:
						goto IL_106;
					case 10:
						break;
					default:
						continue;
					}
					IL_138:
					if (this.\u0018 > 0)
					{
						this.\u0017 = 10;
						int num2 = this.\u001D.\u0001(this.\u0018);
						if (num2 < 0)
						{
							return false;
						}
						this.\u001D.\u0001(this.\u0018);
						this.\u001A += num2;
					}
					this.\u001E.\u0001(this.\u0019, this.\u001A);
					i -= this.\u0019;
					this.\u0017 = 7;
					continue;
					IL_106:
					num = this.\u0080.\u0001(this.\u001D);
					if (num < 0)
					{
						return false;
					}
					this.\u001A = global::\u0008.\u0004.\u0001.\u0015[num];
					this.\u0018 = global::\u0008.\u0004.\u0001.\u0016[num];
					goto IL_138;
					IL_B7:
					if (this.\u0018 > 0)
					{
						this.\u0017 = 8;
						int num3 = this.\u001D.\u0001(this.\u0018);
						if (num3 < 0)
						{
							return false;
						}
						this.\u001D.\u0001(this.\u0018);
						this.\u0019 += num3;
					}
					this.\u0017 = 9;
					goto IL_106;
				}
				return true;
			}

			// Token: 0x06001E59 RID: 7769 RVA: 0x0012726C File Offset: 0x0012546C
			private bool \u0002()
			{
				switch (this.\u0017)
				{
				case 2:
				{
					if (this.\u001C)
					{
						this.\u0017 = 12;
						return false;
					}
					int num = this.\u001D.\u0001(3);
					if (num < 0)
					{
						return false;
					}
					this.\u001D.\u0001(3);
					if ((num & 1) != 0)
					{
						this.\u001C = true;
					}
					switch (num >> 1)
					{
					case 0:
						this.\u001D.\u0001();
						this.\u0017 = 3;
						break;
					case 1:
						this.\u007F = global::\u0008.\u0004.\u0004.\u0003;
						this.\u0080 = global::\u0008.\u0004.\u0004.\u0004;
						this.\u0017 = 7;
						break;
					case 2:
						this.\u001F = new global::\u0008.\u0004.\u0005();
						this.\u0017 = 6;
						break;
					}
					return true;
				}
				case 3:
					if ((this.\u001B = this.\u001D.\u0001(16)) < 0)
					{
						return false;
					}
					this.\u001D.\u0001(16);
					this.\u0017 = 4;
					break;
				case 4:
					break;
				case 5:
					goto IL_137;
				case 6:
					if (!this.\u001F.\u0001(this.\u001D))
					{
						return false;
					}
					this.\u007F = this.\u001F.\u0001();
					this.\u0080 = this.\u001F.\u0002();
					this.\u0017 = 7;
					goto IL_1BB;
				case 7:
				case 8:
				case 9:
				case 10:
					goto IL_1BB;
				case 11:
					return false;
				case 12:
					return false;
				default:
					return false;
				}
				int num2 = this.\u001D.\u0001(16);
				if (num2 < 0)
				{
					return false;
				}
				this.\u001D.\u0001(16);
				this.\u0017 = 5;
				IL_137:
				int num3 = this.\u001E.\u0001(this.\u001D, this.\u001B);
				this.\u001B -= num3;
				if (this.\u001B == 0)
				{
					this.\u0017 = 2;
					return true;
				}
				return !this.\u001D.IsNeedingInput;
				IL_1BB:
				return this.\u0001();
			}

			// Token: 0x06001E5A RID: 7770 RVA: 0x00127440 File Offset: 0x00125640
			public int \u0001(byte[] \u0002, int \u0003, int \u0004)
			{
				int num = 0;
				for (;;)
				{
					if (this.\u0017 != 11)
					{
						int num2 = this.\u001E.\u0001(\u0002, \u0003, \u0004);
						\u0003 += num2;
						num += num2;
						\u0004 -= num2;
						if (\u0004 == 0)
						{
							break;
						}
					}
					if (!this.\u0002() && (this.\u001E.\u0002() <= 0 || this.\u0017 == 11))
					{
						return num;
					}
				}
				return num;
			}

			// Token: 0x040020A6 RID: 8358
			private const int \u0001 = 0;

			// Token: 0x040020A7 RID: 8359
			private const int \u0002 = 1;

			// Token: 0x040020A8 RID: 8360
			private const int \u0003 = 2;

			// Token: 0x040020A9 RID: 8361
			private const int \u0004 = 3;

			// Token: 0x040020AA RID: 8362
			private const int \u0005 = 4;

			// Token: 0x040020AB RID: 8363
			private const int \u0006 = 5;

			// Token: 0x040020AC RID: 8364
			private const int \u0007 = 6;

			// Token: 0x040020AD RID: 8365
			private const int \u0008 = 7;

			// Token: 0x040020AE RID: 8366
			private const int \u000E = 8;

			// Token: 0x040020AF RID: 8367
			private const int \u000F = 9;

			// Token: 0x040020B0 RID: 8368
			private const int \u0010 = 10;

			// Token: 0x040020B1 RID: 8369
			private const int \u0011 = 11;

			// Token: 0x040020B2 RID: 8370
			private const int \u0012 = 12;

			// Token: 0x040020B3 RID: 8371
			private static readonly int[] \u0013 = new int[]
			{
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				13,
				15,
				17,
				19,
				23,
				27,
				31,
				35,
				43,
				51,
				59,
				67,
				83,
				99,
				115,
				131,
				163,
				195,
				227,
				258
			};

			// Token: 0x040020B4 RID: 8372
			private static readonly int[] \u0014 = new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1,
				2,
				2,
				2,
				2,
				3,
				3,
				3,
				3,
				4,
				4,
				4,
				4,
				5,
				5,
				5,
				5,
				0
			};

			// Token: 0x040020B5 RID: 8373
			private static readonly int[] \u0015 = new int[]
			{
				1,
				2,
				3,
				4,
				5,
				7,
				9,
				13,
				17,
				25,
				33,
				49,
				65,
				97,
				129,
				193,
				257,
				385,
				513,
				769,
				1025,
				1537,
				2049,
				3073,
				4097,
				6145,
				8193,
				12289,
				16385,
				24577
			};

			// Token: 0x040020B6 RID: 8374
			private static readonly int[] \u0016 = new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				2,
				2,
				3,
				3,
				4,
				4,
				5,
				5,
				6,
				6,
				7,
				7,
				8,
				8,
				9,
				9,
				10,
				10,
				11,
				11,
				12,
				12,
				13,
				13
			};

			// Token: 0x040020B7 RID: 8375
			private int \u0017;

			// Token: 0x040020B8 RID: 8376
			private int \u0018;

			// Token: 0x040020B9 RID: 8377
			private int \u0019;

			// Token: 0x040020BA RID: 8378
			private int \u001A;

			// Token: 0x040020BB RID: 8379
			private int \u001B;

			// Token: 0x040020BC RID: 8380
			private bool \u001C;

			// Token: 0x040020BD RID: 8381
			private global::\u0008.\u0004.\u0002 \u001D;

			// Token: 0x040020BE RID: 8382
			private global::\u0008.\u0004.\u0003 \u001E;

			// Token: 0x040020BF RID: 8383
			private global::\u0008.\u0004.\u0005 \u001F;

			// Token: 0x040020C0 RID: 8384
			private global::\u0008.\u0004.\u0004 \u007F;

			// Token: 0x040020C1 RID: 8385
			private global::\u0008.\u0004.\u0004 \u0080;
		}

		// Token: 0x02000370 RID: 880
		internal class \u0002
		{
			// Token: 0x06001E5C RID: 7772 RVA: 0x00127508 File Offset: 0x00125708
			public int \u0001(int \u0002)
			{
				if (this.\u0005 < \u0002)
				{
					if (this.\u0002 == this.\u0003)
					{
						return -1;
					}
					this.\u0004 |= (uint)((uint)((int)(this.\u0001[this.\u0002++] & byte.MaxValue) | (int)(this.\u0001[this.\u0002++] & byte.MaxValue) << 8) << this.\u0005);
					this.\u0005 += 16;
				}
				return (int)((ulong)this.\u0004 & (ulong)((long)((1 << \u0002) - 1)));
			}

			// Token: 0x06001E5D RID: 7773 RVA: 0x001275A8 File Offset: 0x001257A8
			public void \u0001(int \u0002)
			{
				this.\u0004 >>= \u0002;
				this.\u0005 -= \u0002;
			}

			// Token: 0x170004C8 RID: 1224
			// (get) Token: 0x06001E5E RID: 7774 RVA: 0x001275CC File Offset: 0x001257CC
			public int AvailableBits
			{
				get
				{
					return this.\u0005;
				}
			}

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x06001E5F RID: 7775 RVA: 0x001275D4 File Offset: 0x001257D4
			public int AvailableBytes
			{
				get
				{
					return this.\u0003 - this.\u0002 + (this.\u0005 >> 3);
				}
			}

			// Token: 0x06001E60 RID: 7776 RVA: 0x001275EC File Offset: 0x001257EC
			public void \u0001()
			{
				this.\u0004 >>= (this.\u0005 & 7);
				this.\u0005 &= -8;
			}

			// Token: 0x170004CA RID: 1226
			// (get) Token: 0x06001E61 RID: 7777 RVA: 0x00127618 File Offset: 0x00125818
			public bool IsNeedingInput
			{
				get
				{
					return this.\u0002 == this.\u0003;
				}
			}

			// Token: 0x06001E62 RID: 7778 RVA: 0x00127628 File Offset: 0x00125828
			public int \u0001(byte[] \u0002, int \u0003, int \u0004)
			{
				int num = 0;
				while (this.\u0005 > 0 && \u0004 > 0)
				{
					\u0002[\u0003++] = (byte)this.\u0004;
					this.\u0004 >>= 8;
					this.\u0005 -= 8;
					\u0004--;
					num++;
				}
				if (\u0004 == 0)
				{
					return num;
				}
				int num2 = this.\u0003 - this.\u0002;
				if (\u0004 > num2)
				{
					\u0004 = num2;
				}
				Array.Copy(this.\u0001, this.\u0002, \u0002, \u0003, \u0004);
				this.\u0002 += \u0004;
				if ((this.\u0002 - this.\u0003 & 1) != 0)
				{
					this.\u0004 = (uint)(this.\u0001[this.\u0002++] & byte.MaxValue);
					this.\u0005 = 8;
				}
				return num + \u0004;
			}

			// Token: 0x06001E64 RID: 7780 RVA: 0x00127700 File Offset: 0x00125900
			public void \u0002()
			{
				this.\u0004 = (uint)(this.\u0002 = (this.\u0003 = (this.\u0005 = 0)));
			}

			// Token: 0x06001E65 RID: 7781 RVA: 0x00127730 File Offset: 0x00125930
			public void \u0001(byte[] \u0002, int \u0003, int \u0004)
			{
				if (this.\u0002 < this.\u0003)
				{
					throw new InvalidOperationException();
				}
				int num = \u0003 + \u0004;
				if (0 > \u0003 || \u0003 > num || num > \u0002.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				if ((\u0004 & 1) != 0)
				{
					this.\u0004 |= (uint)((uint)(\u0002[\u0003++] & byte.MaxValue) << this.\u0005);
					this.\u0005 += 8;
				}
				this.\u0001 = \u0002;
				this.\u0002 = \u0003;
				this.\u0003 = num;
			}

			// Token: 0x040020C2 RID: 8386
			private byte[] \u0001;

			// Token: 0x040020C3 RID: 8387
			private int \u0002;

			// Token: 0x040020C4 RID: 8388
			private int \u0003;

			// Token: 0x040020C5 RID: 8389
			private uint \u0004;

			// Token: 0x040020C6 RID: 8390
			private int \u0005;
		}

		// Token: 0x02000371 RID: 881
		internal class \u0003
		{
			// Token: 0x06001E66 RID: 7782 RVA: 0x001277B8 File Offset: 0x001259B8
			public void \u0001(int \u0002)
			{
				if (this.\u0005++ == 32768)
				{
					throw new InvalidOperationException();
				}
				this.\u0003[this.\u0004++] = (byte)\u0002;
				this.\u0004 &= 32767;
			}

			// Token: 0x06001E67 RID: 7783 RVA: 0x00127810 File Offset: 0x00125A10
			private void \u0001(int \u0002, int \u0003, int \u0004)
			{
				while (\u0003-- > 0)
				{
					this.\u0003[this.\u0004++] = this.\u0003[\u0002++];
					this.\u0004 &= 32767;
					\u0002 &= 32767;
				}
			}

			// Token: 0x06001E68 RID: 7784 RVA: 0x00127868 File Offset: 0x00125A68
			public void \u0001(int \u0002, int \u0003)
			{
				if ((this.\u0005 += \u0002) > 32768)
				{
					throw new InvalidOperationException();
				}
				int num = this.\u0004 - \u0003 & 32767;
				int num2 = 32768 - \u0002;
				if (num > num2 || this.\u0004 >= num2)
				{
					this.\u0001(num, \u0002, \u0003);
					return;
				}
				if (\u0002 <= \u0003)
				{
					Array.Copy(this.\u0003, num, this.\u0003, this.\u0004, \u0002);
					this.\u0004 += \u0002;
					return;
				}
				while (\u0002-- > 0)
				{
					this.\u0003[this.\u0004++] = this.\u0003[num++];
				}
			}

			// Token: 0x06001E69 RID: 7785 RVA: 0x0012791C File Offset: 0x00125B1C
			public int \u0001(global::\u0008.\u0004.\u0002 \u0002, int \u0003)
			{
				\u0003 = Math.Min(Math.Min(\u0003, 32768 - this.\u0005), \u0002.AvailableBytes);
				int num = 32768 - this.\u0004;
				int num2;
				if (\u0003 > num)
				{
					num2 = \u0002.\u0001(this.\u0003, this.\u0004, num);
					if (num2 == num)
					{
						num2 += \u0002.\u0001(this.\u0003, 0, \u0003 - num);
					}
				}
				else
				{
					num2 = \u0002.\u0001(this.\u0003, this.\u0004, \u0003);
				}
				this.\u0004 = (this.\u0004 + num2 & 32767);
				this.\u0005 += num2;
				return num2;
			}

			// Token: 0x06001E6A RID: 7786 RVA: 0x001279C0 File Offset: 0x00125BC0
			public void \u0001(byte[] \u0002, int \u0003, int \u0004)
			{
				if (this.\u0005 > 0)
				{
					throw new InvalidOperationException();
				}
				if (\u0004 > 32768)
				{
					\u0003 += \u0004 - 32768;
					\u0004 = 32768;
				}
				Array.Copy(\u0002, \u0003, this.\u0003, 0, \u0004);
				this.\u0004 = (\u0004 & 32767);
			}

			// Token: 0x06001E6B RID: 7787 RVA: 0x00127A14 File Offset: 0x00125C14
			public int \u0001()
			{
				return 32768 - this.\u0005;
			}

			// Token: 0x06001E6C RID: 7788 RVA: 0x00127A24 File Offset: 0x00125C24
			public int \u0002()
			{
				return this.\u0005;
			}

			// Token: 0x06001E6D RID: 7789 RVA: 0x00127A2C File Offset: 0x00125C2C
			public int \u0001(byte[] \u0002, int \u0003, int \u0004)
			{
				int num = this.\u0004;
				if (\u0004 > this.\u0005)
				{
					\u0004 = this.\u0005;
				}
				else
				{
					num = (this.\u0004 - this.\u0005 + \u0004 & 32767);
				}
				int num2 = \u0004;
				int num3 = \u0004 - num;
				if (num3 > 0)
				{
					Array.Copy(this.\u0003, 32768 - num3, \u0002, \u0003, num3);
					\u0003 += num3;
					\u0004 = num;
				}
				Array.Copy(this.\u0003, num - \u0004, \u0002, \u0003, \u0004);
				this.\u0005 -= num2;
				if (this.\u0005 < 0)
				{
					throw new InvalidOperationException();
				}
				return num2;
			}

			// Token: 0x06001E6E RID: 7790 RVA: 0x00127AC0 File Offset: 0x00125CC0
			public void \u0001()
			{
				this.\u0005 = (this.\u0004 = 0);
			}

			// Token: 0x040020C7 RID: 8391
			private const int \u0001 = 32768;

			// Token: 0x040020C8 RID: 8392
			private const int \u0002 = 32767;

			// Token: 0x040020C9 RID: 8393
			private byte[] \u0003 = new byte[32768];

			// Token: 0x040020CA RID: 8394
			private int \u0004;

			// Token: 0x040020CB RID: 8395
			private int \u0005;
		}

		// Token: 0x02000372 RID: 882
		internal class \u0004
		{
			// Token: 0x06001E70 RID: 7792 RVA: 0x00127AF8 File Offset: 0x00125CF8
			static \u0004()
			{
				byte[] array = new byte[288];
				int i = 0;
				while (i < 144)
				{
					array[i++] = 8;
				}
				while (i < 256)
				{
					array[i++] = 9;
				}
				while (i < 280)
				{
					array[i++] = 7;
				}
				while (i < 288)
				{
					array[i++] = 8;
				}
				global::\u0008.\u0004.\u0004.\u0003 = new global::\u0008.\u0004.\u0004(array);
				array = new byte[32];
				i = 0;
				while (i < 32)
				{
					array[i++] = 5;
				}
				global::\u0008.\u0004.\u0004.\u0004 = new global::\u0008.\u0004.\u0004(array);
			}

			// Token: 0x06001E71 RID: 7793 RVA: 0x00127B8C File Offset: 0x00125D8C
			public \u0004(byte[] codeLengths)
			{
				this.\u0001(codeLengths);
			}

			// Token: 0x06001E72 RID: 7794 RVA: 0x00127B9C File Offset: 0x00125D9C
			private void \u0001(byte[] \u0002)
			{
				int[] array = new int[16];
				int[] array2 = new int[16];
				foreach (int num in \u0002)
				{
					if (num > 0)
					{
						array[num]++;
					}
				}
				int num2 = 0;
				int num3 = 512;
				for (int j = 1; j <= 15; j++)
				{
					array2[j] = num2;
					num2 += array[j] << 16 - j;
					if (j >= 10)
					{
						int num4 = array2[j] & 130944;
						int num5 = num2 & 130944;
						num3 += num5 - num4 >> 16 - j;
					}
				}
				this.\u0002 = new short[num3];
				int num6 = 512;
				for (int k = 15; k >= 10; k--)
				{
					int num7 = num2 & 130944;
					num2 -= array[k] << 16 - k;
					int num8 = num2 & 130944;
					for (int l = num8; l < num7; l += 128)
					{
						this.\u0002[(int)global::\u0008.\u0004.\u0007.\u0001(l)] = (short)(-num6 << 4 | k);
						num6 += 1 << k - 9;
					}
				}
				for (int m = 0; m < \u0002.Length; m++)
				{
					int num9 = (int)\u0002[m];
					if (num9 != 0)
					{
						num2 = array2[num9];
						int num10 = (int)global::\u0008.\u0004.\u0007.\u0001(num2);
						if (num9 <= 9)
						{
							do
							{
								this.\u0002[num10] = (short)(m << 4 | num9);
								num10 += 1 << num9;
							}
							while (num10 < 512);
						}
						else
						{
							int num11 = (int)this.\u0002[num10 & 511];
							int num12 = 1 << (num11 & 15);
							num11 = -(num11 >> 4);
							do
							{
								this.\u0002[num11 | num10 >> 9] = (short)(m << 4 | num9);
								num10 += 1 << num9;
							}
							while (num10 < num12);
						}
						array2[num9] = num2 + (1 << 16 - num9);
					}
				}
			}

			// Token: 0x06001E73 RID: 7795 RVA: 0x00127D90 File Offset: 0x00125F90
			public int \u0001(global::\u0008.\u0004.\u0002 \u0002)
			{
				int num;
				if ((num = \u0002.\u0001(9)) >= 0)
				{
					int num2;
					if ((num2 = (int)this.\u0002[num]) >= 0)
					{
						\u0002.\u0001(num2 & 15);
						return num2 >> 4;
					}
					int num3 = -(num2 >> 4);
					int u = num2 & 15;
					if ((num = \u0002.\u0001(u)) >= 0)
					{
						num2 = (int)this.\u0002[num3 | num >> 9];
						\u0002.\u0001(num2 & 15);
						return num2 >> 4;
					}
					int num4 = \u0002.AvailableBits;
					num = \u0002.\u0001(num4);
					num2 = (int)this.\u0002[num3 | num >> 9];
					if ((num2 & 15) <= num4)
					{
						\u0002.\u0001(num2 & 15);
						return num2 >> 4;
					}
					return -1;
				}
				else
				{
					int num5 = \u0002.AvailableBits;
					num = \u0002.\u0001(num5);
					int num2 = (int)this.\u0002[num];
					if (num2 >= 0 && (num2 & 15) <= num5)
					{
						\u0002.\u0001(num2 & 15);
						return num2 >> 4;
					}
					return -1;
				}
			}

			// Token: 0x040020CC RID: 8396
			private const int \u0001 = 15;

			// Token: 0x040020CD RID: 8397
			private short[] \u0002;

			// Token: 0x040020CE RID: 8398
			public static readonly global::\u0008.\u0004.\u0004 \u0003;

			// Token: 0x040020CF RID: 8399
			public static readonly global::\u0008.\u0004.\u0004 \u0004;
		}

		// Token: 0x02000373 RID: 883
		internal class \u0005
		{
			// Token: 0x06001E75 RID: 7797 RVA: 0x00127E70 File Offset: 0x00126070
			public bool \u0001(global::\u0008.\u0004.\u0002 \u0002)
			{
				for (;;)
				{
					switch (this.\u0011)
					{
					case 0:
						this.\u0012 = \u0002.\u0001(5);
						if (this.\u0012 < 0)
						{
							return false;
						}
						this.\u0012 += 257;
						\u0002.\u0001(5);
						this.\u0011 = 1;
						goto IL_61;
					case 1:
						goto IL_61;
					case 2:
						goto IL_B9;
					case 3:
						break;
					case 4:
						goto IL_1A8;
					case 5:
						goto IL_1DE;
					default:
						continue;
					}
					IL_13B:
					while (this.\u0018 < this.\u0014)
					{
						int num = \u0002.\u0001(3);
						if (num < 0)
						{
							return false;
						}
						\u0002.\u0001(3);
						this.\u000E[global::\u0008.\u0004.\u0005.\u0019[this.\u0018]] = (byte)num;
						this.\u0018++;
					}
					this.\u0010 = new global::\u0008.\u0004.\u0004(this.\u000E);
					this.\u000E = null;
					this.\u0018 = 0;
					this.\u0011 = 4;
					IL_1A8:
					int num2;
					while (((num2 = this.\u0010.\u0001(\u0002)) & -16) == 0)
					{
						this.\u000F[this.\u0018++] = (this.\u0017 = (byte)num2);
						if (this.\u0018 == this.\u0015)
						{
							return true;
						}
					}
					if (num2 < 0)
					{
						return false;
					}
					if (num2 >= 17)
					{
						this.\u0017 = 0;
					}
					this.\u0016 = num2 - 16;
					this.\u0011 = 5;
					IL_1DE:
					int u = global::\u0008.\u0004.\u0005.\u0008[this.\u0016];
					int num3 = \u0002.\u0001(u);
					if (num3 < 0)
					{
						return false;
					}
					\u0002.\u0001(u);
					num3 += global::\u0008.\u0004.\u0005.\u0007[this.\u0016];
					while (num3-- > 0)
					{
						this.\u000F[this.\u0018++] = this.\u0017;
					}
					if (this.\u0018 == this.\u0015)
					{
						return true;
					}
					this.\u0011 = 4;
					continue;
					IL_B9:
					this.\u0014 = \u0002.\u0001(4);
					if (this.\u0014 < 0)
					{
						return false;
					}
					this.\u0014 += 4;
					\u0002.\u0001(4);
					this.\u000E = new byte[19];
					this.\u0018 = 0;
					this.\u0011 = 3;
					goto IL_13B;
					IL_61:
					this.\u0013 = \u0002.\u0001(5);
					if (this.\u0013 < 0)
					{
						return false;
					}
					this.\u0013++;
					\u0002.\u0001(5);
					this.\u0015 = this.\u0012 + this.\u0013;
					this.\u000F = new byte[this.\u0015];
					this.\u0011 = 2;
					goto IL_B9;
				}
				return false;
			}

			// Token: 0x06001E76 RID: 7798 RVA: 0x001280D4 File Offset: 0x001262D4
			public global::\u0008.\u0004.\u0004 \u0001()
			{
				byte[] array = new byte[this.\u0012];
				Array.Copy(this.\u000F, 0, array, 0, this.\u0012);
				return new global::\u0008.\u0004.\u0004(array);
			}

			// Token: 0x06001E77 RID: 7799 RVA: 0x00128108 File Offset: 0x00126308
			public global::\u0008.\u0004.\u0004 \u0002()
			{
				byte[] array = new byte[this.\u0013];
				Array.Copy(this.\u000F, this.\u0012, array, 0, this.\u0013);
				return new global::\u0008.\u0004.\u0004(array);
			}

			// Token: 0x040020D0 RID: 8400
			private const int \u0001 = 0;

			// Token: 0x040020D1 RID: 8401
			private const int \u0002 = 1;

			// Token: 0x040020D2 RID: 8402
			private const int \u0003 = 2;

			// Token: 0x040020D3 RID: 8403
			private const int \u0004 = 3;

			// Token: 0x040020D4 RID: 8404
			private const int \u0005 = 4;

			// Token: 0x040020D5 RID: 8405
			private const int \u0006 = 5;

			// Token: 0x040020D6 RID: 8406
			private static readonly int[] \u0007 = new int[]
			{
				3,
				3,
				11
			};

			// Token: 0x040020D7 RID: 8407
			private static readonly int[] \u0008 = new int[]
			{
				2,
				3,
				7
			};

			// Token: 0x040020D8 RID: 8408
			private byte[] \u000E;

			// Token: 0x040020D9 RID: 8409
			private byte[] \u000F;

			// Token: 0x040020DA RID: 8410
			private global::\u0008.\u0004.\u0004 \u0010;

			// Token: 0x040020DB RID: 8411
			private int \u0011;

			// Token: 0x040020DC RID: 8412
			private int \u0012;

			// Token: 0x040020DD RID: 8413
			private int \u0013;

			// Token: 0x040020DE RID: 8414
			private int \u0014;

			// Token: 0x040020DF RID: 8415
			private int \u0015;

			// Token: 0x040020E0 RID: 8416
			private int \u0016;

			// Token: 0x040020E1 RID: 8417
			private byte \u0017;

			// Token: 0x040020E2 RID: 8418
			private int \u0018;

			// Token: 0x040020E3 RID: 8419
			private static readonly int[] \u0019 = new int[]
			{
				16,
				17,
				18,
				0,
				8,
				7,
				9,
				6,
				10,
				5,
				11,
				4,
				12,
				3,
				13,
				2,
				14,
				1,
				15
			};
		}

		// Token: 0x02000374 RID: 884
		internal class \u0006
		{
			// Token: 0x06001E79 RID: 7801 RVA: 0x00128190 File Offset: 0x00126390
			public \u0006()
			{
				this.\u000E = new global::\u0008.\u0004.\u000E();
				this.\u000F = new global::\u0008.\u0004.\u0008(this.\u000E);
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x06001E7A RID: 7802 RVA: 0x001281BC File Offset: 0x001263BC
			public long TotalOut
			{
				get
				{
					return this.\u0008;
				}
			}

			// Token: 0x06001E7B RID: 7803 RVA: 0x001281C4 File Offset: 0x001263C4
			public void \u0001()
			{
				this.\u0007 |= 12;
			}

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x06001E7C RID: 7804 RVA: 0x001281D8 File Offset: 0x001263D8
			public bool IsFinished
			{
				get
				{
					return this.\u0007 == 30 && this.\u000E.IsFlushed;
				}
			}

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x06001E7D RID: 7805 RVA: 0x001281F4 File Offset: 0x001263F4
			public bool IsNeedingInput
			{
				get
				{
					return this.\u000F.\u0001();
				}
			}

			// Token: 0x06001E7E RID: 7806 RVA: 0x00128204 File Offset: 0x00126404
			public void \u0001(byte[] \u0002)
			{
				this.\u000F.\u0001(\u0002);
			}

			// Token: 0x06001E7F RID: 7807 RVA: 0x00128214 File Offset: 0x00126414
			public int \u0001(byte[] \u0002)
			{
				int num = 0;
				int num2 = \u0002.Length;
				int num3 = num2;
				for (;;)
				{
					int num4 = this.\u000E.\u0001(\u0002, num, num2);
					num += num4;
					this.\u0008 += (long)num4;
					num2 -= num4;
					if (num2 == 0 || this.\u0007 == 30)
					{
						goto IL_E2;
					}
					if (!this.\u000F.\u0002((this.\u0007 & 4) != 0, (this.\u0007 & 8) != 0))
					{
						if (this.\u0007 == 16)
						{
							break;
						}
						if (this.\u0007 == 20)
						{
							for (int i = 8 + (-this.\u000E.BitCount & 7); i > 0; i -= 10)
							{
								this.\u000E.\u0001(2, 10);
							}
							this.\u0007 = 16;
						}
						else if (this.\u0007 == 28)
						{
							this.\u000E.\u0001();
							this.\u0007 = 30;
						}
					}
				}
				return num3 - num2;
				IL_E2:
				return num3 - num2;
			}

			// Token: 0x040020E4 RID: 8420
			private const int \u0001 = 4;

			// Token: 0x040020E5 RID: 8421
			private const int \u0002 = 8;

			// Token: 0x040020E6 RID: 8422
			private const int \u0003 = 16;

			// Token: 0x040020E7 RID: 8423
			private const int \u0004 = 20;

			// Token: 0x040020E8 RID: 8424
			private const int \u0005 = 28;

			// Token: 0x040020E9 RID: 8425
			private const int \u0006 = 30;

			// Token: 0x040020EA RID: 8426
			private int \u0007 = 16;

			// Token: 0x040020EB RID: 8427
			private long \u0008;

			// Token: 0x040020EC RID: 8428
			private global::\u0008.\u0004.\u000E \u000E;

			// Token: 0x040020ED RID: 8429
			private global::\u0008.\u0004.\u0008 \u000F;
		}

		// Token: 0x02000375 RID: 885
		internal class \u0007
		{
			// Token: 0x06001E80 RID: 7808 RVA: 0x00128308 File Offset: 0x00126508
			public static short \u0001(int \u0002)
			{
				return (short)((int)global::\u0008.\u0004.\u0007.\u000F[\u0002 & 15] << 12 | (int)global::\u0008.\u0004.\u0007.\u000F[\u0002 >> 4 & 15] << 8 | (int)global::\u0008.\u0004.\u0007.\u000F[\u0002 >> 8 & 15] << 4 | (int)global::\u0008.\u0004.\u0007.\u000F[\u0002 >> 12]);
			}

			// Token: 0x06001E81 RID: 7809 RVA: 0x00128344 File Offset: 0x00126544
			static \u0007()
			{
				int i = 0;
				while (i < 144)
				{
					global::\u0008.\u0004.\u0007.\u0018[i] = global::\u0008.\u0004.\u0007.\u0001(48 + i << 8);
					global::\u0008.\u0004.\u0007.\u0019[i++] = 8;
				}
				while (i < 256)
				{
					global::\u0008.\u0004.\u0007.\u0018[i] = global::\u0008.\u0004.\u0007.\u0001(256 + i << 7);
					global::\u0008.\u0004.\u0007.\u0019[i++] = 9;
				}
				while (i < 280)
				{
					global::\u0008.\u0004.\u0007.\u0018[i] = global::\u0008.\u0004.\u0007.\u0001(-256 + i << 9);
					global::\u0008.\u0004.\u0007.\u0019[i++] = 7;
				}
				while (i < 286)
				{
					global::\u0008.\u0004.\u0007.\u0018[i] = global::\u0008.\u0004.\u0007.\u0001(-88 + i << 8);
					global::\u0008.\u0004.\u0007.\u0019[i++] = 8;
				}
				global::\u0008.\u0004.\u0007.\u001A = new short[30];
				global::\u0008.\u0004.\u0007.\u001B = new byte[30];
				for (i = 0; i < 30; i++)
				{
					global::\u0008.\u0004.\u0007.\u001A[i] = global::\u0008.\u0004.\u0007.\u0001(i << 11);
					global::\u0008.\u0004.\u0007.\u001B[i] = 5;
				}
			}

			// Token: 0x06001E82 RID: 7810 RVA: 0x00128484 File Offset: 0x00126684
			public \u0007(global::\u0008.\u0004.\u000E pending)
			{
				this.\u0010 = pending;
				this.\u0011 = new global::\u0008.\u0004.\u0007.\u0001(this, 286, 257, 15);
				this.\u0012 = new global::\u0008.\u0004.\u0007.\u0001(this, 30, 1, 15);
				this.\u0013 = new global::\u0008.\u0004.\u0007.\u0001(this, 19, 4, 7);
				this.\u0014 = new short[16384];
				this.\u0015 = new byte[16384];
			}

			// Token: 0x06001E83 RID: 7811 RVA: 0x001284F8 File Offset: 0x001266F8
			public void \u0001()
			{
				this.\u0016 = 0;
				this.\u0017 = 0;
			}

			// Token: 0x06001E84 RID: 7812 RVA: 0x00128508 File Offset: 0x00126708
			private int \u0001(int \u0002)
			{
				if (\u0002 == 255)
				{
					return 285;
				}
				int num = 257;
				while (\u0002 >= 8)
				{
					num += 4;
					\u0002 >>= 1;
				}
				return num + \u0002;
			}

			// Token: 0x06001E85 RID: 7813 RVA: 0x0012853C File Offset: 0x0012673C
			private int \u0002(int \u0002)
			{
				int num = 0;
				while (\u0002 >= 4)
				{
					num += 2;
					\u0002 >>= 1;
				}
				return num + \u0002;
			}

			// Token: 0x06001E86 RID: 7814 RVA: 0x00128560 File Offset: 0x00126760
			public void \u0001(int \u0002)
			{
				this.\u0013.\u0001();
				this.\u0011.\u0001();
				this.\u0012.\u0001();
				this.\u0010.\u0001(this.\u0011.\u0004 - 257, 5);
				this.\u0010.\u0001(this.\u0012.\u0004 - 1, 5);
				this.\u0010.\u0001(\u0002 - 4, 4);
				for (int i = 0; i < \u0002; i++)
				{
					this.\u0010.\u0001((int)this.\u0013.\u0002[global::\u0008.\u0004.\u0007.\u000E[i]], 3);
				}
				this.\u0011.\u0002(this.\u0013);
				this.\u0012.\u0002(this.\u0013);
			}

			// Token: 0x06001E87 RID: 7815 RVA: 0x00128620 File Offset: 0x00126820
			public void \u0002()
			{
				for (int i = 0; i < this.\u0016; i++)
				{
					int num = (int)(this.\u0015[i] & byte.MaxValue);
					int num2 = (int)this.\u0014[i];
					if (num2-- != 0)
					{
						int num3 = this.\u0001(num);
						this.\u0011.\u0001(num3);
						int num4 = (num3 - 261) / 4;
						if (num4 > 0 && num4 <= 5)
						{
							this.\u0010.\u0001(num & (1 << num4) - 1, num4);
						}
						int num5 = this.\u0002(num2);
						this.\u0012.\u0001(num5);
						num4 = num5 / 2 - 1;
						if (num4 > 0)
						{
							this.\u0010.\u0001(num2 & (1 << num4) - 1, num4);
						}
					}
					else
					{
						this.\u0011.\u0001(num);
					}
				}
				this.\u0011.\u0001(256);
			}

			// Token: 0x06001E88 RID: 7816 RVA: 0x00128700 File Offset: 0x00126900
			public void \u0001(byte[] \u0002, int \u0003, int \u0004, bool \u0005)
			{
				this.\u0010.\u0001(\u0005 ? 1 : 0, 3);
				this.\u0010.\u0001();
				this.\u0010.\u0001(\u0004);
				this.\u0010.\u0001(~\u0004);
				this.\u0010.\u0001(\u0002, \u0003, \u0004);
				this.\u0001();
			}

			// Token: 0x06001E89 RID: 7817 RVA: 0x0012875C File Offset: 0x0012695C
			public void \u0002(byte[] \u0002, int \u0003, int \u0004, bool \u0005)
			{
				short[] u = this.\u0011.\u0001;
				int num = 256;
				u[num] += 1;
				this.\u0011.\u0002();
				this.\u0012.\u0002();
				this.\u0011.\u0001(this.\u0013);
				this.\u0012.\u0001(this.\u0013);
				this.\u0013.\u0002();
				int num2 = 4;
				for (int i = 18; i > num2; i--)
				{
					if (this.\u0013.\u0002[global::\u0008.\u0004.\u0007.\u000E[i]] > 0)
					{
						num2 = i + 1;
					}
				}
				int num3 = 14 + num2 * 3 + this.\u0013.\u0001() + this.\u0011.\u0001() + this.\u0012.\u0001() + this.\u0017;
				int num4 = this.\u0017;
				for (int j = 0; j < 286; j++)
				{
					num4 += (int)(this.\u0011.\u0001[j] * (short)global::\u0008.\u0004.\u0007.\u0019[j]);
				}
				for (int k = 0; k < 30; k++)
				{
					num4 += (int)(this.\u0012.\u0001[k] * (short)global::\u0008.\u0004.\u0007.\u001B[k]);
				}
				if (num3 >= num4)
				{
					num3 = num4;
				}
				if (\u0003 >= 0 && \u0004 + 4 < num3 >> 3)
				{
					this.\u0001(\u0002, \u0003, \u0004, \u0005);
					return;
				}
				if (num3 == num4)
				{
					this.\u0010.\u0001(2 + (\u0005 ? 1 : 0), 3);
					this.\u0011.\u0001(global::\u0008.\u0004.\u0007.\u0018, global::\u0008.\u0004.\u0007.\u0019);
					this.\u0012.\u0001(global::\u0008.\u0004.\u0007.\u001A, global::\u0008.\u0004.\u0007.\u001B);
					this.\u0002();
					this.\u0001();
					return;
				}
				this.\u0010.\u0001(4 + (\u0005 ? 1 : 0), 3);
				this.\u0001(num2);
				this.\u0002();
				this.\u0001();
			}

			// Token: 0x06001E8A RID: 7818 RVA: 0x00128924 File Offset: 0x00126B24
			public bool \u0001()
			{
				return this.\u0016 >= 16384;
			}

			// Token: 0x06001E8B RID: 7819 RVA: 0x00128938 File Offset: 0x00126B38
			public bool \u0001(int \u0002)
			{
				this.\u0014[this.\u0016] = 0;
				this.\u0015[this.\u0016++] = (byte)\u0002;
				short[] u = this.\u0011.\u0001;
				u[\u0002] += 1;
				return this.\u0001();
			}

			// Token: 0x06001E8C RID: 7820 RVA: 0x00128994 File Offset: 0x00126B94
			public bool \u0001(int \u0002, int \u0003)
			{
				this.\u0014[this.\u0016] = (short)\u0002;
				this.\u0015[this.\u0016++] = (byte)(\u0003 - 3);
				int num = this.\u0001(\u0003 - 3);
				short[] u = this.\u0011.\u0001;
				int num2 = num;
				u[num2] += 1;
				if (num >= 265 && num < 285)
				{
					this.\u0017 += (num - 261) / 4;
				}
				int num3 = this.\u0002(\u0002 - 1);
				short[] u2 = this.\u0012.\u0001;
				int num4 = num3;
				u2[num4] += 1;
				if (num3 >= 4)
				{
					this.\u0017 += num3 / 2 - 1;
				}
				return this.\u0001();
			}

			// Token: 0x040020EE RID: 8430
			private const int \u0001 = 16384;

			// Token: 0x040020EF RID: 8431
			private const int \u0002 = 286;

			// Token: 0x040020F0 RID: 8432
			private const int \u0003 = 30;

			// Token: 0x040020F1 RID: 8433
			private const int \u0004 = 19;

			// Token: 0x040020F2 RID: 8434
			private const int \u0005 = 16;

			// Token: 0x040020F3 RID: 8435
			private const int \u0006 = 17;

			// Token: 0x040020F4 RID: 8436
			private const int \u0007 = 18;

			// Token: 0x040020F5 RID: 8437
			private const int \u0008 = 256;

			// Token: 0x040020F6 RID: 8438
			private static readonly int[] \u000E = new int[]
			{
				16,
				17,
				18,
				0,
				8,
				7,
				9,
				6,
				10,
				5,
				11,
				4,
				12,
				3,
				13,
				2,
				14,
				1,
				15
			};

			// Token: 0x040020F7 RID: 8439
			private static readonly byte[] \u000F = new byte[]
			{
				0,
				8,
				4,
				12,
				2,
				10,
				6,
				14,
				1,
				9,
				5,
				13,
				3,
				11,
				7,
				15
			};

			// Token: 0x040020F8 RID: 8440
			private global::\u0008.\u0004.\u000E \u0010;

			// Token: 0x040020F9 RID: 8441
			private global::\u0008.\u0004.\u0007.\u0001 \u0011;

			// Token: 0x040020FA RID: 8442
			private global::\u0008.\u0004.\u0007.\u0001 \u0012;

			// Token: 0x040020FB RID: 8443
			private global::\u0008.\u0004.\u0007.\u0001 \u0013;

			// Token: 0x040020FC RID: 8444
			private short[] \u0014;

			// Token: 0x040020FD RID: 8445
			private byte[] \u0015;

			// Token: 0x040020FE RID: 8446
			private int \u0016;

			// Token: 0x040020FF RID: 8447
			private int \u0017;

			// Token: 0x04002100 RID: 8448
			private static readonly short[] \u0018 = new short[286];

			// Token: 0x04002101 RID: 8449
			private static readonly byte[] \u0019 = new byte[286];

			// Token: 0x04002102 RID: 8450
			private static readonly short[] \u001A;

			// Token: 0x04002103 RID: 8451
			private static readonly byte[] \u001B;

			// Token: 0x02000376 RID: 886
			public class \u0001
			{
				// Token: 0x06001E8D RID: 7821 RVA: 0x00128A60 File Offset: 0x00126C60
				public \u0001(global::\u0008.\u0004.\u0007 dh, int elems, int minCodes, int maxLength)
				{
					this.\u0008 = dh;
					this.\u0003 = minCodes;
					this.\u0007 = maxLength;
					this.\u0001 = new short[elems];
					this.\u0006 = new int[maxLength];
				}

				// Token: 0x06001E8E RID: 7822 RVA: 0x00128A98 File Offset: 0x00126C98
				public void \u0001(int \u0002)
				{
					this.\u0008.\u0010.\u0001((int)this.\u0005[\u0002] & 65535, (int)this.\u0002[\u0002]);
				}

				// Token: 0x06001E8F RID: 7823 RVA: 0x00128AC0 File Offset: 0x00126CC0
				public void \u0001(short[] \u0002, byte[] \u0003)
				{
					this.\u0005 = \u0002;
					this.\u0002 = \u0003;
				}

				// Token: 0x06001E90 RID: 7824 RVA: 0x00128AD0 File Offset: 0x00126CD0
				public void \u0001()
				{
					int[] array = new int[this.\u0007];
					int num = 0;
					this.\u0005 = new short[this.\u0001.Length];
					for (int i = 0; i < this.\u0007; i++)
					{
						array[i] = num;
						num += this.\u0006[i] << 15 - i;
					}
					for (int j = 0; j < this.\u0004; j++)
					{
						int num2 = (int)this.\u0002[j];
						if (num2 > 0)
						{
							this.\u0005[j] = global::\u0008.\u0004.\u0007.\u0001(array[num2 - 1]);
							array[num2 - 1] += 1 << 16 - num2;
						}
					}
				}

				// Token: 0x06001E91 RID: 7825 RVA: 0x00128B7C File Offset: 0x00126D7C
				private void \u0001(int[] \u0002)
				{
					this.\u0002 = new byte[this.\u0001.Length];
					int num = \u0002.Length / 2;
					int num2 = (num + 1) / 2;
					int num3 = 0;
					for (int i = 0; i < this.\u0007; i++)
					{
						this.\u0006[i] = 0;
					}
					int[] array = new int[num];
					array[num - 1] = 0;
					for (int j = num - 1; j >= 0; j--)
					{
						if (\u0002[2 * j + 1] != -1)
						{
							int num4 = array[j] + 1;
							if (num4 > this.\u0007)
							{
								num4 = this.\u0007;
								num3++;
							}
							array[\u0002[2 * j]] = (array[\u0002[2 * j + 1]] = num4);
						}
						else
						{
							int num5 = array[j];
							this.\u0006[num5 - 1]++;
							this.\u0002[\u0002[2 * j]] = (byte)array[j];
						}
					}
					if (num3 == 0)
					{
						return;
					}
					int num6 = this.\u0007 - 1;
					for (;;)
					{
						if (this.\u0006[--num6] != 0)
						{
							do
							{
								this.\u0006[num6]--;
								this.\u0006[++num6]++;
								num3 -= 1 << this.\u0007 - 1 - num6;
							}
							while (num3 > 0 && num6 < this.\u0007 - 1);
							if (num3 <= 0)
							{
								break;
							}
						}
					}
					this.\u0006[this.\u0007 - 1] += num3;
					this.\u0006[this.\u0007 - 2] -= num3;
					int num7 = 2 * num2;
					for (int num8 = this.\u0007; num8 != 0; num8--)
					{
						int k = this.\u0006[num8 - 1];
						while (k > 0)
						{
							int num9 = 2 * \u0002[num7++];
							if (\u0002[num9 + 1] == -1)
							{
								this.\u0002[\u0002[num9]] = (byte)num8;
								k--;
							}
						}
					}
				}

				// Token: 0x06001E92 RID: 7826 RVA: 0x00128D80 File Offset: 0x00126F80
				public void \u0002()
				{
					int num = this.\u0001.Length;
					int[] array = new int[num];
					int i = 0;
					int num2 = 0;
					for (int j = 0; j < num; j++)
					{
						int num3 = (int)this.\u0001[j];
						if (num3 != 0)
						{
							int num4 = i++;
							int num5;
							while (num4 > 0 && (int)this.\u0001[array[num5 = (num4 - 1) / 2]] > num3)
							{
								array[num4] = array[num5];
								num4 = num5;
							}
							array[num4] = j;
							num2 = j;
						}
					}
					while (i < 2)
					{
						int num6 = (num2 < 2) ? (++num2) : 0;
						array[i++] = num6;
					}
					this.\u0004 = Math.Max(num2 + 1, this.\u0003);
					int num7 = i;
					int[] array2 = new int[4 * i - 2];
					int[] array3 = new int[2 * i - 1];
					int num8 = num7;
					for (int k = 0; k < i; k++)
					{
						int num9 = array[k];
						array2[2 * k] = num9;
						array2[2 * k + 1] = -1;
						array3[k] = (int)this.\u0001[num9] << 8;
						array[k] = k;
					}
					do
					{
						int num10 = array[0];
						int num11 = array[--i];
						int num12 = 0;
						int l;
						for (l = 1; l < i; l = l * 2 + 1)
						{
							if (l + 1 < i && array3[array[l]] > array3[array[l + 1]])
							{
								l++;
							}
							array[num12] = array[l];
							num12 = l;
						}
						int num13 = array3[num11];
						while ((l = num12) > 0 && array3[array[num12 = (l - 1) / 2]] > num13)
						{
							array[l] = array[num12];
						}
						array[l] = num11;
						int num14 = array[0];
						num11 = num8++;
						array2[2 * num11] = num10;
						array2[2 * num11 + 1] = num14;
						int num15 = Math.Min(array3[num10] & 255, array3[num14] & 255);
						num13 = (array3[num11] = array3[num10] + array3[num14] - num15 + 1);
						num12 = 0;
						for (l = 1; l < i; l = num12 * 2 + 1)
						{
							if (l + 1 < i && array3[array[l]] > array3[array[l + 1]])
							{
								l++;
							}
							array[num12] = array[l];
							num12 = l;
						}
						while ((l = num12) > 0 && array3[array[num12 = (l - 1) / 2]] > num13)
						{
							array[l] = array[num12];
						}
						array[l] = num11;
					}
					while (i > 1);
					this.\u0001(array2);
				}

				// Token: 0x06001E93 RID: 7827 RVA: 0x00128FD8 File Offset: 0x001271D8
				public int \u0001()
				{
					int num = 0;
					for (int i = 0; i < this.\u0001.Length; i++)
					{
						num += (int)(this.\u0001[i] * (short)this.\u0002[i]);
					}
					return num;
				}

				// Token: 0x06001E94 RID: 7828 RVA: 0x00129010 File Offset: 0x00127210
				public void \u0001(global::\u0008.\u0004.\u0007.\u0001 \u0002)
				{
					int num = -1;
					int i = 0;
					while (i < this.\u0004)
					{
						int num2 = 1;
						int num3 = (int)this.\u0002[i];
						int num4;
						int num5;
						if (num3 == 0)
						{
							num4 = 138;
							num5 = 3;
						}
						else
						{
							num4 = 6;
							num5 = 3;
							if (num != num3)
							{
								short[] u = \u0002.\u0001;
								int num6 = num3;
								u[num6] += 1;
								num2 = 0;
							}
						}
						num = num3;
						i++;
						while (i < this.\u0004 && num == (int)this.\u0002[i])
						{
							i++;
							if (++num2 >= num4)
							{
								break;
							}
						}
						if (num2 < num5)
						{
							short[] u2 = \u0002.\u0001;
							int num7 = num;
							u2[num7] += (short)num2;
						}
						else if (num != 0)
						{
							short[] u3 = \u0002.\u0001;
							int num8 = 16;
							u3[num8] += 1;
						}
						else if (num2 <= 10)
						{
							short[] u4 = \u0002.\u0001;
							int num9 = 17;
							u4[num9] += 1;
						}
						else
						{
							short[] u5 = \u0002.\u0001;
							int num10 = 18;
							u5[num10] += 1;
						}
					}
				}

				// Token: 0x06001E95 RID: 7829 RVA: 0x00129124 File Offset: 0x00127324
				public void \u0002(global::\u0008.\u0004.\u0007.\u0001 \u0002)
				{
					int num = -1;
					int i = 0;
					while (i < this.\u0004)
					{
						int num2 = 1;
						int num3 = (int)this.\u0002[i];
						int num4;
						int num5;
						if (num3 == 0)
						{
							num4 = 138;
							num5 = 3;
						}
						else
						{
							num4 = 6;
							num5 = 3;
							if (num != num3)
							{
								\u0002.\u0001(num3);
								num2 = 0;
							}
						}
						num = num3;
						i++;
						while (i < this.\u0004 && num == (int)this.\u0002[i])
						{
							i++;
							if (++num2 >= num4)
							{
								break;
							}
						}
						if (num2 < num5)
						{
							while (num2-- > 0)
							{
								\u0002.\u0001(num);
							}
						}
						else if (num != 0)
						{
							\u0002.\u0001(16);
							this.\u0008.\u0010.\u0001(num2 - 3, 2);
						}
						else if (num2 <= 10)
						{
							\u0002.\u0001(17);
							this.\u0008.\u0010.\u0001(num2 - 3, 3);
						}
						else
						{
							\u0002.\u0001(18);
							this.\u0008.\u0010.\u0001(num2 - 11, 7);
						}
					}
				}

				// Token: 0x04002104 RID: 8452
				public short[] \u0001;

				// Token: 0x04002105 RID: 8453
				public byte[] \u0002;

				// Token: 0x04002106 RID: 8454
				public int \u0003;

				// Token: 0x04002107 RID: 8455
				public int \u0004;

				// Token: 0x04002108 RID: 8456
				private short[] \u0005;

				// Token: 0x04002109 RID: 8457
				private int[] \u0006;

				// Token: 0x0400210A RID: 8458
				private int \u0007;

				// Token: 0x0400210B RID: 8459
				private global::\u0008.\u0004.\u0007 \u0008;
			}
		}

		// Token: 0x02000377 RID: 887
		internal class \u0008
		{
			// Token: 0x06001E96 RID: 7830 RVA: 0x00129220 File Offset: 0x00127420
			public \u0008(global::\u0008.\u0004.\u000E pending)
			{
				this.\u001E = pending;
				this.\u001F = new global::\u0008.\u0004.\u0007(pending);
				this.\u0019 = new byte[65536];
				this.\u0011 = new short[32768];
				this.\u0012 = new short[32768];
				this.\u0016 = (this.\u0017 = 1);
			}

			// Token: 0x06001E97 RID: 7831 RVA: 0x00129288 File Offset: 0x00127488
			private void \u0001()
			{
				this.\u0010 = ((int)this.\u0019[this.\u0017] << 5 ^ (int)this.\u0019[this.\u0017 + 1]);
			}

			// Token: 0x06001E98 RID: 7832 RVA: 0x001292B0 File Offset: 0x001274B0
			private int \u0001()
			{
				int num = (this.\u0010 << 5 ^ (int)this.\u0019[this.\u0017 + 2]) & 32767;
				short num2 = this.\u0012[this.\u0017 & 32767] = this.\u0011[num];
				this.\u0011[num] = (short)this.\u0017;
				this.\u0010 = num;
				return (int)num2 & 65535;
			}

			// Token: 0x06001E99 RID: 7833 RVA: 0x00129318 File Offset: 0x00127518
			private void \u0002()
			{
				Array.Copy(this.\u0019, 32768, this.\u0019, 0, 32768);
				this.\u0013 -= 32768;
				this.\u0017 -= 32768;
				this.\u0016 -= 32768;
				for (int i = 0; i < 32768; i++)
				{
					int num = (int)this.\u0011[i] & 65535;
					this.\u0011[i] = (short)((num >= 32768) ? (num - 32768) : 0);
				}
				for (int j = 0; j < 32768; j++)
				{
					int num2 = (int)this.\u0012[j] & 65535;
					this.\u0012[j] = (short)((num2 >= 32768) ? (num2 - 32768) : 0);
				}
			}

			// Token: 0x06001E9A RID: 7834 RVA: 0x001293EC File Offset: 0x001275EC
			public void \u0003()
			{
				if (this.\u0017 >= 65274)
				{
					this.\u0002();
				}
				while (this.\u0018 < 262 && this.\u001C < this.\u001D)
				{
					int num = 65536 - this.\u0018 - this.\u0017;
					if (num > this.\u001D - this.\u001C)
					{
						num = this.\u001D - this.\u001C;
					}
					Array.Copy(this.\u001A, this.\u001C, this.\u0019, this.\u0017 + this.\u0018, num);
					this.\u001C += num;
					this.\u001B += num;
					this.\u0018 += num;
				}
				if (this.\u0018 >= 3)
				{
					this.\u0001();
				}
			}

			// Token: 0x06001E9B RID: 7835 RVA: 0x001294C4 File Offset: 0x001276C4
			private bool \u0001(int \u0002)
			{
				int num = 128;
				int num2 = 128;
				short[] u = this.\u0012;
				int num3 = this.\u0017;
				int num4 = this.\u0017 + this.\u0014;
				int num5 = Math.Max(this.\u0014, 2);
				int num6 = Math.Max(this.\u0017 - 32506, 0);
				int num7 = this.\u0017 + 258 - 1;
				byte b = this.\u0019[num4 - 1];
				byte b2 = this.\u0019[num4];
				if (num5 >= 8)
				{
					num >>= 2;
				}
				if (num2 > this.\u0018)
				{
					num2 = this.\u0018;
				}
				do
				{
					if (this.\u0019[\u0002 + num5] == b2 && this.\u0019[\u0002 + num5 - 1] == b && this.\u0019[\u0002] == this.\u0019[num3] && this.\u0019[\u0002 + 1] == this.\u0019[num3 + 1])
					{
						int num8 = \u0002 + 2;
						num3 += 2;
						while (this.\u0019[++num3] == this.\u0019[++num8] && this.\u0019[++num3] == this.\u0019[++num8] && this.\u0019[++num3] == this.\u0019[++num8] && this.\u0019[++num3] == this.\u0019[++num8] && this.\u0019[++num3] == this.\u0019[++num8] && this.\u0019[++num3] == this.\u0019[++num8] && this.\u0019[++num3] == this.\u0019[++num8] && this.\u0019[++num3] == this.\u0019[++num8] && num3 < num7)
						{
						}
						if (num3 > num4)
						{
							this.\u0013 = \u0002;
							num4 = num3;
							num5 = num3 - this.\u0017;
							if (num5 >= num2)
							{
								break;
							}
							b = this.\u0019[num4 - 1];
							b2 = this.\u0019[num4];
						}
						num3 = this.\u0017;
					}
				}
				while ((\u0002 = ((int)u[\u0002 & 32767] & 65535)) > num6 && --num != 0);
				this.\u0014 = Math.Min(num5, this.\u0018);
				return this.\u0014 >= 3;
			}

			// Token: 0x06001E9C RID: 7836 RVA: 0x00129728 File Offset: 0x00127928
			private bool \u0001(bool \u0002, bool \u0003)
			{
				if (this.\u0018 < 262 && !\u0002)
				{
					return false;
				}
				while (this.\u0018 >= 262 || \u0002)
				{
					if (this.\u0018 == 0)
					{
						if (this.\u0015)
						{
							this.\u001F.\u0001((int)(this.\u0019[this.\u0017 - 1] & byte.MaxValue));
						}
						this.\u0015 = false;
						this.\u001F.\u0002(this.\u0019, this.\u0016, this.\u0017 - this.\u0016, \u0003);
						this.\u0016 = this.\u0017;
						return false;
					}
					if (this.\u0017 >= 65274)
					{
						this.\u0002();
					}
					int u = this.\u0013;
					int num = this.\u0014;
					if (this.\u0018 >= 3)
					{
						int num2 = this.\u0001();
						if (num2 != 0 && this.\u0017 - num2 <= 32506 && this.\u0001(num2) && this.\u0014 <= 5 && this.\u0014 == 3 && this.\u0017 - this.\u0013 > 4096)
						{
							this.\u0014 = 2;
						}
					}
					if (num >= 3 && this.\u0014 <= num)
					{
						this.\u001F.\u0001(this.\u0017 - 1 - u, num);
						num -= 2;
						do
						{
							this.\u0017++;
							this.\u0018--;
							if (this.\u0018 >= 3)
							{
								this.\u0001();
							}
						}
						while (--num > 0);
						this.\u0017++;
						this.\u0018--;
						this.\u0015 = false;
						this.\u0014 = 2;
					}
					else
					{
						if (this.\u0015)
						{
							this.\u001F.\u0001((int)(this.\u0019[this.\u0017 - 1] & byte.MaxValue));
						}
						this.\u0015 = true;
						this.\u0017++;
						this.\u0018--;
					}
					if (this.\u001F.\u0001())
					{
						int num3 = this.\u0017 - this.\u0016;
						if (this.\u0015)
						{
							num3--;
						}
						bool flag = \u0003 && this.\u0018 == 0 && !this.\u0015;
						this.\u001F.\u0002(this.\u0019, this.\u0016, num3, flag);
						this.\u0016 += num3;
						return !flag;
					}
				}
				return true;
			}

			// Token: 0x06001E9D RID: 7837 RVA: 0x00129990 File Offset: 0x00127B90
			public bool \u0002(bool \u0002, bool \u0003)
			{
				bool flag;
				do
				{
					this.\u0003();
					bool u = \u0002 && this.\u001C == this.\u001D;
					flag = this.\u0001(u, \u0003);
				}
				while (this.\u001E.IsFlushed && flag);
				return flag;
			}

			// Token: 0x06001E9E RID: 7838 RVA: 0x001299D4 File Offset: 0x00127BD4
			public void \u0001(byte[] \u0002)
			{
				this.\u001A = \u0002;
				this.\u001C = 0;
				this.\u001D = \u0002.Length;
			}

			// Token: 0x06001E9F RID: 7839 RVA: 0x001299F0 File Offset: 0x00127BF0
			public bool \u0001()
			{
				return this.\u001D == this.\u001C;
			}

			// Token: 0x0400210C RID: 8460
			private const int \u0001 = 258;

			// Token: 0x0400210D RID: 8461
			private const int \u0002 = 3;

			// Token: 0x0400210E RID: 8462
			private const int \u0003 = 32768;

			// Token: 0x0400210F RID: 8463
			private const int \u0004 = 32767;

			// Token: 0x04002110 RID: 8464
			private const int \u0005 = 32768;

			// Token: 0x04002111 RID: 8465
			private const int \u0006 = 32767;

			// Token: 0x04002112 RID: 8466
			private const int \u0007 = 5;

			// Token: 0x04002113 RID: 8467
			private const int \u0008 = 262;

			// Token: 0x04002114 RID: 8468
			private const int \u000E = 32506;

			// Token: 0x04002115 RID: 8469
			private const int \u000F = 4096;

			// Token: 0x04002116 RID: 8470
			private int \u0010;

			// Token: 0x04002117 RID: 8471
			private short[] \u0011;

			// Token: 0x04002118 RID: 8472
			private short[] \u0012;

			// Token: 0x04002119 RID: 8473
			private int \u0013;

			// Token: 0x0400211A RID: 8474
			private int \u0014;

			// Token: 0x0400211B RID: 8475
			private bool \u0015;

			// Token: 0x0400211C RID: 8476
			private int \u0016;

			// Token: 0x0400211D RID: 8477
			private int \u0017;

			// Token: 0x0400211E RID: 8478
			private int \u0018;

			// Token: 0x0400211F RID: 8479
			private byte[] \u0019;

			// Token: 0x04002120 RID: 8480
			private byte[] \u001A;

			// Token: 0x04002121 RID: 8481
			private int \u001B;

			// Token: 0x04002122 RID: 8482
			private int \u001C;

			// Token: 0x04002123 RID: 8483
			private int \u001D;

			// Token: 0x04002124 RID: 8484
			private global::\u0008.\u0004.\u000E \u001E;

			// Token: 0x04002125 RID: 8485
			private global::\u0008.\u0004.\u0007 \u001F;
		}

		// Token: 0x02000378 RID: 888
		internal class \u000E
		{
			// Token: 0x06001EA0 RID: 7840 RVA: 0x00129A00 File Offset: 0x00127C00
			public void \u0001(int \u0002)
			{
				this.\u0001[this.\u0003++] = (byte)\u0002;
				this.\u0001[this.\u0003++] = (byte)(\u0002 >> 8);
			}

			// Token: 0x06001EA1 RID: 7841 RVA: 0x00129A44 File Offset: 0x00127C44
			public void \u0001(byte[] \u0002, int \u0003, int \u0004)
			{
				Array.Copy(\u0002, \u0003, this.\u0001, this.\u0003, \u0004);
				this.\u0003 += \u0004;
			}

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x00129A68 File Offset: 0x00127C68
			public int BitCount
			{
				get
				{
					return this.\u0005;
				}
			}

			// Token: 0x06001EA3 RID: 7843 RVA: 0x00129A70 File Offset: 0x00127C70
			public void \u0001()
			{
				if (this.\u0005 > 0)
				{
					this.\u0001[this.\u0003++] = (byte)this.\u0004;
					if (this.\u0005 > 8)
					{
						this.\u0001[this.\u0003++] = (byte)(this.\u0004 >> 8);
					}
				}
				this.\u0004 = 0U;
				this.\u0005 = 0;
			}

			// Token: 0x06001EA4 RID: 7844 RVA: 0x00129AE0 File Offset: 0x00127CE0
			public void \u0001(int \u0002, int \u0003)
			{
				this.\u0004 |= (uint)((uint)\u0002 << this.\u0005);
				this.\u0005 += \u0003;
				if (this.\u0005 >= 16)
				{
					this.\u0001[this.\u0003++] = (byte)this.\u0004;
					this.\u0001[this.\u0003++] = (byte)(this.\u0004 >> 8);
					this.\u0004 >>= 16;
					this.\u0005 -= 16;
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00129B7C File Offset: 0x00127D7C
			public bool IsFlushed
			{
				get
				{
					return this.\u0003 == 0;
				}
			}

			// Token: 0x06001EA6 RID: 7846 RVA: 0x00129B88 File Offset: 0x00127D88
			public int \u0001(byte[] \u0002, int \u0003, int \u0004)
			{
				if (this.\u0005 >= 8)
				{
					this.\u0001[this.\u0003++] = (byte)this.\u0004;
					this.\u0004 >>= 8;
					this.\u0005 -= 8;
				}
				if (\u0004 > this.\u0003 - this.\u0002)
				{
					\u0004 = this.\u0003 - this.\u0002;
					Array.Copy(this.\u0001, this.\u0002, \u0002, \u0003, \u0004);
					this.\u0002 = 0;
					this.\u0003 = 0;
				}
				else
				{
					Array.Copy(this.\u0001, this.\u0002, \u0002, \u0003, \u0004);
					this.\u0002 += \u0004;
				}
				return \u0004;
			}

			// Token: 0x04002126 RID: 8486
			protected byte[] \u0001 = new byte[65536];

			// Token: 0x04002127 RID: 8487
			private int \u0002;

			// Token: 0x04002128 RID: 8488
			private int \u0003;

			// Token: 0x04002129 RID: 8489
			private uint \u0004;

			// Token: 0x0400212A RID: 8490
			private int \u0005;
		}

		// Token: 0x02000379 RID: 889
		internal class \u000F : MemoryStream
		{
			// Token: 0x06001EA8 RID: 7848 RVA: 0x00129C58 File Offset: 0x00127E58
			public void \u0001(int \u0002)
			{
				this.WriteByte((byte)(\u0002 & 255));
				this.WriteByte((byte)(\u0002 >> 8 & 255));
			}

			// Token: 0x06001EA9 RID: 7849 RVA: 0x00129C78 File Offset: 0x00127E78
			public void \u0002(int \u0002)
			{
				this.\u0001(\u0002);
				this.\u0001(\u0002 >> 16);
			}

			// Token: 0x06001EAA RID: 7850 RVA: 0x00129C8C File Offset: 0x00127E8C
			public int \u0001()
			{
				return this.ReadByte() | this.ReadByte() << 8;
			}

			// Token: 0x06001EAB RID: 7851 RVA: 0x00129CA0 File Offset: 0x00127EA0
			public int \u0002()
			{
				return this.\u0001() | this.\u0001() << 16;
			}

			// Token: 0x06001EAC RID: 7852 RVA: 0x00129CB4 File Offset: 0x00127EB4
			public \u000F()
			{
			}

			// Token: 0x06001EAD RID: 7853 RVA: 0x00129CBC File Offset: 0x00127EBC
			public \u000F(byte[] buffer) : base(buffer, false)
			{
			}
		}
	}
}
