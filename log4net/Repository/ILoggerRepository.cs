using System;
using System.Collections;
using log4net.Appender;
using log4net.Core;
using log4net.ObjectRenderer;
using log4net.Plugin;
using log4net.Util;

namespace log4net.Repository
{
	// Token: 0x020000C9 RID: 201
	public interface ILoggerRepository
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005C7 RID: 1479
		// (set) Token: 0x060005C8 RID: 1480
		string Name { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005C9 RID: 1481
		RendererMap RendererMap { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005CA RID: 1482
		PluginMap PluginMap { get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005CB RID: 1483
		LevelMap LevelMap { get; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005CC RID: 1484
		// (set) Token: 0x060005CD RID: 1485
		Level Threshold { get; set; }

		// Token: 0x060005CE RID: 1486
		ILogger Exists(string name);

		// Token: 0x060005CF RID: 1487
		ILogger[] GetCurrentLoggers();

		// Token: 0x060005D0 RID: 1488
		ILogger GetLogger(string name);

		// Token: 0x060005D1 RID: 1489
		void Shutdown();

		// Token: 0x060005D2 RID: 1490
		void ResetConfiguration();

		// Token: 0x060005D3 RID: 1491
		void Log(LoggingEvent logEvent);

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005D4 RID: 1492
		// (set) Token: 0x060005D5 RID: 1493
		bool Configured { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005D6 RID: 1494
		// (set) Token: 0x060005D7 RID: 1495
		ICollection ConfigurationMessages { get; set; }

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060005D8 RID: 1496
		// (remove) Token: 0x060005D9 RID: 1497
		event LoggerRepositoryShutdownEventHandler ShutdownEvent;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060005DA RID: 1498
		// (remove) Token: 0x060005DB RID: 1499
		event LoggerRepositoryConfigurationResetEventHandler ConfigurationReset;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060005DC RID: 1500
		// (remove) Token: 0x060005DD RID: 1501
		event LoggerRepositoryConfigurationChangedEventHandler ConfigurationChanged;

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005DE RID: 1502
		PropertiesDictionary Properties { get; }

		// Token: 0x060005DF RID: 1503
		IAppender[] GetAppenders();
	}
}
