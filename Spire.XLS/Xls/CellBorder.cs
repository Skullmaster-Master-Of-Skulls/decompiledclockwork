using System;
using System.Drawing;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;

namespace Spire.Xls
{
	// Token: 0x0200010A RID: 266
	public class CellBorder : IBorder
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x00075D40 File Offset: 0x00074D40
		internal CellBorder(IBorder A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00075D5C File Offset: 0x00074D5C
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00075DA4 File Offset: 0x00074DA4
		public ExcelColors KnownColor
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
				return this.ᜀ.KnownColor;
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
				this.ᜀ.KnownColor = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00075DEC File Offset: 0x00074DEC
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00075E34 File Offset: 0x00074E34
		public Color Color
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
				return this.ᜀ.Color;
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
				this.ᜀ.Color = value;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00075E7C File Offset: 0x00074E7C
		public OColor OColor
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
				return this.ᜀ.OColor;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00075EC4 File Offset: 0x00074EC4
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00075F0C File Offset: 0x00074F0C
		public LineStyleType LineStyle
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
				return this.ᜀ.LineStyle;
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
				this.ᜀ.LineStyle = value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00075F54 File Offset: 0x00074F54
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x00075F9C File Offset: 0x00074F9C
		public bool ShowDiagonalLine
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
				return this.ᜀ.ShowDiagonalLine;
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
				this.ᜀ.ShowDiagonalLine = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00075FE4 File Offset: 0x00074FE4
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x00076030 File Offset: 0x00075030
		internal BordersLineType BorderIndex
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
				return ((XlsBorder)this.ᜀ).BorderIndex;
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
				((XlsBorder)this.ᜀ).BorderIndex = value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0007607C File Offset: 0x0007507C
		internal IBorder Wrapped
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
				return this.ᜀ;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x000760C0 File Offset: 0x000750C0
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

		// Token: 0x06000C0D RID: 3085 RVA: 0x00076108 File Offset: 0x00075108
		public void CopyFrom(CellBorder srcBorder)
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
			this.ᜀ.KnownColor = srcBorder.KnownColor;
			this.ᜀ.LineStyle = srcBorder.LineStyle;
		}

		// Token: 0x04000A03 RID: 2563
		private long \u2593\u007F\u00A0\u00A6;

		// Token: 0x04000A04 RID: 2564
		private float \u25D8\u0093\u0096\u0080;

		// Token: 0x04000A05 RID: 2565
		private IBorder ᜀ;
	}
}
