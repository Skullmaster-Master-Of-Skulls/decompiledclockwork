using System;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent
{
	// Token: 0x02000566 RID: 1382
	[Serializable]
	public class ChannelUnderlyingPerson : ICloneable<ChannelUnderlyingPerson>, ICloneable
	{
		// Token: 0x06002C89 RID: 11401 RVA: 0x0000D55A File Offset: 0x0000B75A
		public ChannelUnderlyingPerson()
		{
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x0003199C File Offset: 0x0002FB9C
		public ChannelUnderlyingPerson(ChannelUnderlyingPerson cup)
		{
			bool flag = cup == null;
			if (!flag)
			{
				this.PersonId = cup.PersonId;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000319C8 File Offset: 0x0002FBC8
		// (set) Token: 0x06002C8C RID: 11404 RVA: 0x000319D0 File Offset: 0x0002FBD0
		public int PersonId { get; set; }

		// Token: 0x06002C8D RID: 11405 RVA: 0x000319DC File Offset: 0x0002FBDC
		public ChannelUnderlyingPerson Clone()
		{
			return new ChannelUnderlyingPerson(this);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000319F4 File Offset: 0x0002FBF4
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
