using System;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting
{
	// Token: 0x02001750 RID: 5968
	internal class ColorBlendEditor : CollectionEditor
	{
		// Token: 0x0600E8DA RID: 59610 RVA: 0x003449B5 File Offset: 0x00342BB5
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public ColorBlendEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600E8DB RID: 59611 RVA: 0x003449BE File Offset: 0x00342BBE
		protected override object CreateInstance(Type itemType)
		{
			return new GradientElement();
		}
	}
}
