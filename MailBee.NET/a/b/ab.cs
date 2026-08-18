using System;
using System.Collections;
using System.IO;
using System.Text;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000242 RID: 578
	internal class ab
	{
		// Token: 0x06001353 RID: 4947 RVA: 0x0005743B File Offset: 0x0005643B
		public Hashtable h()
		{
			return this.n;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00057444 File Offset: 0x00056444
		public void b(string A_0, string A_1)
		{
			try
			{
				this.k.el("__substg1.0_" + A_0 + A_1).u();
				this.m.a(Convert.ToInt64(A_0, 16));
			}
			catch (FileNotFoundException)
			{
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00057498 File Offset: 0x00056498
		public static MemoryStream a(bool A_0)
		{
			byte[] array = new byte[2];
			if (A_0)
			{
				array[0] = 1;
				array[1] = 0;
			}
			else
			{
				array[0] = 0;
				array[1] = 0;
			}
			return new MemoryStream(array, 0, 2);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000574CC File Offset: 0x000564CC
		public static MemoryStream a(long A_0)
		{
			char[] array = new char[4];
			int num = 0;
			for (int i = array.Length - 1; i >= 0; i--)
			{
				int num2 = (array.Length - i - 1) * 8;
				array[num++] = (char)eo.a(A_0 & 255L << (num2 & 31), num2);
			}
			byte[] array2 = new byte[array.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = (byte)array[j];
			}
			return new MemoryStream(array2, 0, 4);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00057546 File Offset: 0x00056546
		public static long a(DateTime A_0)
		{
			return A_0.ToFileTime();
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0005754F File Offset: 0x0005654F
		public static MemoryStream a(string A_0, Encoding A_1)
		{
			return ab.b(A_0, A_1, false);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0005755C File Offset: 0x0005655C
		public static MemoryStream b(string A_0, Encoding A_1, bool A_2)
		{
			if (!A_2)
			{
				A_0 += "\0";
			}
			if (A_1 == null)
			{
				A_1 = Global.DefaultEncoding;
			}
			byte[] bytes = A_1.GetBytes(A_0);
			return new MemoryStream(bytes, 0, bytes.Length);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00057595 File Offset: 0x00056595
		public static MemoryStream b(string A_0)
		{
			return ab.b(A_0, false);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0005759E File Offset: 0x0005659E
		public static MemoryStream a(string A_0, Encoding A_1, bool A_2)
		{
			return ab.a(A_0, A_1, A_2, false);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000575A9 File Offset: 0x000565A9
		public static MemoryStream a(string A_0, Encoding A_1, bool A_2, bool A_3)
		{
			if (!A_2)
			{
				return ab.b(A_0, A_1, A_3);
			}
			return ab.a(A_0);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000575C0 File Offset: 0x000565C0
		public static MemoryStream b(string A_0, bool A_1)
		{
			if (!A_1)
			{
				A_0 += "\0";
			}
			byte[] bytes = Global.DefaultEncoding.GetBytes(A_0);
			return new MemoryStream(bytes, 0, bytes.Length);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000575F4 File Offset: 0x000565F4
		public static MemoryStream a(string A_0)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(A_0);
			return new MemoryStream(bytes, 0, bytes.Length);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00057617 File Offset: 0x00056617
		public static MemoryStream a(string A_0, bool A_1)
		{
			return ab.a(A_0, A_1, false);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00057621 File Offset: 0x00056621
		public static MemoryStream a(string A_0, bool A_1, bool A_2)
		{
			if (!A_1)
			{
				return ab.b(A_0, A_2);
			}
			return ab.a(A_0);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00057634 File Offset: 0x00056634
		public static MemoryStream a(byte[] A_0)
		{
			return new MemoryStream(A_0, 0, A_0.Length);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00057640 File Offset: 0x00056640
		public ig a(ig A_0, string A_1, string A_2, Stream A_3)
		{
			int num = A_0.em(A_1 + A_2, A_3).oy();
			if (A_2 == "001F")
			{
				num += 2;
			}
			string value = A_1.Substring(A_1.Length - 4, 4);
			this.m.a(Convert.ToInt64(value, 16), Convert.ToInt64(A_2, 16), true, (long)num);
			return A_0;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000576A4 File Offset: 0x000566A4
		public ig b(ig A_0, string A_1, string A_2, Stream A_3)
		{
			int num = A_0.em(A_1 + A_2, A_3).oy();
			if (A_2 == "001F")
			{
				num += 2;
			}
			string value = A_1.Substring(A_1.Length - 4, 4);
			this.m.a(Convert.ToInt64(value, 16), Convert.ToInt64(A_2, 16), false, (long)num);
			return A_0;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00057708 File Offset: 0x00056708
		public ig a(ig A_0, string A_1, string A_2, Stream A_3, byte A_4, byte A_5)
		{
			int num = A_0.em(A_1 + A_2, A_3).oy();
			string value = A_1.Substring(A_1.Length - 4, 4);
			this.m.a(Convert.ToInt64(value, 16), Convert.ToInt64(A_2, 16), true, (long)num, A_4, A_5);
			return A_0;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00057760 File Offset: 0x00056760
		public ig a(ig A_0, string A_1, string A_2, long A_3)
		{
			if (A_1 == "0003" || A_1 == "0040")
			{
				return null;
			}
			string value = A_1.Substring(A_1.Length - 4, 4);
			this.m.a(Convert.ToInt64(value, 16), Convert.ToInt64(A_2, 16), true, A_3);
			return A_0;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000577B0 File Offset: 0x000567B0
		public ig a(ig A_0, string A_1, string A_2, long A_3, byte A_4, byte A_5)
		{
			if (A_1 == "0003" || A_1 == "0040")
			{
				return null;
			}
			string value = A_1.Substring(A_1.Length - 4, 4);
			this.m.a(Convert.ToInt64(value, 16), Convert.ToInt64(A_2, 16), true, A_3, A_4, A_5);
			return A_0;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00057802 File Offset: 0x00056802
		public ig a(string A_0, string A_1, Stream A_2)
		{
			return this.a(this.k, A_0, A_1, A_2);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00057813 File Offset: 0x00056813
		public ig b(string A_0, string A_1, Stream A_2)
		{
			return this.b(this.k, A_0, A_1, A_2);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00057824 File Offset: 0x00056824
		public ig a(string A_0, string A_1, Stream A_2, byte A_3, byte A_4)
		{
			return this.a(this.k, A_0, A_1, A_2, A_3, A_4);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0005783C File Offset: 0x0005683C
		public ig a(string A_0, ig A_1)
		{
			try
			{
				FileInfo[] files = new DirectoryInfo(A_0).GetFiles();
				for (int i = 0; i < files.Length; i++)
				{
					if (Directory.Exists(files[i].FullName))
					{
						string name = files[i].Name;
						ig a_ = A_1.eo(name);
						a_ = this.a(files[i].FullName, a_);
					}
					else
					{
						string a_2 = files[i].Name.Replace(".txt", string.Empty);
						Stream stream = new FileStream(files[i].FullName, FileMode.Open, FileAccess.Read);
						A_1.em(a_2, stream);
						stream.Close();
					}
				}
			}
			catch (Exception)
			{
			}
			return A_1;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000578E8 File Offset: 0x000568E8
		public void a(hz A_0, Hashtable A_1)
		{
			string key = A_0.Name;
			IEnumerator enumerator2;
			if (A_0.jk())
			{
				Array array = A_0.ji();
				ArrayList arrayList = new ArrayList(array.Length);
				foreach (object value in array)
				{
					arrayList.Add(value);
				}
				enumerator2 = arrayList.GetEnumerator();
			}
			else
			{
				enumerator2 = A_0.jj();
			}
			while (enumerator2.MoveNext())
			{
				object obj = enumerator2.Current;
				if (!(obj is gg) && obj is eg)
				{
					if (((eg)obj).ap() <= 0)
					{
						MemoryStream memoryStream = new MemoryStream();
						af[] array2 = ((eg)obj).b();
						for (int i = 0; i < array2.Length; i++)
						{
							array2[i].a3(memoryStream);
						}
						A_1[key] = memoryStream;
					}
					else
					{
						MemoryStream memoryStream2 = new MemoryStream();
						((eg)obj).a3(memoryStream2);
						A_1[key] = memoryStream2;
					}
				}
			}
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00057A08 File Offset: 0x00056A08
		public Hashtable a(IEnumerator A_0)
		{
			Hashtable hashtable = new Hashtable();
			while (A_0.MoveNext())
			{
				object obj = A_0.Current;
				if (obj is hz)
				{
					this.a((hz)obj, hashtable);
				}
				else if (obj is DirectoryNode)
				{
					string key = ((DirectoryNode)obj).Name;
					IEnumerator a_;
					if (((DirectoryNode)obj).PreferArray)
					{
						Array array = ((DirectoryNode)obj).ViewableArray;
						ArrayList arrayList = new ArrayList(array.Length);
						foreach (object value in array)
						{
							arrayList.Add(value);
						}
						a_ = arrayList.GetEnumerator();
					}
					else
					{
						a_ = ((DirectoryNode)obj).ViewableIterator;
					}
					hashtable[key] = this.a(a_);
					this.l = string.Empty;
				}
				else if (obj is g8)
				{
					string str = ((g8)obj).f();
					this.l = str + "/";
				}
			}
			return hashtable;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00057B30 File Offset: 0x00056B30
		public string g()
		{
			if (!this.o)
			{
				return "001E";
			}
			return "001F";
		}

		// Token: 0x04000F84 RID: 3972
		public const string a = "0102";

		// Token: 0x04000F85 RID: 3973
		public const string b = "001E";

		// Token: 0x04000F86 RID: 3974
		public const string c = "001F";

		// Token: 0x04000F87 RID: 3975
		public const string d = "0003";

		// Token: 0x04000F88 RID: 3976
		public const string e = "000B";

		// Token: 0x04000F89 RID: 3977
		public const string f = "0040";

		// Token: 0x04000F8A RID: 3978
		public const int g = 1;

		// Token: 0x04000F8B RID: 3979
		public const int h = 2;

		// Token: 0x04000F8C RID: 3980
		public const int i = 3;

		// Token: 0x04000F8D RID: 3981
		public const int j = 3;

		// Token: 0x04000F8E RID: 3982
		protected internal ig k;

		// Token: 0x04000F8F RID: 3983
		protected internal string l;

		// Token: 0x04000F90 RID: 3984
		protected internal e m;

		// Token: 0x04000F91 RID: 3985
		protected internal Hashtable n;

		// Token: 0x04000F92 RID: 3986
		protected bool o;

		// Token: 0x04000F93 RID: 3987
		protected bool p;

		// Token: 0x04000F94 RID: 3988
		protected bool q;
	}
}
