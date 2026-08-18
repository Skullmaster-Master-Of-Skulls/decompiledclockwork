using System;
using System.Data;
using System.Drawing;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000176 RID: 374
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(DateTime))]
	[KnownType(typeof(bool))]
	[KnownType(typeof(string))]
	[KnownType(typeof(byte[]))]
	[KnownType(typeof(Bitmap))]
	[KnownType(typeof(int[]))]
	[KnownType(typeof(int))]
	[KnownType(typeof(Image))]
	[KnownType(typeof(double))]
	public class CommonParameterDTO
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00004055 File Offset: 0x00002255
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x0000405D File Offset: 0x0000225D
		[DataMember]
		public string Name { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00004066 File Offset: 0x00002266
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x0000406E File Offset: 0x0000226E
		[DataMember]
		public object Value { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00004077 File Offset: 0x00002277
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x0000407F File Offset: 0x0000227F
		[DataMember]
		public DbType? DbType { get; set; }
	}
}
