using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000347 RID: 839
	[DataContract]
	public class ResourceData : IResourceData, IResourceBase
	{
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x0005A87D File Offset: 0x00058A7D
		// (set) Token: 0x06001C9D RID: 7325 RVA: 0x0005A885 File Offset: 0x00058A85
		[DataMember]
		public object ID { get; set; }

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0005A88E File Offset: 0x00058A8E
		// (set) Token: 0x06001C9F RID: 7327 RVA: 0x0005A896 File Offset: 0x00058A96
		[DataMember]
		public string Text { get; set; }

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x0005A89F File Offset: 0x00058A9F
		// (set) Token: 0x06001CA1 RID: 7329 RVA: 0x0005A8A7 File Offset: 0x00058AA7
		[DataMember]
		public string Color { get; set; }

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x0005A8B0 File Offset: 0x00058AB0
		// (set) Token: 0x06001CA3 RID: 7331 RVA: 0x0005A8B8 File Offset: 0x00058AB8
		[DataMember]
		public string Format { get; set; }

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0005A8C1 File Offset: 0x00058AC1
		public virtual void CopyFrom(IResource srcResource)
		{
			this.ID = srcResource.ID;
			this.Text = srcResource.Text;
			this.Color = ColorTranslator.ToHtml(srcResource.Color);
			this.Format = srcResource.Format;
		}
	}
}
