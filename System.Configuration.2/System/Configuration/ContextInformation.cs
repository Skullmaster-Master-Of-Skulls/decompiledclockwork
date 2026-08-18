using System;

namespace System.Configuration
{
	// Token: 0x0200004D RID: 77
	public sealed class ContextInformation
	{
		// Token: 0x06000335 RID: 821 RVA: 0x0001298A File Offset: 0x00010B8A
		internal ContextInformation(BaseConfigurationRecord configRecord)
		{
			this._hostingContextEvaluated = false;
			this._hostingContext = null;
			this._configRecord = configRecord;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000336 RID: 822 RVA: 0x000129A7 File Offset: 0x00010BA7
		public object HostingContext
		{
			get
			{
				if (!this._hostingContextEvaluated)
				{
					this._hostingContext = this._configRecord.ConfigContext;
					this._hostingContextEvaluated = true;
				}
				return this._hostingContext;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000129CF File Offset: 0x00010BCF
		public bool IsMachineLevel
		{
			get
			{
				return this._configRecord.IsMachineConfig;
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000129DC File Offset: 0x00010BDC
		public object GetSection(string sectionName)
		{
			return this._configRecord.GetSection(sectionName);
		}

		// Token: 0x04000243 RID: 579
		private bool _hostingContextEvaluated;

		// Token: 0x04000244 RID: 580
		private object _hostingContext;

		// Token: 0x04000245 RID: 581
		private BaseConfigurationRecord _configRecord;
	}
}
