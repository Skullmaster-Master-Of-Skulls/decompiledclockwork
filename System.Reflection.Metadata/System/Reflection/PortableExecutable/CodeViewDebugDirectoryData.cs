using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200001A RID: 26
	public struct CodeViewDebugDirectoryData
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005CCA File Offset: 0x00003ECA
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005CD2 File Offset: 0x00003ED2
		public Guid Guid { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005CDB File Offset: 0x00003EDB
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005CE3 File Offset: 0x00003EE3
		public int Age { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005CEC File Offset: 0x00003EEC
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public string Path { get; private set; }

		// Token: 0x060001B0 RID: 432 RVA: 0x00005CFD File Offset: 0x00003EFD
		internal CodeViewDebugDirectoryData(Guid guid, int age, string path)
		{
			this.Path = path;
			this.Guid = guid;
			this.Age = age;
		}
	}
}
