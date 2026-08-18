using System;
using System.Collections;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace System.Web.Configuration
{
	// Token: 0x020006B5 RID: 1717
	internal class CapabilitiesState
	{
		// Token: 0x0600531B RID: 21275 RVA: 0x00124764 File Offset: 0x00122964
		internal CapabilitiesState(HttpRequest request, IDictionary values)
		{
			this._request = request;
			this._values = values;
			this._matchlist = new ArrayList();
			this._regexlist = new ArrayList();
		}

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x0600531C RID: 21276 RVA: 0x00124790 File Offset: 0x00122990
		// (set) Token: 0x0600531D RID: 21277 RVA: 0x00124798 File Offset: 0x00122998
		internal bool EvaluateOnlyUserAgent
		{
			get
			{
				return this._evaluateOnlyUserAgent;
			}
			set
			{
				this._evaluateOnlyUserAgent = value;
			}
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x001247A1 File Offset: 0x001229A1
		internal virtual void ClearMatch()
		{
			if (this._matchlist == null)
			{
				this._regexlist = new ArrayList();
				this._matchlist = new ArrayList();
				return;
			}
			this._regexlist.Clear();
			this._matchlist.Clear();
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x001247D8 File Offset: 0x001229D8
		internal virtual void AddMatch(DelayedRegex regex, Match match)
		{
			this._regexlist.Add(regex);
			this._matchlist.Add(match);
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x001247F4 File Offset: 0x001229F4
		internal virtual void PopMatch()
		{
			this._regexlist.RemoveAt(this._regexlist.Count - 1);
			this._matchlist.RemoveAt(this._matchlist.Count - 1);
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x00124828 File Offset: 0x00122A28
		internal virtual string ResolveReference(string refname)
		{
			if (this._matchlist == null)
			{
				return string.Empty;
			}
			int i = this._matchlist.Count;
			while (i > 0)
			{
				i--;
				int num = ((DelayedRegex)this._regexlist[i]).GroupNumberFromName(refname);
				if (num >= 0)
				{
					Group group = ((Match)this._matchlist[i]).Groups[num];
					if (group.Success)
					{
						return group.ToString();
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x001248A8 File Offset: 0x00122AA8
		[AspNetHostingPermission(SecurityAction.Assert, Level = AspNetHostingPermissionLevel.Low)]
		private string ResolveServerVariableWithAssert(string varname)
		{
			string text = this._request.ServerVariables[varname];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x06005323 RID: 21283 RVA: 0x001248D1 File Offset: 0x00122AD1
		internal virtual string ResolveServerVariable(string varname)
		{
			if (varname.Length == 0 || varname == "HTTP_USER_AGENT")
			{
				return HttpCapabilitiesDefaultProvider.GetUserAgent(this._request);
			}
			if (this.EvaluateOnlyUserAgent)
			{
				return string.Empty;
			}
			return this.ResolveServerVariableWithAssert(varname);
		}

		// Token: 0x06005324 RID: 21284 RVA: 0x0012490C File Offset: 0x00122B0C
		internal virtual string ResolveVariable(string varname)
		{
			string text = (string)this._values[varname];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x00124935 File Offset: 0x00122B35
		internal virtual void SetVariable(string varname, string value)
		{
			this._values[varname] = value;
		}

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x06005326 RID: 21286 RVA: 0x00124944 File Offset: 0x00122B44
		// (set) Token: 0x06005327 RID: 21287 RVA: 0x0012494C File Offset: 0x00122B4C
		internal virtual bool Exit
		{
			get
			{
				return this._exit;
			}
			set
			{
				this._exit = value;
			}
		}

		// Token: 0x04002B9C RID: 11164
		internal HttpRequest _request;

		// Token: 0x04002B9D RID: 11165
		internal IDictionary _values;

		// Token: 0x04002B9E RID: 11166
		internal ArrayList _matchlist;

		// Token: 0x04002B9F RID: 11167
		internal ArrayList _regexlist;

		// Token: 0x04002BA0 RID: 11168
		internal bool _exit;

		// Token: 0x04002BA1 RID: 11169
		internal bool _evaluateOnlyUserAgent;
	}
}
