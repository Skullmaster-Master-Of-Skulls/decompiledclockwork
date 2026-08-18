using System;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x02000A03 RID: 2563
	internal class SingleSectionBarcode : SingleSectionBarcodeBase
	{
		// Token: 0x06006141 RID: 24897 RVA: 0x0016DEF0 File Offset: 0x0016C0F0
		public SingleSectionBarcode(BarcodeType t)
		{
			switch (t)
			{
			case BarcodeType.Code11:
				base.Code = new Code11();
				return;
			case BarcodeType.Code128:
				base.Code = new Code128();
				return;
			case BarcodeType.Code128A:
				base.Code = new Code128A();
				return;
			case BarcodeType.Code128B:
				base.Code = new Code128B();
				return;
			case BarcodeType.Code128C:
				base.Code = new Code128C();
				return;
			case BarcodeType.Code39:
				base.Code = new Code39();
				return;
			case BarcodeType.Code39Extended:
				base.Code = new Code39Extended();
				return;
			case BarcodeType.Codabar:
				base.Code = new Codabar();
				return;
			case BarcodeType.Code25Standard:
				base.Code = new Code25Standard();
				return;
			case BarcodeType.Code25Interleaved:
				base.Code = new Code25Interleaved();
				return;
			case BarcodeType.Code93:
				base.Code = new Code93();
				return;
			case BarcodeType.Code93Extended:
				base.Code = new Code93Extended();
				return;
			case BarcodeType.UPCSupplement2:
				base.Code = new UPCSupplement2();
				return;
			case BarcodeType.UPCSupplement5:
				base.Code = new UPCSupplement5();
				return;
			case BarcodeType.Postnet:
				base.Code = new Postnet();
				return;
			}
			base.Code = null;
		}
	}
}
