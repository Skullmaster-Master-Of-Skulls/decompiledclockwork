using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020001FE RID: 510
	[Obsolete("This class has been replaced by System.Data.Entity.DbFunctions.")]
	public static class EntityFunctions
	{
		// Token: 0x060011DB RID: 4571 RVA: 0x0004C8E3 File Offset: 0x0004AAE3
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<decimal> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0004C8EB File Offset: 0x0004AAEB
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<decimal?> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0004C8F3 File Offset: 0x0004AAF3
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<double> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0004C8FB File Offset: 0x0004AAFB
		[DbFunction("Edm", "StDev")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? StandardDeviation(IEnumerable<double?> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0004C903 File Offset: 0x0004AB03
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<int> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0004C90B File Offset: 0x0004AB0B
		[DbFunction("Edm", "StDev")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? StandardDeviation(IEnumerable<int?> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004C913 File Offset: 0x0004AB13
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<long> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0004C91B File Offset: 0x0004AB1B
		[DbFunction("Edm", "StDev")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? StandardDeviation(IEnumerable<long?> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0004C923 File Offset: 0x0004AB23
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<decimal> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0004C92B File Offset: 0x0004AB2B
		[DbFunction("Edm", "StDevP")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? StandardDeviationP(IEnumerable<decimal?> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0004C933 File Offset: 0x0004AB33
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<double> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0004C93B File Offset: 0x0004AB3B
		[DbFunction("Edm", "StDevP")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? StandardDeviationP(IEnumerable<double?> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0004C943 File Offset: 0x0004AB43
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<int> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0004C94B File Offset: 0x0004AB4B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<int?> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0004C953 File Offset: 0x0004AB53
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<long> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0004C95B File Offset: 0x0004AB5B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<long?> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0004C963 File Offset: 0x0004AB63
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<decimal> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0004C96B File Offset: 0x0004AB6B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<decimal?> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0004C973 File Offset: 0x0004AB73
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<double> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0004C97B File Offset: 0x0004AB7B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<double?> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0004C983 File Offset: 0x0004AB83
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<int> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0004C98B File Offset: 0x0004AB8B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<int?> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0004C993 File Offset: 0x0004AB93
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<long> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0004C99B File Offset: 0x0004AB9B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<long?> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0004C9A3 File Offset: 0x0004ABA3
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<decimal> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0004C9AB File Offset: 0x0004ABAB
		[DbFunction("Edm", "VarP")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? VarP(IEnumerable<decimal?> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0004C9B3 File Offset: 0x0004ABB3
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<double> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0004C9BB File Offset: 0x0004ABBB
		[DbFunction("Edm", "VarP")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? VarP(IEnumerable<double?> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0004C9C3 File Offset: 0x0004ABC3
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<int> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004C9CB File Offset: 0x0004ABCB
		[DbFunction("Edm", "VarP")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? VarP(IEnumerable<int?> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0004C9D3 File Offset: 0x0004ABD3
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<long> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0004C9DB File Offset: 0x0004ABDB
		[DbFunction("Edm", "VarP")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? VarP(IEnumerable<long?> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0004C9E3 File Offset: 0x0004ABE3
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "length")]
		[DbFunction("Edm", "Left")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "stringArgument")]
		public static string Left(string stringArgument, long? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0004C9EF File Offset: 0x0004ABEF
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "length")]
		[DbFunction("Edm", "Right")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "stringArgument")]
		public static string Right(string stringArgument, long? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0004C9FB File Offset: 0x0004ABFB
		[DbFunction("Edm", "Reverse")]
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "stringArgument")]
		public static string Reverse(string stringArgument)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0004CA07 File Offset: 0x0004AC07
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateTimeOffsetArgument")]
		[DbFunction("Edm", "GetTotalOffsetMinutes")]
		public static int? GetTotalOffsetMinutes(DateTimeOffset? dateTimeOffsetArgument)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0004CA13 File Offset: 0x0004AC13
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		[DbFunction("Edm", "TruncateTime")]
		public static DateTimeOffset? TruncateTime(DateTimeOffset? dateValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0004CA1F File Offset: 0x0004AC1F
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		[DbFunction("Edm", "TruncateTime")]
		public static DateTime? TruncateTime(DateTime? dateValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0004CA2B File Offset: 0x0004AC2B
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "minute")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "second")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "day")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "hour")]
		[DbFunction("Edm", "CreateDateTime")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "year")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "month")]
		public static DateTime? CreateDateTime(int? year, int? month, int? day, int? hour, int? minute, double? second)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0004CA37 File Offset: 0x0004AC37
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "year")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeZoneOffset")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "second")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "hour")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "minute")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "day")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "month")]
		[DbFunction("Edm", "CreateDateTimeOffset")]
		public static DateTimeOffset? CreateDateTimeOffset(int? year, int? month, int? day, int? hour, int? minute, double? second, int? timeZoneOffset)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0004CA43 File Offset: 0x0004AC43
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "hour")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "second")]
		[DbFunction("Edm", "CreateTime")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "minute")]
		public static TimeSpan? CreateTime(int? hour, int? minute, double? second)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0004CA4F File Offset: 0x0004AC4F
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddYears")]
		public static DateTimeOffset? AddYears(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0004CA5B File Offset: 0x0004AC5B
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddYears")]
		public static DateTime? AddYears(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0004CA67 File Offset: 0x0004AC67
		[DbFunction("Edm", "AddMonths")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTimeOffset? AddMonths(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0004CA73 File Offset: 0x0004AC73
		[DbFunction("Edm", "AddMonths")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTime? AddMonths(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0004CA7F File Offset: 0x0004AC7F
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddDays")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		public static DateTimeOffset? AddDays(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0004CA8B File Offset: 0x0004AC8B
		[DbFunction("Edm", "AddDays")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		public static DateTime? AddDays(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0004CA97 File Offset: 0x0004AC97
		[DbFunction("Edm", "AddHours")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static DateTimeOffset? AddHours(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0004CAA3 File Offset: 0x0004ACA3
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[DbFunction("Edm", "AddHours")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTime? AddHours(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0004CAAF File Offset: 0x0004ACAF
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[DbFunction("Edm", "AddHours")]
		public static TimeSpan? AddHours(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0004CABB File Offset: 0x0004ACBB
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddMinutes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static DateTimeOffset? AddMinutes(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0004CAC7 File Offset: 0x0004ACC7
		[DbFunction("Edm", "AddMinutes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static DateTime? AddMinutes(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0004CAD3 File Offset: 0x0004ACD3
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[DbFunction("Edm", "AddMinutes")]
		public static TimeSpan? AddMinutes(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0004CADF File Offset: 0x0004ACDF
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[DbFunction("Edm", "AddSeconds")]
		public static DateTimeOffset? AddSeconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0004CAEB File Offset: 0x0004ACEB
		[DbFunction("Edm", "AddSeconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static DateTime? AddSeconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0004CAF7 File Offset: 0x0004ACF7
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[DbFunction("Edm", "AddSeconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static TimeSpan? AddSeconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0004CB03 File Offset: 0x0004AD03
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddMilliseconds")]
		public static DateTimeOffset? AddMilliseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0004CB0F File Offset: 0x0004AD0F
		[DbFunction("Edm", "AddMilliseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static DateTime? AddMilliseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0004CB1B File Offset: 0x0004AD1B
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddMilliseconds")]
		public static TimeSpan? AddMilliseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004CB27 File Offset: 0x0004AD27
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddMicroseconds")]
		public static DateTimeOffset? AddMicroseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0004CB33 File Offset: 0x0004AD33
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddMicroseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static DateTime? AddMicroseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0004CB3F File Offset: 0x0004AD3F
		[DbFunction("Edm", "AddMicroseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static TimeSpan? AddMicroseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0004CB4B File Offset: 0x0004AD4B
		[DbFunction("Edm", "AddNanoseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static DateTimeOffset? AddNanoseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0004CB57 File Offset: 0x0004AD57
		[DbFunction("Edm", "AddNanoseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTime? AddNanoseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0004CB63 File Offset: 0x0004AD63
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddNanoseconds")]
		public static TimeSpan? AddNanoseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0004CB6F File Offset: 0x0004AD6F
		[DbFunction("Edm", "DiffYears")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		public static int? DiffYears(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0004CB7B File Offset: 0x0004AD7B
		[DbFunction("Edm", "DiffYears")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		public static int? DiffYears(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0004CB87 File Offset: 0x0004AD87
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		[DbFunction("Edm", "DiffMonths")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		public static int? DiffMonths(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0004CB93 File Offset: 0x0004AD93
		[DbFunction("Edm", "DiffMonths")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		public static int? DiffMonths(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0004CB9F File Offset: 0x0004AD9F
		[DbFunction("Edm", "DiffDays")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		public static int? DiffDays(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0004CBAB File Offset: 0x0004ADAB
		[DbFunction("Edm", "DiffDays")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		public static int? DiffDays(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0004CBB7 File Offset: 0x0004ADB7
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[DbFunction("Edm", "DiffHours")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffHours(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0004CBC3 File Offset: 0x0004ADC3
		[DbFunction("Edm", "DiffHours")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffHours(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0004CBCF File Offset: 0x0004ADCF
		[DbFunction("Edm", "DiffHours")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffHours(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0004CBDB File Offset: 0x0004ADDB
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[DbFunction("Edm", "DiffMinutes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffMinutes(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0004CBE7 File Offset: 0x0004ADE7
		[DbFunction("Edm", "DiffMinutes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffMinutes(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0004CBF3 File Offset: 0x0004ADF3
		[DbFunction("Edm", "DiffMinutes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffMinutes(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0004CBFF File Offset: 0x0004ADFF
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[DbFunction("Edm", "DiffSeconds")]
		public static int? DiffSeconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0004CC0B File Offset: 0x0004AE0B
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[DbFunction("Edm", "DiffSeconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffSeconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0004CC17 File Offset: 0x0004AE17
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[DbFunction("Edm", "DiffSeconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffSeconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0004CC23 File Offset: 0x0004AE23
		[DbFunction("Edm", "DiffMilliseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffMilliseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0004CC2F File Offset: 0x0004AE2F
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[DbFunction("Edm", "DiffMilliseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffMilliseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0004CC3B File Offset: 0x0004AE3B
		[DbFunction("Edm", "DiffMilliseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffMilliseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0004CC47 File Offset: 0x0004AE47
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[DbFunction("Edm", "DiffMicroseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffMicroseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0004CC53 File Offset: 0x0004AE53
		[DbFunction("Edm", "DiffMicroseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffMicroseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0004CC5F File Offset: 0x0004AE5F
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[DbFunction("Edm", "DiffMicroseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffMicroseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0004CC6B File Offset: 0x0004AE6B
		[DbFunction("Edm", "DiffNanoseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffNanoseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0004CC77 File Offset: 0x0004AE77
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[DbFunction("Edm", "DiffNanoseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffNanoseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0004CC83 File Offset: 0x0004AE83
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[DbFunction("Edm", "DiffNanoseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffNanoseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0004CC8F File Offset: 0x0004AE8F
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "digits")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
		[DbFunction("Edm", "Truncate")]
		public static double? Truncate(double? value, int? digits)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0004CC9B File Offset: 0x0004AE9B
		[DbFunction("Edm", "Truncate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "digits")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
		public static decimal? Truncate(decimal? value, int? digits)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0004CCA7 File Offset: 0x0004AEA7
		public static string AsUnicode(string value)
		{
			return value;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0004CCAA File Offset: 0x0004AEAA
		public static string AsNonUnicode(string value)
		{
			return value;
		}
	}
}
