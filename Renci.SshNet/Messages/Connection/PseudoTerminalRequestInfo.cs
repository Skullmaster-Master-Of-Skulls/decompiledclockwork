using System;
using System.Collections.Generic;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000B1 RID: 177
	internal class PseudoTerminalRequestInfo : RequestInfo
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		public override string RequestName
		{
			get
			{
				return "pty-req";
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0001E9DF File Offset: 0x0001CBDF
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x0001E9E7 File Offset: 0x0001CBE7
		public string EnvironmentVariable { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x0001E9F8 File Offset: 0x0001CBF8
		public uint Columns { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0001EA01 File Offset: 0x0001CC01
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x0001EA09 File Offset: 0x0001CC09
		public uint Rows { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0001EA12 File Offset: 0x0001CC12
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0001EA1A File Offset: 0x0001CC1A
		public uint PixelWidth { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001EA23 File Offset: 0x0001CC23
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x0001EA2B File Offset: 0x0001CC2B
		public uint PixelHeight { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0001EA34 File Offset: 0x0001CC34
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x0001EA3C File Offset: 0x0001CC3C
		public IDictionary<TerminalModes, uint> TerminalModeValues { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0001EA45 File Offset: 0x0001CC45
		protected override int BufferCapacity
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001E57E File Offset: 0x0001C77E
		public PseudoTerminalRequestInfo()
		{
			base.WantReply = true;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001EA48 File Offset: 0x0001CC48
		public PseudoTerminalRequestInfo(string environmentVariable, uint columns, uint rows, uint width, uint height, IDictionary<TerminalModes, uint> terminalModeValues) : this()
		{
			this.EnvironmentVariable = environmentVariable;
			this.Columns = columns;
			this.Rows = rows;
			this.PixelWidth = width;
			this.PixelHeight = height;
			this.TerminalModeValues = terminalModeValues;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001EA80 File Offset: 0x0001CC80
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.EnvironmentVariable);
			base.Write(this.Columns);
			base.Write(this.Rows);
			base.Write(this.PixelWidth);
			base.Write(this.PixelHeight);
			if (this.TerminalModeValues != null && this.TerminalModeValues.Count > 0)
			{
				base.Write((uint)(this.TerminalModeValues.Count * 5 + 1));
				foreach (KeyValuePair<TerminalModes, uint> keyValuePair in this.TerminalModeValues)
				{
					base.Write((byte)keyValuePair.Key);
					base.Write(keyValuePair.Value);
				}
				base.Write(0);
				return;
			}
			base.Write(0U);
		}

		// Token: 0x0400033C RID: 828
		public const string Name = "pty-req";
	}
}
