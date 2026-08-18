using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebGrease.Css.Ast.MediaQuery;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x0200011D RID: 285
	public sealed class ImportNode : AstNode
	{
		// Token: 0x06001162 RID: 4450 RVA: 0x0004C4DB File Offset: 0x0004A6DB
		public ImportNode(AllowedImportData allowedImportDataType, string importDataValue, ReadOnlyCollection<MediaQueryNode> mediaQueries)
		{
			this.AllowedImportDataType = allowedImportDataType;
			this.ImportDataValue = importDataValue;
			this.MediaQueries = (mediaQueries ?? new List<MediaQueryNode>(0).AsReadOnly());
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0004C507 File Offset: 0x0004A707
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x0004C50F File Offset: 0x0004A70F
		public AllowedImportData AllowedImportDataType { get; private set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0004C518 File Offset: 0x0004A718
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x0004C520 File Offset: 0x0004A720
		public string ImportDataValue { get; private set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0004C529 File Offset: 0x0004A729
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x0004C531 File Offset: 0x0004A731
		public ReadOnlyCollection<MediaQueryNode> MediaQueries { get; private set; }

		// Token: 0x06001169 RID: 4457 RVA: 0x0004C53A File Offset: 0x0004A73A
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitImportNode(this);
		}
	}
}
