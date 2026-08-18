using System;
using System.Reflection;
using AutoMapper.Mappers;

namespace AutoMapper
{
	// Token: 0x02000023 RID: 35
	public interface IProfileExpression
	{
		// Token: 0x060000FB RID: 251
		void DisableConstructorMapping();

		// Token: 0x060000FC RID: 252
		IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>();

		// Token: 0x060000FD RID: 253
		IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>(MemberList memberList);

		// Token: 0x060000FE RID: 254
		IMappingExpression CreateMap(Type sourceType, Type destinationType);

		// Token: 0x060000FF RID: 255
		IMappingExpression CreateMap(Type sourceType, Type destinationType, MemberList memberList);

		// Token: 0x06000100 RID: 256
		void ClearPrefixes();

		// Token: 0x06000101 RID: 257
		void RecognizePrefixes(params string[] prefixes);

		// Token: 0x06000102 RID: 258
		void RecognizePostfixes(params string[] postfixes);

		// Token: 0x06000103 RID: 259
		void RecognizeAlias(string original, string alias);

		// Token: 0x06000104 RID: 260
		void ReplaceMemberName(string original, string newValue);

		// Token: 0x06000105 RID: 261
		void RecognizeDestinationPrefixes(params string[] prefixes);

		// Token: 0x06000106 RID: 262
		void RecognizeDestinationPostfixes(params string[] postfixes);

		// Token: 0x06000107 RID: 263
		void AddGlobalIgnore(string propertyNameStartingWith);

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000108 RID: 264
		// (set) Token: 0x06000109 RID: 265
		bool AllowNullDestinationValues { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010A RID: 266
		// (set) Token: 0x0600010B RID: 267
		bool AllowNullCollections { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010C RID: 268
		// (set) Token: 0x0600010D RID: 269
		INamingConvention SourceMemberNamingConvention { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010E RID: 270
		// (set) Token: 0x0600010F RID: 271
		INamingConvention DestinationMemberNamingConvention { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000110 RID: 272
		// (set) Token: 0x06000111 RID: 273
		bool CreateMissingTypeMaps { get; set; }

		// Token: 0x06000112 RID: 274
		void ForAllMaps(Action<TypeMap, IMappingExpression> configuration);

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000113 RID: 275
		// (set) Token: 0x06000114 RID: 276
		Func<PropertyInfo, bool> ShouldMapProperty { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000115 RID: 277
		// (set) Token: 0x06000116 RID: 278
		Func<FieldInfo, bool> ShouldMapField { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000117 RID: 279
		string ProfileName { get; }

		// Token: 0x06000118 RID: 280
		IMemberConfiguration AddMemberConfiguration();

		// Token: 0x06000119 RID: 281
		IConditionalObjectMapper AddConditionalObjectMapper();

		// Token: 0x0600011A RID: 282
		void IncludeSourceExtensionMethods(Assembly assembly);
	}
}
