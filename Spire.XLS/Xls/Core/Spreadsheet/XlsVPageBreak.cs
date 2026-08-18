using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200016A RID: 362
	public class XlsVPageBreak : XlsObject, IVPageBreak
	{
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x000A9CB8 File Offset: 0x000A8CB8
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x000A9D38 File Offset: 0x000A8D38
		protected internal IXLSRange Location
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
				return this.ᜂ.AllocatedRange[(int)(this.ᜀ.ᜃ() + 1U), (int)(this.ᜀ.ᜁ() + 1), (int)(this.ᜀ.ᜀ() + 1U), (int)(this.ᜀ.ᜁ() + 1)];
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
				this.ᜀ.ᜀ((ushort)(value.Column - 1));
				this.ᜀ.ᜀ((uint)((ushort)(value.Row - 1)));
				this.ᜀ.ᜁ((uint)((ushort)(value.LastRow - 1)));
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x000A9DB0 File Offset: 0x000A8DB0
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x000A9DF4 File Offset: 0x000A8DF4
		public PageBreakType Type
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
				return this.ᜁ;
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
				this.ᜁ = value;
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000A9E38 File Offset: 0x000A8E38
		internal XlsVPageBreak(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000A9E5C File Offset: 0x000A8E5C
		private XlsVPageBreak(spr\u1DF5 A_0, object A_1, sprἛ A_2) : this(A_0, A_1)
		{
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000A9E74 File Offset: 0x000A8E74
		internal XlsVPageBreak(spr\u1DF5 A_0, object A_1, spr\u2583.ᜀ A_2) : this(A_0, A_1)
		{
			this.ᜀ = A_2;
			this.ᜁ = PageBreakType.Manual;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000A9E98 File Offset: 0x000A8E98
		internal XlsVPageBreak(spr\u1DF5 A_0, object A_1, IXLSRange A_2) : this(A_0, A_1)
		{
			this.ᜀ = new spr\u2583.ᜀ();
			this.ᜀ.ᜀ((ushort)(A_2.Column - 1));
			this.ᜀ.ᜀ((uint)((ushort)(A_2.Row - 1)));
			this.ᜀ.ᜁ((uint)((ushort)(A_2.LastRow - 1)));
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x000A9EF4 File Offset: 0x000A8EF4
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x000A9F5C File Offset: 0x000A8F5C
		internal spr\u2583.ᜀ VPageBreak
		{
			get
			{
				int a_ = 15;
				if (this.ᜀ != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						return this.ᜀ;
					}
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("ፄᝆ⡈ⱊ⡌ൎ⍐㙒㑔㱖", a_));
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
				this.ᜀ = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x000A9FA0 File Offset: 0x000A8FA0
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x000A9FE8 File Offset: 0x000A8FE8
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
				return (int)(this.ᜀ.ᜁ() + 1);
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
				this.ᜀ.ᜀ((ushort)(value - 1));
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x000AA034 File Offset: 0x000A9034
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x000AA07C File Offset: 0x000A907C
		public int StartRow
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
				return (int)(this.ᜀ.ᜃ() + 1U);
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
				this.ᜀ.ᜀ((uint)((ushort)(value - 1)));
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x000AA0C8 File Offset: 0x000A90C8
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x000AA110 File Offset: 0x000A9110
		public int EndRow
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
				return (int)(this.ᜀ.ᜀ() + 1U);
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
				this.ᜀ.ᜁ((uint)((ushort)(value - 1)));
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000AA15C File Offset: 0x000A915C
		private void ᜀ()
		{
			int a_ = 14;
			object obj = base.FindParent(typeof(XlsWorksheet));
			if (obj == null)
			{
				if (true)
				{
				}
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.ᜂ = (XlsWorksheet)obj;
					return;
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ᑃ❅㩇⽉≋㩍灏㵑㙓㱕㵗㥙⡛繝͟͡੣ࡥݧṩ䱫౭ᕯ剱ታ᥵൷ᑹ᡻偽", a_));
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000AA1D8 File Offset: 0x000A91D8
		public XlsVPageBreak Clone(object parent)
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
			XlsVPageBreak xlsVPageBreak = (XlsVPageBreak)base.MemberwiseClone();
			xlsVPageBreak.SetParent(parent);
			xlsVPageBreak.ᜀ();
			this.ᜀ = (spr\u2583.ᜀ)this.ᜀ.ᜂ();
			return xlsVPageBreak;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000AA244 File Offset: 0x000A9244
		internal void ᜀ(int A_0, int A_1, int A_2)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6F;
				case 2:
					goto IL_5C;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5C:
					this.ᜀ = new spr\u2583.ᜀ();
					num = 0;
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (this.ᜀ != null)
					{
						goto IL_71;
					}
					num = 2;
					break;
				}
			}
			IL_6F:
			IL_71:
			this.ᜀ.ᜀ((ushort)(A_0 - 1));
			this.ᜀ.ᜀ((uint)((ushort)(A_1 - 1)));
			this.ᜀ.ᜁ((uint)((ushort)(A_2 - 1)));
		}

		// Token: 0x04000E15 RID: 3605
		private spr\u2583.ᜀ ᜀ;

		// Token: 0x04000E16 RID: 3606
		private PageBreakType ᜁ = PageBreakType.Manual;

		// Token: 0x04000E17 RID: 3607
		private string \u25D9\u009D\u009A\u0083;

		// Token: 0x04000E18 RID: 3608
		private XlsWorksheet ᜂ;
	}
}
