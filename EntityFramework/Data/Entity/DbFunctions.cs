using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity
{
	// Token: 0x020006B0 RID: 1712
	public static class DbFunctions
	{
		// Token: 0x06004406 RID: 17414 RVA: 0x00143074 File Offset: 0x00141274
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<decimal> collection)
		{
			return DbFunctions.BootstrapFunction<decimal, double?>((IEnumerable<decimal> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x001430D0 File Offset: 0x001412D0
		[DbFunction("Edm", "StDev")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? StandardDeviation(IEnumerable<decimal?> collection)
		{
			return DbFunctions.BootstrapFunction<decimal?, double?>((IEnumerable<decimal?> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x0014312C File Offset: 0x0014132C
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<double> collection)
		{
			return DbFunctions.BootstrapFunction<double, double?>((IEnumerable<double> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x00143188 File Offset: 0x00141388
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<double?> collection)
		{
			return DbFunctions.BootstrapFunction<double?, double?>((IEnumerable<double?> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x001431E4 File Offset: 0x001413E4
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<int> collection)
		{
			return DbFunctions.BootstrapFunction<int, double?>((IEnumerable<int> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x00143240 File Offset: 0x00141440
		[DbFunction("Edm", "StDev")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? StandardDeviation(IEnumerable<int?> collection)
		{
			return DbFunctions.BootstrapFunction<int?, double?>((IEnumerable<int?> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x0014329C File Offset: 0x0014149C
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<long> collection)
		{
			return DbFunctions.BootstrapFunction<long, double?>((IEnumerable<long> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x001432F8 File Offset: 0x001414F8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<long?> collection)
		{
			return DbFunctions.BootstrapFunction<long?, double?>((IEnumerable<long?> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x00143354 File Offset: 0x00141554
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<decimal> collection)
		{
			return DbFunctions.BootstrapFunction<decimal, double?>((IEnumerable<decimal> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x001433B0 File Offset: 0x001415B0
		[DbFunction("Edm", "StDevP")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? StandardDeviationP(IEnumerable<decimal?> collection)
		{
			return DbFunctions.BootstrapFunction<decimal?, double?>((IEnumerable<decimal?> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x0014340C File Offset: 0x0014160C
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<double> collection)
		{
			return DbFunctions.BootstrapFunction<double, double?>((IEnumerable<double> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x00143468 File Offset: 0x00141668
		[DbFunction("Edm", "StDevP")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? StandardDeviationP(IEnumerable<double?> collection)
		{
			return DbFunctions.BootstrapFunction<double?, double?>((IEnumerable<double?> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x001434C4 File Offset: 0x001416C4
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<int> collection)
		{
			return DbFunctions.BootstrapFunction<int, double?>((IEnumerable<int> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x00143520 File Offset: 0x00141720
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<int?> collection)
		{
			return DbFunctions.BootstrapFunction<int?, double?>((IEnumerable<int?> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x0014357C File Offset: 0x0014177C
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<long> collection)
		{
			return DbFunctions.BootstrapFunction<long, double?>((IEnumerable<long> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x001435D8 File Offset: 0x001417D8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<long?> collection)
		{
			return DbFunctions.BootstrapFunction<long?, double?>((IEnumerable<long?> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x00143634 File Offset: 0x00141834
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<decimal> collection)
		{
			return DbFunctions.BootstrapFunction<decimal, double?>((IEnumerable<decimal> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x00143690 File Offset: 0x00141890
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<decimal?> collection)
		{
			return DbFunctions.BootstrapFunction<decimal?, double?>((IEnumerable<decimal?> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x001436EC File Offset: 0x001418EC
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<double> collection)
		{
			return DbFunctions.BootstrapFunction<double, double?>((IEnumerable<double> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x00143748 File Offset: 0x00141948
		[DbFunction("Edm", "Var")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? Var(IEnumerable<double?> collection)
		{
			return DbFunctions.BootstrapFunction<double?, double?>((IEnumerable<double?> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x001437A4 File Offset: 0x001419A4
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<int> collection)
		{
			return DbFunctions.BootstrapFunction<int, double?>((IEnumerable<int> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x00143800 File Offset: 0x00141A00
		[DbFunction("Edm", "Var")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? Var(IEnumerable<int?> collection)
		{
			return DbFunctions.BootstrapFunction<int?, double?>((IEnumerable<int?> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x0014385C File Offset: 0x00141A5C
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<long> collection)
		{
			return DbFunctions.BootstrapFunction<long, double?>((IEnumerable<long> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x001438B8 File Offset: 0x00141AB8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<long?> collection)
		{
			return DbFunctions.BootstrapFunction<long?, double?>((IEnumerable<long?> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x00143914 File Offset: 0x00141B14
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<decimal> collection)
		{
			return DbFunctions.BootstrapFunction<decimal, double?>((IEnumerable<decimal> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x00143970 File Offset: 0x00141B70
		[DbFunction("Edm", "VarP")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? VarP(IEnumerable<decimal?> collection)
		{
			return DbFunctions.BootstrapFunction<decimal?, double?>((IEnumerable<decimal?> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x001439CC File Offset: 0x00141BCC
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<double> collection)
		{
			return DbFunctions.BootstrapFunction<double, double?>((IEnumerable<double> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x00143A28 File Offset: 0x00141C28
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<double?> collection)
		{
			return DbFunctions.BootstrapFunction<double?, double?>((IEnumerable<double?> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x00143A84 File Offset: 0x00141C84
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<int> collection)
		{
			return DbFunctions.BootstrapFunction<int, double?>((IEnumerable<int> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x00143AE0 File Offset: 0x00141CE0
		[DbFunction("Edm", "VarP")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? VarP(IEnumerable<int?> collection)
		{
			return DbFunctions.BootstrapFunction<int?, double?>((IEnumerable<int?> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x00143B3C File Offset: 0x00141D3C
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<long> collection)
		{
			return DbFunctions.BootstrapFunction<long, double?>((IEnumerable<long> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x00143B98 File Offset: 0x00141D98
		[DbFunction("Edm", "VarP")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static double? VarP(IEnumerable<long?> collection)
		{
			return DbFunctions.BootstrapFunction<long?, double?>((IEnumerable<long?> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x00143BF2 File Offset: 0x00141DF2
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "length")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "stringArgument")]
		[DbFunction("Edm", "Left")]
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		public static string Left(string stringArgument, long? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x00143BFE File Offset: 0x00141DFE
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "stringArgument")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "length")]
		[DbFunction("Edm", "Right")]
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		public static string Right(string stringArgument, long? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x00143C0A File Offset: 0x00141E0A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "stringArgument")]
		[DbFunction("Edm", "Reverse")]
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		public static string Reverse(string stringArgument)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x00143C16 File Offset: 0x00141E16
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateTimeOffsetArgument")]
		[DbFunction("Edm", "GetTotalOffsetMinutes")]
		public static int? GetTotalOffsetMinutes(DateTimeOffset? dateTimeOffsetArgument)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x00143C22 File Offset: 0x00141E22
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		[DbFunction("Edm", "TruncateTime")]
		public static DateTimeOffset? TruncateTime(DateTimeOffset? dateValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x00143C2E File Offset: 0x00141E2E
		[DbFunction("Edm", "TruncateTime")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		public static DateTime? TruncateTime(DateTime? dateValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x00143C3A File Offset: 0x00141E3A
		[DbFunction("Edm", "CreateDateTime")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "minute")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "second")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "day")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "hour")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "year")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "month")]
		public static DateTime? CreateDateTime(int? year, int? month, int? day, int? hour, int? minute, double? second)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x00143C46 File Offset: 0x00141E46
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "month")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "day")]
		[DbFunction("Edm", "CreateDateTimeOffset")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "second")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "hour")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "minute")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeZoneOffset")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "year")]
		public static DateTimeOffset? CreateDateTimeOffset(int? year, int? month, int? day, int? hour, int? minute, double? second, int? timeZoneOffset)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x00143C52 File Offset: 0x00141E52
		[DbFunction("Edm", "CreateTime")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "minute")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "hour")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "second")]
		public static TimeSpan? CreateTime(int? hour, int? minute, double? second)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x00143C5E File Offset: 0x00141E5E
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddYears")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		public static DateTimeOffset? AddYears(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x00143C6A File Offset: 0x00141E6A
		[DbFunction("Edm", "AddYears")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTime? AddYears(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x00143C76 File Offset: 0x00141E76
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		[DbFunction("Edm", "AddMonths")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTimeOffset? AddMonths(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x00143C82 File Offset: 0x00141E82
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddMonths")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		public static DateTime? AddMonths(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x00143C8E File Offset: 0x00141E8E
		[DbFunction("Edm", "AddDays")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		public static DateTimeOffset? AddDays(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x00143C9A File Offset: 0x00141E9A
		[DbFunction("Edm", "AddDays")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue")]
		public static DateTime? AddDays(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x00143CA6 File Offset: 0x00141EA6
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[DbFunction("Edm", "AddHours")]
		public static DateTimeOffset? AddHours(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004436 RID: 17462 RVA: 0x00143CB2 File Offset: 0x00141EB2
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddHours")]
		public static DateTime? AddHours(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x00143CBE File Offset: 0x00141EBE
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[DbFunction("Edm", "AddHours")]
		public static TimeSpan? AddHours(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x00143CCA File Offset: 0x00141ECA
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[DbFunction("Edm", "AddMinutes")]
		public static DateTimeOffset? AddMinutes(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x00143CD6 File Offset: 0x00141ED6
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[DbFunction("Edm", "AddMinutes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTime? AddMinutes(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x00143CE2 File Offset: 0x00141EE2
		[DbFunction("Edm", "AddMinutes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static TimeSpan? AddMinutes(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x00143CEE File Offset: 0x00141EEE
		[DbFunction("Edm", "AddSeconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static DateTimeOffset? AddSeconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600443C RID: 17468 RVA: 0x00143CFA File Offset: 0x00141EFA
		[DbFunction("Edm", "AddSeconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTime? AddSeconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x00143D06 File Offset: 0x00141F06
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[DbFunction("Edm", "AddSeconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static TimeSpan? AddSeconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x00143D12 File Offset: 0x00141F12
		[DbFunction("Edm", "AddMilliseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTimeOffset? AddMilliseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x00143D1E File Offset: 0x00141F1E
		[DbFunction("Edm", "AddMilliseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static DateTime? AddMilliseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x00143D2A File Offset: 0x00141F2A
		[DbFunction("Edm", "AddMilliseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static TimeSpan? AddMilliseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x00143D36 File Offset: 0x00141F36
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[DbFunction("Edm", "AddMicroseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTimeOffset? AddMicroseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x00143D42 File Offset: 0x00141F42
		[DbFunction("Edm", "AddMicroseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTime? AddMicroseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x00143D4E File Offset: 0x00141F4E
		[DbFunction("Edm", "AddMicroseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static TimeSpan? AddMicroseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x00143D5A File Offset: 0x00141F5A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[DbFunction("Edm", "AddNanoseconds")]
		public static DateTimeOffset? AddNanoseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x00143D66 File Offset: 0x00141F66
		[DbFunction("Edm", "AddNanoseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		public static DateTime? AddNanoseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x00143D72 File Offset: 0x00141F72
		[DbFunction("Edm", "AddNanoseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "addValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue")]
		public static TimeSpan? AddNanoseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x00143D7E File Offset: 0x00141F7E
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		[DbFunction("Edm", "DiffYears")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		public static int? DiffYears(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x00143D8A File Offset: 0x00141F8A
		[DbFunction("Edm", "DiffYears")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		public static int? DiffYears(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004449 RID: 17481 RVA: 0x00143D96 File Offset: 0x00141F96
		[DbFunction("Edm", "DiffMonths")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		public static int? DiffMonths(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x00143DA2 File Offset: 0x00141FA2
		[DbFunction("Edm", "DiffMonths")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		public static int? DiffMonths(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x00143DAE File Offset: 0x00141FAE
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		[DbFunction("Edm", "DiffDays")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		public static int? DiffDays(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x00143DBA File Offset: 0x00141FBA
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue1")]
		[DbFunction("Edm", "DiffDays")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateValue2")]
		public static int? DiffDays(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x00143DC6 File Offset: 0x00141FC6
		[DbFunction("Edm", "DiffHours")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffHours(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x00143DD2 File Offset: 0x00141FD2
		[DbFunction("Edm", "DiffHours")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffHours(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x00143DDE File Offset: 0x00141FDE
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[DbFunction("Edm", "DiffHours")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffHours(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x00143DEA File Offset: 0x00141FEA
		[DbFunction("Edm", "DiffMinutes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffMinutes(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x00143DF6 File Offset: 0x00141FF6
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[DbFunction("Edm", "DiffMinutes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffMinutes(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x00143E02 File Offset: 0x00142002
		[DbFunction("Edm", "DiffMinutes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffMinutes(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x00143E0E File Offset: 0x0014200E
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[DbFunction("Edm", "DiffSeconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffSeconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x00143E1A File Offset: 0x0014201A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[DbFunction("Edm", "DiffSeconds")]
		public static int? DiffSeconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x00143E26 File Offset: 0x00142026
		[DbFunction("Edm", "DiffSeconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffSeconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x00143E32 File Offset: 0x00142032
		[DbFunction("Edm", "DiffMilliseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffMilliseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x00143E3E File Offset: 0x0014203E
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[DbFunction("Edm", "DiffMilliseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffMilliseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x00143E4A File Offset: 0x0014204A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[DbFunction("Edm", "DiffMilliseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffMilliseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x00143E56 File Offset: 0x00142056
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[DbFunction("Edm", "DiffMicroseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffMicroseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600445A RID: 17498 RVA: 0x00143E62 File Offset: 0x00142062
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[DbFunction("Edm", "DiffMicroseconds")]
		public static int? DiffMicroseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600445B RID: 17499 RVA: 0x00143E6E File Offset: 0x0014206E
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[DbFunction("Edm", "DiffMicroseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffMicroseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600445C RID: 17500 RVA: 0x00143E7A File Offset: 0x0014207A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		[DbFunction("Edm", "DiffNanoseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		public static int? DiffNanoseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x00143E86 File Offset: 0x00142086
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[DbFunction("Edm", "DiffNanoseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffNanoseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x00143E92 File Offset: 0x00142092
		[DbFunction("Edm", "DiffNanoseconds")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeValue2")]
		public static int? DiffNanoseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x00143E9E File Offset: 0x0014209E
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
		[DbFunction("Edm", "Truncate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "digits")]
		public static double? Truncate(double? value, int? digits)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x00143EAA File Offset: 0x001420AA
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "digits")]
		[DbFunction("Edm", "Truncate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
		public static decimal? Truncate(decimal? value, int? digits)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x00143EB6 File Offset: 0x001420B6
		public static string AsUnicode(string value)
		{
			return value;
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x00143EB9 File Offset: 0x001420B9
		public static string AsNonUnicode(string value)
		{
			return value;
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x00143EBC File Offset: 0x001420BC
		private static TOut BootstrapFunction<TIn, TOut>(Expression<Func<IEnumerable<TIn>, TOut>> methodExpression, IEnumerable<TIn> collection)
		{
			IQueryable queryable = collection as IQueryable;
			if (queryable != null)
			{
				return queryable.Provider.Execute<TOut>(Expression.Call(((MethodCallExpression)methodExpression.Body).Method, Expression.Constant(collection)));
			}
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}
	}
}
