using System;
using log4net.Core;
using log4net.Util;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x020000D1 RID: 209
	public class RootLogger : Logger
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00012F99 File Offset: 0x00011199
		public RootLogger(Level level) : base("root")
		{
			this.Level = level;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00012FAD File Offset: 0x000111AD
		public override Level EffectiveLevel
		{
			get
			{
				return base.Level;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00012FB5 File Offset: 0x000111B5
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x00012FBD File Offset: 0x000111BD
		public override Level Level
		{
			get
			{
				return base.Level;
			}
			set
			{
				if (value == null)
				{
					LogLog.Error(RootLogger.declaringType, "You have tried to set a null level to root.", new LogException());
					return;
				}
				base.Level = value;
			}
		}

		// Token: 0x0400026B RID: 619
		private static readonly Type declaringType = typeof(RootLogger);
	}
}
