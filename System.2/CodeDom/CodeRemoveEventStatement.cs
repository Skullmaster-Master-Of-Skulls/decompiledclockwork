using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000651 RID: 1617
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeRemoveEventStatement : CodeStatement
	{
		// Token: 0x06003AB9 RID: 15033 RVA: 0x000F488E File Offset: 0x000F2A8E
		public CodeRemoveEventStatement()
		{
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000F4896 File Offset: 0x000F2A96
		public CodeRemoveEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this.eventRef = eventRef;
			this.listener = listener;
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000F48AC File Offset: 0x000F2AAC
		public CodeRemoveEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener)
		{
			this.eventRef = new CodeEventReferenceExpression(targetObject, eventName);
			this.listener = listener;
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x000F48C8 File Offset: 0x000F2AC8
		// (set) Token: 0x06003ABD RID: 15037 RVA: 0x000F48E3 File Offset: 0x000F2AE3
		public CodeEventReferenceExpression Event
		{
			get
			{
				if (this.eventRef == null)
				{
					this.eventRef = new CodeEventReferenceExpression();
				}
				return this.eventRef;
			}
			set
			{
				this.eventRef = value;
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06003ABE RID: 15038 RVA: 0x000F48EC File Offset: 0x000F2AEC
		// (set) Token: 0x06003ABF RID: 15039 RVA: 0x000F48F4 File Offset: 0x000F2AF4
		public CodeExpression Listener
		{
			get
			{
				return this.listener;
			}
			set
			{
				this.listener = value;
			}
		}

		// Token: 0x04002C1D RID: 11293
		private CodeEventReferenceExpression eventRef;

		// Token: 0x04002C1E RID: 11294
		private CodeExpression listener;
	}
}
