using System;
using System.ComponentModel.Design;

namespace AjaxControlToolkit
{
	// Token: 0x02000182 RID: 386
	public class OverlayCollectionEditor : CollectionEditor
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001BC18 File Offset: 0x00019E18
		public OverlayCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001BC21 File Offset: 0x00019E21
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001BC24 File Offset: 0x00019E24
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[]
			{
				typeof(SeadragonFixedOverlay),
				typeof(SeadragonScalableOverlay)
			};
		}
	}
}
