using System;
using System.Collections;
using System.Drawing;
using Spire.Xls.Core;

namespace Spire.Xls.Collections
{
	// Token: 0x0200001E RID: 30
	public class BordersCollection : IBorders
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00014BC0 File Offset: 0x00013BC0
		internal BordersCollection(IBorders A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00014BDC File Offset: 0x00013BDC
		public IEnumerator GetEnumerator()
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
			return this.ᜀ.GetEnumerator();
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00014C24 File Offset: 0x00013C24
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00014C6C File Offset: 0x00013C6C
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ.KnownColor = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00014CB4 File Offset: 0x00013CB4
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00014CFC File Offset: 0x00013CFC
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ.Color = value;
			}
		}

		// Token: 0x170000EC RID: 236
		public IBorder this[BordersLineType Index]
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
				return this.ᜀ[Index];
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00014D8C File Offset: 0x00013D8C
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00014DD4 File Offset: 0x00013DD4
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00014E1C File Offset: 0x00013E1C
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00014E64 File Offset: 0x00013E64
		public LineStyleType Value
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
				return this.ᜀ.Value;
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
				this.ᜀ.Value = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00014EAC File Offset: 0x00013EAC
		public int Count
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
				return this.ᜀ.Count;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00014EF4 File Offset: 0x00013EF4
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

		// Token: 0x0400006B RID: 107
		private string \u2593\u00A8\u009F\u009C;

		// Token: 0x0400006C RID: 108
		private string \u25D8\u0097\u0087\u00B0;

		// Token: 0x0400006D RID: 109
		private byte \u2460\u008D\u0099\u0099;

		// Token: 0x0400006E RID: 110
		private bool \u25D8\u009A\u00A9\u00A1;

		// Token: 0x0400006F RID: 111
		private bool \u2609\u0084\u0084\u0082;

		// Token: 0x04000070 RID: 112
		private IBorders ᜀ;
	}
}
