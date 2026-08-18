using System;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002BA RID: 698
	public class EventLogQuery
	{
		// Token: 0x06001965 RID: 6501 RVA: 0x0005CA0F File Offset: 0x0005AC0F
		public EventLogQuery(string path, PathType pathType) : this(path, pathType, null)
		{
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0005CA1A File Offset: 0x0005AC1A
		public EventLogQuery(string path, PathType pathType, string query)
		{
			this.session = EventLogSession.GlobalSession;
			this.path = path;
			this.pathType = pathType;
			if (query == null)
			{
				if (path == null)
				{
					throw new ArgumentNullException("path");
				}
			}
			else
			{
				this.query = query;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x0005CA53 File Offset: 0x0005AC53
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x0005CA5B File Offset: 0x0005AC5B
		public EventLogSession Session
		{
			get
			{
				return this.session;
			}
			set
			{
				this.session = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x0005CA64 File Offset: 0x0005AC64
		// (set) Token: 0x0600196A RID: 6506 RVA: 0x0005CA6C File Offset: 0x0005AC6C
		public bool TolerateQueryErrors
		{
			get
			{
				return this.tolerateErrors;
			}
			set
			{
				this.tolerateErrors = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x0005CA75 File Offset: 0x0005AC75
		// (set) Token: 0x0600196C RID: 6508 RVA: 0x0005CA7D File Offset: 0x0005AC7D
		public bool ReverseDirection
		{
			get
			{
				return this.reverseDirection;
			}
			set
			{
				this.reverseDirection = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x0005CA86 File Offset: 0x0005AC86
		internal string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x0005CA8E File Offset: 0x0005AC8E
		internal PathType ThePathType
		{
			get
			{
				return this.pathType;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x0005CA96 File Offset: 0x0005AC96
		internal string Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x04000C69 RID: 3177
		private string query;

		// Token: 0x04000C6A RID: 3178
		private string path;

		// Token: 0x04000C6B RID: 3179
		private EventLogSession session;

		// Token: 0x04000C6C RID: 3180
		private PathType pathType;

		// Token: 0x04000C6D RID: 3181
		private bool tolerateErrors;

		// Token: 0x04000C6E RID: 3182
		private bool reverseDirection;
	}
}
