using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000637 RID: 1591
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeFieldReferenceExpression : CodeExpression
	{
		// Token: 0x060039EA RID: 14826 RVA: 0x000F36C4 File Offset: 0x000F18C4
		public CodeFieldReferenceExpression()
		{
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x000F36CC File Offset: 0x000F18CC
		public CodeFieldReferenceExpression(CodeExpression targetObject, string fieldName)
		{
			this.TargetObject = targetObject;
			this.FieldName = fieldName;
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000F36E2 File Offset: 0x000F18E2
		// (set) Token: 0x060039ED RID: 14829 RVA: 0x000F36EA File Offset: 0x000F18EA
		public CodeExpression TargetObject
		{
			get
			{
				return this.targetObject;
			}
			set
			{
				this.targetObject = value;
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x060039EE RID: 14830 RVA: 0x000F36F3 File Offset: 0x000F18F3
		// (set) Token: 0x060039EF RID: 14831 RVA: 0x000F3709 File Offset: 0x000F1909
		public string FieldName
		{
			get
			{
				if (this.fieldName != null)
				{
					return this.fieldName;
				}
				return string.Empty;
			}
			set
			{
				this.fieldName = value;
			}
		}

		// Token: 0x04002BCE RID: 11214
		private CodeExpression targetObject;

		// Token: 0x04002BCF RID: 11215
		private string fieldName;
	}
}
