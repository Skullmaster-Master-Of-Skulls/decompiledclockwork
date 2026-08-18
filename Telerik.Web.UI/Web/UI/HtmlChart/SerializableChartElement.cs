using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003C2 RID: 962
	public class SerializableChartElement : StateManager
	{
		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x0007611E File Offset: 0x0007431E
		protected AdvancedJavaScriptSerializer Serializer
		{
			get
			{
				if (this._serializer == null)
				{
					this._serializer = new AdvancedJavaScriptSerializer();
				}
				return this._serializer;
			}
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x00076139 File Offset: 0x00074339
		internal string Serialize()
		{
			return this.Serializer.Serialize(this);
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x00076147 File Offset: 0x00074347
		protected void RegisterConverters(IEnumerable<JavaScriptConverter> converters)
		{
			this.Serializer.RegisterConverters(converters);
		}

		// Token: 0x04000946 RID: 2374
		private AdvancedJavaScriptSerializer _serializer;
	}
}
