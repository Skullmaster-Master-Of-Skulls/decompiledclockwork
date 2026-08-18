using System;
using System.Globalization;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004C5 RID: 1221
	internal static class QueryValueModel
	{
		// Token: 0x06002E2E RID: 11822 RVA: 0x000B4054 File Offset: 0x000B2254
		internal static bool Boolean(string val)
		{
			return val.Length > 0;
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000B405F File Offset: 0x000B225F
		internal static bool Boolean(double dblVal)
		{
			return dblVal != 0.0 && !double.IsNaN(dblVal);
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000B4078 File Offset: 0x000B2278
		internal static bool Boolean(NodeSequence sequence)
		{
			return sequence.IsNotEmpty;
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000B4080 File Offset: 0x000B2280
		internal static bool Boolean(XPathNodeIterator iterator)
		{
			return iterator.Count > 0;
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000B408B File Offset: 0x000B228B
		internal static double Double(bool val)
		{
			return (double)(val ? 1 : 0);
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000B4098 File Offset: 0x000B2298
		internal static double Double(string val)
		{
			val = val.TrimStart(new char[0]);
			double result;
			if (val.Length > 0 && val[0] != '+' && double.TryParse(val, NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out result))
			{
				return result;
			}
			return double.NaN;
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000B40E3 File Offset: 0x000B22E3
		internal static double Double(NodeSequence sequence)
		{
			return QueryValueModel.Double(sequence.StringValue());
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000B40F0 File Offset: 0x000B22F0
		internal static double Double(XPathNodeIterator iterator)
		{
			return QueryValueModel.Double(QueryValueModel.String(iterator));
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000B40FD File Offset: 0x000B22FD
		internal static string String(bool val)
		{
			if (!val)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000B410D File Offset: 0x000B230D
		internal static string String(double val)
		{
			return val.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000B411B File Offset: 0x000B231B
		internal static string String(NodeSequence sequence)
		{
			return sequence.StringValue();
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000B4124 File Offset: 0x000B2324
		internal static string String(XPathNodeIterator iterator)
		{
			if (iterator.Count == 0)
			{
				return string.Empty;
			}
			if (iterator.CurrentPosition == 0)
			{
				iterator.MoveNext();
				return iterator.Current.Value;
			}
			if (iterator.CurrentPosition == 1)
			{
				return iterator.Current.Value;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("QueryCantGetStringForMovedIterator")));
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000B4188 File Offset: 0x000B2388
		internal static bool Compare(bool x, bool y, RelationOperator op)
		{
			if (op == RelationOperator.Eq)
			{
				return x == y;
			}
			if (op != RelationOperator.Ne)
			{
				return QueryValueModel.Compare(QueryValueModel.Double(x), QueryValueModel.Double(y), op);
			}
			return x != y;
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000B41B1 File Offset: 0x000B23B1
		internal static bool Compare(bool x, double y, RelationOperator op)
		{
			if (op == RelationOperator.Eq)
			{
				return x == QueryValueModel.Boolean(y);
			}
			if (op != RelationOperator.Ne)
			{
				return QueryValueModel.Compare(QueryValueModel.Double(x), y, op);
			}
			return x != QueryValueModel.Boolean(y);
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000B41DF File Offset: 0x000B23DF
		internal static bool Compare(bool x, string y, RelationOperator op)
		{
			if (op == RelationOperator.Eq)
			{
				return x == QueryValueModel.Boolean(y);
			}
			if (op != RelationOperator.Ne)
			{
				return QueryValueModel.Compare(QueryValueModel.Double(x), QueryValueModel.Double(y), op);
			}
			return x != QueryValueModel.Boolean(y);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000B4212 File Offset: 0x000B2412
		internal static bool Compare(bool x, NodeSequence y, RelationOperator op)
		{
			return QueryValueModel.Compare(x, QueryValueModel.Boolean(y), op);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000B4221 File Offset: 0x000B2421
		internal static bool Compare(double x, bool y, RelationOperator op)
		{
			if (op == RelationOperator.Eq)
			{
				return QueryValueModel.Boolean(x) == y;
			}
			if (op != RelationOperator.Ne)
			{
				return QueryValueModel.Compare(x, QueryValueModel.Double(y), op);
			}
			return QueryValueModel.Boolean(x) != y;
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000B4250 File Offset: 0x000B2450
		internal static bool Compare(double x, double y, RelationOperator op)
		{
			switch (op)
			{
			case RelationOperator.Eq:
				return x == y;
			case RelationOperator.Ne:
				return x != y;
			case RelationOperator.Gt:
				return x > y;
			case RelationOperator.Ge:
				return x >= y;
			case RelationOperator.Lt:
				return x < y;
			case RelationOperator.Le:
				return x <= y;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
			}
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000B42B4 File Offset: 0x000B24B4
		internal static bool Compare(double x, string y, RelationOperator op)
		{
			return QueryValueModel.Compare(x, QueryValueModel.Double(y), op);
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000B42C4 File Offset: 0x000B24C4
		internal static bool Compare(double x, NodeSequence y, RelationOperator op)
		{
			switch (op)
			{
			case RelationOperator.Gt:
				return y.Compare(x, RelationOperator.Lt);
			case RelationOperator.Ge:
				return y.Compare(x, RelationOperator.Le);
			case RelationOperator.Lt:
				return y.Compare(x, RelationOperator.Gt);
			case RelationOperator.Le:
				return y.Compare(x, RelationOperator.Ge);
			default:
				return y.Compare(x, op);
			}
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000B4315 File Offset: 0x000B2515
		internal static bool Compare(string x, bool y, RelationOperator op)
		{
			if (op == RelationOperator.Eq)
			{
				return y == QueryValueModel.Boolean(x);
			}
			if (op != RelationOperator.Ne)
			{
				return QueryValueModel.Compare(QueryValueModel.Double(x), QueryValueModel.Double(y), op);
			}
			return y != QueryValueModel.Boolean(x);
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000B4348 File Offset: 0x000B2548
		internal static bool Compare(string x, double y, RelationOperator op)
		{
			return QueryValueModel.Compare(QueryValueModel.Double(x), y, op);
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000B4358 File Offset: 0x000B2558
		internal static bool Compare(string x, string y, RelationOperator op)
		{
			switch (op)
			{
			case RelationOperator.Eq:
				return QueryValueModel.Equals(x, y);
			case RelationOperator.Ne:
				return x.Length != y.Length || string.CompareOrdinal(x, y) != 0;
			case RelationOperator.Gt:
			case RelationOperator.Ge:
			case RelationOperator.Lt:
			case RelationOperator.Le:
				return QueryValueModel.Compare(QueryValueModel.Double(x), QueryValueModel.Double(y), op);
			default:
				return false;
			}
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000B43C0 File Offset: 0x000B25C0
		internal static bool Compare(string x, NodeSequence y, RelationOperator op)
		{
			switch (op)
			{
			case RelationOperator.Gt:
				return y.Compare(x, RelationOperator.Lt);
			case RelationOperator.Ge:
				return y.Compare(x, RelationOperator.Le);
			case RelationOperator.Lt:
				return y.Compare(x, RelationOperator.Gt);
			case RelationOperator.Le:
				return y.Compare(x, RelationOperator.Ge);
			default:
				return y.Compare(x, op);
			}
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000B4411 File Offset: 0x000B2611
		internal static bool Compare(NodeSequence x, bool y, RelationOperator op)
		{
			return QueryValueModel.Compare(QueryValueModel.Boolean(x), y, op);
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000B4420 File Offset: 0x000B2620
		internal static bool Compare(NodeSequence x, double y, RelationOperator op)
		{
			return x.Compare(y, op);
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000B442A File Offset: 0x000B262A
		internal static bool Compare(NodeSequence x, string y, RelationOperator op)
		{
			return x.Compare(y, op);
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000B4434 File Offset: 0x000B2634
		internal static bool Compare(NodeSequence x, NodeSequence y, RelationOperator op)
		{
			return x.Compare(y, op);
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000B4440 File Offset: 0x000B2640
		internal static bool CompileTimeCompare(object x, object y, RelationOperator op)
		{
			if (x is string)
			{
				if (y is double)
				{
					return QueryValueModel.Compare((string)x, (double)y, op);
				}
				if (y is string)
				{
					return QueryValueModel.Compare((string)x, (string)y, op);
				}
			}
			else if (x is double)
			{
				if (y is double)
				{
					return QueryValueModel.Compare((double)x, (double)y, op);
				}
				if (y is string)
				{
					return QueryValueModel.Compare((double)x, (string)y, op);
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.InvalidComparison));
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000B44DA File Offset: 0x000B26DA
		internal static bool Equals(bool x, string y)
		{
			return x == QueryValueModel.Boolean(y);
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000B44E5 File Offset: 0x000B26E5
		internal static bool Equals(double x, string y)
		{
			return x == QueryValueModel.Double(y);
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000B44F0 File Offset: 0x000B26F0
		internal static bool Equals(string x, string y)
		{
			return x.Length == y.Length && x == y;
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000B4509 File Offset: 0x000B2709
		internal static bool Equals(NodeSequence x, string y)
		{
			return x.Equals(y);
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000B4512 File Offset: 0x000B2712
		internal static bool Equals(bool x, double y)
		{
			return x == QueryValueModel.Boolean(y);
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000B451D File Offset: 0x000B271D
		internal static bool Equals(double x, double y)
		{
			return x == y;
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000B4523 File Offset: 0x000B2723
		internal static bool Equals(NodeSequence x, double y)
		{
			return x.Equals(y);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000B452C File Offset: 0x000B272C
		internal static double Round(double val)
		{
			if (-0.5 > val || val > 0.0)
			{
				return Math.Floor(val + 0.5);
			}
			return Math.Round(val);
		}
	}
}
