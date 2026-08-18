using System;
using System.Design;
using System.Reflection;

namespace System.ComponentModel.Design
{
	// Token: 0x020001A2 RID: 418
	public class DesignerActionMethodItem : DesignerActionItem
	{
		// Token: 0x06000F54 RID: 3924 RVA: 0x00057A99 File Offset: 0x00055C99
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category, string description, bool includeAsDesignerVerb) : base(displayName, category, description)
		{
			this.actionList = actionList;
			this.memberName = memberName;
			this.includeAsDesignerVerb = includeAsDesignerVerb;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00057ABC File Offset: 0x00055CBC
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName) : this(actionList, memberName, displayName, null, null, false)
		{
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00057ACA File Offset: 0x00055CCA
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, bool includeAsDesignerVerb) : this(actionList, memberName, displayName, null, null, includeAsDesignerVerb)
		{
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00057AD9 File Offset: 0x00055CD9
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category) : this(actionList, memberName, displayName, category, null, false)
		{
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00057AE8 File Offset: 0x00055CE8
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category, bool includeAsDesignerVerb) : this(actionList, memberName, displayName, category, null, includeAsDesignerVerb)
		{
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00057AF8 File Offset: 0x00055CF8
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category, string description) : this(actionList, memberName, displayName, category, description, false)
		{
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00057B08 File Offset: 0x00055D08
		internal DesignerActionMethodItem()
		{
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x00057B10 File Offset: 0x00055D10
		public virtual string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00057B18 File Offset: 0x00055D18
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x00057B20 File Offset: 0x00055D20
		public IComponent RelatedComponent
		{
			get
			{
				return this.relatedComponent;
			}
			set
			{
				this.relatedComponent = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00057B29 File Offset: 0x00055D29
		public virtual bool IncludeAsDesignerVerb
		{
			get
			{
				return this.includeAsDesignerVerb;
			}
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00057B31 File Offset: 0x00055D31
		internal void Invoke(object sender, EventArgs args)
		{
			this.Invoke();
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00057B3C File Offset: 0x00055D3C
		public virtual void Invoke()
		{
			if (this.methodInfo == null)
			{
				this.methodInfo = this.actionList.GetType().GetMethod(this.memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}
			if (this.methodInfo != null)
			{
				this.methodInfo.Invoke(this.actionList, null);
				return;
			}
			throw new InvalidOperationException(SR.GetString("DesignerActionPanel_CouldNotFindMethod", new object[]
			{
				this.MemberName
			}));
		}

		// Token: 0x040008F1 RID: 2289
		private string memberName;

		// Token: 0x040008F2 RID: 2290
		private bool includeAsDesignerVerb;

		// Token: 0x040008F3 RID: 2291
		private DesignerActionList actionList;

		// Token: 0x040008F4 RID: 2292
		private MethodInfo methodInfo;

		// Token: 0x040008F5 RID: 2293
		private IComponent relatedComponent;
	}
}
