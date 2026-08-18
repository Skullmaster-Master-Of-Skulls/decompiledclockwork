using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000349 RID: 841
	[DataContract]
	public class TaskData : ITaskData, ITaskBase
	{
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0005A929 File Offset: 0x00058B29
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x0005A931 File Offset: 0x00058B31
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0005A93A File Offset: 0x00058B3A
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x0005A942 File Offset: 0x00058B42
		[DataMember]
		public object ID { get; set; }

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0005A94B File Offset: 0x00058B4B
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x0005A953 File Offset: 0x00058B53
		[DataMember]
		public object ParentID { get; set; }

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x0005A95C File Offset: 0x00058B5C
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x0005A964 File Offset: 0x00058B64
		[DataMember]
		public object OrderID { get; set; }

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x0005A96D File Offset: 0x00058B6D
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x0005A975 File Offset: 0x00058B75
		[DataMember]
		public bool Summary { get; set; }

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x0005A97E File Offset: 0x00058B7E
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0005A986 File Offset: 0x00058B86
		[DataMember]
		public bool Expanded { get; set; }

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0005A98F File Offset: 0x00058B8F
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x0005A997 File Offset: 0x00058B97
		[DataMember]
		public decimal PercentComplete { get; set; }

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0005A9A0 File Offset: 0x00058BA0
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x0005A9A8 File Offset: 0x00058BA8
		[DataMember]
		public DateTime Start { get; set; }

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0005A9B1 File Offset: 0x00058BB1
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x0005A9B9 File Offset: 0x00058BB9
		[DataMember]
		public DateTime? PlannedStart { get; set; }

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0005A9C2 File Offset: 0x00058BC2
		// (set) Token: 0x06001CBB RID: 7355 RVA: 0x0005A9CA File Offset: 0x00058BCA
		[DataMember]
		public DateTime End { get; set; }

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0005A9D3 File Offset: 0x00058BD3
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x0005A9DB File Offset: 0x00058BDB
		[DataMember]
		public DateTime? PlannedEnd { get; set; }

		// Token: 0x06001CBE RID: 7358 RVA: 0x0005A9E4 File Offset: 0x00058BE4
		public virtual void CopyFrom(ITask srcTask)
		{
			this.ID = srcTask.ID;
			this.ParentID = srcTask.ParentID;
			this.OrderID = srcTask.OrderID;
			this.Start = srcTask.Start;
			this.PlannedStart = srcTask.PlannedStart;
			this.End = srcTask.End;
			this.PlannedEnd = srcTask.PlannedEnd;
			this.Title = srcTask.Title;
			this.Summary = srcTask.Summary;
			this.Expanded = srcTask.Expanded;
			this.PercentComplete = srcTask.PercentComplete;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0005AA78 File Offset: 0x00058C78
		public virtual void CopyTo(ITask destTask)
		{
			destTask.ID = TaskData.GetResolvedID(this.ID);
			destTask.ParentID = TaskData.GetResolvedID(this.ParentID);
			destTask.OrderID = this.OrderID;
			destTask.Start = this.Start;
			destTask.PlannedStart = this.PlannedStart;
			destTask.End = this.End;
			destTask.PlannedEnd = this.PlannedEnd;
			destTask.Title = this.Title;
			destTask.Summary = this.Summary;
			destTask.Expanded = this.Expanded;
			destTask.PercentComplete = this.PercentComplete;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0005AB14 File Offset: 0x00058D14
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
