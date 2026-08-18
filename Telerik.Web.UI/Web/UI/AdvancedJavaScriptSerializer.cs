using System;
using System.Text;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Common.SerializeJS;

namespace Telerik.Web.UI
{
	// Token: 0x020001CC RID: 460
	public class AdvancedJavaScriptSerializer : JavaScriptSerializer
	{
		// Token: 0x060010B2 RID: 4274 RVA: 0x0003D26A File Offset: 0x0003B46A
		public AdvancedJavaScriptSerializer()
		{
			this.Markers = new JavaScriptSerializerMarkers();
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0003D27D File Offset: 0x0003B47D
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x0003D285 File Offset: 0x0003B485
		public JavaScriptSerializerMarkers Markers { get; set; }

		// Token: 0x060010B5 RID: 4277 RVA: 0x0003D290 File Offset: 0x0003B490
		public new string Serialize(object obj)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.Serialize(obj, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0003D2B1 File Offset: 0x0003B4B1
		public new void Serialize(object obj, StringBuilder output)
		{
			base.Serialize(obj, output);
			output = this.RemoveMethodJSMarkers(output);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0003D2C4 File Offset: 0x0003B4C4
		private StringBuilder RemoveMethodJSMarkers(StringBuilder serialized)
		{
			return this.Markers.CleanUpMarkers(serialized);
		}
	}
}
