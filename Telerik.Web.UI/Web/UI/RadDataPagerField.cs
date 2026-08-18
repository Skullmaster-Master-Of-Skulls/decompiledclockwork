using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.Routing;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02001952 RID: 6482
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public abstract class RadDataPagerField : StateManager
	{
		// Token: 0x0600FAB2 RID: 64178 RVA: 0x00386E4D File Offset: 0x0038504D
		public RadDataPagerField()
		{
		}

		// Token: 0x0600FAB3 RID: 64179 RVA: 0x00386E78 File Offset: 0x00385078
		internal void SetOwner(RadDataPager owner)
		{
			this._owner = owner;
		}

		// Token: 0x17004BC1 RID: 19393
		// (get) Token: 0x0600FAB4 RID: 64180 RVA: 0x00386E81 File Offset: 0x00385081
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadDataPager Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17004BC2 RID: 19394
		// (get) Token: 0x0600FAB5 RID: 64181 RVA: 0x00386E89 File Offset: 0x00385089
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string PagerType
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17004BC3 RID: 19395
		// (get) Token: 0x0600FAB6 RID: 64182 RVA: 0x00386E98 File Offset: 0x00385098
		// (set) Token: 0x0600FAB7 RID: 64183 RVA: 0x00386EC1 File Offset: 0x003850C1
		[NotifyParentProperty(true)]
		[DefaultValue(PagerFieldHorizontalPosition.LeftFloat)]
		public PagerFieldHorizontalPosition HorizontalPosition
		{
			get
			{
				object obj = base.ViewState["HorizontalPosition"];
				if (obj == null)
				{
					return PagerFieldHorizontalPosition.LeftFloat;
				}
				return (PagerFieldHorizontalPosition)obj;
			}
			set
			{
				base.ViewState["HorizontalPosition"] = value;
				this.OnFieldChanged();
			}
		}

		// Token: 0x0600FAB8 RID: 64184
		public abstract void InitializeFieldControls(RadDataPagerFieldItem inItem);

		// Token: 0x0600FAB9 RID: 64185 RVA: 0x00386EE0 File Offset: 0x003850E0
		protected virtual void PrepareSkinnableControlProperties(ISkinnableControl control)
		{
			control.EnableEmbeddedSkins = this.Owner.EnableEmbeddedSkins;
			control.EnableEmbeddedScripts = this.Owner.EnableEmbeddedScripts;
			control.EnableEmbeddedBaseStylesheet = this.Owner.EnableEmbeddedBaseStylesheet;
			control.RegisterWithScriptManager = this.Owner.RegisterWithScriptManager;
		}

		// Token: 0x0600FABA RID: 64186 RVA: 0x00386F31 File Offset: 0x00385131
		public override string ToString()
		{
			return this.PagerType;
		}

		// Token: 0x0600FABB RID: 64187 RVA: 0x00386F3C File Offset: 0x0038513C
		protected string SEOPagingLinkBuilder(string argument)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			if (argument != null)
			{
				if (argument == "First")
				{
					num = 0;
					goto IL_94;
				}
				if (argument == "Last")
				{
					num = this.Owner.PageCount - 1;
					goto IL_94;
				}
				if (argument == "Next")
				{
					num = this.Owner.CurrentPageIndex + 1;
					goto IL_94;
				}
				if (argument == "Prev")
				{
					num = ((this.Owner.CurrentPageIndex - 1 < 0) ? 0 : (this.Owner.CurrentPageIndex - 1));
					goto IL_94;
				}
			}
			if (!int.TryParse(argument, out num))
			{
				num = 0;
			}
			IL_94:
			num++;
			HttpRequest request = this.Owner.Page.Request;
			if (this.Owner.AllowRouting)
			{
				this.BuildRoutingNavigationURL(request, stringBuilder, num);
			}
			else
			{
				stringBuilder.Append(request.Path).Append("?");
				this.PrepareQueryString(request, stringBuilder);
				stringBuilder.AppendFormat("{0}={1}", this.Owner.SEOPagingQueryPageKey, this.GenerateNavigationValue(num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600FABC RID: 64188 RVA: 0x00387050 File Offset: 0x00385250
		private string GenerateNavigationValue(int navigationValue)
		{
			if (this.Owner.PageSize != this.Owner._defaultPageSize)
			{
				return string.Format("{0}_{1}", navigationValue, this.Owner.PageSize);
			}
			return navigationValue.ToString();
		}

		// Token: 0x0600FABD RID: 64189 RVA: 0x003870A0 File Offset: 0x003852A0
		protected void PrepareQueryString(HttpRequest request, StringBuilder navigationUrl)
		{
			foreach (string text in request.QueryString.AllKeys)
			{
				if (string.Compare(text, this.Owner.SEOPagingQueryPageKey, true) != 0)
				{
					navigationUrl.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(text), HttpUtility.UrlEncode(request.QueryString[text]));
				}
			}
		}

		// Token: 0x0600FABE RID: 64190 RVA: 0x00387104 File Offset: 0x00385304
		protected void BuildRoutingNavigationURL(HttpRequest request, StringBuilder navigationUrl, int navigationValue)
		{
			this.RouteDictionary[this.Owner.RoutePageIndexParameterName] = this.GenerateNavigationValue(navigationValue);
			VirtualPathData virtualPath;
			if (string.IsNullOrEmpty(this.Owner.RouteName))
			{
				virtualPath = RouteTable.Routes.GetVirtualPath(null, this.RouteDictionary);
			}
			else
			{
				virtualPath = RouteTable.Routes.GetVirtualPath(null, this.Owner.RouteName, this.RouteDictionary);
			}
			if (virtualPath == null)
			{
				return;
			}
			navigationUrl.Append(virtualPath.VirtualPath);
			bool flag = virtualPath.VirtualPath.IndexOf('?') != -1;
			bool flag2 = false;
			if (!flag)
			{
				navigationUrl.Append("?");
			}
			foreach (string text in request.QueryString.AllKeys)
			{
				if (!(text == this.Owner.RoutePageIndexParameterName))
				{
					flag2 = true;
					navigationUrl.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(text), HttpUtility.UrlEncode(request.QueryString[text]));
				}
			}
			if (!flag || flag2)
			{
				navigationUrl.Remove(navigationUrl.Length - 1, 1);
			}
		}

		// Token: 0x17004BC4 RID: 19396
		// (get) Token: 0x0600FABF RID: 64191 RVA: 0x00387220 File Offset: 0x00385420
		protected RouteValueDictionary RouteDictionary
		{
			get
			{
				if (this._dictionary == null)
				{
					this._dictionary = new RouteValueDictionary();
					foreach (KeyValuePair<string, object> keyValuePair in this.Owner.Page.RouteData.Values)
					{
						this._dictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				return this._dictionary;
			}
		}

		// Token: 0x140001D4 RID: 468
		// (add) Token: 0x0600FAC0 RID: 64192 RVA: 0x003872B0 File Offset: 0x003854B0
		// (remove) Token: 0x0600FAC1 RID: 64193 RVA: 0x003872E8 File Offset: 0x003854E8
		internal event EventHandler FieldChanged;

		// Token: 0x0600FAC2 RID: 64194 RVA: 0x0038731D File Offset: 0x0038551D
		protected void OnFieldChanged()
		{
			if (this.FieldChanged != null)
			{
				this.FieldChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x17004BC5 RID: 19397
		// (get) Token: 0x0600FAC3 RID: 64195 RVA: 0x00387338 File Offset: 0x00385538
		// (set) Token: 0x0600FAC4 RID: 64196 RVA: 0x00387361 File Offset: 0x00385561
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool Visible
		{
			get
			{
				object obj = base.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Visible"] = value;
				this.OnFieldChanged();
			}
		}

		// Token: 0x17004BC6 RID: 19398
		// (get) Token: 0x0600FAC5 RID: 64197 RVA: 0x00387380 File Offset: 0x00385580
		// (set) Token: 0x0600FAC6 RID: 64198 RVA: 0x003873A9 File Offset: 0x003855A9
		public bool HiddenXs
		{
			get
			{
				object obj = base.ViewState["HXS"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["HXS"] = value;
			}
		}

		// Token: 0x17004BC7 RID: 19399
		// (get) Token: 0x0600FAC7 RID: 64199 RVA: 0x003873C4 File Offset: 0x003855C4
		// (set) Token: 0x0600FAC8 RID: 64200 RVA: 0x003873ED File Offset: 0x003855ED
		public bool HiddenSm
		{
			get
			{
				object obj = base.ViewState["HSM"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["HSM"] = value;
			}
		}

		// Token: 0x17004BC8 RID: 19400
		// (get) Token: 0x0600FAC9 RID: 64201 RVA: 0x00387408 File Offset: 0x00385608
		// (set) Token: 0x0600FACA RID: 64202 RVA: 0x00387431 File Offset: 0x00385631
		public bool HiddenMd
		{
			get
			{
				object obj = base.ViewState["HMD"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["HMD"] = value;
			}
		}

		// Token: 0x17004BC9 RID: 19401
		// (get) Token: 0x0600FACB RID: 64203 RVA: 0x0038744C File Offset: 0x0038564C
		// (set) Token: 0x0600FACC RID: 64204 RVA: 0x00387475 File Offset: 0x00385675
		public bool HiddenLg
		{
			get
			{
				object obj = base.ViewState["HLG"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["HLG"] = value;
			}
		}

		// Token: 0x17004BCA RID: 19402
		// (get) Token: 0x0600FACD RID: 64205 RVA: 0x00387490 File Offset: 0x00385690
		// (set) Token: 0x0600FACE RID: 64206 RVA: 0x003874B9 File Offset: 0x003856B9
		public bool HiddenXl
		{
			get
			{
				object obj = base.ViewState["HXL"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["HXL"] = value;
			}
		}

		// Token: 0x17004BCB RID: 19403
		// (get) Token: 0x0600FACF RID: 64207 RVA: 0x003874D4 File Offset: 0x003856D4
		// (set) Token: 0x0600FAD0 RID: 64208 RVA: 0x003874FD File Offset: 0x003856FD
		[DefaultValue(PagerFieldAdaptiveHorizontalPosition.NotSet)]
		[NotifyParentProperty(true)]
		public PagerFieldAdaptiveHorizontalPosition HorizontalPositionXs
		{
			get
			{
				object obj = base.ViewState["hpXS"];
				if (obj != null)
				{
					return (PagerFieldAdaptiveHorizontalPosition)obj;
				}
				return PagerFieldAdaptiveHorizontalPosition.NotSet;
			}
			set
			{
				base.ViewState["hpXS"] = value;
			}
		}

		// Token: 0x17004BCC RID: 19404
		// (get) Token: 0x0600FAD1 RID: 64209 RVA: 0x00387518 File Offset: 0x00385718
		// (set) Token: 0x0600FAD2 RID: 64210 RVA: 0x00387541 File Offset: 0x00385741
		[DefaultValue(PagerFieldAdaptiveHorizontalPosition.NotSet)]
		[NotifyParentProperty(true)]
		public PagerFieldAdaptiveHorizontalPosition HorizontalPositionSm
		{
			get
			{
				object obj = base.ViewState["hpSM"];
				if (obj != null)
				{
					return (PagerFieldAdaptiveHorizontalPosition)obj;
				}
				return PagerFieldAdaptiveHorizontalPosition.NotSet;
			}
			set
			{
				base.ViewState["hpSM"] = value;
			}
		}

		// Token: 0x17004BCD RID: 19405
		// (get) Token: 0x0600FAD3 RID: 64211 RVA: 0x0038755C File Offset: 0x0038575C
		// (set) Token: 0x0600FAD4 RID: 64212 RVA: 0x00387585 File Offset: 0x00385785
		[NotifyParentProperty(true)]
		[DefaultValue(PagerFieldAdaptiveHorizontalPosition.NotSet)]
		public PagerFieldAdaptiveHorizontalPosition HorizontalPositionMd
		{
			get
			{
				object obj = base.ViewState["hpMD"];
				if (obj != null)
				{
					return (PagerFieldAdaptiveHorizontalPosition)obj;
				}
				return PagerFieldAdaptiveHorizontalPosition.NotSet;
			}
			set
			{
				base.ViewState["hpMD"] = value;
			}
		}

		// Token: 0x17004BCE RID: 19406
		// (get) Token: 0x0600FAD5 RID: 64213 RVA: 0x003875A0 File Offset: 0x003857A0
		// (set) Token: 0x0600FAD6 RID: 64214 RVA: 0x003875C9 File Offset: 0x003857C9
		[NotifyParentProperty(true)]
		[DefaultValue(PagerFieldAdaptiveHorizontalPosition.NotSet)]
		public PagerFieldAdaptiveHorizontalPosition HorizontalPositionLg
		{
			get
			{
				object obj = base.ViewState["hpLG"];
				if (obj != null)
				{
					return (PagerFieldAdaptiveHorizontalPosition)obj;
				}
				return PagerFieldAdaptiveHorizontalPosition.NotSet;
			}
			set
			{
				base.ViewState["hpLG"] = value;
			}
		}

		// Token: 0x17004BCF RID: 19407
		// (get) Token: 0x0600FAD7 RID: 64215 RVA: 0x003875E4 File Offset: 0x003857E4
		// (set) Token: 0x0600FAD8 RID: 64216 RVA: 0x0038760D File Offset: 0x0038580D
		[DefaultValue(PagerFieldAdaptiveHorizontalPosition.NotSet)]
		[NotifyParentProperty(true)]
		public PagerFieldAdaptiveHorizontalPosition HorizontalPositionXl
		{
			get
			{
				object obj = base.ViewState["hpXL"];
				if (obj != null)
				{
					return (PagerFieldAdaptiveHorizontalPosition)obj;
				}
				return PagerFieldAdaptiveHorizontalPosition.NotSet;
			}
			set
			{
				base.ViewState["hpXL"] = value;
			}
		}

		// Token: 0x04004756 RID: 18262
		private RadDataPager _owner;

		// Token: 0x04004757 RID: 18263
		private RouteValueDictionary _dictionary;

		// Token: 0x04004759 RID: 18265
		internal TFunc<object, string, string> CheckDefaultValue = delegate(object value, string defaultValue)
		{
			if (value == null)
			{
				return null;
			}
			string text = value as string;
			if (text == defaultValue)
			{
				return null;
			}
			return text;
		};
	}
}
