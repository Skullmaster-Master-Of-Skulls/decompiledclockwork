using System;
using System.Runtime.Serialization;

namespace Renci.SshNet.Common
{
	// Token: 0x02000104 RID: 260
	[Serializable]
	public class SshException : Exception
	{
		// Token: 0x06000B26 RID: 2854 RVA: 0x00025206 File Offset: 0x00023406
		public SshException()
		{
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002520E File Offset: 0x0002340E
		public SshException(string message) : base(message)
		{
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00025217 File Offset: 0x00023417
		public SshException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00025221 File Offset: 0x00023421
		protected SshException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
