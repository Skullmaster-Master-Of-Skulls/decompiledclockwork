using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000252 RID: 594
	internal class b8
	{
		// Token: 0x0600146C RID: 5228 RVA: 0x0005F17F File Offset: 0x0005E17F
		public static ba a(FileInfo A_0, ByteToStringConversionHandler A_1, bool A_2, Encoding A_3)
		{
			return b8.a(new FileStream(A_0.FullName, FileMode.Open, FileAccess.Read), true, A_1, A_2, A_3);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0005F197 File Offset: 0x0005E197
		public static ba a(string A_0, ByteToStringConversionHandler A_1, bool A_2, Encoding A_3)
		{
			return b8.a(new FileStream(A_0, FileMode.Open, FileAccess.Read), true, A_1, A_2, A_3);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0005F1AA File Offset: 0x0005E1AA
		public static ba a(Stream A_0, ByteToStringConversionHandler A_1, bool A_2, Encoding A_3)
		{
			return b8.a(A_0, true, A_1, A_2, A_3);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0005F1B8 File Offset: 0x0005E1B8
		public static ba a(Stream A_0, bool A_1, ByteToStringConversionHandler A_2, bool A_3, Encoding A_4)
		{
			ba ba = null;
			int num = 1;
			try
			{
				ig a_ = new POIFSFileSystem(A_0).Root;
				ba = new ba();
				b8.b(a_, ba, A_2, A_3, A_4, ref num);
			}
			finally
			{
				if (A_1)
				{
					try
					{
						A_0.Close();
					}
					catch (Exception)
					{
					}
				}
			}
			return ba;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0005F214 File Offset: 0x0005E214
		protected internal static void b(ig A_0, ba A_1, ByteToStringConversionHandler A_2, bool A_3, Encoding A_4, ref int A_5)
		{
			SortedDictionary<string, e1> sortedDictionary = new SortedDictionary<string, e1>();
			IEnumerator enumerator = A_0.eh();
			while (enumerator.MoveNext())
			{
				sortedDictionary.Add(((e1)enumerator.Current).r(), (e1)enumerator.Current);
			}
			for (int i = 0; i < 2; i++)
			{
				foreach (e1 e in sortedDictionary.Values)
				{
					if (e.aa())
					{
						if (i > 0)
						{
							ig ig = (ig)e;
							if (ig.r().StartsWith("__attach_version1.0"))
							{
								b8.a(ig, A_1, A_2, A_3, A_4, ref A_5);
							}
							else if (!ig.r().StartsWith("__nameid_version1.0"))
							{
								b8.b(ig, A_1, A_2, A_3, A_4, ref A_5);
							}
						}
					}
					else if (e.s())
					{
						h4 a_ = (h4)e;
						az az = new az(a_);
						if (!az.c())
						{
							ek ek = b8.a(a_);
							object a_2 = b8.a(az, ek, A_2);
							if (i > 0)
							{
								if (ek.b() != "properties")
								{
									A_1.a(ek.b(), a_2, A_3, A_4, false);
								}
							}
							else if (ek.b() == "properties")
							{
								A_1.a(ek.b(), a_2, A_3, A_4, false);
								if (A_1.f() != null)
								{
									A_4 = A_1.f();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0005F3C4 File Offset: 0x0005E3C4
		protected internal static object a(az A_0, ek A_1, ByteToStringConversionHandler A_2)
		{
			if (A_1 == null || A_1.a() == "unknown")
			{
				return null;
			}
			if (A_1.a().Equals("001e"))
			{
				MemoryStream memoryStream = new MemoryStream();
				byte[] array = new byte[1024];
				int count;
				while ((count = A_0.a(array)) > 0)
				{
					memoryStream.Write(array, 0, count);
				}
				string text = string.Empty;
				if (A_2 == null)
				{
					if (A_1.b() == "1013")
					{
						return memoryStream.ToArray();
					}
					text = Global.DefaultEncoding.GetString(memoryStream.ToArray(), 0, memoryStream.ToArray().Length);
				}
				else
				{
					text = A_2(memoryStream.ToArray());
					if (text == null)
					{
						text = string.Empty;
					}
				}
				return text;
			}
			if (A_1.a().Equals("001f"))
			{
				MemoryStream memoryStream2 = new MemoryStream();
				byte[] array2 = new byte[1024];
				int count2;
				while ((count2 = A_0.a(array2)) > 0)
				{
					memoryStream2.Write(array2, 0, count2);
				}
				byte[] array3 = memoryStream2.ToArray();
				char[] array4 = new char[array3.Length / 2];
				int num = 0;
				for (int i = 0; i < array3.Length - 1; i += 2)
				{
					int num2 = (int)array3[i + 1];
					int num3 = (int)array3[i];
					array4[num++] = (char)((num2 << 8) + num3);
				}
				return new string(array4);
			}
			if (A_1.a().Equals("0102"))
			{
				MemoryStream memoryStream3 = new MemoryStream();
				byte[] array5 = new byte[1024];
				int count3;
				while ((count3 = A_0.a(array5)) > 0)
				{
					memoryStream3.Write(array5, 0, count3);
				}
				return memoryStream3.ToArray();
			}
			return null;
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0005F56C File Offset: 0x0005E56C
		private static ek a(h4 A_0)
		{
			string text = A_0.r();
			string text2 = "__substg1.";
			if (text.StartsWith(text2))
			{
				string a_ = "unknown";
				string a_2 = "unknown";
				try
				{
					string text3 = text.Substring(text2.Length + 2).ToLower();
					a_ = text3.Substring(0, 4);
					a_2 = text3.Substring(4);
				}
				catch (Exception)
				{
				}
				return new ek(a_, a_2);
			}
			if (text.StartsWith("__properties_version1."))
			{
				return new ek("properties", "0102");
			}
			if (text.Equals("CONTENTS"))
			{
				return new ek("contents", "0102");
			}
			return new ek();
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0005F61C File Offset: 0x0005E61C
		protected internal static void a(ig A_0, ba A_1, ByteToStringConversionHandler A_2, bool A_3, Encoding A_4, ref int A_5)
		{
			e4 e = new e4();
			ba ba = null;
			bool flag = false;
			for (int i = 0; i < 2; i++)
			{
				IEnumerator enumerator = A_0.eh();
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					e1 e2 = (e1)obj;
					if (e2.s())
					{
						h4 h = (h4)e2;
						ek ek = b8.a(h);
						object a_ = b8.a(new az(h), ek, A_2);
						string a_2 = ek.b();
						e.a(a_2, a_, h, ref A_5);
					}
					else if (e2.r().Equals("__substg1.0_3701000D"))
					{
						ig ig = (ig)e2;
						bool flag2 = false;
						for (int j = 0; j < 2; j++)
						{
							IEnumerator enumerator2 = ig.eh();
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								e1 e3 = (e1)obj2;
								if (e3.s())
								{
									h4 h2 = (h4)e3;
									ek ek2 = b8.a(h2);
									object a_3 = b8.a(new az(h2), ek2, A_2);
									string text = ek2.b();
									if (ba == null)
									{
										e.a(text, a_3, h2, ref A_5);
									}
									else if (flag2)
									{
										if (text != "properties")
										{
											ba.a(text, a_3, A_3, A_4, true);
										}
									}
									else if (text == "properties")
									{
										ba.a(text, a_3, A_3, A_4, true);
										flag2 = true;
									}
								}
								else if (e3.aa() && j > 0)
								{
									ig ig2 = (ig)e3;
									if (ig2.r().StartsWith("__attach_version1.0"))
									{
										if (ba != null)
										{
											b8.a(ig2, ba, A_2, A_3, A_4, ref A_5);
										}
									}
									else if (!ig2.r().StartsWith("__nameid_version1.0"))
									{
										b8.b(ig2, A_1, A_2, A_3, A_4, ref A_5);
									}
								}
							}
							if (ba == null)
							{
								break;
							}
						}
					}
					else
					{
						ba ba2 = new ba();
						ij ij = new ij();
						ij.a(ba2);
						A_1.a(ij);
						flag = true;
						b8.b((ig)e2, ba2, A_2, A_3, A_4, ref A_5);
					}
				}
				if (flag || !e.f() || ba != null)
				{
					break;
				}
				ba = new ba();
				ij ij2 = new ij();
				ij2.a(ba);
				A_1.a(ij2);
				e = new e4();
			}
			if (e.d() > -1L)
			{
				A_1.a(e);
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0005F885 File Offset: 0x0005E885
		public static string a(string A_0)
		{
			if (A_0 == string.Empty)
			{
				return string.Empty;
			}
			if (A_0[A_0.Length - 1] == '\0')
			{
				return A_0.Substring(0, A_0.Length - 1);
			}
			return A_0;
		}
	}
}
