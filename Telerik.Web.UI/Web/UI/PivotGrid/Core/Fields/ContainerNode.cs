using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CA6 RID: 3238
	public class ContainerNode
	{
		// Token: 0x06007976 RID: 31094 RVA: 0x001BE90B File Offset: 0x001BCB0B
		public ContainerNode(string name, string caption, ContainerNodeRole role)
		{
			this.children = new List<ContainerNode>();
			this.Caption = caption;
			this.Role = role;
			this.Name = name;
		}

		// Token: 0x06007977 RID: 31095 RVA: 0x001BE933 File Offset: 0x001BCB33
		public ContainerNode(string caption, ContainerNodeRole role) : this(caption, caption, role)
		{
		}

		// Token: 0x17002727 RID: 10023
		// (get) Token: 0x06007978 RID: 31096 RVA: 0x001BE93E File Offset: 0x001BCB3E
		// (set) Token: 0x06007979 RID: 31097 RVA: 0x001BE946 File Offset: 0x001BCB46
		public string Name { get; private set; }

		// Token: 0x17002728 RID: 10024
		// (get) Token: 0x0600797A RID: 31098 RVA: 0x001BE94F File Offset: 0x001BCB4F
		// (set) Token: 0x0600797B RID: 31099 RVA: 0x001BE957 File Offset: 0x001BCB57
		public string Caption { get; private set; }

		// Token: 0x17002729 RID: 10025
		// (get) Token: 0x0600797C RID: 31100 RVA: 0x001BE960 File Offset: 0x001BCB60
		public IList<ContainerNode> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x1700272A RID: 10026
		// (get) Token: 0x0600797D RID: 31101 RVA: 0x001BE968 File Offset: 0x001BCB68
		public virtual bool HasChildren
		{
			get
			{
				return this.children.Count > 0;
			}
		}

		// Token: 0x1700272B RID: 10027
		// (get) Token: 0x0600797E RID: 31102 RVA: 0x001BE978 File Offset: 0x001BCB78
		// (set) Token: 0x0600797F RID: 31103 RVA: 0x001BE980 File Offset: 0x001BCB80
		public ContainerNodeRole Role { get; protected internal set; }

		// Token: 0x06007980 RID: 31104 RVA: 0x001BE98C File Offset: 0x001BCB8C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNode.#ctor(System.String,System.String,Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNodeRole)", Justification = "Will fix in the future.")]
		internal static ContainerNode CreateRootNode()
		{
			return new ContainerNode("Root", "Root", ContainerNodeRole.None);
		}

		// Token: 0x0400212F RID: 8495
		private List<ContainerNode> children;
	}
}
