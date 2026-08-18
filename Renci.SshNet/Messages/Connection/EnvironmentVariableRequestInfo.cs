using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000AC RID: 172
	internal class EnvironmentVariableRequestInfo : RequestInfo
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001E6A9 File Offset: 0x0001C8A9
		public override string RequestName
		{
			get
			{
				return "env";
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001E6B0 File Offset: 0x0001C8B0
		public string VariableName
		{
			get
			{
				return SshData.Utf8.GetString(this._variableName, 0, this._variableName.Length);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0001E6CB File Offset: 0x0001C8CB
		public string VariableValue
		{
			get
			{
				return SshData.Utf8.GetString(this._variableValue, 0, this._variableValue.Length);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001E6E6 File Offset: 0x0001C8E6
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._variableName.Length + 4 + this._variableValue.Length;
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001E57E File Offset: 0x0001C77E
		public EnvironmentVariableRequestInfo()
		{
			base.WantReply = true;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001E704 File Offset: 0x0001C904
		public EnvironmentVariableRequestInfo(string variableName, string variableValue) : this()
		{
			this._variableName = SshData.Utf8.GetBytes(variableName);
			this._variableValue = SshData.Utf8.GetBytes(variableValue);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001E72E File Offset: 0x0001C92E
		protected override void LoadData()
		{
			base.LoadData();
			this._variableName = base.ReadBinary();
			this._variableValue = base.ReadBinary();
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001E74E File Offset: 0x0001C94E
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._variableName);
			base.WriteBinaryString(this._variableValue);
		}

		// Token: 0x0400032E RID: 814
		private byte[] _variableName;

		// Token: 0x0400032F RID: 815
		private byte[] _variableValue;

		// Token: 0x04000330 RID: 816
		public const string Name = "env";
	}
}
