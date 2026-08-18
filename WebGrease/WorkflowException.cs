using System;
using System.Runtime.Serialization;

namespace WebGrease
{
	// Token: 0x020000DF RID: 223
	[Serializable]
	public class WorkflowException : Exception
	{
		// Token: 0x06000E9E RID: 3742 RVA: 0x00044EFC File Offset: 0x000430FC
		public WorkflowException()
		{
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00044F04 File Offset: 0x00043104
		public WorkflowException(string message) : base(message)
		{
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00044F0D File Offset: 0x0004310D
		public WorkflowException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00044F17 File Offset: 0x00043117
		protected WorkflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
