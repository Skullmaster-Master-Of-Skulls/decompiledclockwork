using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000B5 RID: 181
	internal class SubsystemRequestInfo : RequestInfo
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001EC19 File Offset: 0x0001CE19
		public override string RequestName
		{
			get
			{
				return "subsystem";
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001EC20 File Offset: 0x0001CE20
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x0001EC3B File Offset: 0x0001CE3B
		public string SubsystemName
		{
			get
			{
				return SshData.Ascii.GetString(this._subsystemName, 0, this._subsystemName.Length);
			}
			private set
			{
				this._subsystemName = SshData.Ascii.GetBytes(value);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001EC4E File Offset: 0x0001CE4E
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._subsystemName.Length;
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001E57E File Offset: 0x0001C77E
		public SubsystemRequestInfo()
		{
			base.WantReply = true;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001EC61 File Offset: 0x0001CE61
		public SubsystemRequestInfo(string subsystem) : this()
		{
			this.SubsystemName = subsystem;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001EC70 File Offset: 0x0001CE70
		protected override void LoadData()
		{
			base.LoadData();
			this._subsystemName = base.ReadBinary();
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001EC84 File Offset: 0x0001CE84
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._subsystemName);
		}

		// Token: 0x04000347 RID: 839
		private byte[] _subsystemName;

		// Token: 0x04000348 RID: 840
		public const string Name = "subsystem";
	}
}
