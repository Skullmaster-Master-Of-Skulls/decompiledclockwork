using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000286 RID: 646
	public interface IDbExecutionStrategy
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060016AC RID: 5804
		bool RetriesOnFailure { get; }

		// Token: 0x060016AD RID: 5805
		void Execute(Action operation);

		// Token: 0x060016AE RID: 5806
		TResult Execute<TResult>(Func<TResult> operation);

		// Token: 0x060016AF RID: 5807
		Task ExecuteAsync(Func<Task> operation, CancellationToken cancellationToken);

		// Token: 0x060016B0 RID: 5808
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken);
	}
}
