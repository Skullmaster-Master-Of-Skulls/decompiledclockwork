using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x02000432 RID: 1074
	internal static class Patterns
	{
		// Token: 0x0600398E RID: 14734 RVA: 0x000DABAC File Offset: 0x000D8DAC
		internal static Func<DbExpression, bool> And(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2)
		{
			return (DbExpression e) => pattern1(e) && pattern2(e);
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x000DABDC File Offset: 0x000D8DDC
		internal static Func<DbExpression, bool> And(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2, Func<DbExpression, bool> pattern3)
		{
			return (DbExpression e) => pattern1(e) && pattern2(e) && pattern3(e);
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000DAC10 File Offset: 0x000D8E10
		internal static Func<DbExpression, bool> Or(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2)
		{
			return (DbExpression e) => pattern1(e) || pattern2(e);
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000DAC40 File Offset: 0x000D8E40
		internal static Func<DbExpression, bool> Or(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2, Func<DbExpression, bool> pattern3)
		{
			return (DbExpression e) => pattern1(e) || pattern2(e) || pattern3(e);
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x000DAC74 File Offset: 0x000D8E74
		internal static Func<DbExpression, bool> AnyExpression
		{
			get
			{
				return (DbExpression e) => true;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000DAC95 File Offset: 0x000D8E95
		internal static Func<IEnumerable<DbExpression>, bool> AnyExpressions
		{
			get
			{
				return (IEnumerable<DbExpression> elems) => true;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x000DACB6 File Offset: 0x000D8EB6
		internal static Func<DbExpression, bool> MatchComplexType
		{
			get
			{
				return (DbExpression e) => TypeSemantics.IsComplexType(e.ResultType);
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06003995 RID: 14741 RVA: 0x000DACD7 File Offset: 0x000D8ED7
		internal static Func<DbExpression, bool> MatchEntityType
		{
			get
			{
				return (DbExpression e) => TypeSemantics.IsEntityType(e.ResultType);
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06003996 RID: 14742 RVA: 0x000DACF8 File Offset: 0x000D8EF8
		internal static Func<DbExpression, bool> MatchRowType
		{
			get
			{
				return (DbExpression e) => TypeSemantics.IsRowType(e.ResultType);
			}
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000DAD1C File Offset: 0x000D8F1C
		internal static Func<DbExpression, bool> MatchKind(DbExpressionKind kindToMatch)
		{
			return (DbExpression e) => e.ExpressionKind == kindToMatch;
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000DAD44 File Offset: 0x000D8F44
		internal static Func<IEnumerable<DbExpression>, bool> MatchForAll(Func<DbExpression, bool> elementPattern)
		{
			Func<DbExpression, bool> <>9__1;
			return delegate(IEnumerable<DbExpression> elems)
			{
				Func<DbExpression, bool> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = ((DbExpression e) => !elementPattern(e)));
				}
				return elems.FirstOrDefault(predicate) == null;
			};
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000DAD6A File Offset: 0x000D8F6A
		internal static Func<DbExpression, bool> MatchBinary()
		{
			return (DbExpression e) => e is DbBinaryExpression;
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000DAD8C File Offset: 0x000D8F8C
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

		// Token: 0x0600399B RID: 14747 RVA: 0x000DADBC File Offset: 0x000D8FBC
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

		// Token: 0x0600399C RID: 14748 RVA: 0x000DADEC File Offset: 0x000D8FEC
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

		// Token: 0x0600399D RID: 14749 RVA: 0x000DAE20 File Offset: 0x000D9020
		internal static Func<DbExpression, bool> MatchNewInstance()
		{
			return (DbExpression e) => e.ExpressionKind == DbExpressionKind.NewInstance;
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000DAE44 File Offset: 0x000D9044
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
