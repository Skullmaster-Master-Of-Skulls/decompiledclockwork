using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x02000039 RID: 57
	internal class CompositeExceptionLogger : IExceptionLogger
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00007073 File Offset: 0x00005273
		public CompositeExceptionLogger(params IExceptionLogger[] loggers) : this((IEnumerable<IExceptionLogger>)loggers)
		{
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007081 File Offset: 0x00005281
		public CompositeExceptionLogger(IEnumerable<IExceptionLogger> loggers)
		{
			if (loggers == null)
			{
				throw new ArgumentNullException("loggers");
			}
			this._loggers = loggers.ToArray<IExceptionLogger>();
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000070A3 File Offset: 0x000052A3
		public IEnumerable<IExceptionLogger> Loggers
		{
			get
			{
				return this._loggers;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000070AC File Offset: 0x000052AC
		public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
		{
			List<Task> list = new List<Task>();
			foreach (IExceptionLogger exceptionLogger in this._loggers)
			{
				if (exceptionLogger == null)
				{
					throw new InvalidOperationException(Error.Format(SRResources.TypeInstanceMustNotBeNull, new object[]
					{
						typeof(IExceptionLogger).Name
					}));
				}
				Task item = exceptionLogger.LogAsync(context, cancellationToken);
				list.Add(item);
			}
			return Task.WhenAll(list);
		}

		// Token: 0x04000080 RID: 128
		private readonly IExceptionLogger[] _loggers;
	}
}
