using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.Internal;
using System.Linq.Expressions;
using System.Text;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020001DB RID: 475
	internal class CoordinatorFactory<TElement> : CoordinatorFactory
	{
		// Token: 0x060010C7 RID: 4295 RVA: 0x00047CEC File Offset: 0x00045EEC
		internal CoordinatorFactory(int depth, int stateSlot, Expression<Func<Shaper, bool>> hasData, Expression<Func<Shaper, bool>> setKeys, Expression<Func<Shaper, bool>> checkKeys, CoordinatorFactory[] nestedCoordinators, Expression<Func<Shaper, TElement>> element, Expression<Func<Shaper, IEntityWrapper>> wrappedElement, Expression<Func<Shaper, TElement>> elementWithErrorHandling, Expression<Func<Shaper, ICollection<TElement>>> initializeCollection, RecordStateFactory[] recordStateFactories) : base(depth, stateSlot, CoordinatorFactory<TElement>.CompilePredicate(hasData), CoordinatorFactory<TElement>.CompilePredicate(setKeys), CoordinatorFactory<TElement>.CompilePredicate(checkKeys), nestedCoordinators, recordStateFactories)
		{
			this.WrappedElement = ((wrappedElement == null) ? null : wrappedElement.Compile());
			this.Element = ((element == null) ? null : element.Compile());
			this.ElementWithErrorHandling = elementWithErrorHandling.Compile();
			Func<Shaper, ICollection<TElement>> initializeCollection2;
			if (initializeCollection != null)
			{
				initializeCollection2 = initializeCollection.Compile();
			}
			else
			{
				initializeCollection2 = ((Shaper s) => new List<TElement>());
			}
			this.InitializeCollection = initializeCollection2;
			this.Description = new StringBuilder().Append("HasData: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(hasData)).Append("SetKeys: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(setKeys)).Append("CheckKeys: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(checkKeys)).Append("Element: ").AppendLine((element == null) ? CoordinatorFactory<TElement>.DescribeExpression(wrappedElement) : CoordinatorFactory<TElement>.DescribeExpression(element)).Append("ElementWithExceptionHandling: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(elementWithErrorHandling)).Append("InitializeCollection: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(initializeCollection)).ToString();
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00047E20 File Offset: 0x00046020
		public CoordinatorFactory(int depth, int stateSlot, Expression hasData, Expression setKeys, Expression checkKeys, CoordinatorFactory[] nestedCoordinators, Expression element, Expression elementWithErrorHandling, Expression initializeCollection, RecordStateFactory[] recordStateFactories) : this(depth, stateSlot, CodeGenEmitter.BuildShaperLambda<bool>(hasData), CodeGenEmitter.BuildShaperLambda<bool>(setKeys), CodeGenEmitter.BuildShaperLambda<bool>(checkKeys), nestedCoordinators, typeof(IEntityWrapper).IsAssignableFrom(element.Type) ? null : CodeGenEmitter.BuildShaperLambda<TElement>(element), typeof(IEntityWrapper).IsAssignableFrom(element.Type) ? CodeGenEmitter.BuildShaperLambda<IEntityWrapper>(element) : null, CodeGenEmitter.BuildShaperLambda<TElement>(typeof(IEntityWrapper).IsAssignableFrom(element.Type) ? CodeGenEmitter.Emit_UnwrapAndEnsureType(elementWithErrorHandling, typeof(TElement)) : elementWithErrorHandling), CodeGenEmitter.BuildShaperLambda<ICollection<TElement>>(initializeCollection), recordStateFactories)
		{
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00047ECC File Offset: 0x000460CC
		private static Func<Shaper, bool> CompilePredicate(Expression<Func<Shaper, bool>> predicate)
		{
			Func<Shaper, bool> result;
			if (predicate == null)
			{
				result = null;
			}
			else
			{
				result = predicate.Compile();
			}
			return result;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00047EE8 File Offset: 0x000460E8
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

		// Token: 0x060010CB RID: 4299 RVA: 0x00047F08 File Offset: 0x00046108
		internal override Coordinator CreateCoordinator(Coordinator parent, Coordinator next)
		{
			return new Coordinator<TElement>(this, parent, next);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00047F14 File Offset: 0x00046114
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

		// Token: 0x060010CD RID: 4301 RVA: 0x00047F56 File Offset: 0x00046156
		public override string ToString()
		{
			return this.Description;
		}

		// Token: 0x04000500 RID: 1280
		internal readonly Func<Shaper, IEntityWrapper> WrappedElement;

		// Token: 0x04000501 RID: 1281
		internal readonly Func<Shaper, TElement> Element;

		// Token: 0x04000502 RID: 1282
		internal readonly Func<Shaper, TElement> ElementWithErrorHandling;

		// Token: 0x04000503 RID: 1283
		internal readonly Func<Shaper, ICollection<TElement>> InitializeCollection;

		// Token: 0x04000504 RID: 1284
		private readonly string Description;
	}
}
