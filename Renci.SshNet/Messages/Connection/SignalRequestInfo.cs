using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000B4 RID: 180
	internal class SignalRequestInfo : RequestInfo
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0001EB9A File Offset: 0x0001CD9A
		public override string RequestName
		{
			get
			{
				return "signal";
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0001EBA1 File Offset: 0x0001CDA1
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x0001EBBC File Offset: 0x0001CDBC
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

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0001EBCF File Offset: 0x0001CDCF
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._signalName.Length;
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001E69A File Offset: 0x0001C89A
		public SignalRequestInfo()
		{
			base.WantReply = false;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001EBE2 File Offset: 0x0001CDE2
		public SignalRequestInfo(string signalName) : this()
		{
			this.SignalName = signalName;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001EBF1 File Offset: 0x0001CDF1
		protected override void LoadData()
		{
			base.LoadData();
			this._signalName = base.ReadBinary();
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001EC05 File Offset: 0x0001CE05
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._signalName);
		}

		// Token: 0x04000345 RID: 837
		private byte[] _signalName;

		// Token: 0x04000346 RID: 838
		public const string Name = "signal";
	}
}
