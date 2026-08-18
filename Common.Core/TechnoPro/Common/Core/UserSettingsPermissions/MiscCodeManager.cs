using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Impl.UserSettingsPermissions;
using TechnoPro.Common.DAO.UserSettingsPermissions;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.Core.UserSettingsPermissions
{
	// Token: 0x0200002A RID: 42
	public class MiscCodeManager : IMiscCodeManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000755B File Offset: 0x0000575B
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00007563 File Offset: 0x00005763
		public IMiscCodeDAO dao { get; set; }

		// Token: 0x06000169 RID: 361 RVA: 0x0000756C File Offset: 0x0000576C
		public MiscCodeManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new MiscCodeDAO(opContext);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000758B File Offset: 0x0000578B
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00007593 File Offset: 0x00005793
		public OperationContext OpContext { get; set; }

		// Token: 0x0600016C RID: 364 RVA: 0x0000759C File Offset: 0x0000579C
		public string LoadMiscCodeValue(eMiscCode miscCode)
		{
			return this.dao.LoadMiscCodeValue(miscCode);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000075BC File Offset: 0x000057BC
		[DebuggerStepThrough]
		public Task<string> LoadMiscCodeValueAsync(eMiscCode miscCode)
		{
			MiscCodeManager.<LoadMiscCodeValueAsync>d__10 <LoadMiscCodeValueAsync>d__ = new MiscCodeManager.<LoadMiscCodeValueAsync>d__10();
			<LoadMiscCodeValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<LoadMiscCodeValueAsync>d__.<>4__this = this;
			<LoadMiscCodeValueAsync>d__.miscCode = miscCode;
			<LoadMiscCodeValueAsync>d__.<>1__state = -1;
			<LoadMiscCodeValueAsync>d__.<>t__builder.Start<MiscCodeManager.<LoadMiscCodeValueAsync>d__10>(ref <LoadMiscCodeValueAsync>d__);
			return <LoadMiscCodeValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007607 File Offset: 0x00005807
		public void SaveMiscCodeValue(eMiscCode miscCode, string newValue)
		{
			this.dao.SaveMiscCodeValue(miscCode, newValue);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007618 File Offset: 0x00005818
		[DebuggerStepThrough]
		public Task SaveMiscCodeValueAsync(eMiscCode miscCode, string newValue)
		{
			MiscCodeManager.<SaveMiscCodeValueAsync>d__12 <SaveMiscCodeValueAsync>d__ = new MiscCodeManager.<SaveMiscCodeValueAsync>d__12();
			<SaveMiscCodeValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveMiscCodeValueAsync>d__.<>4__this = this;
			<SaveMiscCodeValueAsync>d__.miscCode = miscCode;
			<SaveMiscCodeValueAsync>d__.newValue = newValue;
			<SaveMiscCodeValueAsync>d__.<>1__state = -1;
			<SaveMiscCodeValueAsync>d__.<>t__builder.Start<MiscCodeManager.<SaveMiscCodeValueAsync>d__12>(ref <SaveMiscCodeValueAsync>d__);
			return <SaveMiscCodeValueAsync>d__.<>t__builder.Task;
		}
	}
}
