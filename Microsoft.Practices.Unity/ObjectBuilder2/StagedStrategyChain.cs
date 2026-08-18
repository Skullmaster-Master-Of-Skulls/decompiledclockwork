using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000072 RID: 114
	public class StagedStrategyChain<TStageEnum> : IStagedStrategyChain
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00006C58 File Offset: 0x00004E58
		public StagedStrategyChain()
		{
			this.stages = new List<IBuilderStrategy>[StagedStrategyChain<TStageEnum>.NumberOfEnumValues()];
			for (int i = 0; i < this.stages.Length; i++)
			{
				this.stages[i] = new List<IBuilderStrategy>();
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006CA6 File Offset: 0x00004EA6
		public StagedStrategyChain(StagedStrategyChain<TStageEnum> innerChain) : this()
		{
			this.innerChain = innerChain;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006CB8 File Offset: 0x00004EB8
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "We're converting an enum to an int, no need for globalization here.")]
		public void Add(IBuilderStrategy strategy, TStageEnum stage)
		{
			lock (this.lockObject)
			{
				this.stages[Convert.ToInt32(stage)].Add(strategy);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006D0C File Offset: 0x00004F0C
		[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "This is not a new version of Add, it adds a new strategy")]
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public void AddNew<TStrategy>(TStageEnum stage) where TStrategy : IBuilderStrategy, new()
		{
			this.Add((default(TStrategy) == null) ? Activator.CreateInstance<TStrategy>() : default(TStrategy), stage);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006D48 File Offset: 0x00004F48
		public void Clear()
		{
			lock (this.lockObject)
			{
				foreach (List<IBuilderStrategy> list in this.stages)
				{
					list.Clear();
				}
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public IStrategyChain MakeStrategyChain()
		{
			IStrategyChain result;
			lock (this.lockObject)
			{
				StrategyChain strategyChain = new StrategyChain();
				for (int i = 0; i < this.stages.Length; i++)
				{
					this.FillStrategyChain(strategyChain, i);
				}
				result = strategyChain;
			}
			return result;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006E08 File Offset: 0x00005008
		private void FillStrategyChain(StrategyChain chain, int index)
		{
			lock (this.lockObject)
			{
				if (this.innerChain != null)
				{
					this.innerChain.FillStrategyChain(chain, index);
				}
				chain.AddRange(this.stages[index]);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006E7A File Offset: 0x0000507A
		private static int NumberOfEnumValues()
		{
			return (from f in typeof(TStageEnum).GetTypeInfo().DeclaredFields
			where f.IsPublic && f.IsStatic
			select f).Count<FieldInfo>();
		}

		// Token: 0x0400006C RID: 108
		private readonly StagedStrategyChain<TStageEnum> innerChain;

		// Token: 0x0400006D RID: 109
		private readonly object lockObject = new object();

		// Token: 0x0400006E RID: 110
		private readonly List<IBuilderStrategy>[] stages;
	}
}
