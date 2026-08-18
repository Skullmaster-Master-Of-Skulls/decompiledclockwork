using System;
using WebGrease.Configuration;

namespace WebGrease.Preprocessing
{
	// Token: 0x020001B6 RID: 438
	public interface IPreprocessingEngine
	{
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600165F RID: 5727
		string Name { get; }

		// Token: 0x06001660 RID: 5728
		bool CanProcess(IWebGreaseContext context, ContentItem contentItem, PreprocessingConfig preprocessConfig = null);

		// Token: 0x06001661 RID: 5729
		ContentItem Process(IWebGreaseContext context, ContentItem contentItem, PreprocessingConfig preprocessingConfig, bool minimalOutput);
	}
}
