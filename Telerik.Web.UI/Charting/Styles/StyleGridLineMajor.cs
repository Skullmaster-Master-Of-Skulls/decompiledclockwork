using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017D2 RID: 6098
	[ParseChildren(true)]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleGridLineMajor : StyleGridLine
	{
		// Token: 0x0600ED4D RID: 60749 RVA: 0x00361F3E File Offset: 0x0036013E
		internal override void Reset()
		{
			base.Reset();
		}
	}
}
