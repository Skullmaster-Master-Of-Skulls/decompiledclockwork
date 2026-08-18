using System;

namespace Spire.Xls.Core
{
	// Token: 0x020002D4 RID: 724
	public interface IMarkersDesigner
	{
		// Token: 0x06002C82 RID: 11394
		void ApplyMarkers();

		// Token: 0x06002C83 RID: 11395
		void ApplyMarkers(UnknownVariableAction action);

		// Token: 0x06002C84 RID: 11396
		void AddVariable(string strName, object variable);

		// Token: 0x06002C85 RID: 11397
		void RemoveVariable(string strName);

		// Token: 0x06002C86 RID: 11398
		bool ContainsVariable(string strName);

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06002C87 RID: 11399
		// (set) Token: 0x06002C88 RID: 11400
		string MarkerPrefix { get; set; }

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06002C89 RID: 11401
		// (set) Token: 0x06002C8A RID: 11402
		char ArgumentSeparator { get; set; }
	}
}
