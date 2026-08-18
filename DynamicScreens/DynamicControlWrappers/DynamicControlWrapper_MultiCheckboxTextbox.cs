using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000023 RID: 35
	public class DynamicControlWrapper_MultiCheckboxTextbox : DynamicControlWrapper_Base
	{
		// Token: 0x06000220 RID: 544 RVA: 0x000197B9 File Offset: 0x000187B9
		public DynamicControlWrapper_MultiCheckboxTextbox(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000197C8 File Offset: 0x000187C8
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000197F6 File Offset: 0x000187F6
		[Category("Display")]
		[Description("Indicates the number of rows this textbox should contain.  Use -1 to indicate it should fill it's container vertically.")]
		public int MultilineCount
		{
			get
			{
				return (this.dynamicControl.Setting1 <= 1) ? 1 : this.dynamicControl.Setting1;
			}
			set
			{
				this.dynamicControl.Setting1 = ((value > 1) ? value : 0);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00019810 File Offset: 0x00018810
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00019830 File Offset: 0x00018830
		[Category("Design")]
		[Description("Indicates whether the data for this textbox is encrypted.")]
		public bool Encrypted
		{
			get
			{
				return this.dynamicControl.Setting3 == 1;
			}
			set
			{
				this.dynamicControl.Setting3 = (value ? 1 : 0);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00019848 File Offset: 0x00018848
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00019865 File Offset: 0x00018865
		[Description("Indicates the number of characters wide this textbox should be.")]
		[Category("Display")]
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00019878 File Offset: 0x00018878
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00019895 File Offset: 0x00018895
		[Category("Behaviour")]
		[Description("A default value to use for new data.")]
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000198A8 File Offset: 0x000188A8
		// (set) Token: 0x0600022A RID: 554 RVA: 0x000198C5 File Offset: 0x000188C5
		[Category("Behaviour")]
		[Description("The text masking to use.")]
		public string TextMask
		{
			get
			{
				return this.dynamicControl.Mask;
			}
			set
			{
				this.dynamicControl.Mask = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000198D8 File Offset: 0x000188D8
		// (set) Token: 0x0600022C RID: 556 RVA: 0x000198F5 File Offset: 0x000188F5
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
	}
}
