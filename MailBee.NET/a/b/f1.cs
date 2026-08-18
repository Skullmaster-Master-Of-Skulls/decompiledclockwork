using System;
using System.Collections;
using System.IO;
using System.Text;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002B5 RID: 693
	internal class f1
	{
		// Token: 0x06001831 RID: 6193 RVA: 0x0006E6EC File Offset: 0x0006D6EC
		public static void a(string A_0, bool A_1)
		{
			if (A_1)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(".");
				for (int i = 0; i < A_0.Length; i++)
				{
					stringBuilder.Append("-");
				}
				stringBuilder.Append(".");
			}
			try
			{
				using (Stream stream = File.OpenRead(A_0))
				{
					IEnumerator enumerator = dt.a(new POIFSFileSystem(stream), true, 0, "  ").GetEnumerator();
					while (enumerator.MoveNext())
					{
					}
				}
			}
			catch (IOException)
			{
			}
		}
	}
}
