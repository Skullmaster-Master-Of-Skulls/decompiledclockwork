using System;
using System.Collections.Generic;

namespace WebGrease.Configuration
{
	// Token: 0x02000025 RID: 37
	public interface IFileSet
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002D5 RID: 725
		ResourcePivotGroupCollection ResourcePivots { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002D6 RID: 726
		IDictionary<string, PreprocessingConfig> Preprocessing { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002D7 RID: 727
		IDictionary<string, BundlingConfig> Bundling { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002D8 RID: 728
		string Output { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002D9 RID: 729
		IList<InputSpec> InputSpecs { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002DA RID: 730
		IList<string> LoadedConfigurationFiles { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002DB RID: 731
		IList<string> Locales { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002DC RID: 732
		IList<string> Themes { get; }
	}
}
