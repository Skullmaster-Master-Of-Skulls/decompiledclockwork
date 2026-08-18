using System;
using System.ComponentModel.Design;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000017 RID: 23
	public class MultiHandleSliderTargetsEditor : CollectionEditor
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00004197 File Offset: 0x00002397
		public MultiHandleSliderTargetsEditor(Type type) : base(type)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000041A0 File Offset: 0x000023A0
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[]
			{
				typeof(MultiHandleSliderTarget)
			};
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000041C2 File Offset: 0x000023C2
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}
	}
}
