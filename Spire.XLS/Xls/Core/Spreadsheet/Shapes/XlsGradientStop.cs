using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x0200021D RID: 541
	public class XlsGradientStop
	{
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x001247F4 File Offset: 0x001237F4
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
				return this.ᜁ;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x00124838 File Offset: 0x00123838
		// (set) Token: 0x0600207D RID: 8317 RVA: 0x0012487C File Offset: 0x0012387C
		public int Position
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
				return this.ᜂ;
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
				this.ᜂ = value;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x001248C0 File Offset: 0x001238C0
		// (set) Token: 0x0600207F RID: 8319 RVA: 0x00124904 File Offset: 0x00123904
		public int Transparency
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
				return this.ᜃ;
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
				this.ᜃ = value;
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x00124948 File Offset: 0x00123948
		// (set) Token: 0x06002081 RID: 8321 RVA: 0x0012498C File Offset: 0x0012398C
		public int Tint
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x001249D0 File Offset: 0x001239D0
		// (set) Token: 0x06002083 RID: 8323 RVA: 0x00124A14 File Offset: 0x00123A14
		public int Shade
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00124A58 File Offset: 0x00123A58
		public XlsGradientStop(OColor color, int position, int transparency) : this(color, position, transparency, -1, -1)
		{
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00124A70 File Offset: 0x00123A70
		public XlsGradientStop(OColor color, int position, int transparency, int tint, int shade)
		{
			this.ᜄ = -1;
			this.ᜅ = -1;
			base..ctor();
			this.ᜁ = color;
			this.ᜂ = position;
			this.ᜃ = transparency;
			this.ᜄ = tint;
			this.ᜅ = shade;
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00124AB8 File Offset: 0x00123AB8
		public XlsGradientStop(byte[] data, int offset)
		{
			int a_ = 0;
			this.ᜄ = -1;
			this.ᜅ = -1;
			base..ctor();
			if (data == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("刵夷丹崻", a_));
			}
			this.ᜂ = BitConverter.ToInt32(data, offset);
			offset += 4;
			int a_2 = BitConverter.ToInt32(data, offset);
			this.ᜁ = new OColor(spr\u1D39.ᜀ(a_2));
			offset += 4;
			this.ᜃ = BitConverter.ToInt32(data, offset);
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00124B38 File Offset: 0x00123B38
		internal void ᜀ(Stream A_0)
		{
			int a_ = 5;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (A_0 != null)
				{
					byte[] bytes = BitConverter.GetBytes(this.ᜂ);
					A_0.Write(bytes, 0, bytes.Length);
					bytes = BitConverter.GetBytes(this.ᜁ.Value);
					A_0.Write(bytes, 0, bytes.Length);
					bytes = BitConverter.GetBytes(this.ᜃ);
					A_0.Write(bytes, 0, bytes.Length);
					return;
				}
				break;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺䤼䴾⑀≂⡄", a_));
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00124BE0 File Offset: 0x00123BE0
		internal XlsGradientStop ᜀ()
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
			XlsGradientStop xlsGradientStop = (XlsGradientStop)base.MemberwiseClone();
			xlsGradientStop.ᜁ = new OColor(ExcelColors.Black);
			xlsGradientStop.ᜁ.ᜀ(this.ᜁ, false);
			return xlsGradientStop;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00124C48 File Offset: 0x00123C48
		internal bool ᜀ(XlsGradientStop A_0)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.ᜁ == this.ᜁ)
					{
						num = 1;
						continue;
					}
					return false;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 2:
					goto IL_52;
				case 4:
					if (A_0.ᜂ == this.ᜂ)
					{
						num = 7;
						continue;
					}
					return false;
				case 5:
					return false;
				case 6:
					goto IL_73;
				case 7:
					num = 2;
					continue;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
				IL_52:
				if (true)
				{
				}
				if (A_0.ᜅ != this.ᜅ)
				{
					return false;
				}
				num = 6;
			}
			return false;
			IL_73:
			return A_0.ᜄ == this.ᜄ;
		}

		// Token: 0x04001130 RID: 4400
		internal const int ᜀ = 12;

		// Token: 0x04001131 RID: 4401
		private OColor ᜁ;

		// Token: 0x04001132 RID: 4402
		private int ᜂ;

		// Token: 0x04001133 RID: 4403
		private byte[] \u2460\u008D\u00A4\u008B;

		// Token: 0x04001134 RID: 4404
		private int ᜃ;

		// Token: 0x04001135 RID: 4405
		private int ᜄ;

		// Token: 0x04001136 RID: 4406
		private int ᜅ;
	}
}
