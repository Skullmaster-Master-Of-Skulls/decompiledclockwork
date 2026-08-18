using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Xml;

namespace a.c
{
	// Token: 0x02000234 RID: 564
	[DefaultMember("Item")]
	internal class a
	{
		// Token: 0x0600130B RID: 4875 RVA: 0x00055080 File Offset: 0x00054080
		public a(XmlNode A_0)
		{
			if (A_0 != null)
			{
				string[] array = A_0.Value.Trim(new char[]
				{
					'"',
					'\''
				}).Split(new char[]
				{
					';'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						':'
					});
					if (array2.Length == 2 && array2[0] != null && array2[0].Trim() != string.Empty && array2[1] != null && array2[1].Trim() != string.Empty)
					{
						this.a[array2[0].Trim().ToLower()] = array2[1].Trim();
					}
				}
			}
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0005514A File Offset: 0x0005414A
		public string a(string A_0)
		{
			return this.a[A_0];
		}

		// Token: 0x04000F74 RID: 3956
		private StringDictionary a = new StringDictionary();
	}
}
