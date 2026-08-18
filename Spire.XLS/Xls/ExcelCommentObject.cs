using System;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls
{
	// Token: 0x02000157 RID: 343
	public class ExcelCommentObject : ICommentShape
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x0009AB80 File Offset: 0x00099B80
		internal ExcelCommentObject(ICommentShape A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0009AB9C File Offset: 0x00099B9C
		public string Author
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
				return this.ᜀ.Author;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0009ABE4 File Offset: 0x00099BE4
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x0009AC2C File Offset: 0x00099C2C
		public bool IsVisible
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
				return this.ᜀ.IsVisible;
			}
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
				this.ᜀ.IsVisible = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0009AC74 File Offset: 0x00099C74
		public int Row
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
				return this.ᜀ.Row;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0009ACBC File Offset: 0x00099CBC
		public int Column
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
				return this.ᜀ.Column;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0009AD04 File Offset: 0x00099D04
		public IRichTextString RichText
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
				return this.ᜀ.RichText;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0009AD4C File Offset: 0x00099D4C
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x0009AD94 File Offset: 0x00099D94
		public string Text
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
				return this.ᜀ.Text;
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
				this.ᜀ.Text = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0009ADDC File Offset: 0x00099DDC
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x0009AE24 File Offset: 0x00099E24
		public bool AutoSize
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
				return this.ᜀ.AutoSize;
			}
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
				this.ᜀ.AutoSize = value;
			}
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0009AE6C File Offset: 0x00099E6C
		public void Remove()
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
			this.ᜀ.Remove();
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0009AEB4 File Offset: 0x00099EB4
		public void Scale(int scaleWidth, int scaleHeight)
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
			this.ᜀ.Scale(scaleWidth, scaleHeight);
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0009AEFC File Offset: 0x00099EFC
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x0009AF44 File Offset: 0x00099F44
		public bool Visible
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
				return this.ᜀ.Visible;
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
				this.ᜀ.Visible = true;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0009AF8C File Offset: 0x00099F8C
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x0009AFD4 File Offset: 0x00099FD4
		public int Height
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
				return this.ᜀ.Height;
			}
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
				this.ᜀ.Height = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x0009B01C File Offset: 0x0009A01C
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x0009B064 File Offset: 0x0009A064
		public CommentHAlignType HAlignment
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
				return this.ᜀ.HAlignment;
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
				this.ᜀ.HAlignment = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0009B0AC File Offset: 0x0009A0AC
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x0009B0F4 File Offset: 0x0009A0F4
		public CommentVAlignType VAlignment
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
				return this.ᜀ.VAlignment;
			}
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
				this.ᜀ.VAlignment = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0009B13C File Offset: 0x0009A13C
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x0009B184 File Offset: 0x0009A184
		public TextRotationType TextRotation
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
				return this.ᜀ.TextRotation;
			}
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
				this.ᜀ.TextRotation = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0009B1CC File Offset: 0x0009A1CC
		public int ID
		{
			get
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
				return 0;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0009B208 File Offset: 0x0009A208
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x0009B250 File Offset: 0x0009A250
		public int Left
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
				return this.ᜀ.Left;
			}
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
				this.ᜀ.Left = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0009B298 File Offset: 0x0009A298
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x0009B2E0 File Offset: 0x0009A2E0
		public string Name
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
				return this.ᜀ.Name;
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
				this.ᜀ.Name = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0009B328 File Offset: 0x0009A328
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x0009B370 File Offset: 0x0009A370
		public int Top
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
				return this.ᜀ.Top;
			}
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
				this.ᜀ.Top = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0009B3B8 File Offset: 0x0009A3B8
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x0009B400 File Offset: 0x0009A400
		public int Width
		{
			get
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
				return this.ᜀ.Width;
			}
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
				this.ᜀ.Width = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0009B448 File Offset: 0x0009A448
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x0009B484 File Offset: 0x0009A484
		public ExcelShapeType ShapeType
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
				return ExcelShapeType.Comment;
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
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0009B4C0 File Offset: 0x0009A4C0
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x0009B508 File Offset: 0x0009A508
		public string AlternativeText
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
				return this.ᜀ.AlternativeText;
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
				this.ᜀ.AlternativeText = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0009B550 File Offset: 0x0009A550
		internal ICommentShape Wrapped
		{
			get
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
				return this.ᜀ;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0009B594 File Offset: 0x0009A594
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x0009B5DC File Offset: 0x0009A5DC
		public int Rotation
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
				return this.ᜀ.Rotation;
			}
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
				this.ᜀ.Rotation = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0009B624 File Offset: 0x0009A624
		public object Parent
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
				return this.ᜀ.Parent;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0009B66C File Offset: 0x0009A66C
		public IShapeFill Fill
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
				return this.ᜀ.Fill;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0009B6B4 File Offset: 0x0009A6B4
		public IShapeLineFormat Line
		{
			get
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
				return (this.ᜀ as XlsComment).Line;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0009B700 File Offset: 0x0009A700
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x0009B748 File Offset: 0x0009A748
		public bool IsTextLocked
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
				return this.ᜀ.IsTextLocked;
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
				this.ᜀ.IsTextLocked = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0009B790 File Offset: 0x0009A790
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x0009B7D8 File Offset: 0x0009A7D8
		public string OnAction
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
				return this.ᜀ.OnAction;
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
				this.ᜀ.OnAction = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0009B820 File Offset: 0x0009A820
		public IShadow Shadow
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
				return this.ᜀ.Shadow;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0009B868 File Offset: 0x0009A868
		public IFormat3D ThreeD
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
				return this.ᜀ.ThreeD;
			}
		}

		// Token: 0x04000D7A RID: 3450
		private byte \u2609\u008F\u009B\u00B0;

		// Token: 0x04000D7B RID: 3451
		private ICommentShape ᜀ;
	}
}
