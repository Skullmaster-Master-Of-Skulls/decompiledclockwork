using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers
{
	// Token: 0x02000009 RID: 9
	public class LoginSettings : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002050 File Offset: 0x00000250
		public LoginSettings()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000390A File Offset: 0x00001B0A
		public LoginSettings(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000391C File Offset: 0x00001B1C
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00003924 File Offset: 0x00001B24
		public OperationContext OpContext { get; set; }

		// Token: 0x06000030 RID: 48 RVA: 0x00003930 File Offset: 0x00001B30
		public AuditResult ExecuteAudit()
		{
			string settingValue = SettingManager.CurrentInstance.GetSettingValue<string>(Setting.LOGIN_AuthenticationContext);
			AuthenticationContext authenticationContext = string.IsNullOrEmpty(settingValue) ? null : settingValue.GetAuthenticationContextFromXml();
			string settingValue2 = SettingManager.CurrentInstance.GetSettingValue<string>(Setting.LOGIN_AuthorizationContext);
			AuthorizationContext authorizationContext = string.IsNullOrEmpty(settingValue2) ? null : settingValue2.GetAuthorizationContextFromXml();
			AuditResult auditResult = new AuditResult();
			AuditResult auditResult2 = auditResult;
			List<AuditCheck> list = new List<AuditCheck>();
			List<AuditCheck> list2 = list;
			string title = "Does authentication context (non-default) setting exist?";
			eAuditStatus status;
			if (authenticationContext != null && authenticationContext.ContextItems != null)
			{
				if (!authenticationContext.ContextItems.All((AuthenticationContextItem g) => g.IsDisabled))
				{
					status = eAuditStatus.CompletedSuccessful;
					goto IL_97;
				}
			}
			status = eAuditStatus.Failed;
			IL_97:
			string[] array = new string[2];
			array[0] = "AuthenticationContext={0}";
			int num = 1;
			string text;
			if (authenticationContext != null)
			{
				text = string.Join(", ", (from g in authenticationContext.ContextItems ?? new List<AuthenticationContextItem>()
				select g.ContextItemType.ToString()).ToArray<string>());
			}
			else
			{
				text = "-";
			}
			array[num] = text;
			list2.Add(new AuditCheck(title, status, array));
			List<AuditCheck> list3 = list;
			string title2 = "Does authorization context (non-default) setting exist?";
			eAuditStatus status2;
			if (authorizationContext != null && authorizationContext.ContextItems != null)
			{
				if (!authorizationContext.ContextItems.All((AuthorizationContextItem g) => g.IsDisabled))
				{
					status2 = eAuditStatus.CompletedSuccessful;
					goto IL_141;
				}
			}
			status2 = eAuditStatus.Failed;
			IL_141:
			string[] array2 = new string[2];
			array2[0] = "AuthorizationContext={0}";
			int num2 = 1;
			string text2;
			if (authorizationContext != null)
			{
				text2 = string.Join(", ", (from g in authorizationContext.ContextItems ?? new List<AuthorizationContextItem>()
				select g.ContextItemType.ToString()).ToArray<string>());
			}
			else
			{
				text2 = "-";
			}
			array2[num2] = text2;
			list3.Add(new AuditCheck(title2, status2, array2));
			auditResult2.Checks = list;
			return auditResult;
		}
	}
}
