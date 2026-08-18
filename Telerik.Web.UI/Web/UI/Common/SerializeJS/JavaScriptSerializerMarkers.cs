using System;
using System.Text;

namespace Telerik.Web.UI.Common.SerializeJS
{
	// Token: 0x020001CD RID: 461
	public class JavaScriptSerializerMarkers
	{
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0003D2D2 File Offset: 0x0003B4D2
		// (set) Token: 0x060010B9 RID: 4281 RVA: 0x0003D2DA File Offset: 0x0003B4DA
		internal string MethodJSMarker { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x0003D2E3 File Offset: 0x0003B4E3
		// (set) Token: 0x060010BB RID: 4283 RVA: 0x0003D2EB File Offset: 0x0003B4EB
		internal string MethodJSFormatMarker { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0003D2F4 File Offset: 0x0003B4F4
		// (set) Token: 0x060010BD RID: 4285 RVA: 0x0003D2FC File Offset: 0x0003B4FC
		internal string StartJSMarker { get; set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0003D305 File Offset: 0x0003B505
		// (set) Token: 0x060010BF RID: 4287 RVA: 0x0003D30D File Offset: 0x0003B50D
		internal string EndJSMarker { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x0003D316 File Offset: 0x0003B516
		// (set) Token: 0x060010C1 RID: 4289 RVA: 0x0003D31E File Offset: 0x0003B51E
		internal string SingleQuotationMarker { get; set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x0003D327 File Offset: 0x0003B527
		// (set) Token: 0x060010C3 RID: 4291 RVA: 0x0003D32F File Offset: 0x0003B52F
		internal string DoubleQuotationMarker { get; set; }

		// Token: 0x060010C4 RID: 4292 RVA: 0x0003D338 File Offset: 0x0003B538
		public JavaScriptSerializerMarkers() : this("|_telerik_|")
		{
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0003D348 File Offset: 0x0003B548
		public JavaScriptSerializerMarkers(string marker)
		{
			this.MethodJSMarker = marker;
			this.MethodJSFormatMarker = this.MethodJSMarker + "{0}" + this.MethodJSMarker;
			this.StartJSMarker = "\"" + this.MethodJSMarker;
			this.EndJSMarker = this.MethodJSMarker + "\"";
			this.SingleQuotationMarker = "|-|";
			this.DoubleQuotationMarker = "|--|";
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0003D3C0 File Offset: 0x0003B5C0
		public string WrapInMarkers(string jsString)
		{
			jsString = jsString.Replace("'", this.SingleQuotationMarker);
			jsString = jsString.Replace("\"", this.DoubleQuotationMarker);
			return string.Format(this.MethodJSFormatMarker, jsString);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0003D3F4 File Offset: 0x0003B5F4
		public StringBuilder CleanUpMarkers(StringBuilder stringBuilder)
		{
			return stringBuilder.Replace(this.StartJSMarker, string.Empty).Replace(this.EndJSMarker, string.Empty).Replace(this.SingleQuotationMarker, "'").Replace(this.DoubleQuotationMarker, "\"");
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0003D444 File Offset: 0x0003B644
		public string CleanUpMarkers(string input)
		{
			return input.Replace(this.StartJSMarker, string.Empty).Replace(this.EndJSMarker, string.Empty).Replace(this.SingleQuotationMarker, "'").Replace(this.DoubleQuotationMarker, "\"");
		}
	}
}
