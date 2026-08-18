using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000E4F RID: 3663
	public class InputPasswordStrengthSettings : ObjectWithState
	{
		// Token: 0x06008AE7 RID: 35559 RVA: 0x001FA439 File Offset: 0x001F8639
		public InputPasswordStrengthSettings(StateBag OwnerStateBag) : base("ps_", OwnerStateBag)
		{
		}

		// Token: 0x17002BE0 RID: 11232
		// (get) Token: 0x06008AE8 RID: 35560 RVA: 0x001FA448 File Offset: 0x001F8648
		// (set) Token: 0x06008AE9 RID: 35561 RVA: 0x001FA471 File Offset: 0x001F8671
		[DefaultValue(false)]
		public virtual bool ShowIndicator
		{
			get
			{
				object obj = base.ViewState["ShowIndicator"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ShowIndicator"] = value;
			}
		}

		// Token: 0x17002BE1 RID: 11233
		// (get) Token: 0x06008AEA RID: 35562 RVA: 0x001FA48C File Offset: 0x001F868C
		// (set) Token: 0x06008AEB RID: 35563 RVA: 0x001FA4BC File Offset: 0x001F86BC
		[DefaultValue("50;15;15;20")]
		public virtual string CalculationWeightings
		{
			get
			{
				object obj = base.ViewState["CalculationWeightings"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "50;15;15;20";
			}
			set
			{
				int num = 0;
				if (value != null)
				{
					string[] array = value.Split(new char[]
					{
						';'
					});
					foreach (string s in array)
					{
						int num2;
						if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
						{
							if (num2 < 0 || num2 > 100)
							{
								throw new ArgumentException("There must be 4 Calculation Weighting items separated by ';' which must total 100");
							}
							num += num2;
						}
					}
				}
				if (num == 100)
				{
					base.ViewState["CalculationWeightings"] = value;
					return;
				}
				throw new ArgumentException("There must be 4 Calculation Weighting items separated by ';' which must total 100");
			}
		}

		// Token: 0x17002BE2 RID: 11234
		// (get) Token: 0x06008AEC RID: 35564 RVA: 0x001FA54C File Offset: 0x001F874C
		// (set) Token: 0x06008AED RID: 35565 RVA: 0x001FA576 File Offset: 0x001F8776
		[DefaultValue(10)]
		public virtual int PreferredPasswordLength
		{
			get
			{
				object obj = base.ViewState["PreferredPasswordLength"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				if (value < 0)
				{
					base.ViewState["PreferredPasswordLength"] = 0;
					return;
				}
				base.ViewState["PreferredPasswordLength"] = value;
			}
		}

		// Token: 0x17002BE3 RID: 11235
		// (get) Token: 0x06008AEE RID: 35566 RVA: 0x001FA5AC File Offset: 0x001F87AC
		// (set) Token: 0x06008AEF RID: 35567 RVA: 0x001FA5D5 File Offset: 0x001F87D5
		[DefaultValue(2)]
		public virtual int MinimumNumericCharacters
		{
			get
			{
				object obj = base.ViewState["MinimumNumericCharacters"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 2;
			}
			set
			{
				if (value < 0)
				{
					base.ViewState["MinimumNumericCharacters"] = 0;
					return;
				}
				base.ViewState["MinimumNumericCharacters"] = value;
			}
		}

		// Token: 0x17002BE4 RID: 11236
		// (get) Token: 0x06008AF0 RID: 35568 RVA: 0x001FA608 File Offset: 0x001F8808
		// (set) Token: 0x06008AF1 RID: 35569 RVA: 0x001FA631 File Offset: 0x001F8831
		[DefaultValue(true)]
		public virtual bool RequiresUpperAndLowerCaseCharacters
		{
			get
			{
				object obj = base.ViewState["RequiresUpperAndLowerCaseCharacters"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["RequiresUpperAndLowerCaseCharacters"] = value;
			}
		}

		// Token: 0x17002BE5 RID: 11237
		// (get) Token: 0x06008AF2 RID: 35570 RVA: 0x001FA64C File Offset: 0x001F884C
		// (set) Token: 0x06008AF3 RID: 35571 RVA: 0x001FA675 File Offset: 0x001F8875
		[DefaultValue(2)]
		public virtual int MinimumLowerCaseCharacters
		{
			get
			{
				object obj = base.ViewState["MinimumLowerCaseCharacters"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 2;
			}
			set
			{
				if (value < 0)
				{
					base.ViewState["RequiresUpperAndLowerCaseCharacters"] = 0;
					return;
				}
				base.ViewState["MinimumLowerCaseCharacters"] = value;
			}
		}

		// Token: 0x17002BE6 RID: 11238
		// (get) Token: 0x06008AF4 RID: 35572 RVA: 0x001FA6A8 File Offset: 0x001F88A8
		// (set) Token: 0x06008AF5 RID: 35573 RVA: 0x001FA6D1 File Offset: 0x001F88D1
		[DefaultValue(2)]
		public virtual int MinimumUpperCaseCharacters
		{
			get
			{
				object obj = base.ViewState["MinimumUpperCaseCharacters"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 2;
			}
			set
			{
				if (value < 0)
				{
					base.ViewState["MinimumUpperCaseCharacters"] = 0;
					return;
				}
				base.ViewState["MinimumUpperCaseCharacters"] = value;
			}
		}

		// Token: 0x17002BE7 RID: 11239
		// (get) Token: 0x06008AF6 RID: 35574 RVA: 0x001FA704 File Offset: 0x001F8904
		// (set) Token: 0x06008AF7 RID: 35575 RVA: 0x001FA72D File Offset: 0x001F892D
		[DefaultValue(2)]
		public virtual int MinimumSymbolCharacters
		{
			get
			{
				object obj = base.ViewState["MinimumSymbolCharacters"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 2;
			}
			set
			{
				base.ViewState["MinimumSymbolCharacters"] = value;
			}
		}

		// Token: 0x17002BE8 RID: 11240
		// (get) Token: 0x06008AF8 RID: 35576 RVA: 0x001FA748 File Offset: 0x001F8948
		// (set) Token: 0x06008AF9 RID: 35577 RVA: 0x001FA775 File Offset: 0x001F8975
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public virtual string OnClientPasswordStrengthCalculating
		{
			get
			{
				object obj = base.ViewState["OnClientPasswordStrengthCalculating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["OnClientPasswordStrengthCalculating"] = value;
			}
		}

		// Token: 0x17002BE9 RID: 11241
		// (get) Token: 0x06008AFA RID: 35578 RVA: 0x001FA788 File Offset: 0x001F8988
		// (set) Token: 0x06008AFB RID: 35579 RVA: 0x001FA7B5 File Offset: 0x001F89B5
		[DefaultValue("Very Weak;Weak;Medium;Strong;Very Strong")]
		public virtual string TextStrengthDescriptions
		{
			get
			{
				object obj = base.ViewState["TextStrengthDescriptions"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "Very Weak;Weak;Medium;Strong;Very Strong";
			}
			set
			{
				base.ViewState["TextStrengthDescriptions"] = value;
			}
		}

		// Token: 0x17002BEA RID: 11242
		// (get) Token: 0x06008AFC RID: 35580 RVA: 0x001FA7C8 File Offset: 0x001F89C8
		// (set) Token: 0x06008AFD RID: 35581 RVA: 0x001FA7F5 File Offset: 0x001F89F5
		[DefaultValue("riStrengthBarL0;riStrengthBarL1;riStrengthBarL2;riStrengthBarL3;riStrengthBarL4;riStrengthBarL5;")]
		public virtual string TextStrengthDescriptionStyles
		{
			get
			{
				object obj = base.ViewState["TextStrengthDescriptionStyles"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "riStrengthBarL0;riStrengthBarL1;riStrengthBarL2;riStrengthBarL3;riStrengthBarL4;riStrengthBarL5;";
			}
			set
			{
				base.ViewState["TextStrengthDescriptionStyles"] = value;
			}
		}

		// Token: 0x17002BEB RID: 11243
		// (get) Token: 0x06008AFE RID: 35582 RVA: 0x001FA808 File Offset: 0x001F8A08
		// (set) Token: 0x06008AFF RID: 35583 RVA: 0x001FA835 File Offset: 0x001F8A35
		[DefaultValue("riStrengthBar")]
		public virtual string IndicatorElementBaseStyle
		{
			get
			{
				object obj = base.ViewState["IndicatorElementBaseStyle"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "riStrengthBar";
			}
			set
			{
				base.ViewState["IndicatorElementBaseStyle"] = value;
			}
		}

		// Token: 0x17002BEC RID: 11244
		// (get) Token: 0x06008B00 RID: 35584 RVA: 0x001FA848 File Offset: 0x001F8A48
		// (set) Token: 0x06008B01 RID: 35585 RVA: 0x001FA875 File Offset: 0x001F8A75
		[DefaultValue("")]
		public virtual string IndicatorElementID
		{
			get
			{
				object obj = base.ViewState["IndicatorElementID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["IndicatorElementID"] = value;
			}
		}

		// Token: 0x17002BED RID: 11245
		// (get) Token: 0x06008B02 RID: 35586 RVA: 0x001FA888 File Offset: 0x001F8A88
		// (set) Token: 0x06008B03 RID: 35587 RVA: 0x001FA8B8 File Offset: 0x001F8AB8
		[DefaultValue(typeof(Unit), "100px")]
		public virtual Unit IndicatorWidth
		{
			get
			{
				object obj = base.ViewState["IndicatorWidth"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Pixel(100);
			}
			set
			{
				if (value == Unit.Empty)
				{
					base.ViewState["IndicatorWidth"] = Unit.Pixel(100);
					return;
				}
				base.ViewState["IndicatorWidth"] = value;
			}
		}
	}
}
