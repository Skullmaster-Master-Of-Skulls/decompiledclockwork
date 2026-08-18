using System;
using System.Collections;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x020002B4 RID: 692
	internal class dt
	{
		// Token: 0x0600182D RID: 6189 RVA: 0x0006E538 File Offset: 0x0006D538
		public static IList a(object A_0, bool A_1, int A_2, string A_3)
		{
			IList list = new ArrayList();
			if (A_0 is DictionaryEntry)
			{
				dt.a(((DictionaryEntry)A_0).Value, A_1, A_2, A_3, list);
			}
			else if (A_0 is gj)
			{
				dt.a(A_0, A_1, A_2, A_3, list);
			}
			else
			{
				list.Add(dt.a(A_2, A_3, A_0.ToString()));
			}
			return list;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0006E598 File Offset: 0x0006D598
		internal static void a(object A_0, bool A_1, int A_2, string A_3, IList A_4)
		{
			gj gj = (gj)A_0;
			A_4.Add(dt.a(A_2, A_3, gj.jl()));
			if (A_1)
			{
				if (gj is eg)
				{
					((ArrayList)A_4).AddRange(dt.a("POIFSDocument content is too long so ignored", A_1, A_2 + 1, A_3));
					return;
				}
				if (gj.jk())
				{
					Array array = gj.ji();
					for (int i = 0; i < array.Length; i++)
					{
						((ArrayList)A_4).AddRange(dt.a(array.GetValue(i), A_1, A_2 + 1, A_3));
					}
					return;
				}
				IEnumerator enumerator = gj.jj();
				while (enumerator.MoveNext())
				{
					object a_ = enumerator.Current;
					((ArrayList)A_4).AddRange(dt.a(a_, A_1, A_2 + 1, A_3));
				}
			}
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0006E658 File Offset: 0x0006D658
		private static string a(int A_0, string A_1, string A_2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < A_0; i++)
			{
				stringBuilder2.Append(A_1);
			}
			string result;
			using (StringReader stringReader = new StringReader(A_2))
			{
				for (string value = stringReader.ReadLine(); value != null; value = stringReader.ReadLine())
				{
					stringBuilder.Append(stringBuilder2).Append(value).Append(Environment.NewLine);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}
	}
}
