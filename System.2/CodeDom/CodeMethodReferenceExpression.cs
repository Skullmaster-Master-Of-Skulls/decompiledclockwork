using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	// Token: 0x02000642 RID: 1602
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMethodReferenceExpression : CodeExpression
	{
		// Token: 0x06003A40 RID: 14912 RVA: 0x000F3DF8 File Offset: 0x000F1FF8
		public CodeMethodReferenceExpression()
		{
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000F3E00 File Offset: 0x000F2000
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x000F3E16 File Offset: 0x000F2016
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName, params CodeTypeReference[] typeParameters)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
			if (typeParameters != null && typeParameters.Length != 0)
			{
				this.TypeArguments.AddRange(typeParameters);
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003A43 RID: 14915 RVA: 0x000F3E3F File Offset: 0x000F203F
		// (set) Token: 0x06003A44 RID: 14916 RVA: 0x000F3E47 File Offset: 0x000F2047
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

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06003A45 RID: 14917 RVA: 0x000F3E50 File Offset: 0x000F2050
		// (set) Token: 0x06003A46 RID: 14918 RVA: 0x000F3E66 File Offset: 0x000F2066
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

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x000F3E6F File Offset: 0x000F206F
		[ComVisible(false)]
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				if (this.typeArguments == null)
				{
					this.typeArguments = new CodeTypeReferenceCollection();
				}
				return this.typeArguments;
			}
		}

		// Token: 0x04002BF8 RID: 11256
		private CodeExpression targetObject;

		// Token: 0x04002BF9 RID: 11257
		private string methodName;

		// Token: 0x04002BFA RID: 11258
		[OptionalField]
		private CodeTypeReferenceCollection typeArguments;
	}
}
