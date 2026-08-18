using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000069 RID: 105
	public class DynamicControlWrapper_AccommodationTxt : DynamicControlWrapper_Base
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x00042392 File Offset: 0x00041392
		public DynamicControlWrapper_AccommodationTxt(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000423A0 File Offset: 0x000413A0
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x000423C0 File Offset: 0x000413C0
		[Description("Indent (number of pixels to pad on the left of the control)")]
		[Category("Display")]
		public int Indent
		{
			get
			{
				return this.dynamicControl.DefaultValue >> 1;
			}
			set
			{
				int num = this.dynamicControl.DefaultValue & 1;
				this.dynamicControl.DefaultValue = (value << 1) + num;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x000423F0 File Offset: 0x000413F0
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0004241E File Offset: 0x0004141E
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

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00042438 File Offset: 0x00041438
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x00042458 File Offset: 0x00041458
		[Description("Indicates whether the data for this textbox is encrypted.")]
		[Category("Design")]
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

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00042470 File Offset: 0x00041470
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x0004248D File Offset: 0x0004148D
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

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000424A0 File Offset: 0x000414A0
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x000424BD File Offset: 0x000414BD
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

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x000424D0 File Offset: 0x000414D0
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x000424ED File Offset: 0x000414ED
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

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00042500 File Offset: 0x00041500
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0004251D File Offset: 0x0004151D
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

		// Token: 0x0600055D RID: 1373 RVA: 0x0004252D File Offset: 0x0004152D
		public override void SetDefaultValues(DynamicControl dc)
		{
			dc.Setting3 = 1;
		}
	}
}
