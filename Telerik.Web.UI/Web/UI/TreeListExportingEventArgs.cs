using System;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02001212 RID: 4626
	public class TreeListExportingEventArgs : EventArgs
	{
		// Token: 0x17003DAB RID: 15787
		// (get) Token: 0x0600BF25 RID: 48933 RVA: 0x002A56CB File Offset: 0x002A38CB
		// (set) Token: 0x0600BF26 RID: 48934 RVA: 0x002A56D3 File Offset: 0x002A38D3
		public byte[] ExportBytes { get; internal set; }

		// Token: 0x17003DAC RID: 15788
		// (get) Token: 0x0600BF27 RID: 48935 RVA: 0x002A56DC File Offset: 0x002A38DC
		// (set) Token: 0x0600BF28 RID: 48936 RVA: 0x002A56E4 File Offset: 0x002A38E4
		public ExportFormat ExportType { get; internal set; }

		// Token: 0x17003DAD RID: 15789
		// (get) Token: 0x0600BF29 RID: 48937 RVA: 0x002A56ED File Offset: 0x002A38ED
		public string ExportOutput
		{
			get
			{
				return Encoding.GetEncoding(1252).GetString(this.ExportBytes);
			}
		}

		// Token: 0x0600BF2A RID: 48938 RVA: 0x002A5704 File Offset: 0x002A3904
		public TreeListExportingEventArgs(byte[] exportOutput, ExportFormat exportType)
		{
			this.ExportBytes = exportOutput;
			this.ExportType = exportType;
		}
	}
}
