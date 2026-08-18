using System;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200014A RID: 330
	public class DatabaseLogger : IDisposable, IDbConfigurationInterceptor, IDbInterceptor
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x00036E97 File Offset: 0x00035097
		public DatabaseLogger()
		{
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00036EAA File Offset: 0x000350AA
		public DatabaseLogger(string path) : this(path, false)
		{
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00036EB4 File Offset: 0x000350B4
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public DatabaseLogger(string path, bool append)
		{
			Check.NotEmpty(path, "path");
			this._writer = new StreamWriter(path, append)
			{
				AutoFlush = true
			};
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00036EF4 File Offset: 0x000350F4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00036F03 File Offset: 0x00035103
		protected virtual void Dispose(bool disposing)
		{
			this.StopLogging();
			if (disposing && this._writer != null)
			{
				this._writer.Dispose();
				this._writer = null;
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00036F28 File Offset: 0x00035128
		public void StartLogging()
		{
			this.StartLogging(DbConfiguration.DependencyResolver);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00036F35 File Offset: 0x00035135
		public void StopLogging()
		{
			if (this._formatter != null)
			{
				DbInterception.Remove(this._formatter);
				this._formatter = null;
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00036F51 File Offset: 0x00035151
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		void IDbConfigurationInterceptor.Loaded(DbConfigurationLoadedEventArgs loadedEventArgs, DbConfigurationInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConfigurationLoadedEventArgs>(loadedEventArgs, "loadedEventArgs");
			Check.NotNull<DbConfigurationInterceptionContext>(interceptionContext, "interceptionContext");
			this.StartLogging(loadedEventArgs.DependencyResolver);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00036F78 File Offset: 0x00035178
		private void StartLogging(IDbDependencyResolver resolver)
		{
			if (this._formatter == null)
			{
				this._formatter = resolver.GetService<Func<DbContext, Action<string>, DatabaseLogFormatter>>()(null, (this._writer == null) ? new Action<string>(Console.Write) : new Action<string>(this.WriteThreadSafe));
				DbInterception.Add(this._formatter);
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00036FCC File Offset: 0x000351CC
		private void WriteThreadSafe(string value)
		{
			lock (this._lock)
			{
				this._writer.Write(value);
			}
		}

		// Token: 0x040002E4 RID: 740
		private TextWriter _writer;

		// Token: 0x040002E5 RID: 741
		private DatabaseLogFormatter _formatter;

		// Token: 0x040002E6 RID: 742
		private readonly object _lock = new object();
	}
}
