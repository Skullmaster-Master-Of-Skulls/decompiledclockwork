using System;
using System.Collections.Generic;

namespace AutoMapper.Mappers
{
	// Token: 0x02000073 RID: 115
	public interface IConditionalObjectMapper
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003D9 RID: 985
		string ProfileName { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003DA RID: 986
		ICollection<Func<TypePair, bool>> Conventions { get; }

		// Token: 0x060003DB RID: 987
		bool IsMatch(TypePair context);
	}
}
