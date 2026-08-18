using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F51 RID: 3921
	internal class InvalidResourcesIndicatorScriptEntry : InvalidScriptEntry
	{
		// Token: 0x06009598 RID: 38296 RVA: 0x00216A56 File Offset: 0x00214C56
		public InvalidResourcesIndicatorScriptEntry(int invalidResourceCount) : base(string.Empty, string.Empty)
		{
			this._invalidResourceCount = invalidResourceCount;
		}

		// Token: 0x06009599 RID: 38297 RVA: 0x00216A6F File Offset: 0x00214C6F
		public override string GetScript()
		{
			return string.Format("/* Skipped loading {0} invalid resource{1}. */", this._invalidResourceCount, (this._invalidResourceCount == 1) ? string.Empty : "s");
		}

		// Token: 0x04002ACC RID: 10956
		private const string PluralSuffix = "s";

		// Token: 0x04002ACD RID: 10957
		private int _invalidResourceCount;
	}
}
