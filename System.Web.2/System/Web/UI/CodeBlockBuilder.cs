using System;

namespace System.Web.UI
{
	// Token: 0x02000255 RID: 597
	internal class CodeBlockBuilder : ControlBuilder, ICodeBlockTypeAccessor
	{
		// Token: 0x06001BA0 RID: 7072 RVA: 0x0005732A File Offset: 0x0005552A
		internal CodeBlockBuilder(CodeBlockType blockType, string content, int lineNumber, int column, VirtualPath virtualPath, bool encode)
		{
			this._content = content;
			this._blockType = blockType;
			this._column = column;
			this.IsEncoded = encode;
			base.Line = lineNumber;
			base.VirtualPath = virtualPath;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0005735F File Offset: 0x0005555F
		internal CodeBlockBuilder(CodeBlockType blockType, string content, int lineNumber, int column, VirtualPath virtualPath) : this(blockType, content, lineNumber, column, virtualPath, false)
		{
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object BuildObject()
		{
			return null;
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x0005736F File Offset: 0x0005556F
		internal string Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00057377 File Offset: 0x00055577
		public CodeBlockType BlockType
		{
			get
			{
				return this._blockType;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0005737F File Offset: 0x0005557F
		internal int Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x00057387 File Offset: 0x00055587
		// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x0005738F File Offset: 0x0005558F
		internal bool IsEncoded { get; private set; }

		// Token: 0x040018C5 RID: 6341
		protected CodeBlockType _blockType;

		// Token: 0x040018C6 RID: 6342
		protected string _content;

		// Token: 0x040018C7 RID: 6343
		private int _column;
	}
}
