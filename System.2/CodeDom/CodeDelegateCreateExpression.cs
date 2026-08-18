using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200062D RID: 1581
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDelegateCreateExpression : CodeExpression
	{
		// Token: 0x060039AF RID: 14767 RVA: 0x000F32ED File Offset: 0x000F14ED
		public CodeDelegateCreateExpression()
		{
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000F32F5 File Offset: 0x000F14F5
		public CodeDelegateCreateExpression(CodeTypeReference delegateType, CodeExpression targetObject, string methodName)
		{
			this.delegateType = delegateType;
			this.targetObject = targetObject;
			this.methodName = methodName;
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000F3312 File Offset: 0x000F1512
		// (set) Token: 0x060039B2 RID: 14770 RVA: 0x000F3332 File Offset: 0x000F1532
		public CodeTypeReference DelegateType
		{
			get
			{
				if (this.delegateType == null)
				{
					this.delegateType = new CodeTypeReference("");
				}
				return this.delegateType;
			}
			set
			{
				this.delegateType = value;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000F333B File Offset: 0x000F153B
		// (set) Token: 0x060039B4 RID: 14772 RVA: 0x000F3343 File Offset: 0x000F1543
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

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x000F334C File Offset: 0x000F154C
		// (set) Token: 0x060039B6 RID: 14774 RVA: 0x000F3362 File Offset: 0x000F1562
		public string MethodName
		{
			get
			{
				if (this.methodName != null)
				{
					return this.methodName;
				}
				return string.Empty;
			}
			set
			{
				this.methodName = value;
			}
		}

		// Token: 0x04002BC4 RID: 11204
		private CodeTypeReference delegateType;

		// Token: 0x04002BC5 RID: 11205
		private CodeExpression targetObject;

		// Token: 0x04002BC6 RID: 11206
		private string methodName;
	}
}
