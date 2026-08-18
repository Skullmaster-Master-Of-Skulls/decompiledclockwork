using System;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000309 RID: 777
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class TagPrefixAttribute : Attribute
	{
		// Token: 0x060023D6 RID: 9174 RVA: 0x00075123 File Offset: 0x00073323
		public TagPrefixAttribute(string namespaceName, string tagPrefix)
		{
			if (string.IsNullOrEmpty(namespaceName))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("namespaceName");
			}
			if (string.IsNullOrEmpty(tagPrefix))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("tagPrefix");
			}
			this.namespaceName = namespaceName;
			this.tagPrefix = tagPrefix;
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x0007515F File Offset: 0x0007335F
		public string NamespaceName
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x00075167 File Offset: 0x00073367
		public string TagPrefix
		{
			get
			{
				return this.tagPrefix;
			}
		}

		// Token: 0x04001CD7 RID: 7383
		private string namespaceName;

		// Token: 0x04001CD8 RID: 7384
		private string tagPrefix;
	}
}
