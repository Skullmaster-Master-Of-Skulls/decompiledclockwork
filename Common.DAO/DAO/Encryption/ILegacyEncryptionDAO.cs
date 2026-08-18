using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.Common.DAO.Encryption
{
	// Token: 0x02000076 RID: 118
	public interface ILegacyEncryptionDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002EC RID: 748
		IList<LegacyDynamicDataItemItemsThatHaveBeenDecrypted> DecryptLegacyDataItemsNeedingDecryption(IList<LegacyDynamicDataItemItemsToBeDecrypted> itemsToBeDecrypted);
	}
}
