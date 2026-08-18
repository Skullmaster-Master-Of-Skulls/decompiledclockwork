using System;

namespace Telerik.Web.UI
{
	// Token: 0x020016B2 RID: 5810
	public class BinaryImageFilterProcessor
	{
		// Token: 0x170044B9 RID: 17593
		// (get) Token: 0x0600E046 RID: 57414 RVA: 0x0031E1A8 File Offset: 0x0031C3A8
		public BinaryImageFilterCollection Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x0600E047 RID: 57415 RVA: 0x0031E1B0 File Offset: 0x0031C3B0
		public BinaryImageFilterProcessor(BinaryImageFilterCollection filters)
		{
			this._filters = filters;
		}

		// Token: 0x0600E048 RID: 57416 RVA: 0x0031E1C0 File Offset: 0x0031C3C0
		public virtual byte[] ProcessFilters(byte[] imageData)
		{
			if (imageData == null)
			{
				throw new ArgumentNullException("imageData");
			}
			byte[] array = imageData;
			foreach (BinaryImageFilter binaryImageFilter in this.Filters)
			{
				array = binaryImageFilter.ProcessImage(array);
			}
			return array;
		}

		// Token: 0x040040E0 RID: 16608
		private readonly BinaryImageFilterCollection _filters;
	}
}
