using System;
using System.Collections.Generic;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x02000165 RID: 357
	public abstract class MethodCallTargetBase : Target
	{
		// Token: 0x06000D9E RID: 3486 RVA: 0x00020DAD File Offset: 0x0001EFAD
		protected MethodCallTargetBase()
		{
			this.Parameters = new List<MethodCallParameter>();
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00020DC0 File Offset: 0x0001EFC0
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x00020DC8 File Offset: 0x0001EFC8
		[ArrayParameter(typeof(MethodCallParameter), "parameter")]
		public IList<MethodCallParameter> Parameters { get; private set; }

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00020DD4 File Offset: 0x0001EFD4
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			object[] array = new object[this.Parameters.Count];
			int num = 0;
			foreach (MethodCallParameter methodCallParameter in this.Parameters)
			{
				array[num++] = methodCallParameter.GetValue(logEvent.LogEvent);
			}
			this.DoInvoke(array, logEvent.Continuation);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00020E50 File Offset: 0x0001F050
		protected virtual void DoInvoke(object[] parameters, AsyncContinuation continuation)
		{
			try
			{
				this.DoInvoke(parameters);
				continuation(null);
			}
			catch (Exception exception)
			{
				if (exception.MustBeRethrown())
				{
					throw;
				}
				continuation(exception);
			}
		}

		// Token: 0x06000DA3 RID: 3491
		protected abstract void DoInvoke(object[] parameters);
	}
}
