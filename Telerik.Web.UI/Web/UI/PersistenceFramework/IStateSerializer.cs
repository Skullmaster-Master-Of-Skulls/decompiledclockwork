using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000497 RID: 1175
	public interface IStateSerializer
	{
		// Token: 0x060029D0 RID: 10704
		string Serialize(RadControlState state);

		// Token: 0x060029D1 RID: 10705
		string Serialize(List<RadControlState> state);

		// Token: 0x060029D2 RID: 10706
		RadControlState Deserialize(string stateData);

		// Token: 0x060029D3 RID: 10707
		List<RadControlState> DeserializeCollection(string stateData);
	}
}
