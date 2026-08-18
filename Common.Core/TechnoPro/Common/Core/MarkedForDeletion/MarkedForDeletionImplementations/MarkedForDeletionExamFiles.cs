using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.MarkedForDeletion;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MarkedForDeletion;
using TechnoPro.Common.Public.Entities.MarkedForDeletion.JobResults;

namespace TechnoPro.Common.Core.MarkedForDeletion.MarkedForDeletionImplementations
{
	// Token: 0x020000C3 RID: 195
	public class MarkedForDeletionExamFiles : MarkedForDeletion_Base, IMarkedForDeletion
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x0002946B File Offset: 0x0002766B
		public MarkedForDeletionExamFiles(OperationContext opContext) : base(opContext)
		{
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00029478 File Offset: 0x00027678
		public MarkItemsForDeletionResult FigureOutNewItemsToBeMarkedForDeletion(MarkedForDeletionJob job, bool runInProductionMode)
		{
			IExamFileManager examFileManager = new ExamFileManager(base.OpContext);
			eMarkedForDeletionRuleType ruleType = job.RuleType;
			eMarkedForDeletionRuleType eMarkedForDeletionRuleType = ruleType;
			IList<int> list;
			if (eMarkedForDeletionRuleType != eMarkedForDeletionRuleType.EntryOrCreationDate)
			{
				if (eMarkedForDeletionRuleType != eMarkedForDeletionRuleType.AfterCourseEndDate)
				{
					return new MarkItemsForDeletionResult
					{
						ErrorMessage = string.Format("Rule type '{0}' is not implemented", job.RuleType)
					};
				}
				try
				{
					int numDays = job.NumDays;
					list = examFileManager.LoadExamFileIdsWhereCourseEndDateIsInThePast(numDays);
				}
				catch (Exception ex)
				{
					return new MarkItemsForDeletionResult
					{
						ErrorMessage = ex.ToString()
					};
				}
			}
			else
			{
				try
				{
					list = examFileManager.LoadExamFileIdsOlderThanDate(job.CutoffDate.Value);
				}
				catch (Exception ex2)
				{
					return new MarkItemsForDeletionResult
					{
						ErrorMessage = ex2.ToString()
					};
				}
			}
			IList<string> ids;
			if (list == null)
			{
				ids = null;
			}
			else
			{
				ids = (from g in list
				select g.ToString()).ToList<string>();
			}
			return base.MarkItemsForDeletion(job, ids, runInProductionMode);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00029594 File Offset: 0x00027794
		public MoveItemsToTempResult MoveMarkedItemsToTemp(MarkedForDeletionJob job, bool runInProductionMode)
		{
			IMarkedForDeletionArchiveManager markedForDeletionArchiveManager = new MarkedForDeletionArchiveManager(base.OpContext);
			throw new NotImplementedException();
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000072EA File Offset: 0x000054EA
		public DeleteItemsFromTempResult DeleteTempItems(MarkedForDeletionJob job, bool runInProductionMode)
		{
			throw new NotImplementedException();
		}
	}
}
