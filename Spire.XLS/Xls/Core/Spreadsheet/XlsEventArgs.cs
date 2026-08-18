using System;
using System.Diagnostics;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x020005F3 RID: 1523
	[DebuggerStepThrough]
	public class XlsEventArgs : EventArgs
	{
		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060059F3 RID: 23027 RVA: 0x003864DC File Offset: 0x003854DC
		public object newValue
		{
			[DebuggerStepThrough]
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

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x060059F4 RID: 23028 RVA: 0x00386520 File Offset: 0x00385520
		public object oldValue
		{
			[DebuggerStepThrough]
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

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060059F5 RID: 23029 RVA: 0x00386564 File Offset: 0x00385564
		public string Name
		{
			[DebuggerStepThrough]
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

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x060059F6 RID: 23030 RVA: 0x003865A8 File Offset: 0x003855A8
		// (set) Token: 0x060059F7 RID: 23031 RVA: 0x003865EC File Offset: 0x003855EC
		public XlsEventArgs Next
		{
			[DebuggerStepThrough]
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
				this.ᜄ = null;
			}
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x00386630 File Offset: 0x00385630
		private XlsEventArgs()
		{
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x00386644 File Offset: 0x00385644
		public XlsEventArgs(object oldValue, object newValue, string objectName) : this(oldValue, newValue, objectName, null)
		{
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x0038665C File Offset: 0x0038565C
		public XlsEventArgs(object old, object newValue, string objectName, XlsEventArgs next)
		{
			this.ᜁ = old;
			this.ᜂ = newValue;
			this.ᜃ = objectName;
			this.ᜄ = next;
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x060059FB RID: 23035 RVA: 0x0038668C File Offset: 0x0038568C
		public new static XlsEventArgs Empty
		{
			[DebuggerStepThrough]
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
				return XlsEventArgs.ᜀ;
			}
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x003866CC File Offset: 0x003856CC
		// Note: this type is marked as 'beforefieldinit'.
		static XlsEventArgs()
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
			XlsEventArgs.ᜀ = new XlsEventArgs();
		}

		// Token: 0x04002C23 RID: 11299
		private static XlsEventArgs ᜀ;

		// Token: 0x04002C24 RID: 11300
		private long[] \u2460\u00AB\u00A7\u0087;

		// Token: 0x04002C25 RID: 11301
		private object ᜁ;

		// Token: 0x04002C26 RID: 11302
		private string[] \u25D8\u008A\u0095\u0099;

		// Token: 0x04002C27 RID: 11303
		private byte \u25D8\u0086\u00A6\u0093;

		// Token: 0x04002C28 RID: 11304
		private object ᜂ;

		// Token: 0x04002C29 RID: 11305
		private long \u25D8\u0083\u008E\u008E;

		// Token: 0x04002C2A RID: 11306
		private float[] \u25D9\u008C\u0089\u0096;

		// Token: 0x04002C2B RID: 11307
		private string ᜃ;

		// Token: 0x04002C2C RID: 11308
		private XlsEventArgs ᜄ;
	}
}
