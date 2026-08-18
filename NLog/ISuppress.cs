using System;
using System.Threading.Tasks;

namespace NLog
{
	// Token: 0x0200006B RID: 107
	public interface ISuppress
	{
		// Token: 0x060002AA RID: 682
		void Swallow(Action action);

		// Token: 0x060002AB RID: 683
		T Swallow<T>(Func<T> func);

		// Token: 0x060002AC RID: 684
		T Swallow<T>(Func<T> func, T fallback);

		// Token: 0x060002AD RID: 685
		void Swallow(Task task);

		// Token: 0x060002AE RID: 686
		Task SwallowAsync(Task task);

		// Token: 0x060002AF RID: 687
		Task SwallowAsync(Func<Task> asyncAction);

		// Token: 0x060002B0 RID: 688
		Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc);

		// Token: 0x060002B1 RID: 689
		Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc, TResult fallback);
	}
}
