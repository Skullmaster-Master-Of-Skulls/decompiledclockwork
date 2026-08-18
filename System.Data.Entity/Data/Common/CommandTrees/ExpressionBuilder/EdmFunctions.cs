using System;
using System.Collections.Generic;
using System.Data.Common.EntitySql;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Common.CommandTrees.ExpressionBuilder
{
	// Token: 0x02000428 RID: 1064
	public static class EdmFunctions
	{
		// Token: 0x06003824 RID: 14372 RVA: 0x000D5588 File Offset: 0x000D3788
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
					throw EntityUtil.Argument(Strings.Cqt_Function_CanonicalFunction_AmbiguousMatch(functionName));
				}
			}
			if (edmFunction == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_Function_CanonicalFunction_NotFound(functionName));
			}
			return edmFunction;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000D5604 File Offset: 0x000D3804
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

		// Token: 0x06003826 RID: 14374 RVA: 0x000D5642 File Offset: 0x000D3842
		public static DbFunctionExpression Average(this DbExpression collection)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Avg", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000D5664 File Offset: 0x000D3864
		public static DbFunctionExpression Count(this DbExpression collection)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Count", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000D5686 File Offset: 0x000D3886
		public static DbFunctionExpression LongCount(this DbExpression collection)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("BigCount", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000D56A8 File Offset: 0x000D38A8
		public static DbFunctionExpression Max(this DbExpression collection)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Max", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000D56CA File Offset: 0x000D38CA
		public static DbFunctionExpression Min(this DbExpression collection)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Min", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000D56EC File Offset: 0x000D38EC
		public static DbFunctionExpression Sum(this DbExpression collection)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Sum", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000D570E File Offset: 0x000D390E
		public static DbFunctionExpression StDev(this DbExpression collection)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("StDev", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000D5730 File Offset: 0x000D3930
		public static DbFunctionExpression StDevP(this DbExpression collection)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("StDevP", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x000D5752 File Offset: 0x000D3952
		public static DbFunctionExpression Var(this DbExpression collection)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("Var", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000D5774 File Offset: 0x000D3974
		public static DbFunctionExpression VarP(this DbExpression collection)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(collection, "collection");
			return EdmFunctions.InvokeCanonicalFunction("VarP", new DbExpression[]
			{
				collection
			});
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000D5796 File Offset: 0x000D3996
		public static DbFunctionExpression Concat(this DbExpression string1, DbExpression string2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(string1, "string1");
			EntityUtil.CheckArgumentNull<DbExpression>(string2, "string2");
			return EdmFunctions.InvokeCanonicalFunction("Concat", new DbExpression[]
			{
				string1,
				string2
			});
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000D57C8 File Offset: 0x000D39C8
		public static DbExpression Contains(this DbExpression searchedString, DbExpression searchedForString)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(searchedString, "searchedString");
			EntityUtil.CheckArgumentNull<DbExpression>(searchedForString, "searchedForString");
			return EdmFunctions.InvokeCanonicalFunction("Contains", new DbExpression[]
			{
				searchedString,
				searchedForString
			});
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000D57FA File Offset: 0x000D39FA
		public static DbFunctionExpression EndsWith(this DbExpression stringArgument, DbExpression suffix)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			EntityUtil.CheckArgumentNull<DbExpression>(suffix, "suffix");
			return EdmFunctions.InvokeCanonicalFunction("EndsWith", new DbExpression[]
			{
				stringArgument,
				suffix
			});
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000D582C File Offset: 0x000D3A2C
		public static DbFunctionExpression IndexOf(this DbExpression searchString, DbExpression stringToFind)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(searchString, "searchString");
			EntityUtil.CheckArgumentNull<DbExpression>(stringToFind, "stringToFind");
			return EdmFunctions.InvokeCanonicalFunction("IndexOf", new DbExpression[]
			{
				stringToFind,
				searchString
			});
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000D585E File Offset: 0x000D3A5E
		public static DbFunctionExpression Left(this DbExpression stringArgument, DbExpression length)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			EntityUtil.CheckArgumentNull<DbExpression>(length, "length");
			return EdmFunctions.InvokeCanonicalFunction("Left", new DbExpression[]
			{
				stringArgument,
				length
			});
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000D5890 File Offset: 0x000D3A90
		public static DbFunctionExpression Length(this DbExpression stringArgument)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("Length", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000D58B4 File Offset: 0x000D3AB4
		public static DbFunctionExpression Replace(this DbExpression stringArgument, DbExpression toReplace, DbExpression replacement)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			EntityUtil.CheckArgumentNull<DbExpression>(toReplace, "toReplace");
			EntityUtil.CheckArgumentNull<DbExpression>(replacement, "replacement");
			return EdmFunctions.InvokeCanonicalFunction("Replace", new DbExpression[]
			{
				stringArgument,
				toReplace,
				replacement
			});
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000D5901 File Offset: 0x000D3B01
		public static DbFunctionExpression Reverse(this DbExpression stringArgument)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("Reverse", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000D5923 File Offset: 0x000D3B23
		public static DbFunctionExpression Right(this DbExpression stringArgument, DbExpression length)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			EntityUtil.CheckArgumentNull<DbExpression>(length, "length");
			return EdmFunctions.InvokeCanonicalFunction("Right", new DbExpression[]
			{
				stringArgument,
				length
			});
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000D5955 File Offset: 0x000D3B55
		public static DbFunctionExpression StartsWith(this DbExpression stringArgument, DbExpression prefix)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			EntityUtil.CheckArgumentNull<DbExpression>(prefix, "prefix");
			return EdmFunctions.InvokeCanonicalFunction("StartsWith", new DbExpression[]
			{
				stringArgument,
				prefix
			});
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000D5988 File Offset: 0x000D3B88
		public static DbFunctionExpression Substring(this DbExpression stringArgument, DbExpression start, DbExpression length)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			EntityUtil.CheckArgumentNull<DbExpression>(start, "start");
			EntityUtil.CheckArgumentNull<DbExpression>(length, "length");
			return EdmFunctions.InvokeCanonicalFunction("Substring", new DbExpression[]
			{
				stringArgument,
				start,
				length
			});
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000D59D5 File Offset: 0x000D3BD5
		public static DbFunctionExpression ToLower(this DbExpression stringArgument)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("ToLower", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000D59F7 File Offset: 0x000D3BF7
		public static DbFunctionExpression ToUpper(this DbExpression stringArgument)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("ToUpper", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000D5A19 File Offset: 0x000D3C19
		public static DbFunctionExpression Trim(this DbExpression stringArgument)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("Trim", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000D5A3B File Offset: 0x000D3C3B
		public static DbFunctionExpression TrimEnd(this DbExpression stringArgument)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("RTrim", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000D5A5D File Offset: 0x000D3C5D
		public static DbFunctionExpression TrimStart(this DbExpression stringArgument)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(stringArgument, "stringArgument");
			return EdmFunctions.InvokeCanonicalFunction("LTrim", new DbExpression[]
			{
				stringArgument
			});
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000D5A7F File Offset: 0x000D3C7F
		public static DbFunctionExpression Year(this DbExpression dateValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("Year", new DbExpression[]
			{
				dateValue
			});
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000D5AA1 File Offset: 0x000D3CA1
		public static DbFunctionExpression Month(this DbExpression dateValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("Month", new DbExpression[]
			{
				dateValue
			});
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x000D5AC3 File Offset: 0x000D3CC3
		public static DbFunctionExpression Day(this DbExpression dateValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("Day", new DbExpression[]
			{
				dateValue
			});
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000D5AE5 File Offset: 0x000D3CE5
		public static DbFunctionExpression DayOfYear(this DbExpression dateValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("DayOfYear", new DbExpression[]
			{
				dateValue
			});
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x000D5B07 File Offset: 0x000D3D07
		public static DbFunctionExpression Hour(this DbExpression timeValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Hour", new DbExpression[]
			{
				timeValue
			});
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000D5B29 File Offset: 0x000D3D29
		public static DbFunctionExpression Minute(this DbExpression timeValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Minute", new DbExpression[]
			{
				timeValue
			});
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x000D5B4B File Offset: 0x000D3D4B
		public static DbFunctionExpression Second(this DbExpression timeValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Second", new DbExpression[]
			{
				timeValue
			});
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000D5B6D File Offset: 0x000D3D6D
		public static DbFunctionExpression Millisecond(this DbExpression timeValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue, "timeValue");
			return EdmFunctions.InvokeCanonicalFunction("Millisecond", new DbExpression[]
			{
				timeValue
			});
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x000D5B8F File Offset: 0x000D3D8F
		public static DbFunctionExpression GetTotalOffsetMinutes(this DbExpression dateTimeOffsetArgument)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateTimeOffsetArgument, "dateTimeOffsetArgument");
			return EdmFunctions.InvokeCanonicalFunction("GetTotalOffsetMinutes", new DbExpression[]
			{
				dateTimeOffsetArgument
			});
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x000D5BB1 File Offset: 0x000D3DB1
		public static DbFunctionExpression CurrentDateTime()
		{
			return EdmFunctions.InvokeCanonicalFunction("CurrentDateTime", new DbExpression[0]);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000D5BC3 File Offset: 0x000D3DC3
		public static DbFunctionExpression CurrentDateTimeOffset()
		{
			return EdmFunctions.InvokeCanonicalFunction("CurrentDateTimeOffset", new DbExpression[0]);
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000D5BD5 File Offset: 0x000D3DD5
		public static DbFunctionExpression CurrentUtcDateTime()
		{
			return EdmFunctions.InvokeCanonicalFunction("CurrentUtcDateTime", new DbExpression[0]);
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000D5BE7 File Offset: 0x000D3DE7
		public static DbFunctionExpression TruncateTime(this DbExpression dateValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue, "dateValue");
			return EdmFunctions.InvokeCanonicalFunction("TruncateTime", new DbExpression[]
			{
				dateValue
			});
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000D5C0C File Offset: 0x000D3E0C
		public static DbFunctionExpression CreateDateTime(DbExpression year, DbExpression month, DbExpression day, DbExpression hour, DbExpression minute, DbExpression second)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(year, "year");
			EntityUtil.CheckArgumentNull<DbExpression>(month, "month");
			EntityUtil.CheckArgumentNull<DbExpression>(day, "day");
			EntityUtil.CheckArgumentNull<DbExpression>(hour, "hour");
			EntityUtil.CheckArgumentNull<DbExpression>(minute, "minute");
			EntityUtil.CheckArgumentNull<DbExpression>(second, "second");
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

		// Token: 0x0600384E RID: 14414 RVA: 0x000D5C90 File Offset: 0x000D3E90
		public static DbFunctionExpression CreateDateTimeOffset(DbExpression year, DbExpression month, DbExpression day, DbExpression hour, DbExpression minute, DbExpression second, DbExpression timeZoneOffset)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(year, "year");
			EntityUtil.CheckArgumentNull<DbExpression>(month, "month");
			EntityUtil.CheckArgumentNull<DbExpression>(day, "day");
			EntityUtil.CheckArgumentNull<DbExpression>(hour, "hour");
			EntityUtil.CheckArgumentNull<DbExpression>(minute, "minute");
			EntityUtil.CheckArgumentNull<DbExpression>(second, "second");
			EntityUtil.CheckArgumentNull<DbExpression>(timeZoneOffset, "timeZoneOffset");
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

		// Token: 0x0600384F RID: 14415 RVA: 0x000D5D24 File Offset: 0x000D3F24
		public static DbFunctionExpression CreateTime(DbExpression hour, DbExpression minute, DbExpression second)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(hour, "hour");
			EntityUtil.CheckArgumentNull<DbExpression>(minute, "minute");
			EntityUtil.CheckArgumentNull<DbExpression>(second, "second");
			return EdmFunctions.InvokeCanonicalFunction("CreateTime", new DbExpression[]
			{
				hour,
				minute,
				second
			});
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x000D5D71 File Offset: 0x000D3F71
		public static DbFunctionExpression AddYears(this DbExpression dateValue, DbExpression addValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue, "dateValue");
			EntityUtil.CheckArgumentNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddYears", new DbExpression[]
			{
				dateValue,
				addValue
			});
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000D5DA3 File Offset: 0x000D3FA3
		public static DbFunctionExpression AddMonths(this DbExpression dateValue, DbExpression addValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue, "dateValue");
			EntityUtil.CheckArgumentNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMonths", new DbExpression[]
			{
				dateValue,
				addValue
			});
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000D5DD5 File Offset: 0x000D3FD5
		public static DbFunctionExpression AddDays(this DbExpression dateValue, DbExpression addValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue, "dateValue");
			EntityUtil.CheckArgumentNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddDays", new DbExpression[]
			{
				dateValue,
				addValue
			});
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000D5E07 File Offset: 0x000D4007
		public static DbFunctionExpression AddHours(this DbExpression timeValue, DbExpression addValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue, "timeValue");
			EntityUtil.CheckArgumentNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddHours", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000D5E39 File Offset: 0x000D4039
		public static DbFunctionExpression AddMinutes(this DbExpression timeValue, DbExpression addValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue, "timeValue");
			EntityUtil.CheckArgumentNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMinutes", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000D5E6B File Offset: 0x000D406B
		public static DbFunctionExpression AddSeconds(this DbExpression timeValue, DbExpression addValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue, "timeValue");
			EntityUtil.CheckArgumentNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddSeconds", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000D5E9D File Offset: 0x000D409D
		public static DbFunctionExpression AddMilliseconds(this DbExpression timeValue, DbExpression addValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue, "timeValue");
			EntityUtil.CheckArgumentNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMilliseconds", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000D5ECF File Offset: 0x000D40CF
		public static DbFunctionExpression AddMicroseconds(this DbExpression timeValue, DbExpression addValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue, "timeValue");
			EntityUtil.CheckArgumentNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddMicroseconds", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000D5F01 File Offset: 0x000D4101
		public static DbFunctionExpression AddNanoseconds(this DbExpression timeValue, DbExpression addValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue, "timeValue");
			EntityUtil.CheckArgumentNull<DbExpression>(addValue, "addValue");
			return EdmFunctions.InvokeCanonicalFunction("AddNanoseconds", new DbExpression[]
			{
				timeValue,
				addValue
			});
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x000D5F33 File Offset: 0x000D4133
		public static DbFunctionExpression DiffYears(this DbExpression dateValue1, DbExpression dateValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue1, "dateValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue2, "dateValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffYears", new DbExpression[]
			{
				dateValue1,
				dateValue2
			});
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000D5F65 File Offset: 0x000D4165
		public static DbFunctionExpression DiffMonths(this DbExpression dateValue1, DbExpression dateValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue1, "dateValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue2, "dateValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMonths", new DbExpression[]
			{
				dateValue1,
				dateValue2
			});
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000D5F97 File Offset: 0x000D4197
		public static DbFunctionExpression DiffDays(this DbExpression dateValue1, DbExpression dateValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue1, "dateValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(dateValue2, "dateValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffDays", new DbExpression[]
			{
				dateValue1,
				dateValue2
			});
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x000D5FC9 File Offset: 0x000D41C9
		public static DbFunctionExpression DiffHours(this DbExpression timeValue1, DbExpression timeValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue1, "timeValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffHours", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000D5FFB File Offset: 0x000D41FB
		public static DbFunctionExpression DiffMinutes(this DbExpression timeValue1, DbExpression timeValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue1, "timeValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMinutes", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000D602D File Offset: 0x000D422D
		public static DbFunctionExpression DiffSeconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue1, "timeValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffSeconds", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x000D605F File Offset: 0x000D425F
		public static DbFunctionExpression DiffMilliseconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue1, "timeValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMilliseconds", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000D6091 File Offset: 0x000D4291
		public static DbFunctionExpression DiffMicroseconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue1, "timeValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffMicroseconds", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000D60C3 File Offset: 0x000D42C3
		public static DbFunctionExpression DiffNanoseconds(this DbExpression timeValue1, DbExpression timeValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue1, "timeValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(timeValue2, "timeValue2");
			return EdmFunctions.InvokeCanonicalFunction("DiffNanoseconds", new DbExpression[]
			{
				timeValue1,
				timeValue2
			});
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000D60F5 File Offset: 0x000D42F5
		public static DbFunctionExpression Round(this DbExpression value)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Round", new DbExpression[]
			{
				value
			});
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000D6117 File Offset: 0x000D4317
		public static DbFunctionExpression Round(this DbExpression value, DbExpression digits)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(value, "value");
			EntityUtil.CheckArgumentNull<DbExpression>(digits, "digits");
			return EdmFunctions.InvokeCanonicalFunction("Round", new DbExpression[]
			{
				value,
				digits
			});
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000D6149 File Offset: 0x000D4349
		public static DbFunctionExpression Floor(this DbExpression value)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Floor", new DbExpression[]
			{
				value
			});
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000D616B File Offset: 0x000D436B
		public static DbFunctionExpression Ceiling(this DbExpression value)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Ceiling", new DbExpression[]
			{
				value
			});
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000D618D File Offset: 0x000D438D
		public static DbFunctionExpression Abs(this DbExpression value)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("Abs", new DbExpression[]
			{
				value
			});
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000D61AF File Offset: 0x000D43AF
		public static DbFunctionExpression Truncate(this DbExpression value, DbExpression digits)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(value, "value");
			EntityUtil.CheckArgumentNull<DbExpression>(digits, "digits");
			return EdmFunctions.InvokeCanonicalFunction("Truncate", new DbExpression[]
			{
				value,
				digits
			});
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000D61E1 File Offset: 0x000D43E1
		public static DbFunctionExpression Power(this DbExpression baseArgument, DbExpression exponent)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(baseArgument, "baseArgument");
			EntityUtil.CheckArgumentNull<DbExpression>(exponent, "exponent");
			return EdmFunctions.InvokeCanonicalFunction("Power", new DbExpression[]
			{
				baseArgument,
				exponent
			});
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000D6213 File Offset: 0x000D4413
		public static DbFunctionExpression BitwiseAnd(this DbExpression value1, DbExpression value2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(value1, "value1");
			EntityUtil.CheckArgumentNull<DbExpression>(value2, "value2");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseAnd", new DbExpression[]
			{
				value1,
				value2
			});
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000D6245 File Offset: 0x000D4445
		public static DbFunctionExpression BitwiseOr(this DbExpression value1, DbExpression value2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(value1, "value1");
			EntityUtil.CheckArgumentNull<DbExpression>(value2, "value2");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseOr", new DbExpression[]
			{
				value1,
				value2
			});
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000D6277 File Offset: 0x000D4477
		public static DbFunctionExpression BitwiseNot(this DbExpression value)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(value, "value");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseNot", new DbExpression[]
			{
				value
			});
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000D6299 File Offset: 0x000D4499
		public static DbFunctionExpression BitwiseXor(this DbExpression value1, DbExpression value2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(value1, "value1");
			EntityUtil.CheckArgumentNull<DbExpression>(value2, "value2");
			return EdmFunctions.InvokeCanonicalFunction("BitwiseXor", new DbExpression[]
			{
				value1,
				value2
			});
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000D62CB File Offset: 0x000D44CB
		public static DbFunctionExpression NewGuid()
		{
			return EdmFunctions.InvokeCanonicalFunction("NewGuid", new DbExpression[0]);
		}
	}
}
