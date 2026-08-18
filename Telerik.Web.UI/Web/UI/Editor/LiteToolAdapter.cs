using System;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002DE RID: 734
	internal class LiteToolAdapter : DefaultToolAdapter
	{
		// Token: 0x06001988 RID: 6536 RVA: 0x0005463A File Offset: 0x0005283A
		public LiteToolAdapter()
		{
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00054642 File Offset: 0x00052842
		public LiteToolAdapter(RadEditor editor) : base(editor)
		{
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x0005464B File Offset: 0x0005284B
		public override string ClientType
		{
			get
			{
				return "Telerik.Web.UI.Editor.LightweightToolAdapter";
			}
		}
	}
}
