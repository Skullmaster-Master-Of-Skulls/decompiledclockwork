using System;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	internal class InternalMappingException : EntityException
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002F89 File Offset: 0x00001189
		internal InternalMappingException()
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F91 File Offset: 0x00001191
		internal InternalMappingException(string message) : base(message)
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F9A File Offset: 0x0000119A
		internal InternalMappingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002FA4 File Offset: 0x000011A4
		protected InternalMappingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FBB File Offset: 0x000011BB
		internal InternalMappingException(string message, ErrorLog errorLog) : base(message)
		{
			EntityUtil.CheckArgumentNull<ErrorLog>(errorLog, "errorLog");
			this.m_errorLog = errorLog;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FD7 File Offset: 0x000011D7
		internal InternalMappingException(string message, ErrorLog.Record record) : base(message)
		{
			EntityUtil.CheckArgumentNull<ErrorLog.Record>(record, "record");
			this.m_errorLog = new ErrorLog();
			this.m_errorLog.AddEntry(record);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003003 File Offset: 0x00001203
		internal ErrorLog ErrorLog
		{
			get
			{
				return this.m_errorLog;
			}
		}

		// Token: 0x04000083 RID: 131
		private ErrorLog m_errorLog;
	}
}
