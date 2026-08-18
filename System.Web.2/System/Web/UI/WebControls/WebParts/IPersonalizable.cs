using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000541 RID: 1345
	public interface IPersonalizable
	{
		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x060044C3 RID: 17603
		bool IsDirty { get; }

		// Token: 0x060044C4 RID: 17604
		void Load(PersonalizationDictionary state);

		// Token: 0x060044C5 RID: 17605
		void Save(PersonalizationDictionary state);
	}
}
