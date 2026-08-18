using System;
using System.ComponentModel;
using Spire.DataExport.Common;

namespace Spire.DataExport.TXT
{
	// Token: 0x0200020C RID: 524
	public class CSVOption : DisposabledObject, ICloneable
	{
		// Token: 0x06000FD7 RID: 4055 RVA: 0x000AAC10 File Offset: 0x000A9C10
		public CSVOption(object Holder)
		{
			this.ᜀ = Holder;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000AAC44 File Offset: 0x000A9C44
		protected override void Dispose(bool Disposing)
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
			bool flag = this.ᜁ;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x000AAC88 File Offset: 0x000A9C88
		public object Clone()
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
			return new CSVOption(this)
			{
				AllowQuote = this.AllowQuote,
				Quote = this.Quote,
				Separator = this.Separator
			};
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x000AACF0 File Offset: 0x000A9CF0
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x000AAD34 File Offset: 0x000A9D34
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(",")]
		public string Separator
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
				return this.ᜂ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						if (false)
						{
						}
						this.ᜂ = value;
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						return;
					}
					if (!(value != this.ᜂ))
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x000AADB4 File Offset: 0x000A9DB4
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x000AADF8 File Offset: 0x000A9DF8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue('"')]
		public char Quote
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
				return this.ᜃ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							this.ᜃ = value;
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (value == this.ᜃ)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x000AAE74 File Offset: 0x000A9E74
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x000AAEB8 File Offset: 0x000A9EB8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		public bool AllowQuote
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
				return this.ᜄ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜄ = value;
						num = 0;
						continue;
					}
					if (value == this.ᜄ)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x04000B96 RID: 2966
		private object ᜀ;

		// Token: 0x04000B97 RID: 2967
		private bool ᜁ;

		// Token: 0x04000B98 RID: 2968
		private long \u2460\u00AE\u008A\u008E;

		// Token: 0x04000B99 RID: 2969
		private long[] \u25D8\u0087\u0092\u0097;

		// Token: 0x04000B9A RID: 2970
		private string \u25D9\u0085\u0086\u0099;

		// Token: 0x04000B9B RID: 2971
		private string ᜂ = spr\u1C2B.ᡜ;

		// Token: 0x04000B9C RID: 2972
		private char ᜃ = '"';

		// Token: 0x04000B9D RID: 2973
		private bool ᜄ = true;
	}
}
