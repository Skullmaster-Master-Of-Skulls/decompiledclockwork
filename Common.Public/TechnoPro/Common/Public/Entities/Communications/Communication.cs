using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Communications
{
	// Token: 0x02000443 RID: 1091
	public class Communication : CommunicationBase
	{
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000253BF File Offset: 0x000235BF
		public override int WhoSentPersonId
		{
			get
			{
				PersonBase whoSent = this.WhoSent;
				return (whoSent != null) ? whoSent.PersonId : 0;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x000253D3 File Offset: 0x000235D3
		// (set) Token: 0x06002117 RID: 8471 RVA: 0x000253DB File Offset: 0x000235DB
		public PersonBase WhoSent { get; set; }
	}
}
