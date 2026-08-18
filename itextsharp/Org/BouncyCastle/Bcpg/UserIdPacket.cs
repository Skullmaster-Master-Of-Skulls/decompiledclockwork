using System;
using System.Text;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000440 RID: 1088
	public class UserIdPacket : ContainedPacket
	{
		// Token: 0x060024EA RID: 9450 RVA: 0x000E0594 File Offset: 0x000DF594
		public UserIdPacket(BcpgInputStream bcpgIn)
		{
			this.idData = bcpgIn.ReadAll();
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000E05A8 File Offset: 0x000DF5A8
		public UserIdPacket(string id)
		{
			this.idData = Encoding.UTF8.GetBytes(id);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000E05C1 File Offset: 0x000DF5C1
		public string GetId()
		{
			return Encoding.UTF8.GetString(this.idData, 0, this.idData.Length);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000E05DC File Offset: 0x000DF5DC
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.UserId, this.idData, true);
		}

		// Token: 0x040019B5 RID: 6581
		private readonly byte[] idData;
	}
}
