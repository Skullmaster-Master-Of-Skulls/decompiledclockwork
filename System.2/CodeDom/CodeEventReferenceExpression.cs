using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000633 RID: 1587
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeEventReferenceExpression : CodeExpression
	{
		// Token: 0x060039D2 RID: 14802 RVA: 0x000F3538 File Offset: 0x000F1738
		public CodeEventReferenceExpression()
		{
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000F3540 File Offset: 0x000F1740
		public CodeEventReferenceExpression(CodeExpression targetObject, string eventName)
		{
			this.targetObject = targetObject;
			this.eventName = eventName;
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x000F3556 File Offset: 0x000F1756
		// (set) Token: 0x060039D5 RID: 14805 RVA: 0x000F355E File Offset: 0x000F175E
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

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000F3567 File Offset: 0x000F1767
		// (set) Token: 0x060039D7 RID: 14807 RVA: 0x000F357D File Offset: 0x000F177D
		public string EventName
		{
			get
			{
				if (this.eventName != null)
				{
					return this.eventName;
				}
				return string.Empty;
			}
			set
			{
				this.eventName = value;
			}
		}

		// Token: 0x04002BCB RID: 11211
		private CodeExpression targetObject;

		// Token: 0x04002BCC RID: 11212
		private string eventName;
	}
}
