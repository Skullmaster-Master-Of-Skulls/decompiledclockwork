using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000B6 RID: 182
	internal class WindowChangeRequestInfo : RequestInfo
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0001EC98 File Offset: 0x0001CE98
		public override string RequestName
		{
			get
			{
				return "window-change";
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0001EC9F File Offset: 0x0001CE9F
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0001ECA7 File Offset: 0x0001CEA7
		public uint Columns { get; private set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0001ECB0 File Offset: 0x0001CEB0
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x0001ECB8 File Offset: 0x0001CEB8
		public uint Rows { get; private set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0001ECC1 File Offset: 0x0001CEC1
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x0001ECC9 File Offset: 0x0001CEC9
		public uint Width { get; private set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001ECD2 File Offset: 0x0001CED2
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0001ECDA File Offset: 0x0001CEDA
		public uint Height { get; private set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001ECE3 File Offset: 0x0001CEE3
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + 4 + 4 + 4;
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001E69A File Offset: 0x0001C89A
		public WindowChangeRequestInfo()
		{
			base.WantReply = false;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001ECF3 File Offset: 0x0001CEF3
		public WindowChangeRequestInfo(uint columns, uint rows, uint width, uint height) : this()
		{
			this.Columns = columns;
			this.Rows = rows;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001ED18 File Offset: 0x0001CF18
		protected override void LoadData()
		{
			base.LoadData();
			this.Columns = base.ReadUInt32();
			this.Rows = base.ReadUInt32();
			this.Width = base.ReadUInt32();
			this.Height = base.ReadUInt32();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001ED50 File Offset: 0x0001CF50
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.Columns);
			base.Write(this.Rows);
			base.Write(this.Width);
			base.Write(this.Height);
		}

		// Token: 0x04000349 RID: 841
		public const string Name = "window-change";
	}
}
