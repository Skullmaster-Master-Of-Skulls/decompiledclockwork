using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002CE RID: 718
	internal class cn
	{
		// Token: 0x060018E8 RID: 6376 RVA: 0x0006F5AC File Offset: 0x0006E5AC
		public static void a(e1 A_0, ig A_1)
		{
			if (A_0.aa())
			{
				ig a_ = A_1.eo(A_0.r());
				IEnumerator<e1> enumerator = ((ig)A_0).eh();
				while (enumerator.MoveNext())
				{
					e1 a_2 = enumerator.Current;
					cn.a(a_2, a_);
				}
				return;
			}
			h4 h = (h4)A_0;
			az az = new az(h);
			A_1.em(h.r(), az);
			az.Close();
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0006F618 File Offset: 0x0006E618
		public static void b(ig A_0, ig A_1)
		{
			foreach (e1 a_ in A_0)
			{
				cn.a(a_, A_1);
			}
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0006F660 File Offset: 0x0006E660
		public static void a(b2 A_0, b2 A_1)
		{
			cn.b(A_0, A_1);
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0006F66C File Offset: 0x0006E66C
		[Obsolete]
		public static void a(ig A_0, ig A_1, List<string> A_2)
		{
			IEnumerator enumerator = A_0.eh();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				e1 e = (e1)obj;
				if (!A_2.Contains(e.r()))
				{
					cn.a(e, A_1);
				}
			}
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0006F6AB File Offset: 0x0006E6AB
		public static void a(POIFSFileSystem A_0, POIFSFileSystem A_1)
		{
			cn.b(A_0.Root, A_1.Root);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0006F6BE File Offset: 0x0006E6BE
		public static void a(POIFSFileSystem A_0, POIFSFileSystem A_1, List<string> A_2)
		{
			cn.a(new b2(A_0.Root, A_2), new b2(A_1.Root, A_2));
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0006F6E0 File Offset: 0x0006E6E0
		public static bool a(ig A_0, ig A_1)
		{
			if (!A_0.r().Equals(A_1.r()))
			{
				return false;
			}
			if (A_0.ek() != A_1.ek())
			{
				return false;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			int num = -12345;
			foreach (e1 e in A_0)
			{
				string key = e.r();
				if (e.aa())
				{
					dictionary.Add(key, num);
				}
				else
				{
					dictionary.Add(key, ((hz)e).oy());
				}
			}
			foreach (e1 e2 in A_1)
			{
				string key2 = e2.r();
				if (!dictionary.ContainsKey(key2))
				{
					return false;
				}
				int num2;
				if (e2.aa())
				{
					num2 = num;
				}
				else
				{
					num2 = ((hz)e2).oy();
				}
				if (num2 != dictionary[key2])
				{
					return false;
				}
				dictionary.Remove(key2);
			}
			if (dictionary.Count != 0)
			{
				return false;
			}
			foreach (e1 e3 in A_0)
			{
				try
				{
					e1 e4 = A_1.el(e3.r());
					bool flag;
					if (e3.aa())
					{
						flag = cn.a((ig)e3, (ig)e4);
					}
					else
					{
						flag = cn.a((h4)e3, (h4)e4);
					}
					if (!flag)
					{
						return false;
					}
				}
				catch (FileNotFoundException)
				{
					return false;
				}
				catch (IOException)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0006F8B8 File Offset: 0x0006E8B8
		public static bool a(h4 A_0, h4 A_1)
		{
			if (!A_0.r().Equals(A_1.r()))
			{
				return false;
			}
			if (A_0.oy() != A_1.oy())
			{
				return false;
			}
			bool result = true;
			az az = null;
			az az2 = null;
			try
			{
				az = new az(A_0);
				az2 = new az(A_1);
				for (;;)
				{
					int num = az.@as();
					int num2 = az2.@as();
					if (num != num2)
					{
						break;
					}
					if (num == -1 || num2 == -1)
					{
						goto IL_5A;
					}
				}
				result = false;
				IL_5A:;
			}
			finally
			{
				if (az != null)
				{
					az.Close();
				}
				if (az2 != null)
				{
					az2.Close();
				}
			}
			return result;
		}
	}
}
