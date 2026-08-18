using System;
using System.Runtime.Serialization;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000116 RID: 278
	[Serializable]
	public class AstException : Exception
	{
		// Token: 0x06001130 RID: 4400 RVA: 0x0004C0FF File Offset: 0x0004A2FF
		public AstException()
		{
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0004C107 File Offset: 0x0004A307
		public AstException(string message) : base(message)
		{
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0004C110 File Offset: 0x0004A310
		public AstException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0004C11A File Offset: 0x0004A31A
		protected AstException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
