using System;
using System.Data;
using System.Drawing;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000346 RID: 838
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(DataTable))]
	[KnownType(typeof(DateTime))]
	[KnownType(typeof(bool))]
	[KnownType(typeof(string))]
	[KnownType(typeof(byte[]))]
	[KnownType(typeof(Bitmap))]
	[KnownType(typeof(int[]))]
	[KnownType(typeof(TimeSpan))]
	[KnownType(typeof(Color))]
	[KnownType(typeof(int))]
	[KnownType(typeof(Image))]
	public class ReportParameterDTO
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00008FD1 File Offset: 0x000071D1
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x00008FD9 File Offset: 0x000071D9
		[DataMember]
		public virtual string Name { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00008FE2 File Offset: 0x000071E2
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x00008FEA File Offset: 0x000071EA
		[DataMember]
		public virtual object Value { get; set; }
	}
}
