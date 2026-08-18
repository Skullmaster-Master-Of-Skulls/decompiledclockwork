using System;
using System.Collections.Specialized;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class AuthenticationMethod
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000040BC File Offset: 0x000022BC
		public AuthenticationMethod(string type, string name, string args)
		{
			this.type = type;
			this.name = name;
			this.args = Utility.ParseArgs(args);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000040E0 File Offset: 0x000022E0
		public AuthenticationMethod(string type, string name, StringDictionary args)
		{
			this.type = type;
			this.name = name;
			this.args = args;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004100 File Offset: 0x00002300
		public string GetArgSafe(string argName)
		{
			return this.args.ContainsKey(argName) ? this.args[argName] : "";
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004134 File Offset: 0x00002334
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000419C File Offset: 0x0000239C
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000041B4 File Offset: 0x000023B4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000041CC File Offset: 0x000023CC
		public StringDictionary Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000041E4 File Offset: 0x000023E4
		public bool Is(string name)
		{
			return this.name.CompareTo(name) == 0;
		}

		// Token: 0x04000011 RID: 17
		private string type;

		// Token: 0x04000012 RID: 18
		private string name;

		// Token: 0x04000013 RID: 19
		private StringDictionary args;
	}
}
