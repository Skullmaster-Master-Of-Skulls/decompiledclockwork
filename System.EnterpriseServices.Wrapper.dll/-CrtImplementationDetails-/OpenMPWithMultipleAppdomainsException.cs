using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020000AF RID: 175
	[Serializable]
	internal class OpenMPWithMultipleAppdomainsException : Exception
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00006FF4 File Offset: 0x000063F4
		protected OpenMPWithMultipleAppdomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006FE0 File Offset: 0x000063E0
		public OpenMPWithMultipleAppdomainsException()
		{
		}
	}
}
