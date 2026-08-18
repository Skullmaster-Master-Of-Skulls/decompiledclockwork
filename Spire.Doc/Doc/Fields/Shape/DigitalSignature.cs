using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x02000068 RID: 104
	public class DigitalSignature
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00007670 File Offset: 0x00006670
		internal DigitalSignature(DigitalSignatureType A_0)
		{
			this.ᜀ = A_0;
			this.ᜂ = DateTime.MinValue;
			this.ᜃ = "";
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000076AC File Offset: 0x000066AC
		public DigitalSignatureType Type
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000076F0 File Offset: 0x000066F0
		public DateTime DateTime
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
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00007734 File Offset: 0x00006734
		internal void ᜀ(DateTime A_0)
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
			this.ᜂ = A_0;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00007778 File Offset: 0x00006778
		public string Comments
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
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000077BC File Offset: 0x000067BC
		internal void ᜀ(string A_0)
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
			this.ᜃ = A_0;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00007800 File Offset: 0x00006800
		public bool IsValid
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
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00007844 File Offset: 0x00006844
		internal void ᜂ(bool A_0)
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
			this.ᜄ = A_0;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00007888 File Offset: 0x00006888
		public X509Certificate2 Certificate
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

		// Token: 0x0600004F RID: 79 RVA: 0x000078CC File Offset: 0x000068CC
		internal void ᜀ(X509Certificate2 A_0)
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
			this.ᜁ = A_0;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00007910 File Offset: 0x00006910
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00007954 File Offset: 0x00006954
		internal bool SignedXmlResult
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00007998 File Offset: 0x00006998
		internal ArrayList References
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
				return this.ᜆ;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000079DC File Offset: 0x000069DC
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00007A20 File Offset: 0x00006A20
		internal byte[] ImageBytes
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
				return this.ᜇ;
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
				this.ᜇ = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00007A64 File Offset: 0x00006A64
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00007AA8 File Offset: 0x00006AA8
		internal byte[] ImageBytesValid
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
				return this.ᜈ;
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
				this.ᜈ = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00007AEC File Offset: 0x00006AEC
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00007B30 File Offset: 0x00006B30
		internal byte[] ImageBytesInvalid
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00007B74 File Offset: 0x00006B74
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00007BB8 File Offset: 0x00006BB8
		internal bool Visible
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
				return this.\u170D;
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
				this.\u170D = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00007BFC File Offset: 0x00006BFC
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00007C40 File Offset: 0x00006C40
		internal string Text
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00007C84 File Offset: 0x00006C84
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00007CC8 File Offset: 0x00006CC8
		internal Guid SetupId
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
				return this.ᜋ;
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
				this.ᜋ = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00007D0C File Offset: 0x00006D0C
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00007D50 File Offset: 0x00006D50
		internal Guid ProviderId
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
				return this.ᜌ;
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
				this.ᜌ = value;
			}
		}

		// Token: 0x0400068F RID: 1679
		private readonly DigitalSignatureType ᜀ;

		// Token: 0x04000690 RID: 1680
		private X509Certificate2 ᜁ;

		// Token: 0x04000691 RID: 1681
		private DateTime ᜂ;

		// Token: 0x04000692 RID: 1682
		private byte[] \u25D9\u0094\u0083\u00A9;

		// Token: 0x04000693 RID: 1683
		private string ᜃ;

		// Token: 0x04000694 RID: 1684
		private bool ᜄ;

		// Token: 0x04000695 RID: 1685
		private bool ᜅ;

		// Token: 0x04000696 RID: 1686
		private readonly ArrayList ᜆ = new ArrayList();

		// Token: 0x04000697 RID: 1687
		private byte[] ᜇ;

		// Token: 0x04000698 RID: 1688
		private byte[] ᜈ;

		// Token: 0x04000699 RID: 1689
		private byte[] ᜉ;

		// Token: 0x0400069A RID: 1690
		private byte[] \u25D8\u00B0\u0093\u00A4;

		// Token: 0x0400069B RID: 1691
		private string[] \u2609\u0094\u00A2\u0086;

		// Token: 0x0400069C RID: 1692
		private string ᜊ;

		// Token: 0x0400069D RID: 1693
		private Guid ᜋ;

		// Token: 0x0400069E RID: 1694
		private Guid ᜌ;

		// Token: 0x0400069F RID: 1695
		private bool \u170D;
	}
}
