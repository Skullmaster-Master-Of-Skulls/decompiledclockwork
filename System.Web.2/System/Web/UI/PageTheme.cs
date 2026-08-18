using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.Util;
using System.Xml;

namespace System.Web.UI
{
	// Token: 0x020002E0 RID: 736
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public abstract class PageTheme
	{
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x0600223E RID: 8766
		protected abstract string[] LinkedStyleSheets { get; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x0600223F RID: 8767
		protected abstract IDictionary ControlSkins { get; }

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002240 RID: 8768
		protected abstract string AppRelativeTemplateSourceDirectory { get; }

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x000700CB File Offset: 0x0006E2CB
		protected Page Page
		{
			get
			{
				return this._page;
			}
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000700D3 File Offset: 0x0006E2D3
		internal void Initialize(Page page, bool styleSheetTheme)
		{
			this._page = page;
			this._styleSheetTheme = styleSheetTheme;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000700E3 File Offset: 0x0006E2E3
		protected object Eval(string expression)
		{
			return this.Page.Eval(expression);
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000700F1 File Offset: 0x0006E2F1
		protected string Eval(string expression, string format)
		{
			return this.Page.Eval(expression, format);
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x00070100 File Offset: 0x0006E300
		public static object CreateSkinKey(Type controlType, string skinID)
		{
			if (controlType == null)
			{
				throw new ArgumentNullException("controlType");
			}
			return new PageTheme.SkinKey(controlType.ToString(), skinID);
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x00070124 File Offset: 0x0006E324
		internal void ApplyControlSkin(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			string skinID = control.SkinID;
			ControlSkin controlSkin = (ControlSkin)this.ControlSkins[PageTheme.CreateSkinKey(control.GetType(), skinID)];
			if (controlSkin != null)
			{
				controlSkin.ApplySkin(control);
			}
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x00070170 File Offset: 0x0006E370
		internal void SetStyleSheet()
		{
			if (this.LinkedStyleSheets != null && this.LinkedStyleSheets.Length != 0)
			{
				if (this.Page.Header == null)
				{
					throw new InvalidOperationException(SR.GetString("Page_theme_requires_page_header"));
				}
				int num = 0;
				foreach (string href in this.LinkedStyleSheets)
				{
					HtmlLink htmlLink = new HtmlLink();
					htmlLink.Href = href;
					htmlLink.Attributes["type"] = "text/css";
					htmlLink.Attributes["rel"] = "stylesheet";
					if (this._styleSheetTheme)
					{
						this.Page.Header.Controls.AddAt(num++, htmlLink);
					}
					else
					{
						this.Page.Header.Controls.Add(htmlLink);
					}
				}
			}
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0007024A File Offset: 0x0006E44A
		public bool TestDeviceFilter(string deviceFilterName)
		{
			return this.Page.TestDeviceFilter(deviceFilterName);
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x00070258 File Offset: 0x0006E458
		protected object XPath(string xPathExpression)
		{
			return this.Page.XPath(xPathExpression);
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00070266 File Offset: 0x0006E466
		protected object XPath(string xPathExpression, IXmlNamespaceResolver resolver)
		{
			return this.Page.XPath(xPathExpression, resolver);
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00070275 File Offset: 0x0006E475
		protected string XPath(string xPathExpression, string format)
		{
			return this.Page.XPath(xPathExpression, format);
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x00070284 File Offset: 0x0006E484
		protected string XPath(string xPathExpression, string format, IXmlNamespaceResolver resolver)
		{
			return this.Page.XPath(xPathExpression, format, resolver);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x00070294 File Offset: 0x0006E494
		protected IEnumerable XPathSelect(string xPathExpression)
		{
			return this.Page.XPathSelect(xPathExpression);
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000702A2 File Offset: 0x0006E4A2
		protected IEnumerable XPathSelect(string xPathExpression, IXmlNamespaceResolver resolver)
		{
			return this.Page.XPathSelect(xPathExpression, resolver);
		}

		// Token: 0x04001C31 RID: 7217
		private Page _page;

		// Token: 0x04001C32 RID: 7218
		private bool _styleSheetTheme;

		// Token: 0x02000980 RID: 2432
		private class SkinKey
		{
			// Token: 0x06006A37 RID: 27191 RVA: 0x0017B74E File Offset: 0x0017994E
			internal SkinKey(string typeName, string skinID)
			{
				this._typeName = typeName;
				if (string.IsNullOrEmpty(skinID))
				{
					this._skinID = null;
					return;
				}
				this._skinID = skinID.ToLower(CultureInfo.InvariantCulture);
			}

			// Token: 0x06006A38 RID: 27192 RVA: 0x0017B77E File Offset: 0x0017997E
			public override int GetHashCode()
			{
				if (this._skinID == null)
				{
					return this._typeName.GetHashCode();
				}
				return HashCodeCombiner.CombineHashCodes(this._typeName.GetHashCode(), this._skinID.GetHashCode());
			}

			// Token: 0x06006A39 RID: 27193 RVA: 0x0017B7B0 File Offset: 0x001799B0
			public override bool Equals(object o)
			{
				PageTheme.SkinKey skinKey = (PageTheme.SkinKey)o;
				return this._typeName == skinKey._typeName && this._skinID == skinKey._skinID;
			}

			// Token: 0x040038B8 RID: 14520
			private string _skinID;

			// Token: 0x040038B9 RID: 14521
			private string _typeName;
		}
	}
}
