using System;
using System.Reflection;
using log4net.Repository;

namespace log4net.Config
{
	// Token: 0x0200004B RID: 75
	[AttributeUsage(AttributeTargets.Assembly)]
	public abstract class ConfiguratorAttribute : Attribute, IComparable
	{
		// Token: 0x06000292 RID: 658 RVA: 0x00008F7E File Offset: 0x0000717E
		protected ConfiguratorAttribute(int priority)
		{
			this.m_priority = priority;
		}

		// Token: 0x06000293 RID: 659
		public abstract void Configure(Assembly sourceAssembly, ILoggerRepository targetRepository);

		// Token: 0x06000294 RID: 660 RVA: 0x00008F90 File Offset: 0x00007190
		public int CompareTo(object obj)
		{
			if (this == obj)
			{
				return 0;
			}
			int num = -1;
			ConfiguratorAttribute configuratorAttribute = obj as ConfiguratorAttribute;
			if (configuratorAttribute != null)
			{
				num = configuratorAttribute.m_priority.CompareTo(this.m_priority);
				if (num == 0)
				{
					num = -1;
				}
			}
			return num;
		}

		// Token: 0x04000146 RID: 326
		private int m_priority;
	}
}
