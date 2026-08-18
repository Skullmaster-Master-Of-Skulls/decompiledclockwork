using System;
using System.Collections;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000309 RID: 777
	internal class hb
	{
		// Token: 0x06001BBF RID: 7103 RVA: 0x0007A88C File Offset: 0x0007988C
		public static BitField a(int A_0)
		{
			BitField bitField = (BitField)hb.a[A_0];
			if (bitField == null)
			{
				bitField = new BitField(A_0);
				hb.a[A_0] = bitField;
			}
			return bitField;
		}

		// Token: 0x04001340 RID: 4928
		private static Hashtable a = new Hashtable();
	}
}
