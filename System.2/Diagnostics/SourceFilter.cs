using System;

namespace System.Diagnostics
{
	// Token: 0x020004A3 RID: 1187
	public class SourceFilter : TraceFilter
	{
		// Token: 0x06002C09 RID: 11273 RVA: 0x000C7061 File Offset: 0x000C5261
		public SourceFilter(string source)
		{
			this.Source = source;
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x000C7070 File Offset: 0x000C5270
		public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return string.Equals(this.src, source);
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x000C708C File Offset: 0x000C528C
		// (set) Token: 0x06002C0C RID: 11276 RVA: 0x000C7094 File Offset: 0x000C5294
		public string Source
		{
			get
			{
				return this.src;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("source");
				}
				this.src = value;
			}
		}

		// Token: 0x040026AA RID: 9898
		private string src;
	}
}
