using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000398 RID: 920
	[TypeConverter(typeof(TableLayoutSettings.StyleConverter))]
	public abstract class TableLayoutStyle
	{
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003C25 RID: 15397 RVA: 0x00106D98 File Offset: 0x00104F98
		// (set) Token: 0x06003C26 RID: 15398 RVA: 0x00106DA0 File Offset: 0x00104FA0
		[DefaultValue(SizeType.AutoSize)]
		public SizeType SizeType
		{
			get
			{
				return this._sizeType;
			}
			set
			{
				if (this._sizeType != value)
				{
					this._sizeType = value;
					if (this.Owner != null)
					{
						LayoutTransaction.DoLayout(this.Owner, this.Owner, PropertyNames.Style);
						Control control = this.Owner as Control;
						if (control != null)
						{
							control.Invalidate();
						}
					}
				}
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06003C27 RID: 15399 RVA: 0x00106DF0 File Offset: 0x00104FF0
		// (set) Token: 0x06003C28 RID: 15400 RVA: 0x00106DF8 File Offset: 0x00104FF8
		internal float Size
		{
			get
			{
				return this._size;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentOutOfRangeException("Size", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"Size",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this._size != value)
				{
					this._size = value;
					if (this.Owner != null)
					{
						LayoutTransaction.DoLayout(this.Owner, this.Owner, PropertyNames.Style);
						Control control = this.Owner as Control;
						if (control != null)
						{
							control.Invalidate();
						}
					}
				}
			}
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x00106E93 File Offset: 0x00105093
		private bool ShouldSerializeSize()
		{
			return this.SizeType > SizeType.AutoSize;
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x00106E9E File Offset: 0x0010509E
		// (set) Token: 0x06003C2B RID: 15403 RVA: 0x00106EA6 File Offset: 0x001050A6
		internal IArrangedElement Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x00106EAF File Offset: 0x001050AF
		internal void SetSize(float size)
		{
			this._size = size;
		}

		// Token: 0x0400239C RID: 9116
		private IArrangedElement _owner;

		// Token: 0x0400239D RID: 9117
		private SizeType _sizeType;

		// Token: 0x0400239E RID: 9118
		private float _size;
	}
}
