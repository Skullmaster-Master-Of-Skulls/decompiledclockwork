using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000645 RID: 1605
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060039EB RID: 14827 RVA: 0x000C2970 File Offset: 0x000C1970
		public StrongNameIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000C2979 File Offset: 0x000C1979
		// (set) Token: 0x060039ED RID: 14829 RVA: 0x000C2981 File Offset: 0x000C1981
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060039EE RID: 14830 RVA: 0x000C298A File Offset: 0x000C198A
		// (set) Token: 0x060039EF RID: 14831 RVA: 0x000C2992 File Offset: 0x000C1992
		public string Version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				this.m_version = value;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000C299B File Offset: 0x000C199B
		// (set) Token: 0x060039F1 RID: 14833 RVA: 0x000C29A3 File Offset: 0x000C19A3
		public string PublicKey
		{
			get
			{
				return this.m_blob;
			}
			set
			{
				this.m_blob = value;
			}
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x000C29AC File Offset: 0x000C19AC
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new StrongNameIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_blob == null && this.m_name == null && this.m_version == null)
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			if (this.m_blob == null)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Key"));
			}
			StrongNamePublicKeyBlob blob = new StrongNamePublicKeyBlob(this.m_blob);
			if (this.m_version == null || this.m_version.Equals(string.Empty))
			{
				return new StrongNameIdentityPermission(blob, this.m_name, null);
			}
			return new StrongNameIdentityPermission(blob, this.m_name, new Version(this.m_version));
		}

		// Token: 0x04001E17 RID: 7703
		private string m_name;

		// Token: 0x04001E18 RID: 7704
		private string m_version;

		// Token: 0x04001E19 RID: 7705
		private string m_blob;
	}
}
