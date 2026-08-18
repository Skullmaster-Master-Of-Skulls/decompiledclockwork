using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.ICore.Settings
{
	// Token: 0x02000036 RID: 54
	public interface ISpecialControlManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000162 RID: 354
		int GetSpecialControlId(eSpecialControlType SpecialControlType);

		// Token: 0x06000163 RID: 355
		T? GetSpecialControlValue<T>(int PersonId, eSpecialControlType SpecialControlType) where T : struct;
	}
}
