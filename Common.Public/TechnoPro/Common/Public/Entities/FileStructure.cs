using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000EB RID: 235
	[Serializable]
	public class FileStructure : BusinessBase<int>
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000E942 File Offset: 0x0000CB42
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x0000E94A File Offset: 0x0000CB4A
		public virtual byte[] BinaryData { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000E953 File Offset: 0x0000CB53
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x0000E95B File Offset: 0x0000CB5B
		public virtual FileType FileType { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000E964 File Offset: 0x0000CB64
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x0000E96C File Offset: 0x0000CB6C
		public virtual string Version { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000E975 File Offset: 0x0000CB75
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0000E97D File Offset: 0x0000CB7D
		public virtual DateTime UploadDateTime { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000E986 File Offset: 0x0000CB86
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0000E98E File Offset: 0x0000CB8E
		public virtual bool IsActive { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000E997 File Offset: 0x0000CB97
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0000E99F File Offset: 0x0000CB9F
		public virtual int WhoUploaded { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000E9A8 File Offset: 0x0000CBA8
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
		public virtual int AddrSize { get; set; }
	}
}
