using System;
using System.Security.Policy;

namespace System.Security.Permissions
{
	// Token: 0x02000653 RID: 1619
	[Serializable]
	internal sealed class StrongName2
	{
		// Token: 0x06003A62 RID: 14946 RVA: 0x000C47C5 File Offset: 0x000C37C5
		public StrongName2(StrongNamePublicKeyBlob publicKeyBlob, string name, Version version)
		{
			this.m_publicKeyBlob = publicKeyBlob;
			this.m_name = name;
			this.m_version = version;
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x000C47E2 File Offset: 0x000C37E2
		public StrongName2 Copy()
		{
			return new StrongName2(this.m_publicKeyBlob, this.m_name, this.m_version);
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000C47FC File Offset: 0x000C37FC
		public bool IsSubsetOf(StrongName2 target)
		{
			return this.m_publicKeyBlob == null || (this.m_publicKeyBlob.Equals(target.m_publicKeyBlob) && (this.m_name == null || (target.m_name != null && StrongName.CompareNames(target.m_name, this.m_name))) && (this.m_version == null || (target.m_version != null && target.m_version.CompareTo(this.m_version) == 0)));
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000C4873 File Offset: 0x000C3873
		public StrongName2 Intersect(StrongName2 target)
		{
			if (target.IsSubsetOf(this))
			{
				return target.Copy();
			}
			if (this.IsSubsetOf(target))
			{
				return this.Copy();
			}
			return null;
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000C4896 File Offset: 0x000C3896
		public bool Equals(StrongName2 target)
		{
			return target.IsSubsetOf(this) && this.IsSubsetOf(target);
		}

		// Token: 0x04001E55 RID: 7765
		public StrongNamePublicKeyBlob m_publicKeyBlob;

		// Token: 0x04001E56 RID: 7766
		public string m_name;

		// Token: 0x04001E57 RID: 7767
		public Version m_version;
	}
}
