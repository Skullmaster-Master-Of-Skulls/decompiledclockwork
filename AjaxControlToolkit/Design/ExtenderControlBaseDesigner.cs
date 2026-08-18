using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using System.Web.UI.Design;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000009 RID: 9
	public class ExtenderControlBaseDesigner<T> : ExtenderControlDesigner, IExtenderProvider where T : ExtenderControlBase
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003870 File Offset: 0x00001A70
		protected bool DesignerFeaturesEnabled
		{
			get
			{
				if (this._disableDesignerFeatures == 0)
				{
					this._disableDesignerFeatures = 2;
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						IComponent rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent.Site != null)
						{
							IDictionaryService dictionaryService = (IDictionaryService)rootComponent.Site.GetService(typeof(IDictionaryService));
							if (dictionaryService != null && dictionaryService.GetValue("ExtenderControlFeaturesPresent") != null)
							{
								this._disableDesignerFeatures = 1;
							}
						}
					}
				}
				return this._disableDesignerFeatures == 2;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000038F4 File Offset: 0x00001AF4
		public bool CanExtend(object extendee)
		{
			Control control = extendee as Control;
			bool flag = false;
			if (this.DesignerFeaturesEnabled && control != null)
			{
				string id = control.ID;
				T extenderControl = this.ExtenderControl;
				flag = (id == extenderControl.TargetControlID);
				if (flag && this._renameProvider == null)
				{
					this._renameProvider = new ExtenderPropertyRenameDescProv<T>(this, control);
					TypeDescriptor.AddProvider(this._renameProvider, control);
				}
			}
			return flag;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000395A File Offset: 0x00001B5A
		protected T ExtenderControl
		{
			get
			{
				return base.Component as T;
			}
		}

		// Token: 0x04000020 RID: 32
		private const int DisableDesignerFeaturesUnknown = 0;

		// Token: 0x04000021 RID: 33
		private const int DisableDesignerFeaturesYes = 1;

		// Token: 0x04000022 RID: 34
		private const int DisableDesignerFeaturesNo = 2;

		// Token: 0x04000023 RID: 35
		private const string ExtenderControlDictionaryKey = "ExtenderControlFeaturesPresent";

		// Token: 0x04000024 RID: 36
		private ExtenderPropertyRenameDescProv<T> _renameProvider;

		// Token: 0x04000025 RID: 37
		private int _disableDesignerFeatures;
	}
}
