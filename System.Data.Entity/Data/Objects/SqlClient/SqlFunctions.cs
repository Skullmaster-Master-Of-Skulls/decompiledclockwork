using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Objects.SqlClient
{
	// Token: 0x0200015C RID: 348
	public static class SqlFunctions
	{
		// Token: 0x060019A4 RID: 6564 RVA: 0x00059C10 File Offset: 0x00057E10
		[EdmFunction("SqlServer", "CHECKSUM_AGG")]
		public static int? ChecksumAggregate(IEnumerable<int> arg)
		{
			ObjectQuery<int> objectQuery = arg as ObjectQuery<int>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<int?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(arg)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00059C54 File Offset: 0x00057E54
		[EdmFunction("SqlServer", "CHECKSUM_AGG")]
		public static int? ChecksumAggregate(IEnumerable<int?> arg)
		{
			ObjectQuery<int?> objectQuery = arg as ObjectQuery<int?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<int?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(arg)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ASCII")]
		public static int? Ascii(string arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHAR")]
		public static string Char(int? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHARINDEX")]
		public static int? CharIndex(string toSearch, string target)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHARINDEX")]
		public static int? CharIndex(byte[] toSearch, byte[] target)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHARINDEX")]
		public static int? CharIndex(string toSearch, string target, int? startLocation)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHARINDEX")]
		public static int? CharIndex(byte[] toSearch, byte[] target, int? startLocation)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHARINDEX")]
		public static long? CharIndex(string toSearch, string target, long? startLocation)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHARINDEX")]
		public static long? CharIndex(byte[] toSearch, byte[] target, long? startLocation)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DIFFERENCE")]
		public static int? Difference(string string1, string string2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "NCHAR")]
		public static string NChar(int? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "PATINDEX")]
		public static int? PatIndex(string stringPattern, string target)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "QUOTENAME")]
		public static string QuoteName(string stringArg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "QUOTENAME")]
		public static string QuoteName(string stringArg, string quoteCharacter)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "REPLICATE")]
		public static string Replicate(string target, int? count)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SOUNDEX")]
		public static string SoundCode(string arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SPACE")]
		public static string Space(int? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "STR")]
		public static string StringConvert(double? number)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "STR")]
		public static string StringConvert(decimal? number)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "STR")]
		public static string StringConvert(double? number, int? length)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "STR")]
		public static string StringConvert(decimal? number, int? length)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "STR")]
		public static string StringConvert(double? number, int? length, int? decimalArg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "STR")]
		public static string StringConvert(decimal? number, int? length, int? decimalArg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "STUFF")]
		public static string Stuff(string stringInput, int? start, int? length, string stringReplacement)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "UNICODE")]
		public static int? Unicode(string arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ACOS")]
		public static double? Acos(double? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ACOS")]
		public static double? Acos(decimal? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ASIN")]
		public static double? Asin(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ASIN")]
		public static double? Asin(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ATAN")]
		public static double? Atan(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ATAN")]
		public static double? Atan(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ATN2")]
		public static double? Atan2(double? arg1, double? arg2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ATN2")]
		public static double? Atan2(decimal? arg1, decimal? arg2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "COS")]
		public static double? Cos(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "COS")]
		public static double? Cos(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "COT")]
		public static double? Cot(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "COT")]
		public static double? Cot(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DEGREES")]
		public static int? Degrees(int? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DEGREES")]
		public static long? Degrees(long? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DEGREES")]
		public static decimal? Degrees(decimal? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DEGREES")]
		public static double? Degrees(double? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "EXP")]
		public static double? Exp(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "EXP")]
		public static double? Exp(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "LOG")]
		public static double? Log(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "LOG")]
		public static double? Log(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "LOG10")]
		public static double? Log10(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "LOG10")]
		public static double? Log10(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "PI")]
		public static double? Pi()
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "RADIANS")]
		public static int? Radians(int? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "RADIANS")]
		public static long? Radians(long? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "RADIANS")]
		public static decimal? Radians(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "RADIANS")]
		public static double? Radians(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "RAND")]
		public static double? Rand()
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "RAND")]
		public static double? Rand(int? seed)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SIGN")]
		public static int? Sign(int? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SIGN")]
		public static long? Sign(long? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SIGN")]
		public static decimal? Sign(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SIGN")]
		public static double? Sign(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SIN")]
		public static double? Sin(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SIN")]
		public static double? Sin(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SQRT")]
		public static double? SquareRoot(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SQRT")]
		public static double? SquareRoot(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SQUARE")]
		public static double? Square(double? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "SQUARE")]
		public static double? Square(decimal? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "TAN")]
		public static double? Tan(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "TAN")]
		public static double? Tan(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEADD")]
		public static DateTime? DateAdd(string datePartArg, double? number, DateTime? date)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEADD")]
		public static TimeSpan? DateAdd(string datePartArg, double? number, TimeSpan? time)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEADD")]
		public static DateTimeOffset? DateAdd(string datePartArg, double? number, DateTimeOffset? dateTimeOffsetArg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEADD")]
		public static DateTime? DateAdd(string datePartArg, double? number, string date)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, DateTime? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, TimeSpan? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, string startDate, DateTime? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, string startDate, DateTimeOffset? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, string startDate, TimeSpan? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, string endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, string endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, string endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, string startDate, string endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, DateTime? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, DateTimeOffset? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, TimeSpan? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, TimeSpan? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, DateTimeOffset? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, DateTime? endDate)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATENAME")]
		public static string DateName(string datePartArg, DateTime? date)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATENAME")]
		public static string DateName(string datePartArg, string date)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATENAME")]
		public static string DateName(string datePartArg, TimeSpan? date)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATENAME")]
		public static string DateName(string datePartArg, DateTimeOffset? date)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEPART")]
		public static int? DatePart(string datePartArg, DateTime? date)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEPART")]
		public static int? DatePart(string datePartArg, DateTimeOffset? date)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEPART")]
		public static int? DatePart(string datePartArg, string date)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATEPART")]
		public static int? DatePart(string datePartArg, TimeSpan? date)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "GETDATE")]
		public static DateTime? GetDate()
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "GETUTCDATE")]
		public static DateTime? GetUtcDate()
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(bool? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(double? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(decimal? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(DateTime? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(TimeSpan? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(DateTimeOffset? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(string arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(byte[] arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(Guid? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(bool? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(double? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(decimal? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(string arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTime? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(TimeSpan? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTimeOffset? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(byte[] arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(Guid? arg1)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(bool? arg1, bool? arg2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(double? arg1, double? arg2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(decimal? arg1, decimal? arg2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(string arg1, string arg2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTime? arg1, DateTime? arg2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(TimeSpan? arg1, TimeSpan? arg2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTimeOffset? arg1, DateTimeOffset? arg2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(byte[] arg1, byte[] arg2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(Guid? arg1, Guid? arg2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(bool? arg1, bool? arg2, bool? arg3)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(double? arg1, double? arg2, double? arg3)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(decimal? arg1, decimal? arg2, decimal? arg3)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(string arg1, string arg2, string arg3)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTime? arg1, DateTime? arg2, DateTime? arg3)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTimeOffset? arg1, DateTimeOffset? arg2, DateTimeOffset? arg3)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(TimeSpan? arg1, TimeSpan? arg2, TimeSpan? arg3)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(byte[] arg1, byte[] arg2, byte[] arg3)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(Guid? arg1, Guid? arg2, Guid? arg3)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CURRENT_TIMESTAMP")]
		public static DateTime? CurrentTimestamp()
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "CURRENT_USER")]
		public static string CurrentUser()
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "HOST_NAME")]
		public static string HostName()
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "USER_NAME")]
		public static string UserName(int? arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "USER_NAME")]
		public static string UserName()
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ISNUMERIC")]
		public static int? IsNumeric(string arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ISDATE")]
		public static int? IsDate(string arg)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}
	}
}
