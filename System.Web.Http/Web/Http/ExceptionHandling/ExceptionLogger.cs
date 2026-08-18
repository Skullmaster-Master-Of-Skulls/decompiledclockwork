using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x0200003C RID: 60
	public abstract class ExceptionLogger : IExceptionLogger
	{
		// Token: 0x0600015A RID: 346 RVA: 0x000073D6 File Offset: 0x000055D6
		Task IExceptionLogger.LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionContext exceptionContext = context.ExceptionContext;
			if (!this.ShouldLog(context))
			{
				return TaskHelpers.Completed();
			}
			return this.LogAsync(context, cancellationToken);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007404 File Offset: 0x00005604
		public virtual Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
		{
			this.Log(context);
			return TaskHelpers.Completed();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007412 File Offset: 0x00005612
		public virtual void Log(ExceptionLoggerContext context)
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007414 File Offset: 0x00005614
		public virtual bool ShouldLog(ExceptionLoggerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionContext exceptionContext = context.ExceptionContext;
			IDictionary data = exceptionContext.Exception.Data;
			if (data == null || data.IsReadOnly)
			{
				return true;
			}
			ICollection<object> collection;
			if (data.Contains("MS_LoggedBy"))
			{
				object obj = data["MS_LoggedBy"];
				collection = (obj as ICollection<object>);
				if (collection == null)
				{
					return true;
				}
				if (collection.Contains(this))
				{
					return false;
				}
			}
			else
			{
				collection = new List<object>();
				data.Add("MS_LoggedBy", collection);
			}
			collection.Add(this);
			return true;
		}

		// Token: 0x04000084 RID: 132
		internal const string LoggedByKey = "MS_LoggedBy";
	}
}
