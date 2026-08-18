using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004B0 RID: 1200
	internal struct FilterResult
	{
		// Token: 0x06002DD7 RID: 11735 RVA: 0x000B2B56 File Offset: 0x000B0D56
		internal FilterResult(QueryProcessor processor)
		{
			this.processor = processor;
			this.result = this.processor.Result;
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000B2B70 File Offset: 0x000B0D70
		internal FilterResult(bool result)
		{
			this.processor = null;
			this.result = result;
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x000B2B80 File Offset: 0x000B0D80
		internal QueryProcessor Processor
		{
			get
			{
				return this.processor;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002DDA RID: 11738 RVA: 0x000B2B88 File Offset: 0x000B0D88
		internal bool Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000B2B90 File Offset: 0x000B0D90
		internal MessageFilter GetSingleMatch()
		{
			Collection<MessageFilter> matchList = this.processor.MatchList;
			int count = matchList.Count;
			MessageFilter messageFilter;
			if (count != 0)
			{
				if (count != 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, matchList));
				}
				messageFilter = matchList[0];
			}
			else
			{
				messageFilter = null;
			}
			return messageFilter;
		}

		// Token: 0x040024EA RID: 9450
		private QueryProcessor processor;

		// Token: 0x040024EB RID: 9451
		private bool result;
	}
}
