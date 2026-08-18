using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000589 RID: 1417
	public class StudentMediaRequest : BusinessBase<int>
	{
		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x00032596 File Offset: 0x00030796
		// (set) Token: 0x06002DB4 RID: 11700 RVA: 0x0003259E File Offset: 0x0003079E
		public int StudentMediaRequestId { get; set; }

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x000325A7 File Offset: 0x000307A7
		// (set) Token: 0x06002DB6 RID: 11702 RVA: 0x000325AF File Offset: 0x000307AF
		public PersonBase RequestMadeFromStudent { get; set; }

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000325B8 File Offset: 0x000307B8
		// (set) Token: 0x06002DB8 RID: 11704 RVA: 0x000325C0 File Offset: 0x000307C0
		public DateTime CreatedDatetime { get; set; }

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x000325C9 File Offset: 0x000307C9
		// (set) Token: 0x06002DBA RID: 11706 RVA: 0x000325D1 File Offset: 0x000307D1
		public IList<MediaContentRequestedInfo> ContentRequestedList { get; set; }

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000325DA File Offset: 0x000307DA
		// (set) Token: 0x06002DBC RID: 11708 RVA: 0x000325E2 File Offset: 0x000307E2
		public DateTime? CompletedDateTime { get; set; }

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x000325EB File Offset: 0x000307EB
		// (set) Token: 0x06002DBE RID: 11710 RVA: 0x000325F3 File Offset: 0x000307F3
		public SchoolCampus Campus { get; set; }
	}
}
