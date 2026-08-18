using System;

namespace System.Web.Configuration
{
	// Token: 0x020006B1 RID: 1713
	internal class CapabilitiesAssignment : CapabilitiesRule
	{
		// Token: 0x06005310 RID: 21264 RVA: 0x0012439C File Offset: 0x0012259C
		internal CapabilitiesAssignment(string var, CapabilitiesPattern pat)
		{
			this._type = 1;
			this._var = var;
			this._pat = pat;
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x001243B9 File Offset: 0x001225B9
		internal override void Evaluate(CapabilitiesState state)
		{
			state.SetVariable(this._var, this._pat.Expand(state));
			state.Exit = false;
		}

		// Token: 0x04002B88 RID: 11144
		internal string _var;

		// Token: 0x04002B89 RID: 11145
		internal CapabilitiesPattern _pat;
	}
}
