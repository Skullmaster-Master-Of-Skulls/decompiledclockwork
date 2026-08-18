using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000076 RID: 118
	public sealed class VirtualDirectoryDefaults : ConfigurationElement
	{
		// Token: 0x06000366 RID: 870 RVA: 0x00008F0A File Offset: 0x00007F0A
		internal VirtualDirectoryDefaults() : this(null)
		{
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00008F13 File Offset: 0x00007F13
		internal VirtualDirectoryDefaults(VirtualDirectoryDefaults parentDefaults)
		{
			this._parentDefaults = parentDefaults;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00008F22 File Offset: 0x00007F22
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00008F34 File Offset: 0x00007F34
		public AuthenticationLogonMethod LogonMethod
		{
			get
			{
				return (AuthenticationLogonMethod)this.GetValue("logonMethod");
			}
			set
			{
				base["logonMethod"] = (int)value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00008F47 File Offset: 0x00007F47
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00008F59 File Offset: 0x00007F59
		public string Password
		{
			get
			{
				return (string)this.GetValue("password");
			}
			set
			{
				base["password"] = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00008F67 File Offset: 0x00007F67
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00008F79 File Offset: 0x00007F79
		public string UserName
		{
			get
			{
				return (string)this.GetValue("userName");
			}
			set
			{
				base["userName"] = value;
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00008F88 File Offset: 0x00007F88
		private object GetValue(string attributeName)
		{
			ConfigurationAttribute attribute = base.GetAttribute(attributeName);
			if (this._parentDefaults != null && attribute.IsInheritedFromDefaultValue)
			{
				return this._parentDefaults.GetValue(attributeName);
			}
			return attribute.Value;
		}

		// Token: 0x0400012D RID: 301
		private VirtualDirectoryDefaults _parentDefaults;
	}
}
