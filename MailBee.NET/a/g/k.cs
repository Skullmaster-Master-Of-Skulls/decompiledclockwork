using System;
using System.Collections.Specialized;
using System.Text;
using MailBee;
using MailBee.DnsMX;

namespace a.g
{
	// Token: 0x020003F4 RID: 1012
	internal class k
	{
		// Token: 0x060023CD RID: 9165 RVA: 0x000965E8 File Offset: 0x000955E8
		private k()
		{
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000965F0 File Offset: 0x000955F0
		public static byte[] a(short A_0, string A_1, Encoding A_2, h A_3, int A_4, out int A_5)
		{
			if (A_1 == null || A_2 == null)
			{
				throw new ArgumentNullException();
			}
			if (A_1.Length > 255)
			{
				throw new MailBeeInvalidArgumentException(201);
			}
			byte[] array = new byte[512];
			array[0] = (byte)(A_0 / 256);
			array[1] = (byte)(A_0 % 256);
			array[2] = 1;
			array[3] = 0;
			array[4] = 0;
			array[5] = 1;
			array[6] = 0;
			array[7] = 0;
			array[8] = 0;
			array[9] = 0;
			array[10] = 0;
			array[11] = 0;
			string[] array2 = A_1.Split(new char[]
			{
				'.'
			});
			int num = 12;
			foreach (string text in array2)
			{
				if (text.Length > 63)
				{
					throw new MailBeeInvalidArgumentException(201);
				}
				byte[] bytes = A_2.GetBytes(text);
				array[num++] = (byte)bytes.Length;
				if (bytes.Length + num > array.Length)
				{
					throw new MailBeeInvalidArgumentException(201);
				}
				bytes.CopyTo(array, num);
				num += bytes.Length;
			}
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = (byte)A_3;
			array[num++] = 0;
			array[num++] = (byte)A_4;
			A_5 = num;
			return array;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x00096718 File Offset: 0x00095718
		public static f a(byte[] A_0, Encoding A_1, short A_2, bool A_3)
		{
			if (A_0 == null || A_1 == null)
			{
				throw new ArgumentNullException();
			}
			if (A_0.Length < 12)
			{
				throw new a(200, A_0);
			}
			short num = (short)(A_0[0] >> 8 | (int)A_0[1]);
			if (num != A_2)
			{
				throw new t(210, A_2, num);
			}
			int num2 = A_0[2] >> 3 & 15;
			DnsReplyCode a_ = (DnsReplyCode)(A_0[3] & 15);
			switch (a_)
			{
			case DnsReplyCode.NoError:
			{
				f f = new f();
				f.a(((A_0[3] & 128) == 128) ? b.b : b.c);
				int num3 = (int)A_0[4] << 8 | (int)A_0[5];
				int num4 = (int)A_0[6] << 8 | (int)A_0[7];
				byte b = A_0[8];
				byte b2 = A_0[9];
				byte b3 = A_0[10];
				byte b4 = A_0[11];
				int i = 12;
				for (int j = 0; j < num3; j++)
				{
					k.a(A_0, A_1, ref i);
					i += 4;
				}
				for (int k = 0; k < num4; k++)
				{
					k.a(A_0, A_1, ref i);
					h h = (h)((int)A_0[i] << 8 | (int)A_0[i + 1]);
					byte b5 = A_0[i + 2];
					byte b6 = A_0[i + 3];
					byte b7 = A_0[i + 4];
					byte b8 = A_0[i + 5];
					byte b9 = A_0[i + 6];
					byte b10 = A_0[i + 7];
					int num5 = (int)A_0[i + 8] << 8 | (int)A_0[i + 9];
					i += 10;
					if (A_0.Length < i + num5)
					{
						throw new a(123, A_0);
					}
					h h2 = h;
					m a_2;
					if (h2 != h.a)
					{
						if (h2 != h.d)
						{
							switch (h2)
							{
							case h.l:
								if (num5 < 2)
								{
									throw new a(123, A_0);
								}
								a_2 = new c(k.a(A_0, A_1, ref i));
								goto IL_362;
							case h.o:
							{
								if (num5 < 4)
								{
									throw new a(123, A_0);
								}
								int a_3 = (int)((short)((int)A_0[i] << 8 | (int)A_0[i + 1]));
								i += 2;
								string a_4 = k.a(A_0, A_1, ref i);
								a_2 = new q(a_3, a_4);
								goto IL_362;
							}
							case h.p:
							{
								int num6 = i + num5;
								StringCollection stringCollection = new StringCollection();
								int num7 = i;
								while (i < num6)
								{
									string value = null;
									try
									{
										value = A_1.GetString(A_0, i + 1, (int)A_0[i]);
									}
									catch
									{
										throw new a(123, A_0);
									}
									stringCollection.Add(value);
									i += (int)(A_0[i] + 1);
								}
								string[] array = new string[stringCollection.Count];
								stringCollection.CopyTo(array, 0);
								a_2 = new l(array);
								i = num7 + num5;
								goto IL_362;
							}
							}
							a_2 = null;
						}
						else
						{
							if (num5 < 2)
							{
								throw new a(123, A_0);
							}
							a_2 = new n(k.a(A_0, A_1, ref i));
						}
					}
					else
					{
						if (num5 < 4)
						{
							throw new a(123, A_0);
						}
						a_2 = new r(string.Concat(new string[]
						{
							A_0[i].ToString(),
							".",
							A_0[i + 1].ToString(),
							".",
							A_0[i + 2].ToString(),
							".",
							A_0[i + 3].ToString()
						}));
						i += num5;
					}
					IL_362:
					f.a(a_2);
				}
				return f;
			}
			case DnsReplyCode.FormatError:
				throw new i(220, num, A_0, a_);
			case DnsReplyCode.ServerFailure:
				throw new i(221, num, A_0, a_);
			case DnsReplyCode.NameError:
				if (A_3)
				{
					throw new g(222, num, A_0, a_);
				}
				return f.a();
			case DnsReplyCode.NotImplemented:
				throw new i(223, num, A_0, a_);
			case DnsReplyCode.Refused:
				throw new i(224, num, A_0, a_);
			default:
				throw new i(225, num, A_0, a_);
			}
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00096AB0 File Offset: 0x00095AB0
		private static string a(byte[] A_0, Encoding A_1, ref int A_2)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			while (A_0.Length > A_2)
			{
				int num = A_2;
				A_2 = num + 1;
				byte b = A_0[num];
				if (b != 0)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append('.');
					}
					if ((b & 192) != 192)
					{
						stringBuilder.Append(A_1.GetString(A_0, A_2, (int)b));
						A_2 += (int)b;
						continue;
					}
					int num2 = (int)(b & 63) << 8;
					num = A_2;
					A_2 = num + 1;
					int num3 = num2 | (int)A_0[num];
					stringBuilder.Append(k.a(A_0, A_1, ref num3));
				}
				return stringBuilder.ToString();
			}
			throw new a(123, A_0);
		}
	}
}
