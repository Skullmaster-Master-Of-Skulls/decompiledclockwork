using System;
using System.Design;

namespace System.Web.UI.Design
{
	// Token: 0x0200006C RID: 108
	public class TemplatedEditableDesignerRegion : EditableDesignerRegion
	{
		// Token: 0x06000346 RID: 838 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		public TemplatedEditableDesignerRegion(TemplateDefinition templateDefinition) : base(templateDefinition.Designer, templateDefinition.Name, templateDefinition.ServerControlsOnly)
		{
			this._templateDefinition = templateDefinition;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00011011 File Offset: 0x0000F211
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00011019 File Offset: 0x0000F219
		public virtual bool IsSingleInstanceTemplate
		{
			get
			{
				return this._isSingleInstance;
			}
			set
			{
				this._isSingleInstance = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00011022 File Offset: 0x0000F222
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0001102F File Offset: 0x0000F22F
		public override bool SupportsDataBinding
		{
			get
			{
				return this._templateDefinition.SupportsDataBinding;
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("TemplateEditableDesignerRegion_CannotSetSupportsDataBinding"));
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00011040 File Offset: 0x0000F240
		public TemplateDefinition TemplateDefinition
		{
			get
			{
				return this._templateDefinition;
			}
		}

		// Token: 0x0400016E RID: 366
		private TemplateDefinition _templateDefinition;

		// Token: 0x0400016F RID: 367
		private bool _isSingleInstance;
	}
}
