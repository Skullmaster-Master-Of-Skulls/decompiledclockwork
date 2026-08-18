using System;
using System.Collections;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200002E RID: 46
	public class UserAttributePacket : ContainedPacket
	{
		// Token: 0x06000140 RID: 320 RVA: 0x000089A0 File Offset: 0x000079A0
		public UserAttributePacket(BcpgInputStream bcpgIn)
		{
			UserAttributeSubpacketsParser userAttributeSubpacketsParser = new UserAttributeSubpacketsParser(bcpgIn);
			ArrayList arrayList = new ArrayList();
			UserAttributeSubpacket value;
			while ((value = userAttributeSubpacketsParser.ReadPacket()) != null)
			{
				arrayList.Add(value);
			}
			this.subpackets = new UserAttributeSubpacket[arrayList.Count];
			for (int num = 0; num != this.subpackets.Length; num++)
			{
				this.subpackets[num] = (UserAttributeSubpacket)arrayList[num];
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008A0C File Offset: 0x00007A0C
		public UserAttributePacket(UserAttributeSubpacket[] subpackets)
		{
			this.subpackets = subpackets;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00008A1B File Offset: 0x00007A1B
		public UserAttributeSubpacket[] GetSubpackets()
		{
			return this.subpackets;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00008A24 File Offset: 0x00007A24
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			for (int num = 0; num != this.subpackets.Length; num++)
			{
				this.subpackets[num].Encode(memoryStream);
			}
			bcpgOut.WritePacket(PacketTag.UserAttribute, memoryStream.ToArray(), false);
		}

		// Token: 0x0400009F RID: 159
		private readonly UserAttributeSubpacket[] subpackets;
	}
}
