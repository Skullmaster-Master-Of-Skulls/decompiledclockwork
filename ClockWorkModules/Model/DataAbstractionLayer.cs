using System;
using Databases;
using EncryptionClassLibrary;

namespace ClockWorkModules.Model
{
	// Token: 0x02000002 RID: 2
	[Obsolete("Use Databases.DatabaseLayerFactory instead")]
	public class DataAbstractionLayer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static DataAbstractionLayer GetInstance()
		{
			if (DataAbstractionLayer._dataAbstractionLayer == null)
			{
				DataAbstractionLayer._dataAbstractionLayer = new DataAbstractionLayer();
			}
			return DataAbstractionLayer._dataAbstractionLayer;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public DatabaseLayer DBLayer
		{
			get
			{
				return DatabaseLayerFactory.ClockWork;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000206F File Offset: 0x0000026F
		public IEncryption Encryption
		{
			get
			{
				return this.DBLayer.Encryption;
			}
		}

		// Token: 0x04000001 RID: 1
		private static DataAbstractionLayer _dataAbstractionLayer;
	}
}
