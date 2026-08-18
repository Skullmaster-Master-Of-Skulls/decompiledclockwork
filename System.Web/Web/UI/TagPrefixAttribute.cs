using System;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200046E RID: 1134
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TagPrefixAttribute : Attribute
	{
		// Token: 0x060035A3 RID: 13731 RVA: 0x000E7E3A File Offset: 0x000E6E3A
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

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x060035A4 RID: 13732 RVA: 0x000E7E76 File Offset: 0x000E6E76
		public string NamespaceName
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x000E7E7E File Offset: 0x000E6E7E
		public string TagPrefix
		{
			get
			{
				return this.tagPrefix;
			}
		}

		// Token: 0x04002540 RID: 9536
		private string namespaceName;

		// Token: 0x04002541 RID: 9537
		private string tagPrefix;
	}
}
