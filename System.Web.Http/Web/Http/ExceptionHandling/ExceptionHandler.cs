using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x02000040 RID: 64
	public abstract class ExceptionHandler : IExceptionHandler
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00007738 File Offset: 0x00005938
		Task IExceptionHandler.HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionContext exceptionContext = context.ExceptionContext;
			if (!this.ShouldHandle(context))
			{
				return TaskHelpers.Completed();
			}
			return this.HandleAsync(context, cancellationToken);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007766 File Offset: 0x00005966
		public virtual Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			this.Handle(context);
			return TaskHelpers.Completed();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007774 File Offset: 0x00005974
		public virtual void Handle(ExceptionHandlerContext context)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007778 File Offset: 0x00005978
		public virtual bool ShouldHandle(ExceptionHandlerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionContext exceptionContext = context.ExceptionContext;
			ExceptionContextCatchBlock catchBlock = exceptionContext.CatchBlock;
			return catchBlock.IsTopLevel;
		}
	}
}
