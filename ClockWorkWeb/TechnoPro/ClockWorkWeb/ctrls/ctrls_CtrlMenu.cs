using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;
using TechnoPro.Common.Unity.IoC;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls
{
	// Token: 0x02000122 RID: 290
	public class ctrls_CtrlMenu : UserControl
	{
		// Token: 0x06000874 RID: 2164 RVA: 0x0003C548 File Offset: 0x0003A748
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				bool flag2 = this.RadTabStrip1.Tabs.Count < 1;
				if (flag2)
				{
					this.DoInit(this.module);
				}
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0003C58E File Offset: 0x0003A78E
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x0003C596 File Offset: 0x0003A796
		private eClockWorkWebPageModule module { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0003C5A0 File Offset: 0x0003A7A0
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0003C5C8 File Offset: 0x0003A7C8
		public string Module
		{
			get
			{
				return this.module.ToString();
			}
			set
			{
				bool flag = value != null && Enum.IsDefined(typeof(eClockWorkWebPageModule), value);
				if (flag)
				{
					this.module = (eClockWorkWebPageModule)Enum.Parse(typeof(eClockWorkWebPageModule), value);
				}
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0003C60C File Offset: 0x0003A80C
		public void SetCurrentPage(eClockWorkWebPage webPage)
		{
			bool flag = this.RadTabStrip1.Tabs.Count < 1;
			if (flag)
			{
				this.DoInit(this.module);
			}
			string itemValue = webPage.ToString();
			RadTab radTab = this.RadTabStrip1.Tabs.FirstOrDefault((RadTab g) => g.Value == itemValue);
			bool flag2 = radTab != null;
			if (flag2)
			{
				radTab.Selected = true;
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0003C684 File Offset: 0x0003A884
		private void DoInit(eClockWorkWebPageModule clockWorkWebPageModule)
		{
			ClockWorkWebPageModuleAttribute attribute = ClockWorkWebPageModuleAttribute.GetAttribute(clockWorkWebPageModule);
			IList<ClockWorkWebPageAttribute> clockWorkWebPageAttributes = this.GetClockWorkWebPageAttributes(clockWorkWebPageModule);
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.GENERAL_HideAllSubmitCommentMenuItems);
			IList<char> list = new List<char>();
			foreach (ClockWorkWebPageAttribute clockWorkWebPageAttribute in clockWorkWebPageAttributes)
			{
				eClockWorkWebPage enumValue = clockWorkWebPageAttribute.EnumValue;
				bool flag = settingValue && clockWorkWebPageAttribute.IsSubmitCommentPage;
				if (!flag)
				{
					AddMenuItemEventArgs addMenuItemEventArgs = new AddMenuItemEventArgs
					{
						MenuItem = enumValue,
						MenuItemTitle = clockWorkWebPageAttribute.Title,
						NavigatePage = clockWorkWebPageAttribute.NavigatePage
					};
					this.FireOnBeforeAddMenuItem(addMenuItemEventArgs);
					bool flag2 = !addMenuItemEventArgs.AbortAddingMenuItem;
					if (flag2)
					{
						RadTab radTab = new RadTab(addMenuItemEventArgs.MenuItemTitle, enumValue.ToString());
						char accessString = this.GetAccessString(addMenuItemEventArgs.MenuItemTitle, ref list);
						bool flag3 = accessString != ' ';
						if (flag3)
						{
							radTab.AccessKey = accessString.ToString();
						}
						radTab.NavigateUrl = attribute.NavigateUrlWithTrailingSlash + addMenuItemEventArgs.NavigatePage;
						this.RadTabStrip1.Tabs.Add(radTab);
					}
				}
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600087B RID: 2171 RVA: 0x0003C7DC File Offset: 0x0003A9DC
		// (remove) Token: 0x0600087C RID: 2172 RVA: 0x0003C814 File Offset: 0x0003AA14
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<AddMenuItemEventArgs> OnBeforeAddMenuItem;

		// Token: 0x0600087D RID: 2173 RVA: 0x0003C84C File Offset: 0x0003AA4C
		private void FireOnBeforeAddMenuItem(AddMenuItemEventArgs e)
		{
			EventHandler<AddMenuItemEventArgs> onBeforeAddMenuItem = this.OnBeforeAddMenuItem;
			bool flag = onBeforeAddMenuItem != null;
			if (flag)
			{
				onBeforeAddMenuItem(this, e);
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0003C874 File Offset: 0x0003AA74
		private char GetAccessString(string s, ref IList<char> usedAccessKeys)
		{
			for (int i = 0; i < s.Length; i++)
			{
				char c = char.ToLower(s[i]);
				bool flag = !usedAccessKeys.Contains(c);
				if (flag)
				{
					usedAccessKeys.Add(c);
					return c;
				}
			}
			return ' ';
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0003C8CC File Offset: 0x0003AACC
		private IList<ClockWorkWebPageAttribute> GetClockWorkWebPageAttributes(eClockWorkWebPageModule clockWorkWebPageModule)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			Dictionary<eClockWorkWebPageModule, IList<ClockWorkWebPageAttribute>> dictionary = (Dictionary<eClockWorkWebPageModule, IList<ClockWorkWebPageAttribute>>)clientCache["clockWorkWebPageAttributeDictionary"];
			bool flag = dictionary == null;
			if (flag)
			{
				eClockWorkWebPage[] array = (eClockWorkWebPage[])Enum.GetValues(typeof(eClockWorkWebPage));
				dictionary = new Dictionary<eClockWorkWebPageModule, IList<ClockWorkWebPageAttribute>>();
				foreach (eClockWorkWebPage clockWorkWebPage in array)
				{
					ClockWorkWebPageAttribute attribute = ClockWorkWebPageAttribute.GetAttribute(clockWorkWebPage);
					bool flag2 = attribute != null && !attribute.IsHidden;
					if (flag2)
					{
						bool flag3 = dictionary.ContainsKey(attribute.Module);
						if (flag3)
						{
							dictionary[attribute.Module].Add(attribute);
						}
						else
						{
							dictionary.Add(attribute.Module, new List<ClockWorkWebPageAttribute>
							{
								attribute
							});
						}
					}
				}
				clientCache.Insert("clockWorkWebPageAttributeDictionary", dictionary, TimeSpan.FromMinutes(120.0));
			}
			IList<ClockWorkWebPageAttribute> result;
			if (!dictionary.ContainsKey(clockWorkWebPageModule))
			{
				IList<ClockWorkWebPageAttribute> list = new List<ClockWorkWebPageAttribute>();
				result = list;
			}
			else
			{
				result = dictionary[clockWorkWebPageModule];
			}
			return result;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0003C9DD File Offset: 0x0003ABDD
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.RadTabStrip1.CausesValidation = newCausesValidation;
		}

		// Token: 0x04000674 RID: 1652
		protected RadTabStrip RadTabStrip1;
	}
}
