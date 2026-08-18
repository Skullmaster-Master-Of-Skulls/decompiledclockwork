using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder
{
	// Token: 0x0200011D RID: 285
	public static class EdmFunctions
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x0002ABB0 File Offset: 0x00028DB0
		private static EdmFunction ResolveCanonicalFunction(string functionName, TypeUsage[] argumentTypes)
		{
			List<EdmFunction> list = new List<EdmFunction>(from func in EdmProviderManifest.Instance.GetStoreFunctions()
			where string.Equals(func.Name, functionName, StringComparison.Ordinal)
			select func);
			EdmFunction edmFunction = null;
			bool flag = false;
			if (list.Count > 0)
			{
				edmFunction = FunctionOverloadResolver.ResolveFunctionOverloads(list, argumentTypes, false, out flag);
				if (flag)
				{
					throw new ArgumentException(Strings.Cqt_Function_CanonicalFunction_AmbiguousMatch(functionName));
				}
			}
			if (edmFunction == null)
			{
				throw new ArgumentException(Strings.Cqt_Function_CanonicalFunction_NotFound(functionName));
			}
			return edmFunction;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0002AC2C File Offset: 0x00028E2C
		internal static DbFunctionExpression InvokeCanonicalFunction(string functionName, params DbExpression[] arguments)
		{
			TypeUsage[] array = new TypeUsage[arguments.Length];
			for (int i = 0; i < arguments.Length; i++)
			{
				array[i] = arguments[i].ResultType;
			}
			EdmFunction function = EdmFunctions.ResolveCanonicalFunction(functionName, array);
			return function.Invoke(arguments);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0002AC6C File Offset: 0x00028E6C
		public static DbFunctionExpression Average(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Avg", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002AC9C File Offset: 0x00028E9C
		public static DbFunctionExpression Count(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Count", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0002ACCC File Offset: 0x00028ECC
		public static DbFunctionExpression LongCount(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("BigCount", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0002ACFC File Offset: 0x00028EFC
		public static DbFunctionExpression Max(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Max", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002AD2C File Offset: 0x00028F2C
		public static DbFunctionExpression Min(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Min", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002AD5C File Offset: 0x00028F5C
		public static DbFunctionExpression Sum(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Sum", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002AD8C File Offset: 0x00028F8C
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "St")]
		public static DbFunctionExpression StDev(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("StDev", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002ADBC File Offset: 0x00028FBC
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "St")]
		public static DbFunctionExpression StDevP(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("StDevP", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002ADEC File Offset: 0x00028FEC
		public static DbFunctionExpression Var(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Var", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0002AE1C File Offset: 0x0002901C
		public static DbFunctionExpression VarP(this DbExpression collection)
		{
			Check.NotNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("VarP", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0002AE4C File Offset: 0x0002904C
		public static DbFunctionExpression Concat(this DbExpression string1, DbExpression string2)
		{
			Check.NotNull<DbExpression>(string1, "string1");
			Check.NotNull<DbExpression>(string2, "string2");
			return EdmFunctions.InvokeCanonicalFunction("Concat", new DbExpression[]
			{
				string1,
				string2
			});
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002AE8C File Offset: 0x0002908C
		public static DbExpression Contains(this DbExpression searchedString, DbExpression searchedForString)
		{
			Check.NotNull<DbExpression>(searchedString, "searchedString");
			Check.NotNull<DbExpression>(searchedForString, "searchedForString");
			return EdmFunctions.InvokeCanonicalFunction("Contains", new DbExpression[]
			{
				searchedString,
				searchedForString
			});
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0002AECC File Offset: 0x000290CC
		public static DbFunctionExpression EndsWith(this DbExpression stringArgument, DbExpression suffix)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(suffix, "suffix");
			return EdmFunctions.InvokeCanonicalFunction("EndsWith", new DbExpression[]
			{
				stringArgument,
				suffix
			});
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0002AF0C File Offset: 0x0002910C
		public static DbFunctionExpression IndexOf(this DbExpression searchString, DbExpression stringToFind)
		{
			Check.NotNull<DbExpression>(searchString, "searchString");
			Check.NotNull<DbExpression>(stringToFind, "stringToFind");
			return EdmFunctions.InvokeCanonicalFunction("IndexOf", new DbExpression[]
			{
				stringToFind,
				searchString
			});
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002AF4C File Offset: 0x0002914C
		public static DbFunctionExpression Left(this DbExpression stringArgument, DbExpression length)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(length, "length");
			return EdmFunctions.InvokeCanonicalFunction("Left", new DbExpression[]
			{
				stringArgument,
				length
			});
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002AF8C File Offset: 0x0002918C
		public static DbFunctionExpression Length(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("Length", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002AFBC File Offset: 0x000291BC
		public static DbFunctionExpression Replace(this DbExpression stringArgument, DbExpression toReplace, DbExpression replacement)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(toReplace, "toReplace");
			Check.NotNull<DbExpression>(replacement, "replacement");
			return EdmFunctions.InvokeCanonicalFunction("Replace", new DbExpression[]
			{
				stringArgument,
				toReplace,
				replacement
			});
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0002B00C File Offset: 0x0002920C
		public static DbFunctionExpression Reverse(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("Reverse", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0002B03C File Offset: 0x0002923C
		public static DbFunctionExpression Right(this DbExpression stringArgument, DbExpression length)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(length, "length");
			return EdmFunctions.InvokeCanonicalFunction("Right", new DbExpression[]
			{
				stringArgument,
				length
			});
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002B07C File Offset: 0x0002927C
		public static DbFunctionExpression StartsWith(this DbExpression stringArgument, DbExpression prefix)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(prefix, "prefix");
			return EdmFunctions.InvokeCanonicalFunction("StartsWith", new DbExpression[]
			{
				stringArgument,
				prefix
			});
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0002B0BC File Offset: 0x000292BC
		public static DbFunctionExpression Substring(this DbExpression stringArgument, DbExpression start, DbExpression length)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			Check.NotNull<DbExpression>(start, "start");
			Check.NotNull<DbExpression>(length, "length");
			return EdmFunctions.InvokeCanonicalFunction("Substring", new DbExpression[]
			{
				stringArgument,
				start,
				length
			});
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002B10C File Offset: 0x0002930C
		public static DbFunctionExpression ToLower(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("ToLower", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0002B13C File Offset: 0x0002933C
		public static DbFunctionExpression ToUpper(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("ToUpper", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0002B16C File Offset: 0x0002936C
		public static DbFunctionExpression Trim(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("Trim", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0002B19C File Offset: 0x0002939C
		public static DbFunctionExpression TrimEnd(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("RTrim", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002B1CC File Offset: 0x000293CC
		public static DbFunctionExpression TrimStart(this DbExpression stringArgument)
		{
			Check.NotNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("LTrim", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002B1FC File Offset: 0x000293FC
		public static DbFunctionExpression Year(this DbExpression dateValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("Year", new DbExpression[]
			{
				dateValue
			});
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002B22C File Offset: 0x0002942C
		public static DbFunctionExpression Month(this DbExpression dateValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("Month", new DbExpression[]
			{
				dateValue
			});
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0002B25C File Offset: 0x0002945C
		public static DbFunctionExpression Day(this DbExpression dateValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("Day", new DbExpression[]
			{
				dateValue
			});
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0002B28C File Offset: 0x0002948C
		public static DbFunctionExpression DayOfYear(this DbExpression dateValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("DayOfYear", new DbExpression[]
			{
				dateValue
			});
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0002B2BC File Offset: 0x000294BC
		public static DbFunctionExpression Hour(this DbExpression timeValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Hour", new DbExpression[]
			{
				timeValue
			});
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002B2EC File Offset: 0x000294EC
		public static DbFunctionExpression Minute(this DbExpression timeValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Minute", new DbExpression[]
			{
				timeValue
			});
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0002B31C File Offset: 0x0002951C
		public static DbFunctionExpression Second(this DbExpression timeValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Second", new DbExpression[]
			{
				timeValue
			});
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0002B34C File Offset: 0x0002954C
		public static DbFunctionExpression Millisecond(this DbExpression timeValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Millisecond", new DbExpression[]
			{
				timeValue
			});
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0002B37C File Offset: 0x0002957C
		public static DbFunctionExpression GetTotalOffsetMinutes(this DbExpression dateTimeOffsetArgument)
		{
			Check.NotNull<DbExpression>(dateTimeOffsetArgument, "dateTimeOffsetArgument");
			return EdmFunctions.InvokeCanonicalFunction("GetTotalOffsetMinutes", new DbExpression[]
			{
				dateTimeOffsetArgument
			});
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002B3AB File Offset: 0x000295AB
		public static DbFunctionExpression CurrentDateTime()
		{
			return EdmFunctions.InvokeCanonicalFunction("CurrentDateTime", new DbExpression[0]);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002B3BD File Offset: 0x000295BD
		public static DbFunctionExpression CurrentDateTimeOffset()
		{
			return EdmFunctions.InvokeCanonicalFunction("CurrentDateTimeOffset", new DbExpression[0]);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002B3CF File Offset: 0x000295CF
		public static DbFunctionExpression CurrentUtcDateTime()
		{
			return EdmFunctions.InvokeCanonicalFunction("CurrentUtcDateTime", new DbExpression[0]);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002B3E4 File Offset: 0x000295E4
		public static DbFunctionExpression TruncateTime(this DbExpression dateValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("TruncateTime", new DbExpression[]
			{
				dateValue
			});
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002B414 File Offset: 0x00029614
		public static DbFunctionExpression CreateDateTime(DbExpression year, DbExpression month, DbExpression day, DbExpression hour, DbExpression minute, DbExpression second)
		{
			Check.NotNull<DbExpression>(year, "year");
			Check.NotNull<DbExpression>(month, "month");
			Check.NotNull<DbExpression>(day, "day");
			Check.NotNull<DbExpression>(hour, "hour");
			Check.NotNull<DbExpression>(minute, "minute");
			Check.NotNull<DbExpression>(second, "second");
			return EdmFunctions.InvokeCanonicalFunction("CreateDateTime", new DbExpression[]
			{
				year,
				month,
				day,
				hour,
				minute,
				second
			});
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002B498 File Offset: 0x00029698
		public static DbFunctionExpression CreateDateTimeOffset(DbExpression year, DbExpression month, DbExpression day, DbExpression hour, DbExpression minute, DbExpression second, DbExpression timeZoneOffset)
		{
			Check.NotNull<DbExpression>(year, "year");
			Check.NotNull<DbExpression>(month, "month");
			Check.NotNull<DbExpression>(day, "day");
			Check.NotNull<DbExpression>(hour, "hour");
			Check.NotNull<DbExpression>(minute, "minute");
			Check.NotNull<DbExpression>(second, "second");
			Check.NotNull<DbExpression>(timeZoneOffset, "timeZoneOffset");
			return EdmFunctions.InvokeCanonicalFunction("CreateDateTimeOffset", new DbExpression[]
			{
				year,
				month,
				day,
				hour,
				minute,
				second,
				timeZoneOffset
			});
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0002B530 File Offset: 0x00029730
		public static DbFunctionExpression CreateTime(DbExpression hour, DbExpression minute, DbExpression second)
		{
			Check.NotNull<DbExpression>(hour, "hour");
			Check.NotNull<DbExpression>(minute, "minute");
			Check.NotNull<DbExpression>(second, "second");
			return EdmFunctions.InvokeCanonicalFunction("CreateTime", new DbExpression[]
			{
				hour,
				minute,
				second
			});
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0002B580 File Offset: 0x00029780
		public static DbFunctionExpression AddYears(this DbExpression dateValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddYears", new DbExpression[]
			{
				dateValue,
				addValue
			});
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002B5C0 File Offset: 0x000297C0
		public static DbFunctionExpression AddMonths(this DbExpression dateValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMonths", new DbExpression[]
			{
				dateValue,
				addValue
			});
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002B600 File Offset: 0x00029800
		public static DbFunctionExpression AddDays(this DbExpression dateValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(dateValue, "dateValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddDays", new DbExpression[]
			{
				dateValue,
				addValue
			});
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0002B640 File Offset: 0x00029840
		public static DbFunctionExpression AddHours(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddHours", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002B680 File Offset: 0x00029880
		public static DbFunctionExpression AddMinutes(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMinutes", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002B6C0 File Offset: 0x000298C0
		public static DbFunctionExpression AddSeconds(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddSeconds", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002B700 File Offset: 0x00029900
		public static DbFunctionExpression AddMilliseconds(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMilliseconds", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002B740 File Offset: 0x00029940
		public static DbFunctionExpression AddMicroseconds(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMicroseconds", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002B780 File Offset: 0x00029980
		public static DbFunctionExpression AddNanoseconds(this DbExpression timeValue, DbExpression addValue)
		{
			Check.NotNull<DbExpression>(timeValue, "timeValue");
			Check.NotNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddNanoseconds", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002B7C0 File Offset: 0x000299C0
		public static DbFunctionExpression DiffYears(this DbExpression dateValue1, DbExpression dateValue2)
		{
			Check.NotNull<DbExpression>(dateValue1, "dateValue1");
			Check.NotNull<DbExpression>(dateValue2, "dateValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffYears", new DbExpression[]
			{
				dateValue1,
				dateValue2
			});
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002B800 File Offset: 0x00029A00
		public static DbFunctionExpression DiffMonths(this DbExpression dateValue1, DbExpression dateValue2)
		{
			Check.NotNull<DbExpression>(dateValue1, "dateValue1");
			Check.NotNull<DbExpression>(dateValue2, "dateValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMonths", new DbExpression[]
			{
				dateValue1,
				dateValue2
			});
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002B840 File Offset: 0x00029A40
		public static DbFunctionExpression DiffDays(this DbExpression dateValue1, DbExpression dateValue2)
		{
			Check.NotNull<DbExpression>(dateValue1, "dateValue1");
			Check.NotNull<DbExpression>(dateValue2, "dateValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffDays", new DbExpression[]
			{
				dateValue1,
				dateValue2
			});
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002B880 File Offset: 0x00029A80
		public static DbFunctionExpression DiffHours(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffHours", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002B8C0 File Offset: 0x00029AC0
		public static DbFunctionExpression DiffMinutes(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMinutes", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002B900 File Offset: 0x00029B00
		public static DbFunctionExpression DiffSeconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffSeconds", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002B940 File Offset: 0x00029B40
		public static DbFunctionExpression DiffMilliseconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMilliseconds", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002B980 File Offset: 0x00029B80
		public static DbFunctionExpression DiffMicroseconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMicroseconds", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002B9C0 File Offset: 0x00029BC0
		public static DbFunctionExpression DiffNanoseconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			Check.NotNull<DbExpression>(timeValue1, "timeValue1");
			Check.NotNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffNanoseconds", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0002BA00 File Offset: 0x00029C00
		public static DbFunctionExpression Round(this DbExpression value)
		{
			Check.NotNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Round", new DbExpression[]
			{
				value
			});
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002BA30 File Offset: 0x00029C30
		public static DbFunctionExpression Round(this DbExpression value, DbExpression digits)
		{
			Check.NotNull<DbExpression>(value, "value");
			Check.NotNull<DbExpression>(digits, "digits");
			return EdmFunctions.InvokeCanonicalFunction("Round", new DbExpression[]
			{
				value,
				digits
			});
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002BA70 File Offset: 0x00029C70
		public static DbFunctionExpression Floor(this DbExpression value)
		{
			Check.NotNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Floor", new DbExpression[]
			{
				value
			});
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002BAA0 File Offset: 0x00029CA0
		public static DbFunctionExpression Ceiling(this DbExpression value)
		{
			Check.NotNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Ceiling", new DbExpression[]
			{
				value
			});
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002BAD0 File Offset: 0x00029CD0
		public static DbFunctionExpression Abs(this DbExpression value)
		{
			Check.NotNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Abs", new DbExpression[]
			{
				value
			});
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0002BB00 File Offset: 0x00029D00
		public static DbFunctionExpression Truncate(this DbExpression value, DbExpression digits)
		{
			Check.NotNull<DbExpression>(value, "value");
			Check.NotNull<DbExpression>(digits, "digits");
			return EdmFunctions.InvokeCanonicalFunction("Truncate", new DbExpression[]
			{
				value,
				digits
			});
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002BB40 File Offset: 0x00029D40
		public static DbFunctionExpression Power(this DbExpression baseArgument, DbExpression exponent)
		{
			Check.NotNull<DbExpression>(baseArgument, "baseArgument");
			Check.NotNull<DbExpression>(exponent, "exponent");
			return EdmFunctions.InvokeCanonicalFunction("Power", new DbExpression[]
			{
				baseArgument,
				exponent
			});
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002BB80 File Offset: 0x00029D80
		public static DbFunctionExpression BitwiseAnd(this DbExpression value1, DbExpression value2)
		{
			Check.NotNull<DbExpression>(value1, "value1");
			Check.NotNull<DbExpression>(value2, "value2");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseAnd", new DbExpression[]
			{
				value1,
				value2
			});
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002BBC0 File Offset: 0x00029DC0
		public static DbFunctionExpression BitwiseOr(this DbExpression value1, DbExpression value2)
		{
			Check.NotNull<DbExpression>(value1, "value1");
			Check.NotNull<DbExpression>(value2, "value2");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseOr", new DbExpression[]
			{
				value1,
				value2
			});
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002BC00 File Offset: 0x00029E00
		public static DbFunctionExpression BitwiseNot(this DbExpression value)
		{
			Check.NotNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseNot", new DbExpression[]
			{
				value
			});
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002BC30 File Offset: 0x00029E30
		public static DbFunctionExpression BitwiseXor(this DbExpression value1, DbExpression value2)
		{
			Check.NotNull<DbExpression>(value1, "value1");
			Check.NotNull<DbExpression>(value2, "value2");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseXor", new DbExpression[]
			{
				value1,
				value2
			});
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002BC6F File Offset: 0x00029E6F
		public static DbFunctionExpression NewGuid()
		{
			return EdmFunctions.InvokeCanonicalFunction("NewGuid", new DbExpression[0]);
		}
	}
}
