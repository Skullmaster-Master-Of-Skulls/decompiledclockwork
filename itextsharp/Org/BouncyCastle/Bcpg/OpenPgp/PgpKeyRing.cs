using System;
using System.Collections;
using System.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000342 RID: 834
	public abstract class PgpKeyRing : PgpObject
	{
		// Token: 0x06001E35 RID: 7733 RVA: 0x000B550E File Offset: 0x000B450E
		internal PgpKeyRing()
		{
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000B5516 File Offset: 0x000B4516
		internal static TrustPacket ReadOptionalTrustPacket(BcpgInputStream bcpgInput)
		{
			if (bcpgInput.NextPacketTag() != PacketTag.Trust)
			{
				return null;
			}
			return (TrustPacket)bcpgInput.ReadPacket();
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x000B5530 File Offset: 0x000B4530
		internal static ArrayList ReadSignaturesAndTrust(BcpgInputStream bcpgInput)
		{
			ArrayList result;
			try
			{
				ArrayList arrayList = new ArrayList();
				while (bcpgInput.NextPacketTag() == PacketTag.Signature)
				{
					SignaturePacket sigPacket = (SignaturePacket)bcpgInput.ReadPacket();
					TrustPacket trustPacket = PgpKeyRing.ReadOptionalTrustPacket(bcpgInput);
					arrayList.Add(new PgpSignature(sigPacket, trustPacket));
				}
				result = arrayList;
			}
			catch (PgpException ex)
			{
				throw new IOException("can't create signature object: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000B55A0 File Offset: 0x000B45A0
		internal static void ReadUserIDs(BcpgInputStream bcpgInput, out ArrayList ids, out ArrayList idTrusts, out ArrayList idSigs)
		{
			ids = new ArrayList();
			idTrusts = new ArrayList();
			idSigs = new ArrayList();
			while (bcpgInput.NextPacketTag() == PacketTag.UserId || bcpgInput.NextPacketTag() == PacketTag.UserAttribute)
			{
				Packet packet = bcpgInput.ReadPacket();
				if (packet is UserIdPacket)
				{
					UserIdPacket userIdPacket = (UserIdPacket)packet;
					ids.Add(userIdPacket.GetId());
				}
				else
				{
					UserAttributePacket userAttributePacket = (UserAttributePacket)packet;
					ids.Add(new PgpUserAttributeSubpacketVector(userAttributePacket.GetSubpackets()));
				}
				idTrusts.Add(PgpKeyRing.ReadOptionalTrustPacket(bcpgInput));
				idSigs.Add(PgpKeyRing.ReadSignaturesAndTrust(bcpgInput));
			}
		}
	}
}
