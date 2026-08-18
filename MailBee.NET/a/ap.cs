using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using a.i;
using MailBee;

namespace a
{
	// Token: 0x020004FB RID: 1275
	internal class ap
	{
		// Token: 0x06002A82 RID: 10882 RVA: 0x000C929E File Offset: 0x000C829E
		private ap()
		{
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000C92A8 File Offset: 0x000C82A8
		public static string a(string A_0, string A_1)
		{
			int num = 248;
			int num2 = 260;
			if (A_0 == null)
			{
				return A_1;
			}
			if (A_1 == null)
			{
				return A_0;
			}
			if (A_0.Length > num)
			{
				throw new MailBeeIOException(20);
			}
			string result;
			try
			{
				if (A_0.Length + A_1.Length > num2)
				{
					if (A_1.Length > num2)
					{
						A_1 = A_1.Substring(A_1.Length - num2);
					}
					string text = Path.GetFileNameWithoutExtension(A_1);
					string extension = Path.GetExtension(A_1);
					text = string.Format("{0}~1", text.Substring(0, 6));
					A_1 = text + extension;
				}
				result = Path.Combine(A_0, A_1);
			}
			catch (ArgumentException a_)
			{
				throw new MailBeeIOException(20, a_);
			}
			return result;
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000C935C File Offset: 0x000C835C
		public static string g(string A_0)
		{
			byte[] bytes = new byte[]
			{
				34,
				0,
				60,
				0,
				62,
				0,
				124,
				0,
				0,
				0,
				1,
				0,
				2,
				0,
				3,
				0,
				4,
				0,
				5,
				0,
				6,
				0,
				7,
				0,
				8,
				0,
				9,
				0,
				10,
				0,
				11,
				0,
				12,
				0,
				13,
				0,
				14,
				0,
				15,
				0,
				16,
				0,
				17,
				0,
				18,
				0,
				19,
				0,
				20,
				0,
				21,
				0,
				22,
				0,
				23,
				0,
				24,
				0,
				25,
				0,
				26,
				0,
				27,
				0,
				28,
				0,
				29,
				0,
				30,
				0,
				31,
				0
			};
			foreach (char oldChar in Encoding.Unicode.GetChars(bytes))
			{
				A_0 = A_0.Replace(oldChar, '_');
			}
			return A_0;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000C93A8 File Offset: 0x000C83A8
		public static string f(string A_0)
		{
			byte[] bytes = new byte[]
			{
				34,
				0,
				60,
				0,
				62,
				0,
				124,
				0,
				0,
				0,
				1,
				0,
				2,
				0,
				3,
				0,
				4,
				0,
				5,
				0,
				6,
				0,
				7,
				0,
				8,
				0,
				9,
				0,
				10,
				0,
				11,
				0,
				12,
				0,
				13,
				0,
				14,
				0,
				15,
				0,
				16,
				0,
				17,
				0,
				18,
				0,
				19,
				0,
				20,
				0,
				21,
				0,
				22,
				0,
				23,
				0,
				24,
				0,
				25,
				0,
				26,
				0,
				27,
				0,
				28,
				0,
				29,
				0,
				30,
				0,
				31,
				0,
				58,
				0,
				42,
				0,
				63,
				0,
				92,
				0,
				47,
				0
			};
			foreach (char oldChar in Encoding.Unicode.GetChars(bytes))
			{
				A_0 = A_0.Replace(oldChar, '_');
			}
			bool flag = false;
			for (int j = 0; j < A_0.Length; j++)
			{
				if (char.IsSurrogate(A_0[j]))
				{
					if (flag)
					{
						A_0 = A_0.Remove(j, 1);
						flag = false;
					}
					else
					{
						A_0 = A_0.Replace(A_0[j], '_');
						flag = true;
					}
				}
				else if (flag)
				{
					flag = false;
				}
			}
			return A_0;
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000C9447 File Offset: 0x000C8447
		public static bool b(string A_0, byte[] A_1, byte[] A_2)
		{
			return ap.b(A_0, A_1, 0, A_1.Length, A_2);
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000C9458 File Offset: 0x000C8458
		public static bool b(string A_0, byte[] A_1, int A_2, int A_3, byte[] A_4)
		{
			A_0 = ap.g(A_0);
			try
			{
				using (FileStream fileStream = new FileStream(A_0, FileMode.Create))
				{
					if (A_4 != null)
					{
						fileStream.Write(A_4, 0, A_4.Length);
					}
					fileStream.Write(A_1, A_2, A_3);
				}
			}
			catch (IOException a_)
			{
				throw new MailBeeIOException(30, a_);
			}
			catch (NotSupportedException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (ArgumentException a_3)
			{
				throw new MailBeeIOException(20, a_3);
			}
			return true;
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000C94F0 File Offset: 0x000C84F0
		public static bool b(string A_0, byte[] A_1, int A_2, int A_3)
		{
			A_0 = ap.g(A_0);
			try
			{
				using (FileStream fileStream = new FileStream(A_0, FileMode.Create))
				{
					byte[] array = new byte[]
					{
						13,
						10,
						46
					};
					byte[] array2 = new byte[]
					{
						13,
						10,
						46,
						46
					};
					int num = A_2;
					if (A_1[num] == 46)
					{
						fileStream.Write(new byte[]
						{
							46,
							46
						}, 0, 2);
						num++;
					}
					int num2;
					do
					{
						num2 = w.b(A_1, num, A_2 + A_3 - num, array);
						if (num2 > 0)
						{
							fileStream.Write(A_1, num, num2 - num);
							fileStream.Write(array2, 0, array2.Length);
						}
						else
						{
							fileStream.Write(A_1, num, A_2 + A_3 - num);
						}
						num = num2 + array.Length;
					}
					while (num2 > 0);
				}
			}
			catch (IOException a_)
			{
				throw new MailBeeIOException(30, a_);
			}
			catch (NotSupportedException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (ArgumentException a_3)
			{
				throw new MailBeeIOException(20, a_3);
			}
			catch (IndexOutOfRangeException a_4)
			{
				throw new MailBeeIOException(20, a_4);
			}
			return true;
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000C9620 File Offset: 0x000C8620
		public static byte[] e(string A_0)
		{
			return ap.b(A_0, false, 0).c();
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000C9630 File Offset: 0x000C8630
		public static ao b(string A_0, bool A_1, int A_2)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			ao result;
			try
			{
				int i = (int)new FileInfo(A_0).Length;
				int num = 0;
				byte[] array = new byte[i + A_2];
				using (FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					while (i > 0)
					{
						int num2 = fileStream.Read(array, num, i);
						if (num2 == 0)
						{
							break;
						}
						num += num2;
						i -= num2;
					}
				}
				ao ao = new ao(array, num);
				if (A_1)
				{
					result = global::a.i.k.a(ao);
				}
				else
				{
					result = ao;
				}
			}
			catch (ArgumentException a_)
			{
				throw new MailBeeIOException(20, a_);
			}
			catch (NotSupportedException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (IOException a_3)
			{
				throw new MailBeeIOException(30, a_3);
			}
			return result;
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000C9710 File Offset: 0x000C8710
		public static ao d(string A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			FileStream fileStream = null;
			ao result;
			try
			{
				fileStream = File.OpenRead(A_0);
				result = ap.g(fileStream);
			}
			catch (UnauthorizedAccessException a_)
			{
				throw new MailBeeIOException(32, a_);
			}
			catch (ArgumentException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (NotSupportedException a_3)
			{
				throw new MailBeeIOException(20, a_3);
			}
			catch (IOException a_4)
			{
				throw new MailBeeIOException(30, a_4);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000C97B4 File Offset: 0x000C87B4
		public static byte[] h(Stream A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!A_0.CanSeek || !A_0.CanRead)
			{
				throw new MailBeeInvalidArgumentException(40);
			}
			byte[] array = new byte[0];
			byte[] array2 = new byte[(A_0.Length - A_0.Position < 65536L) ? ((int)(A_0.Length - A_0.Position)) : 65536];
			while (A_0.Position < A_0.Length)
			{
				A_0.Read(array2, 0, array2.Length);
				byte[] array3 = new byte[array.Length + array2.Length];
				array.CopyTo(array3, 0);
				Buffer.BlockCopy(array2, 0, array3, array.Length, array2.Length);
				array = array3;
				int num;
				if ((num = global::a.i.k.a(array, 0, array.Length)) != -1)
				{
					array3 = new byte[num];
					Buffer.BlockCopy(array, 0, array3, 0, num);
					array = array3;
					break;
				}
				if (A_0.Length - A_0.Position < (long)array2.Length)
				{
					array2 = new byte[A_0.Length - A_0.Position];
				}
			}
			return array;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000C98B0 File Offset: 0x000C88B0
		public static ao g(Stream A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!A_0.CanSeek || !A_0.CanRead)
			{
				throw new MailBeeInvalidArgumentException(40);
			}
			byte[] array = new byte[4096];
			int num = 0;
			byte[] array2 = new byte[(A_0.Length - A_0.Position < 65536L) ? ((int)(A_0.Length - A_0.Position)) : 65536];
			while (A_0.Position < A_0.Length)
			{
				A_0.Read(array2, 0, array2.Length);
				byte[] array3 = new byte[array.Length + array2.Length];
				array.CopyTo(array3, 0);
				Buffer.BlockCopy(array2, 0, array3, num, array2.Length);
				array = array3;
				num += array2.Length;
				int num2;
				if ((num2 = global::a.i.k.a(array, 0, num)) != -1)
				{
					num = num2;
					break;
				}
				if (A_0.Length - A_0.Position < (long)array2.Length)
				{
					array2 = new byte[A_0.Length - A_0.Position];
				}
			}
			return new ao(array, num);
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000C99A8 File Offset: 0x000C89A8
		public static byte[] f(Stream A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!A_0.CanSeek || !A_0.CanRead)
			{
				throw new MailBeeInvalidArgumentException(40);
			}
			byte[] array = new byte[0];
			byte[] array2 = new byte[(A_0.Length - A_0.Position < 65536L) ? ((int)(A_0.Length - A_0.Position)) : 65536];
			while (A_0.Position < A_0.Length)
			{
				A_0.Read(array2, 0, array2.Length);
				byte[] array3 = new byte[array.Length + array2.Length];
				array.CopyTo(array3, 0);
				Buffer.BlockCopy(array2, 0, array3, array.Length, array2.Length);
				array = array3;
				if (A_0.Length - A_0.Position > 0L && A_0.Length - A_0.Position < (long)array2.Length)
				{
					array2 = new byte[A_0.Length - A_0.Position];
				}
			}
			return array;
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000C9A8C File Offset: 0x000C8A8C
		public static ao e(Stream A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!A_0.CanSeek || !A_0.CanRead)
			{
				throw new MailBeeInvalidArgumentException(40);
			}
			byte[] array = new byte[4096];
			int num = 0;
			byte[] array2 = new byte[(A_0.Length - A_0.Position < 65536L) ? ((int)(A_0.Length - A_0.Position)) : 65536];
			while (A_0.Position < A_0.Length)
			{
				A_0.Read(array2, 0, array2.Length);
				byte[] array3 = new byte[array.Length + array2.Length];
				array.CopyTo(array3, 0);
				Buffer.BlockCopy(array2, 0, array3, num, array2.Length);
				array = array3;
				num += array2.Length;
				if (A_0.Length - A_0.Position < (long)array2.Length)
				{
					array2 = new byte[A_0.Length - A_0.Position];
				}
			}
			return new ao(array, num);
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000C9B6C File Offset: 0x000C8B6C
		public static void c(string A_0)
		{
			try
			{
				File.Delete(A_0);
			}
			catch (ArgumentException a_)
			{
				throw new MailBeeIOException(20, a_);
			}
			catch (NotSupportedException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (UnauthorizedAccessException a_3)
			{
				throw new MailBeeIOException(32, a_3);
			}
			catch (IOException a_4)
			{
				throw new MailBeeIOException(30, a_4);
			}
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000C9BE0 File Offset: 0x000C8BE0
		public static Task<bool> a(string A_0, byte[] A_1, byte[] A_2)
		{
			return ap.a(A_0, A_1, 0, A_1.Length, A_2);
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000C9BF0 File Offset: 0x000C8BF0
		public static Task<bool> a(string A_0, byte[] A_1, int A_2, int A_3, byte[] A_4)
		{
			ap.c c;
			c.c = A_0;
			c.f = A_1;
			c.g = A_2;
			c.h = A_3;
			c.d = A_4;
			c.b = AsyncTaskMethodBuilder<bool>.Create();
			c.a = -1;
			AsyncTaskMethodBuilder<bool> b = c.b;
			b.Start<ap.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000C9C58 File Offset: 0x000C8C58
		public static Task<bool> a(string A_0, byte[] A_1, int A_2, int A_3)
		{
			ap.g g;
			g.c = A_0;
			g.e = A_1;
			g.d = A_2;
			g.g = A_3;
			g.b = AsyncTaskMethodBuilder<bool>.Create();
			g.a = -1;
			AsyncTaskMethodBuilder<bool> b = g.b;
			b.Start<ap.g>(ref g);
			return g.b.Task;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000C9CB8 File Offset: 0x000C8CB8
		public static Task<byte[]> b(string A_0)
		{
			ap.h h;
			h.c = A_0;
			h.b = AsyncTaskMethodBuilder<byte[]>.Create();
			h.a = -1;
			AsyncTaskMethodBuilder<byte[]> b = h.b;
			b.Start<ap.h>(ref h);
			return h.b.Task;
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000C9D00 File Offset: 0x000C8D00
		public static Task<ao> a(string A_0, bool A_1, int A_2)
		{
			ap.a a;
			a.c = A_0;
			a.i = A_1;
			a.d = A_2;
			a.b = AsyncTaskMethodBuilder<ao>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<ao> b = a.b;
			b.Start<ap.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000C9D58 File Offset: 0x000C8D58
		public static Task<ao> a(string A_0)
		{
			ap.e e;
			e.c = A_0;
			e.b = AsyncTaskMethodBuilder<ao>.Create();
			e.a = -1;
			AsyncTaskMethodBuilder<ao> b = e.b;
			b.Start<ap.e>(ref e);
			return e.b.Task;
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000C9DA0 File Offset: 0x000C8DA0
		public static Task<byte[]> d(Stream A_0)
		{
			ap.d d;
			d.c = A_0;
			d.b = AsyncTaskMethodBuilder<byte[]>.Create();
			d.a = -1;
			AsyncTaskMethodBuilder<byte[]> b = d.b;
			b.Start<ap.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000C9DE8 File Offset: 0x000C8DE8
		public static Task<ao> c(Stream A_0)
		{
			ap.f f;
			f.c = A_0;
			f.b = AsyncTaskMethodBuilder<ao>.Create();
			f.a = -1;
			AsyncTaskMethodBuilder<ao> b = f.b;
			b.Start<ap.f>(ref f);
			return f.b.Task;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000C9E30 File Offset: 0x000C8E30
		public static Task<byte[]> b(Stream A_0)
		{
			ap.b b;
			b.c = A_0;
			b.b = AsyncTaskMethodBuilder<byte[]>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<byte[]> b2 = b.b;
			b2.Start<ap.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000C9E78 File Offset: 0x000C8E78
		public static Task<ao> a(Stream A_0)
		{
			ap.i i;
			i.c = A_0;
			i.b = AsyncTaskMethodBuilder<ao>.Create();
			i.a = -1;
			AsyncTaskMethodBuilder<ao> b = i.b;
			b.Start<ap.i>(ref i);
			return i.b.Task;
		}
	}
}
