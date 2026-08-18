using System;

namespace log4net.Util
{
	// Token: 0x020000FB RID: 251
	public static class ILogExtensions
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x0001644C File Offset: 0x0001464C
		public static void DebugExt(this ILog logger, Func<object> callback)
		{
			try
			{
				if (logger.IsDebugEnabled)
				{
					logger.Debug(callback());
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00016494 File Offset: 0x00014694
		public static void DebugExt(this ILog logger, Func<object> callback, Exception exception)
		{
			try
			{
				if (logger.IsDebugEnabled)
				{
					logger.Debug(callback(), exception);
				}
			}
			catch (Exception exception2)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception2);
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000164E0 File Offset: 0x000146E0
		public static void DebugExt(this ILog logger, object message)
		{
			try
			{
				if (logger.IsDebugEnabled)
				{
					logger.Debug(message);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00016524 File Offset: 0x00014724
		public static void DebugExt(this ILog logger, object message, Exception exception)
		{
			try
			{
				if (logger.IsDebugEnabled)
				{
					logger.Debug(message, exception);
				}
			}
			catch (Exception exception2)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception2);
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00016568 File Offset: 0x00014768
		public static void DebugFormatExt(this ILog logger, string format, object arg0)
		{
			try
			{
				if (logger.IsDebugEnabled)
				{
					logger.DebugFormat(format, arg0);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000165AC File Offset: 0x000147AC
		public static void DebugFormatExt(this ILog logger, string format, params object[] args)
		{
			try
			{
				if (logger.IsDebugEnabled)
				{
					logger.DebugFormat(format, args);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000165F0 File Offset: 0x000147F0
		public static void DebugFormatExt(this ILog logger, IFormatProvider provider, string format, params object[] args)
		{
			try
			{
				if (logger.IsDebugEnabled)
				{
					logger.DebugFormat(provider, format, args);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00016638 File Offset: 0x00014838
		public static void DebugFormatExt(this ILog logger, string format, object arg0, object arg1)
		{
			try
			{
				if (logger.IsDebugEnabled)
				{
					logger.DebugFormat(format, arg0, arg1);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00016680 File Offset: 0x00014880
		public static void DebugFormatExt(this ILog logger, string format, object arg0, object arg1, object arg2)
		{
			try
			{
				if (logger.IsDebugEnabled)
				{
					logger.DebugFormat(format, arg0, arg1, arg2);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000166C8 File Offset: 0x000148C8
		public static void InfoExt(this ILog logger, Func<object> callback)
		{
			try
			{
				if (logger.IsInfoEnabled)
				{
					logger.Info(callback());
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00016710 File Offset: 0x00014910
		public static void InfoExt(this ILog logger, Func<object> callback, Exception exception)
		{
			try
			{
				if (logger.IsInfoEnabled)
				{
					logger.Info(callback(), exception);
				}
			}
			catch (Exception exception2)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception2);
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001675C File Offset: 0x0001495C
		public static void InfoExt(this ILog logger, object message)
		{
			try
			{
				if (logger.IsInfoEnabled)
				{
					logger.Info(message);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000167A0 File Offset: 0x000149A0
		public static void InfoExt(this ILog logger, object message, Exception exception)
		{
			try
			{
				if (logger.IsInfoEnabled)
				{
					logger.Info(message, exception);
				}
			}
			catch (Exception exception2)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception2);
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000167E4 File Offset: 0x000149E4
		public static void InfoFormatExt(this ILog logger, string format, object arg0)
		{
			try
			{
				if (logger.IsInfoEnabled)
				{
					logger.InfoFormat(format, arg0);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00016828 File Offset: 0x00014A28
		public static void InfoFormatExt(this ILog logger, string format, params object[] args)
		{
			try
			{
				if (logger.IsInfoEnabled)
				{
					logger.InfoFormat(format, args);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001686C File Offset: 0x00014A6C
		public static void InfoFormatExt(this ILog logger, IFormatProvider provider, string format, params object[] args)
		{
			try
			{
				if (logger.IsInfoEnabled)
				{
					logger.InfoFormat(provider, format, args);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000168B4 File Offset: 0x00014AB4
		public static void InfoFormatExt(this ILog logger, string format, object arg0, object arg1)
		{
			try
			{
				if (logger.IsInfoEnabled)
				{
					logger.InfoFormat(format, arg0, arg1);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000168FC File Offset: 0x00014AFC
		public static void InfoFormatExt(this ILog logger, string format, object arg0, object arg1, object arg2)
		{
			try
			{
				if (logger.IsInfoEnabled)
				{
					logger.InfoFormat(format, arg0, arg1, arg2);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00016944 File Offset: 0x00014B44
		public static void WarnExt(this ILog logger, Func<object> callback)
		{
			try
			{
				if (logger.IsWarnEnabled)
				{
					logger.Warn(callback());
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001698C File Offset: 0x00014B8C
		public static void WarnExt(this ILog logger, Func<object> callback, Exception exception)
		{
			try
			{
				if (logger.IsWarnEnabled)
				{
					logger.Warn(callback(), exception);
				}
			}
			catch (Exception exception2)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception2);
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000169D8 File Offset: 0x00014BD8
		public static void WarnExt(this ILog logger, object message)
		{
			try
			{
				if (logger.IsWarnEnabled)
				{
					logger.Warn(message);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00016A1C File Offset: 0x00014C1C
		public static void WarnExt(this ILog logger, object message, Exception exception)
		{
			try
			{
				if (logger.IsWarnEnabled)
				{
					logger.Warn(message, exception);
				}
			}
			catch (Exception exception2)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception2);
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00016A60 File Offset: 0x00014C60
		public static void WarnFormatExt(this ILog logger, string format, object arg0)
		{
			try
			{
				if (logger.IsWarnEnabled)
				{
					logger.WarnFormat(format, arg0);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00016AA4 File Offset: 0x00014CA4
		public static void WarnFormatExt(this ILog logger, string format, params object[] args)
		{
			try
			{
				if (logger.IsWarnEnabled)
				{
					logger.WarnFormat(format, args);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00016AE8 File Offset: 0x00014CE8
		public static void WarnFormatExt(this ILog logger, IFormatProvider provider, string format, params object[] args)
		{
			try
			{
				if (logger.IsWarnEnabled)
				{
					logger.WarnFormat(provider, format, args);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00016B30 File Offset: 0x00014D30
		public static void WarnFormatExt(this ILog logger, string format, object arg0, object arg1)
		{
			try
			{
				if (logger.IsWarnEnabled)
				{
					logger.WarnFormat(format, arg0, arg1);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00016B78 File Offset: 0x00014D78
		public static void WarnFormatExt(this ILog logger, string format, object arg0, object arg1, object arg2)
		{
			try
			{
				if (logger.IsWarnEnabled)
				{
					logger.WarnFormat(format, arg0, arg1, arg2);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public static void ErrorExt(this ILog logger, Func<object> callback)
		{
			try
			{
				if (logger.IsErrorEnabled)
				{
					logger.Error(callback());
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00016C08 File Offset: 0x00014E08
		public static void ErrorExt(this ILog logger, Func<object> callback, Exception exception)
		{
			try
			{
				if (logger.IsErrorEnabled)
				{
					logger.Error(callback(), exception);
				}
			}
			catch (Exception exception2)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception2);
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00016C54 File Offset: 0x00014E54
		public static void ErrorExt(this ILog logger, object message)
		{
			try
			{
				if (logger.IsErrorEnabled)
				{
					logger.Error(message);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00016C98 File Offset: 0x00014E98
		public static void ErrorExt(this ILog logger, object message, Exception exception)
		{
			try
			{
				if (logger.IsErrorEnabled)
				{
					logger.Error(message, exception);
				}
			}
			catch (Exception exception2)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception2);
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00016CDC File Offset: 0x00014EDC
		public static void ErrorFormatExt(this ILog logger, string format, object arg0)
		{
			try
			{
				if (logger.IsErrorEnabled)
				{
					logger.ErrorFormat(format, arg0);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00016D20 File Offset: 0x00014F20
		public static void ErrorFormatExt(this ILog logger, string format, params object[] args)
		{
			try
			{
				if (logger.IsErrorEnabled)
				{
					logger.ErrorFormat(format, args);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00016D64 File Offset: 0x00014F64
		public static void ErrorFormatExt(this ILog logger, IFormatProvider provider, string format, params object[] args)
		{
			try
			{
				if (logger.IsErrorEnabled)
				{
					logger.ErrorFormat(provider, format, args);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00016DAC File Offset: 0x00014FAC
		public static void ErrorFormatExt(this ILog logger, string format, object arg0, object arg1)
		{
			try
			{
				if (logger.IsErrorEnabled)
				{
					logger.ErrorFormat(format, arg0, arg1);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00016DF4 File Offset: 0x00014FF4
		public static void ErrorFormatExt(this ILog logger, string format, object arg0, object arg1, object arg2)
		{
			try
			{
				if (logger.IsErrorEnabled)
				{
					logger.ErrorFormat(format, arg0, arg1, arg2);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00016E3C File Offset: 0x0001503C
		public static void FatalExt(this ILog logger, Func<object> callback)
		{
			try
			{
				if (logger.IsFatalEnabled)
				{
					logger.Fatal(callback());
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00016E84 File Offset: 0x00015084
		public static void FatalExt(this ILog logger, Func<object> callback, Exception exception)
		{
			try
			{
				if (logger.IsFatalEnabled)
				{
					logger.Fatal(callback(), exception);
				}
			}
			catch (Exception exception2)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception2);
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00016ED0 File Offset: 0x000150D0
		public static void FatalExt(this ILog logger, object message)
		{
			try
			{
				if (logger.IsFatalEnabled)
				{
					logger.Fatal(message);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00016F14 File Offset: 0x00015114
		public static void FatalExt(this ILog logger, object message, Exception exception)
		{
			try
			{
				if (logger.IsFatalEnabled)
				{
					logger.Fatal(message, exception);
				}
			}
			catch (Exception exception2)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception2);
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00016F58 File Offset: 0x00015158
		public static void FatalFormatExt(this ILog logger, string format, object arg0)
		{
			try
			{
				if (logger.IsFatalEnabled)
				{
					logger.FatalFormat(format, arg0);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00016F9C File Offset: 0x0001519C
		public static void FatalFormatExt(this ILog logger, string format, params object[] args)
		{
			try
			{
				if (logger.IsFatalEnabled)
				{
					logger.FatalFormat(format, args);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00016FE0 File Offset: 0x000151E0
		public static void FatalFormatExt(this ILog logger, IFormatProvider provider, string format, params object[] args)
		{
			try
			{
				if (logger.IsFatalEnabled)
				{
					logger.FatalFormat(provider, format, args);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00017028 File Offset: 0x00015228
		public static void FatalFormatExt(this ILog logger, string format, object arg0, object arg1)
		{
			try
			{
				if (logger.IsFatalEnabled)
				{
					logger.FatalFormat(format, arg0, arg1);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00017070 File Offset: 0x00015270
		public static void FatalFormatExt(this ILog logger, string format, object arg0, object arg1, object arg2)
		{
			try
			{
				if (logger.IsFatalEnabled)
				{
					logger.FatalFormat(format, arg0, arg1, arg2);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(ILogExtensions.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x040002B2 RID: 690
		private static readonly Type declaringType = typeof(ILogExtensions);
	}
}
