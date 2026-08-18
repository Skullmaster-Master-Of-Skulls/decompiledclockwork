using System;
using EncryptionClassLibrary;

namespace TechnoPro.Common.DAO.Snapshot
{
	// Token: 0x02000003 RID: 3
	public class Encryptors
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00003135 File Offset: 0x00001335
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000313D File Offset: 0x0000133D
		public IBatchDecryptor BatchDecryptorSource { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00003146 File Offset: 0x00001346
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000314E File Offset: 0x0000134E
		public IBatchEncryptor BatchEncryptorDestination { get; set; }
	}
}
