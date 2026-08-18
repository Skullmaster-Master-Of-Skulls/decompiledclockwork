using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.AlternativeFormat.Adapters
{
	// Token: 0x02000164 RID: 356
	public static class MediaContentRequestedExtensions
	{
		// Token: 0x0600100E RID: 4110 RVA: 0x0007571C File Offset: 0x0007391C
		[DebuggerStepThrough]
		public static Task NotifyStudentsAsync(this IEnumerable<MediaContentRequestedInfo> studentRequestList, Setting emailSetting, OperationContext opContext)
		{
			MediaContentRequestedExtensions.<NotifyStudentsAsync>d__0 <NotifyStudentsAsync>d__ = new MediaContentRequestedExtensions.<NotifyStudentsAsync>d__0();
			<NotifyStudentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyStudentsAsync>d__.studentRequestList = studentRequestList;
			<NotifyStudentsAsync>d__.emailSetting = emailSetting;
			<NotifyStudentsAsync>d__.opContext = opContext;
			<NotifyStudentsAsync>d__.<>1__state = -1;
			<NotifyStudentsAsync>d__.<>t__builder.Start<MediaContentRequestedExtensions.<NotifyStudentsAsync>d__0>(ref <NotifyStudentsAsync>d__);
			return <NotifyStudentsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00075770 File Offset: 0x00073970
		[DebuggerStepThrough]
		public static Task NotifyStudentsAsync(this MediaContentRequestedInfo contentRequestedInfo, Setting emailSetting, OperationContext opContext)
		{
			MediaContentRequestedExtensions.<NotifyStudentsAsync>d__1 <NotifyStudentsAsync>d__ = new MediaContentRequestedExtensions.<NotifyStudentsAsync>d__1();
			<NotifyStudentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyStudentsAsync>d__.contentRequestedInfo = contentRequestedInfo;
			<NotifyStudentsAsync>d__.emailSetting = emailSetting;
			<NotifyStudentsAsync>d__.opContext = opContext;
			<NotifyStudentsAsync>d__.<>1__state = -1;
			<NotifyStudentsAsync>d__.<>t__builder.Start<MediaContentRequestedExtensions.<NotifyStudentsAsync>d__1>(ref <NotifyStudentsAsync>d__);
			return <NotifyStudentsAsync>d__.<>t__builder.Task;
		}
	}
}
