using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoMapper.Mappers
{
	// Token: 0x02000074 RID: 116
	public class ConditionalObjectMapper : IConditionalObjectMapper
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00010351 File Offset: 0x0000E551
		public string ProfileName { get; }

		// Token: 0x060003DD RID: 989 RVA: 0x00010359 File Offset: 0x0000E559
		public ConditionalObjectMapper(string profileName)
		{
			this.ProfileName = profileName;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00010374 File Offset: 0x0000E574
		public bool IsMatch(TypePair typePair)
		{
			return this.Conventions.All((Func<TypePair, bool> c) => c(typePair));
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003DF RID: 991 RVA: 0x000103A5 File Offset: 0x0000E5A5
		public ICollection<Func<TypePair, bool>> Conventions { get; } = new Collection<Func<TypePair, bool>>();
	}
}
