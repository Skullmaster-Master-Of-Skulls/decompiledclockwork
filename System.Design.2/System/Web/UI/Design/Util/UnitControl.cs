using System;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x02000169 RID: 361
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class UnitControl : Panel
	{
		// Token: 0x06000CC1 RID: 3265 RVA: 0x000520AC File Offset: 0x000502AC
		public UnitControl()
		{
			this.initMode = true;
			base.Size = new Size(88, 21);
			this.InitControl();
			this.InitUI();
			this.initMode = false;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00052101 File Offset: 0x00050301
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x0005210E File Offset: 0x0005030E
		public bool AllowNegativeValues
		{
			get
			{
				return this.valueEdit.AllowNegative;
			}
			set
			{
				this.valueEdit.AllowNegative = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0005211C File Offset: 0x0005031C
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x00052124 File Offset: 0x00050324
		public bool AllowNonUnitValues
		{
			get
			{
				return this.allowNonUnit;
			}
			set
			{
				if (value == this.allowNonUnit)
				{
					return;
				}
				if (value && !this.allowPercent)
				{
					throw new Exception();
				}
				this.allowNonUnit = value;
				if (this.allowNonUnit)
				{
					this.unitCombo.Items.Add(UnitControl.UNIT_VALUES[9]);
					return;
				}
				this.unitCombo.Items.Remove(UnitControl.UNIT_VALUES[9]);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0005218D File Offset: 0x0005038D
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00052198 File Offset: 0x00050398
		public bool AllowPercentValues
		{
			get
			{
				return this.allowPercent;
			}
			set
			{
				if (value == this.allowPercent)
				{
					return;
				}
				if (!value && this.allowNonUnit)
				{
					throw new Exception();
				}
				this.allowPercent = value;
				if (this.allowPercent)
				{
					this.unitCombo.Items.Add(UnitControl.UNIT_VALUES[8]);
					return;
				}
				this.unitCombo.Items.Remove(UnitControl.UNIT_VALUES[8]);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x000521FF File Offset: 0x000503FF
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x00052207 File Offset: 0x00050407
		public int DefaultUnit
		{
			get
			{
				return this.defaultUnit;
			}
			set
			{
				this.defaultUnit = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00052210 File Offset: 0x00050410
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x00052218 File Offset: 0x00050418
		public int MaxValue
		{
			get
			{
				return this.maxValue;
			}
			set
			{
				this.maxValue = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00052221 File Offset: 0x00050421
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00052229 File Offset: 0x00050429
		public int MinValue
		{
			get
			{
				return this.minValue;
			}
			set
			{
				this.minValue = value;
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00052232 File Offset: 0x00050432
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.valueEdit.Enabled = base.Enabled;
			this.unitCombo.Enabled = base.Enabled;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0005225D File Offset: 0x0005045D
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x00052265 File Offset: 0x00050465
		public bool ValidateMinMax
		{
			get
			{
				return this.validateMinMax;
			}
			set
			{
				this.validateMinMax = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00052270 File Offset: 0x00050470
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x000522DC File Offset: 0x000504DC
		public string Value
		{
			get
			{
				string text = this.GetValidatedValue();
				if (text == null)
				{
					text = this.valueEdit.Text;
				}
				else
				{
					this.valueEdit.Text = text;
					this.OnValueTextChanged(this.valueEdit, EventArgs.Empty);
				}
				int selectedIndex = this.unitCombo.SelectedIndex;
				if (text.Length == 0 || selectedIndex == -1)
				{
					return null;
				}
				return text + UnitControl.UNIT_VALUES[selectedIndex];
			}
			set
			{
				this.initMode = true;
				this.InitUI();
				if (value != null)
				{
					string text = value.Trim().ToLower(CultureInfo.InvariantCulture);
					int length = text.Length;
					int num = -1;
					int num2 = -1;
					for (int i = 0; i < length; i++)
					{
						char c = text[i];
						if ((c < '0' || c > '9') && !NumberFormatInfo.CurrentInfo.NumberDecimalSeparator.Contains(c.ToString(CultureInfo.CurrentCulture)) && (!NumberFormatInfo.CurrentInfo.NegativeSign.Contains(c.ToString(CultureInfo.CurrentCulture)) || !this.valueEdit.AllowNegative))
						{
							break;
						}
						num2 = i;
					}
					if (num2 != -1)
					{
						if (num2 + 1 < length)
						{
							int num3 = this.allowPercent ? 8 : 7;
							string value2 = text.Substring(num2 + 1);
							for (int j = 0; j <= num3; j++)
							{
								if (UnitControl.UNIT_VALUES[j].Equals(value2))
								{
									num = j;
									break;
								}
							}
						}
						else if (this.allowNonUnit)
						{
							num = 9;
						}
						if (num != -1)
						{
							this.valueEdit.Text = text.Substring(0, num2 + 1);
							this.unitCombo.SelectedIndex = num;
						}
					}
				}
				this.initMode = false;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0005240B File Offset: 0x0005060B
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x00052426 File Offset: 0x00050626
		public string UnitAccessibleName
		{
			get
			{
				if (this.unitCombo != null)
				{
					return this.unitCombo.AccessibleName;
				}
				return string.Empty;
			}
			set
			{
				if (this.unitCombo != null)
				{
					this.unitCombo.AccessibleName = value;
				}
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0005243C File Offset: 0x0005063C
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x00052457 File Offset: 0x00050657
		public string UnitAccessibleDescription
		{
			get
			{
				if (this.unitCombo != null)
				{
					return this.unitCombo.AccessibleDescription;
				}
				return string.Empty;
			}
			set
			{
				if (this.unitCombo != null)
				{
					this.unitCombo.AccessibleDescription = value;
				}
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0005246D File Offset: 0x0005066D
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x00052488 File Offset: 0x00050688
		public string ValueAccessibleName
		{
			get
			{
				if (this.valueEdit != null)
				{
					return this.valueEdit.AccessibleName;
				}
				return string.Empty;
			}
			set
			{
				if (this.valueEdit != null)
				{
					this.valueEdit.AccessibleName = value;
				}
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0005249E File Offset: 0x0005069E
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x000524B9 File Offset: 0x000506B9
		public string ValueAccessibleDescription
		{
			get
			{
				if (this.valueEdit != null)
				{
					return this.valueEdit.AccessibleDescription;
				}
				return string.Empty;
			}
			set
			{
				if (this.valueEdit != null)
				{
					this.valueEdit.AccessibleDescription = value;
				}
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000CDB RID: 3291 RVA: 0x000524CF File Offset: 0x000506CF
		// (remove) Token: 0x06000CDC RID: 3292 RVA: 0x000524E8 File Offset: 0x000506E8
		public event EventHandler Changed
		{
			add
			{
				this.onChangedHandler = (EventHandler)Delegate.Combine(this.onChangedHandler, value);
			}
			remove
			{
				this.onChangedHandler = (EventHandler)Delegate.Remove(this.onChangedHandler, value);
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00052504 File Offset: 0x00050704
		private string GetValidatedValue()
		{
			string result = null;
			if (this.validateMinMax)
			{
				string text = this.valueEdit.Text;
				if (text.Length != 0)
				{
					try
					{
						if (!text.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
						{
							int num = int.Parse(text, CultureInfo.CurrentCulture);
							if (num < this.minValue)
							{
								result = this.minValue.ToString(NumberFormatInfo.CurrentInfo);
							}
							else if (num > this.maxValue)
							{
								result = this.maxValue.ToString(NumberFormatInfo.CurrentInfo);
							}
						}
						else
						{
							float num2 = float.Parse(text, CultureInfo.CurrentCulture);
							if (num2 < (float)this.minValue)
							{
								result = this.minValue.ToString(NumberFormatInfo.CurrentInfo);
							}
							else if (num2 > (float)this.maxValue)
							{
								result = this.maxValue.ToString(NumberFormatInfo.CurrentInfo);
							}
						}
					}
					catch
					{
						result = this.maxValue.ToString(NumberFormatInfo.CurrentInfo);
					}
				}
			}
			return result;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x000525F8 File Offset: 0x000507F8
		private void InitControl()
		{
			int num = base.Width - 44;
			if (num < 0)
			{
				num = 0;
			}
			this.valueEdit = new NumberEdit();
			this.valueEdit.Location = new Point(0, 0);
			this.valueEdit.Size = new Size(num, 21);
			this.valueEdit.TabIndex = 0;
			this.valueEdit.MaxLength = 10;
			this.valueEdit.TextChanged += this.OnValueTextChanged;
			this.valueEdit.LostFocus += this.OnValueLostFocus;
			this.unitCombo = new ComboBox();
			this.unitCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.unitCombo.Location = new Point(num + 4, 0);
			this.unitCombo.Size = new Size(40, 21);
			this.unitCombo.TabIndex = 1;
			this.unitCombo.MaxDropDownItems = 9;
			this.unitCombo.SelectedIndexChanged += this.OnUnitSelectedIndexChanged;
			base.Controls.Clear();
			base.Controls.AddRange(new Control[]
			{
				this.unitCombo,
				this.valueEdit
			});
			for (int i = 0; i <= 7; i++)
			{
				this.unitCombo.Items.Add(UnitControl.UNIT_VALUES[i]);
			}
			if (this.allowPercent)
			{
				this.unitCombo.Items.Add(UnitControl.UNIT_VALUES[8]);
			}
			if (this.allowNonUnit)
			{
				this.unitCombo.Items.Add(UnitControl.UNIT_VALUES[9]);
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0005278E File Offset: 0x0005098E
		private void InitUI()
		{
			this.valueEdit.Text = string.Empty;
			this.unitCombo.SelectedIndex = -1;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000527AC File Offset: 0x000509AC
		private void OnChanged(EventArgs e)
		{
			if (this.onChangedHandler != null)
			{
				this.onChangedHandler(this, e);
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000527C3 File Offset: 0x000509C3
		protected override void OnGotFocus(EventArgs e)
		{
			this.valueEdit.Focus();
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000527D4 File Offset: 0x000509D4
		private void OnValueTextChanged(object source, EventArgs e)
		{
			if (this.initMode)
			{
				return;
			}
			string text = this.valueEdit.Text;
			if (text.Length == 0)
			{
				this.internalChange = true;
				this.unitCombo.SelectedIndex = -1;
				this.internalChange = false;
			}
			else if (this.unitCombo.SelectedIndex == -1)
			{
				this.internalChange = true;
				this.unitCombo.SelectedIndex = this.defaultUnit;
				this.internalChange = false;
			}
			this.valueChanged = true;
			this.OnChanged(null);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00052858 File Offset: 0x00050A58
		private void OnValueLostFocus(object source, EventArgs e)
		{
			if (this.valueChanged)
			{
				string validatedValue = this.GetValidatedValue();
				if (validatedValue != null)
				{
					this.valueEdit.Text = validatedValue;
					this.OnValueTextChanged(this.valueEdit, EventArgs.Empty);
				}
				this.valueChanged = false;
				this.OnChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x000528A6 File Offset: 0x00050AA6
		private void OnUnitSelectedIndexChanged(object source, EventArgs e)
		{
			if (this.initMode || this.internalChange)
			{
				return;
			}
			this.OnChanged(EventArgs.Empty);
		}

		// Token: 0x040007B6 RID: 1974
		private const int EDIT_X_SIZE = 44;

		// Token: 0x040007B7 RID: 1975
		private const int COMBO_X_SIZE = 40;

		// Token: 0x040007B8 RID: 1976
		private const int SEPARATOR_X_SIZE = 4;

		// Token: 0x040007B9 RID: 1977
		private const int CTL_Y_SIZE = 21;

		// Token: 0x040007BA RID: 1978
		public const int UNIT_PX = 0;

		// Token: 0x040007BB RID: 1979
		public const int UNIT_PT = 1;

		// Token: 0x040007BC RID: 1980
		public const int UNIT_PC = 2;

		// Token: 0x040007BD RID: 1981
		public const int UNIT_MM = 3;

		// Token: 0x040007BE RID: 1982
		public const int UNIT_CM = 4;

		// Token: 0x040007BF RID: 1983
		public const int UNIT_IN = 5;

		// Token: 0x040007C0 RID: 1984
		public const int UNIT_EM = 6;

		// Token: 0x040007C1 RID: 1985
		public const int UNIT_EX = 7;

		// Token: 0x040007C2 RID: 1986
		public const int UNIT_PERCENT = 8;

		// Token: 0x040007C3 RID: 1987
		public const int UNIT_NONE = 9;

		// Token: 0x040007C4 RID: 1988
		private static readonly string[] UNIT_VALUES = new string[]
		{
			"px",
			"pt",
			"pc",
			"mm",
			"cm",
			"in",
			"em",
			"ex",
			"%",
			""
		};

		// Token: 0x040007C5 RID: 1989
		private NumberEdit valueEdit;

		// Token: 0x040007C6 RID: 1990
		private ComboBox unitCombo;

		// Token: 0x040007C7 RID: 1991
		private bool allowPercent = true;

		// Token: 0x040007C8 RID: 1992
		private bool allowNonUnit;

		// Token: 0x040007C9 RID: 1993
		private int defaultUnit = 1;

		// Token: 0x040007CA RID: 1994
		private int minValue;

		// Token: 0x040007CB RID: 1995
		private int maxValue = 65535;

		// Token: 0x040007CC RID: 1996
		private bool validateMinMax;

		// Token: 0x040007CD RID: 1997
		private EventHandler onChangedHandler;

		// Token: 0x040007CE RID: 1998
		private bool initMode;

		// Token: 0x040007CF RID: 1999
		private bool internalChange;

		// Token: 0x040007D0 RID: 2000
		private bool valueChanged;
	}
}
