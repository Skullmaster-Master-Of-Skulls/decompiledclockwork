using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.SpireDoc;
using TechnoPro.Common.Core.Templates;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.ICore.Templates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers
{
	// Token: 0x02000003 RID: 3
	public class AccommodationLetters : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002050 File Offset: 0x00000250
		public AccommodationLetters()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000220E File Offset: 0x0000040E
		public AccommodationLetters(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002220 File Offset: 0x00000420
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002228 File Offset: 0x00000428
		public OperationContext OpContext { get; set; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002234 File Offset: 0x00000434
		public AuditResult ExecuteAudit()
		{
			ITemplateManager templateManager = new TemplateManager(this.OpContext);
			TemplateCollection templateCollection = templateManager.LoadTemplates("accommodations", false);
			bool flag = templateCollection.Templates == null || templateCollection.Templates.Count < 1;
			AuditResult result;
			if (flag)
			{
				result = new AuditResult(eClockWorkAuditType.AccommodationLetters)
				{
					Checks = new List<AuditCheck>
					{
						new AuditCheck("Check LOA exists", eAuditStatus.Failed, new string[]
						{
							"No templates exist in 'accommodations' template group."
						})
					}
				};
			}
			else
			{
				AuditResult auditResult = new AuditResult(eClockWorkAuditType.AccommodationLetters);
				AuditResult auditResult2 = auditResult;
				List<AuditCheck> list = new List<AuditCheck>();
				List<AuditCheck> list2 = list;
				string title = "Check LOA exists";
				eAuditStatus status = eAuditStatus.CompletedSuccessful;
				string[] array = new string[2];
				array[0] = "AccommodationTemplates={0}";
				array[1] = string.Join(",", (from g in templateCollection.Templates
				select g.TemplateId.ToString()).ToArray<string>());
				list2.Add(new AuditCheck(title, status, array));
				auditResult2.Checks = list;
				AuditResult auditResult3 = auditResult;
				IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(this.OpContext);
				List<int> luCourseIds = new List<int>();
				MailMergeContextWithCustomDictionary contextWithDictionary = new MailMergeContextWithCustomDictionary
				{
					Context = new MailMergeContext
					{
						PersonId = 1
					},
					CustomDictionary = new MailMergeCustomDictionary
					{
						Args = new Dictionary<string, string>()
					}
				};
				foreach (Template template in templateCollection.Templates)
				{
					string title2 = string.Concat(new string[]
					{
						"Verifying template ",
						template.TemplateId.ToString(),
						" (",
						template.TemplateTitle ?? "",
						")"
					});
					try
					{
						BinaryFile binaryFile = mailMergingDocManager.MailMergeAccommodationSingleLetter(luCourseIds, contextWithDictionary, eFileFormat.PDF, template.TemplateId);
						bool flag2 = binaryFile != null && binaryFile.ByteArray != null && binaryFile.ByteArray.Length != 0;
						if (flag2)
						{
							auditResult3.Checks.Add(new AuditCheck(title2, eAuditStatus.CompletedSuccessful, new string[]
							{
								"Filename={0}:FileBinaryLength={1}",
								binaryFile.FileName ?? "NULL",
								binaryFile.ByteArray.Length.ToString()
							}));
						}
						else
						{
							auditResult3.Checks.Add(new AuditCheck(title2, eAuditStatus.Failed, new string[]
							{
								"Filename={0}",
								(binaryFile == null) ? "Doc is null" : (binaryFile.FileName ?? "NULL")
							}));
						}
					}
					catch (Exception ex)
					{
						auditResult3.Checks.Add(new AuditCheck(title2, eAuditStatus.Failed, new string[]
						{
							"Err={0}",
							ex.ToString()
						}));
					}
				}
				result = auditResult3;
			}
			return result;
		}
	}
}
