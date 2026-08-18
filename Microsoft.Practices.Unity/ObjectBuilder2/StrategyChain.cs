using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000073 RID: 115
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "See IStrategyChain")]
	public class StrategyChain : IStrategyChain, IEnumerable<IBuilderStrategy>, IEnumerable
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00006EB7 File Offset: 0x000050B7
		public StrategyChain()
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006ECA File Offset: 0x000050CA
		public StrategyChain(IEnumerable strategies)
		{
			this.AddRange(strategies);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006EE4 File Offset: 0x000050E4
		public void Add(IBuilderStrategy strategy)
		{
			this.strategies.Add(strategy);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006EF4 File Offset: 0x000050F4
		public void AddRange(IEnumerable strategyEnumerable)
		{
			Guard.ArgumentNotNull(strategyEnumerable, "strategyEnumerable");
			foreach (object obj in strategyEnumerable)
			{
				IBuilderStrategy strategy = (IBuilderStrategy)obj;
				this.Add(strategy);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006F54 File Offset: 0x00005154
		public IStrategyChain Reverse()
		{
			List<IBuilderStrategy> list = new List<IBuilderStrategy>(this.strategies);
			list.Reverse();
			return new StrategyChain(list);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006F7C File Offset: 0x0000517C
		public object ExecuteBuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			int i = 0;
			object existing;
			try
			{
				while (i < this.strategies.Count && !context.BuildComplete)
				{
					this.strategies[i].PreBuildUp(context);
					i++;
				}
				if (context.BuildComplete)
				{
					i--;
				}
				for (i--; i >= 0; i--)
				{
					this.strategies[i].PostBuildUp(context);
				}
				existing = context.Existing;
			}
			catch (Exception)
			{
				context.RecoveryStack.ExecuteRecovery();
				throw;
			}
			return existing;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007018 File Offset: 0x00005218
		public void ExecuteTearDown(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			int i = 0;
			try
			{
				while (i < this.strategies.Count)
				{
					if (context.BuildComplete)
					{
						i--;
						break;
					}
					this.strategies[i].PreTearDown(context);
					i++;
				}
				for (i--; i >= 0; i--)
				{
					this.strategies[i].PostTearDown(context);
				}
			}
			catch (Exception)
			{
				context.RecoveryStack.ExecuteRecovery();
				throw;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000070A8 File Offset: 0x000052A8
		IEnumerator<IBuilderStrategy> IEnumerable<IBuilderStrategy>.GetEnumerator()
		{
			return this.strategies.GetEnumerator();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000070BA File Offset: 0x000052BA
		public IEnumerator GetEnumerator()
		{
			return this.strategies.GetEnumerator();
		}

		// Token: 0x04000070 RID: 112
		private readonly List<IBuilderStrategy> strategies = new List<IBuilderStrategy>();
	}
}
