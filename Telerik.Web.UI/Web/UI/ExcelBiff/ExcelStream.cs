using System;
using System.IO;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AA1 RID: 2721
	internal class ExcelStream : IDisposable
	{
		// Token: 0x060067D2 RID: 26578 RVA: 0x001846F5 File Offset: 0x001828F5
		public ExcelStream(string name)
		{
			if (name == null || name.Length < 1)
			{
				throw new ArgumentException("Invalid ExcelStream name", "name");
			}
			this.name = name;
		}

		// Token: 0x17002224 RID: 8740
		// (get) Token: 0x060067D3 RID: 26579 RVA: 0x00184720 File Offset: 0x00182920
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002225 RID: 8741
		// (get) Token: 0x060067D4 RID: 26580 RVA: 0x00184728 File Offset: 0x00182928
		public Stream ServerStream
		{
			get
			{
				if (this.serverStream == null)
				{
					this.serverStream = new MemoryStream();
				}
				return this.serverStream;
			}
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x00184743 File Offset: 0x00182943
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060067D6 RID: 26582 RVA: 0x0018474C File Offset: 0x0018294C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.serverStream != null)
			{
				this.serverStream.Dispose();
			}
		}

		// Token: 0x04001AC9 RID: 6857
		private string name;

		// Token: 0x04001ACA RID: 6858
		private Stream serverStream;
	}
}
