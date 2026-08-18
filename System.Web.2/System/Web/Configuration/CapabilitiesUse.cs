using System;

namespace System.Web.Configuration
{
	// Token: 0x020006B6 RID: 1718
	internal class CapabilitiesUse : CapabilitiesRule
	{
		// Token: 0x06005328 RID: 21288 RVA: 0x00124955 File Offset: 0x00122B55
		internal CapabilitiesUse(string var, string asParam)
		{
			this._var = var;
			this._as = asParam;
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x0012496B File Offset: 0x00122B6B
		internal override void Evaluate(CapabilitiesState state)
		{
			state.SetVariable(this._as, state.ResolveServerVariable(this._var));
			state.Exit = false;
		}

		// Token: 0x04002BA2 RID: 11170
		internal string _var;

		// Token: 0x04002BA3 RID: 11171
		internal string _as;
	}
}
