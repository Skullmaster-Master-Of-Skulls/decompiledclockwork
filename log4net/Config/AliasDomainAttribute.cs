using System;

namespace log4net.Config
{
	// Token: 0x02000049 RID: 73
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Obsolete("Use AliasRepositoryAttribute instead of AliasDomainAttribute")]
	[Serializable]
	public sealed class AliasDomainAttribute : AliasRepositoryAttribute
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00008DB7 File Offset: 0x00006FB7
		public AliasDomainAttribute(string name) : base(name)
		{
		}
	}
}
