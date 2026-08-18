using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200046F RID: 1135
	public class PgpEncryptedDataList : PgpObject
	{
		// Token: 0x060026BB RID: 9915 RVA: 0x000EA9E0 File Offset: 0x000E99E0
		public PgpEncryptedDataList(BcpgInputStream bcpgInput)
		{
			while (bcpgInput.NextPacketTag() == PacketTag.PublicKeyEncryptedSession || bcpgInput.NextPacketTag() == PacketTag.SymmetricKeyEncryptedSessionKey)
			{
				this.list.Add(bcpgInput.ReadPacket());
			}
			this.data = (InputStreamPacket)bcpgInput.ReadPacket();
			for (int num = 0; num != this.list.Count; num++)
			{
				if (this.list[num] is SymmetricKeyEncSessionPacket)
				{
					this.list[num] = new PgpPbeEncryptedData((SymmetricKeyEncSessionPacket)this.list[num], this.data);
				}
				else
				{
					this.list[num] = new PgpPublicKeyEncryptedData((PublicKeyEncSessionPacket)this.list[num], this.data);
				}
			}
		}

		// Token: 0x170006A1 RID: 1697
		public PgpEncryptedData this[int index]
		{
			get
			{
				return (PgpEncryptedData)this.list[index];
			}
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x000EAAC3 File Offset: 0x000E9AC3
		[Obsolete("Use 'object[index]' syntax instead")]
		public object Get(int index)
		{
			return this[index];
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x000EAACC File Offset: 0x000E9ACC
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x000EAAD9 File Offset: 0x000E9AD9
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x000EAAE6 File Offset: 0x000E9AE6
		public bool IsEmpty
		{
			get
			{
				return this.list.Count == 0;
			}
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000EAAF6 File Offset: 0x000E9AF6
		public IEnumerable GetEncryptedDataObjects()
		{
			return new EnumerableProxy(this.list);
		}

		// Token: 0x04001AB6 RID: 6838
		private ArrayList list = new ArrayList();

		// Token: 0x04001AB7 RID: 6839
		private InputStreamPacket data;
	}
}
