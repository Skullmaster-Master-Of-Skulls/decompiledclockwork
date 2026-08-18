using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x0200036A RID: 874
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	public class GridMobileRatingColumnEditor : GridTextColumnEditor
	{
		// Token: 0x06001E10 RID: 7696 RVA: 0x0005DA12 File Offset: 0x0005BC12
		public GridMobileRatingColumnEditor()
		{
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x0005DA1A File Offset: 0x0005BC1A
		public GridMobileRatingColumnEditor(GridRatingColumn owner)
		{
			this.owner = owner;
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x0005DA29 File Offset: 0x0005BC29
		public override void SetOwner(IGridEditableColumn owner)
		{
			this.owner = (owner as GridRatingColumn);
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x0005DA38 File Offset: 0x0005BC38
		// (set) Token: 0x06001E14 RID: 7700 RVA: 0x0005DA65 File Offset: 0x0005BC65
		public decimal Value
		{
			get
			{
				string value = this.TextBoxControl.Text;
				if (string.IsNullOrEmpty(value))
				{
					value = "0";
				}
				return Convert.ToDecimal(value);
			}
			set
			{
				this.TextBoxControl.Text = value.ToString();
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x0005DA79 File Offset: 0x0005BC79
		// (set) Token: 0x06001E16 RID: 7702 RVA: 0x0005DA86 File Offset: 0x0005BC86
		public override string Text
		{
			get
			{
				return this.TextBoxControl.Text;
			}
			set
			{
				this.TextBoxControl.Text = value;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x0005DA94 File Offset: 0x0005BC94
		public override bool IsInitialized
		{
			get
			{
				return this._textBox != null;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x0005DAA2 File Offset: 0x0005BCA2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TextBox TextBoxControl
		{
			get
			{
				this.EnsureControlsCreated();
				return this._textBox;
			}
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x0005DAB0 File Offset: 0x0005BCB0
		protected override void CreateControls()
		{
			this._textBox = new TextBox();
			this._textBox.Attributes.Add("type", "range");
			this._textBox.Attributes.Add("max", this.owner.ItemCount.ToString());
			switch (this.owner.Precision)
			{
			case RatingPrecision.Item:
				this._textBox.Attributes.Add("step", "1");
				break;
			case RatingPrecision.Half:
				this._textBox.Attributes.Add("step", "0.5");
				break;
			}
			if (this.owner != null)
			{
				this._textBox.ID = string.Format("TB_{0}", this.owner.UniqueName);
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0005DB84 File Offset: 0x0005BD84
		protected override void AddControlsToContainer()
		{
			this.TextBoxControl.ApplyStyle(this.TextBoxStyle);
			this.ContainerControl.Controls.Add(this.TextBoxControl);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0005DBAD File Offset: 0x0005BDAD
		protected override void LoadControlsFromContainer()
		{
			this._textBox = (this.ContainerControl.FindControl(string.Format("TB_{0}", this.owner.UniqueName)) as TextBox);
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x0005DBDA File Offset: 0x0005BDDA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style TextBoxStyle
		{
			get
			{
				if (this._textBoxStyle == null)
				{
					this._textBoxStyle = new Style(this.ViewState);
				}
				return this._textBoxStyle;
			}
		}

		// Token: 0x0400076E RID: 1902
		private TextBox _textBox;

		// Token: 0x0400076F RID: 1903
		private GridRatingColumn owner;

		// Token: 0x04000770 RID: 1904
		private Style _textBoxStyle;
	}
}
