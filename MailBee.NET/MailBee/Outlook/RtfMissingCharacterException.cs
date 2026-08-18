using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005CB RID: 1483
	[Serializable]
	internal class RtfMissingCharacterException : RtfStructureException
	{
		// Token: 0x06003170 RID: 12656 RVA: 0x000E707D File Offset: 0x000E607D
		public RtfMissingCharacterException()
		{
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000E7085 File Offset: 0x000E6085
		public RtfMissingCharacterException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x000E708E File Offset: 0x000E608E
		public RtfMissingCharacterException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000E7098 File Offset: 0x000E6098
		protected RtfMissingCharacterException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
