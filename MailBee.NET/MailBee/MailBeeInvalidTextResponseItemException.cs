using System;
using System.Runtime.Serialization;
using System.Text;
using a;

namespace MailBee
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	public class MailBeeInvalidTextResponseItemException : MailBeeInvalidTextResponseException
	{
		// Token: 0x0600039B RID: 923 RVA: 0x00008F8C File Offset: 0x00007F8C
		internal MailBeeInvalidTextResponseItemException(int A_0, ai A_1, string A_2, Encoding A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00008F99 File Offset: 0x00007F99
		protected MailBeeInvalidTextResponseItemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
