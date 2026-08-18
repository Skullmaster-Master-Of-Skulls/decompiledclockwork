using System;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x0200023F RID: 575
	[Serializable]
	public class DataSyncExecuteRestWebServiceParameters
	{
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00018335 File Offset: 0x00016535
		// (set) Token: 0x06001181 RID: 4481 RVA: 0x0001833D File Offset: 0x0001653D
		public bool ReturnXml { get; set; }

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00018346 File Offset: 0x00016546
		// (set) Token: 0x06001183 RID: 4483 RVA: 0x0001834E File Offset: 0x0001654E
		public string Username { get; set; }

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00018357 File Offset: 0x00016557
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x0001835F File Offset: 0x0001655F
		public string Password { get; set; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00018368 File Offset: 0x00016568
		// (set) Token: 0x06001187 RID: 4487 RVA: 0x00018370 File Offset: 0x00016570
		public Setting? PasswordEncryptedSettingCode { get; set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00018379 File Offset: 0x00016579
		// (set) Token: 0x06001189 RID: 4489 RVA: 0x00018381 File Offset: 0x00016581
		public string Domain { get; set; }

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0001838A File Offset: 0x0001658A
		// (set) Token: 0x0600118B RID: 4491 RVA: 0x00018392 File Offset: 0x00016592
		public string RootNodeName { get; set; }

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0001839B File Offset: 0x0001659B
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x000183A3 File Offset: 0x000165A3
		public string Url { get; set; }

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x000183AC File Offset: 0x000165AC
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x000183B4 File Offset: 0x000165B4
		public string HashType { get; set; }

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x000183BD File Offset: 0x000165BD
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x000183C5 File Offset: 0x000165C5
		public string HashSecret { get; set; }

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x000183CE File Offset: 0x000165CE
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x000183D6 File Offset: 0x000165D6
		public string[] HashParameterNamesInOrder { get; set; }

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x000183DF File Offset: 0x000165DF
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x000183E7 File Offset: 0x000165E7
		public Setting? HashSecretEncryptedSettingCode { get; set; }
	}
}
