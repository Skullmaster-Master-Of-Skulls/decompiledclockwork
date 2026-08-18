using System;
using System.Collections;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x0200162E RID: 5678
	public class FontStyles
	{
		// Token: 0x17004393 RID: 17299
		// (get) Token: 0x0600DCBB RID: 56507 RVA: 0x00303E3B File Offset: 0x0030203B
		public bool RegularAvailable
		{
			get
			{
				return this.styles.Contains("Regular") || this.styles.Contains("Normal");
			}
		}

		// Token: 0x17004394 RID: 17300
		// (get) Token: 0x0600DCBC RID: 56508 RVA: 0x00303E61 File Offset: 0x00302061
		public bool BoldAvailable
		{
			get
			{
				return this.styles.Contains("Bold");
			}
		}

		// Token: 0x17004395 RID: 17301
		// (get) Token: 0x0600DCBD RID: 56509 RVA: 0x00303E73 File Offset: 0x00302073
		public bool ItalicAvailable
		{
			get
			{
				return this.styles.Contains("Italic");
			}
		}

		// Token: 0x17004396 RID: 17302
		// (get) Token: 0x0600DCBE RID: 56510 RVA: 0x00303E85 File Offset: 0x00302085
		public bool BoldItalicAvailable
		{
			get
			{
				return this.styles.Contains("Bold Italic");
			}
		}

		// Token: 0x0600DCBF RID: 56511 RVA: 0x00303E97 File Offset: 0x00302097
		internal void AddStyle(string styleName)
		{
			this.styles.Add(styleName, string.Empty);
		}

		// Token: 0x0600DCC0 RID: 56512 RVA: 0x00303EAA File Offset: 0x003020AA
		internal void Clear()
		{
			this.styles.Clear();
		}

		// Token: 0x0600DCC1 RID: 56513 RVA: 0x00303EB7 File Offset: 0x003020B7
		internal bool Contains(string styleName)
		{
			return this.styles.Contains(styleName);
		}

		// Token: 0x04003E54 RID: 15956
		private IDictionary styles = new Hashtable();
	}
}
