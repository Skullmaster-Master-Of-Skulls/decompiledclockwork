using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000079 RID: 121
	public class DynamicControlWrapper_RichTextbox : DynamicControlWrapper_Base
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0004821C File Offset: 0x0004721C
		public DynamicControlWrapper_RichTextbox(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00048228 File Offset: 0x00047228
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00048245 File Offset: 0x00047245
		[Category("Display")]
		[Description("Indicates the number of rows this textbox should contain.  Use -1 to indicate it should fill it's container vertically.")]
		public int MultilineCount
		{
			get
			{
				return this.dynamicControl.Setting1;
			}
			set
			{
				this.dynamicControl.Setting1 = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00048258 File Offset: 0x00047258
		[ReadOnly(true)]
		[Description("Indicates whether the data for this textbox is encrypted.")]
		[Category("Design")]
		public bool Encrypted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0004826C File Offset: 0x0004726C
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0004828C File Offset: 0x0004728C
		[Category("Behaviour")]
		[Description("If true the spell checker will be disabled.")]
		public bool DontUseSpellChecker
		{
			get
			{
				return this.dynamicControl.Setting4 > 0;
			}
			set
			{
				this.dynamicControl.Setting4 = (value ? 1 : 0);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000482A4 File Offset: 0x000472A4
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x000482C1 File Offset: 0x000472C1
		[Category("Display")]
		[Description("Indicates the number of characters wide this textbox should be.")]
		public int CharacterWidth
		{
			get
			{
				return this.dynamicControl.Setting2;
			}
			set
			{
				this.dynamicControl.Setting2 = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000482D4 File Offset: 0x000472D4
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x000482F1 File Offset: 0x000472F1
		[Description("A default value to use for new data.")]
		[Category("Behaviour")]
		public string DefaultValue
		{
			get
			{
				return this.dynamicControl.DefaultValueString;
			}
			set
			{
				this.dynamicControl.DefaultValueString = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00048304 File Offset: 0x00047304
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00048321 File Offset: 0x00047321
		[Category("Behaviour")]
		[Description("How will users will be able to enter text and modify text.")]
		public TextBoxEnterModifyBehaviour TextBoxEnterModifyBehaviour
		{
			get
			{
				return (TextBoxEnterModifyBehaviour)this.dynamicControl.DefaultValue;
			}
			set
			{
				this.dynamicControl.DefaultValue = (int)value;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00048331 File Offset: 0x00047331
		public override void SetDefaultValues(DynamicControl dc)
		{
			dc.Setting3 = 1;
			dc.Setting1 = 4;
		}
	}
}
