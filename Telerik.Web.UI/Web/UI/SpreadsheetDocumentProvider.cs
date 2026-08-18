using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Provider;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;
using Telerik.Web.Spreadsheet;

namespace Telerik.Web.UI
{
	// Token: 0x020008C8 RID: 2248
	public class SpreadsheetDocumentProvider : SpreadsheetProviderBase
	{
		// Token: 0x17001BEE RID: 7150
		// (get) Token: 0x06005494 RID: 21652 RVA: 0x00102D4D File Offset: 0x00100F4D
		// (set) Token: 0x06005495 RID: 21653 RVA: 0x00102D55 File Offset: 0x00100F55
		public int RetryAttempts
		{
			get
			{
				return this._retryAttempts;
			}
			set
			{
				this._retryAttempts = value;
			}
		}

		// Token: 0x17001BEF RID: 7151
		// (get) Token: 0x06005496 RID: 21654 RVA: 0x00102D5E File Offset: 0x00100F5E
		// (set) Token: 0x06005497 RID: 21655 RVA: 0x00102D66 File Offset: 0x00100F66
		public int RetryDelay
		{
			get
			{
				return this._retryDelay;
			}
			set
			{
				this._retryDelay = value;
			}
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x00102D6F File Offset: 0x00100F6F
		public SpreadsheetDocumentProvider()
		{
		}

		// Token: 0x06005499 RID: 21657 RVA: 0x00102D86 File Offset: 0x00100F86
		public SpreadsheetDocumentProvider(string path) : this()
		{
			this._path = path;
			this.LoadWorkbook();
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x00102D9B File Offset: 0x00100F9B
		protected internal SpreadsheetDocumentProvider(Workbook workbook) : this()
		{
			this._workbook = workbook;
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x00102DAC File Offset: 0x00100FAC
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "SpreadsheetDocumentProvider";
			}
			base.Initialize(name, config);
			this._path = config["fileName"];
			if (string.IsNullOrEmpty(this._path))
			{
				throw new ProviderException("Missing data file name. Please specify it with the fileName property.");
			}
			this.LoadWorkbook();
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x00102E10 File Offset: 0x00101010
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected void LoadWorkbook()
		{
			if (this._workbookLoaded)
			{
				return;
			}
			if (string.IsNullOrEmpty(this._path))
			{
				return;
			}
			if (!Path.IsPathRooted(this._path))
			{
				this._path = HttpContext.Current.Server.MapPath(this._path);
			}
			int i = 0;
			while (i < this.RetryAttempts)
			{
				try
				{
					this._workbook = Workbook.Load(this._path);
					this._workbookLoaded = true;
					break;
				}
				catch (Exception)
				{
					i++;
					Thread.Sleep(this.RetryDelay);
				}
			}
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x00102EA8 File Offset: 0x001010A8
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void SaveWorkbook(Workbook workbook)
		{
			if (!string.IsNullOrEmpty(this._path))
			{
				workbook.Save(this._path);
			}
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x00102EC4 File Offset: 0x001010C4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Worksheet> GetSheets()
		{
			List<Worksheet> list = new List<Worksheet>();
			return this._workbook.Sheets.ToList<Worksheet>();
		}

		// Token: 0x04001468 RID: 5224
		private string _path;

		// Token: 0x04001469 RID: 5225
		private int _retryAttempts = 5;

		// Token: 0x0400146A RID: 5226
		private int _retryDelay = 100;

		// Token: 0x0400146B RID: 5227
		private Workbook _workbook;

		// Token: 0x0400146C RID: 5228
		private bool _workbookLoaded;
	}
}
