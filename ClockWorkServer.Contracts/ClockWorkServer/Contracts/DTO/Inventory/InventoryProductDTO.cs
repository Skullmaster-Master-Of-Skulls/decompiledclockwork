using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000571 RID: 1393
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(Bitmap))]
	public class InventoryProductDTO
	{
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0000D17E File Offset: 0x0000B37E
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x0000D186 File Offset: 0x0000B386
		[DataMember]
		public Guid UniqueId { get; set; }

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0000D18F File Offset: 0x0000B38F
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x0000D197 File Offset: 0x0000B397
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		[DataMember]
		public string SerialNumber { get; set; }

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x0000D1B1 File Offset: 0x0000B3B1
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x0000D1B9 File Offset: 0x0000B3B9
		[DataMember]
		public string BarCode { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x0000D1C2 File Offset: 0x0000B3C2
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x0000D1CA File Offset: 0x0000B3CA
		[DataMember]
		public bool IsLoaned { get; set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x0000D1D3 File Offset: 0x0000B3D3
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0000D1DB File Offset: 0x0000B3DB
		[DataMember]
		public InventoryProductStatusDTO Status { get; set; }

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		[DataMember]
		public string CategoryName { get; set; }

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0000D1F5 File Offset: 0x0000B3F5
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x0000D1FD File Offset: 0x0000B3FD
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0000D206 File Offset: 0x0000B406
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x0000D20E File Offset: 0x0000B40E
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0000D217 File Offset: 0x0000B417
		// (set) Token: 0x06001CBB RID: 7355 RVA: 0x0000D21F File Offset: 0x0000B41F
		[DataMember]
		public Image Thumbnail { get; set; }

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0000D228 File Offset: 0x0000B428
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x0000D230 File Offset: 0x0000B430
		[DataMember]
		public InventoryVendorInfoDTO Vendor { get; set; }

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x0000D239 File Offset: 0x0000B439
		// (set) Token: 0x06001CBF RID: 7359 RVA: 0x0000D241 File Offset: 0x0000B441
		[DataMember]
		public InventoryLocationDTO Location { get; set; }

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x0000D24A File Offset: 0x0000B44A
		// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x0000D252 File Offset: 0x0000B452
		[DataMember]
		public DateTime? LocationDatetime { get; set; }

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0000D25B File Offset: 0x0000B45B
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x0000D263 File Offset: 0x0000B463
		[DataMember]
		public PersonBaseDTO InChargePerson { get; set; }

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x0000D26C File Offset: 0x0000B46C
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x0000D274 File Offset: 0x0000B474
		[DataMember]
		public InventoryGroupDTO Group { get; set; }

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x0000D27D File Offset: 0x0000B47D
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x0000D285 File Offset: 0x0000B485
		[DataMember]
		public int ProductDynamicDataId { get; set; }

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0000D28E File Offset: 0x0000B48E
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x0000D296 File Offset: 0x0000B496
		[DataMember]
		public IList<InventoryProductAccessoryDTO> Accessories { get; set; }
	}
}
