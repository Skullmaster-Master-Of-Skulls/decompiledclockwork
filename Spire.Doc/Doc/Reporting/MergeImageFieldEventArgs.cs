using System;
using System.Drawing;
using System.IO;
using Spire.Doc.Interface;

namespace Spire.Doc.Reporting
{
	// Token: 0x02000109 RID: 265
	public class MergeImageFieldEventArgs : MergeFieldEventArgs
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00056278 File Offset: 0x00055278
		public bool UseText
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

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x000562BC File Offset: 0x000552BC
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x00056300 File Offset: 0x00055300
		public string ImageFileName
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
				this.ᜀ(this.ᜂ);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00056350 File Offset: 0x00055350
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x00056394 File Offset: 0x00055394
		public Stream ImageStream
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
				this.ᜀ(this.ᜃ);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x000563E4 File Offset: 0x000553E4
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x00056428 File Offset: 0x00055428
		public Image Image
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

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0005646C File Offset: 0x0005546C
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x000564B0 File Offset: 0x000554B0
		public bool Skip
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

		// Token: 0x06000771 RID: 1905 RVA: 0x000564F4 File Offset: 0x000554F4
		public MergeImageFieldEventArgs(IDocument doc, string tableName, int rowIndex, IMergeField field, Image image) : base(doc, tableName, rowIndex, field, null)
		{
			this.ᜁ = image;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00056520 File Offset: 0x00055520
		public MergeImageFieldEventArgs(IDocument doc, string tableName, int rowIndex, IMergeField field, object obj) : base(doc, tableName, rowIndex, field, obj)
		{
			this.ᜁ = (obj as Image);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00056554 File Offset: 0x00055554
		private void ᜀ(string A_0)
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
			this.ᜁ = new Bitmap(A_0);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0005659C File Offset: 0x0005559C
		private void ᜀ(Stream A_0)
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
			this.ᜁ = new Bitmap(A_0);
		}

		// Token: 0x04000E1D RID: 3613
		private bool ᜀ;

		// Token: 0x04000E1E RID: 3614
		private Image ᜁ;

		// Token: 0x04000E1F RID: 3615
		private bool \u2609\u008E\u0099\u00A1;

		// Token: 0x04000E20 RID: 3616
		private string ᜂ = "";

		// Token: 0x04000E21 RID: 3617
		private bool[] \u25D8\u00A4\u009D\u00AB;

		// Token: 0x04000E22 RID: 3618
		private Stream ᜃ;

		// Token: 0x04000E23 RID: 3619
		private float \u2593\u0086\u00A7\u008D;

		// Token: 0x04000E24 RID: 3620
		private bool ᜄ;
	}
}
