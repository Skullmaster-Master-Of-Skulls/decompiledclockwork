using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace System.Web.Configuration
{
	// Token: 0x020006B4 RID: 1716
	internal class CapabilitiesSection : CapabilitiesRule
	{
		// Token: 0x06005319 RID: 21273 RVA: 0x0012469B File Offset: 0x0012289B
		internal CapabilitiesSection(int type, DelayedRegex regex, CapabilitiesPattern expr, ArrayList rulelist)
		{
			this._type = type;
			this._regex = regex;
			this._expr = expr;
			this._rules = (CapabilitiesRule[])rulelist.ToArray(typeof(CapabilitiesRule));
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x001246D4 File Offset: 0x001228D4
		internal override void Evaluate(CapabilitiesState state)
		{
			state.Exit = false;
			if (this._regex != null)
			{
				Match match = this._regex.Match(this._expr.Expand(state));
				if (!match.Success)
				{
					return;
				}
				state.AddMatch(this._regex, match);
			}
			for (int i = 0; i < this._rules.Length; i++)
			{
				this._rules[i].Evaluate(state);
				if (state.Exit)
				{
					break;
				}
			}
			if (this._regex != null)
			{
				state.PopMatch();
			}
			state.Exit = (this.Type == 3);
		}

		// Token: 0x04002B99 RID: 11161
		internal CapabilitiesPattern _expr;

		// Token: 0x04002B9A RID: 11162
		internal DelayedRegex _regex;

		// Token: 0x04002B9B RID: 11163
		internal CapabilitiesRule[] _rules;
	}
}
