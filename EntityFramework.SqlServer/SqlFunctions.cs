using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200001B RID: 27
	public static class SqlFunctions
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00005A3C File Offset: 0x00003C3C
		[DbFunction("SqlServer", "CHECKSUM_AGG")]
		public static int? ChecksumAggregate(IEnumerable<int> arg)
		{
			return SqlFunctions.BootstrapFunction<int, int?>((IEnumerable<int> a) => SqlFunctions.ChecksumAggregate(a), arg);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005A98 File Offset: 0x00003C98
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[DbFunction("SqlServer", "CHECKSUM_AGG")]
		public static int? ChecksumAggregate(IEnumerable<int?> arg)
		{
			return SqlFunctions.BootstrapFunction<int?, int?>((IEnumerable<int?> a) => SqlFunctions.ChecksumAggregate(a), arg);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005AF4 File Offset: 0x00003CF4
		private static TOut BootstrapFunction<TIn, TOut>(Expression<Func<IEnumerable<TIn>, TOut>> methodExpression, IEnumerable<TIn> arg)
		{
			IQueryable queryable = arg as IQueryable;
			if (queryable != null)
			{
				return queryable.Provider.Execute<TOut>(Expression.Call(((MethodCallExpression)methodExpression.Body).Method, Expression.Constant(arg)));
			}
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005B3C File Offset: 0x00003D3C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "ASCII")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ascii")]
		public static int? Ascii(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005B48 File Offset: 0x00003D48
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "CHAR")]
		public static string Char(int? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005B54 File Offset: 0x00003D54
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "target")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "toSearch")]
		[DbFunction("SqlServer", "CHARINDEX")]
		public static int? CharIndex(string toSearch, string target)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005B60 File Offset: 0x00003D60
		[DbFunction("SqlServer", "CHARINDEX")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "toSearch")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "target")]
		public static int? CharIndex(byte[] toSearch, byte[] target)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005B6C File Offset: 0x00003D6C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "target")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "toSearch")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startLocation")]
		[DbFunction("SqlServer", "CHARINDEX")]
		public static int? CharIndex(string toSearch, string target, int? startLocation)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005B78 File Offset: 0x00003D78
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "target")]
		[DbFunction("SqlServer", "CHARINDEX")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "toSearch")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startLocation")]
		public static int? CharIndex(byte[] toSearch, byte[] target, int? startLocation)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005B84 File Offset: 0x00003D84
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startLocation")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "toSearch")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "target")]
		[DbFunction("SqlServer", "CHARINDEX")]
		public static long? CharIndex(string toSearch, string target, long? startLocation)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005B90 File Offset: 0x00003D90
		[DbFunction("SqlServer", "CHARINDEX")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "target")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startLocation")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "toSearch")]
		public static long? CharIndex(byte[] toSearch, byte[] target, long? startLocation)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005B9C File Offset: 0x00003D9C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "string2")]
		[DbFunction("SqlServer", "DIFFERENCE")]
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "string1")]
		public static int? Difference(string string1, string string2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005BA8 File Offset: 0x00003DA8
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "NCHAR")]
		public static string NChar(int? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005BB4 File Offset: 0x00003DB4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "stringPattern")]
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "target")]
		[DbFunction("SqlServer", "PATINDEX")]
		public static int? PatIndex(string stringPattern, string target)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005BC0 File Offset: 0x00003DC0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "stringArg")]
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		[DbFunction("SqlServer", "QUOTENAME")]
		public static string QuoteName(string stringArg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005BCC File Offset: 0x00003DCC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "quoteCharacter")]
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "stringArg")]
		[DbFunction("SqlServer", "QUOTENAME")]
		public static string QuoteName(string stringArg, string quoteCharacter)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005BD8 File Offset: 0x00003DD8
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "count")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "target")]
		[DbFunction("SqlServer", "REPLICATE")]
		public static string Replicate(string target, int? count)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005BE4 File Offset: 0x00003DE4
		[DbFunction("SqlServer", "SOUNDEX")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static string SoundCode(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005BF0 File Offset: 0x00003DF0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[DbFunction("SqlServer", "SPACE")]
		public static string Space(int? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005BFC File Offset: 0x00003DFC
		[DbFunction("SqlServer", "STR")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "number")]
		public static string StringConvert(double? number)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005C08 File Offset: 0x00003E08
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "number")]
		[DbFunction("SqlServer", "STR")]
		public static string StringConvert(decimal? number)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005C14 File Offset: 0x00003E14
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "number")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "length")]
		[DbFunction("SqlServer", "STR")]
		public static string StringConvert(double? number, int? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005C20 File Offset: 0x00003E20
		[DbFunction("SqlServer", "STR")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "number")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "length")]
		public static string StringConvert(decimal? number, int? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005C2C File Offset: 0x00003E2C
		[DbFunction("SqlServer", "STR")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "number")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "length")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "decimalArg")]
		public static string StringConvert(double? number, int? length, int? decimalArg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005C38 File Offset: 0x00003E38
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "number")]
		[DbFunction("SqlServer", "STR")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "decimalArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "length")]
		public static string StringConvert(decimal? number, int? length, int? decimalArg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005C44 File Offset: 0x00003E44
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "start")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "stringInput")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "stringReplacement")]
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "length")]
		[DbFunction("SqlServer", "STUFF")]
		public static string Stuff(string stringInput, int? start, int? length, string stringReplacement)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005C50 File Offset: 0x00003E50
		[DbFunction("SqlServer", "UNICODE")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static int? Unicode(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005C5C File Offset: 0x00003E5C
		[DbFunction("SqlServer", "ACOS")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static double? Acos(double? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005C68 File Offset: 0x00003E68
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[DbFunction("SqlServer", "ACOS")]
		public static double? Acos(decimal? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005C74 File Offset: 0x00003E74
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "ASIN")]
		public static double? Asin(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005C80 File Offset: 0x00003E80
		[DbFunction("SqlServer", "ASIN")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static double? Asin(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005C8C File Offset: 0x00003E8C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "ATAN")]
		public static double? Atan(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005C98 File Offset: 0x00003E98
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "ATAN")]
		public static double? Atan(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005CA4 File Offset: 0x00003EA4
		[DbFunction("SqlServer", "ATN2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		public static double? Atan2(double? arg1, double? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005CB0 File Offset: 0x00003EB0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "ATN2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static double? Atan2(decimal? arg1, decimal? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005CBC File Offset: 0x00003EBC
		[DbFunction("SqlServer", "COS")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static double? Cos(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005CC8 File Offset: 0x00003EC8
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "COS")]
		public static double? Cos(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005CD4 File Offset: 0x00003ED4
		[DbFunction("SqlServer", "COT")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static double? Cot(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005CE0 File Offset: 0x00003EE0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "COT")]
		public static double? Cot(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005CEC File Offset: 0x00003EEC
		[DbFunction("SqlServer", "DEGREES")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Degrees(int? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005CF8 File Offset: 0x00003EF8
		[DbFunction("SqlServer", "DEGREES")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static long? Degrees(long? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005D04 File Offset: 0x00003F04
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[DbFunction("SqlServer", "DEGREES")]
		public static decimal? Degrees(decimal? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005D10 File Offset: 0x00003F10
		[DbFunction("SqlServer", "DEGREES")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static double? Degrees(double? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005D1C File Offset: 0x00003F1C
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Exp")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "EXP")]
		public static double? Exp(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005D28 File Offset: 0x00003F28
		[DbFunction("SqlServer", "EXP")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Exp")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static double? Exp(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005D34 File Offset: 0x00003F34
		[DbFunction("SqlServer", "LOG")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static double? Log(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005D40 File Offset: 0x00003F40
		[DbFunction("SqlServer", "LOG")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static double? Log(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005D4C File Offset: 0x00003F4C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "LOG10")]
		public static double? Log10(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005D58 File Offset: 0x00003F58
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "LOG10")]
		public static double? Log10(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005D64 File Offset: 0x00003F64
		[DbFunction("SqlServer", "PI")]
		public static double? Pi()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005D70 File Offset: 0x00003F70
		[DbFunction("SqlServer", "RADIANS")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static int? Radians(int? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005D7C File Offset: 0x00003F7C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "RADIANS")]
		public static long? Radians(long? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005D88 File Offset: 0x00003F88
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "RADIANS")]
		public static decimal? Radians(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005D94 File Offset: 0x00003F94
		[DbFunction("SqlServer", "RADIANS")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static double? Radians(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005DA0 File Offset: 0x00003FA0
		[DbFunction("SqlServer", "RAND")]
		public static double? Rand()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005DAC File Offset: 0x00003FAC
		[DbFunction("SqlServer", "RAND")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "seed")]
		public static double? Rand(int? seed)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005DB8 File Offset: 0x00003FB8
		[DbFunction("SqlServer", "SIGN")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static int? Sign(int? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005DC4 File Offset: 0x00003FC4
		[DbFunction("SqlServer", "SIGN")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static long? Sign(long? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005DD0 File Offset: 0x00003FD0
		[DbFunction("SqlServer", "SIGN")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static decimal? Sign(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005DDC File Offset: 0x00003FDC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "SIGN")]
		public static double? Sign(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005DE8 File Offset: 0x00003FE8
		[DbFunction("SqlServer", "SIN")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static double? Sin(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005DF4 File Offset: 0x00003FF4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "SIN")]
		public static double? Sin(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005E00 File Offset: 0x00004000
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "SQRT")]
		public static double? SquareRoot(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005E0C File Offset: 0x0000400C
		[DbFunction("SqlServer", "SQRT")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static double? SquareRoot(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005E18 File Offset: 0x00004018
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[DbFunction("SqlServer", "SQUARE")]
		public static double? Square(double? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005E24 File Offset: 0x00004024
		[DbFunction("SqlServer", "SQUARE")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static double? Square(decimal? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005E30 File Offset: 0x00004030
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "TAN")]
		public static double? Tan(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005E3C File Offset: 0x0000403C
		[DbFunction("SqlServer", "TAN")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static double? Tan(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005E48 File Offset: 0x00004048
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "number")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "date")]
		[DbFunction("SqlServer", "DATEADD")]
		public static DateTime? DateAdd(string datePartArg, double? number, DateTime? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005E54 File Offset: 0x00004054
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "number")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "time")]
		[DbFunction("SqlServer", "DATEADD")]
		public static TimeSpan? DateAdd(string datePartArg, double? number, TimeSpan? time)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005E60 File Offset: 0x00004060
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "number")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dateTimeOffsetArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[DbFunction("SqlServer", "DATEADD")]
		public static DateTimeOffset? DateAdd(string datePartArg, double? number, DateTimeOffset? dateTimeOffsetArg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005E6C File Offset: 0x0000406C
		[DbFunction("SqlServer", "DATEADD")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "date")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "number")]
		public static DateTime? DateAdd(string datePartArg, double? number, string date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005E78 File Offset: 0x00004078
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, DateTime? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005E84 File Offset: 0x00004084
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005E90 File Offset: 0x00004090
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, TimeSpan? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005E9C File Offset: 0x0000409C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		public static int? DateDiff(string datePartArg, string startDate, DateTime? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005EA8 File Offset: 0x000040A8
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		public static int? DateDiff(string datePartArg, string startDate, DateTimeOffset? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005EB4 File Offset: 0x000040B4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		public static int? DateDiff(string datePartArg, string startDate, TimeSpan? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005EC0 File Offset: 0x000040C0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, string endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005ECC File Offset: 0x000040CC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, string endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005ED8 File Offset: 0x000040D8
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, string endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005EE4 File Offset: 0x000040E4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		public static int? DateDiff(string datePartArg, string startDate, string endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005EF0 File Offset: 0x000040F0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, DateTime? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005EFC File Offset: 0x000040FC
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, DateTimeOffset? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005F08 File Offset: 0x00004108
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, TimeSpan? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005F14 File Offset: 0x00004114
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, TimeSpan? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005F20 File Offset: 0x00004120
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[DbFunction("SqlServer", "DATEDIFF")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, DateTimeOffset? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005F2C File Offset: 0x0000412C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "endDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "startDate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, DateTime? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005F38 File Offset: 0x00004138
		[DbFunction("SqlServer", "DATENAME")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "date")]
		public static string DateName(string datePartArg, DateTime? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005F44 File Offset: 0x00004144
		[DbFunction("SqlServer", "DATENAME")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "date")]
		public static string DateName(string datePartArg, string date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005F50 File Offset: 0x00004150
		[DbFunction("SqlServer", "DATENAME")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "date")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		public static string DateName(string datePartArg, TimeSpan? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005F5C File Offset: 0x0000415C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[DbFunction("SqlServer", "DATENAME")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "date")]
		public static string DateName(string datePartArg, DateTimeOffset? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005F68 File Offset: 0x00004168
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "date")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[DbFunction("SqlServer", "DATEPART")]
		public static int? DatePart(string datePartArg, DateTime? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005F74 File Offset: 0x00004174
		[DbFunction("SqlServer", "DATEPART")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "date")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		public static int? DatePart(string datePartArg, DateTimeOffset? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005F80 File Offset: 0x00004180
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "date")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		[DbFunction("SqlServer", "DATEPART")]
		public static int? DatePart(string datePartArg, string date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005F8C File Offset: 0x0000418C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "date")]
		[DbFunction("SqlServer", "DATEPART")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "datePartArg")]
		public static int? DatePart(string datePartArg, TimeSpan? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005F98 File Offset: 0x00004198
		[DbFunction("SqlServer", "GETDATE")]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public static DateTime? GetDate()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005FA4 File Offset: 0x000041A4
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[DbFunction("SqlServer", "GETUTCDATE")]
		public static DateTime? GetUtcDate()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005FB0 File Offset: 0x000041B0
		[DbFunction("SqlServer", "DATALENGTH")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static int? DataLength(bool? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005FBC File Offset: 0x000041BC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005FC8 File Offset: 0x000041C8
		[DbFunction("SqlServer", "DATALENGTH")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static int? DataLength(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005FD4 File Offset: 0x000041D4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(DateTime? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005FE0 File Offset: 0x000041E0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(TimeSpan? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005FEC File Offset: 0x000041EC
		[DbFunction("SqlServer", "DATALENGTH")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static int? DataLength(DateTimeOffset? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005FF8 File Offset: 0x000041F8
		[DbFunction("SqlServer", "DATALENGTH")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static int? DataLength(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006004 File Offset: 0x00004204
		[DbFunction("SqlServer", "DATALENGTH")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static int? DataLength(byte[] arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006010 File Offset: 0x00004210
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(Guid? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000601C File Offset: 0x0000421C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(bool? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006028 File Offset: 0x00004228
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(double? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006034 File Offset: 0x00004234
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(decimal? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006040 File Offset: 0x00004240
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(string arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000604C File Offset: 0x0000424C
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(DateTime? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006058 File Offset: 0x00004258
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(TimeSpan? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006064 File Offset: 0x00004264
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(DateTimeOffset? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006070 File Offset: 0x00004270
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(byte[] arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000607C File Offset: 0x0000427C
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(Guid? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006088 File Offset: 0x00004288
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(bool? arg1, bool? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006094 File Offset: 0x00004294
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(double? arg1, double? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000060A0 File Offset: 0x000042A0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(decimal? arg1, decimal? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000060AC File Offset: 0x000042AC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(string arg1, string arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000060B8 File Offset: 0x000042B8
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTime? arg1, DateTime? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000060C4 File Offset: 0x000042C4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(TimeSpan? arg1, TimeSpan? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000060D0 File Offset: 0x000042D0
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(DateTimeOffset? arg1, DateTimeOffset? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000060DC File Offset: 0x000042DC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(byte[] arg1, byte[] arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000060E8 File Offset: 0x000042E8
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(Guid? arg1, Guid? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000060F4 File Offset: 0x000042F4
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg3")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		public static int? Checksum(bool? arg1, bool? arg2, bool? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006100 File Offset: 0x00004300
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg3")]
		public static int? Checksum(double? arg1, double? arg2, double? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000610C File Offset: 0x0000430C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg3")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(decimal? arg1, decimal? arg2, decimal? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006118 File Offset: 0x00004318
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg3")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		public static int? Checksum(string arg1, string arg2, string arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006124 File Offset: 0x00004324
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg3")]
		public static int? Checksum(DateTime? arg1, DateTime? arg2, DateTime? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006130 File Offset: 0x00004330
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[DbFunction("SqlServer", "CHECKSUM")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg3")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		public static int? Checksum(DateTimeOffset? arg1, DateTimeOffset? arg2, DateTimeOffset? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000613C File Offset: 0x0000433C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg3")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(TimeSpan? arg1, TimeSpan? arg2, TimeSpan? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006148 File Offset: 0x00004348
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg3")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(byte[] arg1, byte[] arg2, byte[] arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006154 File Offset: 0x00004354
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg1")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg2")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg3")]
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(Guid? arg1, Guid? arg2, Guid? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006160 File Offset: 0x00004360
		[DbFunction("SqlServer", "CURRENT_TIMESTAMP")]
		public static DateTime? CurrentTimestamp()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000616C File Offset: 0x0000436C
		[DbFunction("SqlServer", "CURRENT_USER")]
		public static string CurrentUser()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006178 File Offset: 0x00004378
		[DbFunction("SqlServer", "HOST_NAME")]
		public static string HostName()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006184 File Offset: 0x00004384
		[DbFunction("SqlServer", "USER_NAME")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static string UserName(int? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006190 File Offset: 0x00004390
		[DbFunction("SqlServer", "USER_NAME")]
		public static string UserName()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000619C File Offset: 0x0000439C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		[DbFunction("SqlServer", "ISNUMERIC")]
		public static int? IsNumeric(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000061A8 File Offset: 0x000043A8
		[DbFunction("SqlServer", "ISDATE")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "arg")]
		public static int? IsDate(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}
	}
}
