using System;

namespace AutoMapper
{
	// Token: 0x02000012 RID: 18
	public interface IMappingAction<TSource, TDestination>
	{
		// Token: 0x0600006B RID: 107
		void Process(TSource source, TDestination destination);
	}
}
