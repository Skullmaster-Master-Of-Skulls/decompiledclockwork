using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200005E RID: 94
	internal abstract class SftpRequest : SftpMessage
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001319D File Offset: 0x0001139D
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x000131A5 File Offset: 0x000113A5
		public uint RequestId { get; private set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x000131AE File Offset: 0x000113AE
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x000131B6 File Offset: 0x000113B6
		public uint ProtocolVersion { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x000128B8 File Offset: 0x00010AB8
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4;
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000131BF File Offset: 0x000113BF
		protected SftpRequest(uint protocolVersion, uint requestId, Action<SftpStatusResponse> statusAction)
		{
			this.RequestId = requestId;
			this.ProtocolVersion = protocolVersion;
			this._statusAction = statusAction;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000131DC File Offset: 0x000113DC
		public void Complete(SftpResponse response)
		{
			if (response is SftpStatusResponse)
			{
				this._statusAction(response as SftpStatusResponse);
				return;
			}
			if (response is SftpAttrsResponse)
			{
				this._attrsAction(response as SftpAttrsResponse);
				return;
			}
			if (response is SftpDataResponse)
			{
				this._dataAction(response as SftpDataResponse);
				return;
			}
			if (response is SftpExtendedReplyResponse)
			{
				this._extendedReplyAction(response as SftpExtendedReplyResponse);
				return;
			}
			if (response is SftpHandleResponse)
			{
				this._handleAction(response as SftpHandleResponse);
				return;
			}
			if (response is SftpNameResponse)
			{
				this._nameAction(response as SftpNameResponse);
				return;
			}
			throw new InvalidOperationException(string.Format("Response of type '{0}' is not expected.", response.GetType().Name));
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001329F File Offset: 0x0001149F
		protected void SetAction(Action<SftpAttrsResponse> action)
		{
			this._attrsAction = action;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000132A8 File Offset: 0x000114A8
		protected void SetAction(Action<SftpDataResponse> action)
		{
			this._dataAction = action;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000132B1 File Offset: 0x000114B1
		protected void SetAction(Action<SftpExtendedReplyResponse> action)
		{
			this._extendedReplyAction = action;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000132BA File Offset: 0x000114BA
		protected void SetAction(Action<SftpHandleResponse> action)
		{
			this._handleAction = action;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000132C3 File Offset: 0x000114C3
		protected void SetAction(Action<SftpNameResponse> action)
		{
			this._nameAction = action;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000132CC File Offset: 0x000114CC
		protected override void LoadData()
		{
			throw new InvalidOperationException("Request cannot be saved.");
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000132D8 File Offset: 0x000114D8
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.RequestId);
		}

		// Token: 0x04000218 RID: 536
		private readonly Action<SftpStatusResponse> _statusAction;

		// Token: 0x04000219 RID: 537
		private Action<SftpAttrsResponse> _attrsAction;

		// Token: 0x0400021A RID: 538
		private Action<SftpDataResponse> _dataAction;

		// Token: 0x0400021B RID: 539
		private Action<SftpExtendedReplyResponse> _extendedReplyAction;

		// Token: 0x0400021C RID: 540
		private Action<SftpHandleResponse> _handleAction;

		// Token: 0x0400021D RID: 541
		private Action<SftpNameResponse> _nameAction;
	}
}
