using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000129 RID: 297
	internal struct PermissionSetEncoder
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0001CE11 File Offset: 0x0001B011
		public BlobBuilder Builder { get; }

		// Token: 0x060009CF RID: 2511 RVA: 0x0001CE19 File Offset: 0x0001B019
		public PermissionSetEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001CE22 File Offset: 0x0001B022
		public PermissionSetEncoder AddPermission(string typeName, BlobBuilder arguments)
		{
			this.Builder.WriteSerializedString(typeName);
			this.Builder.WriteCompressedInteger(arguments.Count);
			arguments.WriteContentTo(this.Builder);
			return new PermissionSetEncoder(this.Builder);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000031EB File Offset: 0x000013EB
		public void EndPermissions()
		{
		}
	}
}
