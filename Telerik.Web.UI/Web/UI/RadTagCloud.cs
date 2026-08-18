using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;

namespace Telerik.Web.UI
{
	// Token: 0x02000FA0 RID: 4000
	[RequiredScript(typeof(jQueryPlugins))]
	[ToolboxData("<{0}:RadTagCloud runat=\"server\"></{0}:RadTagCloud>")]
	[TelerikToolboxCategory("Miscellaneous")]
	[ToolboxBitmap(typeof(RadTagCloud), "Telerik.Web.UI.TagCloud.png")]
	[Designer("Telerik.Web.Design.RadTagCloudDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("TagCloud")]
	[EmbeddedSkin("TagCloud", "Default")]
	[ClientScriptResource("Telerik.Web.UI.RadTagCloud", "Telerik.Web.UI.TagCloud.RadTagCloud.js")]
	public class RadTagCloud : RadDataBoundControl, IPostBackEventHandler, INamingContainer
	{
		// Token: 0x0600991D RID: 39197 RVA: 0x00222868 File Offset: 0x00220A68
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "appendClientDataBoundItems", this.AppendClientDataBoundItems, false);
			base.DescribeProperty<TagCloudDistribution>(descriptor, "distribution", this.Distribution, TagCloudDistribution.Linear);
			base.DescribeProperty<string>(descriptor, "maxFontSize", this.MaxFontSize.ToString(CultureInfo.InvariantCulture), "1.7em");
			base.DescribeProperty<double>(descriptor, "maximalWeight", this.MaximalWeight, 0.0);
			base.DescribeProperty<int>(descriptor, "maxNumberOfItems", this.MaxNumberOfItems, 0);
			base.DescribeProperty<string>(descriptor, "minFontSize", this.MinFontSize.ToString(CultureInfo.InvariantCulture), "0.8em");
			base.DescribeProperty<double>(descriptor, "minimalWeight", this.MinimalWeight, 0.0);
			base.DescribeProperty<double>(descriptor, "minimalWeightAllowed", this.MinimalWeightAllowed, 0.0);
			base.DescribeProperty<bool>(descriptor, "renderItemWeight", this.RenderItemWeight, false);
			base.DescribeProperty<TagCloudSorting>(descriptor, "sorting", this.Sorting, TagCloudSorting.NotSorted);
			base.DescribeProperty<string>(descriptor, "target", this.Target, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600991E RID: 39198 RVA: 0x0022298C File Offset: 0x00220B8C
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "dataBound", this.OnClientDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClicked", this.OnClientItemClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "itemClicking", this.OnClientItemClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "itemDataBound", this.OnClientItemDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequested", this.OnClientItemsRequested);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequestFailed", this.OnClientItemsRequestFailed);
			RadDataBoundControl.DescribeEvent(descriptor, "itemsRequesting", this.OnClientItemsRequesting);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0600991F RID: 39199 RVA: 0x00222A28 File Offset: 0x00220C28
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("maxColor", (this.MaxColor != Color.Empty) ? string.Format("#{0:X2}{1:X2}{2:X2}", this.MaxColor.R, this.MaxColor.G, this.MaxColor.B) : "");
			descriptor.AddProperty("minColor", (this.MinColor != Color.Empty) ? string.Format("#{0:X2}{1:X2}{2:X2}", this.MinColor.R, this.MinColor.G, this.MinColor.B) : "");
			descriptor.AddProperty("foreColor", (this.ForeColor != Color.Empty) ? string.Format("#{0:X2}{1:X2}{2:X2}", this.ForeColor.R, this.ForeColor.G, this.ForeColor.B) : "");
			descriptor.AddProperty("_tabIndex", this.TabIndex);
			descriptor.AddProperty("_accessKey", this.AccessKey);
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			this.WebServiceSettings.Describe("webServiceSettings", serializer, descriptor);
			if (this.AutoPostBack)
			{
				descriptor.AddScriptProperty("_postBackReference", "\"" + this.GetPostbackEventReference() + "\"");
			}
			if (!base.IsEnabled)
			{
				descriptor.AddProperty("enabled", false);
			}
			this.SerializeDataBindingProperties(descriptor);
		}

		// Token: 0x06009920 RID: 39200 RVA: 0x00222BFE File Offset: 0x00220DFE
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.Target = (string)clientState["target"];
		}

