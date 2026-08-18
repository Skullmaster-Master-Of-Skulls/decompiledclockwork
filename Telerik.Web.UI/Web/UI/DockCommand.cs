using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000FAA RID: 4010
	public class DockCommand
	{
		// Token: 0x060099ED RID: 39405 RVA: 0x0022577E File Offset: 0x0022397E
		public DockCommand() : this("Telerik.Web.UI.DockCommand", "rdCustom", "Custom", "Custom", false)
		{
		}

		// Token: 0x060099EE RID: 39406 RVA: 0x0022579B File Offset: 0x0022399B
		protected DockCommand(string clientTypeName, string cssClass, string name, string text, bool autoPostBack)
		{
			this._clientTypeName = clientTypeName;
			this._cssClass = cssClass;
			this._name = name;
			this._text = text;
			this._autoPostBack = autoPostBack;
		}

		// Token: 0x170030B2 RID: 12466
		// (get) Token: 0x060099EF RID: 39407 RVA: 0x002257C8 File Offset: 0x002239C8
		// (set) Token: 0x060099F0 RID: 39408 RVA: 0x002257D0 File Offset: 0x002239D0
		[ClientPropertyName("clientTypeName")]
		public virtual string ClientTypeName
		{
			get
			{
				return this._clientTypeName;
			}
			set
			{
				this._clientTypeName = value;
			}
		}

		// Token: 0x170030B3 RID: 12467
		// (get) Token: 0x060099F1 RID: 39409 RVA: 0x002257D9 File Offset: 0x002239D9
		// (set) Token: 0x060099F2 RID: 39410 RVA: 0x002257E1 File Offset: 0x002239E1
		[DefaultValue("Custom")]
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170030B4 RID: 12468
		// (get) Token: 0x060099F3 RID: 39411 RVA: 0x002257EA File Offset: 0x002239EA
		// (set) Token: 0x060099F4 RID: 39412 RVA: 0x002257F2 File Offset: 0x002239F2
		[DefaultValue("Custom")]
		public virtual string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x170030B5 RID: 12469
		// (get) Token: 0x060099F5 RID: 39413 RVA: 0x002257FB File Offset: 0x002239FB
		// (set) Token: 0x060099F6 RID: 39414 RVA: 0x00225803 File Offset: 0x00223A03
		[DefaultValue(false)]
		public virtual bool AutoPostBack
		{
			get
			{
				return this._autoPostBack;
			}
			set
			{
				this._autoPostBack = value;
			}
		}

		// Token: 0x170030B6 RID: 12470
		// (get) Token: 0x060099F7 RID: 39415 RVA: 0x0022580C File Offset: 0x00223A0C
		// (set) Token: 0x060099F8 RID: 39416 RVA: 0x00225814 File Offset: 0x00223A14
		[ClientPropertyName("shortCut")]
		[DefaultValue("")]
		public virtual string ShortCut
		{
			get
			{
				return this._shortCut;
			}
			set
			{
				this._shortCut = value;
			}
		}

		// Token: 0x170030B7 RID: 12471
		// (get) Token: 0x060099F9 RID: 39417 RVA: 0x0022581D File Offset: 0x00223A1D
		// (set) Token: 0x060099FA RID: 39418 RVA: 0x00225825 File Offset: 0x00223A25
		[DefaultValue("")]
		[ClientPropertyName("command")]
		public virtual string OnClientCommand
		{
			get
			{
				return this._onClientCommand;
			}
			set
			{
				this._onClientCommand = value;
			}
		}

		// Token: 0x170030B8 RID: 12472
		// (get) Token: 0x060099FB RID: 39419 RVA: 0x0022582E File Offset: 0x00223A2E
		// (set) Token: 0x060099FC RID: 39420 RVA: 0x00225836 File Offset: 0x00223A36
		[DefaultValue("rdCustom")]
		public virtual string CssClass
		{
			get
			{
				return this._cssClass;
			}
			set
			{
				this._cssClass = value;
			}
		}

		// Token: 0x170030B9 RID: 12473
		// (get) Token: 0x060099FD RID: 39421 RVA: 0x0022583F File Offset: 0x00223A3F
		// (set) Token: 0x060099FE RID: 39422 RVA: 0x00225847 File Offset: 0x00223A47
		[ScriptIgnore]
		internal RadDock RadDock
		{
			get
			{
				return this._radDock;
			}
			set
			{
				this._radDock = value;
			}
		}

		// Token: 0x060099FF RID: 39423 RVA: 0x00225850 File Offset: 0x00223A50
		public virtual Control CreateElement()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("li");
			if (this.RadDock.ResolvedRenderMode == RenderMode.Classic)
			{
				HtmlAnchor htmlAnchor = new HtmlAnchor();
				htmlAnchor.Title = this.GetText();
				htmlGenericControl.Controls.Add(htmlAnchor);
				HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
				htmlGenericControl2.InnerHtml = "&nbsp;";
				htmlGenericControl2.Attributes["class"] = this.GetCssClass();
				htmlAnchor.Controls.Add(htmlGenericControl2);
			}
			else
			{
				htmlGenericControl.Attributes["class"] = "rdListItem";
				HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("span");
				htmlGenericControl3.Attributes["title"] = this.GetText();
				htmlGenericControl3.Attributes["class"] = string.Format("rdCommandButton {0}", this.GetCssClass());
				htmlGenericControl3.InnerHtml = this.GetText();
				htmlGenericControl.Controls.Add(htmlGenericControl3);
			}
			return htmlGenericControl;
		}

		// Token: 0x06009A00 RID: 39424 RVA: 0x0022593D File Offset: 0x00223B3D
		protected virtual string GetCssClass()
		{
			return this.CssClass;
		}

		// Token: 0x06009A01 RID: 39425 RVA: 0x00225945 File Offset: 0x00223B45
		protected virtual string GetText()
		{
			return this.Text;
		}

		// Token: 0x06009A02 RID: 39426 RVA: 0x00225950 File Offset: 0x00223B50
		internal Dictionary<string, object> GetProperties()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Type type = base.GetType();
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (propertyInfo.GetCustomAttributes(typeof(ScriptIgnoreAttribute), true).Length <= 0)
				{
					object value = propertyInfo.GetValue(this, null);
					if (value != null)
					{
						DefaultValueAttribute[] array = (DefaultValueAttribute[])propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), true);
						if (array.Length <= 0 || !value.Equals(array[0].Value))
						{
							ClientPropertyNameAttribute[] array2 = (ClientPropertyNameAttribute[])propertyInfo.GetCustomAttributes(typeof(ClientPropertyNameAttribute), true);
							string key = string.Empty;
							if (array2.Length == 0)
							{
								key = propertyInfo.Name[0].ToString().ToLower() + propertyInfo.Name.Substring(1);
							}
							else
							{
								key = array2[0].PropertyName;
							}
							dictionary[key] = value;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x04002BAF RID: 11183
		private string _shortCut;

		// Token: 0x04002BB0 RID: 11184
		private string _clientTypeName;

		// Token: 0x04002BB1 RID: 11185
		private string _onClientCommand;

		// Token: 0x04002BB2 RID: 11186
		private string _cssClass;

		// Token: 0x04002BB3 RID: 11187
		private string _name;

		// Token: 0x04002BB4 RID: 11188
		private string _text;

		// Token: 0x04002BB5 RID: 11189
		private bool _autoPostBack;

		// Token: 0x04002BB6 RID: 11190
		private RadDock _radDock;
	}
}
