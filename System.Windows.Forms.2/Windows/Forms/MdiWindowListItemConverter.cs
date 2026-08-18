using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020002F0 RID: 752
	internal class MdiWindowListItemConverter : ComponentConverter
	{
		// Token: 0x06002F98 RID: 12184 RVA: 0x000D6E2F File Offset: 0x000D502F
		public MdiWindowListItemConverter(Type type) : base(type)
		{
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000D6E38 File Offset: 0x000D5038
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			MenuStrip menuStrip = context.Instance as MenuStrip;
			if (menuStrip != null)
			{
				TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
				ArrayList arrayList = new ArrayList();
				int count = standardValues.Count;
				for (int i = 0; i < count; i++)
				{
					ToolStripItem toolStripItem = standardValues[i] as ToolStripItem;
					if (toolStripItem != null && toolStripItem.Owner == menuStrip)
					{
						arrayList.Add(toolStripItem);
					}
				}
				return new TypeConverter.StandardValuesCollection(arrayList);
			}
			return base.GetStandardValues(context);
		}
	}
}
