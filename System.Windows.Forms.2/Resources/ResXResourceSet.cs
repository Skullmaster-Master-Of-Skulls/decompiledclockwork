using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;

namespace System.Resources
{
	// Token: 0x020000F2 RID: 242
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ResXResourceSet : ResourceSet
	{
		// Token: 0x06000396 RID: 918 RVA: 0x0000B4C9 File Offset: 0x000096C9
		public ResXResourceSet(string fileName)
		{
			this.Reader = new ResXResourceReader(fileName);
			this.Table = new Hashtable();
			this.ReadResources();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000B4EE File Offset: 0x000096EE
		public ResXResourceSet(Stream stream)
		{
			this.Reader = new ResXResourceReader(stream);
			this.Table = new Hashtable();
			this.ReadResources();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000B513 File Offset: 0x00009713
		public override Type GetDefaultReader()
		{
			return typeof(ResXResourceReader);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000B51F File Offset: 0x0000971F
		public override Type GetDefaultWriter()
		{
			return typeof(ResXResourceWriter);
		}
	}
}
