using System;

namespace log4net.Config
{
	// Token: 0x0200004D RID: 77
	[Obsolete("Use RepositoryAttribute instead of DomainAttribute")]
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class DomainAttribute : RepositoryAttribute
	{
		// Token: 0x0600029B RID: 667 RVA: 0x00009000 File Offset: 0x00007200
		public DomainAttribute()
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009008 File Offset: 0x00007208
		public DomainAttribute(string name) : base(name)
		{
		}
	}
}
