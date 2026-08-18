using System;

namespace TechnoPro.Common.DAO.Misc
{
	// Token: 0x0200004A RID: 74
	public interface IMiscDAO
	{
		// Token: 0x060001A4 RID: 420
		void Save(int key, string value);

		// Token: 0x060001A5 RID: 421
		string GetValue(int key);
	}
}
