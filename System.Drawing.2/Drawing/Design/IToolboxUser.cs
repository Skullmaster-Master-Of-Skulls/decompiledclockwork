using System;

namespace System.Drawing.Design
{
	// Token: 0x02000076 RID: 118
	public interface IToolboxUser
	{
		// Token: 0x06000855 RID: 2133
		bool GetToolSupported(ToolboxItem tool);

		// Token: 0x06000856 RID: 2134
		void ToolPicked(ToolboxItem tool);
	}
}
