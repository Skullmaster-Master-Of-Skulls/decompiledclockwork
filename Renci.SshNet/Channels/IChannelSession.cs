using System;
using System.Collections.Generic;
using Renci.SshNet.Common;

namespace Renci.SshNet.Channels
{
	// Token: 0x0200010C RID: 268
	internal interface IChannelSession : IChannel, IDisposable
	{
		// Token: 0x06000B60 RID: 2912
		void Open();

		// Token: 0x06000B61 RID: 2913
		bool SendPseudoTerminalRequest(string environmentVariable, uint columns, uint rows, uint width, uint height, IDictionary<TerminalModes, uint> terminalModeValues);

		// Token: 0x06000B62 RID: 2914
		bool SendX11ForwardingRequest(bool isSingleConnection, string protocol, byte[] cookie, uint screenNumber);

		// Token: 0x06000B63 RID: 2915
		bool SendEnvironmentVariableRequest(string variableName, string variableValue);

		// Token: 0x06000B64 RID: 2916
		bool SendShellRequest();

		// Token: 0x06000B65 RID: 2917
		bool SendExecRequest(string command);

		// Token: 0x06000B66 RID: 2918
		bool SendBreakRequest(uint breakLength);

		// Token: 0x06000B67 RID: 2919
		bool SendSubsystemRequest(string subsystem);

		// Token: 0x06000B68 RID: 2920
		bool SendWindowChangeRequest(uint columns, uint rows, uint width, uint height);

		// Token: 0x06000B69 RID: 2921
		bool SendLocalFlowRequest(bool clientCanDo);

		// Token: 0x06000B6A RID: 2922
		bool SendSignalRequest(string signalName);

		// Token: 0x06000B6B RID: 2923
		bool SendExitStatusRequest(uint exitStatus);

		// Token: 0x06000B6C RID: 2924
		bool SendExitSignalRequest(string signalName, bool coreDumped, string errorMessage, string language);

		// Token: 0x06000B6D RID: 2925
		bool SendEndOfWriteRequest();

		// Token: 0x06000B6E RID: 2926
		bool SendKeepAliveRequest();
	}
}
