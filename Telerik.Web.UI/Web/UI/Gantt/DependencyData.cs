using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000331 RID: 817
	[DataContract]
	public class DependencyData : IDependencyData, IDependencyBase
	{
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x0005A1F7 File Offset: 0x000583F7
		// (set) Token: 0x06001C2A RID: 7210 RVA: 0x0005A1FF File Offset: 0x000583FF
		[DataMember]
		public object ID { get; set; }

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0005A208 File Offset: 0x00058408
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x0005A210 File Offset: 0x00058410
		[DataMember]
		public object SuccessorID { get; set; }

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0005A219 File Offset: 0x00058419
		// (set) Token: 0x06001C2E RID: 7214 RVA: 0x0005A221 File Offset: 0x00058421
		[DataMember]
		public object PredecessorID { get; set; }

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0005A22A File Offset: 0x0005842A
		// (set) Token: 0x06001C30 RID: 7216 RVA: 0x0005A232 File Offset: 0x00058432
		[DataMember]
		public DependencyType Type { get; set; }

		// Token: 0x06001C31 RID: 7217 RVA: 0x0005A23B File Offset: 0x0005843B
		public virtual void CopyFrom(IDependency srcDependency)
		{
			this.ID = srcDependency.ID;
			this.PredecessorID = srcDependency.PredecessorID;
			this.SuccessorID = srcDependency.SuccessorID;
			this.Type = srcDependency.Type;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0005A270 File Offset: 0x00058470
		public virtual void CopyTo(IDependency destDependency)
		{
			destDependency.ID = DependencyData.GetResolvedID(this.ID);
			destDependency.PredecessorID = DependencyData.GetResolvedID(this.PredecessorID);
			destDependency.SuccessorID = DependencyData.GetResolvedID(this.SuccessorID);
			destDependency.Type = this.Type;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0005A2BC File Offset: 0x000584BC
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
