using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers
{
	// Token: 0x02000008 RID: 8
	public class ListControls : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002050 File Offset: 0x00000250
		public ListControls()
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000361B File Offset: 0x0000181B
		public ListControls(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000362D File Offset: 0x0000182D
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00003635 File Offset: 0x00001835
		public OperationContext OpContext { get; set; }

		// Token: 0x0600002A RID: 42 RVA: 0x00003640 File Offset: 0x00001840
		public AuditResult ExecuteAudit()
		{
			IDynamicFieldManager dfm = new DynamicFieldManager(this.OpContext);
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
			IList<DynamicFormWithExtendedInfo> source = dynamicFormManager.LoadActiveFormsWithExtendedInfo();
			Dictionary<int, IList<DynamicField>> dictionary = source.ToDictionary((DynamicFormWithExtendedInfo activeForm) => activeForm.ScreenNum, (DynamicFormWithExtendedInfo activeForm) => (from h in dfm.LoadFields(activeForm.ScreenNum, true)
			where h.ControlCode == eControlCode.DropList || h.ControlCode == eControlCode.StaffComboBox || h.ControlCode == eControlCode.AccommodationDropList || h.ControlCode == eControlCode.FileList || h.ControlCode == eControlCode.ListView || h.ControlCode == eControlCode.MultiCheckBoxDropList || h.ControlCode == eControlCode.RadioGroup
			select h).ToList<DynamicField>());
			AuditResult auditResult = new AuditResult(eClockWorkAuditType.ListControls);
			using (Dictionary<int, IList<DynamicField>>.Enumerator enumerator = dictionary.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, IList<DynamicField>> kvp = enumerator.Current;
					DynamicFormWithExtendedInfo dynamicFormWithExtendedInfo = source.FirstOrDefault((DynamicFormWithExtendedInfo g) => g.ScreenNum == kvp.Key);
					foreach (DynamicField dynamicField in kvp.Value)
					{
						string title = string.Concat(new string[]
						{
							"Check list control has list (cid=",
							dynamicField.ControlId.ToString(),
							"; caption=",
							dynamicField.GetCaptionForDisplay(),
							"; form=",
							(dynamicFormWithExtendedInfo == null) ? "NULL" : (dynamicFormWithExtendedInfo.Title ?? "")
						});
						eControlCode controlCode = dynamicField.ControlCode;
						eControlCode eControlCode = controlCode;
						if (eControlCode != eControlCode.StaffComboBox)
						{
							auditResult.Checks.Add(this.CheckList(title, dynamicField.Setting1));
						}
						else
						{
							int setting = dynamicField.Setting1;
							auditResult.Checks.Add(new AuditCheck(title, (setting > 0) ? eAuditStatus.CompletedSuccessful : eAuditStatus.Failed, new string[]
							{
								"gid={0}",
								setting.ToString()
							}));
						}
					}
				}
			}
			return auditResult;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003858 File Offset: 0x00001A58
		private AuditCheck CheckList(string title, int lookupListGroupId)
		{
			bool flag = lookupListGroupId < 1;
			AuditCheck result;
			if (flag)
			{
				result = new AuditCheck(title, eAuditStatus.Failed, new string[]
				{
					"lookupListGroupId={0}",
					lookupListGroupId.ToString()
				});
			}
			else
			{
				IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				List<DynamicListItem> list = dynamicFieldManager.LoadListItems(lookupListGroupId);
				eAuditStatus status = (list.Count < 1) ? eAuditStatus.Failed : eAuditStatus.CompletedSuccessful;
				string[] array = new string[3];
				array[0] = "LookupListGroupId={0}:List items={1}";
				array[1] = lookupListGroupId.ToString();
				array[2] = string.Join(", ", (from g in list
				select g.LookupText ?? "").ToArray<string>());
				result = new AuditCheck(title, status, array);
			}
			return result;
		}
	}
}
