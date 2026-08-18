using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017EB RID: 6123
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class StyleTextBlockTitle : StyleTextBlock
	{
		// Token: 0x0600EE6C RID: 61036 RVA: 0x00364F1E File Offset: 0x0036311E
		public StyleTextBlockTitle()
		{
			this.styleTextBlockTextProperties = new TextPropertiesTitle();
		}

		// Token: 0x0600EE6D RID: 61037 RVA: 0x00364F31 File Offset: 0x00363131
		internal override void Reset()
		{
			base.Reset();
			this.styleTextBlockTextProperties = new TextPropertiesTitle();
		}
	}
}
