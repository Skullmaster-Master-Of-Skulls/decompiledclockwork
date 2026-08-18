using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009DD RID: 2525
	public class QRCodeSettings : ObjectWithState
	{
		// Token: 0x0600609F RID: 24735 RVA: 0x0012C560 File Offset: 0x0012A760
		public QRCodeSettings(StateBag ownerStateBag) : base("qr_", ownerStateBag)
		{
		}

		// Token: 0x17001FC4 RID: 8132
		// (get) Token: 0x060060A0 RID: 24736 RVA: 0x0012C56E File Offset: 0x0012A76E
		// (set) Token: 0x060060A1 RID: 24737 RVA: 0x0012C599 File Offset: 0x0012A799
		[Category("Appearance")]
		[Description("Use this to specify size of the barcode dots in pixels.\n Use this to achieve sharp rendered QR Code.\n  You can use this in combination with Width=”” and Higth=”” and the QR will be sized according to the number of its dots.\n If you set DotSize to zero, the QR symbol will be resized to fill up the Width and Height. ")]
		[DefaultValue(3)]
		public int DotSize
		{
			get
			{
				if (base.ViewState["DotSize"] != null)
				{
					return (int)base.ViewState["DotSize"];
				}
				return 3;
			}
			set
			{
				if (value < -1)
				{
					base.ViewState["DotSize"] = 0;
					return;
				}
				base.ViewState["DotSize"] = value;
			}
		}

		// Token: 0x17001FC5 RID: 8133
		// (get) Token: 0x060060A2 RID: 24738 RVA: 0x0012C5CC File Offset: 0x0012A7CC
		// (set) Token: 0x060060A3 RID: 24739 RVA: 0x0012C5F7 File Offset: 0x0012A7F7
		[Description("Auto increases the Version depending on the text length")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool AutoIncreaseVersion
		{
			get
			{
				return base.ViewState["AutoIncreaseVersion"] == null || (bool)base.ViewState["AutoIncreaseVersion"];
			}
			set
			{
				base.ViewState["AutoIncreaseVersion"] = value;
			}
		}

		// Token: 0x17001FC6 RID: 8134
		// (get) Token: 0x060060A4 RID: 24740 RVA: 0x0012C60F File Offset: 0x0012A80F
		// (set) Token: 0x060060A5 RID: 24741 RVA: 0x0012C63A File Offset: 0x0012A83A
		[Category("Behavior")]
		[Description("There are four values available for this property - Alphanumeric, Numeric, Byte and Kanji. \n Essentially, this determines the sets of acceptable symbols - numbers, characters, etc.")]
		[DefaultValue(Modes.CodeMode.Byte)]
		public Modes.CodeMode Mode
		{
			get
			{
				if (base.ViewState["CodeMode"] != null)
				{
					return (Modes.CodeMode)base.ViewState["CodeMode"];
				}
				return Modes.CodeMode.Byte;
			}
			set
			{
				base.ViewState["CodeMode"] = value;
			}
		}

		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x060060A6 RID: 24742 RVA: 0x0012C652 File Offset: 0x0012A852
		// (set) Token: 0x060060A7 RID: 24743 RVA: 0x0012C67D File Offset: 0x0012A87D
		[DefaultValue(7)]
		[Category("Behavior")]
		[Description("This is an integer value, in the range from 1 to 40, representing the version which one desires to use. \nUsually, higher-version QR codes are used do accommodate larger amounts of data.")]
		public int Version
		{
			get
			{
				if (base.ViewState["Version"] != null)
				{
					return (int)base.ViewState["Version"];
				}
				return 7;
			}
			set
			{
				base.ViewState["Version"] = value;
			}
		}

		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x060060A8 RID: 24744 RVA: 0x0012C695 File Offset: 0x0012A895
		// (set) Token: 0x060060A9 RID: 24745 RVA: 0x0012C6C0 File Offset: 0x0012A8C0
		[Description("There are four possible values to choose from - L(Low), M(Medium), Q(Quartile), H(High). \nThese values allow for 7%, 15%, 25% and 30% recovery of symbol code words. \nAdditionally, choosing a higher version of error correction dedicates a larger portion of modules for error correction. \nThus, given two QR codes with the same sizes, the one with a lower error correction level would be able to accommodate more data. ")]
		[DefaultValue(Modes.ErrorCorrectionLevel.L)]
		[Category("Behavior")]
		public Modes.ErrorCorrectionLevel ErrorCorrectionLevel
		{
			get
			{
				if (base.ViewState["ErrorCorrectionLevel"] != null)
				{
					return (Modes.ErrorCorrectionLevel)base.ViewState["ErrorCorrectionLevel"];
				}
				return Modes.ErrorCorrectionLevel.L;
			}
			set
			{
				base.ViewState["ErrorCorrectionLevel"] = value;
			}
		}

		// Token: 0x17001FC9 RID: 8137
		// (get) Token: 0x060060AA RID: 24746 RVA: 0x0012C6D8 File Offset: 0x0012A8D8
		// (set) Token: 0x060060AB RID: 24747 RVA: 0x0012C703 File Offset: 0x0012A903
		[DefaultValue(Modes.ECIMode.None)]
		[Category("Behavior")]
		[Description("(Extended Channel Interpretations Encoding) property allows for additional data to be applied to the FNC1 data. \nPlease, keep in mind, that this is only applicable with FNC1Mode.FNC1SecondPosition. \nAdditionally, the acceptable data for this property is in the range {a-z}],{[A-Z} and {00-99}. \nDo not change the encoding if you plan to decode your barcodes on smartphones. \nSome readers are working with the default encoding only.")]
		public Modes.ECIMode ECI
		{
			get
			{
				if (base.ViewState["ECI"] != null)
				{
					return (Modes.ECIMode)base.ViewState["ECI"];
				}
				return Modes.ECIMode.None;
			}
			set
			{
				base.ViewState["ECI"] = value;
			}
		}

		// Token: 0x17001FCA RID: 8138
		// (get) Token: 0x060060AC RID: 24748 RVA: 0x0012C71B File Offset: 0x0012A91B
		// (set) Token: 0x060060AD RID: 24749 RVA: 0x0012C746 File Offset: 0x0012A946
		[DefaultValue(Modes.FNC1Mode.None)]
		[Description("This mode is used for messages containing data formatted either in accordance with the UCC/EAN Application Identifiers standard,\nor in accordance with a specific industry standard previously agreed with AIM International.")]
		[Category("Behavior")]
		public Modes.FNC1Mode FNC1
		{
			get
			{
				if (base.ViewState["FNC1"] != null)
				{
					return (Modes.FNC1Mode)base.ViewState["FNC1"];
				}
				return Modes.FNC1Mode.None;
			}
			set
			{
				base.ViewState["FNC1"] = value;
			}
		}

		// Token: 0x17001FCB RID: 8139
		// (get) Token: 0x060060AE RID: 24750 RVA: 0x0012C75E File Offset: 0x0012A95E
		// (set) Token: 0x060060AF RID: 24751 RVA: 0x0012C78D File Offset: 0x0012A98D
		[Description("This property allows for additional data to be applied to the FNC1 data. \nPlease, keep in mind, that this is only applicable with FNC1Mode.FNC1SecondPosition. \nAdditionally, the acceptable data for this property is in the range {a-z}],{[A-Z} and {00-99}.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string ApplicationIndicator
		{
			get
			{
				if (base.ViewState["ApplicationIndicator"] != null)
				{
					return (string)base.ViewState["ApplicationIndicator"];
				}
				return "";
			}
			set
			{
				base.ViewState["ApplicationIndicator"] = value;
			}
		}
	}
}
