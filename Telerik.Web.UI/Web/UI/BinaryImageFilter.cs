using System;

namespace Telerik.Web.UI
{
	// Token: 0x020016B1 RID: 5809
	public abstract class BinaryImageFilter
	{
		// Token: 0x0600E043 RID: 57411
		public abstract byte[] ProcessImage(byte[] input);

		// Token: 0x170044B8 RID: 17592
		// (get) Token: 0x0600E044 RID: 57412
		public abstract string Name { get; }
	}
}
