using System;
using System.Runtime.Serialization;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public class RewriteEmptyStreamException : RewriteCardinalityException
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0000A41C File Offset: 0x0000861C
		public RewriteEmptyStreamException()
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000A424 File Offset: 0x00008624
		public RewriteEmptyStreamException(string elementDescription) : base(elementDescription)
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000A42D File Offset: 0x0000862D
		public RewriteEmptyStreamException(string elementDescription, Exception innerException) : base(elementDescription, innerException)
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000A437 File Offset: 0x00008637
		public RewriteEmptyStreamException(string message, string elementDescription) : base(message, elementDescription)
		{
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000A441 File Offset: 0x00008641
		public RewriteEmptyStreamException(string message, string elementDescription, Exception innerException) : base(message, elementDescription, innerException)
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000A44C File Offset: 0x0000864C
		protected RewriteEmptyStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
