using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x02000130 RID: 304
	internal static class Patterns
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x00034104 File Offset: 0x00032304
		internal static Func<DbExpression, bool> And(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2)
		{
			return (DbExpression e) => pattern1(e) && pattern2(e);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00034168 File Offset: 0x00032368
		internal static Func<DbExpression, bool> And(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2, Func<DbExpression, bool> pattern3)
		{
			return (DbExpression e) => pattern1(e) && pattern2(e) && pattern3(e);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000341C4 File Offset: 0x000323C4
		internal static Func<DbExpression, bool> Or(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2)
		{
			return (DbExpression e) => pattern1(e) || pattern2(e);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00034228 File Offset: 0x00032428
		internal static Func<DbExpression, bool> Or(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2, Func<DbExpression, bool> pattern3)
		{
			return (DbExpression e) => pattern1(e) || pattern2(e) || pattern3(e);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0003425F File Offset: 0x0003245F
		internal static Func<DbExpression, bool> AnyExpression
		{
			get
			{
				return (DbExpression e) => true;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00034281 File Offset: 0x00032481
		internal static Func<IEnumerable<DbExpression>, bool> AnyExpressions
		{
			get
			{
				return (IEnumerable<DbExpression> elems) => true;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x000342AD File Offset: 0x000324AD
		internal static Func<DbExpression, bool> MatchComplexType
		{
			get
			{
				return (DbExpression e) => TypeSemantics.IsComplexType(e.ResultType);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x000342D9 File Offset: 0x000324D9
		internal static Func<DbExpression, bool> MatchEntityType
		{
			get
			{
				return (DbExpression e) => TypeSemantics.IsEntityType(e.ResultType);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00034305 File Offset: 0x00032505
		internal static Func<DbExpression, bool> MatchRowType
		{
			get
			{
				return (DbExpression e) => TypeSemantics.IsRowType(e.ResultType);
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0003433C File Offset: 0x0003253C
		internal static Func<DbExpression, bool> MatchKind(DbExpressionKind kindToMatch)
		{
			return (DbExpression e) => e.ExpressionKind == kindToMatch;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00034394 File Offset: 0x00032594
		internal static Func<IEnumerable<DbExpression>, bool> MatchForAll(Func<DbExpression, bool> elementPattern)
		{
			return (IEnumerable<DbExpression> elems) => elems.FirstOrDefault((DbExpression e) => !elementPattern(e)) == null;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000343C5 File Offset: 0x000325C5
		internal static Func<DbExpression, bool> MatchBinary()
		{
			return (DbExpression e) => e is DbBinaryExpression;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00034438 File Offset: 0x00032638
		internal static Func<DbExpression, bool> MatchFilter(Func<DbExpression, bool> inputPattern, Func<DbExpression, bool> predicatePattern)
		{
			return delegate(DbExpression e)
			{
				if (e.ExpressionKind != DbExpressionKind.Filter)
				{
					return false;
				}
				DbFilterExpression dbFilterExpression = (DbFilterExpression)e;
				return inputPattern(dbFilterExpression.Input.Expression) && predicatePattern(dbFilterExpression.Predicate);
			};
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000344BC File Offset: 0x000326BC
		internal static Func<DbExpression, bool> MatchProject(Func<DbExpression, bool> inputPattern, Func<DbExpression, bool> projectionPattern)
		{
			return delegate(DbExpression e)
			{
				if (e.ExpressionKind != DbExpressionKind.Project)
				{
					return false;
				}
				DbProjectExpression dbProjectExpression = (DbProjectExpression)e;
				return inputPattern(dbProjectExpression.Input.Expression) && projectionPattern(dbProjectExpression.Projection);
			};
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0003454C File Offset: 0x0003274C
		internal static Func<DbExpression, bool> MatchCase(Func<IEnumerable<DbExpression>, bool> whenPattern, Func<IEnumerable<DbExpression>, bool> thenPattern, Func<DbExpression, bool> elsePattern)
		{
			return delegate(DbExpression e)
			{
				if (e.ExpressionKind != DbExpressionKind.Case)
				{
					return false;
				}
				DbCaseExpression dbCaseExpression = (DbCaseExpression)e;
				return whenPattern(dbCaseExpression.When) && thenPattern(dbCaseExpression.Then) && elsePattern(dbCaseExpression.Else);
			};
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0003458C File Offset: 0x0003278C
		internal static Func<DbExpression, bool> MatchNewInstance()
		{
			return (DbExpression e) => e.ExpressionKind == DbExpressionKind.NewInstance;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000345E8 File Offset: 0x000327E8
		internal static Func<DbExpression, bool> MatchNewInstance(Func<IEnumerable<DbExpression>, bool> argumentsPattern)
		{
			return delegate(DbExpression e)
			{
				if (e.ExpressionKind != DbExpressionKind.NewInstance)
				{
					return false;
				}
				DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)e;
				return argumentsPattern(dbNewInstanceExpression.Arguments);
			};
		}
	}
}
