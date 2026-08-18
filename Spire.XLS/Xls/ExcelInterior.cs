using System;
using System.Drawing;
using Spire.Xls.Core;

namespace Spire.Xls
{
	// Token: 0x0200016E RID: 366
	public class ExcelInterior : IInterior
	{
		// Token: 0x0600118B RID: 4491 RVA: 0x000AD490 File Offset: 0x000AC490
		internal ExcelInterior(IInterior A_0)
		{
			this.m_interior = A_0;
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x000AD4AC File Offset: 0x000AC4AC
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x000AD4F4 File Offset: 0x000AC4F4
		public ExcelColors PatternKnownColor
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
				return this.m_interior.PatternKnownColor;
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
				this.m_interior.PatternKnownColor = value;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x000AD53C File Offset: 0x000AC53C
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x000AD584 File Offset: 0x000AC584
		public Color PatternColor
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
				return this.m_interior.PatternColor;
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
				this.m_interior.PatternColor = value;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x000AD5CC File Offset: 0x000AC5CC
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x000AD614 File Offset: 0x000AC614
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
				return this.m_interior.KnownColor;
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
				this.m_interior.KnownColor = value;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x000AD65C File Offset: 0x000AC65C
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x000AD6A4 File Offset: 0x000AC6A4
		public Color Color
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
				return this.m_interior.Color;
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
				this.m_interior.Color = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x000AD6EC File Offset: 0x000AC6EC
		public ExcelGradient Gradient
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
				return this.m_interior.Gradient;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x000AD734 File Offset: 0x000AC734
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x000AD77C File Offset: 0x000AC77C
		public ExcelPatternType FillPattern
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
				return this.m_interior.FillPattern;
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
				this.m_interior.FillPattern = value;
			}
		}

		// Token: 0x04000E29 RID: 3625
		private float \u25D9\u0081\u0092\u009A;

		// Token: 0x04000E2A RID: 3626
		public IInterior m_interior;
	}
}
