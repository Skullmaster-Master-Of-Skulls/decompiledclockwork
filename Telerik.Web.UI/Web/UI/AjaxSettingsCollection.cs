using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000FCD RID: 4045
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class AjaxSettingsCollection : CollectionBase
	{
		// Token: 0x06009D0A RID: 40202 RVA: 0x0022F248 File Offset: 0x0022D448
		internal string SerializeToJavascript(RadAjaxManager manager)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			int count = base.Count;
			foreach (object obj in this)
			{
				AjaxSetting ajaxSetting = (AjaxSetting)obj;
				stringBuilder.AppendFormat("{0}", ajaxSetting.SerializeToJavascript(manager));
				if (count-- > 1)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06009D0B RID: 40203 RVA: 0x0022F2EC File Offset: 0x0022D4EC
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06009D0C RID: 40204 RVA: 0x0022F2F3 File Offset: 0x0022D4F3
		public void AddAjaxSetting(Control ajaxifiedControl, Control updatedControl)
		{
			this.AddAjaxSetting(ajaxifiedControl, updatedControl, null);
		}

		// Token: 0x06009D0D RID: 40205 RVA: 0x0022F2FE File Offset: 0x0022D4FE
		public void AddAjaxSetting(Control ajaxifiedControl, Control updatedControl, RadAjaxLoadingPanel loadingPanel)
		{
			this.AddAjaxSetting(ajaxifiedControl, updatedControl, loadingPanel, UpdatePanelRenderMode.Block);
		}

		// Token: 0x06009D0E RID: 40206 RVA: 0x0022F30A File Offset: 0x0022D50A
		public void AddAjaxSetting(Control ajaxifiedControl, Control updatedControl, RadAjaxLoadingPanel loadingPanel, UpdatePanelRenderMode renderMode)
		{
			this.AddAjaxSetting(ajaxifiedControl, updatedControl, loadingPanel, renderMode, Unit.Empty);
		}

		// Token: 0x06009D0F RID: 40207 RVA: 0x0022F31C File Offset: 0x0022D51C
		public void AddAjaxSetting(Control ajaxifiedControl, Control updatedControl, RadAjaxLoadingPanel loadingPanel, UpdatePanelRenderMode renderMode, Unit updatePanelHeight)
		{
			AjaxSetting ajaxSetting = new AjaxSetting();
			ajaxSetting.AjaxControlID = ajaxifiedControl.UniqueID;
			AjaxUpdatedControl ajaxUpdatedControl = new AjaxUpdatedControl();
			ajaxUpdatedControl.ControlID = updatedControl.UniqueID;
			ajaxUpdatedControl.UpdatePanelRenderMode = renderMode;
			ajaxUpdatedControl.UpdatePanelHeight = updatePanelHeight;
			if (loadingPanel != null)
			{
				ajaxUpdatedControl.LoadingPanelID = loadingPanel.UniqueID;
			}
			else
			{
				ajaxUpdatedControl.LoadingPanelID = string.Empty;
			}
			ajaxSetting.UpdatedControls.Add(ajaxUpdatedControl);
			RadAjaxManager.GetCurrent(ajaxifiedControl.Page).AddedSetting(ajaxifiedControl, updatedControl);
			this.Add(ajaxSetting);
		}

		// Token: 0x170031B8 RID: 12728
		public AjaxSetting this[int index]
		{
			get
			{
				return base.List[index] as AjaxSetting;
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06009D12 RID: 40210 RVA: 0x0022F3C2 File Offset: 0x0022D5C2
		public int Add(AjaxSetting ajaxSetting)
		{
			return base.List.Add(ajaxSetting);
		}

		// Token: 0x06009D13 RID: 40211 RVA: 0x0022F3D0 File Offset: 0x0022D5D0
		public void Remove(AjaxSetting ajaxSetting)
		{
			base.List.Remove(ajaxSetting);
		}

		// Token: 0x06009D14 RID: 40212 RVA: 0x0022F3DE File Offset: 0x0022D5DE
		public bool Contains(AjaxSetting ajaxSetting)
		{
			return base.List.Contains(ajaxSetting);
		}

		// Token: 0x06009D15 RID: 40213 RVA: 0x0022F3EC File Offset: 0x0022D5EC
		public int IndexOf(AjaxSetting ajaxSetting)
		{
			return base.List.IndexOf(ajaxSetting);
		}

		// Token: 0x06009D16 RID: 40214 RVA: 0x0022F3FA File Offset: 0x0022D5FA
		public void Insert(int index, AjaxSetting ajaxSetting)
		{
			base.List.Insert(index, ajaxSetting);
		}
	}
}
