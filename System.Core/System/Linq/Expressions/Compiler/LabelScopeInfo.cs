using System;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x02000282 RID: 642
	internal sealed class LabelScopeInfo
	{
		// Token: 0x060016FA RID: 5882 RVA: 0x0004D49D File Offset: 0x0004B69D
		internal LabelScopeInfo(LabelScopeInfo parent, LabelScopeKind kind)
		{
			this.Parent = parent;
			this.Kind = kind;
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x0004D4B4 File Offset: 0x0004B6B4
		internal bool CanJumpInto
		{
			get
			{
				LabelScopeKind kind = this.Kind;
				return kind <= LabelScopeKind.Lambda;
			}
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0004D4CF File Offset: 0x0004B6CF
		internal bool ContainsTarget(LabelTarget target)
		{
			return this.Labels != null && this.Labels.ContainsKey(target);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0004D4E7 File Offset: 0x0004B6E7
		internal bool TryGetLabelInfo(LabelTarget target, out LabelInfo info)
		{
			if (this.Labels == null)
			{
				info = null;
				return false;
			}
			return this.Labels.TryGetValue(target, out info);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0004D503 File Offset: 0x0004B703
		internal void AddLabelInfo(LabelTarget target, LabelInfo info)
		{
			if (this.Labels == null)
			{
				this.Labels = new Dictionary<LabelTarget, LabelInfo>();
			}
			this.Labels.Add(target, info);
		}

		// Token: 0x04000B60 RID: 2912
		private Dictionary<LabelTarget, LabelInfo> Labels;

		// Token: 0x04000B61 RID: 2913
		internal readonly LabelScopeKind Kind;

		// Token: 0x04000B62 RID: 2914
		internal readonly LabelScopeInfo Parent;
	}
}
