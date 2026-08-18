using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;
using Spire.DataExport.XLS;

// Token: 0x02000083 RID: 131
internal abstract class sprᮌ
{
	// Token: 0x060003EA RID: 1002
	[DllImport("kernel32")]
	public static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

	// Token: 0x060003EB RID: 1003 RVA: 0x00024BEC File Offset: 0x00023BEC
	public unsafe static ushort ᜁ(byte[] A_0, int A_1)
	{
		int a_ = 6;
		int num = 6;
		for (;;)
		{
			ushort result;
			switch (num)
			{
			case 0:
				goto IL_57;
			case 1:
				goto IL_A8;
			case 2:
				goto IL_A8;
			case 3:
				return result;
			case 4:
				goto IL_F7;
			case 5:
				num = 7;
				continue;
			case 7:
				if (A_0.Length == 0)
				{
					num = 4;
					continue;
				}
				fixed (byte* ptr = &A_0[0])
				{
					if (true)
					{
					}
					num = 2;
					continue;
					break;
				}
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (A_0 == null)
					{
						goto IL_F7;
					}
					break;
				}
				num = 5;
				continue;
			}
			if (A_0.Length - A_1 < 2)
			{
				num = 0;
				continue;
			}
			num = 8;
			continue;
			IL_A8:
			byte* ptr;
			result = ((ushort*)ptr)[A_1 / 2];
			num = 3;
			continue;
			IL_F7:
			ptr = null;
			num = 1;
		}
		IL_57:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("次䨣倥䤧䘩䔫䨭缯䈱儳䐵夷丹唻儽⸿ᵁ̓⍅㱇ὉὋ♍㽏⁑⁓U㥗㙙⥛㭝", a_)));
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00024D04 File Offset: 0x00023D04
	public static void ᜀ(byte[] A_0, int A_1, ushort A_2)
	{
		int a_ = 19;
		while (A_0.Length - A_1 >= 2)
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
				byte[] bytes = BitConverter.GetBytes(A_2);
				Array.Copy(bytes, 0, A_0, A_1, bytes.Length);
				return;
			}
			}
		}
		if (true)
		{
		}
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("昮弰䔲吴嬶倸强爼伾⑀ㅂ⑄㍆⁈⑊⍌၎ၐ⁒♔㹖㹘㕚࡜౞ॠౢᝤ፦", a_)));
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00024D84 File Offset: 0x00023D84
	public unsafe static int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 2;
		int num = 4;
		for (;;)
		{
			int result;
			switch (num)
			{
			case 0:
				goto IL_50;
			case 1:
				goto IL_FD;
			case 2:
				goto IL_C2;
			case 3:
				if (A_0.Length != 0)
				{
					fixed (byte* ptr = &A_0[0])
					{
						num = 6;
						continue;
					}
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_62;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 5:
				return result;
			case 6:
				goto IL_62;
			case 7:
				if (A_0 != null)
				{
					if (true)
					{
					}
					num = 8;
					continue;
				}
				goto IL_FD;
			case 8:
				num = 3;
				continue;
			}
			if (A_0.Length - A_1 < 4)
			{
				num = 0;
				continue;
			}
			num = 7;
			continue;
			IL_C2:
			byte* ptr;
			result = ((int*)ptr)[A_1 / 4];
			num = 5;
			continue;
			IL_62:
			goto IL_C2;
			IL_FD:
			ptr = null;
			num = 2;
		}
		IL_50:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("圝丟吡䔣䨥䄧丩挫席唯䀱唳䈵儷唹刻愽ܿ❁ぃཅ♇㹉⥋⥍㕏⁑", a_)));
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00024EA0 File Offset: 0x00023EA0
	public static void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 14;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_37;
		}
		if (false)
		{
		}
		if (A_0.Length - A_1 >= 4)
		{
			byte[] bytes = BitConverter.GetBytes(A_2);
			Array.Copy(bytes, 0, A_0, A_1, bytes.Length);
			return;
		}
		IL_37:
		if (true)
		{
		}
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("挩䈫堭儯帱崳刵眷䨹夻䰽ℿ㙁ⵃ⥅♇ᕉോ㵍⍏㭑㍓㡕ᅗ㑙⡛ᩝşᙡգ", a_)));
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00024F20 File Offset: 0x00023F20
	internal unsafe static void ᜀ(ref spr\u2320 A_0, ref int A_1, int A_2, void* A_3)
	{
		int a_ = 11;
		int num = 21;
		int num2;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_203;
			case 1:
				if (A_3 != null)
				{
					num = 15;
					continue;
				}
				goto IL_2D6;
			case 2:
				if (num2 == 0)
				{
					num = 18;
					continue;
				}
				goto IL_203;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2B8;
				default:
					if (false)
					{
					}
					if (A_2 > 0)
					{
						num = 13;
						continue;
					}
					goto IL_203;
				}
				break;
			case 4:
				if (num2 < 0)
				{
					num = 14;
					continue;
				}
				num = 2;
				continue;
			case 5:
				if (array.Length == 0)
				{
					num = 9;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 8;
					continue;
					break;
				}
			case 6:
				goto IL_1C3;
			case 7:
				num = 5;
				continue;
			case 8:
				goto IL_1C3;
			case 9:
				goto IL_8A;
			case 10:
				goto IL_1F0;
			case 11:
				num = 12;
				continue;
			case 12:
				goto IL_2B8;
			case 13:
				A_1 = 0;
				A_0 = A_0.ᜡ();
				num = 0;
				continue;
			case 14:
				goto IL_10A;
			case 15:
				goto IL_159;
			case 16:
				goto IL_220;
			case 17:
				if (A_2 <= num2)
				{
					num = 20;
					continue;
				}
				sprᮌ.ᜀ(ref A_0, ref A_1, num2, A_3);
				num = 1;
				continue;
			case 18:
				num = 3;
				continue;
			case 19:
				if (A_0 == null)
				{
					num = 16;
					continue;
				}
				num2 = A_0.ᜌ() - A_1;
				num = 17;
				continue;
			case 20:
				num = 22;
				continue;
			case 22:
				if (A_3 != null)
				{
					num = 11;
					continue;
				}
				goto IL_DE;
			case 23:
				goto IL_85;
			}
			if (A_0 == null)
			{
				num = 23;
				continue;
			}
			num2 = A_0.ᜌ() - A_1;
			num = 4;
			continue;
			IL_8A:
			byte* ptr = null;
			num = 6;
			continue;
			IL_2B8:
			if ((array = A_0.ᜢ()) != null)
			{
				num = 7;
				continue;
			}
			goto IL_8A;
			IL_1C3:
			sprᮌ.CopyMemory((IntPtr)A_3, (IntPtr)((int)ptr[A_1]), A_2);
			ptr = null;
			if (true)
			{
			}
			num = 10;
			continue;
			IL_203:
			num = 19;
		}
		IL_85:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("渦䜨崪䰬䌮堰圲稴䜶尸䤺尼䬾⡀ⱂ⭄ᡆᭈ⹊ⱌ⭎㡐㵒㉔Ֆ㱘㡚㉜ⵞՠ", a_)));
		IL_DE:
		A_1 += A_2;
		return;
		IL_10A:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("渦䜨崪䰬䌮堰圲稴䜶尸䤺尼䬾⡀ⱂ⭄ᡆᭈ⹊ⱌ⭎㡐㵒㉔Ֆ㱘㡚㉜ⵞՠ", a_)));
		IL_159:
		sprᮌ.ᜀ(ref A_0, ref A_1, A_2 - num2, (void*)((byte*)A_3 + num2));
		return;
		IL_1F0:
		goto IL_DE;
		IL_220:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("渦䜨崪䰬䌮堰圲稴䜶尸䤺尼䬾⡀ⱂ⭄ᡆᭈ⹊ⱌ⭎㡐㵒㉔Ֆ㱘㡚㉜ⵞՠ", a_)));
		IL_2D6:
		sprᮌ.ᜀ(ref A_0, ref A_1, A_2 - num2, null);
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x0002521C File Offset: 0x0002421C
	internal unsafe static void ᜀ(ref spr\u2320 A_0, ref int A_1, ref byte[] A_2, ref char[] A_3, ref byte A_4, ref byte A_5, ref int A_6, int A_7)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			IntPtr* ptr2;
			IntPtr* ptr3;
			for (;;)
			{
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_319:
					fixed (byte* ptr = &A_0.ᜢ()[A_1])
					{
						sprᮌ.CopyMemory((IntPtr)((void*)ptr2), (IntPtr)((void*)ptr), num);
					}
					num2 = 56;
					break;
				default:
					if (false)
					{
					}
					num = A_0.ᜌ() - A_1;
					num2 = 47;
					break;
				}
				for (;;)
				{
					int num4;
					int num7;
					switch (num2)
					{
					case 0:
						goto IL_618;
					case 1:
						goto IL_55D;
					case 2:
						num2 = 16;
						continue;
					case 3:
						num2 = 62;
						continue;
					case 4:
						goto IL_618;
					case 5:
					{
						string text = sprᮌ.ᜀ(A_2);
						char[] chars = Encoding.Unicode.GetChars(Encoding.Unicode.GetBytes(text));
						Array.Copy(chars, A_3, text.Length);
						A_4 |= 1;
						num2 = 1;
						continue;
					}
					case 6:
						num2 = 10;
						continue;
					case 7:
						goto IL_422;
					case 8:
						goto IL_7D6;
					case 9:
						if ((A_4 & 1) == 1)
						{
							num2 = 13;
							continue;
						}
						goto IL_78A;
					case 10:
						if ((A_5 & 1) == 0)
						{
							num2 = 29;
							continue;
						}
						goto IL_4FB;
					case 11:
					{
						int num3;
						if (num3 >= num / num4)
						{
							num2 = 25;
							continue;
						}
						A_3[A_6 + 1 + num3] = (char)A_0.ᜢ()[A_1 + num3];
						num3++;
						num2 = 20;
						continue;
					}
					case 12:
						if ((A_4 & 1) == 1)
						{
							num2 = 17;
							continue;
						}
						goto IL_4FB;
					case 13:
					{
						int num3 = 0;
						num2 = 28;
						continue;
					}
					case 14:
						if ((A_5 & 1) == 0)
						{
							num2 = 35;
							continue;
						}
						goto IL_472;
					case 15:
						goto IL_55D;
					case 16:
						if ((A_4 & 1) == 0)
						{
							num2 = 5;
							continue;
						}
						goto IL_55D;
					case 17:
					{
						int num5 = 0;
						num2 = 37;
						continue;
					}
					case 18:
						num2 = 14;
						continue;
					case 19:
						if (A_6 == 0)
						{
							num2 = 52;
							continue;
						}
						A_1 = 1;
						num2 = 40;
						continue;
					case 20:
						goto IL_287;
					case 21:
						if ((A_5 & 1) == 0)
						{
							num2 = 41;
							continue;
						}
						goto IL_78A;
					case 22:
						if ((A_4 & 1) == 1)
						{
							num2 = 65;
							continue;
						}
						goto IL_472;
					case 23:
					{
						int num6;
						if (num6 >= num7 / num4)
						{
							num2 = 46;
							continue;
						}
						A_3[A_6 + 1 + num6] = (char)A_0.ᜢ()[A_1 + num6];
						num6++;
						num2 = 55;
						continue;
					}
					case 24:
						goto IL_634;
					case 25:
						num2 = 8;
						continue;
					case 26:
						goto IL_803;
					case 27:
						if (A_5 == 1)
						{
							num2 = 2;
							continue;
						}
						goto IL_55D;
					case 28:
						goto IL_287;
					case 29:
						num2 = 12;
						continue;
					case 30:
						goto IL_20E;
					case 31:
						if (num7 <= num)
						{
							num2 = 18;
							continue;
						}
						num2 = 21;
						continue;
					case 32:
						goto IL_671;
					case 33:
						goto IL_808;
					case 34:
					{
						int num5;
						if (num5 >= num7 / num4)
						{
							num2 = 3;
							continue;
						}
						A_3[A_6 + 1 + num5] = (char)A_0.ᜢ()[A_1 + num5];
						num5++;
						num2 = 7;
						continue;
					}
					case 35:
						num2 = 22;
						continue;
					case 36:
						num2 = 59;
						continue;
					case 37:
						goto IL_422;
					case 38:
						num2 = 64;
						continue;
					case 39:
						if (true)
						{
						}
						num7 = A_7 - A_6;
						num4 = 1;
						fixed (IntPtr* ptr3 = (IntPtr*)(&A_2[A_6 + 1]))
						{
							num2 = 31;
							continue;
						}
					case 40:
						if (A_0.ᜡ() == null)
						{
							num2 = 43;
							continue;
						}
						A_0 = A_0.ᜡ();
						A_5 = A_0.ᜢ()[0];
						num2 = 27;
						continue;
					case 41:
						num2 = 9;
						continue;
					case 42:
						num2 = 33;
						continue;
					case 43:
						goto IL_3C8;
					case 44:
						if (num7 <= num)
						{
							num2 = 6;
							continue;
						}
						num2 = 67;
						continue;
					case 45:
					{
						int num8 = 0;
						num2 = 51;
						continue;
					}
					case 46:
						num2 = 0;
						continue;
					case 47:
						if (num < 0)
						{
							num2 = 60;
							continue;
						}
						num2 = 66;
						continue;
					case 48:
						goto IL_6BA;
					case 49:
						if (A_0.ᜡ() == null)
						{
							num2 = 48;
							continue;
						}
						A_0 = A_0.ᜡ();
						num2 = 15;
						continue;
					case 50:
						num2 = 19;
						continue;
					case 51:
						goto IL_671;
					case 52:
						A_1 = 0;
						num2 = 49;
						continue;
					case 53:
						if ((A_5 & 1) == 0)
						{
							num2 = 39;
							continue;
						}
						num7 = (A_7 - A_6) * 2;
						num4 = 2;
						fixed (IntPtr* ptr2 = (IntPtr*)(&A_3[A_6 + 1]))
						{
							num2 = 44;
							continue;
							break;
						}
					case 54:
					{
						int num8;
						if (num8 >= num / num4)
						{
							num2 = 42;
							continue;
						}
						A_3[A_6 + 1 + num8] = (char)A_0.ᜢ()[A_1 + num8];
						num8++;
						num2 = 32;
						continue;
					}
					case 55:
						goto IL_44A;
					case 56:
						goto IL_808;
					case 57:
						goto IL_44A;
					case 58:
						goto IL_835;
					case 59:
						if ((A_4 & 1) == 1)
						{
							num2 = 45;
							continue;
						}
						goto IL_319;
					case 60:
						goto IL_179;
					case 61:
						goto IL_1F2;
					case 62:
						goto IL_1F2;
					case 63:
						goto IL_7D6;
					case 64:
						if (A_7 > 0)
						{
							num2 = 50;
							continue;
						}
						goto IL_55D;
					case 65:
					{
						int num6 = 0;
						num2 = 57;
						continue;
					}
					case 66:
						if (num == 0)
						{
							num2 = 38;
							continue;
						}
						goto IL_55D;
					case 67:
						if ((A_5 & 1) == 0)
						{
							num2 = 36;
							continue;
						}
						goto IL_319;
					}
					break;
					IL_1F2:
					A_1 += num7;
					A_6 += num7 / num4;
					num2 = 30;
					continue;
					IL_287:
					num2 = 11;
					continue;
					IL_422:
					num2 = 34;
					continue;
					IL_44A:
					num2 = 23;
					continue;
					IL_472:
					fixed (byte* ptr4 = &A_0.ᜢ()[A_1])
					{
						sprᮌ.CopyMemory((IntPtr)((void*)ptr3), (IntPtr)((void*)ptr4), num7);
					}
					num2 = 4;
					continue;
					IL_4FB:
					fixed (byte* ptr5 = &A_0.ᜢ()[A_1])
					{
						sprᮌ.CopyMemory((IntPtr)((void*)ptr2), (IntPtr)((void*)ptr5), num7);
					}
					num2 = 61;
					continue;
					IL_55D:
					num = A_0.ᜌ() - A_1;
					num7 = 0;
					num4 = 0;
					num2 = 53;
					continue;
					IL_618:
					A_1 += num7;
					A_6 += num7 / num4;
					num2 = 24;
					continue;
					IL_671:
					num2 = 54;
					continue;
					IL_78A:
					fixed (byte* ptr6 = &A_0.ᜢ()[A_1])
					{
						sprᮌ.CopyMemory((IntPtr)((void*)ptr3), (IntPtr)((void*)ptr6), num);
					}
					num2 = 63;
					continue;
					IL_7D6:
					A_1 += num;
					A_6 += num / num4;
					sprᮌ.ᜀ(ref A_0, ref A_1, ref A_2, ref A_3, ref A_4, ref A_5, ref A_6, A_7);
					num2 = 26;
					continue;
					IL_808:
					A_1 += num;
					A_6 += num / num4;
					sprᮌ.ᜀ(ref A_0, ref A_1, ref A_2, ref A_3, ref A_4, ref A_5, ref A_6, A_7);
					num2 = 58;
				}
			}
			IL_179:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("搬䄮䜰刲头帶崸琺䴼娾㍀≂ㅄ⹆♈╊ቌᵎ㑐㉒ㅔ㹖㝘㱚ཛྷ㩞ɠౢᝤͦ", a_)));
			IL_20E:
			goto IL_8CF;
			IL_3C8:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("搬䄮䜰刲头帶崸琺䴼娾㍀≂ㅄ⹆♈╊ቌᵎ㑐㉒ㅔ㹖㝘㱚ཛྷ㩞ɠౢᝤͦ", a_)));
			IL_4AD:
			ptr3 = null;
			return;
			IL_634:
			goto IL_4AD;
			IL_6BA:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("搬䄮䜰刲头帶崸琺䴼娾㍀≂ㅄ⹆♈╊ቌᵎ㑐㉒ㅔ㹖㝘㱚ཛྷ㩞ɠౢᝤͦ", a_)));
			IL_803:
			goto IL_4AD;
			IL_835:
			IL_8CF:
			ptr2 = null;
			return;
		}
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00025AFC File Offset: 0x00024AFC
	public static string ᜀ(byte[] A_0)
	{
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				for (;;)
				{
					stringBuilder = new StringBuilder(A_0.Length);
					int num = 0;
					if (true)
					{
					}
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_47;
						case 1:
							goto IL_47;
						case 2:
							goto IL_5F;
						case 3:
						{
							if (num >= A_0.Length)
							{
								num2 = 2;
								continue;
							}
							byte value = A_0[num];
							stringBuilder.Append((char)value);
							num++;
							num2 = 1;
							continue;
						}
						}
						break;
						IL_47:
						num2 = 3;
					}
				}
				IL_5F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_9C;
				}
			}
			IL_9C:
			if (false)
			{
			}
			return stringBuilder.ToString();
		}
		}
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00025BB4 File Offset: 0x00024BB4
	public static byte[] ᜆ(string A_0)
	{
		byte[] array;
		for (;;)
		{
			array = new byte[A_0.Length];
			int num = 0;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E;
					default:
						goto IL_82;
					}
					break;
				case 1:
					if (num >= A_0.Length)
					{
						num2 = 0;
						continue;
					}
					if (true)
					{
					}
					array[num] = (byte)A_0[num];
					num++;
					num2 = 3;
					continue;
				case 2:
					goto IL_2E;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 1;
				continue;
				IL_2E:
				goto IL_30;
			}
		}
		IL_82:
		if (false)
		{
		}
		return array;
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00025C54 File Offset: 0x00024C54
	public static string ᜁ(int A_0)
	{
		int a_ = 12;
		string str;
		int startIndex;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					int num = A_0 - 1;
					str = string.Empty;
					int num2 = num / 26;
					startIndex = num % 26;
					int num3 = 0;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (num2 > 0)
							{
								num3 = 2;
								continue;
							}
							goto IL_B2;
						case 1:
							goto IL_B0;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							default:
								if (false)
								{
								}
								str += HyperlinksCollectionEditor.b("椧栩漫樭甯琱猳縵焷瀹眻爽ിుୃᙅ᥇ᡉὋᩍՏё͓๕ŗY", a_).Substring(num2 - 1, 1);
								num3 = 1;
								continue;
							}
							break;
						}
						break;
					}
				}
				break;
			}
		}
		IL_B0:
		IL_B2:
		return str + HyperlinksCollectionEditor.b("椧栩漫樭甯琱猳縵焷瀹眻爽ിుୃᙅ᥇ᡉὋᩍՏё͓๕ŗY", a_).Substring(startIndex, 1);
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00025D30 File Offset: 0x00024D30
	public static string ᜀ(int A_0)
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
		return A_0.ToString();
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00025D74 File Offset: 0x00024D74
	public static string ᜀ(byte A_0)
	{
		int a_ = 3;
		for (;;)
		{
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 != 42)
					{
						num = 7;
						continue;
					}
					goto IL_185;
				case 1:
					goto IL_24A;
				case 2:
					if (A_0 != 23)
					{
						num = 8;
						continue;
					}
					goto IL_176;
				case 3:
					if (A_0 <= 15)
					{
						num = 14;
						continue;
					}
					goto IL_156;
				case 4:
					if (A_0 != 7)
					{
						num = 12;
						continue;
					}
					goto IL_8C;
				case 5:
					num = 0;
					continue;
				case 6:
					num = 4;
					continue;
				case 7:
					num = 1;
					continue;
				case 8:
					num = 17;
					continue;
				case 9:
					if (A_0 != 15)
					{
						num = 11;
						continue;
					}
					goto IL_230;
				case 10:
					goto IL_C9;
				case 11:
					num = 10;
					continue;
				case 12:
					num = 9;
					continue;
				case 13:
					if (A_0 <= 29)
					{
						num = 16;
						continue;
					}
					num = 18;
					continue;
				case 14:
					num = 20;
					continue;
				case 15:
					num = 19;
					continue;
				case 16:
					num = 2;
					continue;
				case 17:
					if (A_0 != 29)
					{
						num = 15;
						continue;
					}
					goto IL_CE;
				case 18:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_156;
					default:
						if (false)
						{
						}
						if (A_0 != 36)
						{
							num = 5;
							continue;
						}
						goto IL_10B;
					}
					break;
				case 19:
					goto IL_22E;
				case 20:
					if (A_0 != 0)
					{
						num = 6;
						continue;
					}
					goto IL_FC;
				}
				break;
				IL_156:
				num = 13;
			}
		}
		IL_8C:
		return HyperlinksCollectionEditor.b("㰞攠樢猤ࠦᤨਪ", a_);
		IL_C9:
		goto IL_24C;
		IL_CE:
		return HyperlinksCollectionEditor.b("㰞漠戢栤戦ᘨ", a_);
		IL_FC:
		return HyperlinksCollectionEditor.b("㰞漠瘢椤欦ࠨ", a_);
		IL_10B:
		return HyperlinksCollectionEditor.b("㰞漠瘢栤ئ", a_);
		IL_176:
		return HyperlinksCollectionEditor.b("㰞猠昢挤ئ", a_);
		IL_185:
		return HyperlinksCollectionEditor.b("㰞漠ఢ搤", a_);
		IL_22E:
		goto IL_24C;
		IL_230:
		return HyperlinksCollectionEditor.b("㰞眠戢椤爦氨ਪ", a_);
		IL_24A:
		IL_24C:
		return HyperlinksCollectionEditor.b("㰞漠瘢椤欦ࠨ", a_);
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00025FDC File Offset: 0x00024FDC
	public static byte ᜅ(string A_0)
	{
		int a_ = 18;
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return 7;
			case 1:
				if (A_0 == HyperlinksCollectionEditor.b("ഭ戯眱爳᜵", a_))
				{
					num = 8;
					continue;
				}
				num = 6;
				continue;
			case 2:
				return 29;
			case 3:
				if (A_0 == HyperlinksCollectionEditor.b("ഭ琯笱戳ᤵ࠷ᬹ", a_))
				{
					num = 0;
					continue;
				}
				num = 13;
				continue;
			case 4:
				return 36;
			case 5:
				return 0;
			case 6:
				if (A_0 == HyperlinksCollectionEditor.b("ഭ縯猱礳猵ܷ", a_))
				{
					num = 2;
					continue;
				}
				num = 10;
				continue;
			case 7:
				return 15;
			case 8:
				return 23;
			case 9:
				return 42;
			case 10:
				if (A_0 == HyperlinksCollectionEditor.b("ഭ縯朱礳᜵", a_))
				{
					num = 4;
					continue;
				}
				goto IL_B4;
			case 12:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B4;
				default:
					if (false)
					{
					}
					if (A_0 == HyperlinksCollectionEditor.b("ഭ縯ᴱ申", a_))
					{
						num = 9;
						continue;
					}
					goto IL_1DB;
				}
				break;
			case 13:
				if (A_0 == HyperlinksCollectionEditor.b("ഭ是猱砳挵紷ᬹ", a_))
				{
					num = 7;
					continue;
				}
				num = 1;
				continue;
			}
			if (A_0 == HyperlinksCollectionEditor.b("ഭ縯朱砳稵ᤷ", a_))
			{
				num = 5;
				continue;
			}
			num = 3;
			continue;
			IL_B4:
			num = 12;
		}
		return 0;
		IL_1DB:
		throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("漭䈯唱䜳椵笷弹倻刽ጿ㙁㙃", a_)), A_0));
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x000261E8 File Offset: 0x000251E8
	public unsafe static bool ᜀ(double A_0, ref int A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			double num3;
			int* ptr2;
			for (;;)
			{
				num = 0;
				int num2 = 0;
				for (;;)
				{
					IL_10:
					int num4;
					switch (num2)
					{
					case 0:
						goto IL_152;
					case 1:
						goto IL_152;
					case 2:
						num2 = 9;
						continue;
					case 3:
						return false;
					case 4:
						goto IL_DF;
					case 5:
						if ((double)((int)num3) == num3)
						{
							num2 = 2;
							continue;
						}
						goto IL_E4;
					case 6:
					{
						int* ptr;
						if (*ptr == 0)
						{
							num2 = 7;
							continue;
						}
						goto IL_170;
					}
					case 7:
						num2 = 10;
						continue;
					case 8:
						goto IL_14D;
					case 9:
						if (num3 <= (double)num4)
						{
							num2 = 12;
							continue;
						}
						goto IL_E4;
					case 10:
						while ((*ptr2 & 3) == 0)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num2 = 4;
								goto IL_10;
							}
						}
						goto IL_170;
					case 11:
						if (num3 >= (double)(-(double)num4 - 1))
						{
							num2 = 8;
							continue;
						}
						goto IL_E4;
					case 12:
						if (true)
						{
						}
						num2 = 11;
						continue;
					case 13:
					{
						if (num > 1)
						{
							num2 = 3;
							continue;
						}
						num3 = A_0 * (double)(1 + 99 * num);
						int* ptr = (int*)(&num3);
						ptr2 = ptr;
						ptr2++;
						num2 = 6;
						continue;
					}
					}
					break;
					IL_E4:
					num++;
					num2 = 1;
					continue;
					IL_152:
					num2 = 13;
					continue;
					IL_170:
					num4 = 536870911;
					num2 = 5;
				}
			}
			IL_DF:
			A_1 = *ptr2 + num;
			return true;
			IL_14D:
			A_1 = ((int)Math.Round(num3) << 2) + num + 2;
			return true;
		}
		}
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x0002639C File Offset: 0x0002539C
	public static int ᜀ(bool A_0, byte[] A_1, int A_2, bool A_3, int A_4)
	{
		switch (0)
		{
		default:
		{
			int num;
			uint num2;
			uint num4;
			uint num5;
			byte b2;
			for (;;)
			{
				num = A_2;
				num2 = 0U;
				int num3 = 12;
				for (;;)
				{
					byte b;
					switch (num3)
					{
					case 0:
						num4 = (uint)sprᮌ.ᜀ(A_1, num);
						num += 4;
						num3 = 2;
						continue;
					case 1:
						goto IL_8E;
					case 2:
						goto IL_118;
					case 3:
						goto IL_12E;
					case 4:
						if ((b & 4) == 4)
						{
							num3 = 0;
							continue;
						}
						goto IL_187;
					case 5:
						goto IL_8E;
					case 6:
						if (A_0)
						{
							num3 = 7;
							continue;
						}
						num2 = (uint)A_1[num];
						num++;
						num3 = 10;
						continue;
					case 7:
						num2 = (uint)sprᮌ.ᜁ(A_1, num);
						num += 2;
						num3 = 1;
						continue;
					case 8:
						num5 = (uint)sprᮌ.ᜁ(A_1, num);
						num += 2;
						num3 = 3;
						continue;
					case 9:
						num2 = (uint)A_4;
						num3 = 5;
						continue;
					case 10:
						goto IL_8E;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
							if (false)
							{
							}
							if ((b & 8) == 8)
							{
								num3 = 8;
								continue;
							}
							goto IL_12E;
						}
						break;
					case 12:
						if (A_3)
						{
							num3 = 9;
							continue;
						}
						num3 = 6;
						continue;
					}
					break;
					IL_9E:
					if (true)
					{
					}
					num3 = 11;
					continue;
					IL_8E:
					b = A_1[num];
					num++;
					b2 = (b & 1);
					num5 = 0U;
					goto IL_9E;
					IL_12E:
					num4 = 0U;
					num3 = 4;
				}
			}
			IL_118:
			IL_187:
			return (int)((long)(num - A_2) + (long)((ulong)((ulong)num2 << (int)b2)) + (long)((ulong)((ulong)num5 << 2)) + (long)((ulong)num4));
		}
		}
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00026548 File Offset: 0x00025548
	public static void ᜀ(ushort A_0, ushort A_1, ushort A_2, int A_3, sprḗ A_4)
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
		spr\u1CC5 a_;
		a_.ᜀ = (ushort)((int)A_1 + ((int)A_2 << 4));
		a_.ᜁ = A_0;
		a_.ᜂ = (uint)A_3;
		byte[] array = spr\u1CC5.ᜀ(a_);
		A_4.ᜁ(array, array.Length);
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x000265B4 File Offset: 0x000255B4
	public static void ᜀ(ushort A_0, ushort A_1, ushort A_2, int A_3, byte[] A_4, ref int A_5)
	{
		int a_ = 14;
		if (true)
		{
		}
		spr\u1CC5 a_2;
		a_2.ᜀ = (ushort)((int)A_1 + ((int)A_2 << 4));
		a_2.ᜁ = A_0;
		a_2.ᜂ = (uint)A_3;
		if (A_4.Length >= spr\u1CC5.ᜀ())
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
				byte[] array = spr\u1CC5.ᜀ(a_2);
				Array.Copy(array, 0, A_4, A_5, array.Length);
				A_5 += spr\u1CC5.ᜀ();
				return;
			}
			}
		}
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("欩師䤭䌯洱瘳伵䰷弹紻䰽㈿⍁㵃ᕅⅇぉ⥋", a_)));
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00026664 File Offset: 0x00025664
	internal static spr\u2320 ᜀ(sprᲤ A_0, sprḗ A_1, spr\u1DCF A_2)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			spr\u2320 spr_u;
			for (;;)
			{
				byte[] array = new byte[(int)A_2.ᜁ];
				int num = 50;
				for (;;)
				{
					spr\u1DCF a_2;
					byte[] array2;
					switch (num)
					{
					case 0:
						goto IL_846;
					case 1:
						num = 36;
						continue;
					case 2:
						num = 57;
						continue;
					case 3:
						num = 51;
						continue;
					case 4:
					{
						ushort ᜀ;
						if (ᜀ != 6)
						{
							num = 69;
							continue;
						}
						spr_u = new spr᠗(A_0, A_2.ᜀ, A_2.ᜁ, array);
						num = 22;
						continue;
					}
					case 5:
					{
						ushort ᜀ;
						if (ᜀ != 10)
						{
							num = 3;
							continue;
						}
						spr_u = new spr\u1809(A_0, A_2.ᜀ, A_2.ᜁ, array);
						num = 61;
						continue;
					}
					case 6:
						goto IL_846;
					case 7:
						if (!(spr_u is sprẴ))
						{
							num = 32;
							continue;
						}
						goto IL_8B4;
					case 8:
						goto IL_420;
					case 9:
					{
						ushort ᜀ;
						if (ᜀ != 1054)
						{
							num = 25;
							continue;
						}
						spr_u = new sprᵾ(A_0, A_2.ᜀ, A_2.ᜁ, array);
						num = 65;
						continue;
					}
					case 10:
					{
						ushort ᜀ;
						if (ᜀ <= 190)
						{
							num = 30;
							continue;
						}
						num = 42;
						continue;
					}
					case 11:
						num = 46;
						continue;
					case 12:
						goto IL_846;
					case 13:
					{
						ushort ᜀ;
						switch (ᜀ)
						{
						case 252:
							spr_u = new spr\u2422(A_0, A_2.ᜀ, A_2.ᜁ, array);
							num = 28;
							continue;
						case 253:
							spr_u = new spr\u2103(A_0, A_2.ᜀ, A_2.ᜁ, array);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8D4;
							default:
								if (false)
								{
								}
								num = 15;
								continue;
							}
							break;
						default:
							num = 24;
							continue;
						}
						break;
					}
					case 14:
						num = 66;
						continue;
					case 15:
						goto IL_846;
					case 16:
						return spr_u;
					case 17:
						goto IL_846;
					case 18:
					{
						ushort ᜀ;
						if (ᜀ <= 24)
						{
							num = 38;
							continue;
						}
						num = 62;
						continue;
					}
					case 19:
						goto IL_846;
					case 20:
						goto IL_846;
					case 21:
						if (a_2.ᜀ == 60)
						{
							num = 35;
							continue;
						}
						num = 39;
						continue;
					case 22:
						goto IL_846;
					case 23:
					{
						ushort ᜀ;
						if (ᜀ != 1212)
						{
							num = 14;
							continue;
						}
						spr_u = new sprẴ(A_0, A_2.ᜀ, A_2.ᜁ, array);
						num = 58;
						continue;
					}
					case 24:
						num = 60;
						continue;
					case 25:
						num = 47;
						continue;
					case 26:
						goto IL_39E;
					case 27:
						num = 13;
						continue;
					case 28:
						goto IL_846;
					case 29:
						goto IL_846;
					case 30:
						num = 18;
						continue;
					case 31:
						if (!(spr_u is spr᠗))
						{
							num = 40;
							continue;
						}
						goto IL_8B4;
					case 32:
						goto IL_728;
					case 33:
						num = 9;
						continue;
					case 34:
						spr_u = new spr\u1F46(A_0, A_2.ᜀ, A_2.ᜁ, array);
						num = 43;
						continue;
					case 35:
						spr_u.ᜀ(sprᮌ.ᜀ(A_0, A_1, a_2) as spr\u20E7);
						num = 8;
						continue;
					case 36:
						goto IL_1D9;
					case 37:
						goto IL_1D9;
					case 38:
						if (true)
						{
						}
						num = 4;
						continue;
					case 39:
						if (a_2.ᜀ == 519)
						{
							num = 59;
							continue;
						}
						goto IL_8B4;
					case 40:
						num = 7;
						continue;
					case 41:
						goto IL_846;
					case 42:
					{
						ushort ᜀ;
						if (ᜀ <= 519)
						{
							num = 48;
							continue;
						}
						num = 63;
						continue;
					}
					case 43:
						goto IL_846;
					case 44:
						goto IL_846;
					case 45:
						if (A_1.ᜀ(array2, array2.Length) == array2.Length)
						{
							num = 49;
							continue;
						}
						return spr_u;
					case 46:
					{
						ushort ᜀ;
						if (ᜀ != 638)
						{
							num = 33;
							continue;
						}
						spr_u = new spr\u1DC2(A_0, A_2.ᜀ, A_2.ᜁ, array);
						num = 29;
						continue;
					}
					case 47:
						goto IL_1D9;
					case 48:
						num = 52;
						continue;
					case 49:
						spr\u1DCF.ᜀ(array2, ref a_2);
						num = 21;
						continue;
					case 50:
					{
						try
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_36B;
								case 2:
									goto IL_394;
								}
								if (A_1.ᜀ(array, (int)A_2.ᜁ) != (int)A_2.ᜁ)
								{
									num = 0;
								}
								else
								{
									num = 2;
								}
							}
							IL_36B:
							throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("吜焞圠䈢䤤並䴨搪崬䨮䌰刲䄴帶嘸唺戼稾㥀⁂⁄⭆ᭈ⹊⹌⁎⍐㝒", a_)));
							IL_394:
							goto IL_6D1;
						}
						catch
						{
							array = null;
							throw;
						}
						goto IL_39E;
						IL_6D1:
						spr_u = null;
						ushort ᜀ = A_2.ᜀ;
						num = 10;
						continue;
					}
					case 51:
					{
						ushort ᜀ;
						if (ᜀ != 24)
						{
							num = 2;
							continue;
						}
						spr_u = new sprṱ(A_0, A_2.ᜀ, A_2.ᜁ, array);
						num = 17;
						continue;
					}
					case 52:
					{
						ushort ᜀ;
						if (ᜀ != 224)
						{
							num = 27;
							continue;
						}
						spr_u = new spr\u1885(A_0, A_2.ᜀ, A_2.ᜁ, array);
						num = 12;
						continue;
					}
					case 53:
						num = 37;
						continue;
					case 54:
						num = 67;
						continue;
					case 55:
						goto IL_846;
					case 56:
						goto IL_846;
					case 57:
						goto IL_1D9;
					case 58:
						goto IL_846;
					case 59:
						num = 31;
						continue;
					case 60:
					{
						ushort ᜀ;
						switch (ᜀ)
						{
						case 513:
							spr_u = new sprᜀ(A_0, A_2.ᜀ, A_2.ᜁ, array);
							num = 19;
							continue;
						case 514:
						case 516:
						case 518:
							goto IL_1D9;
						case 515:
							spr_u = new spr\u2416(A_0, A_2.ᜀ, A_2.ᜁ, array);
							num = 56;
							continue;
						case 517:
							spr_u = new sprᮘ(A_0, A_2.ᜀ, A_2.ᜁ, array);
							num = 44;
							continue;
						case 519:
							spr_u = new sprᮕ(A_0, A_2.ᜀ, A_2.ᜁ, array);
							num = 0;
							continue;
						default:
							num = 1;
							continue;
						}
						break;
					}
					case 61:
						goto IL_846;
					case 62:
					{
						ushort ᜀ;
						if (ᜀ != 60)
						{
							num = 26;
							continue;
						}
						spr_u = new spr\u20E7(A_0, A_2.ᜀ, A_2.ᜁ, array);
						num = 55;
						continue;
					}
					case 63:
					{
						ushort ᜀ;
						if (ᜀ <= 1054)
						{
							num = 11;
							continue;
						}
						num = 23;
						continue;
					}
					case 64:
					{
						ushort ᜀ;
						if (ᜀ != 133)
						{
							num = 54;
							continue;
						}
						spr_u = new spr᭒(A_0, A_2.ᜀ, A_2.ᜁ, array);
						num = 6;
						continue;
					}
					case 65:
						goto IL_846;
					case 66:
					{
						ushort ᜀ;
						if (ᜀ == 2057)
						{
							num = 34;
							continue;
						}
						goto IL_1D9;
					}
					case 67:
					{
						ushort ᜀ;
						switch (ᜀ)
						{
						case 189:
							spr_u = new spr\u222A(A_0, A_2.ᜀ, A_2.ᜁ, array);
							num = 41;
							continue;
						case 190:
							goto IL_8D4;
						default:
							num = 53;
							continue;
						}
						break;
					}
					case 68:
						goto IL_846;
					case 69:
						num = 5;
						continue;
					}
					break;
					IL_1D9:
					spr_u = new spr\u2320(A_0, A_2.ᜀ, A_2.ᜁ, array);
					num = 68;
					continue;
					IL_39E:
					num = 64;
					continue;
					IL_846:
					a_2.ᜀ = 0;
					a_2.ᜁ = 0;
					array2 = new byte[spr\u1DCF.ᜀ()];
					num = 45;
					continue;
					IL_8B4:
					A_1.Seek((long)(-(long)spr\u1DCF.ᜀ()), SeekOrigin.Current);
					num = 16;
					continue;
					IL_8D4:
					spr_u = new sprḐ(A_0, A_2.ᜀ, A_2.ᜁ, array);
					num = 20;
				}
			}
			IL_420:
			return spr_u;
			IL_728:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("吜焞圠䈢䤤並䴨搪崬䨮䌰刲䄴帶嘸唺戼稾㥀⁂⁄⭆ᭈ⹊⹌⁎⍐㝒", a_)));
		}
		}
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00026FD8 File Offset: 0x00025FD8
	public static string ᜀ(CellColor A_0)
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
		return spr\u2009.᠑[(int)A_0].ToString();
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00027024 File Offset: 0x00026024
	public static CellColor ᜄ(string A_0)
	{
		int a_ = 12;
		CellColor result;
		for (;;)
		{
			int num = spr\u2009.᠑.GetLowerBound(0);
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num > spr\u2009.᠑.GetUpperBound(0))
					{
						num2 = 3;
						continue;
					}
					try
					{
						num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 1:
								result = (CellColor)num;
								num2 = 3;
								continue;
							case 2:
								goto IL_CE;
							case 3:
								goto IL_C4;
							}
							if (Convert.ToUInt32(A_0) == spr\u2009.᠑[num])
							{
								num2 = 1;
							}
							else
							{
								num2 = 2;
							}
						}
						IL_C4:
						goto IL_F2;
						IL_CE:
						goto IL_73;
					}
					catch (Exception ex)
					{
						throw new Exception(ex.Message + HyperlinksCollectionEditor.b("┧\u2029琫䈭䌯朱䀳張吷䤹ػнጿ㙁㙃瑅၇♉㽋്㱏⁑硓⁕㥗⡙晛", a_));
					}
					return CellColor.Black;
					IL_73:
					num++;
					num2 = 2;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return CellColor.Black;
					default:
						if (false)
						{
						}
						goto IL_53;
					}
					break;
				case 2:
					goto IL_53;
				case 3:
					return CellColor.Black;
				}
				break;
				IL_53:
				num2 = 0;
			}
		}
		return CellColor.Black;
		IL_F2:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00027148 File Offset: 0x00026148
	internal static Image ᜃ(string A_0)
	{
		Image result;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return result;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		WebRequest webRequest = WebRequest.Create(A_0);
		try
		{
			result = Image.FromStream(webRequest.GetResponse().GetResponseStream());
		}
		catch (Exception)
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x000271B8 File Offset: 0x000261B8
	internal static FileStream ᜂ(string A_0)
	{
		FileStream result;
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
			WebRequest webRequest = WebRequest.Create(A_0);
			try
			{
				if (true)
				{
				}
				result = (FileStream)webRequest.GetResponse().GetResponseStream();
			}
			catch (Exception)
			{
				result = null;
			}
			break;
		}
		}
		return result;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00027228 File Offset: 0x00026228
	internal static bool ᜀ()
	{
		if (true)
		{
		}
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_37;
		}
		if (false)
		{
		}
		if (Environment.UserInteractive)
		{
			return false;
		}
		IL_37:
		return HttpContext.Current != null;
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x0002727C File Offset: 0x0002627C
	internal static bool ᜁ(string A_0)
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
			num = 1;
			break;
		}
		for (;;)
		{
			WebRequest webRequest;
			switch (num)
			{
			case 0:
				try
				{
					Image.FromStream(webRequest.GetResponse().GetResponseStream());
					return true;
				}
				catch (Exception)
				{
					return false;
				}
				goto IL_6D;
			case 2:
				goto IL_6D;
			}
			if (sprᮌ.ᜀ())
			{
				num = 2;
				continue;
			}
			break;
			IL_6D:
			webRequest = WebRequest.Create(A_0);
			if (true)
			{
			}
			num = 0;
		}
		return File.Exists(A_0);
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00027328 File Offset: 0x00026328
	public static CellPictureType ᜀ(string A_0)
	{
		int a_ = 11;
		for (;;)
		{
			string text = Path.GetExtension(A_0).Trim().ToUpper();
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (text == HyperlinksCollectionEditor.b("戦搨洪", a_))
					{
						num = 12;
						continue;
					}
					num = 16;
					continue;
				case 1:
					if (true)
					{
					}
					num = 15;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return CellPictureType.EMF;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				case 3:
					return CellPictureType.DIB;
				case 4:
					num = 9;
					continue;
				case 5:
					if (!(text == HyperlinksCollectionEditor.b("眦木氪", a_)))
					{
						num = 6;
						continue;
					}
					return CellPictureType.PNG;
				case 6:
					num = 17;
					continue;
				case 7:
					return CellPictureType.WMF;
				case 8:
					if (text.Length > 0)
					{
						num = 4;
						continue;
					}
					return CellPictureType.Undefined;
				case 9:
					if (text[0] == '.')
					{
						num = 18;
						continue;
					}
					return CellPictureType.Undefined;
				case 10:
					if (text == HyperlinksCollectionEditor.b("攦搨笪", a_))
					{
						num = 3;
						continue;
					}
					return CellPictureType.Undefined;
				case 11:
					goto IL_E9;
				case 12:
					goto IL_21B;
				case 13:
					if (text == HyperlinksCollectionEditor.b("瀦搨洪", a_))
					{
						num = 7;
						continue;
					}
					num = 0;
					continue;
				case 14:
					goto IL_1BF;
				case 15:
					if (text == HyperlinksCollectionEditor.b("洦礨渪樬", a_))
					{
						num = 11;
						continue;
					}
					num = 5;
					continue;
				case 16:
					if (!(text == HyperlinksCollectionEditor.b("洦礨氪", a_)))
					{
						num = 1;
						continue;
					}
					return CellPictureType.JPEG;
				case 17:
					if (text == HyperlinksCollectionEditor.b("怦怨洪", a_))
					{
						num = 14;
						continue;
					}
					num = 10;
					continue;
				case 18:
					text = text.Remove(0, 1);
					num = 2;
					continue;
				}
				break;
			}
		}
		IL_E9:
		return CellPictureType.JPEG;
		IL_1BF:
		return CellPictureType.PNG;
		IL_21B:
		return CellPictureType.EMF;
	}
}
