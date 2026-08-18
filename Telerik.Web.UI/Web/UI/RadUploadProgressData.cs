using System;
using System.IO;
using Telerik.Web.UI.Upload;

namespace Telerik.Web.UI
{
	// Token: 0x02001B7F RID: 7039
	internal class RadUploadProgressData : ProgressData
	{
		// Token: 0x1700535C RID: 21340
		// (get) Token: 0x060110EF RID: 69871 RVA: 0x003C3296 File Offset: 0x003C1496
		// (set) Token: 0x060110F0 RID: 69872 RVA: 0x003C329E File Offset: 0x003C149E
		public int RequestLength
		{
			get
			{
				return this._requestLength;
			}
			set
			{
				this._requestLength = value;
			}
		}

		// Token: 0x1700535D RID: 21341
		// (get) Token: 0x060110F1 RID: 69873 RVA: 0x003C32A7 File Offset: 0x003C14A7
		// (set) Token: 0x060110F2 RID: 69874 RVA: 0x003C32AF File Offset: 0x003C14AF
		public int CompleteBytes
		{
			get
			{
				return this._completeBytes;
			}
			set
			{
				this._completeBytes = value;
			}
		}

		// Token: 0x1700535E RID: 21342
		// (get) Token: 0x060110F3 RID: 69875 RVA: 0x003C32B8 File Offset: 0x003C14B8
		private string CurrentFileName
		{
			get
			{
				if (this.CurrentOperationText == null)
				{
					return string.Empty;
				}
				return this.CurrentOperationText.ToString();
			}
		}

		// Token: 0x1700535F RID: 21343
		// (get) Token: 0x060110F4 RID: 69876 RVA: 0x003C32D3 File Offset: 0x003C14D3
		// (set) Token: 0x060110F5 RID: 69877 RVA: 0x003C32DB File Offset: 0x003C14DB
		internal bool CheckHandlerRegistration
		{
			get
			{
				return this._checkHandlerRegistration;
			}
			set
			{
				this._checkHandlerRegistration = value;
			}
		}

		// Token: 0x060110F6 RID: 69878 RVA: 0x003C32E4 File Offset: 0x003C14E4
		protected override void SerializeCustomData(TextWriter writer)
		{
			base.SerializeCustomData(writer);
			if (this.CheckHandlerRegistration && !RadUploadHttpModule.IsRegistered)
			{
				writer.Write(",ProgressError:'RadUpload Ajax callback error: Cannot find RadUploadHttpModule.\\r\\n\\r\\nDid you register the RadUploadHttpModule?\\r\\n\\r\\nIf you do not register the HttpModule you cannot benefit from RadMemoryOptimization and RadProgressArea.\\r\\n\\r\\nPlease, see the help for more details: RadUpload - Using RadUpload - Configuration - RadUploadHttpModule.'");
			}
			writer.Write(",RadUpload:{");
			writer.Write("RequestSize:");
			writer.Write(this.RequestLength);
			writer.Write(",");
			writer.Write("Bytes:");
			writer.Write(this.CompleteBytes);
			writer.Write(",");
			writer.Write("FilesCount:");
			writer.Write(this.SecondaryValue);
			writer.Write(",");
			writer.Write("CurrentFileName:'");
			writer.Write(base.FormatString(this.CurrentFileName));
			writer.Write("',");
			writer.Write("RequestLength:");
			writer.Write(this.RequestLength);
			writer.Write("}");
		}

		// Token: 0x04004C63 RID: 19555
		private int _requestLength;

		// Token: 0x04004C64 RID: 19556
		private int _completeBytes;

		// Token: 0x04004C65 RID: 19557
		private bool _checkHandlerRegistration = true;
	}
}
