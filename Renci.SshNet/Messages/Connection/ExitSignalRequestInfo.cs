using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000AE RID: 174
	internal class ExitSignalRequestInfo : RequestInfo
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001E820 File Offset: 0x0001CA20
		public override string RequestName
		{
			get
			{
				return "exit-signal";
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0001E827 File Offset: 0x0001CA27
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x0001E842 File Offset: 0x0001CA42
		public string SignalName
		{
			get
			{
				return SshData.Ascii.GetString(this._signalName, 0, this._signalName.Length);
			}
			private set
			{
				this._signalName = SshData.Ascii.GetBytes(value);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001E855 File Offset: 0x0001CA55
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x0001E85D File Offset: 0x0001CA5D
		public bool CoreDumped { get; private set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001E866 File Offset: 0x0001CA66
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x0001E881 File Offset: 0x0001CA81
		public string ErrorMessage
		{
			get
			{
				return SshData.Utf8.GetString(this._errorMessage, 0, this._errorMessage.Length);
			}
			private set
			{
				this._errorMessage = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0001E894 File Offset: 0x0001CA94
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x0001E8AF File Offset: 0x0001CAAF
		public string Language
		{
			get
			{
				return SshData.Utf8.GetString(this._language, 0, this._language.Length);
			}
			private set
			{
				this._language = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0001E8C2 File Offset: 0x0001CAC2
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._signalName.Length + 1 + 4 + this._errorMessage.Length + 4 + this._language.Length;
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001E69A File Offset: 0x0001C89A
		public ExitSignalRequestInfo()
		{
			base.WantReply = false;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001E8ED File Offset: 0x0001CAED
		public ExitSignalRequestInfo(string signalName, bool coreDumped, string errorMessage, string language) : this()
		{
			this.SignalName = signalName;
			this.CoreDumped = coreDumped;
			this.ErrorMessage = errorMessage;
			this.Language = language;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001E912 File Offset: 0x0001CB12
		protected override void LoadData()
		{
			base.LoadData();
			this._signalName = base.ReadBinary();
			this.CoreDumped = base.ReadBoolean();
			this._errorMessage = base.ReadBinary();
			this._language = base.ReadBinary();
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001E94A File Offset: 0x0001CB4A
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._signalName);
			base.Write(this.CoreDumped);
			base.Write(this._errorMessage);
			base.Write(this._language);
		}

		// Token: 0x04000334 RID: 820
		private byte[] _signalName;

		// Token: 0x04000335 RID: 821
		private byte[] _errorMessage;

		// Token: 0x04000336 RID: 822
		private byte[] _language;

		// Token: 0x04000337 RID: 823
		public const string Name = "exit-signal";
	}
}
