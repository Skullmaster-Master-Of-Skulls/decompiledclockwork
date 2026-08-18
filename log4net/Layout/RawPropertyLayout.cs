using System;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x020000AF RID: 175
	public class RawPropertyLayout : IRawLayout
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000FF6A File Offset: 0x0000E16A
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x0000FF72 File Offset: 0x0000E172
		public string Key
		{
			get
			{
				return this.m_key;
			}
			set
			{
				this.m_key = value;
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000FF7B File Offset: 0x0000E17B
		public virtual object Format(LoggingEvent loggingEvent)
		{
			return loggingEvent.LookupProperty(this.m_key);
		}

		// Token: 0x04000213 RID: 531
		private string m_key;
	}
}
