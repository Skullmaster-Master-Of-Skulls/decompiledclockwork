using System;
using System.Collections;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002B2 RID: 690
	internal class gh
	{
		// Token: 0x06001826 RID: 6182 RVA: 0x0006E434 File Offset: 0x0006D434
		public static void a(string A_0)
		{
			using (Stream stream = new FileStream(A_0, FileMode.Open))
			{
				gh.a(new POIFSFileSystem(stream).Root, "");
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0006E47C File Offset: 0x0006D47C
		public static void a(DirectoryNode A_0, string A_1)
		{
			string a_ = A_1 + "  ";
			IEnumerator enumerator = A_0.Entries;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (obj is DirectoryNode)
				{
					gh.a((DirectoryNode)obj, a_);
				}
				else
				{
					string text = ((hz)obj).Name;
					if (text[0] < '\n')
					{
						string str = string.Concat(new object[]
						{
							"(0x0",
							(int)text[0],
							")",
							text.Substring(1)
						});
						text = text.Substring(1) + " <" + str + ">";
					}
				}
			}
		}
	}
}
