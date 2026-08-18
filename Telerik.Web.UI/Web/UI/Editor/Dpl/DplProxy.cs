using System;
using System.Reflection;

namespace Telerik.Web.UI.Editor.Dpl
{
	// Token: 0x02000284 RID: 644
	public abstract class DplProxy
	{
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0004DB9D File Offset: 0x0004BD9D
		public Assembly DocumentsFlow
		{
			get
			{
				if (this.documentsFlow == null)
				{
					this.documentsFlow = ReflectionHelper.GetAssembly("Telerik.Windows.Documents.Flow.dll");
				}
				return this.documentsFlow;
			}
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0004DBC3 File Offset: 0x0004BDC3
		public virtual object CreateHtmlFormatProvider()
		{
			return ReflectionHelper.CreateInstance(this.DocumentsFlow, "Telerik.Windows.Documents.Flow.FormatProviders.Html.HtmlFormatProvider", null);
		}

		// Token: 0x04000607 RID: 1543
		private Assembly documentsFlow;
	}
}
