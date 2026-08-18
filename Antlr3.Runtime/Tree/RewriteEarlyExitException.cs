using System;
using System.Runtime.Serialization;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public class RewriteEarlyExitException : RewriteCardinalityException
	{
		// Token: 0x060003BF RID: 959 RVA: 0x0000A3E2 File Offset: 0x000085E2
		public RewriteEarlyExitException()
		{
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000A3EA File Offset: 0x000085EA
		public RewriteEarlyExitException(string elementDescription) : base(elementDescription)
		{
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000A3F3 File Offset: 0x000085F3
		public RewriteEarlyExitException(string elementDescription, Exception innerException) : base(elementDescription, innerException)
		{
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000A3FD File Offset: 0x000085FD
		public RewriteEarlyExitException(string message, string elementDescription) : base(message, elementDescription)
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000A407 File Offset: 0x00008607
		public RewriteEarlyExitException(string message, string elementDescription, Exception innerException) : base(message, elementDescription, innerException)
		{
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000A412 File Offset: 0x00008612
		protected RewriteEarlyExitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
