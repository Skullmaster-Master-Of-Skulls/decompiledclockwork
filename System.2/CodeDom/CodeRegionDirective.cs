using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200064F RID: 1615
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeRegionDirective : CodeDirective
	{
		// Token: 0x06003AB3 RID: 15027 RVA: 0x000F4840 File Offset: 0x000F2A40
		public CodeRegionDirective()
		{
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000F4848 File Offset: 0x000F2A48
		public CodeRegionDirective(CodeRegionMode regionMode, string regionText)
		{
			this.RegionText = regionText;
			this.regionMode = regionMode;
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x000F485E File Offset: 0x000F2A5E
		// (set) Token: 0x06003AB6 RID: 15030 RVA: 0x000F4874 File Offset: 0x000F2A74
		public string RegionText
		{
			get
			{
				if (this.regionText != null)
				{
					return this.regionText;
				}
				return string.Empty;
			}
			set
			{
				this.regionText = value;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x000F487D File Offset: 0x000F2A7D
		// (set) Token: 0x06003AB8 RID: 15032 RVA: 0x000F4885 File Offset: 0x000F2A85
		public CodeRegionMode RegionMode
		{
			get
			{
				return this.regionMode;
			}
			set
			{
				this.regionMode = value;
			}
		}

		// Token: 0x04002C17 RID: 11287
		private string regionText;

		// Token: 0x04002C18 RID: 11288
		private CodeRegionMode regionMode;
	}
}
