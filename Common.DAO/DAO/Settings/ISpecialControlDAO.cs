using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.DAO.Settings
{
	// Token: 0x02000030 RID: 48
	public interface ISpecialControlDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000C9 RID: 201
		IDictionary<eSpecialControlType, int> GetDefinedSpecialControlIds(IList<eSpecialControlType> RestrictSearchToTheseSpecialControlTypes = null);

		// Token: 0x060000CA RID: 202
		int GetSpecialControlId(eSpecialControlType SpecialControlType);

		// Token: 0x060000CB RID: 203
		DateTime? GetSpecialControlValueDateTime(int PersonId, eSpecialControlType SpecialControlType);

		// Token: 0x060000CC RID: 204
		bool? GetSpecialControlValueBool(int PersonId, eSpecialControlType SpecialControlType);

		// Token: 0x060000CD RID: 205
		int? GetSpecialControlValueInt(int PersonId, eSpecialControlType SpecialControlType);

		// Token: 0x060000CE RID: 206
		string GetSpecialControlValueString(int PersonId, eSpecialControlType SpecialControlType);
	}
}
