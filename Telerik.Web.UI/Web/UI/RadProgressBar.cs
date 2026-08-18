using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000776 RID: 1910
	[RequiredScript(typeof(AnimationFramework))]
	[LightweightRendering]
	[RequiredScript(typeof(jQueryPlugins))]
	[ClientScriptResource("Telerik.Web.UI.RadProgressBar", "Telerik.Web.UI.ProgressBar.RadProgressBarScripts.js")]
	[EmbeddedSkin("ProgressBar", typeof(RadProgressBar))]
	[ToolboxBitmap(typeof(RadProgressBar), "Telerik.Web.UI.ProgressBar.png")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Designer("Telerik.Web.Design.RadProgressBarDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[EmbeddedSkin("ProgressBar", "Default", typeof(RadProgressBar))]
	[TelerikToolboxCategory("Visualization")]
	[ParseChildren(ChildrenAsProperties = true)]
	public class RadProgressBar : RadWebControl
	{
		// Token: 0x06004360 RID: 17248 RVA: 0x000D2C6C File Offset: 0x000D0E6C
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<ProgressBarType>(descriptor, "barType", this.BarType, ProgressBarType.Value);
			base.DescribeProperty<int>(descriptor, "chunksCount", this.ChunksCount, 5);
			base.DescribeProperty<double>(descriptor, "maxValue", this.MaxValue, 100.0);
			base.DescribeProperty<double>(descriptor, "minValue", this.MinValue, 0.0);
			base.DescribeProperty<ProgressBarOrientation>(descriptor, "orientation", this.Orientation, ProgressBarOrientation.Horizontal);
			base.DescribeProperty<bool>(descriptor, "reversed", this.Reversed, false);
			base.DescribeProperty<bool>(descriptor, "showLabel", this.ShowLabel, true);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x000D2D18 File Offset: 0x000D0F18
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "completed", this.ClientEvents.OnCompleted);
			RadWebControl.DescribeEvent(descriptor, "initialize", this.ClientEvents.OnInitialize);
			RadWebControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			RadWebControl.DescribeEvent(descriptor, "valueChanged", this.ClientEvents.OnValueChanged);
			RadWebControl.DescribeEvent(descriptor, "valueChanging", this.ClientEvents.OnValueChanging);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x06004362 RID: 17250 RVA: 0x000D2D9A File Offset: 0x000D0F9A
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06004363 RID: 17251 RVA: 0x000D2DA0 File Offset: 0x000D0FA0
		protected override string CssClassFormatString
		{
			get
			{
				List<string> list = new List<string>
				{
					"RadProgressBar",
					"RadProgressBar_{0}",
					string.Format("rpb{0}", this.Orientation.ToString())
				};
				if (!this.Enabled)
				{
					list.Add("rpbDisabled");
				}
				if (this.Indeterminate)
				{
					list.Add("rpbIndeterminate");
				}
				if (this.Reversed)
				{
					list.Add("rpbReversed");
				}
				if (this.RenderMode == RenderMode.Classic)
				{
					list.Add("rpbClassic");
				}
				return string.Join(" ", list.ToArray());
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06004364 RID: 17252 RVA: 0x000D2E48 File Offset: 0x000D1048
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06004365 RID: 17253 RVA: 0x000D2E4C File Offset: 0x000D104C
		// (set) Token: 0x06004366 RID: 17254 RVA: 0x000D2E6D File Offset: 0x000D106D
		[DefaultValue(ProgressBarType.Value)]
		[ClientControlProperty]
		[Category("Behavior")]
		public ProgressBarType BarType
		{
			get
			{
				return (ProgressBarType)(this.ViewState["BarType"] ?? ProgressBarType.Value);
			}
			set
			{
				this.ViewState["BarType"] = value;
			}
		}

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x000D2E85 File Offset: 0x000D1085
		// (set) Token: 0x06004368 RID: 17256 RVA: 0x000D2EA6 File Offset: 0x000D10A6
		[DefaultValue(5)]
		[ClientControlProperty]
		[Category("Behavior")]
		public int ChunksCount
		{
			get
			{
				return (int)(this.ViewState["ChunksCount"] ?? 5);
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ChunksCount"] = value;
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06004369 RID: 17257 RVA: 0x000D2ECD File Offset: 0x000D10CD
		// (set) Token: 0x0600436A RID: 17258 RVA: 0x000D2EF6 File Offset: 0x000D10F6
		[DefaultValue(0.0)]
		[Category("Behavior")]
		public double Value
		{
			get
			{
				return (double)(this.ViewState["Value"] ?? 0.0);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x000D2F0E File Offset: 0x000D110E
		// (set) Token: 0x0600436C RID: 17260 RVA: 0x000D2F37 File Offset: 0x000D1137
		[Category("Behavior")]
		[DefaultValue(0.0)]
		[ClientControlProperty]
		public double MinValue
		{
			get
			{
				return (double)(this.ViewState["MinValue"] ?? 0.0);
			}
			set
			{
				this.ViewState["MinValue"] = value;
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x0600436D RID: 17261 RVA: 0x000D2F4F File Offset: 0x000D114F
		// (set) Token: 0x0600436E RID: 17262 RVA: 0x000D2F78 File Offset: 0x000D1178
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(100)]
		public double MaxValue
		{
			get
			{
				return (double)(this.ViewState["MaxValue"] ?? 100.0);
			}
			set
			{
				this.ViewState["MaxValue"] = value;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x0600436F RID: 17263 RVA: 0x000D2F90 File Offset: 0x000D1190
		// (set) Token: 0x06004370 RID: 17264 RVA: 0x000D2FB1 File Offset: 0x000D11B1
		[ClientControlProperty]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool Reversed
		{
			get
			{
				return (bool)(this.ViewState["Reversed"] ?? false);
			}
			set
			{
				this.ViewState["Reversed"] = value;
			}
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06004371 RID: 17265 RVA: 0x000D2FC9 File Offset: 0x000D11C9
		// (set) Token: 0x06004372 RID: 17266 RVA: 0x000D2FEA File Offset: 0x000D11EA
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool Indeterminate
		{
			get
			{
				return (bool)(this.ViewState["Indeterminate"] ?? false);
			}
			set
			{
				this.ViewState["Indeterminate"] = value;
			}
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06004373 RID: 17267 RVA: 0x000D3002 File Offset: 0x000D1202
		// (set) Token: 0x06004374 RID: 17268 RVA: 0x000D3023 File Offset: 0x000D1223
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool ShowLabel
		{
			get
			{
				return (bool)(this.ViewState["ShowLabel"] ?? true);
			}
			set
			{
				this.ViewState["ShowLabel"] = value;
			}
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x000D303B File Offset: 0x000D123B
		// (set) Token: 0x06004376 RID: 17270 RVA: 0x000D305B File Offset: 0x000D125B
		[Category("Behavior")]
		[DefaultValue("")]
		public string Label
		{
			get
			{
				return (string)(this.ViewState["Label"] ?? "");
			}
			set
			{
				this.ViewState["Label"] = value;
			}
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06004377 RID: 17271 RVA: 0x000D306E File Offset: 0x000D126E
		// (set) Token: 0x06004378 RID: 17272 RVA: 0x000D308F File Offset: 0x000D128F
		[DefaultValue(ProgressBarOrientation.Horizontal)]
		[ClientControlProperty]
		[Category("Behavior")]
		public ProgressBarOrientation Orientation
		{
			get
			{
				return (ProgressBarOrientation)(this.ViewState["Orientation"] ?? ProgressBarOrientation.Horizontal);
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06004379 RID: 17273 RVA: 0x000D30A7 File Offset: 0x000D12A7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ProgressBarAnimationSettings AnimationSettings
		{
			get
			{
				if (this._animationSettings == null)
				{
					this._animationSettings = new ProgressBarAnimationSettings();
				}
				return this._animationSettings;
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x000D30C2 File Offset: 0x000D12C2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ProgressBarClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new ProgressBarClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x0600437B RID: 17275 RVA: 0x000D30DD File Offset: 0x000D12DD
		protected HtmlTextWriterStyle ProgressProperty
		{
			get
			{
				if (this.Orientation != ProgressBarOrientation.Vertical)
				{
					return HtmlTextWriterStyle.Width;
				}
				return HtmlTextWriterStyle.Height;
			}
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x000D30ED File Offset: 0x000D12ED
		private double CurrentValue
		{
			get
			{
				if (this.Value < this.MinValue)
				{
					return this.MinValue;
				}
				if (this.Value > this.MaxValue)
				{
					return this.MaxValue;
				}
				return this.Value;
			}
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x000D3120 File Offset: 0x000D1320
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new ProgressBarAnimationSettingsConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			if (!this.AnimationSettings.IsDefault)
			{
				descriptor.AddScriptProperty("animationSettings", javaScriptSerializer.Serialize(this.AnimationSettings));
			}
			if (!this.Enabled)
			{
				descriptor.AddProperty("enabled", false);
			}
			if (!this.Indeterminate)
			{
				descriptor.AddProperty("value", this.Value);
			}
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x000D31AF File Offset: 0x000D13AF
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (!this.Indeterminate)
			{
				if (this.BarType == ProgressBarType.Chunk)
				{
					this.RenderChunkContents(writer);
					return;
				}
				this.RenderProgressContents(writer);
			}
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x000D31D8 File Offset: 0x000D13D8
		private void RenderProgressContents(HtmlTextWriter writer)
		{
			this.RenderLabel(writer, false);
			if (this.CurrentValue == this.MinValue)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			string value = (this.CurrentValue != this.MaxValue) ? "rpbStateSelected" : "rpbStateSelected rpbStateCompleted";
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.AddStyleAttribute(this.ProgressProperty, string.Format("{0}%", this.CalculatePercentage()).Replace(",", "."));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderLabel(writer, true);
			writer.RenderEndTag();
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x000D3274 File Offset: 0x000D1474
		private void RenderLabel(HtmlTextWriter writer, bool addWrapperWidth = false)
		{
			if (addWrapperWidth && this.CurrentValue != this.MinValue && this.CurrentValue != 0.0)
			{
				double num = Math.Round(10000.0 / this.CalculatePercentage(), 3);
				writer.AddStyleAttribute(this.ProgressProperty, string.Format("{0}%", num).Replace(",", "."));
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpbLabelWrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (!this.ShowLabel)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpbLabel");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (!string.IsNullOrEmpty(this.Label))
			{
				writer.Write(this.Label);
			}
			else if (this.BarType == ProgressBarType.Percent)
			{
				writer.Write(string.Format("{0}%", Math.Round(this.CalculatePercentage(), 3)));
			}
			else
			{
				writer.Write(this.CurrentValue);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x000D3383 File Offset: 0x000D1583
		private void RenderChunkContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpbChunksWrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			this.RenderChunks(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x000D33A8 File Offset: 0x000D15A8
		private void RenderChunks(HtmlTextWriter writer)
		{
			this.InitSelectedChunksCount();
			for (int i = 0; i < this.ChunksCount; i++)
			{
				this.RenderChunk(writer, i);
			}
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x000D33D4 File Offset: 0x000D15D4
		private void InitSelectedChunksCount()
		{
			int num = 100 / this.ChunksCount * 100 / 100;
			this._selectedChunksCount = ((this.CurrentValue == 0.0) ? 0 : ((int)Math.Floor(this.CalculatePercentage() / (double)num)));
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x000D341C File Offset: 0x000D161C
		private void RenderChunk(HtmlTextWriter writer, int chunkIndex)
		{
			writer.AddStyleAttribute(this.ProgressProperty, string.Format("{0}%", this.GetChunkSize(chunkIndex)));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetChunkCssClass(chunkIndex));
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.RenderEndTag();
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x000D3468 File Offset: 0x000D1668
		private double GetChunkSize(int chunkIndex)
		{
			double num = Math.Round(100.0 / (double)this.ChunksCount, 4);
			int num2 = this.ChunksCount - 1;
			if (chunkIndex == num2)
			{
				num = 100.0 - num * (double)num2;
			}
			return num;
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x000D34AC File Offset: 0x000D16AC
		private string GetChunkCssClass(int chunkIndex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("rpbChunk");
			if (this.ChunkIsSelected(chunkIndex))
			{
				stringBuilder.Append(" rpbStateSelected");
			}
			else
			{
				stringBuilder.Append(" rpbStateDefault");
			}
			if (chunkIndex == 0)
			{
				stringBuilder.Append(" rpbChunkFirst");
			}
			if (chunkIndex == this.ChunksCount - 1)
			{
				stringBuilder.Append(" rpbChunkLast");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x000D351C File Offset: 0x000D171C
		private bool ChunkIsSelected(int chunkIndex)
		{
			int selectedChunksCount = this._selectedChunksCount;
			if (this.SelectedChunksAreFirst())
			{
				return selectedChunksCount != 0 && chunkIndex < selectedChunksCount;
			}
			if (this.SelectedChunksAreLast())
			{
				int num = this.ChunksCount - selectedChunksCount;
				return selectedChunksCount == this.ChunksCount || (num != 0 && chunkIndex >= num);
			}
			return false;
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x000D356C File Offset: 0x000D176C
		private bool SelectedChunksAreFirst()
		{
			return (this.Orientation == ProgressBarOrientation.Horizontal && !this.Reversed) || (this.Orientation == ProgressBarOrientation.Vertical && this.Reversed);
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x000D3591 File Offset: 0x000D1791
		private bool SelectedChunksAreLast()
		{
			return (this.Orientation == ProgressBarOrientation.Horizontal && this.Reversed) || (this.Orientation == ProgressBarOrientation.Vertical && !this.Reversed);
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x000D35BC File Offset: 0x000D17BC
		private double CalculatePercentage()
		{
			double num = Math.Abs((this.MaxValue - this.MinValue) / 100.0);
			if (num != 0.0)
			{
				return Math.Abs((this.CurrentValue - this.MinValue) / num);
			}
			return 0.0;
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x000D3610 File Offset: 0x000D1810
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			ProgressBarClientState progressBarClientState = null;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				progressBarClientState = javaScriptSerializer.Deserialize<ProgressBarClientState>(text);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (progressBarClientState == null)
			{
				return false;
			}
			bool result = false;
			if (this.Value != progressBarClientState.Value)
			{
				this.Value = progressBarClientState.Value;
				result = true;
			}
			if (this.Indeterminate != progressBarClientState.Indeterminate)
			{
				this.Indeterminate = progressBarClientState.Indeterminate;
				result = true;
			}
			return result;
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x000D36A8 File Offset: 0x000D18A8
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ClientEvents).LoadViewState(array[num++]);
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x000D36E0 File Offset: 0x000D18E0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState()
			};
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x000D370E File Offset: 0x000D190E
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
		}

		// Token: 0x040011DD RID: 4573
		private ProgressBarAnimationSettings _animationSettings;

		// Token: 0x040011DE RID: 4574
		private ProgressBarClientEvents _clientEvents;

		// Token: 0x040011DF RID: 4575
		private int _selectedChunksCount;
	}
}
