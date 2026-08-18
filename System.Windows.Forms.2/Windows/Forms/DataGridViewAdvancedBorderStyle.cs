using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000191 RID: 401
	public sealed class DataGridViewAdvancedBorderStyle : ICloneable
	{
		// Token: 0x06001CA4 RID: 7332 RVA: 0x000863F7 File Offset: 0x000845F7
		public DataGridViewAdvancedBorderStyle() : this(null, DataGridViewAdvancedCellBorderStyle.NotSet, DataGridViewAdvancedCellBorderStyle.NotSet, DataGridViewAdvancedCellBorderStyle.NotSet)
		{
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00086403 File Offset: 0x00084603
		internal DataGridViewAdvancedBorderStyle(DataGridView owner) : this(owner, DataGridViewAdvancedCellBorderStyle.NotSet, DataGridViewAdvancedCellBorderStyle.NotSet, DataGridViewAdvancedCellBorderStyle.NotSet)
		{
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00086410 File Offset: 0x00084610
		internal DataGridViewAdvancedBorderStyle(DataGridView owner, DataGridViewAdvancedCellBorderStyle banned1, DataGridViewAdvancedCellBorderStyle banned2, DataGridViewAdvancedCellBorderStyle banned3)
		{
			this.owner = owner;
			this.banned1 = banned1;
			this.banned2 = banned2;
			this.banned3 = banned3;
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x00086463 File Offset: 0x00084663
		// (set) Token: 0x06001CA8 RID: 7336 RVA: 0x00086478 File Offset: 0x00084678
		public DataGridViewAdvancedCellBorderStyle All
		{
			get
			{
				if (!this.all)
				{
					return DataGridViewAdvancedCellBorderStyle.NotSet;
				}
				return this.top;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAdvancedCellBorderStyle));
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet || value == this.banned1 || value == this.banned2 || value == this.banned3)
				{
					throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[]
					{
						"All"
					}));
				}
				if (!this.all || this.top != value)
				{
					this.all = true;
					this.bottom = value;
					this.right = value;
					this.left = value;
					this.top = value;
					if (this.owner != null)
					{
						this.owner.OnAdvancedBorderStyleChanged(this);
					}
				}
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x00086535 File Offset: 0x00084735
		// (set) Token: 0x06001CAA RID: 7338 RVA: 0x0008654C File Offset: 0x0008474C
		public DataGridViewAdvancedCellBorderStyle Bottom
		{
			get
			{
				if (this.all)
				{
					return this.top;
				}
				return this.bottom;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAdvancedCellBorderStyle));
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[]
					{
						"Bottom"
					}));
				}
				this.BottomInternal = value;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x000865A8 File Offset: 0x000847A8
		internal DataGridViewAdvancedCellBorderStyle BottomInternal
		{
			set
			{
				if ((this.all && this.top != value) || (!this.all && this.bottom != value))
				{
					if (this.all && this.right == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
					{
						this.right = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					this.all = false;
					this.bottom = value;
					if (this.owner != null)
					{
						this.owner.OnAdvancedBorderStyleChanged(this);
					}
				}
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x00086611 File Offset: 0x00084811
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x00086628 File Offset: 0x00084828
		public DataGridViewAdvancedCellBorderStyle Left
		{
			get
			{
				if (this.all)
				{
					return this.top;
				}
				return this.left;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAdvancedCellBorderStyle));
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[]
					{
						"Left"
					}));
				}
				this.LeftInternal = value;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (set) Token: 0x06001CAE RID: 7342 RVA: 0x00086684 File Offset: 0x00084884
		internal DataGridViewAdvancedCellBorderStyle LeftInternal
		{
			set
			{
				if ((this.all && this.top != value) || (!this.all && this.left != value))
				{
					if (this.owner != null && this.owner.RightToLeftInternal && (value == DataGridViewAdvancedCellBorderStyle.InsetDouble || value == DataGridViewAdvancedCellBorderStyle.OutsetDouble))
					{
						throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[]
						{
							"Left"
						}));
					}
					if (this.all)
					{
						if (this.right == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
						{
							this.right = DataGridViewAdvancedCellBorderStyle.Outset;
						}
						if (this.bottom == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
						{
							this.bottom = DataGridViewAdvancedCellBorderStyle.Outset;
						}
					}
					this.all = false;
					this.left = value;
					if (this.owner != null)
					{
						this.owner.OnAdvancedBorderStyleChanged(this);
					}
				}
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x0008673E File Offset: 0x0008493E
		// (set) Token: 0x06001CB0 RID: 7344 RVA: 0x00086758 File Offset: 0x00084958
		public DataGridViewAdvancedCellBorderStyle Right
		{
			get
			{
				if (this.all)
				{
					return this.top;
				}
				return this.right;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAdvancedCellBorderStyle));
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[]
					{
						"Right"
					}));
				}
				this.RightInternal = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x000867B4 File Offset: 0x000849B4
		internal DataGridViewAdvancedCellBorderStyle RightInternal
		{
			set
			{
				if ((this.all && this.top != value) || (!this.all && this.right != value))
				{
					if (this.owner != null && !this.owner.RightToLeftInternal && (value == DataGridViewAdvancedCellBorderStyle.InsetDouble || value == DataGridViewAdvancedCellBorderStyle.OutsetDouble))
					{
						throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[]
						{
							"Right"
						}));
					}
					if (this.all && this.bottom == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
					{
						this.bottom = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					this.all = false;
					this.right = value;
					if (this.owner != null)
					{
						this.owner.OnAdvancedBorderStyleChanged(this);
					}
				}
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00086858 File Offset: 0x00084A58
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x00086860 File Offset: 0x00084A60
		public DataGridViewAdvancedCellBorderStyle Top
		{
			get
			{
				return this.top;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAdvancedCellBorderStyle));
				}
				if (value == DataGridViewAdvancedCellBorderStyle.NotSet)
				{
					throw new ArgumentException(SR.GetString("DataGridView_AdvancedCellBorderStyleInvalid", new object[]
					{
						"Top"
					}));
				}
				this.TopInternal = value;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (set) Token: 0x06001CB4 RID: 7348 RVA: 0x000868BC File Offset: 0x00084ABC
		internal DataGridViewAdvancedCellBorderStyle TopInternal
		{
			set
			{
				if ((this.all && this.top != value) || (!this.all && this.top != value))
				{
					if (this.all)
					{
						if (this.right == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
						{
							this.right = DataGridViewAdvancedCellBorderStyle.Outset;
						}
						if (this.bottom == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
						{
							this.bottom = DataGridViewAdvancedCellBorderStyle.Outset;
						}
					}
					this.all = false;
					this.top = value;
					if (this.owner != null)
					{
						this.owner.OnAdvancedBorderStyleChanged(this);
					}
				}
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00086938 File Offset: 0x00084B38
		public override bool Equals(object other)
		{
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = other as DataGridViewAdvancedBorderStyle;
			return dataGridViewAdvancedBorderStyle != null && (dataGridViewAdvancedBorderStyle.all == this.all && dataGridViewAdvancedBorderStyle.top == this.top && dataGridViewAdvancedBorderStyle.left == this.left && dataGridViewAdvancedBorderStyle.bottom == this.bottom) && dataGridViewAdvancedBorderStyle.right == this.right;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00086999 File Offset: 0x00084B99
		public override int GetHashCode()
		{
			return WindowsFormsUtils.GetCombinedHashCodes(new int[]
			{
				(int)this.top,
				(int)this.left,
				(int)this.bottom,
				(int)this.right
			});
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000869CC File Offset: 0x00084BCC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewAdvancedBorderStyle { All=",
				this.All.ToString(),
				", Left=",
				this.Left.ToString(),
				", Right=",
				this.Right.ToString(),
				", Top=",
				this.Top.ToString(),
				", Bottom=",
				this.Bottom.ToString(),
				" }"
			});
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00086A8C File Offset: 0x00084C8C
		object ICloneable.Clone()
		{
			return new DataGridViewAdvancedBorderStyle(this.owner, this.banned1, this.banned2, this.banned3)
			{
				all = this.all,
				top = this.top,
				right = this.right,
				bottom = this.bottom,
				left = this.left
			};
		}

		// Token: 0x04000C29 RID: 3113
		private DataGridView owner;

		// Token: 0x04000C2A RID: 3114
		private bool all = true;

		// Token: 0x04000C2B RID: 3115
		private DataGridViewAdvancedCellBorderStyle banned1;

		// Token: 0x04000C2C RID: 3116
		private DataGridViewAdvancedCellBorderStyle banned2;

		// Token: 0x04000C2D RID: 3117
		private DataGridViewAdvancedCellBorderStyle banned3;

		// Token: 0x04000C2E RID: 3118
		private DataGridViewAdvancedCellBorderStyle top = DataGridViewAdvancedCellBorderStyle.None;

		// Token: 0x04000C2F RID: 3119
		private DataGridViewAdvancedCellBorderStyle left = DataGridViewAdvancedCellBorderStyle.None;

		// Token: 0x04000C30 RID: 3120
		private DataGridViewAdvancedCellBorderStyle right = DataGridViewAdvancedCellBorderStyle.None;

		// Token: 0x04000C31 RID: 3121
		private DataGridViewAdvancedCellBorderStyle bottom = DataGridViewAdvancedCellBorderStyle.None;
	}
}
