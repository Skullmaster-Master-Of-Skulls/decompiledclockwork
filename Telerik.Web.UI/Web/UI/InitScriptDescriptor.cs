using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FCE RID: 4046
	internal class InitScriptDescriptor : ScriptDescriptor
	{
		// Token: 0x06009D17 RID: 40215 RVA: 0x0022F409 File Offset: 0x0022D609
		public InitScriptDescriptor(string scriptCode)
		{
			this._code = scriptCode;
		}

		// Token: 0x06009D18 RID: 40216 RVA: 0x0022F418 File Offset: 0x0022D618
		protected override string GetScript()
		{
			return this._code;
		}

		// Token: 0x04002C39 RID: 11321
		private string _code;
	}
}
