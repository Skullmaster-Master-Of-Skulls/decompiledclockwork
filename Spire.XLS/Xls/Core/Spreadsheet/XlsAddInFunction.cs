using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000127 RID: 295
	public class XlsAddInFunction : XlsObject, IAddInFunction, ICloneParent
	{
		// Token: 0x06000CCD RID: 3277 RVA: 0x0007D374 File Offset: 0x0007C374
		internal XlsAddInFunction(spr\u1DF5 A_0, object A_1, int A_2, int A_3) : base(A_0, A_1)
		{
			this.ᜀ = A_2;
			this.ᜁ = A_3;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0007D398 File Offset: 0x0007C398
		private void ᜀ()
		{
			int a_ = 2;
			this.ᜂ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜂ == null)
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
					break;
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("唷改帻儽⼿⥁", a_));
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0007D418 File Offset: 0x0007C418
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x0007D45C File Offset: 0x0007C45C
		public int BookIndex
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
				this.ᜀ = value;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0007D4A0 File Offset: 0x0007C4A0
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0007D4E4 File Offset: 0x0007C4E4
		public int NameIndex
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0007D528 File Offset: 0x0007C528
		public string Name
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
				XlsExternWorkbook xlsExternWorkbook = this.ᜂ.ExternWorkbooks[this.BookIndex];
				return xlsExternWorkbook.ExternNames.ᜀ(this.NameIndex).ᜃ();
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0007D590 File Offset: 0x0007C590
		public object Clone(object parent)
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
			XlsAddInFunction xlsAddInFunction = (XlsAddInFunction)base.MemberwiseClone();
			xlsAddInFunction.SetParent(parent);
			xlsAddInFunction.ᜀ();
			return xlsAddInFunction;
		}

		// Token: 0x04000B7E RID: 2942
		private string \u25D9\u009F\u00B0\u00A7;

		// Token: 0x04000B7F RID: 2943
		private int ᜀ;

		// Token: 0x04000B80 RID: 2944
		private int ᜁ;

		// Token: 0x04000B81 RID: 2945
		private byte \u2609\u0099\u008F\u0087;

		// Token: 0x04000B82 RID: 2946
		private int \u2593\u009E\u0084\u008A;

		// Token: 0x04000B83 RID: 2947
		private byte[] \u2593\u00A5\u009F\u0085;

		// Token: 0x04000B84 RID: 2948
		private XlsWorkbook ᜂ;
	}
}
