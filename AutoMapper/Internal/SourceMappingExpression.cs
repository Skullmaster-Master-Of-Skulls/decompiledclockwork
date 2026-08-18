using System;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000AC RID: 172
	internal class SourceMappingExpression : ISourceMemberConfigurationExpression
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x00012FDE File Offset: 0x000111DE
		public SourceMappingExpression(TypeMap typeMap, MemberInfo sourceMember)
		{
			this._sourcePropertyConfig = typeMap.FindOrCreateSourceMemberConfigFor(sourceMember);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00012FF3 File Offset: 0x000111F3
		public void Ignore()
		{
			this._sourcePropertyConfig.Ignore();
		}

		// Token: 0x040000E1 RID: 225
		private readonly SourceMemberConfig _sourcePropertyConfig;
	}
}
