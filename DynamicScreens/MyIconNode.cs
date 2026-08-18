using System;
using System.Drawing;
using Aga.Controls.Tree;

namespace DynamicScreens
{
	// Token: 0x02000014 RID: 20
	public class MyIconNode : Node, IComparable
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00015D54 File Offset: 0x00014D54
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00015D6C File Offset: 0x00014D6C
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00015D78 File Offset: 0x00014D78
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00015D90 File Offset: 0x00014D90
		public Image Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00015D9C File Offset: 0x00014D9C
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00015DB4 File Offset: 0x00014DB4
		public DynamicControl DynamicControl
		{
			get
			{
				return this.dynamicControl;
			}
			set
			{
				this.dynamicControl = value;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00015DBE File Offset: 0x00014DBE
		public MyIconNode(string title, DynamicControl dynamicControl) : base(title)
		{
			this.dynamicControl = dynamicControl;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00015DD4 File Offset: 0x00014DD4
		public int CompareTo(object o)
		{
			int result;
			if (o is MyIconNode)
			{
				int num = this.index.CompareTo(((MyIconNode)o).Index);
				result = num;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x04000122 RID: 290
		private int index;

		// Token: 0x04000123 RID: 291
		private Image icon;

		// Token: 0x04000124 RID: 292
		private DynamicControl dynamicControl;
	}
}
