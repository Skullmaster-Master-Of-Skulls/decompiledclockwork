using System;

namespace MailBee.Outlook
{
	// Token: 0x02000594 RID: 1428
	[Serializable]
	internal class IllegalPropertySetDataException : HPSFRuntimeException
	{
		// Token: 0x06002FF2 RID: 12274 RVA: 0x000E24A2 File Offset: 0x000E14A2
		public IllegalPropertySetDataException()
		{
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000E24AA File Offset: 0x000E14AA
		public IllegalPropertySetDataException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000E24B3 File Offset: 0x000E14B3
		public IllegalPropertySetDataException(Exception A_0) : base(A_0)
		{
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000E24BC File Offset: 0x000E14BC
		public IllegalPropertySetDataException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}
	}
}
