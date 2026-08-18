using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020001AC RID: 428
	internal class DesignerActionVerbItem : DesignerActionMethodItem
	{
		// Token: 0x06000FBB RID: 4027 RVA: 0x00059CA8 File Offset: 0x00057EA8
		public DesignerActionVerbItem(DesignerVerb verb)
		{
			if (verb == null)
			{
				throw new ArgumentNullException();
			}
			this._targetVerb = verb;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00059CC0 File Offset: 0x00057EC0
		public override string Category
		{
			get
			{
				return "Verbs";
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00059CC7 File Offset: 0x00057EC7
		public override string Description
		{
			get
			{
				return this._targetVerb.Description;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x00059CD4 File Offset: 0x00057ED4
		public override string DisplayName
		{
			get
			{
				return this._targetVerb.Text;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00003598 File Offset: 0x00001798
		public override string MemberName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool IncludeAsDesignerVerb
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00059CE1 File Offset: 0x00057EE1
		public override void Invoke()
		{
			this._targetVerb.Invoke();
		}

		// Token: 0x04000933 RID: 2355
		private DesignerVerb _targetVerb;
	}
}
