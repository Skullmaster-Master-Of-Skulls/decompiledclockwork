using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200061A RID: 1562
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeAttachEventStatement : CodeStatement
	{
		// Token: 0x06003921 RID: 14625 RVA: 0x000F28AF File Offset: 0x000F0AAF
		public CodeAttachEventStatement()
		{
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000F28B7 File Offset: 0x000F0AB7
		public CodeAttachEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this.eventRef = eventRef;
			this.listener = listener;
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000F28CD File Offset: 0x000F0ACD
		public CodeAttachEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener)
		{
			this.eventRef = new CodeEventReferenceExpression(targetObject, eventName);
			this.listener = listener;
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x000F28E9 File Offset: 0x000F0AE9
		// (set) Token: 0x06003925 RID: 14629 RVA: 0x000F28FF File Offset: 0x000F0AFF
		public CodeEventReferenceExpression Event
		{
			get
			{
				if (this.eventRef == null)
				{
					return new CodeEventReferenceExpression();
				}
				return this.eventRef;
			}
			set
			{
				this.eventRef = value;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06003926 RID: 14630 RVA: 0x000F2908 File Offset: 0x000F0B08
		// (set) Token: 0x06003927 RID: 14631 RVA: 0x000F2910 File Offset: 0x000F0B10
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

		// Token: 0x04002B92 RID: 11154
		private CodeEventReferenceExpression eventRef;

		// Token: 0x04002B93 RID: 11155
		private CodeExpression listener;
	}
}
