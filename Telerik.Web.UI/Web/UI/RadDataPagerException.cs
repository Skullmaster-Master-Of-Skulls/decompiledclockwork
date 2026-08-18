using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200195B RID: 6491
	[Serializable]
	public class RadDataPagerException : Exception
	{
		// Token: 0x0600FB5E RID: 64350 RVA: 0x0038A094 File Offset: 0x00388294
		public RadDataPagerException()
		{
		}

		// Token: 0x0600FB5F RID: 64351 RVA: 0x0038A09C File Offset: 0x0038829C
		public RadDataPagerException(string Message) : base(Message)
		{
		}

		// Token: 0x0600FB60 RID: 64352 RVA: 0x0038A0A5 File Offset: 0x003882A5
		public RadDataPagerException(string Message, Exception Inner) : base(Message, Inner)
		{
		}
	}
}
