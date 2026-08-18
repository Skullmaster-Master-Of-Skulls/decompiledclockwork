using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper.Mappers;

namespace AutoMapper
{
	// Token: 0x02000022 RID: 34
	public interface IProfileConfiguration
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000ED RID: 237
		IEnumerable<IMemberConfiguration> MemberConfigurations { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000EE RID: 238
		IEnumerable<IConditionalObjectMapper> TypeConfigurations { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000EF RID: 239
		bool ConstructorMappingEnabled { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F0 RID: 240
		bool AllowNullDestinationValues { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F1 RID: 241
		bool AllowNullCollections { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F2 RID: 242
		INamingConvention SourceMemberNamingConvention { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F3 RID: 243
		INamingConvention DestinationMemberNamingConvention { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F4 RID: 244
		bool CreateMissingTypeMaps { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F5 RID: 245
		IMemberConfiguration DefaultMemberConfig { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F6 RID: 246
		IEnumerable<MethodInfo> SourceExtensionMethods { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F7 RID: 247
		Func<PropertyInfo, bool> ShouldMapProperty { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F8 RID: 248
		Func<FieldInfo, bool> ShouldMapField { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F9 RID: 249
		string ProfileName { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000FA RID: 250
		IEnumerable<string> GlobalIgnores { get; }
	}
}
