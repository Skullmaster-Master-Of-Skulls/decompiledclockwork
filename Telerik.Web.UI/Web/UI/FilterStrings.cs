using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020018A9 RID: 6313
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class FilterStrings : LocalizationStrings
	{
		// Token: 0x0600F434 RID: 62516 RVA: 0x003788E1 File Offset: 0x00376AE1
		internal FilterStrings(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x1700499C RID: 18844
		// (get) Token: 0x0600F435 RID: 62517 RVA: 0x003788EA File Offset: 0x00376AEA
		// (set) Token: 0x0600F436 RID: 62518 RVA: 0x003788F7 File Offset: 0x00376AF7
		[NotifyParentProperty(true)]
		[DefaultValue("Apply")]
		internal string ApplyButtonText
		{
			get
			{
				return this.GetString("ApplyButtonText");
			}
			set
			{
				this.SetString("ApplyButtonText", value);
			}
		}

		// Token: 0x1700499D RID: 18845
		// (get) Token: 0x0600F437 RID: 62519 RVA: 0x00378905 File Offset: 0x00376B05
		// (set) Token: 0x0600F438 RID: 62520 RVA: 0x00378912 File Offset: 0x00376B12
		[NotifyParentProperty(true)]
		[DefaultValue("Add Expression")]
		internal string AddExpressionToolTip
		{
			get
			{
				return this.GetString("AddExpressionToolTip");
			}
			set
			{
				this.SetString("AddExpressionToolTip", value);
			}
		}

		// Token: 0x1700499E RID: 18846
		// (get) Token: 0x0600F439 RID: 62521 RVA: 0x00378920 File Offset: 0x00376B20
		// (set) Token: 0x0600F43A RID: 62522 RVA: 0x0037892D File Offset: 0x00376B2D
		[NotifyParentProperty(true)]
		[DefaultValue("Add Group")]
		internal string AddGroupToolTip
		{
			get
			{
				return this.GetString("AddGroupToolTip");
			}
			set
			{
				this.SetString("AddGroupToolTip", value);
			}
		}

		// Token: 0x1700499F RID: 18847
		// (get) Token: 0x0600F43B RID: 62523 RVA: 0x0037893B File Offset: 0x00376B3B
		// (set) Token: 0x0600F43C RID: 62524 RVA: 0x00378948 File Offset: 0x00376B48
		[DefaultValue("Remove Item")]
		[NotifyParentProperty(true)]
		internal string RemoveToolTip
		{
			get
			{
				return this.GetString("RemoveToolTip");
			}
			set
			{
				this.SetString("RemoveToolTip", value);
			}
		}

		// Token: 0x170049A0 RID: 18848
		// (get) Token: 0x0600F43D RID: 62525 RVA: 0x00378956 File Offset: 0x00376B56
		// (set) Token: 0x0600F43E RID: 62526 RVA: 0x00378963 File Offset: 0x00376B63
		[NotifyParentProperty(true)]
		[DefaultValue("And")]
		internal string BetweenDelimeterText
		{
			get
			{
				return this.GetString("BetweenDelimeterText");
			}
			set
			{
				this.SetString("BetweenDelimeterText", value);
			}
		}

		// Token: 0x170049A1 RID: 18849
		// (get) Token: 0x0600F43F RID: 62527 RVA: 0x00378971 File Offset: 0x00376B71
		// (set) Token: 0x0600F440 RID: 62528 RVA: 0x0037897E File Offset: 0x00376B7E
		[DefaultValue("And")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string GroupOperationAnd
		{
			get
			{
				return this.GetString("GroupOperationAnd");
			}
			set
			{
				this.SetString("GroupOperationAnd", value);
			}
		}

		// Token: 0x170049A2 RID: 18850
		// (get) Token: 0x0600F441 RID: 62529 RVA: 0x0037898C File Offset: 0x00376B8C
		// (set) Token: 0x0600F442 RID: 62530 RVA: 0x00378999 File Offset: 0x00376B99
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Or")]
		public string GroupOperationOr
		{
			get
			{
				return this.GetString("GroupOperationOr");
			}
			set
			{
				this.SetString("GroupOperationOr", value);
			}
		}

		// Token: 0x170049A3 RID: 18851
		// (get) Token: 0x0600F443 RID: 62531 RVA: 0x003789A7 File Offset: 0x00376BA7
		// (set) Token: 0x0600F444 RID: 62532 RVA: 0x003789B4 File Offset: 0x00376BB4
		[DefaultValue("Not And")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string GroupOperationNotAnd
		{
			get
			{
				return this.GetString("GroupOperationNotAnd");
			}
			set
			{
				this.SetString("GroupOperationNotAnd", value);
			}
		}

		// Token: 0x170049A4 RID: 18852
		// (get) Token: 0x0600F445 RID: 62533 RVA: 0x003789C2 File Offset: 0x00376BC2
		// (set) Token: 0x0600F446 RID: 62534 RVA: 0x003789CF File Offset: 0x00376BCF
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Not Or")]
		public string GroupOperationNotOr
		{
			get
			{
				return this.GetString("GroupOperationNotOr");
			}
			set
			{
				this.SetString("GroupOperationNotOr", value);
			}
		}

		// Token: 0x170049A5 RID: 18853
		// (get) Token: 0x0600F447 RID: 62535 RVA: 0x003789DD File Offset: 0x00376BDD
		// (set) Token: 0x0600F448 RID: 62536 RVA: 0x003789EA File Offset: 0x00376BEA
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Contains")]
		public string FilterFunctionContains
		{
			get
			{
				return this.GetString("FilterFunctionContains");
			}
			set
			{
				this.SetString("FilterFunctionContains", value);
			}
		}

		// Token: 0x170049A6 RID: 18854
		// (get) Token: 0x0600F449 RID: 62537 RVA: 0x003789F8 File Offset: 0x00376BF8
		// (set) Token: 0x0600F44A RID: 62538 RVA: 0x00378A05 File Offset: 0x00376C05
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("DoesNotContain")]
		public string FilterFunctionDoesNotContain
		{
			get
			{
				return this.GetString("FilterFunctionDoesNotContain");
			}
			set
			{
				this.SetString("FilterFunctionDoesNotContain", value);
			}
		}

		// Token: 0x170049A7 RID: 18855
		// (get) Token: 0x0600F44B RID: 62539 RVA: 0x00378A13 File Offset: 0x00376C13
		// (set) Token: 0x0600F44C RID: 62540 RVA: 0x00378A20 File Offset: 0x00376C20
		[Localizable(true)]
		[DefaultValue("StartsWith")]
		[NotifyParentProperty(true)]
		public string FilterFunctionStartsWith
		{
			get
			{
				return this.GetString("FilterFunctionStartsWith");
			}
			set
			{
				this.SetString("FilterFunctionStartsWith", value);
			}
		}

		// Token: 0x170049A8 RID: 18856
		// (get) Token: 0x0600F44D RID: 62541 RVA: 0x00378A2E File Offset: 0x00376C2E
		// (set) Token: 0x0600F44E RID: 62542 RVA: 0x00378A3B File Offset: 0x00376C3B
		[NotifyParentProperty(true)]
		[DefaultValue("EndsWith")]
		[Localizable(true)]
		public string FilterFunctionEndsWith
		{
			get
			{
				return this.GetString("FilterFunctionEndsWith");
			}
			set
			{
				this.SetString("FilterFunctionEndsWith", value);
			}
		}

		// Token: 0x170049A9 RID: 18857
		// (get) Token: 0x0600F44F RID: 62543 RVA: 0x00378A49 File Offset: 0x00376C49
		// (set) Token: 0x0600F450 RID: 62544 RVA: 0x00378A56 File Offset: 0x00376C56
		[DefaultValue("EqualTo")]
		[NotifyParentProperty(true)]
		public string FilterFunctionEqualTo
		{
			get
			{
				return this.GetString("FilterFunctionEqualTo");
			}
			set
			{
				this.SetString("FilterFunctionEqualTo", value);
			}
		}

		// Token: 0x170049AA RID: 18858
		// (get) Token: 0x0600F451 RID: 62545 RVA: 0x00378A64 File Offset: 0x00376C64
		// (set) Token: 0x0600F452 RID: 62546 RVA: 0x00378A71 File Offset: 0x00376C71
		[DefaultValue("NotEqualTo")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string FilterFunctionNotEqualTo
		{
			get
			{
				return this.GetString("FilterFunctionNotEqualTo");
			}
			set
			{
				this.SetString("FilterFunctionNotEqualTo", value);
			}
		}

		// Token: 0x170049AB RID: 18859
		// (get) Token: 0x0600F453 RID: 62547 RVA: 0x00378A7F File Offset: 0x00376C7F
		// (set) Token: 0x0600F454 RID: 62548 RVA: 0x00378A8C File Offset: 0x00376C8C
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("GreaterThan")]
		public string FilterFunctionGreaterThan
		{
			get
			{
				return this.GetString("FilterFunctionGreaterThan");
			}
			set
			{
				this.SetString("FilterFunctionGreaterThan", value);
			}
		}

		// Token: 0x170049AC RID: 18860
		// (get) Token: 0x0600F455 RID: 62549 RVA: 0x00378A9A File Offset: 0x00376C9A
		// (set) Token: 0x0600F456 RID: 62550 RVA: 0x00378AA7 File Offset: 0x00376CA7
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("LessThan")]
		public string FilterFunctionLessThan
		{
			get
			{
				return this.GetString("FilterFunctionLessThan");
			}
			set
			{
				this.SetString("FilterFunctionLessThan", value);
			}
		}

		// Token: 0x170049AD RID: 18861
		// (get) Token: 0x0600F457 RID: 62551 RVA: 0x00378AB5 File Offset: 0x00376CB5
		// (set) Token: 0x0600F458 RID: 62552 RVA: 0x00378AC2 File Offset: 0x00376CC2
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("GreaterThanOrEqualTo")]
		public string FilterFunctionGreaterThanOrEqualTo
		{
			get
			{
				return this.GetString("FilterFunctionGreaterThanOrEqualTo");
			}
			set
			{
				this.SetString("FilterFunctionGreaterThanOrEqualTo", value);
			}
		}

		// Token: 0x170049AE RID: 18862
		// (get) Token: 0x0600F459 RID: 62553 RVA: 0x00378AD0 File Offset: 0x00376CD0
		// (set) Token: 0x0600F45A RID: 62554 RVA: 0x00378ADD File Offset: 0x00376CDD
		[DefaultValue("LessThanOrEqualTo")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string FilterFunctionLessThanOrEqualTo
		{
			get
			{
				return this.GetString("FilterFunctionLessThanOrEqualTo");
			}
			set
			{
				this.SetString("FilterFunctionLessThanOrEqualTo", value);
			}
		}

		// Token: 0x170049AF RID: 18863
		// (get) Token: 0x0600F45B RID: 62555 RVA: 0x00378AEB File Offset: 0x00376CEB
		// (set) Token: 0x0600F45C RID: 62556 RVA: 0x00378AF8 File Offset: 0x00376CF8
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Between")]
		public string FilterFunctionBetween
		{
			get
			{
				return this.GetString("FilterFunctionBetween");
			}
			set
			{
				this.SetString("FilterFunctionBetween", value);
			}
		}

		// Token: 0x170049B0 RID: 18864
		// (get) Token: 0x0600F45D RID: 62557 RVA: 0x00378B06 File Offset: 0x00376D06
		// (set) Token: 0x0600F45E RID: 62558 RVA: 0x00378B13 File Offset: 0x00376D13
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("NotBetween")]
		public string FilterFunctionNotBetween
		{
			get
			{
				return this.GetString("FilterFunctionNotBetween");
			}
			set
			{
				this.SetString("FilterFunctionNotBetween", value);
			}
		}

		// Token: 0x170049B1 RID: 18865
		// (get) Token: 0x0600F45F RID: 62559 RVA: 0x00378B21 File Offset: 0x00376D21
		// (set) Token: 0x0600F460 RID: 62560 RVA: 0x00378B2E File Offset: 0x00376D2E
		[Localizable(true)]
		[DefaultValue("IsEmpty")]
		[NotifyParentProperty(true)]
		public string FilterFunctionIsEmpty
		{
			get
			{
				return this.GetString("FilterFunctionIsEmpty");
			}
			set
			{
				this.SetString("FilterFunctionIsEmpty", value);
			}
		}

		// Token: 0x170049B2 RID: 18866
		// (get) Token: 0x0600F461 RID: 62561 RVA: 0x00378B3C File Offset: 0x00376D3C
		// (set) Token: 0x0600F462 RID: 62562 RVA: 0x00378B49 File Offset: 0x00376D49
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("NotIsEmpty")]
		public string FilterFunctionNotIsEmpty
		{
			get
			{
				return this.GetString("FilterFunctionNotIsEmpty");
			}
			set
			{
				this.SetString("FilterFunctionNotIsEmpty", value);
			}
		}

		// Token: 0x170049B3 RID: 18867
		// (get) Token: 0x0600F463 RID: 62563 RVA: 0x00378B57 File Offset: 0x00376D57
		// (set) Token: 0x0600F464 RID: 62564 RVA: 0x00378B64 File Offset: 0x00376D64
		[DefaultValue("IsNull")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string FilterFunctionIsNull
		{
			get
			{
				return this.GetString("FilterFunctionIsNull");
			}
			set
			{
				this.SetString("FilterFunctionIsNull", value);
			}
		}

		// Token: 0x170049B4 RID: 18868
		// (get) Token: 0x0600F465 RID: 62565 RVA: 0x00378B72 File Offset: 0x00376D72
		// (set) Token: 0x0600F466 RID: 62566 RVA: 0x00378B7F File Offset: 0x00376D7F
		[DefaultValue("NotIsNull")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string FilterFunctionNotIsNull
		{
			get
			{
				return this.GetString("FilterFunctionNotIsNull");
			}
			set
			{
				this.SetString("FilterFunctionNotIsNull", value);
			}
		}

		// Token: 0x0600F467 RID: 62567 RVA: 0x00378B90 File Offset: 0x00376D90
		internal string RetrieveGroupLocalizationString(RadFilterGroupOperation group)
		{
			string result = string.Empty;
			switch (group)
			{
			case RadFilterGroupOperation.And:
				result = this.GroupOperationAnd;
				break;
			case RadFilterGroupOperation.Or:
				result = this.GroupOperationOr;
				break;
			case RadFilterGroupOperation.NotAnd:
				result = this.GroupOperationNotAnd;
				break;
			case RadFilterGroupOperation.NotOr:
				result = this.GroupOperationNotOr;
				break;
			}
			return result;
		}

		// Token: 0x0600F468 RID: 62568 RVA: 0x00378BE0 File Offset: 0x00376DE0
		internal string RetrieveFilterFunctionLocalizationString(RadFilterFunction function)
		{
			return this.GetString(string.Format("FilterFunction{0}", Enum.GetName(typeof(RadFilterFunction), function)));
		}

		// Token: 0x170049B5 RID: 18869
		// (get) Token: 0x0600F469 RID: 62569 RVA: 0x00378C14 File Offset: 0x00376E14
		// (set) Token: 0x0600F46A RID: 62570 RVA: 0x00378C21 File Offset: 0x00376E21
		[NotifyParentProperty(true)]
		[DefaultValue("Between")]
		[Localizable(true)]
		public string PreviewProviderBetweenText
		{
			get
			{
				return this.GetString("PreviewProviderBetweenText");
			}
			set
			{
				this.SetString("PreviewProviderBetweenText", value);
			}
		}

		// Token: 0x170049B6 RID: 18870
		// (get) Token: 0x0600F46B RID: 62571 RVA: 0x00378C2F File Offset: 0x00376E2F
		// (set) Token: 0x0600F46C RID: 62572 RVA: 0x00378C3C File Offset: 0x00376E3C
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("NotBetween")]
		public string PreviewProviderNotBetweenText
		{
			get
			{
				return this.GetString("PreviewProviderNotBetweenText");
			}
			set
			{
				this.SetString("PreviewProviderNotBetweenText", value);
			}
		}

		// Token: 0x170049B7 RID: 18871
		// (get) Token: 0x0600F46D RID: 62573 RVA: 0x00378C4A File Offset: 0x00376E4A
		// (set) Token: 0x0600F46E RID: 62574 RVA: 0x00378C57 File Offset: 0x00376E57
		[Localizable(true)]
		[DefaultValue("Contains")]
		[NotifyParentProperty(true)]
		public string PreviewProviderContainsText
		{
			get
			{
				return this.GetString("PreviewProviderContainsText");
			}
			set
			{
				this.SetString("PreviewProviderContainsText", value);
			}
		}

		// Token: 0x170049B8 RID: 18872
		// (get) Token: 0x0600F46F RID: 62575 RVA: 0x00378C65 File Offset: 0x00376E65
		// (set) Token: 0x0600F470 RID: 62576 RVA: 0x00378C72 File Offset: 0x00376E72
		[DefaultValue("Does Not Contain")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string PreviewProviderDoesNotContainText
		{
			get
			{
				return this.GetString("PreviewProviderDoesNotContainText");
			}
			set
			{
				this.SetString("PreviewProviderDoesNotContainText", value);
			}
		}

		// Token: 0x170049B9 RID: 18873
		// (get) Token: 0x0600F471 RID: 62577 RVA: 0x00378C80 File Offset: 0x00376E80
		// (set) Token: 0x0600F472 RID: 62578 RVA: 0x00378C8D File Offset: 0x00376E8D
		[NotifyParentProperty(true)]
		[DefaultValue("Ends With")]
		[Localizable(true)]
		public string PreviewProviderEndsWithText
		{
			get
			{
				return this.GetString("PreviewProviderEndsWithText");
			}
			set
			{
				this.SetString("PreviewProviderEndsWithText", value);
			}
		}

		// Token: 0x170049BA RID: 18874
		// (get) Token: 0x0600F473 RID: 62579 RVA: 0x00378C9B File Offset: 0x00376E9B
		// (set) Token: 0x0600F474 RID: 62580 RVA: 0x00378CA8 File Offset: 0x00376EA8
		[DefaultValue("Starts With")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string PreviewProviderStartsWithText
		{
			get
			{
				return this.GetString("PreviewProviderStartsWithText");
			}
			set
			{
				this.SetString("PreviewProviderStartsWithText", value);
			}
		}

		// Token: 0x170049BB RID: 18875
		// (get) Token: 0x0600F475 RID: 62581 RVA: 0x00378CB6 File Offset: 0x00376EB6
		// (set) Token: 0x0600F476 RID: 62582 RVA: 0x00378CC3 File Offset: 0x00376EC3
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Is Empty")]
		public string PreviewProviderIsEmptyText
		{
			get
			{
				return this.GetString("PreviewProviderIsEmptyText");
			}
			set
			{
				this.SetString("PreviewProviderIsEmptyText", value);
			}
		}

		// Token: 0x170049BC RID: 18876
		// (get) Token: 0x0600F477 RID: 62583 RVA: 0x00378CD1 File Offset: 0x00376ED1
		// (set) Token: 0x0600F478 RID: 62584 RVA: 0x00378CDE File Offset: 0x00376EDE
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Is Not Empty")]
		public string PreviewProviderNotIsEmptyText
		{
			get
			{
				return this.GetString("PreviewProviderNotIsEmptyText");
			}
			set
			{
				this.SetString("PreviewProviderNotIsEmptyText", value);
			}
		}

		// Token: 0x170049BD RID: 18877
		// (get) Token: 0x0600F479 RID: 62585 RVA: 0x00378CEC File Offset: 0x00376EEC
		// (set) Token: 0x0600F47A RID: 62586 RVA: 0x00378CF9 File Offset: 0x00376EF9
		[DefaultValue("Is Null")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string PreviewProviderIsNullText
		{
			get
			{
				return this.GetString("PreviewProviderIsNullText");
			}
			set
			{
				this.SetString("PreviewProviderIsNullText", value);
			}
		}

		// Token: 0x170049BE RID: 18878
		// (get) Token: 0x0600F47B RID: 62587 RVA: 0x00378D07 File Offset: 0x00376F07
		// (set) Token: 0x0600F47C RID: 62588 RVA: 0x00378D14 File Offset: 0x00376F14
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Is Not Null")]
		public string PreviewProviderNotIsNullText
		{
			get
			{
				return this.GetString("PreviewProviderNotIsNullText");
			}
			set
			{
				this.SetString("PreviewProviderNotIsNullText", value);
			}
		}

		// Token: 0x170049BF RID: 18879
		// (get) Token: 0x0600F47D RID: 62589 RVA: 0x00378D22 File Offset: 0x00376F22
		// (set) Token: 0x0600F47E RID: 62590 RVA: 0x00378D2F File Offset: 0x00376F2F
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("=")]
		public string PreviewProviderEqualToText
		{
			get
			{
				return this.GetString("PreviewProviderEqualToText");
			}
			set
			{
				this.SetString("PreviewProviderEqualToText", value);
			}
		}

		// Token: 0x170049C0 RID: 18880
		// (get) Token: 0x0600F47F RID: 62591 RVA: 0x00378D3D File Offset: 0x00376F3D
		// (set) Token: 0x0600F480 RID: 62592 RVA: 0x00378D4A File Offset: 0x00376F4A
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(">")]
		public string PreviewProviderGreaterThanText
		{
			get
			{
				return this.GetString("PreviewProviderGreaterThanText");
			}
			set
			{
				this.SetString("PreviewProviderGreaterThanText", value);
			}
		}

		// Token: 0x170049C1 RID: 18881
		// (get) Token: 0x0600F481 RID: 62593 RVA: 0x00378D58 File Offset: 0x00376F58
		// (set) Token: 0x0600F482 RID: 62594 RVA: 0x00378D65 File Offset: 0x00376F65
		[NotifyParentProperty(true)]
		[DefaultValue(">=")]
		[Localizable(true)]
		public string PreviewProviderGreaterThanOrEqualToText
		{
			get
			{
				return this.GetString("PreviewProviderGreaterThanOrEqualToText");
			}
			set
			{
				this.SetString("PreviewProviderGreaterThanOrEqualToText", value);
			}
		}

		// Token: 0x170049C2 RID: 18882
		// (get) Token: 0x0600F483 RID: 62595 RVA: 0x00378D73 File Offset: 0x00376F73
		// (set) Token: 0x0600F484 RID: 62596 RVA: 0x00378D80 File Offset: 0x00376F80
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("<")]
		public string PreviewProviderLessThanText
		{
			get
			{
				return this.GetString("PreviewProviderLessThanText");
			}
			set
			{
				this.SetString("PreviewProviderLessThanText", value);
			}
		}

		// Token: 0x170049C3 RID: 18883
		// (get) Token: 0x0600F485 RID: 62597 RVA: 0x00378D8E File Offset: 0x00376F8E
		// (set) Token: 0x0600F486 RID: 62598 RVA: 0x00378D9B File Offset: 0x00376F9B
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("<=")]
		public string PreviewProviderLessThanOrEqualToText
		{
			get
			{
				return this.GetString("PreviewProviderLessThanOrEqualToText");
			}
			set
			{
				this.SetString("PreviewProviderLessThanOrEqualToText", value);
			}
		}

		// Token: 0x170049C4 RID: 18884
		// (get) Token: 0x0600F487 RID: 62599 RVA: 0x00378DA9 File Offset: 0x00376FA9
		// (set) Token: 0x0600F488 RID: 62600 RVA: 0x00378DB6 File Offset: 0x00376FB6
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("<>")]
		public string PreviewProviderNotEqualToText
		{
			get
			{
				return this.GetString("PreviewProviderNotEqualToText");
			}
			set
			{
				this.SetString("PreviewProviderNotEqualToText", value);
			}
		}
	}
}
