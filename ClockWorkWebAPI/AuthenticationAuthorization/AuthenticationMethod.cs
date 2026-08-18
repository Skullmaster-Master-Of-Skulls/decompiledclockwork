using System;
using System.Collections.Specialized;

namespace ClockWorkWebAPI.AuthenticationAuthorization
{
	// Token: 0x02000077 RID: 119
	[Obsolete("use TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Auth.AuthenticationMethod instead")]
	[Serializable]
	public class AuthenticationMethod
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x000286B4 File Offset: 0x000268B4
		public AuthenticationMethod(string type, string name, string args)
		{
			this.type = type;
			this.name = name;
			this.args = Utility.ParseArgs(args);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000286D8 File Offset: 0x000268D8
		public AuthenticationMethod(string type, string name, StringDictionary args)
		{
			this.type = type;
			this.name = name;
			this.args = args;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000286F8 File Offset: 0x000268F8
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

		// Token: 0x0600060E RID: 1550 RVA: 0x00028730 File Offset: 0x00026930
		public int GetArgIntSafe(string argName)
		{
			bool flag = this.args.ContainsKey(argName);
			int result;
			if (flag)
			{
				string text = this.args[argName].Trim();
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					try
					{
						return int.Parse(text);
					}
					catch
					{
						return 0;
					}
				}
				result = 0;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00028798 File Offset: 0x00026998
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x000287B0 File Offset: 0x000269B0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x000287C8 File Offset: 0x000269C8
		public StringDictionary Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000287E0 File Offset: 0x000269E0
		public bool Is(string name)
		{
			return this.name.CompareTo(name) == 0;
		}

		// Token: 0x0400032B RID: 811
		private string type;

		// Token: 0x0400032C RID: 812
		private string name;

		// Token: 0x0400032D RID: 813
		private StringDictionary args;
	}
}
