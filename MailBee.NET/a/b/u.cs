using System;
using System.IO;
using System.Threading;

namespace a.b
{
	// Token: 0x0200032F RID: 815
	internal class u
	{
		// Token: 0x06001D8E RID: 7566 RVA: 0x0007F5BC File Offset: 0x0007E5BC
		public static FileInfo b(string A_0, string A_1)
		{
			Random random = new Random(DateTime.Now.Millisecond);
			string text = A_0 + random.Next() + A_1;
			File.Create(text).Close();
			return new FileInfo(text);
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0007F600 File Offset: 0x0007E600
		public static string a(string A_0, string A_1)
		{
			Random random = new Random(DateTime.Now.Millisecond);
			Thread.Sleep(10);
			return A_0 + random.Next() + A_1;
		}
	}
}