		// Token: 0x06009921 RID: 39201 RVA: 0x00222C20 File Offset: 0x00220E20
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			if (this._shouldRetrieveTextFromSource)
			{
				string text = this.ReadTextFromTextFile(this.TextFile);
				string text2 = this.ReadTextFromURL(this.TextUrl);
				if (!string.IsNullOrEmpty(this.Text) || !string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2))
				{
					this.GenerateTagsFromText(this.Text + " " + (text + " ") + text2);
				}
				this._shouldRetrieveTextFromSource = false;
			}
			this.items = this.Items.FilteredAndSorted;
			if (this.generateFromText)
			{
				this._items = this.items;
			}
		}

		// Token: 0x06009922 RID: 39202 RVA: 0x00222CC8 File Offset: 0x00220EC8
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] == null)
			{
				this.Items.Clear();
				return;
			}
			((IStateManager)this.Items).LoadViewState(array[1]);
		}

		// Token: 0x06009923 RID: 39203 RVA: 0x00222D04 File Offset: 0x00220F04
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Items).SaveViewState()
			};
		}

		// Token: 0x06009924 RID: 39204 RVA: 0x00222D32 File Offset: 0x00220F32
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x06009925 RID: 39205 RVA: 0x00222D45 File Offset: 0x00220F45
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				return;
			}
			if (!base.DesignMode)
			{
				this.PrepareForDataBinding();
				this.BindToEnumerableData(data);
			}
		}

		// Token: 0x06009926 RID: 39206 RVA: 0x00222D68 File Offset: 0x00220F68
		public void BindToEnumerableData(IEnumerable dataSource)
		{
			foreach (object dataObject in dataSource)
			{
				this.BindItem(this.Items, dataObject);
			}
		}

		// Token: 0x06009927 RID: 39207 RVA: 0x00222DC0 File Offset: 0x00220FC0
		protected void PrepareForDataBinding()
		{
			if (!this.AppendDataBoundItems)
			{
				this.Items.Clear();
				base.ClearChildViewState();
			}
			this.TrackViewState();
		}

		// Token: 0x06009928 RID: 39208 RVA: 0x00222DE4 File Offset: 0x00220FE4
		private RadTagCloudItem BindItem(RadTagCloudItemCollection items, object dataObject)
		{
			RadTagCloudItem radTagCloudItem = new RadTagCloudItem();
			if (this.DataWeightField.Length > 0)
			{
				object obj = DataBinder.Eval(dataObject, this.DataWeightField);
				if (obj != DBNull.Value && obj != null)
				{
					radTagCloudItem.Weight = Convert.ToDouble(obj);
				}
			}
			if (this.DataNavigateUrlField.Length > 0)
			{
				object obj2 = DataBinder.Eval(dataObject, this.DataNavigateUrlField);
				if (obj2 != DBNull.Value && obj2 != null)
				{
					radTagCloudItem.NavigateUrl = (string.IsNullOrEmpty(this.DataNavigateUrlFormatString) ? obj2.ToString() : string.Format(this.DataNavigateUrlFormatString, obj2));
				}
			}
			if (this.DataToolTipField.Length > 0)
			{
				object obj3 = DataBinder.Eval(dataObject, this.DataToolTipField);
				if (obj3 != DBNull.Value && obj3 != null)
				{
					radTagCloudItem.ToolTip = (string.IsNullOrEmpty(this.DataToolTipFormatString) ? obj3.ToString() : string.Format(this.DataToolTipFormatString, obj3));
				}
			}
			if (this.DataValueField.Length > 0)
			{
				object obj4 = DataBinder.Eval(dataObject, this.DataValueField);
				if (obj4 != DBNull.Value && obj4 != null)
				{
					radTagCloudItem.Value = obj4.ToString();
				}
			}
			object obj5 = (this.DataTextField.Length > 0) ? DataBinder.Eval(dataObject, this.DataTextField) : dataObject;
			if (string.IsNullOrEmpty(this.DataTextFormatString))
			{
				radTagCloudItem.Text = obj5.ToString();
			}
			else
			{
				radTagCloudItem.Text = string.Format(this.DataTextFormatString, obj5);
			}
			if (!string.IsNullOrEmpty(radTagCloudItem.Text))
			{
				items.Add(radTagCloudItem);
				this.RaiseItemDataBound(radTagCloudItem, dataObject);
			}
			return radTagCloudItem;
		}

		// Token: 0x06009929 RID: 39209 RVA: 0x00222F64 File Offset: 0x00221164
		private void RaiseItemDataBound(RadTagCloudItem item, object dataItem)
		{
			item.DataItem = dataItem;
			this.OnItemDataBound(item);
			item.DataItem = null;
		}

		// Token: 0x0600992A RID: 39210 RVA: 0x00222F7C File Offset: 0x0022117C
		private void SerializeDataBindingProperties(IScriptDescriptor descriptor)
		{
			this.SerializeClientDataSourceID(descriptor);
			descriptor.AddProperty("_dataNavigateUrlField", this.DataNavigateUrlField);
			descriptor.AddProperty("_dataTextField", this.DataTextField);
			descriptor.AddProperty("_dataToolTipField", this.DataToolTipField);
			descriptor.AddProperty("_dataValueField", this.DataValueField);
			descriptor.AddProperty("_dataWeightField", this.DataWeightField);
		}

		// Token: 0x0600992B RID: 39211 RVA: 0x00222FE8 File Offset: 0x002211E8
		private void SerializeClientDataSourceID(IScriptDescriptor descriptor)
		{
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("_clientDataSourceID", control.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("_clientDataSourceID", this.ClientDataSourceID);
				}
			}
		}

		// Token: 0x17003074 RID: 12404
		// (get) Token: 0x0600992C RID: 39212 RVA: 0x00223048 File Offset: 0x00221248
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17003075 RID: 12405
		// (get) Token: 0x0600992D RID: 39213 RVA: 0x0022304C File Offset: 0x0022124C
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadTagCloud RadTagCloud_{0}" + ((!base.IsEnabled || !this.originalEnabled) ? " rtcDisabled" : "");
				if (this.RenderMode == RenderMode.Classic)
				{
					text += " rtcClassic";
				}
				return text;
			}
		}

		// Token: 0x0600992E RID: 39214 RVA: 0x00223098 File Offset: 0x00221298
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.originalEnabled = this.Enabled;
			this.Enabled = true;
			string accessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			short tabIndex = this.TabIndex;
			this.TabIndex = 0;
			base.AddAttributesToRender(writer);
			this.Enabled = this.originalEnabled;
			this.AccessKey = accessKey;
			this.TabIndex = tabIndex;
		}

		// Token: 0x0600992F RID: 39215 RVA: 0x002230FC File Offset: 0x002212FC
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (base.DesignMode && (!string.IsNullOrEmpty(this.DataSourceID) || this.DataSource != null))
			{
				writer.Write("<div style='font-size:16px;padding-top:3px 0 0 0;'>RadTagCloud is Databound</div>");
			}
			if (base.DesignMode)
			{
				string text = this.ReadTextFromTextFile(this.TextFile);
				string text2 = this.ReadTextFromURL(this.TextUrl);
				if (!string.IsNullOrEmpty(this.Text) || !string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2))
				{
					this.GenerateTagsFromText(this.Text + " " + (text + " ") + text2);
				}
				this.items = this.Items.FilteredAndSorted;
			}
			if (this.items.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtcTagList");
				if (base.DesignMode)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0px");
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				this.RenderItems(writer, this.items);
				writer.RenderEndTag();
				return;
			}
			if (base.DesignMode)
			{
				RadTagCloud.RenderDefaultDesigner(writer);
			}
		}

		// Token: 0x06009930 RID: 39216 RVA: 0x0022320C File Offset: 0x0022140C
		private void RenderItems(HtmlTextWriter writer, RadTagCloudItemCollection items)
		{
			int count = items.Count;
			this.CalculateImportance(items);
			for (int i = 0; i < count; i++)
			{
				RadTagCloudItem radTagCloudItem = items[i];
				double coefficient = (this.Distribution == TagCloudDistribution.Linear) ? this.GetLinearCoefficient(radTagCloudItem) : this.GetLogarithmicCoefficient(radTagCloudItem);
				Unit fontSize = this.CalculateFontSize(coefficient);
				Color itemColor = this.GetItemColor(coefficient);
				this.RenderLIAttributesAndStyles(writer);
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Concat(new string[]
				{
					this.ClientID,
					"_",
					i.ToString(),
					"_",
					radTagCloudItem.Weight.ToString()
				}));
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				this.RenderAnchorAttributesAndStyles(writer, radTagCloudItem, fontSize, itemColor);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write(radTagCloudItem.Text);
				writer.RenderEndTag();
				if (this.RenderItemWeight)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, fontSize.ToString());
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					writer.Write("(" + Convert.ToInt32(radTagCloudItem.Weight) + ")");
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
		}

		// Token: 0x06009931 RID: 39217 RVA: 0x00223348 File Offset: 0x00221548
		private void RenderAnchorAttributesAndStyles(HtmlTextWriter writer, RadTagCloudItem _tagItem, Unit fontSize, Color foreColor)
		{
			string value = base.ResolveUrl(_tagItem.NavigateUrl);
			if (string.IsNullOrEmpty(value))
			{
				value = "javascript:void(0)";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
			writer.AddAttribute(HtmlTextWriterAttribute.Rel, "tag");
			if (!string.IsNullOrEmpty(_tagItem.AccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, _tagItem.AccessKey);
			}
			if (_tagItem.TabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, _tagItem.TabIndex.ToString());
			}
			if (string.IsNullOrEmpty(_tagItem.ToolTip) && !string.IsNullOrEmpty(_tagItem.Text))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, _tagItem.Text);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, _tagItem.ToolTip);
			}
			if (foreColor != Color.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(foreColor));
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, fontSize.ToString(CultureInfo.InvariantCulture));
			if (base.DesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "2px 4px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, "'Segoe UI', Arial, sans-serif");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "#000");
				writer.AddStyleAttribute(HtmlTextWriterStyle.TextDecoration, "none");
			}
		}

		// Token: 0x06009932 RID: 39218 RVA: 0x0022345F File Offset: 0x0022165F
		private void RenderLIAttributesAndStyles(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtcTagItem");
			if (base.DesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline-block");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "5px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "#6e9ded");
			}
		}

		// Token: 0x06009933 RID: 39219 RVA: 0x0022349C File Offset: 0x0022169C
		internal PostBackOptions GetPostBackOptions(Control control, string argument, string postBackUrl)
		{
			PostBackOptions postBackOptions = new PostBackOptions(control, argument);
			postBackOptions.ClientSubmit = true;
			if (this.Page != null && !string.IsNullOrEmpty(postBackUrl))
			{
				postBackOptions.ActionUrl = postBackUrl;
			}
			return postBackOptions;
		}

		// Token: 0x06009934 RID: 39220 RVA: 0x002234D0 File Offset: 0x002216D0
		internal PostBackOptions GetPostBackOptions(Control control, string argument)
		{
			string postBackUrl = string.Empty;
			if (!string.IsNullOrEmpty(this.PostBackUrl))
			{
				postBackUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
			}
			return this.GetPostBackOptions(control, argument, postBackUrl);
		}

		// Token: 0x06009935 RID: 39221 RVA: 0x0022350C File Offset: 0x0022170C
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments"));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x06009936 RID: 39222 RVA: 0x00223548 File Offset: 0x00221748
		protected internal virtual RadTagCloudItem FindItemByHierarchicalIndex(string hierarchicalIndex)
		{
			if (string.IsNullOrEmpty(hierarchicalIndex))
			{
				return null;
			}
			int num = Convert.ToInt32(hierarchicalIndex);
			if (num >= this.Items.Count)
			{
				return null;
			}
			return this.Items.FilteredAndSorted[num];
		}

		// Token: 0x06009937 RID: 39223 RVA: 0x00223587 File Offset: 0x00221787
		void IPostBackEventHandler.RaisePostBackEvent(string itemIndex)
		{
			this.RaisePostBackEvent(itemIndex);
		}

		// Token: 0x06009938 RID: 39224 RVA: 0x00223590 File Offset: 0x00221790
		protected virtual void RaisePostBackEvent(string itemIndex)
		{
			RadTagCloudItem radTagCloudItem = this.FindItemByHierarchicalIndex(itemIndex);
			if (radTagCloudItem != null)
			{
				this.OnItemClick(radTagCloudItem);
			}
		}

		// Token: 0x06009939 RID: 39225 RVA: 0x002235B0 File Offset: 0x002217B0
		protected void RaiseTagCloudItemEvent(RadTagCloudItem item, object eventKey)
		{
			RadTagCloudEventHandler radTagCloudEventHandler = (RadTagCloudEventHandler)base.Events[eventKey];
			if (radTagCloudEventHandler != null)
			{
				radTagCloudEventHandler(this, new RadTagCloudEventArgs(item));
			}
		}

		// Token: 0x0600993A RID: 39226 RVA: 0x002235DF File Offset: 0x002217DF
		protected virtual void OnItemDataBound(RadTagCloudItem item)
		{
			this.RaiseTagCloudItemEvent(item, RadTagCloud.ItemDataBoundEvent);
		}

		// Token: 0x0600993B RID: 39227 RVA: 0x002235ED File Offset: 0x002217ED
		protected virtual void OnItemClick(RadTagCloudItem item)
		{
			this.RaiseTagCloudItemEvent(item, RadTagCloud.ItemClickEvent);
		}

		// Token: 0x0600993C RID: 39228 RVA: 0x002235FC File Offset: 0x002217FC
		private double GetLinearCoefficient(RadTagCloudItem item)
		{
			double result = 0.0;
			double num = item.Weight - this.MinimalWeight;
			double num2 = this.MaximalWeight - this.MinimalWeight;
			if (num2 != 0.0)
			{
				result = num / num2;
			}
			return result;
		}

		// Token: 0x0600993D RID: 39229 RVA: 0x00223640 File Offset: 0x00221840
		private double GetLogarithmicCoefficient(RadTagCloudItem item)
		{
			double num = (this.MinimalWeight <= 0.0 || Math.Log(this.MinimalWeight) < 0.0) ? 0.0 : Math.Log(this.MinimalWeight);
			double num2 = (this.MaximalWeight <= 0.0 || Math.Log(this.MaximalWeight) < 0.0) ? 0.0 : Math.Log(this.MaximalWeight);
			double num3 = (item.Weight <= 0.0 || Math.Log(item.Weight) < 0.0) ? 0.0 : Math.Log(item.Weight);
			double result = 0.0;
			double num4 = num3 - num;
			double num5 = num2 - num;
			if (num5 != 0.0)
			{
				result = num4 / num5;
			}
			return result;
		}

		// Token: 0x0600993E RID: 39230 RVA: 0x00223730 File Offset: 0x00221930
		internal Unit CalculateFontSize(double coefficient)
		{
			if (this.MinFontSize.Type != this.MaxFontSize.Type)
			{
				throw new ArgumentException("The MinFontSize and MaxFontSize properties should be of the same unit type (i.e. em, px or pt).Their default values for both of them are set in UnitType.Em.");
			}
			double value = this.MinFontSize.Value + (this.MaxFontSize.Value - this.MinFontSize.Value) * coefficient;
			if (this.MinFontSize.Type.ToString().ToLower() == "em")
			{
				return new Unit(value, this.MinFontSize.Type);
			}
			return new Unit(Math.Round(value, MidpointRounding.AwayFromZero), this.MinFontSize.Type);
		}

		// Token: 0x0600993F RID: 39231 RVA: 0x002237F4 File Offset: 0x002219F4
		private Color CalculateColor(double coefficient)
		{
			if (coefficient > 1.0)
			{
				coefficient = 1.0;
			}
			if (coefficient < 0.0 || double.IsNaN(coefficient))
			{
				coefficient = 0.0;
			}
			double num = Math.Round((double)this.MinColor.R + (double)(this.MaxColor.R - this.MinColor.R) * coefficient);
			double num2 = Math.Round((double)this.MinColor.G + (double)(this.MaxColor.G - this.MinColor.G) * coefficient);
			double num3 = Math.Round((double)this.MinColor.B + (double)(this.MaxColor.B - this.MinColor.B) * coefficient);
			return Color.FromArgb((int)num, (int)num2, (int)num3);
		}

		// Token: 0x06009940 RID: 39232 RVA: 0x002238EC File Offset: 0x00221AEC
		private Color GetItemColor(double coefficient)
		{
			if (this.MinColor != Color.Empty && this.MaxColor != Color.Empty)
			{
				return this.CalculateColor(coefficient);
			}
			return this.ForeColor;
		}

		// Token: 0x06009941 RID: 39233 RVA: 0x00223920 File Offset: 0x00221B20
		private Dictionary<string, int> CreateWordMap(string text)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			string text2 = text + " ";
			int length = text2.Length;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				char c = text2[i];
				if (RadTagCloud.IsValidCharacter(c, this.PunctuationCharacters, i, text2, this.PunctuationCharactersValid))
				{
					stringBuilder.Append(c);
				}
				else
				{
					string text3 = stringBuilder.ToString().ToLower().Trim();
					if (RadTagCloud.ExcludeWord(text3, this.WordsToExclude))
					{
						stringBuilder = new StringBuilder();
					}
					else
					{
						if (!string.IsNullOrEmpty(text3) && dictionary.ContainsKey(text3))
						{
							Dictionary<string, int> dictionary2;
							string key;
							(dictionary2 = dictionary)[key = text3] = dictionary2[key] + 1;
						}
						else if (!string.IsNullOrEmpty(text3))
						{
							dictionary[text3] = 1;
						}
						stringBuilder = new StringBuilder();
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06009942 RID: 39234 RVA: 0x00223A00 File Offset: 0x00221C00
		public void GenerateTagsFromText(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			this.generateFromText = true;
			Dictionary<string, int> dictionary = this.CreateWordMap(text);
			if (!this.AppendDataBoundItems)
			{
				this.Items.Clear();
			}
			if (this.MaxNumberOfItems > 0 && this.MaxNumberOfItems <= this.Items.Count)
			{
				return;
			}
			int num = 0;
			foreach (string text2 in dictionary.Keys)
			{
				if ((double)dictionary[text2] >= this.MinimalWeightAllowed)
				{
					this.Items.Add(new RadTagCloudItem(text2, (double)dictionary[text2]));
				}
				if (!this.TakeTopWeightedItems && this.MaxNumberOfItems > 0 && num >= this.MaxNumberOfItems)
				{
					break;
				}
				num++;
			}
		}

		// Token: 0x06009943 RID: 39235 RVA: 0x00223AE0 File Offset: 0x00221CE0
		public string ReadTextFromTextFile(string fileName)
		{
			if (string.IsNullOrEmpty(fileName) || base.DesignMode)
			{
				return string.Empty;
			}
			string path = this.Context.Request.MapPath(fileName);
			string result = string.Empty;
			if (!File.Exists(path))
			{
				return result;
			}
			using (StreamReader streamReader = File.OpenText(path))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x06009944 RID: 39236 RVA: 0x00223B50 File Offset: 0x00221D50
		public string ReadTextFromURL(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return string.Empty;
			}
			string text = base.ResolveClientUrl(url);
			string text2 = string.Empty;
			Uri requestUri;
			if (Uri.IsWellFormedUriString(text, UriKind.Absolute))
			{
				requestUri = new Uri(text);
			}
			else
			{
				if (base.DesignMode || !Uri.IsWellFormedUriString(RadTagCloud.BaseSiteUrl + text, UriKind.Absolute))
				{
					return text2;
				}
				requestUri = new Uri(RadTagCloud.BaseSiteUrl + text);
			}
			WebRequest webRequest = WebRequest.Create(requestUri);
			WebResponse response = webRequest.GetResponse();
			using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
			{
				text2 = streamReader.ReadToEnd();
				streamReader.Close();
			}
			text2 = RadTagCloud.StripHtml(HttpUtility.HtmlDecode(text2), true);
			return text2;
		}

		// Token: 0x17003076 RID: 12406
		// (get) Token: 0x06009945 RID: 39237 RVA: 0x00223C14 File Offset: 0x00221E14
		private static string BaseSiteUrl
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				return string.Concat(new object[]
				{
					httpContext.Request.Url.Scheme,
					"://",
					httpContext.Request.Url.Authority,
					httpContext.Request.ApplicationPath.TrimEnd(new char[]
					{
						'/'
					}),
					'/'
				});
			}
		}

		// Token: 0x06009946 RID: 39238 RVA: 0x00223C8E File Offset: 0x00221E8E
		private void CalculateImportance(RadTagCloudItemCollection _items)
		{
			if (this.Items.Count > 0)
			{
				this.MinimalWeight = _items.Min().Weight;
				this.MaximalWeight = _items.Max().Weight;
			}
		}

		// Token: 0x06009947 RID: 39239 RVA: 0x00223CC0 File Offset: 0x00221EC0
		private static void RenderDefaultDesigner(HtmlTextWriter writer)
		{
			string[] array = new string[]
			{
				"Arabic",
				"Bulgarian",
				"Chinese",
				"Czech",
				"Dutch",
				"English",
				"French",
				"German",
				"Hebrew",
				"Hindi",
				"Hungarian",
				"Italian",
				"Japanese",
				"Norwegian",
				"Persian",
				"Russian",
				"Slovak",
				"Slovenian",
				"Thai",
				"Turkish"
			};
			int num = array.Length;
			Random random = new Random(2002);
			for (int i = 0; i < num; i++)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline-block");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "7px");
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
				writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, random.Next(10, 20).ToString() + "px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "Blue");
				writer.AddStyleAttribute(HtmlTextWriterStyle.TextDecoration, "none");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write(array[i]);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x06009948 RID: 39240 RVA: 0x00223E44 File Offset: 0x00222044
		private static bool ExcludeWord(string word, string[] wordsToEscape)
		{
			return Array.IndexOf<string>(wordsToEscape, word) > -1;
		}

		// Token: 0x06009949 RID: 39241 RVA: 0x00223E5F File Offset: 0x0022205F
		private static bool IsValidCharacter(char c, string invalidChars, int charPosition, string text, string punctuationCharactersValid)
		{
			return !char.IsSeparator(c) && (!RadTagCloud.IsPunctuation(c, invalidChars) || (punctuationCharactersValid.IndexOf(c) != -1 && RadTagCloud.IsCharSurroundedByAlphaNumeric(c, charPosition, text)));
		}

		// Token: 0x0600994A RID: 39242 RVA: 0x00223E8C File Offset: 0x0022208C
		private static bool IsPunctuation(char c, string invalidChars)
		{
			if (string.IsNullOrEmpty(invalidChars))
			{
				return "$#".IndexOf(c) == -1 && char.IsPunctuation(c);
			}
			return invalidChars.IndexOf(c) != -1;
		}

		// Token: 0x0600994B RID: 39243 RVA: 0x00223EBC File Offset: 0x002220BC
		private static bool IsCharSurroundedByAlphaNumeric(char c, int charPosition, string text)
		{
			bool flag = charPosition == 0 || char.IsLetterOrDigit(text[charPosition - 1]);
			bool flag2 = charPosition == text.Length - 1 || char.IsLetterOrDigit(text[charPosition + 1]);
			if (flag2)
			{
				if (-1 != "._".IndexOf(c) && (flag || char.IsSeparator(text[charPosition - 1])))
				{
					return true;
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600994C RID: 39244 RVA: 0x00223F28 File Offset: 0x00222128
		internal static string StripHtml(string text, bool isFullHtmlDocument)
		{
			int num = 0;
			if (isFullHtmlDocument)
			{
				num = text.IndexOf("<body", StringComparison.InvariantCultureIgnoreCase);
			}
			if (num == -1)
			{
				return string.Empty;
			}
			int length = text.Length;
			char[] array = new char[length];
			int num2 = 0;
			bool flag = true;
			bool flag2 = false;
			for (int i = num; i < length; i++)
			{
				char c = text[i];
				int num3 = RadTagCloud.SkipScriptTag(text, c, i, flag2, length);
				if (i != num3 && c == '<')
				{
					i = num3 - 1;
					flag2 = true;
				}
				if (i != num3 && text[num3 - 1] == '>')
				{
					flag2 = false;
					i = num3 - 1;
				}
				if (!flag2)
				{
					c = text[i];
					if (c == '<' && i + 1 < length && text[i + 1] != ' ')
					{
						flag = false;
					}
					else if (c == '>')
					{
						flag = true;
						array[num2] = ' ';
						num2++;
					}
					else if (flag)
					{
						array[num2] = c;
						num2++;
					}
					else
					{
						i = RadTagCloud.SkipAttribute(text, c, i);
					}
				}
			}
			return new string(array, 0, num2);
		}

		// Token: 0x0600994D RID: 39245 RVA: 0x00224034 File Offset: 0x00222234
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private static int SkipAttribute(string text, char character, int position)
		{
			string text2 = "\"'";
			if (text2.IndexOf(character) != -1)
			{
				return text.IndexOf(character, position + 1);
			}
			return position;
		}

		// Token: 0x0600994E RID: 39246 RVA: 0x00224060 File Offset: 0x00222260
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private static int SkipScriptTag(string text, char character, int position, bool isScript, int textLength)
		{
			int result = position;
			if (!isScript)
			{
				if (character == '<' && textLength > position + 8 && ("<script " == text.Substring(position, 8).ToLower() || "<script>" == text.Substring(position, 8).ToLower()))
				{
					result = position + 7;
				}
			}
			else
			{
				if (text[text.IndexOf('>', position) - 1] == '/')
				{
					return position + 1;
				}
				int num = text.IndexOf("</script>", position, StringComparison.InvariantCultureIgnoreCase);
				if (num != -1)
				{
					return num + 9;
				}
			}
			return result;
		}

		// Token: 0x17003077 RID: 12407
		// (get) Token: 0x0600994F RID: 39247 RVA: 0x002240EB File Offset: 0x002222EB
		// (set) Token: 0x06009950 RID: 39248 RVA: 0x0022410B File Offset: 0x0022230B
		[ClientPropertyName("itemClicking")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when an item is clicked.")]
		public string OnClientItemClicking
		{
			get
			{
				return ((string)this.ViewState["OnClientItemClicking"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientItemClicking"] = value;
			}
		}

		// Token: 0x17003078 RID: 12408
		// (get) Token: 0x06009951 RID: 39249 RVA: 0x0022411E File Offset: 0x0022231E
		// (set) Token: 0x06009952 RID: 39250 RVA: 0x0022413E File Offset: 0x0022233E
		[Category("Client-side events")]
		[Description("The name of the javascript function called after an item is clicked.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("itemClicked")]
		[ClientControlEvent]
		public string OnClientItemClicked
		{
			get
			{
				return ((string)this.ViewState["OnClientItemClicked"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientItemClicked"] = value;
			}
		}

		// Token: 0x17003079 RID: 12409
		// (get) Token: 0x06009953 RID: 39251 RVA: 0x00224151 File Offset: 0x00222351
		// (set) Token: 0x06009954 RID: 39252 RVA: 0x00224171 File Offset: 0x00222371
		[Category("Client-side events")]
		[ClientPropertyName("load")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		[Description("The name of the javascript function when the control loads.")]
		public string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x1400016E RID: 366
		// (add) Token: 0x06009955 RID: 39253 RVA: 0x00224184 File Offset: 0x00222384
		// (remove) Token: 0x06009956 RID: 39254 RVA: 0x00224197 File Offset: 0x00222397
		[Description("Fired after a RadTagCloudItem is databound.")]
		[Category("Behavior")]
		public event RadTagCloudEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadTagCloud.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTagCloud.ItemDataBoundEvent, value);
			}
		}

		// Token: 0x1400016F RID: 367
		// (add) Token: 0x06009957 RID: 39255 RVA: 0x002241AA File Offset: 0x002223AA
		// (remove) Token: 0x06009958 RID: 39256 RVA: 0x002241BD File Offset: 0x002223BD
		[Description("Fired after a RadTagCloud item is clicked")]
		[Category("Behavior")]
		public event RadTagCloudEventHandler ItemClick
		{
			add
			{
				base.Events.AddHandler(RadTagCloud.ItemClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTagCloud.ItemClickEvent, value);
			}
		}

		// Token: 0x1700307A RID: 12410
		// (get) Token: 0x06009959 RID: 39257 RVA: 0x002241D0 File Offset: 0x002223D0
		// (set) Token: 0x0600995A RID: 39258 RVA: 0x002241F0 File Offset: 0x002223F0
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("itemsRequesting")]
		[Description("The name of the javascript function called just before the request for items begins.")]
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Client-side events")]
		public string OnClientItemsRequesting
		{
			get
			{
				return (string)(this.ViewState["OnClientItemsRequesting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemsRequesting"] = value;
			}
		}

		// Token: 0x1700307B RID: 12411
		// (get) Token: 0x0600995B RID: 39259 RVA: 0x00224203 File Offset: 0x00222403
		// (set) Token: 0x0600995C RID: 39260 RVA: 0x00224223 File Offset: 0x00222423
		[Bindable(false)]
		[ClientPropertyName("itemsRequested")]
		[Description("The name of the javascript function called after the request for items has completed.")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientItemsRequested
		{
			get
			{
				return (string)(this.ViewState["OnClientItemsRequested"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemsRequested"] = value;
			}
		}

		// Token: 0x1700307C RID: 12412
		// (get) Token: 0x0600995D RID: 39261 RVA: 0x00224236 File Offset: 0x00222436
		// (set) Token: 0x0600995E RID: 39262 RVA: 0x00224256 File Offset: 0x00222456
		[Description("The name of the javascript function called after the request for items has failed.")]
		[Bindable(false)]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("itemsRequestFailed")]
		[Category("Client-side events")]
		public string OnClientItemsRequestFailed
		{
			get
			{
				return (string)(this.ViewState["OnClientItemsRequestFailed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemsRequestFailed"] = value;
			}
		}

		// Token: 0x1700307D RID: 12413
		// (get) Token: 0x0600995F RID: 39263 RVA: 0x00224269 File Offset: 0x00222469
		// (set) Token: 0x06009960 RID: 39264 RVA: 0x00224289 File Offset: 0x00222489
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("itemDataBound")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when an item is databound on the client-side.")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientItemDataBound
		{
			get
			{
				return ((string)this.ViewState["OnClientItemDataBound"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientItemDataBound"] = value;
			}
		}

		// Token: 0x1700307E RID: 12414
		// (get) Token: 0x06009961 RID: 39265 RVA: 0x0022429C File Offset: 0x0022249C
		// (set) Token: 0x06009962 RID: 39266 RVA: 0x002242BC File Offset: 0x002224BC
		[Description("The name of the JavaScript function which is called when the Rotator has been populated with data.")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("dataBound")]
		public string OnClientDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDataBound"] = value;
			}
		}

		// Token: 0x1700307F RID: 12415
		// (get) Token: 0x06009963 RID: 39267 RVA: 0x002242CF File Offset: 0x002224CF
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003080 RID: 12416
		// (get) Token: 0x06009964 RID: 39268 RVA: 0x002242D2 File Offset: 0x002224D2
		// (set) Token: 0x06009965 RID: 39269 RVA: 0x002242DA File Offset: 0x002224DA
		[ClientControlProperty]
		private double MinimalWeight
		{
			get
			{
				return this._minimalWeight;
			}
			set
			{
				this._minimalWeight = value;
			}
		}

		// Token: 0x17003081 RID: 12417
		// (get) Token: 0x06009966 RID: 39270 RVA: 0x002242E3 File Offset: 0x002224E3
		// (set) Token: 0x06009967 RID: 39271 RVA: 0x002242EB File Offset: 0x002224EB
		[ClientControlProperty]
		private double MaximalWeight
		{
			get
			{
				return this._maximalWeight;
			}
			set
			{
				this._maximalWeight = value;
			}
		}

		// Token: 0x17003082 RID: 12418
		// (get) Token: 0x06009968 RID: 39272 RVA: 0x002242F4 File Offset: 0x002224F4
		// (set) Token: 0x06009969 RID: 39273 RVA: 0x00224315 File Offset: 0x00222515
		private bool _shouldRetrieveTextFromSource
		{
			get
			{
				return (bool)(this.ViewState["_shouldRetrieveTextFromSource"] ?? true);
			}
			set
			{
				this.ViewState["_shouldRetrieveTextFromSource"] = value;
			}
		}

		// Token: 0x17003083 RID: 12419
		// (get) Token: 0x0600996A RID: 39274 RVA: 0x00224330 File Offset: 0x00222530
		// (set) Token: 0x0600996B RID: 39275 RVA: 0x0022435D File Offset: 0x0022255D
		internal SortedList<RadTagCloudItem, double> ListOfSortedItems
		{
			get
			{
				if (this._listOfSortedItems == null)
				{
					IComparer<RadTagCloudItem> comparer = new RadTagCloud.WeightComparerDsc();
					this._listOfSortedItems = new SortedList<RadTagCloudItem, double>(comparer);
				}
				return this._listOfSortedItems;
			}
			set
			{
				this._listOfSortedItems = value;
			}
		}

		// Token: 0x17003084 RID: 12420
		// (get) Token: 0x0600996C RID: 39276 RVA: 0x00224366 File Offset: 0x00222566
		// (set) Token: 0x0600996D RID: 39277 RVA: 0x00224387 File Offset: 0x00222587
		[Category("Behavior")]
		[Description("Gets or sets a bool value that indicates whether tagCloud items are cleared before data binding.")]
		[DefaultValue(false)]
		public bool AppendDataBoundItems
		{
			get
			{
				return (bool)(this.ViewState["AppendDataBoundItems"] ?? false);
			}
			set
			{
				this.ViewState["AppendDataBoundItems"] = value;
			}
		}

		// Token: 0x17003085 RID: 12421
		// (get) Token: 0x0600996E RID: 39278 RVA: 0x0022439F File Offset: 0x0022259F
		// (set) Token: 0x0600996F RID: 39279 RVA: 0x002243C0 File Offset: 0x002225C0
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Specifies whether the TagCloud items created on the client-side should be cleared before data binding.")]
		[ClientControlProperty]
		public bool AppendClientDataBoundItems
		{
			get
			{
				return (bool)(this.ViewState["AppendClientDataBoundItems"] ?? false);
			}
			set
			{
				this.ViewState["AppendClientDataBoundItems"] = value;
			}
		}

		// Token: 0x17003086 RID: 12422
		// (get) Token: 0x06009970 RID: 39280 RVA: 0x002243D8 File Offset: 0x002225D8
		// (set) Token: 0x06009971 RID: 39281 RVA: 0x002243F9 File Offset: 0x002225F9
		[Description("Gets or sets a value indicating whether a postback to the server automatically occurs when the user interacts with the control.")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public virtual bool AutoPostBack
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBack"] ?? false);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x17003087 RID: 12423
		// (get) Token: 0x06009972 RID: 39282 RVA: 0x00224411 File Offset: 0x00222611
		// (set) Token: 0x06009973 RID: 39283 RVA: 0x00224431 File Offset: 0x00222631
		[Category("Data")]
		[DefaultValue("")]
		[Description("Gets or sets the field of the data source that provides the URL (NavigateUrl) of the TagCloud items.")]
		public string DataNavigateUrlField
		{
			get
			{
				return (this.ViewState["DataNavigateUrlField"] as string) ?? "";
			}
			set
			{
				this.ViewState["DataNavigateUrlField"] = value;
			}
		}

		// Token: 0x17003088 RID: 12424
		// (get) Token: 0x06009974 RID: 39284 RVA: 0x00224444 File Offset: 0x00222644
		// (set) Token: 0x06009975 RID: 39285 RVA: 0x00224464 File Offset: 0x00222664
		[DefaultValue("")]
		[Description("Gets or sets the formatting string used to control how data bound to the NavigateUrl property of the TagCloud item is displayed.")]
		[Category("Data")]
		public string DataNavigateUrlFormatString
		{
			get
			{
				return (this.ViewState["DataNavigateUrlFormatString"] as string) ?? "";
			}
			set
			{
				this.ViewState["DataNavigateUrlFormatString"] = value;
			}
		}

		// Token: 0x17003089 RID: 12425
		// (get) Token: 0x06009976 RID: 39286 RVA: 0x00224477 File Offset: 0x00222677
		// (set) Token: 0x06009977 RID: 39287 RVA: 0x00224497 File Offset: 0x00222697
		[Category("Data")]
		[Description("Gets or sets the field of the data source that provides the text content of the TagCloud items.")]
		[DefaultValue("")]
		public string DataTextField
		{
			get
			{
				return (this.ViewState["DataTextField"] as string) ?? "";
			}
			set
			{
				this.ViewState["DataTextField"] = value;
			}
		}

		// Token: 0x1700308A RID: 12426
		// (get) Token: 0x06009978 RID: 39288 RVA: 0x002244AA File Offset: 0x002226AA
		// (set) Token: 0x06009979 RID: 39289 RVA: 0x002244CA File Offset: 0x002226CA
		[DefaultValue("")]
		[Category("Data")]
		[Description("Gets or sets the formatting string used to control how data bound to the Text property of the TagCloud item is displayed.")]
		public string DataTextFormatString
		{
			get
			{
				return (this.ViewState["DataTextFormatString"] as string) ?? "";
			}
			set
			{
				this.ViewState["DataTextFormatString"] = value;
			}
		}

		// Token: 0x1700308B RID: 12427
		// (get) Token: 0x0600997A RID: 39290 RVA: 0x002244DD File Offset: 0x002226DD
		// (set) Token: 0x0600997B RID: 39291 RVA: 0x002244FD File Offset: 0x002226FD
		[DefaultValue("")]
		[Category("Data")]
		[Description("Gets or sets the field of the data source that provides the ToolTip content of the TagCloud items.")]
		public string DataToolTipField
		{
			get
			{
				return (this.ViewState["DataToolTipField"] as string) ?? "";
			}
			set
			{
				this.ViewState["DataToolTipField"] = value;
			}
		}

		// Token: 0x1700308C RID: 12428
		// (get) Token: 0x0600997C RID: 39292 RVA: 0x00224510 File Offset: 0x00222710
		// (set) Token: 0x0600997D RID: 39293 RVA: 0x00224530 File Offset: 0x00222730
		[Description("Gets or sets the formatting string used to control how data bound to the ToolTip property of the TagCloud item is displayed.")]
		[DefaultValue("")]
		[Category("Data")]
		public string DataToolTipFormatString
		{
			get
			{
				return (this.ViewState["DataToolTipFormatString"] as string) ?? "";
			}
			set
			{
				this.ViewState["DataToolTipFormatString"] = value;
			}
		}

		// Token: 0x1700308D RID: 12429
		// (get) Token: 0x0600997E RID: 39294 RVA: 0x00224543 File Offset: 0x00222743
		// (set) Token: 0x0600997F RID: 39295 RVA: 0x00224563 File Offset: 0x00222763
		[Category("Data")]
		[DefaultValue("")]
		[Description("Gets or sets the field of the data source that provides the value content of the TagCloud items.")]
		public string DataValueField
		{
			get
			{
				return (this.ViewState["DataValueField"] as string) ?? "";
			}
			set
			{
				this.ViewState["DataValueField"] = value;
			}
		}

		// Token: 0x1700308E RID: 12430
		// (get) Token: 0x06009980 RID: 39296 RVA: 0x00224576 File Offset: 0x00222776
		// (set) Token: 0x06009981 RID: 39297 RVA: 0x00224596 File Offset: 0x00222796
		[Category("Data")]
		[DefaultValue("")]
		[Description("Gets or sets the field of the data source that provides the weight of the TagCloud items.")]
		public string DataWeightField
		{
			get
			{
				return (this.ViewState["DataWeightField"] as string) ?? "";
			}
			set
			{
				this.ViewState["DataWeightField"] = value;
			}
		}

		// Token: 0x1700308F RID: 12431
		// (get) Token: 0x06009982 RID: 39298 RVA: 0x002245A9 File Offset: 0x002227A9
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public RadTagCloudItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RadTagCloudItemCollection(this);
				}
				return this._items;
			}
		}

		// Token: 0x17003090 RID: 12432
		// (get) Token: 0x06009983 RID: 39299 RVA: 0x002245C5 File Offset: 0x002227C5
		// (set) Token: 0x06009984 RID: 39300 RVA: 0x002245E6 File Offset: 0x002227E6
		[ClientControlProperty]
		[Description("Gets or sets a value indicating how the font-size will be distributed among the different words.")]
		[Category("Behavior")]
		[DefaultValue(TagCloudDistribution.Linear)]
		public TagCloudDistribution Distribution
		{
			get
			{
				return (TagCloudDistribution)(this.ViewState["Distribution"] ?? TagCloudDistribution.Linear);
			}
			set
			{
				this.ViewState["Distribution"] = value;
			}
		}

		// Token: 0x17003091 RID: 12433
		// (get) Token: 0x06009985 RID: 39301 RVA: 0x002245FE File Offset: 0x002227FE
		// (set) Token: 0x06009986 RID: 39302 RVA: 0x00224623 File Offset: 0x00222823
		[DefaultValue(typeof(Color), "")]
		[Category("Behavior")]
		[Description("Gets or sets the fore color to the most important (frequent) item.")]
		public Color MaxColor
		{
			get
			{
				return (Color)(this.ViewState["MaxColor"] ?? Color.Empty);
			}
			set
			{
				this.ViewState["MaxColor"] = value;
			}
		}

		// Token: 0x17003092 RID: 12434
		// (get) Token: 0x06009987 RID: 39303 RVA: 0x0022463B File Offset: 0x0022283B
		// (set) Token: 0x06009988 RID: 39304 RVA: 0x00224660 File Offset: 0x00222860
		[Description("Gets or sets the fore color to the least important (frequent) item.")]
		[Category("Behavior")]
		[DefaultValue(typeof(Color), "")]
		public Color MinColor
		{
			get
			{
				return (Color)(this.ViewState["MinColor"] ?? Color.Empty);
			}
			set
			{
				this.ViewState["MinColor"] = value;
			}
		}

		// Token: 0x17003093 RID: 12435
		// (get) Token: 0x06009989 RID: 39305 RVA: 0x00224678 File Offset: 0x00222878
		// (set) Token: 0x0600998A RID: 39306 RVA: 0x002246A7 File Offset: 0x002228A7
		[Description("Gets or sets the font-size to the most important (frequent) item.")]
		[DefaultValue(typeof(Unit), "1.7em")]
		[Category("Behavior")]
		[ClientControlProperty]
		public Unit MaxFontSize
		{
			get
			{
				return (Unit)(this.ViewState["MaxFontSize"] ?? new Unit(1.7, UnitType.Em));
			}
			set
			{
				this.ViewState["MaxFontSize"] = value;
			}
		}

		// Token: 0x17003094 RID: 12436
		// (get) Token: 0x0600998B RID: 39307 RVA: 0x002246BF File Offset: 0x002228BF
		// (set) Token: 0x0600998C RID: 39308 RVA: 0x002246EE File Offset: 0x002228EE
		[DefaultValue(typeof(Unit), "0.8em")]
		[Description("Gets or sets the font-size to the least important (frequent) item.")]
		[Category("Behavior")]
		[ClientControlProperty]
		public Unit MinFontSize
		{
			get
			{
				return (Unit)(this.ViewState["MinFontSize"] ?? new Unit(0.8, UnitType.Em));
			}
			set
			{
				this.ViewState["MinFontSize"] = value;
			}
		}

		// Token: 0x17003095 RID: 12437
		// (get) Token: 0x0600998D RID: 39309 RVA: 0x00224706 File Offset: 0x00222906
		// (set) Token: 0x0600998E RID: 39310 RVA: 0x0022472F File Offset: 0x0022292F
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(0.0)]
		[Description("Gets or sets the minimal weight a TagCloud item could have.")]
		public double MinimalWeightAllowed
		{
			get
			{
				return (double)(this.ViewState["MinimalWeightAllowed"] ?? 0.0);
			}
			set
			{
				this.ViewState["MinimalWeightAllowed"] = value;
				this._shouldRetrieveTextFromSource = true;
			}
		}

		// Token: 0x17003096 RID: 12438
		// (get) Token: 0x0600998F RID: 39311 RVA: 0x0022474E File Offset: 0x0022294E
		// (set) Token: 0x06009990 RID: 39312 RVA: 0x0022476F File Offset: 0x0022296F
		[DefaultValue(0)]
		[Description("Gets or sets the number of visible items in the cloud.")]
		[Category("Behavior")]
		[ClientControlProperty]
		public int MaxNumberOfItems
		{
			get
			{
				return (int)(this.ViewState["MaxNumberOfItems"] ?? 0);
			}
			set
			{
				this.ViewState["MaxNumberOfItems"] = value;
				this._shouldRetrieveTextFromSource = true;
			}
		}

		// Token: 0x17003097 RID: 12439
		// (get) Token: 0x06009991 RID: 39313 RVA: 0x0022478E File Offset: 0x0022298E
		// (set) Token: 0x06009992 RID: 39314 RVA: 0x002247AE File Offset: 0x002229AE
		[Description("Gets or sets the target window or frame to display the new content when the TagCloud item is clicked.")]
		[DefaultValue("")]
		[TypeConverter(typeof(TargetConverter))]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("target")]
		public string Target
		{
			get
			{
				return ((string)this.ViewState["Target"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x17003098 RID: 12440
		// (get) Token: 0x06009993 RID: 39315 RVA: 0x002247C1 File Offset: 0x002229C1
		// (set) Token: 0x06009994 RID: 39316 RVA: 0x002247E2 File Offset: 0x002229E2
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Must be used with MaxNumberOfItems property. Gets or sets a bool value indicating whether the [MaxNumberOfItems] visible items will be the ones with the biggest weight, or the ones that occur first in the DataSource. The default value is true.")]
		public bool TakeTopWeightedItems
		{
			get
			{
				return (bool)(this.ViewState["TakeTopWeightedItems"] ?? false);
			}
			set
			{
				this.ViewState["TakeTopWeightedItems"] = value;
				this._shouldRetrieveTextFromSource = true;
			}
		}

		// Token: 0x17003099 RID: 12441
		// (get) Token: 0x06009995 RID: 39317 RVA: 0x00224801 File Offset: 0x00222A01
		// (set) Token: 0x06009996 RID: 39318 RVA: 0x00224821 File Offset: 0x00222A21
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("The URL to post to when an item is clicked.")]
		[UrlProperty("*.aspx")]
		[Themeable(false)]
		public string PostBackUrl
		{
			get
			{
				return ((string)this.ViewState["PostBackUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x1700309A RID: 12442
		// (get) Token: 0x06009997 RID: 39319 RVA: 0x00224834 File Offset: 0x00222A34
		// (set) Token: 0x06009998 RID: 39320 RVA: 0x00224855 File Offset: 0x00222A55
		[Category("Behavior")]
		[Description("Gets or sets a bool value indicating whether the item weight will be rendered. It is rendered right next to the item's text.")]
		[ClientControlProperty]
		[DefaultValue(false)]
		public bool RenderItemWeight
		{
			get
			{
				return (bool)(this.ViewState["RenderItemWeight"] ?? false);
			}
			set
			{
				this.ViewState["RenderItemWeight"] = value;
			}
		}

		// Token: 0x1700309B RID: 12443
		// (get) Token: 0x06009999 RID: 39321 RVA: 0x0022486D File Offset: 0x00222A6D
		// (set) Token: 0x0600999A RID: 39322 RVA: 0x0022488E File Offset: 0x00222A8E
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Gets or sets a value indicating how the TagCloud items will be sorted.")]
		[DefaultValue(TagCloudSorting.NotSorted)]
		public TagCloudSorting Sorting
		{
			get
			{
				return (TagCloudSorting)(this.ViewState["Sorting"] ?? TagCloudSorting.NotSorted);
			}
			set
			{
				this.ViewState["Sorting"] = value;
			}
		}

		// Token: 0x1700309C RID: 12444
		// (get) Token: 0x0600999B RID: 39323 RVA: 0x002248A6 File Offset: 0x00222AA6
		// (set) Token: 0x0600999C RID: 39324 RVA: 0x002248C6 File Offset: 0x00222AC6
		[DefaultValue("")]
		[Description("Gets or sets the punctuation characters that will not be included in the TagCloud, when generated from text source.")]
		[Category("Generate TagCloud from Text Source")]
		public string PunctuationCharacters
		{
			get
			{
				return ((string)this.ViewState["PunctuationCharacters"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["PunctuationCharacters"] = value;
				this._shouldRetrieveTextFromSource = true;
			}
		}

		// Token: 0x1700309D RID: 12445
		// (get) Token: 0x0600999D RID: 39325 RVA: 0x002248E0 File Offset: 0x00222AE0
		// (set) Token: 0x0600999E RID: 39326 RVA: 0x00224900 File Offset: 0x00222B00
		[Description("Gets or sets the punctuation characters that will be considered valid (i.e. they should be considered as a character of the word), if they appear between alphanumeric characters. For example the following words are valid, although they have punctuation characters: ASP.NET, web-site, telerik.com")]
		[Category("Generate TagCloud from Text Source")]
		[DefaultValue(".-_")]
		public string PunctuationCharactersValid
		{
			get
			{
				return ((string)this.ViewState["PunctuationCharactersValid"]) ?? ".-_";
			}
			set
			{
				this.ViewState["PunctuationCharactersValid"] = value;
				this._shouldRetrieveTextFromSource = true;
			}
		}

		// Token: 0x1700309E RID: 12446
		// (get) Token: 0x0600999F RID: 39327 RVA: 0x0022491A File Offset: 0x00222B1A
		// (set) Token: 0x060099A0 RID: 39328 RVA: 0x0022493B File Offset: 0x00222B3B
		[TypeConverter(typeof(ListConverter))]
		[Description("Gets or sets the array of words that will be excluded from the TagCloud, when the cloud is generated from a text source.")]
		[DefaultValue("a,about,after,all,also,an,and,are,as,at,be,been,but,by,can,could,did,do,does,each,for,from,get,had,has,have,he,her,him,his,how,i,if,in,into,is,it,its,just,me,more,most,my,not,of,on,or,our,said,see,shall,she,should,so,some,than,that,the,their,there,they,this,those,to,up,used,was,we,were,what,when,which,while,who,why,will,with,would,you,your")]
		[Category("Generate TagCloud from Text Source")]
		public string[] WordsToExclude
		{
			get
			{
				return (string[])(this.ViewState["WordsToEscape"] ?? this.wordsToEscape);
			}
			set
			{
				this.ViewState["WordsToEscape"] = value;
				this._shouldRetrieveTextFromSource = true;
			}
		}

		// Token: 0x1700309F RID: 12447
		// (get) Token: 0x060099A1 RID: 39329 RVA: 0x00224955 File Offset: 0x00222B55
		// (set) Token: 0x060099A2 RID: 39330 RVA: 0x00224975 File Offset: 0x00222B75
		[Category("Generate TagCloud from Text Source")]
		[DefaultValue("")]
		[Description("Gets or sets text from which a weighted cloud will be generated. Most frequent words are more important.")]
		public string Text
		{
			get
			{
				return ((string)this.ViewState["Text"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
				this._shouldRetrieveTextFromSource = true;
			}
		}

		// Token: 0x170030A0 RID: 12448
		// (get) Token: 0x060099A3 RID: 39331 RVA: 0x0022498F File Offset: 0x00222B8F
		// (set) Token: 0x060099A4 RID: 39332 RVA: 0x002249AF File Offset: 0x00222BAF
		[Category("Generate TagCloud from Text Source")]
		[DefaultValue("")]
		[Description("Gets or sets the text (.TXT) file from which text will be retrieved to generate tags.")]
		public string TextFile
		{
			get
			{
				return ((string)this.ViewState["TextFile"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["TextFile"] = value;
				this._shouldRetrieveTextFromSource = true;
			}
		}

		// Token: 0x170030A1 RID: 12449
		// (get) Token: 0x060099A5 RID: 39333 RVA: 0x002249C9 File Offset: 0x00222BC9
		// (set) Token: 0x060099A6 RID: 39334 RVA: 0x002249E9 File Offset: 0x00222BE9
		[UrlProperty]
		[DefaultValue("")]
		[Category("Generate TagCloud from Text Source")]
		[Description("Gets or sets the URL from which text will be retrieved to generate tags.")]
		public string TextUrl
		{
			get
			{
				return ((string)this.ViewState["TextUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["TextUrl"] = value;
				this._shouldRetrieveTextFromSource = true;
			}
		}

		// Token: 0x170030A2 RID: 12450
		// (get) Token: 0x060099A7 RID: 39335 RVA: 0x00224A03 File Offset: 0x00222C03
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The web service to be used for populating tad cloud items.")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		public WebServiceSettings WebServiceSettings
		{
			get
			{
				if (this._webServiceSettings == null)
				{
					this._webServiceSettings = new WebServiceSettings(this.ViewState);
				}
				return this._webServiceSettings;
			}
		}

		// Token: 0x060099A9 RID: 39337 RVA: 0x00224A24 File Offset: 0x00222C24
		// Note: this type is marked as 'beforefieldinit'.
		static RadTagCloud()
		{
			RadTagCloud.ItemDataBoundEvent = new object();
			RadTagCloud.ItemClickEvent = new object();
		}

		// Token: 0x04002B9D RID: 11165
		private bool generateFromText;

		// Token: 0x04002B9E RID: 11166
		private bool originalEnabled = true;

		// Token: 0x04002B9F RID: 11167
		private RadTagCloudItemCollection items;

		// Token: 0x04002BA2 RID: 11170
		private double _minimalWeight;

		// Token: 0x04002BA3 RID: 11171
		private double _maximalWeight = 100.0;

		// Token: 0x04002BA4 RID: 11172
		private SortedList<RadTagCloudItem, double> _listOfSortedItems;

		// Token: 0x04002BA5 RID: 11173
		private RadTagCloudItemCollection _items;

		// Token: 0x04002BA6 RID: 11174
		private string[] wordsToEscape = new string[]
		{
			"a",
			"about",
			"after",
			"all",
			"also",
			"an",
			"and",
			"are",
			"as",
			"at",
			"be",
			"been",
			"but",
			"by",
			"can",
			"could",
			"did",
			"do",
			"does",
			"each",
			"for",
			"from",
			"get",
			"had",
			"has",
			"have",
			"he",
			"her",
			"him",
			"his",
			"how",
			"i",
			"if",
			"in",
			"into",
			"is",
			"it",
			"its",
			"just",
			"me",
			"more",
			"most",
			"my",
			"not",
			"of",
			"on",
			"or",
			"our",
			"said",
			"see",
			"shall",
			"she",
			"should",
			"so",
			"some",
			"than",
			"that",
			"the",
			"their",
			"there",
			"they",
			"this",
			"those",
			"to",
			"up",
			"used",
			"was",
			"we",
			"were",
			"what",
			"when",
			"which",
			"while",
			"who",
			"why",
			"will",
			"with",
			"would",
			"you",
			"your"
		};

		// Token: 0x04002BA7 RID: 11175
		private WebServiceSettings _webServiceSettings;

		// Token: 0x02000FA1 RID: 4001
		private class WeightComparerDsc : IComparer<RadTagCloudItem>
		{
			// Token: 0x060099AA RID: 39338 RVA: 0x00224D3C File Offset: 0x00222F3C
			public int Compare(RadTagCloudItem _tagItem1, RadTagCloudItem _tagItem2)
			{
				if (_tagItem1.Index == _tagItem2.Index && _tagItem1.Weight == _tagItem2.Weight && _tagItem1.Text == _tagItem1.Text)
				{
					return 0;
				}
				if (_tagItem1.Weight >= _tagItem2.Weight)
				{
					return -1;
				}
				return 1;
			}
		}
	}
}
