using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x020005B2 RID: 1458
	public class SignerUserId : SignatureSubpacket
	{
		// Token: 0x06003240 RID: 12864 RVA: 0x0013873C File Offset: 0x0013773C
		private static byte[] UserIdToBytes(string id)
		{
			byte[] array = new byte[id.Length];
			for (int num = 0; num != id.Length; num++)
			{
				array[num] = (byte)id[num];
			}
			return array;
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x00138772 File Offset: 0x00137772
		public SignerUserId(bool critical, byte[] data) : base(SignatureSubpacketTag.SignerUserId, critical, data)
		{
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x0013877E File Offset: 0x0013777E
		public SignerUserId(bool critical, string userId) : base(SignatureSubpacketTag.SignerUserId, critical, SignerUserId.UserIdToBytes(userId))
		{
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x00138790 File Offset: 0x00137790
		public string GetId()
		{
			char[] array = new char[this.data.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = (char)(this.data[num] & byte.MaxValue);
			}
			return new string(array);
		}
	}
}
