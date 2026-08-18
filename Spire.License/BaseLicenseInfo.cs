using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Spire.License
{
	// Token: 0x0200000D RID: 13
	[XmlRoot("License")]
	[Serializable]
	public class BaseLicenseInfo : License
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00005F90 File Offset: 0x00004190
		[XmlIgnore]
		public override string LicenseKey
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
				return this.Key;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00005FD4 File Offset: 0x000041D4
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00006018 File Offset: 0x00004218
		[XmlAttribute]
		public virtual string Key
		{
			[CompilerGenerated]
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
				return this.b;
			}
			[CompilerGenerated]
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
				this.b = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000605C File Offset: 0x0000425C
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000060A0 File Offset: 0x000042A0
		[XmlAttribute]
		public virtual string Version
		{
			[CompilerGenerated]
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
				return this.c;
			}
			[CompilerGenerated]
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
				this.c = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000060E4 File Offset: 0x000042E4
		[XmlIgnore]
		internal f Version2
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_75;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_77;
						default:
							if (false)
							{
							}
							this.a = f.a(this.Version);
							num = 0;
							continue;
						}
						break;
					}
					if (true)
					{
					}
					if (this.a != null)
					{
						break;
					}
					num = 2;
				}
				IL_75:
				IL_77:
				return this.a;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00006170 File Offset: 0x00004370
		public override void Dispose()
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
			this.Key = string.Empty;
		}

		// Token: 0x04000026 RID: 38
		private float \u25D9\u0087\u00A3\u007F;

		// Token: 0x04000027 RID: 39
		private int \u25D9\u00A4\u009F\u0096;

		// Token: 0x04000028 RID: 40
		[XmlIgnore]
		internal f a;

		// Token: 0x04000029 RID: 41
		[CompilerGenerated]
		private string b;

		// Token: 0x0400002A RID: 42
		[CompilerGenerated]
		private string c;
	}
}
