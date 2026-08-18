using System;

namespace AutoMapper.Mappers
{
	// Token: 0x02000075 RID: 117
	public static class ConventionGeneratorExtensions
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x000103B0 File Offset: 0x0000E5B0
		public static IConditionalObjectMapper Where(this IConditionalObjectMapper self, Func<Type, Type, bool> condition)
		{
			self.Conventions.Add((TypePair rc) => condition(rc.SourceType, rc.DestinationType));
			return self;
		}
	}
}
