using System;
using System.Collections.Specialized;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	public class LookupMethod
	{
		// Token: 0x06000059 RID: 89 RVA: 0x000048EE File Offset: 0x00002AEE
		public LookupMethod(string lookupMethodType, string argsString)
		{
			this._lookupMethodType = lookupMethodType;
			this._args = Utility.ParseArgs(argsString);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000490B File Offset: 0x00002B0B
		public LookupMethod(string lookupMethodType, StringDictionary args)
		{
			this._lookupMethodType = lookupMethodType;
			this._args = args;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00004923 File Offset: 0x00002B23
		public string LookupMethodType
		{
			get
			{
				return this._lookupMethodType;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000492B File Offset: 0x00002B2B
		public StringDictionary Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004934 File Offset: 0x00002B34
		public string GetArgSafe(string argName)
		{
			return this._args.ContainsKey(argName) ? this._args[argName] : "";
		}

		// Token: 0x04000017 RID: 23
		private readonly string _lookupMethodType;

		// Token: 0x04000018 RID: 24
		private readonly StringDictionary _args;
	}
}
