using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	// Token: 0x02000276 RID: 630
	[ProvideProperty("HelpString", typeof(Control))]
	[ProvideProperty("HelpKeyword", typeof(Control))]
	[ProvideProperty("HelpNavigator", typeof(Control))]
	[ProvideProperty("ShowHelp", typeof(Control))]
	[ToolboxItemFilter("System.Windows.Forms")]
	[SRDescription("DescriptionHelpProvider")]
	public class HelpProvider : Component, IExtenderProvider
	{
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x000BAB41 File Offset: 0x000B8D41
		// (set) Token: 0x06002823 RID: 10275 RVA: 0x000BAB49 File Offset: 0x000B8D49
		[Localizable(true)]
		[DefaultValue(null)]
		[Editor("System.Windows.Forms.Design.HelpNamespaceEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("HelpProviderHelpNamespaceDescr")]
		public virtual string HelpNamespace
		{
			get
			{
				return this.helpNamespace;
			}
			set
			{
				this.helpNamespace = value;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x000BAB52 File Offset: 0x000B8D52
		// (set) Token: 0x06002825 RID: 10277 RVA: 0x000BAB5A File Offset: 0x000B8D5A
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000BAB63 File Offset: 0x000B8D63
		public virtual bool CanExtend(object target)
		{
			return target is Control;
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000BAB6E File Offset: 0x000B8D6E
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("HelpProviderHelpKeywordDescr")]
		public virtual string GetHelpKeyword(Control ctl)
		{
			return (string)this.keywords[ctl];
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000BAB84 File Offset: 0x000B8D84
		[DefaultValue(HelpNavigator.AssociateIndex)]
		[Localizable(true)]
		[SRDescription("HelpProviderNavigatorDescr")]
		public virtual HelpNavigator GetHelpNavigator(Control ctl)
		{
			object obj = this.navigators[ctl];
			if (obj != null)
			{
				return (HelpNavigator)obj;
			}
			return HelpNavigator.AssociateIndex;
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x000BABAD File Offset: 0x000B8DAD
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("HelpProviderHelpStringDescr")]
		public virtual string GetHelpString(Control ctl)
		{
			return (string)this.helpStrings[ctl];
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000BABC0 File Offset: 0x000B8DC0
		[Localizable(true)]
		[SRDescription("HelpProviderShowHelpDescr")]
		public virtual bool GetShowHelp(Control ctl)
		{
			object obj = this.showHelp[ctl];
			return obj != null && (bool)obj;
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000BABE8 File Offset: 0x000B8DE8
		private void OnControlHelp(object sender, HelpEventArgs hevent)
		{
			Control control = (Control)sender;
			string helpString = this.GetHelpString(control);
			string helpKeyword = this.GetHelpKeyword(control);
			HelpNavigator helpNavigator = this.GetHelpNavigator(control);
			if (!this.GetShowHelp(control))
			{
				return;
			}
			if (Control.MouseButtons != MouseButtons.None && helpString != null && helpString.Length > 0)
			{
				Help.ShowPopup(control, helpString, hevent.MousePos);
				hevent.Handled = true;
			}
			if (!hevent.Handled && this.helpNamespace != null)
			{
				if (helpKeyword != null && helpKeyword.Length > 0)
				{
					Help.ShowHelp(control, this.helpNamespace, helpNavigator, helpKeyword);
					hevent.Handled = true;
				}
				if (!hevent.Handled)
				{
					Help.ShowHelp(control, this.helpNamespace, helpNavigator);
					hevent.Handled = true;
				}
			}
			if (!hevent.Handled && helpString != null && helpString.Length > 0)
			{
				Help.ShowPopup(control, helpString, hevent.MousePos);
				hevent.Handled = true;
			}
			if (!hevent.Handled && this.helpNamespace != null)
			{
				Help.ShowHelp(control, this.helpNamespace);
				hevent.Handled = true;
			}
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000BACE4 File Offset: 0x000B8EE4
		private void OnQueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
		{
			Control ctl = (Control)sender;
			e.HelpString = this.GetHelpString(ctl);
			e.HelpKeyword = this.GetHelpKeyword(ctl);
			e.HelpNamespace = this.HelpNamespace;
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000BAD1E File Offset: 0x000B8F1E
		public virtual void SetHelpString(Control ctl, string helpString)
		{
			this.helpStrings[ctl] = helpString;
			if (helpString != null && helpString.Length > 0)
			{
				this.SetShowHelp(ctl, true);
			}
			this.UpdateEventBinding(ctl);
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000BAD48 File Offset: 0x000B8F48
		public virtual void SetHelpKeyword(Control ctl, string keyword)
		{
			this.keywords[ctl] = keyword;
			if (keyword != null && keyword.Length > 0)
			{
				this.SetShowHelp(ctl, true);
			}
			this.UpdateEventBinding(ctl);
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000BAD74 File Offset: 0x000B8F74
		public virtual void SetHelpNavigator(Control ctl, HelpNavigator navigator)
		{
			if (!ClientUtils.IsEnumValid(navigator, (int)navigator, -2147483647, -2147483641))
			{
				throw new InvalidEnumArgumentException("navigator", (int)navigator, typeof(HelpNavigator));
			}
			this.navigators[ctl] = navigator;
			this.SetShowHelp(ctl, true);
			this.UpdateEventBinding(ctl);
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000BADD0 File Offset: 0x000B8FD0
		public virtual void SetShowHelp(Control ctl, bool value)
		{
			this.showHelp[ctl] = value;
			this.UpdateEventBinding(ctl);
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000BADEB File Offset: 0x000B8FEB
		internal virtual bool ShouldSerializeShowHelp(Control ctl)
		{
			return this.showHelp.ContainsKey(ctl);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000BADF9 File Offset: 0x000B8FF9
		public virtual void ResetShowHelp(Control ctl)
		{
			this.showHelp.Remove(ctl);
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000BAE08 File Offset: 0x000B9008
		private void UpdateEventBinding(Control ctl)
		{
			if (this.GetShowHelp(ctl) && !this.boundControls.ContainsKey(ctl))
			{
				ctl.HelpRequested += this.OnControlHelp;
				ctl.QueryAccessibilityHelp += this.OnQueryAccessibilityHelp;
				this.boundControls[ctl] = ctl;
				return;
			}
			if (!this.GetShowHelp(ctl) && this.boundControls.ContainsKey(ctl))
			{
				ctl.HelpRequested -= this.OnControlHelp;
				ctl.QueryAccessibilityHelp -= this.OnQueryAccessibilityHelp;
				this.boundControls.Remove(ctl);
			}
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000BAEA8 File Offset: 0x000B90A8
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", HelpNamespace: " + this.HelpNamespace;
		}

		// Token: 0x0400109D RID: 4253
		private string helpNamespace;

		// Token: 0x0400109E RID: 4254
		private Hashtable helpStrings = new Hashtable();

		// Token: 0x0400109F RID: 4255
		private Hashtable showHelp = new Hashtable();

		// Token: 0x040010A0 RID: 4256
		private Hashtable boundControls = new Hashtable();

		// Token: 0x040010A1 RID: 4257
		private Hashtable keywords = new Hashtable();

		// Token: 0x040010A2 RID: 4258
		private Hashtable navigators = new Hashtable();

		// Token: 0x040010A3 RID: 4259
		private object userData;
	}
}
