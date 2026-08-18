using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper
{
	// Token: 0x0200001A RID: 26
	public interface IMappingOptions
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C2 RID: 194
		Func<PropertyInfo, bool> ShouldMapProperty { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C3 RID: 195
		Func<FieldInfo, bool> ShouldMapField { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C4 RID: 196
		INamingConvention SourceMemberNamingConvention { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C5 RID: 197
		INamingConvention DestinationMemberNamingConvention { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C6 RID: 198
		IEnumerable<string> Prefixes { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C7 RID: 199
		IEnumerable<string> Postfixes { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C8 RID: 200
		IEnumerable<string> DestinationPrefixes { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C9 RID: 201
		IEnumerable<string> DestinationPostfixes { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CA RID: 202
		IEnumerable<MemberNameReplacer> MemberNameReplacers { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CB RID: 203
		IEnumerable<AliasedMember> Aliases { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CC RID: 204
		bool ConstructorMappingEnabled { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CD RID: 205
		IEnumerable<MethodInfo> SourceExtensionMethods { get; }
	}
}
