using System;
using System.Collections.Specialized;
using System.Reflection;

namespace a.k
{
	// Token: 0x0200012F RID: 303
	[DefaultMember("Item")]
	internal class e
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x0002D2D8 File Offset: 0x0002C2D8
		internal StringDictionary b()
		{
			return this.a;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002D2E0 File Offset: 0x0002C2E0
		internal string b(string A_0)
		{
			if (this.a[A_0] != null)
			{
				return this.a[A_0].Trim();
			}
			return null;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002D303 File Offset: 0x0002C303
		internal int a()
		{
			return this.a.Count;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002D310 File Offset: 0x0002C310
		internal e()
		{
			this.a = new StringDictionary();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002D323 File Offset: 0x0002C323
		internal void a(string A_0, string A_1)
		{
			if (!this.a.ContainsKey(A_0.Trim()))
			{
				this.a.Add(A_0.Trim(), A_1.Trim());
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002D34F File Offset: 0x0002C34F
		internal string d(string A_0)
		{
			if (this.a[A_0] != null)
			{
				return this.a[A_0].Split(new char[]
				{
					';'
				}, 2)[0].Trim();
			}
			return null;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0002D388 File Offset: 0x0002C388
		internal string a(string A_0)
		{
			if (this.a[A_0] != null)
			{
				string[] array = this.a[A_0].Split(new char[]
				{
					';'
				}, 2);
				if (array.Length == 2)
				{
					return array[1].Trim();
				}
			}
			return null;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002D3D4 File Offset: 0x0002C3D4
		internal string c(string A_0)
		{
			string text = this.a(A_0);
			if (text != null && text != string.Empty && text[0] == '<' && text[text.Length - 1] == '>')
			{
				return text.Substring(1, text.Length - 2);
			}
			return text;
		}

		// Token: 0x040007A4 RID: 1956
		private StringDictionary a;
	}
}
