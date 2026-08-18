using System;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class PipeException : IOException
	{
		// Token: 0x0600015A RID: 346 RVA: 0x0000888C File Offset: 0x00006A8C
		public PipeException()
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00008894 File Offset: 0x00006A94
		public PipeException(string message) : base(message)
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000889D File Offset: 0x00006A9D
		public PipeException(string message, int errorCode) : base(message, errorCode)
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000088A7 File Offset: 0x00006AA7
		public PipeException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000088B1 File Offset: 0x00006AB1
		protected PipeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000088BB File Offset: 0x00006ABB
		public virtual int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}
	}
}
