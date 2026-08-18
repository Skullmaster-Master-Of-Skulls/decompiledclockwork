using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002E9 RID: 745
	[DataContract]
	public class AssignmentData : IAssignmentData, IAssignmentBase
	{
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x00054AB1 File Offset: 0x00052CB1
		// (set) Token: 0x060019C3 RID: 6595 RVA: 0x00054AB9 File Offset: 0x00052CB9
		[DataMember]
		public object ID { get; set; }

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x00054AC2 File Offset: 0x00052CC2
		// (set) Token: 0x060019C5 RID: 6597 RVA: 0x00054ACA File Offset: 0x00052CCA
		[DataMember]
		public object TaskID { get; set; }

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00054AD3 File Offset: 0x00052CD3
		// (set) Token: 0x060019C7 RID: 6599 RVA: 0x00054ADB File Offset: 0x00052CDB
		[DataMember]
		public object ResourceID { get; set; }

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00054AE4 File Offset: 0x00052CE4
		// (set) Token: 0x060019C9 RID: 6601 RVA: 0x00054AEC File Offset: 0x00052CEC
		[DataMember]
		public object Units { get; set; }

		// Token: 0x060019CA RID: 6602 RVA: 0x00054AF5 File Offset: 0x00052CF5
		public void CopyFrom(IAssignment srcAssignment)
		{
			this.ID = srcAssignment.ID;
			this.TaskID = srcAssignment.TaskID;
			this.ResourceID = srcAssignment.ResourceID;
			this.Units = srcAssignment.Units;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00054B28 File Offset: 0x00052D28
		public void CopyTo(IAssignment destAssignment)
		{
			destAssignment.ID = AssignmentData.GetResolvedID(this.ID);
			destAssignment.TaskID = AssignmentData.GetResolvedID(this.TaskID);
			destAssignment.ResourceID = AssignmentData.GetResolvedID(this.ResourceID);
			destAssignment.Units = this.Units;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00054B74 File Offset: 0x00052D74
		private static object GetResolvedID(object srcID)
		{
			if (srcID is string)
			{
				try
				{
					Guid guid = new Guid(srcID.ToString());
					srcID = guid;
				}
				catch
				{
				}
			}
			return srcID;
		}
	}
}
