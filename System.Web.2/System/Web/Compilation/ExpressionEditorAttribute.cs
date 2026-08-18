using System;

namespace System.Web.Compilation
{
	// Token: 0x0200083C RID: 2108
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ExpressionEditorAttribute : Attribute
	{
		// Token: 0x0600648F RID: 25743 RVA: 0x0016088F File Offset: 0x0015EA8F
		public ExpressionEditorAttribute(Type type) : this((type != null) ? type.AssemblyQualifiedName : null)
		{
		}

		// Token: 0x06006490 RID: 25744 RVA: 0x001608A9 File Offset: 0x0015EAA9
		public ExpressionEditorAttribute(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw new ArgumentNullException("typeName");
			}
			this._editorTypeName = typeName;
		}

		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x06006491 RID: 25745 RVA: 0x001608CB File Offset: 0x0015EACB
		public string EditorTypeName
		{
			get
			{
				return this._editorTypeName;
			}
		}

		// Token: 0x06006492 RID: 25746 RVA: 0x001608D4 File Offset: 0x0015EAD4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ExpressionEditorAttribute expressionEditorAttribute = obj as ExpressionEditorAttribute;
			return expressionEditorAttribute != null && expressionEditorAttribute.EditorTypeName == this.EditorTypeName;
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x00160904 File Offset: 0x0015EB04
		public override int GetHashCode()
		{
			return this.EditorTypeName.GetHashCode();
		}

		// Token: 0x040033E6 RID: 13286
		private string _editorTypeName;
	}
}
