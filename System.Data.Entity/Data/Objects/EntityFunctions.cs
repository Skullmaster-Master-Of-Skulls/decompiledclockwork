using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Objects
{
	// Token: 0x0200012A RID: 298
	public static class EntityFunctions
	{
		// Token: 0x0600156D RID: 5485 RVA: 0x00048AC0 File Offset: 0x00046CC0
		public static string AsUnicode(string value)
		{
			return value;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00048AC0 File Offset: 0x00046CC0
		public static string AsNonUnicode(string value)
		{
			return value;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00048AC4 File Offset: 0x00046CC4
		[EdmFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<decimal> collection)
		{
			ObjectQuery<decimal> objectQuery = collection as ObjectQuery<decimal>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00048B08 File Offset: 0x00046D08
		[EdmFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<decimal?> collection)
		{
			ObjectQuery<decimal?> objectQuery = collection as ObjectQuery<decimal?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00048B4C File Offset: 0x00046D4C
		[EdmFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<double> collection)
		{
			ObjectQuery<double> objectQuery = collection as ObjectQuery<double>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00048B90 File Offset: 0x00046D90
		[EdmFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<double?> collection)
		{
			ObjectQuery<double?> objectQuery = collection as ObjectQuery<double?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00048BD4 File Offset: 0x00046DD4
		[EdmFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<int> collection)
		{
			ObjectQuery<int> objectQuery = collection as ObjectQuery<int>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00048C18 File Offset: 0x00046E18
		[EdmFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<int?> collection)
		{
			ObjectQuery<int?> objectQuery = collection as ObjectQuery<int?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00048C5C File Offset: 0x00046E5C
		[EdmFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<long> collection)
		{
			ObjectQuery<long> objectQuery = collection as ObjectQuery<long>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00048CA0 File Offset: 0x00046EA0
		[EdmFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<long?> collection)
		{
			ObjectQuery<long?> objectQuery = collection as ObjectQuery<long?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00048CE4 File Offset: 0x00046EE4
		[EdmFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<decimal> collection)
		{
			ObjectQuery<decimal> objectQuery = collection as ObjectQuery<decimal>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00048D28 File Offset: 0x00046F28
		[EdmFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<decimal?> collection)
		{
			ObjectQuery<decimal?> objectQuery = collection as ObjectQuery<decimal?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00048D6C File Offset: 0x00046F6C
		[EdmFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<double> collection)
		{
			ObjectQuery<double> objectQuery = collection as ObjectQuery<double>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00048DB0 File Offset: 0x00046FB0
		[EdmFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<double?> collection)
		{
			ObjectQuery<double?> objectQuery = collection as ObjectQuery<double?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00048DF4 File Offset: 0x00046FF4
		[EdmFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<int> collection)
		{
			ObjectQuery<int> objectQuery = collection as ObjectQuery<int>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00048E38 File Offset: 0x00047038
		[EdmFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<int?> collection)
		{
			ObjectQuery<int?> objectQuery = collection as ObjectQuery<int?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00048E7C File Offset: 0x0004707C
		[EdmFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<long> collection)
		{
			ObjectQuery<long> objectQuery = collection as ObjectQuery<long>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00048EC0 File Offset: 0x000470C0
		[EdmFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<long?> collection)
		{
			ObjectQuery<long?> objectQuery = collection as ObjectQuery<long?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00048F04 File Offset: 0x00047104
		[EdmFunction("Edm", "Var")]
		public static double? Var(IEnumerable<decimal> collection)
		{
			ObjectQuery<decimal> objectQuery = collection as ObjectQuery<decimal>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00048F48 File Offset: 0x00047148
		[EdmFunction("Edm", "Var")]
		public static double? Var(IEnumerable<decimal?> collection)
		{
			ObjectQuery<decimal?> objectQuery = collection as ObjectQuery<decimal?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00048F8C File Offset: 0x0004718C
		[EdmFunction("Edm", "Var")]
		public static double? Var(IEnumerable<double> collection)
		{
			ObjectQuery<double> objectQuery = collection as ObjectQuery<double>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00048FD0 File Offset: 0x000471D0
		[EdmFunction("Edm", "Var")]
		public static double? Var(IEnumerable<double?> collection)
		{
			ObjectQuery<double?> objectQuery = collection as ObjectQuery<double?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00049014 File Offset: 0x00047214
		[EdmFunction("Edm", "Var")]
		public static double? Var(IEnumerable<int> collection)
		{
			ObjectQuery<int> objectQuery = collection as ObjectQuery<int>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00049058 File Offset: 0x00047258
		[EdmFunction("Edm", "Var")]
		public static double? Var(IEnumerable<int?> collection)
		{
			ObjectQuery<int?> objectQuery = collection as ObjectQuery<int?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0004909C File Offset: 0x0004729C
		[EdmFunction("Edm", "Var")]
		public static double? Var(IEnumerable<long> collection)
		{
			ObjectQuery<long> objectQuery = collection as ObjectQuery<long>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x000490E0 File Offset: 0x000472E0
		[EdmFunction("Edm", "Var")]
		public static double? Var(IEnumerable<long?> collection)
		{
			ObjectQuery<long?> objectQuery = collection as ObjectQuery<long?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00049124 File Offset: 0x00047324
		[EdmFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<decimal> collection)
		{
			ObjectQuery<decimal> objectQuery = collection as ObjectQuery<decimal>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00049168 File Offset: 0x00047368
		[EdmFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<decimal?> collection)
		{
			ObjectQuery<decimal?> objectQuery = collection as ObjectQuery<decimal?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000491AC File Offset: 0x000473AC
		[EdmFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<double> collection)
		{
			ObjectQuery<double> objectQuery = collection as ObjectQuery<double>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x000491F0 File Offset: 0x000473F0
		[EdmFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<double?> collection)
		{
			ObjectQuery<double?> objectQuery = collection as ObjectQuery<double?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00049234 File Offset: 0x00047434
		[EdmFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<int> collection)
		{
			ObjectQuery<int> objectQuery = collection as ObjectQuery<int>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00049278 File Offset: 0x00047478
		[EdmFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<int?> collection)
		{
			ObjectQuery<int?> objectQuery = collection as ObjectQuery<int?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x000492BC File Offset: 0x000474BC
		[EdmFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<long> collection)
		{
			ObjectQuery<long> objectQuery = collection as ObjectQuery<long>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00049300 File Offset: 0x00047500
		[EdmFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<long?> collection)
		{
			ObjectQuery<long?> objectQuery = collection as ObjectQuery<long?>;
			if (objectQuery != null)
			{
				return ((IQueryable)objectQuery).Provider.Execute<double?>(Expression.Call((MethodInfo)MethodBase.GetCurrentMethod(), Expression.Constant(collection)));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "Left")]
		public static string Left(string stringArgument, long? length)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "Right")]
		public static string Right(string stringArgument, long? length)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "Reverse")]
		public static string Reverse(string stringArgument)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "GetTotalOffsetMinutes")]
		public static int? GetTotalOffsetMinutes(DateTimeOffset? dateTimeOffsetArgument)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "TruncateTime")]
		public static DateTimeOffset? TruncateTime(DateTimeOffset? dateValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "TruncateTime")]
		public static DateTime? TruncateTime(DateTime? dateValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "CreateDateTime")]
		public static DateTime? CreateDateTime(int? year, int? month, int? day, int? hour, int? minute, double? second)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "CreateDateTimeOffset")]
		public static DateTimeOffset? CreateDateTimeOffset(int? year, int? month, int? day, int? hour, int? minute, double? second, int? timeZoneOffset)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "CreateTime")]
		public static TimeSpan? CreateTime(int? hour, int? minute, double? second)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddYears")]
		public static DateTimeOffset? AddYears(DateTimeOffset? dateValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddYears")]
		public static DateTime? AddYears(DateTime? dateValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddMonths")]
		public static DateTimeOffset? AddMonths(DateTimeOffset? dateValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddMonths")]
		public static DateTime? AddMonths(DateTime? dateValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddDays")]
		public static DateTimeOffset? AddDays(DateTimeOffset? dateValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddDays")]
		public static DateTime? AddDays(DateTime? dateValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddHours")]
		public static DateTimeOffset? AddHours(DateTimeOffset? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddHours")]
		public static DateTime? AddHours(DateTime? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddHours")]
		public static TimeSpan? AddHours(TimeSpan? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddMinutes")]
		public static DateTimeOffset? AddMinutes(DateTimeOffset? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddMinutes")]
		public static DateTime? AddMinutes(DateTime? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddMinutes")]
		public static TimeSpan? AddMinutes(TimeSpan? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddSeconds")]
		public static DateTimeOffset? AddSeconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddSeconds")]
		public static DateTime? AddSeconds(DateTime? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddSeconds")]
		public static TimeSpan? AddSeconds(TimeSpan? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddMilliseconds")]
		public static DateTimeOffset? AddMilliseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddMilliseconds")]
		public static DateTime? AddMilliseconds(DateTime? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddMilliseconds")]
		public static TimeSpan? AddMilliseconds(TimeSpan? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddMicroseconds")]
		public static DateTimeOffset? AddMicroseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddMicroseconds")]
		public static DateTime? AddMicroseconds(DateTime? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddMicroseconds")]
		public static TimeSpan? AddMicroseconds(TimeSpan? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddNanoseconds")]
		public static DateTimeOffset? AddNanoseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddNanoseconds")]
		public static DateTime? AddNanoseconds(DateTime? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "AddNanoseconds")]
		public static TimeSpan? AddNanoseconds(TimeSpan? timeValue, int? addValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffYears")]
		public static int? DiffYears(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffYears")]
		public static int? DiffYears(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffMonths")]
		public static int? DiffMonths(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffMonths")]
		public static int? DiffMonths(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffDays")]
		public static int? DiffDays(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffDays")]
		public static int? DiffDays(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffHours")]
		public static int? DiffHours(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffHours")]
		public static int? DiffHours(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffHours")]
		public static int? DiffHours(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffMinutes")]
		public static int? DiffMinutes(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffMinutes")]
		public static int? DiffMinutes(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffMinutes")]
		public static int? DiffMinutes(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffSeconds")]
		public static int? DiffSeconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffSeconds")]
		public static int? DiffSeconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffSeconds")]
		public static int? DiffSeconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffMilliseconds")]
		public static int? DiffMilliseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffMilliseconds")]
		public static int? DiffMilliseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffMilliseconds")]
		public static int? DiffMilliseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffMicroseconds")]
		public static int? DiffMicroseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffMicroseconds")]
		public static int? DiffMicroseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffMicroseconds")]
		public static int? DiffMicroseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffNanoseconds")]
		public static int? DiffNanoseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffNanoseconds")]
		public static int? DiffNanoseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "DiffNanoseconds")]
		public static int? DiffNanoseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "Truncate")]
		public static double? Truncate(double? value, int? digits)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("Edm", "Truncate")]
		public static decimal? Truncate(decimal? value, int? digits)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}
	}
}
