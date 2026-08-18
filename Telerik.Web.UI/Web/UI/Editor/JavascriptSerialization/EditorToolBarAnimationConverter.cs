using System;
using System.Collections.Generic;
using Telerik.Web.UI.Editor.Animations;

namespace Telerik.Web.UI.Editor.JavascriptSerialization
{
	// Token: 0x020002BD RID: 701
	internal class EditorToolBarAnimationConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x000509C4 File Offset: 0x0004EBC4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(EditorToolbarAnimation)
				};
			}
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x000509E8 File Offset: 0x0004EBE8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			EditorToolbarAnimation editorToolbarAnimation = (EditorToolbarAnimation)obj;
			if (editorToolbarAnimation != null)
			{
				state.Add("type", editorToolbarAnimation.Type);
				state.Add("duration", editorToolbarAnimation.Duration);
			}
		}
	}
}
