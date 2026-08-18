using System;
using System.IO;
using System.Reflection;
using System.Xml;
using log4net.Repository;

namespace log4net.Config
{
	// Token: 0x0200004E RID: 78
	[Obsolete("Use XmlConfigurator instead of DOMConfigurator")]
	public sealed class DOMConfigurator
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00009011 File Offset: 0x00007211
		private DOMConfigurator()
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009019 File Offset: 0x00007219
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		public static void Configure()
		{
			XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000902B File Offset: 0x0000722B
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		public static void Configure(ILoggerRepository repository)
		{
			XmlConfigurator.Configure(repository);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009034 File Offset: 0x00007234
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		public static void Configure(XmlElement element)
		{
			XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()), element);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00009047 File Offset: 0x00007247
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		public static void Configure(ILoggerRepository repository, XmlElement element)
		{
			XmlConfigurator.Configure(repository, element);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009051 File Offset: 0x00007251
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		public static void Configure(FileInfo configFile)
		{
			XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()), configFile);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00009064 File Offset: 0x00007264
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		public static void Configure(Stream configStream)
		{
			XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()), configStream);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009077 File Offset: 0x00007277
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		public static void Configure(ILoggerRepository repository, FileInfo configFile)
		{
			XmlConfigurator.Configure(repository, configFile);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009081 File Offset: 0x00007281
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		public static void Configure(ILoggerRepository repository, Stream configStream)
		{
			XmlConfigurator.Configure(repository, configStream);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000908B File Offset: 0x0000728B
		[Obsolete("Use XmlConfigurator.ConfigureAndWatch instead of DOMConfigurator.ConfigureAndWatch")]
		public static void ConfigureAndWatch(FileInfo configFile)
		{
			XmlConfigurator.ConfigureAndWatch(LogManager.GetRepository(Assembly.GetCallingAssembly()), configFile);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000909E File Offset: 0x0000729E
		[Obsolete("Use XmlConfigurator.ConfigureAndWatch instead of DOMConfigurator.ConfigureAndWatch")]
		public static void ConfigureAndWatch(ILoggerRepository repository, FileInfo configFile)
		{
			XmlConfigurator.ConfigureAndWatch(repository, configFile);
		}
	}
}
