using System;
using System.Collections.Generic;
using System.Data.Objects.Internal;
using System.Linq.Expressions;
using System.Text;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003CC RID: 972
	internal sealed class CoordinatorFactory<TElement> : CoordinatorFactory
	{
		// Token: 0x06003478 RID: 13432 RVA: 0x000CA938 File Offset: 0x000C8B38
		public CoordinatorFactory(int depth, int stateSlot, Expression hasData, Expression setKeys, Expression checkKeys, CoordinatorFactory[] nestedCoordinators, Expression element, Expression elementWithErrorHandling, Expression initializeCollection, RecordStateFactory[] recordStateFactories) : base(depth, stateSlot, CoordinatorFactory<TElement>.CompilePredicate(hasData), CoordinatorFactory<TElement>.CompilePredicate(setKeys), CoordinatorFactory<TElement>.CompilePredicate(checkKeys), nestedCoordinators, recordStateFactories)
		{
			if (typeof(IEntityWrapper).IsAssignableFrom(element.Type))
			{
				this.WrappedElement = Translator.Compile<IEntityWrapper>(element);
				elementWithErrorHandling = Translator.Emit_UnwrapAndEnsureType(elementWithErrorHandling, typeof(TElement));
			}
			else
			{
				this.Element = Translator.Compile<TElement>(element);
			}
			this.ElementWithErrorHandling = Translator.Compile<TElement>(elementWithErrorHandling);
			Func<Shaper, ICollection<TElement>> initializeCollection2;
			if (initializeCollection != null)
			{
				initializeCollection2 = Translator.Compile<ICollection<TElement>>(initializeCollection);
			}
			else
			{
				initializeCollection2 = ((Shaper s) => new List<TElement>());
			}
			this.InitializeCollection = initializeCollection2;
			this.Description = new StringBuilder().Append("HasData: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(hasData)).Append("SetKeys: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(setKeys)).Append("CheckKeys: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(checkKeys)).Append("Element: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(element)).Append("ElementWithExceptionHandling: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(elementWithErrorHandling)).Append("InitializeCollection: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(initializeCollection)).ToString();
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000CAA80 File Offset: 0x000C8C80
		private static Func<Shaper, bool> CompilePredicate(Expression predicate)
		{
			Func<Shaper, bool> result;
			if (predicate == null)
			{
				result = null;
			}
			else
			{
				result = Translator.Compile<bool>(predicate);
			}
			return result;
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000CAA9C File Offset: 0x000C8C9C
		private static string DescribeExpression(Expression expression)
		{
			string result;
			if (expression == null)
			{
				result = "undefined";
			}
			else
			{
				result = expression.ToString();
			}
			return result;
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000CAABC File Offset: 0x000C8CBC
		internal override Coordinator CreateCoordinator(Coordinator parent, Coordinator next)
		{
			return new Coordinator<TElement>(this, parent, next);
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000CAAC8 File Offset: 0x000C8CC8
		internal RecordState GetDefaultRecordState(Shaper<RecordState> shaper)
		{
			RecordState recordState = null;
			if (this.RecordStateFactories.Count > 0)
			{
				recordState = (RecordState)shaper.State[this.RecordStateFactories[0].StateSlotNumber];
				recordState.ResetToDefaultState();
			}
			return recordState;
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000CAB0A File Offset: 0x000C8D0A
		public override string ToString()
		{
			return this.Description;
		}

		// Token: 0x040016F1 RID: 5873
		internal readonly Func<Shaper, IEntityWrapper> WrappedElement;

		// Token: 0x040016F2 RID: 5874
		internal readonly Func<Shaper, TElement> Element;

		// Token: 0x040016F3 RID: 5875
		internal readonly Func<Shaper, TElement> ElementWithErrorHandling;

		// Token: 0x040016F4 RID: 5876
		internal readonly Func<Shaper, ICollection<TElement>> InitializeCollection;

		// Token: 0x040016F5 RID: 5877
		private readonly string Description;
	}
}
