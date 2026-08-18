using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000559 RID: 1369
	public class PreferredAlgorithms : SignatureSubpacket
	{
		// Token: 0x06002F37 RID: 12087 RVA: 0x0012583C File Offset: 0x0012483C
		private static byte[] IntToByteArray(int[] v)
		{
			byte[] array = new byte[v.Length];
			for (int num = 0; num != v.Length; num++)
			{
				array[num] = (byte)v[num];
			}
			return array;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x00125868 File Offset: 0x00124868
		public PreferredAlgorithms(SignatureSubpacketTag type, bool critical, byte[] data) : base(type, critical, data)
		{
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x00125873 File Offset: 0x00124873
		public PreferredAlgorithms(SignatureSubpacketTag type, bool critical, int[] preferences) : base(type, critical, PreferredAlgorithms.IntToByteArray(preferences))
		{
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x00125884 File Offset: 0x00124884
		public int[] GetPreferences()
		{
			int[] array = new int[this.data.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = (int)(this.data[num] & byte.MaxValue);
			}
			return array;
		}
	}
}
