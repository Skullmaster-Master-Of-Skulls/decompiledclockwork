using System;

namespace System.Security.AccessControl
{
	// Token: 0x02000923 RID: 2339
	[Flags]
	public enum FileSystemRights
	{
		// Token: 0x04002BCE RID: 11214
		ReadData = 1,
		// Token: 0x04002BCF RID: 11215
		ListDirectory = 1,
		// Token: 0x04002BD0 RID: 11216
		WriteData = 2,
		// Token: 0x04002BD1 RID: 11217
		CreateFiles = 2,
		// Token: 0x04002BD2 RID: 11218
		AppendData = 4,
		// Token: 0x04002BD3 RID: 11219
		CreateDirectories = 4,
		// Token: 0x04002BD4 RID: 11220
		ReadExtendedAttributes = 8,
		// Token: 0x04002BD5 RID: 11221
		WriteExtendedAttributes = 16,
		// Token: 0x04002BD6 RID: 11222
		ExecuteFile = 32,
		// Token: 0x04002BD7 RID: 11223
		Traverse = 32,
		// Token: 0x04002BD8 RID: 11224
		DeleteSubdirectoriesAndFiles = 64,
		// Token: 0x04002BD9 RID: 11225
		ReadAttributes = 128,
		// Token: 0x04002BDA RID: 11226
		WriteAttributes = 256,
		// Token: 0x04002BDB RID: 11227
		Delete = 65536,
		// Token: 0x04002BDC RID: 11228
		ReadPermissions = 131072,
		// Token: 0x04002BDD RID: 11229
		ChangePermissions = 262144,
		// Token: 0x04002BDE RID: 11230
		TakeOwnership = 524288,
		// Token: 0x04002BDF RID: 11231
		Synchronize = 1048576,
		// Token: 0x04002BE0 RID: 11232
		FullControl = 2032127,
		// Token: 0x04002BE1 RID: 11233
		Read = 131209,
		// Token: 0x04002BE2 RID: 11234
		ReadAndExecute = 131241,
		// Token: 0x04002BE3 RID: 11235
		Write = 278,
		// Token: 0x04002BE4 RID: 11236
		Modify = 197055
	}
}
