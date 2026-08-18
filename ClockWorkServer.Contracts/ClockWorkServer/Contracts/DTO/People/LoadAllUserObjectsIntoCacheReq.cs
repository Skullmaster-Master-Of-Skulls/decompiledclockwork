using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003A2 RID: 930
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllUserObjectsIntoCacheReq : BaseMsmqMessageReq
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00009CA4 File Offset: 0x00007EA4
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x00009CAC File Offset: 0x00007EAC
		[DataMember]
		public bool CheckForNewStudents { get; set; }

		// Token: 0x060014D9 RID: 5337 RVA: 0x00009CB5 File Offset: 0x00007EB5
		public LoadAllUserObjectsIntoCacheReq()
		{
			this.SetDefaults();
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00009CC6 File Offset: 0x00007EC6
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetDefaults();
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00009CD0 File Offset: 0x00007ED0
		private void SetDefaults()
		{
			this.CheckForNewStudents = true;
		}
	}
}
