using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x0200052A RID: 1322
	[Serializable]
	public sealed class ObjectNotFoundException : DataException
	{
		// Token: 0x0600321A RID: 12826 RVA: 0x000EF294 File Offset: 0x000ED494
		public ObjectNotFoundException()
		{
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000EF29C File Offset: 0x000ED49C
		public ObjectNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000EF2A5 File Offset: 0x000ED4A5
		public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000EF2AF File Offset: 0x000ED4AF
		private ObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
