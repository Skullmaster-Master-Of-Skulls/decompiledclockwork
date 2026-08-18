using System;

namespace log4net.Config
{
	// Token: 0x02000048 RID: 72
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public class AliasRepositoryAttribute : Attribute
	{
		// Token: 0x06000285 RID: 645 RVA: 0x00008D97 File Offset: 0x00006F97
		public AliasRepositoryAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00008DA6 File Offset: 0x00006FA6
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00008DAE File Offset: 0x00006FAE
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

		// Token: 0x04000144 RID: 324
		private string m_name;
	}
}
