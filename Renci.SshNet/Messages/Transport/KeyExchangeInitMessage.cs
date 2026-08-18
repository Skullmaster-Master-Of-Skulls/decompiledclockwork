using System;
using Renci.SshNet.Abstractions;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000D9 RID: 217
	[Message("SSH_MSG_KEXINIT", 20)]
	public class KeyExchangeInitMessage : Message, IKeyExchangedAllowed
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x0001FF10 File Offset: 0x0001E110
		public KeyExchangeInitMessage()
		{
			byte[] array = new byte[16];
			CryptoAbstraction.GenerateRandom(array);
			this.Cookie = array;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001FF38 File Offset: 0x0001E138
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0001FF40 File Offset: 0x0001E140
		public byte[] Cookie { get; private set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0001FF49 File Offset: 0x0001E149
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x0001FF51 File Offset: 0x0001E151
		public string[] KeyExchangeAlgorithms { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0001FF5A File Offset: 0x0001E15A
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0001FF62 File Offset: 0x0001E162
		public string[] ServerHostKeyAlgorithms { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0001FF6B File Offset: 0x0001E16B
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x0001FF73 File Offset: 0x0001E173
		public string[] EncryptionAlgorithmsClientToServer { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0001FF7C File Offset: 0x0001E17C
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0001FF84 File Offset: 0x0001E184
		public string[] EncryptionAlgorithmsServerToClient { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0001FF8D File Offset: 0x0001E18D
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x0001FF95 File Offset: 0x0001E195
		public string[] MacAlgorithmsClientToServer { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0001FF9E File Offset: 0x0001E19E
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0001FFA6 File Offset: 0x0001E1A6
		public string[] MacAlgorithmsServerToClient { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0001FFAF File Offset: 0x0001E1AF
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0001FFB7 File Offset: 0x0001E1B7
		public string[] CompressionAlgorithmsClientToServer { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0001FFC8 File Offset: 0x0001E1C8
		public string[] CompressionAlgorithmsServerToClient { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001FFD1 File Offset: 0x0001E1D1
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0001FFD9 File Offset: 0x0001E1D9
		public string[] LanguagesClientToServer { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0001FFE2 File Offset: 0x0001E1E2
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x0001FFEA File Offset: 0x0001E1EA
		public string[] LanguagesServerToClient { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001FFF3 File Offset: 0x0001E1F3
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x0001FFFB File Offset: 0x0001E1FB
		public bool FirstKexPacketFollows { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00020004 File Offset: 0x0001E204
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0002000C File Offset: 0x0001E20C
		public uint Reserved { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001EA45 File Offset: 0x0001CC45
		protected override int BufferCapacity
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00020018 File Offset: 0x0001E218
		protected override void LoadData()
		{
			base.ResetReader();
			this.Cookie = base.ReadBytes(16);
			this.KeyExchangeAlgorithms = base.ReadNamesList();
			this.ServerHostKeyAlgorithms = base.ReadNamesList();
			this.EncryptionAlgorithmsClientToServer = base.ReadNamesList();
			this.EncryptionAlgorithmsServerToClient = base.ReadNamesList();
			this.MacAlgorithmsClientToServer = base.ReadNamesList();
			this.MacAlgorithmsServerToClient = base.ReadNamesList();
			this.CompressionAlgorithmsClientToServer = base.ReadNamesList();
			this.CompressionAlgorithmsServerToClient = base.ReadNamesList();
			this.LanguagesClientToServer = base.ReadNamesList();
			this.LanguagesServerToClient = base.ReadNamesList();
			this.FirstKexPacketFollows = base.ReadBoolean();
			this.Reserved = base.ReadUInt32();
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000200CC File Offset: 0x0001E2CC
		protected override void SaveData()
		{
			base.Write(this.Cookie);
			base.Write(this.KeyExchangeAlgorithms);
			base.Write(this.ServerHostKeyAlgorithms);
			base.Write(this.EncryptionAlgorithmsClientToServer);
			base.Write(this.EncryptionAlgorithmsServerToClient);
			base.Write(this.MacAlgorithmsClientToServer);
			base.Write(this.MacAlgorithmsServerToClient);
			base.Write(this.CompressionAlgorithmsClientToServer);
			base.Write(this.CompressionAlgorithmsServerToClient);
			base.Write(this.LanguagesClientToServer);
			base.Write(this.LanguagesServerToClient);
			base.Write(this.FirstKexPacketFollows);
			base.Write(this.Reserved);
		}
	}
}
