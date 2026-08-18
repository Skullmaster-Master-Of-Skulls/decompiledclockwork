using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000057 RID: 87
	internal class SftpOpenRequest : SftpRequest
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00012C5A File Offset: 0x00010E5A
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Open;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00012C5D File Offset: 0x00010E5D
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x00012C79 File Offset: 0x00010E79
		public string Filename
		{
			get
			{
				return this.Encoding.GetString(this._fileName, 0, this._fileName.Length);
			}
			private set
			{
				this._fileName = this.Encoding.GetBytes(value);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00012C8D File Offset: 0x00010E8D
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x00012C95 File Offset: 0x00010E95
		public Flags Flags { get; private set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00012C9E File Offset: 0x00010E9E
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x00012CAB File Offset: 0x00010EAB
		public SftpFileAttributes Attributes
		{
			get
			{
				return SftpFileAttributes.FromBytes(this._attributes);
			}
			private set
			{
				this._attributes = value.GetBytes();
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00012CB9 File Offset: 0x00010EB9
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00012CC1 File Offset: 0x00010EC1
		public Encoding Encoding { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00012CCA File Offset: 0x00010ECA
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._fileName.Length + 4 + this._attributes.Length;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00012CE8 File Offset: 0x00010EE8
		public SftpOpenRequest(uint protocolVersion, uint requestId, string fileName, Encoding encoding, Flags flags, Action<SftpHandleResponse> handleAction, Action<SftpStatusResponse> statusAction) : this(protocolVersion, requestId, fileName, encoding, flags, SftpFileAttributes.Empty, handleAction, statusAction)
		{
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00012D0B File Offset: 0x00010F0B
		private SftpOpenRequest(uint protocolVersion, uint requestId, string fileName, Encoding encoding, Flags flags, SftpFileAttributes attributes, Action<SftpHandleResponse> handleAction, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Encoding = encoding;
			this.Filename = fileName;
			this.Flags = flags;
			this.Attributes = attributes;
			base.SetAction(handleAction);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00012D3E File Offset: 0x00010F3E
		protected override void LoadData()
		{
			base.LoadData();
			throw new NotSupportedException();
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00012D4B File Offset: 0x00010F4B
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._fileName);
			base.Write((uint)this.Flags);
			base.Write(this._attributes);
		}

		// Token: 0x04000207 RID: 519
		private byte[] _fileName;

		// Token: 0x04000208 RID: 520
		private byte[] _attributes;
	}
}
