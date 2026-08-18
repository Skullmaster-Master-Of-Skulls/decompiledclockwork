using System;
using TechnoPro.Common.DAO.Impl.Settings;
using TechnoPro.Common.DAO.Settings;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Login;

namespace TechnoPro.Common.Core.Settings
{
	// Token: 0x02000044 RID: 68
	public class MiscTableSettingsManager : IMiscTableSettingsManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00010468 File Offset: 0x0000E668
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00010470 File Offset: 0x0000E670
		public IMiscTableSettingsDAO dao { get; set; }

		// Token: 0x060002BB RID: 699 RVA: 0x00010479 File Offset: 0x0000E679
		public MiscTableSettingsManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new MiscTableSettingsDAO(opContext);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00010498 File Offset: 0x0000E698
		// (set) Token: 0x060002BD RID: 701 RVA: 0x000104A0 File Offset: 0x0000E6A0
		public OperationContext OpContext { get; set; }

		// Token: 0x060002BE RID: 702 RVA: 0x000104AC File Offset: 0x0000E6AC
		public string LoadMiscSettingValue(int code)
		{
			return this.dao.LoadMiscSettingValue(code);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000104CA File Offset: 0x0000E6CA
		public void SaveMiscSettingValue(int code, string value)
		{
			this.dao.SaveMiscSettingValue(code, value);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000104DC File Offset: 0x0000E6DC
		public eLoginMethod GetLoginMethod()
		{
			string val = this.LoadMiscSettingValue(101);
			return val.ELoginMethodFromString();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00010500 File Offset: 0x0000E700
		public void SetLoginMethod(eLoginMethod loginMethod)
		{
			string value = loginMethod.ELoginMethodToString();
			this.SaveMiscSettingValue(101, value);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00010520 File Offset: 0x0000E720
		public LdapConnectionInfo LoadLdapConnectionInfo()
		{
			string infoStr = this.LoadMiscSettingValue(1101);
			return infoStr.LdapConnectionInfoFromString();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010544 File Offset: 0x0000E744
		public void SaveLdapConnectionInfo(LdapConnectionInfo info)
		{
			string value = info.LdapConnectionInfoToString();
			this.SaveMiscSettingValue(1101, value);
		}
	}
}
