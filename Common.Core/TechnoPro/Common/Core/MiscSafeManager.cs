using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO;
using TechnoPro.Common.DAO.Impl;
using TechnoPro.Common.ICore;

namespace TechnoPro.Common.Core
{
	// Token: 0x02000020 RID: 32
	public class MiscSafeManager : IMiscSafeManager
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006627 File Offset: 0x00004827
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000662F File Offset: 0x0000482F
		public IMiscSafeDAO MiscSafeDAO { get; set; }

		// Token: 0x06000108 RID: 264 RVA: 0x00006638 File Offset: 0x00004838
		public MiscSafeManager()
		{
			this.MiscSafeDAO = new MiscSafeDAO();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000664E File Offset: 0x0000484E
		public void Save(string key, string value)
		{
			this.MiscSafeDAO.Save(key, value);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006660 File Offset: 0x00004860
		public string GetValue(string key)
		{
			return this.MiscSafeDAO.GetValue(key);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006680 File Offset: 0x00004880
		public IList<string> GetKeys(string value)
		{
			return this.MiscSafeDAO.GetKeys(value);
		}
	}
}
