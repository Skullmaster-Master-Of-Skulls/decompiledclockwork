using System;

namespace System.Runtime
{
	// Token: 0x02000033 RID: 51
	internal struct TracePayload
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00006DDE File Offset: 0x00004FDE
		public TracePayload(string serializedException, string eventSource, string appDomainFriendlyName, string extendedData, string hostReference)
		{
			this.serializedException = serializedException;
			this.eventSource = eventSource;
			this.appDomainFriendlyName = appDomainFriendlyName;
			this.extendedData = extendedData;
			this.hostReference = hostReference;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006E05 File Offset: 0x00005005
		public string SerializedException
		{
			get
			{
				return this.serializedException;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00006E0D File Offset: 0x0000500D
		public string EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006E15 File Offset: 0x00005015
		public string AppDomainFriendlyName
		{
			get
			{
				return this.appDomainFriendlyName;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006E1D File Offset: 0x0000501D
		public string ExtendedData
		{
			get
			{
				return this.extendedData;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00006E25 File Offset: 0x00005025
		public string HostReference
		{
			get
			{
				return this.hostReference;
			}
		}

		// Token: 0x040000C6 RID: 198
		private string serializedException;

		// Token: 0x040000C7 RID: 199
		private string eventSource;

		// Token: 0x040000C8 RID: 200
		private string appDomainFriendlyName;

		// Token: 0x040000C9 RID: 201
		private string extendedData;

		// Token: 0x040000CA RID: 202
		private string hostReference;
	}
}
