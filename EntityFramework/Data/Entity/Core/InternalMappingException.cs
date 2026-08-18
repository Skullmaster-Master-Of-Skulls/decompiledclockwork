using System;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020003A3 RID: 931
	[Serializable]
	internal class InternalMappingException : EntityException
	{
		// Token: 0x060021A9 RID: 8617 RVA: 0x0009DDA2 File Offset: 0x0009BFA2
		internal InternalMappingException()
		{
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0009DDAA File Offset: 0x0009BFAA
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal InternalMappingException(string message) : base(message)
		{
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0009DDB3 File Offset: 0x0009BFB3
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal InternalMappingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0009DDBD File Offset: 0x0009BFBD
		protected InternalMappingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x0009DDC7 File Offset: 0x0009BFC7
		internal InternalMappingException(string message, ErrorLog errorLog) : base(message)
		{
			this.m_errorLog = errorLog;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0009DDD7 File Offset: 0x0009BFD7
		internal InternalMappingException(string message, ErrorLog.Record record) : base(message)
		{
			this.m_errorLog = new ErrorLog();
			this.m_errorLog.AddEntry(record);
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x0009DDF7 File Offset: 0x0009BFF7
		internal ErrorLog ErrorLog
		{
			get
			{
				return this.m_errorLog;
			}
		}

		// Token: 0x04000BDA RID: 3034
		private readonly ErrorLog m_errorLog;
	}
}
