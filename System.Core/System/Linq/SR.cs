using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.Linq
{
	// Token: 0x02000174 RID: 372
	internal sealed class SR
	{
		// Token: 0x06000DB5 RID: 3509 RVA: 0x00030B37 File Offset: 0x0002ED37
		internal SR()
		{
			this.resources = new ResourceManager("System.Linq", base.GetType().Assembly);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00030B5C File Offset: 0x0002ED5C
		private static SR GetLoader()
		{
			if (SR.loader == null)
			{
				SR value = new SR();
				Interlocked.CompareExchange<SR>(ref SR.loader, value, null);
			}
			return SR.loader;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00030B88 File Offset: 0x0002ED88
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00030B8B File Offset: 0x0002ED8B
		public static ResourceManager Resources
		{
			get
			{
				return SR.GetLoader().resources;
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00030B98 File Offset: 0x0002ED98
		public static string GetString(string name, params object[] args)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			string @string = sr.resources.GetString(name, SR.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00030C18 File Offset: 0x0002EE18
		public static string GetString(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetString(name, SR.Culture);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00030C41 File Offset: 0x0002EE41
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return SR.GetString(name);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00030C4C File Offset: 0x0002EE4C
		public static object GetObject(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetObject(name, SR.Culture);
		}

		// Token: 0x040007B3 RID: 1971
		internal const string OwningTeam = "OwningTeam";

		// Token: 0x040007B4 RID: 1972
		internal const string ArgumentArrayHasTooManyElements = "ArgumentArrayHasTooManyElements";

		// Token: 0x040007B5 RID: 1973
		internal const string ArgumentNotIEnumerableGeneric = "ArgumentNotIEnumerableGeneric";

		// Token: 0x040007B6 RID: 1974
		internal const string ArgumentNotSequence = "ArgumentNotSequence";

		// Token: 0x040007B7 RID: 1975
		internal const string ArgumentNotValid = "ArgumentNotValid";

		// Token: 0x040007B8 RID: 1976
		internal const string IncompatibleElementTypes = "IncompatibleElementTypes";

		// Token: 0x040007B9 RID: 1977
		internal const string ArgumentNotLambda = "ArgumentNotLambda";

		// Token: 0x040007BA RID: 1978
		internal const string MoreThanOneElement = "MoreThanOneElement";

		// Token: 0x040007BB RID: 1979
		internal const string MoreThanOneMatch = "MoreThanOneMatch";

		// Token: 0x040007BC RID: 1980
		internal const string NoArgumentMatchingMethodsInQueryable = "NoArgumentMatchingMethodsInQueryable";

		// Token: 0x040007BD RID: 1981
		internal const string NoElements = "NoElements";

		// Token: 0x040007BE RID: 1982
		internal const string NoMatch = "NoMatch";

		// Token: 0x040007BF RID: 1983
		internal const string NoMethodOnType = "NoMethodOnType";

		// Token: 0x040007C0 RID: 1984
		internal const string NoMethodOnTypeMatchingArguments = "NoMethodOnTypeMatchingArguments";

		// Token: 0x040007C1 RID: 1985
		internal const string NoNameMatchingMethodsInQueryable = "NoNameMatchingMethodsInQueryable";

		// Token: 0x040007C2 RID: 1986
		internal const string EmptyEnumerable = "EmptyEnumerable";

		// Token: 0x040007C3 RID: 1987
		internal const string Argument_AdjustmentRulesNoNulls = "Argument_AdjustmentRulesNoNulls";

		// Token: 0x040007C4 RID: 1988
		internal const string Argument_AdjustmentRulesOutOfOrder = "Argument_AdjustmentRulesOutOfOrder";

		// Token: 0x040007C5 RID: 1989
		internal const string Argument_AdjustmentRulesAmbiguousOverlap = "Argument_AdjustmentRulesAmbiguousOverlap";

		// Token: 0x040007C6 RID: 1990
		internal const string Argument_AdjustmentRulesrDaylightSavingTimeOverlap = "Argument_AdjustmentRulesrDaylightSavingTimeOverlap";

		// Token: 0x040007C7 RID: 1991
		internal const string Argument_AdjustmentRulesrDaylightSavingTimeOverlapNonRuleRange = "Argument_AdjustmentRulesrDaylightSavingTimeOverlapNonRuleRange";

		// Token: 0x040007C8 RID: 1992
		internal const string Argument_AdjustmentRulesInvalidOverlap = "Argument_AdjustmentRulesInvalidOverlap";

		// Token: 0x040007C9 RID: 1993
		internal const string Argument_ConvertMismatch = "Argument_ConvertMismatch";

		// Token: 0x040007CA RID: 1994
		internal const string Argument_DateTimeHasTimeOfDay = "Argument_DateTimeHasTimeOfDay";

		// Token: 0x040007CB RID: 1995
		internal const string Argument_DateTimeIsInvalid = "Argument_DateTimeIsInvalid";

		// Token: 0x040007CC RID: 1996
		internal const string Argument_DateTimeIsNotAmbiguous = "Argument_DateTimeIsNotAmbiguous";

		// Token: 0x040007CD RID: 1997
		internal const string Argument_DateTimeOffsetIsNotAmbiguous = "Argument_DateTimeOffsetIsNotAmbiguous";

		// Token: 0x040007CE RID: 1998
		internal const string Argument_DateTimeKindMustBeUnspecified = "Argument_DateTimeKindMustBeUnspecified";

		// Token: 0x040007CF RID: 1999
		internal const string Argument_DateTimeHasTicks = "Argument_DateTimeHasTicks";

		// Token: 0x040007D0 RID: 2000
		internal const string Argument_InvalidId = "Argument_InvalidId";

		// Token: 0x040007D1 RID: 2001
		internal const string Argument_InvalidSerializedString = "Argument_InvalidSerializedString";

		// Token: 0x040007D2 RID: 2002
		internal const string Argument_InvalidREG_TZI_FORMAT = "Argument_InvalidREG_TZI_FORMAT";

		// Token: 0x040007D3 RID: 2003
		internal const string Argument_OutOfOrderDateTimes = "Argument_OutOfOrderDateTimes";

		// Token: 0x040007D4 RID: 2004
		internal const string Argument_TimeSpanHasSeconds = "Argument_TimeSpanHasSeconds";

		// Token: 0x040007D5 RID: 2005
		internal const string Argument_TimeZoneInfoBadTZif = "Argument_TimeZoneInfoBadTZif";

		// Token: 0x040007D6 RID: 2006
		internal const string Argument_TimeZoneInfoInvalidTZif = "Argument_TimeZoneInfoInvalidTZif";

		// Token: 0x040007D7 RID: 2007
		internal const string Argument_TransitionTimesAreIdentical = "Argument_TransitionTimesAreIdentical";

		// Token: 0x040007D8 RID: 2008
		internal const string ArgumentOutOfRange_DayParam = "ArgumentOutOfRange_DayParam";

		// Token: 0x040007D9 RID: 2009
		internal const string ArgumentOutOfRange_DayOfWeek = "ArgumentOutOfRange_DayOfWeek";

		// Token: 0x040007DA RID: 2010
		internal const string ArgumentOutOfRange_MonthParam = "ArgumentOutOfRange_MonthParam";

		// Token: 0x040007DB RID: 2011
		internal const string ArgumentOutOfRange_UtcOffset = "ArgumentOutOfRange_UtcOffset";

		// Token: 0x040007DC RID: 2012
		internal const string ArgumentOutOfRange_UtcOffsetAndDaylightDelta = "ArgumentOutOfRange_UtcOffsetAndDaylightDelta";

		// Token: 0x040007DD RID: 2013
		internal const string ArgumentOutOfRange_Week = "ArgumentOutOfRange_Week";

		// Token: 0x040007DE RID: 2014
		internal const string InvalidTimeZone_InvalidRegistryData = "InvalidTimeZone_InvalidRegistryData";

		// Token: 0x040007DF RID: 2015
		internal const string InvalidTimeZone_InvalidWin32APIData = "InvalidTimeZone_InvalidWin32APIData";

		// Token: 0x040007E0 RID: 2016
		internal const string Security_CannotReadRegistryData = "Security_CannotReadRegistryData";

		// Token: 0x040007E1 RID: 2017
		internal const string Serialization_CorruptField = "Serialization_CorruptField";

		// Token: 0x040007E2 RID: 2018
		internal const string Serialization_InvalidEscapeSequence = "Serialization_InvalidEscapeSequence";

		// Token: 0x040007E3 RID: 2019
		internal const string TimeZoneNotFound_MissingRegistryData = "TimeZoneNotFound_MissingRegistryData";

		// Token: 0x040007E4 RID: 2020
		internal const string ArgumentOutOfRange_DateTimeBadTicks = "ArgumentOutOfRange_DateTimeBadTicks";

		// Token: 0x040007E5 RID: 2021
		internal const string PLINQ_CommonEnumerator_Current_NotStarted = "PLINQ_CommonEnumerator_Current_NotStarted";

		// Token: 0x040007E6 RID: 2022
		internal const string PLINQ_ExternalCancellationRequested = "PLINQ_ExternalCancellationRequested";

		// Token: 0x040007E7 RID: 2023
		internal const string PLINQ_DisposeRequested = "PLINQ_DisposeRequested";

		// Token: 0x040007E8 RID: 2024
		internal const string PLINQ_EnumerationPreviouslyFailed = "PLINQ_EnumerationPreviouslyFailed";

		// Token: 0x040007E9 RID: 2025
		internal const string ParallelPartitionable_NullReturn = "ParallelPartitionable_NullReturn";

		// Token: 0x040007EA RID: 2026
		internal const string ParallelPartitionable_NullElement = "ParallelPartitionable_NullElement";

		// Token: 0x040007EB RID: 2027
		internal const string ParallelPartitionable_IncorretElementCount = "ParallelPartitionable_IncorretElementCount";

		// Token: 0x040007EC RID: 2028
		internal const string ParallelEnumerable_ToArray_DimensionRequired = "ParallelEnumerable_ToArray_DimensionRequired";

		// Token: 0x040007ED RID: 2029
		internal const string ParallelEnumerable_WithQueryExecutionMode_InvalidMode = "ParallelEnumerable_WithQueryExecutionMode_InvalidMode";

		// Token: 0x040007EE RID: 2030
		internal const string ParallelEnumerable_WithMergeOptions_InvalidOptions = "ParallelEnumerable_WithMergeOptions_InvalidOptions";

		// Token: 0x040007EF RID: 2031
		internal const string ParallelEnumerable_BinaryOpMustUseAsParallel = "ParallelEnumerable_BinaryOpMustUseAsParallel";

		// Token: 0x040007F0 RID: 2032
		internal const string ParallelEnumerable_WithCancellation_TokenSourceDisposed = "ParallelEnumerable_WithCancellation_TokenSourceDisposed";

		// Token: 0x040007F1 RID: 2033
		internal const string ParallelQuery_InvalidAsOrderedCall = "ParallelQuery_InvalidAsOrderedCall";

		// Token: 0x040007F2 RID: 2034
		internal const string ParallelQuery_InvalidNonGenericAsOrderedCall = "ParallelQuery_InvalidNonGenericAsOrderedCall";

		// Token: 0x040007F3 RID: 2035
		internal const string ParallelQuery_PartitionerNotOrderable = "ParallelQuery_PartitionerNotOrderable";

		// Token: 0x040007F4 RID: 2036
		internal const string ParallelQuery_DuplicateTaskScheduler = "ParallelQuery_DuplicateTaskScheduler";

		// Token: 0x040007F5 RID: 2037
		internal const string ParallelQuery_DuplicateDOP = "ParallelQuery_DuplicateDOP";

		// Token: 0x040007F6 RID: 2038
		internal const string ParallelQuery_DuplicateWithCancellation = "ParallelQuery_DuplicateWithCancellation";

		// Token: 0x040007F7 RID: 2039
		internal const string ParallelQuery_DuplicateExecutionMode = "ParallelQuery_DuplicateExecutionMode";

		// Token: 0x040007F8 RID: 2040
		internal const string ParallelQuery_DuplicateMergeOptions = "ParallelQuery_DuplicateMergeOptions";

		// Token: 0x040007F9 RID: 2041
		internal const string PartitionerQueryOperator_NullPartitionList = "PartitionerQueryOperator_NullPartitionList";

		// Token: 0x040007FA RID: 2042
		internal const string PartitionerQueryOperator_WrongNumberOfPartitions = "PartitionerQueryOperator_WrongNumberOfPartitions";

		// Token: 0x040007FB RID: 2043
		internal const string PartitionerQueryOperator_NullPartition = "PartitionerQueryOperator_NullPartition";

		// Token: 0x040007FC RID: 2044
		internal const string event_ParallelQueryBegin = "event_ParallelQueryBegin";

		// Token: 0x040007FD RID: 2045
		internal const string event_ParallelQueryEnd = "event_ParallelQueryEnd";

		// Token: 0x040007FE RID: 2046
		internal const string event_ParallelQueryFork = "event_ParallelQueryFork";

		// Token: 0x040007FF RID: 2047
		internal const string event_ParallelQueryJoin = "event_ParallelQueryJoin";

		// Token: 0x04000800 RID: 2048
		private static SR loader;

		// Token: 0x04000801 RID: 2049
		private ResourceManager resources;
	}
}
