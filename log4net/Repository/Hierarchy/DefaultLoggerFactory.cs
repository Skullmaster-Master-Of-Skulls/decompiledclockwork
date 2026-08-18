using System;
using log4net.Core;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x020000C4 RID: 196
	internal class DefaultLoggerFactory : ILoggerFactory
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x0001185E File Offset: 0x0000FA5E
		internal DefaultLoggerFactory()
		{
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00011866 File Offset: 0x0000FA66
		public Logger CreateLogger(ILoggerRepository repository, string name)
		{
			if (name == null)
			{
				return new RootLogger(repository.LevelMap.LookupWithDefault(Level.Debug));
			}
			return new DefaultLoggerFactory.LoggerImpl(name);
		}

		// Token: 0x020000C6 RID: 198
		internal sealed class LoggerImpl : Logger
		{
			// Token: 0x060005C0 RID: 1472 RVA: 0x00011E89 File Offset: 0x00010089
			internal LoggerImpl(string name) : base(name)
			{
			}
		}
	}
}
