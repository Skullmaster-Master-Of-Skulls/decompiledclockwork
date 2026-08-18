using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200003F RID: 63
	internal struct CodeViewDebugDirectoryData
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000428E File Offset: 0x0000248E
		public Guid Guid { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00004296 File Offset: 0x00002496
		public int Age { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000429E File Offset: 0x0000249E
		public string Path { get; }

		// Token: 0x0600019D RID: 413 RVA: 0x000042A6 File Offset: 0x000024A6
		internal CodeViewDebugDirectoryData(Guid guid, int age, string path)
		{
			this.Path = path;
			this.Guid = guid;
			this.Age = age;
		}
	}
}
