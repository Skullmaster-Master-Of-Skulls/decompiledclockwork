using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Xml;

namespace a.c
{
	// Token: 0x0200022C RID: 556
	[DefaultMember("Item")]
	internal class g
	{
		// Token: 0x0600129E RID: 4766 RVA: 0x00052FD4 File Offset: 0x00051FD4
		public g(XmlNode A_0)
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

		// Token: 0x0600129F RID: 4767 RVA: 0x0005309E File Offset: 0x0005209E
		public string a(string A_0)
		{
			return this.a[A_0];
		}

		// Token: 0x04000F43 RID: 3907
		private StringDictionary a = new StringDictionary();
	}
}
