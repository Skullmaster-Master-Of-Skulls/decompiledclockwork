using System;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace System.Management.Instrumentation
{
	// Token: 0x02000289 RID: 649
	[AttributeUsage(AttributeTargets.Assembly)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class WmiConfigurationAttribute : Attribute
	{
		// Token: 0x060017FE RID: 6142 RVA: 0x0005719C File Offset: 0x0005539C
		public WmiConfigurationAttribute(string scope)
		{
			string text = scope;
			if (text != null)
			{
				text = text.Replace('/', '\\');
			}
			if (text == null || text.Length == 0)
			{
				text = "root\\default";
			}
			bool flag = true;
			foreach (string text2 in text.Split(new char[]
			{
				'\\'
			}))
			{
				if (text2.Length != 0 && (!flag || string.Compare(text2, "root", StringComparison.OrdinalIgnoreCase) == 0) && Regex.Match(text2, "^[a-z,A-Z]").Success && !Regex.Match(text2, "_$").Success)
				{
					bool success = Regex.Match(text2, "[^a-z,A-Z,0-9,_,\\u0080-\\uFFFF]").Success;
				}
				flag = false;
			}
			this._Scope = text;
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x0005725A File Offset: 0x0005545A
		// (set) Token: 0x06001800 RID: 6144 RVA: 0x00057262 File Offset: 0x00055462
		public string SecurityRestriction
		{
			get
			{
				return this._SecurityRestriction;
			}
			set
			{
				this._SecurityRestriction = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0005726B File Offset: 0x0005546B
		// (set) Token: 0x06001802 RID: 6146 RVA: 0x00057273 File Offset: 0x00055473
		public string NamespaceSecurity
		{
			get
			{
				return this._NamespaceSecurity;
			}
			set
			{
				this._NamespaceSecurity = value;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0005727C File Offset: 0x0005547C
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x00057284 File Offset: 0x00055484
		public bool IdentifyLevel
		{
			get
			{
				return this._IdentifyLevel;
			}
			set
			{
				this._IdentifyLevel = value;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0005728D File Offset: 0x0005548D
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x00057295 File Offset: 0x00055495
		public ManagementHostingModel HostingModel
		{
			get
			{
				return this._HostingModel;
			}
			set
			{
				this._HostingModel = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0005729E File Offset: 0x0005549E
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x000572A6 File Offset: 0x000554A6
		public string HostingGroup
		{
			get
			{
				return this._HostingGroup;
			}
			set
			{
				this._HostingGroup = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x000572AF File Offset: 0x000554AF
		public string Scope
		{
			get
			{
				return this._Scope;
			}
		}

		// Token: 0x04000B82 RID: 2946
		private string _Scope;

		// Token: 0x04000B83 RID: 2947
		private string _SecurityRestriction;

		// Token: 0x04000B84 RID: 2948
		private string _NamespaceSecurity;

		// Token: 0x04000B85 RID: 2949
		private ManagementHostingModel _HostingModel;

		// Token: 0x04000B86 RID: 2950
		private string _HostingGroup;

		// Token: 0x04000B87 RID: 2951
		private bool _IdentifyLevel = true;
	}
}
