using System;

namespace System.Web.UI
{
	// Token: 0x020002FB RID: 763
	internal class WebHandlerParser : SimpleWebHandlerParser
	{
		// Token: 0x0600234E RID: 9038 RVA: 0x00073165 File Offset: 0x00071365
		internal WebHandlerParser(string virtualPath) : base(null, virtualPath, null)
		{
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x00073170 File Offset: 0x00071370
		protected override string DefaultDirectiveName
		{
			get
			{
				return "webhandler";
			}
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x00073177 File Offset: 0x00071377
		internal override void ValidateBaseType(Type t)
		{
			Util.CheckAssignableType(typeof(IHttpHandler), t);
		}
	}
}
