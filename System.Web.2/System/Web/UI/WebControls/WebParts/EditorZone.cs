using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200053B RID: 1339
	[Designer("System.Web.UI.Design.WebControls.WebParts.EditorZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class EditorZone : EditorZoneBase
	{
		// Token: 0x06004459 RID: 17497 RVA: 0x000E257C File Offset: 0x000E077C
		protected override EditorPartCollection CreateEditorParts()
		{
			EditorPartCollection editorPartCollection = new EditorPartCollection();
			if (this._zoneTemplate != null)
			{
				Control control = new NonParentingControl();
				this._zoneTemplate.InstantiateIn(control);
				if (control.HasControls())
				{
					foreach (object obj in control.Controls)
					{
						Control control2 = (Control)obj;
						EditorPart editorPart = control2 as EditorPart;
						if (editorPart != null)
						{
							editorPartCollection.Add(editorPart);
						}
						else
						{
							LiteralControl literalControl = control2 as LiteralControl;
							if ((literalControl == null || literalControl.Text.Trim().Length != 0) && !base.DesignMode)
							{
								throw new InvalidOperationException(SR.GetString("EditorZone_OnlyEditorParts", new object[]
								{
									this.ID
								}));
							}
						}
					}
				}
			}
			return editorPartCollection;
		}

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x000E2660 File Offset: 0x000E0860
		// (set) Token: 0x0600445B RID: 17499 RVA: 0x000E2668 File Offset: 0x000E0868
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(EditorZone))]
		[TemplateInstance(TemplateInstance.Single)]
		public virtual ITemplate ZoneTemplate
		{
			get
			{
				return this._zoneTemplate;
			}
			set
			{
				base.InvalidateEditorParts();
				this._zoneTemplate = value;
			}
		}

		// Token: 0x04002628 RID: 9768
		private ITemplate _zoneTemplate;
	}
}
