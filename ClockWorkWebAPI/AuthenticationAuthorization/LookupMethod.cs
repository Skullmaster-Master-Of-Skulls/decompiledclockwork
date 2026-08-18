using System;
using System.Collections.Specialized;

namespace ClockWorkWebAPI.AuthenticationAuthorization
{
	// Token: 0x0200007A RID: 122
	[Serializable]
	public class LookupMethod
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x00028860 File Offset: 0x00026A60
		public LookupMethod(string lookupMethodType, string argsString)
		{
			this.lookupMethodType = lookupMethodType;
			this.args = Utility.ParseArgs(argsString);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002887D File Offset: 0x00026A7D
		public LookupMethod(string lookupMethodType, StringDictionary args)
		{
			this.lookupMethodType = lookupMethodType;
			this.args = args;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00028898 File Offset: 0x00026A98
		public string LookupMethodType
		{
			get
			{
				return this.lookupMethodType;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x000288B0 File Offset: 0x00026AB0
		public StringDictionary Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000288C8 File Offset: 0x00026AC8
		public string GetArgSafe(string argName)
		{
			bool flag = this.args.ContainsKey(argName);
			string result;
			if (flag)
			{
				result = this.args[argName];
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x04000330 RID: 816
		private string lookupMethodType;

		// Token: 0x04000331 RID: 817
		private StringDictionary args;
	}
}
