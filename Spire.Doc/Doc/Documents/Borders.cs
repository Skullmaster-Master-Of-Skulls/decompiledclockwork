using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Formatting;

namespace Spire.Doc.Documents
{
	// Token: 0x020004A1 RID: 1185
	public class Borders : FormatBase
	{
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x003D8444 File Offset: 0x003D7444
		public bool NoBorder
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						IL_08:
						break;
					case 1:
						if (this.Top.BorderType == BorderStyle.None)
						{
							num = 2;
							continue;
						}
						goto IL_A1;
					case 2:
						goto IL_7D;
					case 3:
						num = 4;
						continue;
					case 4:
						if (this.Right.BorderType == BorderStyle.None)
						{
							num = 5;
							continue;
						}
						goto IL_A1;
					case 5:
						num = 1;
						continue;
					}
					if (this.Left.BorderType == BorderStyle.None)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					IL_A1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_08;
					default:
						goto IL_B7;
					}
				}
				IL_7D:
				return this.Bottom.BorderType == BorderStyle.None;
				IL_B7:
				if (false)
				{
				}
				return false;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060040F7 RID: 16631 RVA: 0x003D8510 File Offset: 0x003D7510
		public Border Left
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base[1] as Border;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x003D8558 File Offset: 0x003D7558
		public Border Top
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return base[2] as Border;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x003D85A0 File Offset: 0x003D75A0
		public Border Right
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return base[4] as Border;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060040FA RID: 16634 RVA: 0x003D85E8 File Offset: 0x003D75E8
		public Border Bottom
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base[3] as Border;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060040FB RID: 16635 RVA: 0x003D8630 File Offset: 0x003D7630
		public Border Vertical
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base[5] as Border;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x003D8678 File Offset: 0x003D7678
		public Border Horizontal
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base[6] as Border;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x003D86C0 File Offset: 0x003D76C0
		internal Border DiagonalDown
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base[7] as Border;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060040FE RID: 16638 RVA: 0x003D8708 File Offset: 0x003D7708
		internal Border DiagonalUp
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base[8] as Border;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (set) Token: 0x060040FF RID: 16639 RVA: 0x003D8750 File Offset: 0x003D7750
		internal string ColorShemeName
		{
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				Border left = this.Left;
				Border right = this.Right;
				Border top = this.Top;
				this.Bottom.ColorShemeName = value;
				top.ColorShemeName = value;
				right.ColorShemeName = value;
				left.ColorShemeName = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (set) Token: 0x06004100 RID: 16640 RVA: 0x003D87C4 File Offset: 0x003D77C4
		public Color Color
		{
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				Border left = this.Left;
				Border right = this.Right;
				Border top = this.Top;
				this.Bottom.Color = value;
				top.Color = value;
				right.Color = value;
				left.Color = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (set) Token: 0x06004101 RID: 16641 RVA: 0x003D8838 File Offset: 0x003D7838
		public float LineWidth
		{
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				Border left = this.Left;
				Border right = this.Right;
				Border top = this.Top;
				this.Bottom.LineWidth = value;
				top.LineWidth = value;
				right.LineWidth = value;
				left.LineWidth = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (set) Token: 0x06004102 RID: 16642 RVA: 0x003D88AC File Offset: 0x003D78AC
		public BorderStyle BorderType
		{
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				Border left = this.Left;
				Border right = this.Right;
				Border top = this.Top;
				this.Bottom.BorderType = value;
				top.BorderType = value;
				right.BorderType = value;
				left.BorderType = value;
				Border vertical = this.Vertical;
				this.Horizontal.BorderType = value;
				vertical.BorderType = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (set) Token: 0x06004103 RID: 16643 RVA: 0x003D8938 File Offset: 0x003D7938
		public float Space
		{
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(value);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (set) Token: 0x06004104 RID: 16644 RVA: 0x003D897C File Offset: 0x003D797C
		public bool IsShadow
		{
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				Border left = this.Left;
				Border right = this.Right;
				Border top = this.Top;
				this.Bottom.Shadow = value;
				top.Shadow = value;
				right.Shadow = value;
				left.Shadow = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x003D89F0 File Offset: 0x003D79F0
		internal TableCell CurrentCell
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						CellFormat cellFormat;
						if (cellFormat.OwnerBase != null)
						{
							goto IL_89;
						}
						goto IL_100;
					}
					case 1:
					{
						CellFormat cellFormat;
						this.ᜀ = (cellFormat.OwnerBase as TableCell);
						num = 2;
						continue;
					}
					case 2:
						goto IL_FE;
					case 3:
						if (true)
						{
						}
						break;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_89;
						default:
						{
							if (false)
							{
							}
							CellFormat cellFormat = base.OwnerBase as CellFormat;
							num = 0;
							continue;
						}
						}
						break;
					case 5:
						num = 7;
						continue;
					case 6:
						if (base.OwnerBase != null)
						{
							num = 5;
							continue;
						}
						goto IL_100;
					case 7:
						if (base.OwnerBase is CellFormat)
						{
							num = 4;
							continue;
						}
						goto IL_100;
					case 8:
						num = 6;
						continue;
					}
					if (this.ᜀ == null)
					{
						num = 8;
						continue;
					}
					break;
					IL_89:
					num = 1;
				}
				IL_FE:
				IL_100:
				return this.ᜀ;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06004106 RID: 16646 RVA: 0x003D8B04 File Offset: 0x003D7B04
		// (set) Token: 0x06004107 RID: 16647 RVA: 0x003D8C10 File Offset: 0x003D7C10
		internal TableRow CurrentRow
		{
			get
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_82;
					case 1:
						if (base.OwnerBase != null)
						{
							num = 7;
							continue;
						}
						goto IL_F6;
					case 2:
						num = 6;
						continue;
					case 3:
						goto IL_F4;
					case 5:
						if (base.OwnerBase is TableRow)
						{
							num = 0;
							continue;
						}
						goto IL_F6;
					case 6:
						if (this.CurrentCell != null)
						{
							num = 8;
							continue;
						}
						num = 1;
						continue;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_82;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 8:
						this.ᜁ = this.CurrentCell.OwnerRow;
						num = 3;
						continue;
					}
					if (this.ᜁ != null)
					{
						goto IL_F6;
					}
					num = 2;
				}
				IL_82:
				if (true)
				{
				}
				return null;
				IL_F4:
				IL_F6:
				return this.ᜁ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x003D8C54 File Offset: 0x003D7C54
		internal Borders(FormatBase A_0, int A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x003D8C70 File Offset: 0x003D7C70
		public Borders()
		{
			this.ᜀ();
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x003D8C8C File Offset: 0x003D7C8C
		internal Borders(Borders A_0)
		{
			base.ImportContainer(A_0);
			this.ᜀ();
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x003D8CAC File Offset: 0x003D7CAC
		protected internal override void EnsureComposites()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.EnsureComposites(new int[]
			{
				1,
				4,
				2,
				3,
				5,
				6,
				7,
				8
			});
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x003D8D18 File Offset: 0x003D7D18
		protected override object GetDefValue(int key)
		{
			int a_ = 15;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new ArgumentException(ClipboardData.b("Ṵቶx孺ᕼṾꎂﾈ뎒", a_));
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x003D8D70 File Offset: 0x003D7D70
		protected override FormatBase GetDefComposite(int key)
		{
			for (;;)
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						switch (key)
						{
						case 1:
							goto IL_94;
						case 2:
							goto IL_F4;
						case 3:
							goto IL_85;
						case 4:
							goto IL_E5;
						case 5:
							goto IL_C7;
						case 6:
							goto IL_76;
						case 7:
							goto IL_A3;
						case 8:
							goto IL_D6;
						default:
							num = 0;
							continue;
						}
						break;
					case 2:
						goto IL_C5;
					}
					break;
				}
			}
			IL_76:
			return base.GetDefComposite(6, new Border(this, 6));
			IL_85:
			return base.GetDefComposite(3, new Border(this, 3));
			IL_94:
			return base.GetDefComposite(1, new Border(this, 1));
			IL_A3:
			if (true)
			{
			}
			return base.GetDefComposite(7, new Border(this, 7));
			IL_C5:
			return null;
			IL_C7:
			return base.GetDefComposite(5, new Border(this, 5));
			IL_D6:
			return base.GetDefComposite(8, new Border(this, 8));
			IL_E5:
			return base.GetDefComposite(4, new Border(this, 4));
			IL_F4:
			return base.GetDefComposite(2, new Border(this, 2));
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x003D8E84 File Offset: 0x003D7E84
		protected override void InitXDLSHolder()
		{
			int a_ = 3;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4B;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				IL_39:
				switch (num)
				{
				case 0:
					goto IL_79;
				case 1:
					base.XDLSHolder.SkipMe = true;
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_4B;
			}
			IL_79:
			goto IL_7B;
			IL_4B:
			if (base.IsDefault)
			{
				num = 1;
				goto IL_39;
			}
			IL_7B:
			base.XDLSHolder.AddElement(ClipboardData.b("⭨Ѫᥬ᭮ṰṲ", a_), this.Bottom);
			base.XDLSHolder.AddElement(ClipboardData.b("㵨Ѫᵬ", a_), this.Top);
			base.XDLSHolder.AddElement(ClipboardData.b("╨๪୬᭮", a_), this.Left);
			base.XDLSHolder.AddElement(ClipboardData.b("㭨ɪ੬ݮհ", a_), this.Right);
			base.XDLSHolder.AddElement(ClipboardData.b("ⅨѪὬٮ୰ᱲ᭴Ͷᡸ᝺", a_), this.Horizontal);
			base.XDLSHolder.AddElement(ClipboardData.b("㽨๪Ὤ᭮ᡰၲᑴ᭶", a_), this.Vertical);
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x003D8FC8 File Offset: 0x003D7FC8
		public Borders Clone()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return (Borders)this.CloneImpl();
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x003D9010 File Offset: 0x003D8010
		protected override object CloneImpl()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return new Borders(this);
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x003D9054 File Offset: 0x003D8054
		protected override void OnChange(FormatBase format, int propertyKey)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.OnChange(format, propertyKey);
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x003D9098 File Offset: 0x003D8098
		internal override void ApplyBase(FormatBase baseFormat)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.ApplyBase(baseFormat);
			this.Left.ApplyBase((baseFormat as Borders).Left);
			this.Right.ApplyBase((baseFormat as Borders).Right);
			this.Top.ApplyBase((baseFormat as Borders).Top);
			this.Bottom.ApplyBase((baseFormat as Borders).Bottom);
			this.Horizontal.ApplyBase((baseFormat as Borders).Horizontal);
			this.Vertical.ApplyBase((baseFormat as Borders).Vertical);
			this.DiagonalDown.ApplyBase((baseFormat as Borders).DiagonalDown);
			this.DiagonalUp.ApplyBase((baseFormat as Borders).DiagonalUp);
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x003D918C File Offset: 0x003D818C
		private void ᜀ(float A_0)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (base.ParentFormat is CellFormat)
					{
						num = 5;
						continue;
					}
					return;
				case 1:
					goto IL_AB;
				case 2:
					goto IL_93;
				case 4:
					num = 0;
					continue;
				case 5:
					goto IL_88;
				case 6:
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_93:
					if (base.ParentFormat is RowFormat)
					{
						num = 1;
						continue;
					}
					(base.ParentFormat as CellFormat).Paddings.All = A_0;
					num = 6;
					continue;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (!(base.ParentFormat is RowFormat))
					{
						num = 4;
						continue;
					}
					break;
				}
				IL_88:
				num = 2;
			}
			IL_AB:
			(base.ParentFormat as RowFormat).Paddings.All = A_0;
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x003D9290 File Offset: 0x003D8290
		private void ᜀ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.Left.ᜀ(this);
			this.Left.BorderPosition = Border.BorderPositions.Left;
			this.Top.ᜀ(this);
			this.Top.BorderPosition = Border.BorderPositions.Top;
			this.Right.ᜀ(this);
			this.Right.BorderPosition = Border.BorderPositions.Right;
			this.Bottom.ᜀ(this);
			this.Bottom.BorderPosition = Border.BorderPositions.Bottom;
			this.Vertical.ᜀ(this);
			this.Vertical.BorderPosition = Border.BorderPositions.Vertical;
			this.Horizontal.ᜀ(this);
			this.Horizontal.BorderPosition = Border.BorderPositions.Horizontal;
			this.DiagonalDown.ᜀ(this);
			this.DiagonalDown.BorderPosition = Border.BorderPositions.DiagonalDown;
			this.DiagonalUp.ᜀ(this);
			this.DiagonalUp.BorderPosition = Border.BorderPositions.DiagonalUp;
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x003D938C File Offset: 0x003D838C
		internal void ᜀ(Borders A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.Left.ᜁ(A_0.Left);
			this.Right.ᜁ(A_0.Right);
			this.Top.ᜁ(A_0.Top);
			this.Bottom.ᜁ(A_0.Bottom);
			this.Horizontal.ᜁ(A_0.Horizontal);
			this.Vertical.ᜁ(A_0.Vertical);
			this.DiagonalDown.ᜁ(A_0.DiagonalDown);
			this.DiagonalUp.ᜁ(A_0.DiagonalUp);
		}

		// Token: 0x04003039 RID: 12345
		private int \u25D9\u00A9\u008A\u0092;

		// Token: 0x0400303A RID: 12346
		public const int LeftKey = 1;

		// Token: 0x0400303B RID: 12347
		public const int TopKey = 2;

		// Token: 0x0400303C RID: 12348
		public const int BottomKey = 3;

		// Token: 0x0400303D RID: 12349
		private bool[] \u2460\u00AE\u0081\u00AC;

		// Token: 0x0400303E RID: 12350
		public const int RightKey = 4;

		// Token: 0x0400303F RID: 12351
		public const int VerticalKey = 5;

		// Token: 0x04003040 RID: 12352
		public const int HorizontalKey = 6;

		// Token: 0x04003041 RID: 12353
		public const int DiagonalDownKey = 7;

		// Token: 0x04003042 RID: 12354
		public const int DiagonalUpKey = 8;

		// Token: 0x04003043 RID: 12355
		private float \u2460\u0090\u00AD\u008B;

		// Token: 0x04003044 RID: 12356
		private new TableCell ᜀ;

		// Token: 0x04003045 RID: 12357
		private bool \u25D8\u008C\u00A5\u0085;

		// Token: 0x04003046 RID: 12358
		private TableRow ᜁ;
	}
}
