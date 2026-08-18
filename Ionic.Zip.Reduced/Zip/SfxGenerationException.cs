using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x02000013 RID: 19
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00008")]
	[Serializable]
	public class SfxGenerationException : ZipException
	{
		// Token: 0x06000059 RID: 89 RVA: 0x000025AA File Offset: 0x000007AA
		public SfxGenerationException()
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000025B2 File Offset: 0x000007B2
		public SfxGenerationException(string message) : base(message)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000025BB File Offset: 0x000007BB
		protected SfxGenerationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
