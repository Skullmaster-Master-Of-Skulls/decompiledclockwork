using System;
using System.IO;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200009C RID: 156
	public interface ISourceMap : IDisposable
	{
		// Token: 0x06000958 RID: 2392
		void StartPackage(string sourcePath, string mapPath);

		// Token: 0x06000959 RID: 2393
		void EndPackage();

		// Token: 0x0600095A RID: 2394
		object StartSymbol(AstNode node, int startLine, int startColumn);

		// Token: 0x0600095B RID: 2395
		void MarkSegment(AstNode node, int startLine, int startColumn, string name, Context context);

		// Token: 0x0600095C RID: 2396
		void EndSymbol(object symbol, int endLine, int endColumn, string parentContext);

		// Token: 0x0600095D RID: 2397
		void EndOutputRun(int lineNumber, int columnPosition);

		// Token: 0x0600095E RID: 2398
		void EndFile(TextWriter writer, string newLine);

		// Token: 0x0600095F RID: 2399
		void NewLineInsertedInOutput();

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000960 RID: 2400
		string Name { get; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000961 RID: 2401
		// (set) Token: 0x06000962 RID: 2402
		string SourceRoot { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000963 RID: 2403
		// (set) Token: 0x06000964 RID: 2404
		bool SafeHeader { get; set; }
	}
}
