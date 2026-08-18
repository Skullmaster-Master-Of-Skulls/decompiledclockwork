using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x020001B9 RID: 441
	[Designer(typeof(UpdatePanelAnimationExtenderDesigner))]
	[RequiredScript(typeof(CommonToolkitScripts), 0)]
	[RequiredScript(typeof(AnimationScripts), 1)]
	[RequiredScript(typeof(AnimationExtender), 2)]
	[ClientScriptResource("Sys.Extended.UI.Animation.UpdatePanelAnimationBehavior", "UpdatePanelAnimation")]
	[TargetControlType(typeof(UpdatePanel))]
	[ToolboxBitmap(typeof(Accessor), "UpdatePanelAnimation.bmp")]
	public class UpdatePanelAnimationExtender : AnimationExtenderControlBase
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0002271E File Offset: 0x0002091E
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x00022731 File Offset: 0x00020931
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientPropertyName("onUpdating")]
		[Browsable(false)]
		[ExtenderControlProperty]
		public Animation OnUpdating
		{
			get
			{
				return base.GetAnimation(ref this._updating, "OnUpdating");
			}
			set
			{
				base.SetAnimation(ref this._updating, "OnUpdating", value);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00022745 File Offset: 0x00020945
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x00022758 File Offset: 0x00020958
		[ClientPropertyName("onUpdated")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ExtenderControlProperty]
		[DefaultValue(null)]
		[Browsable(false)]
		public Animation OnUpdated
		{
			get
			{
				return base.GetAnimation(ref this._updated, "OnUpdated");
			}
			set
			{
				base.SetAnimation(ref this._updated, "OnUpdated", value);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0002276C File Offset: 0x0002096C
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x0002277A File Offset: 0x0002097A
		[ClientPropertyName("alwaysFinishOnUpdatingAnimation")]
		[Browsable(true)]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool AlwaysFinishOnUpdatingAnimation
		{
			get
			{
				return base.GetPropertyValue<bool>("AlwaysFinishOnUpdatingAnimation", false);
			}
			set
			{
				base.SetPropertyValue<bool>("AlwaysFinishOnUpdatingAnimation", value);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00022788 File Offset: 0x00020988
		[Browsable(false)]
		[ExtenderControlProperty(true, true)]
		[ClientPropertyName("triggerControlsClientID")]
		[DefaultValue(null)]
		public string[] TriggerControlsClientID
		{
			get
			{
				return this._triggerControlsClientID.ToArray();
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00022798 File Offset: 0x00020998
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.ResolveControlIDs(this._updating);
			base.ResolveControlIDs(this._updated);
			this.ReplaceStaticAnimationTargets(this._updating);
			this.ReplaceStaticAnimationTargets(this._updated);
			UpdatePanel updatePanel = base.TargetControl as UpdatePanel;
			foreach (UpdatePanelTrigger updatePanelTrigger in updatePanel.Triggers)
			{
				AsyncPostBackTrigger asyncPostBackTrigger = updatePanelTrigger as AsyncPostBackTrigger;
				if (asyncPostBackTrigger != null)
				{
					string clientID = this.FindControl(asyncPostBackTrigger.ControlID).ClientID;
					this._triggerControlsClientID.Add(clientID);
				}
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00022850 File Offset: 0x00020A50
		private void ReplaceStaticAnimationTargets(Animation animation)
		{
			if (animation == null)
			{
				return;
			}
			string text;
			string value;
			if (animation.Properties.TryGetValue("AnimationTarget", out text) && !string.IsNullOrEmpty(text) && (!animation.Properties.TryGetValue("AnimationTargetScript", out value) || string.IsNullOrEmpty(value)) && (!animation.Properties.TryGetValue("TargetScript", out value) || string.IsNullOrEmpty(value)))
			{
				animation.Properties.Remove("AnimationTarget");
				animation.Properties["TargetScript"] = string.Format(CultureInfo.InvariantCulture, "$get('{0}')", new object[]
				{
					text
				});
			}
			foreach (Animation animation2 in animation.Children)
			{
				this.ReplaceStaticAnimationTargets(animation2);
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00022938 File Offset: 0x00020B38
		public Control GetTargetControl()
		{
			return base.TargetControl;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00022940 File Offset: 0x00020B40
		public Control GetControl(string id)
		{
			return this.FindControl(id);
		}

		// Token: 0x040004BB RID: 1211
		private Animation _updating;

		// Token: 0x040004BC RID: 1212
		private Animation _updated;

		// Token: 0x040004BD RID: 1213
		private List<string> _triggerControlsClientID = new List<string>();
	}
}
