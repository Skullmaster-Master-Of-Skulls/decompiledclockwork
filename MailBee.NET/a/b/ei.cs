using System;
using System.Collections;
using System.Collections.Generic;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000305 RID: 773
	internal class ei
	{
		// Token: 0x06001B2B RID: 6955 RVA: 0x00076AB4 File Offset: 0x00075AB4
		public static void a(e1 A_0, ig A_1)
		{
			if (A_0.aa())
			{
				ig a_ = A_1.eo(A_0.r());
				IEnumerator enumerator = ((ig)A_0).eh();
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					ei.a((e1)obj, a_);
				}
				return;
			}
			h4 h = (h4)A_0;
			using (az az = new az(h))
			{
				A_1.em(h.r(), az);
			}
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00076B38 File Offset: 0x00075B38
		public static void a(ig A_0, ig A_1, List<string> A_2)
		{
			IEnumerator enumerator = A_0.eh();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				e1 e = (e1)obj;
				if (!A_2.Contains(e.r()))
				{
					ei.a(e, A_1);
				}
			}
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00076B77 File Offset: 0x00075B77
		public static void a(POIFSFileSystem A_0, POIFSFileSystem A_1, List<string> A_2)
		{
			ei.a(A_0.Root, A_1.Root, A_2);
		}
	}
}
