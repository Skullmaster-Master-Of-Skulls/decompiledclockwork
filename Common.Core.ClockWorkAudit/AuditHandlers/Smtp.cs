using System;
using System.Collections.Generic;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers
{
	// Token: 0x0200000D RID: 13
	public class Smtp : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002050 File Offset: 0x00000250
		public Smtp()
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004053 File Offset: 0x00002253
		public Smtp(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00004065 File Offset: 0x00002265
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000406D File Offset: 0x0000226D
		public OperationContext OpContext { get; set; }

		// Token: 0x06000044 RID: 68 RVA: 0x00004078 File Offset: 0x00002278
		public AuditResult ExecuteAudit()
		{
			string text = "Sent test email to: " + "admin@clockworks.ca";
			AuditResult auditResult;
			try
			{
				IEmailManager emailManager = new EmailManager(this.OpContext);
				TPMailResult tpmailResult = emailManager.SendEmail(new TPMailMessage
				{
					From = new TPMailAddress
					{
						EmailAddress = "admin@clockworks.ca"
					},
					To = new List<TPMailAddress>
					{
						new TPMailAddress
						{
							EmailAddress = "admin@clockworks.ca"
						}
					},
					Cc = new List<TPMailAddress>(),
					Bcc = new List<TPMailAddress>(),
					Subject = "ClockWork Audit Test Email",
					Body = "Test"
				});
				bool flag = tpmailResult.Status == eTPMailResultStatus.CompletedSuccess;
				if (flag)
				{
					auditResult = Smtp.GetAuditResult("Check email", eAuditStatus.CompletedSuccessful, new string[]
					{
						text
					});
				}
				else
				{
					bool flag2 = tpmailResult.Status == eTPMailResultStatus.CompletedWithWarnings;
					if (flag2)
					{
						auditResult = Smtp.GetAuditResult("Check email", eAuditStatus.CompletedSuccessfulWithWarnings, new string[]
						{
							"Completed with warnings:{0}",
							text
						});
					}
					else
					{
						auditResult = Smtp.GetAuditResult("Check email", eAuditStatus.Failed, new string[]
						{
							"Email failed:err={0}",
							tpmailResult.ErrorMessage ?? "NULL"
						});
					}
				}
			}
			catch (Exception ex)
			{
				auditResult = Smtp.GetAuditResult("Check email", eAuditStatus.Failed, new string[]
				{
					"Email failed try catch:err={0}",
					ex.ToString()
				});
			}
			return auditResult;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000041EC File Offset: 0x000023EC
		private static AuditResult GetAuditResult(string title, eAuditStatus status, params string[] note)
		{
			return new AuditResult(eClockWorkAuditType.Smtp)
			{
				Checks = new List<AuditCheck>
				{
					new AuditCheck(title, status, note)
				}
			};
		}
	}
}
