using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005BF RID: 1471
	[Serializable]
	internal class RtfException : Exception
	{
		// Token: 0x06003144 RID: 12612 RVA: 0x000E6EE6 File Offset: 0x000E5EE6
		public RtfException()
		{
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000E6EEE File Offset: 0x000E5EEE
		public RtfException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000E6EF7 File Offset: 0x000E5EF7
		public RtfException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000E6F01 File Offset: 0x000E5F01
		protected RtfException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
