using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	// Token: 0x02000665 RID: 1637
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeReference : CodeObject
	{
		// Token: 0x06003B4D RID: 15181 RVA: 0x000F54EC File Offset: 0x000F36EC
		public CodeTypeReference()
		{
			this.baseType = string.Empty;
			this.arrayRank = 0;
			this.arrayElementType = null;
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000F5510 File Offset: 0x000F3710
		public CodeTypeReference(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsArray)
			{
				this.arrayRank = type.GetArrayRank();
				this.arrayElementType = new CodeTypeReference(type.GetElementType());
				this.baseType = null;
			}
			else
			{
				this.InitializeFromType(type);
				this.arrayRank = 0;
				this.arrayElementType = null;
			}
			this.isInterface = type.IsInterface;
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x000F5586 File Offset: 0x000F3786
		public CodeTypeReference(Type type, CodeTypeReferenceOptions codeTypeReferenceOption) : this(type)
		{
			this.referenceOptions = codeTypeReferenceOption;
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x000F5596 File Offset: 0x000F3796
		public CodeTypeReference(string typeName, CodeTypeReferenceOptions codeTypeReferenceOption)
		{
			this.Initialize(typeName, codeTypeReferenceOption);
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000F55A6 File Offset: 0x000F37A6
		public CodeTypeReference(string typeName)
		{
			this.Initialize(typeName);
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000F55B8 File Offset: 0x000F37B8
		private void InitializeFromType(Type type)
		{
			this.baseType = type.Name;
			if (!type.IsGenericParameter)
			{
				Type type2 = type;
				while (type2.IsNested)
				{
					type2 = type2.DeclaringType;
					this.baseType = type2.Name + "+" + this.baseType;
				}
				if (!string.IsNullOrEmpty(type.Namespace))
				{
					this.baseType = type.Namespace + "." + this.baseType;
				}
			}
			if (type.IsGenericType && !type.ContainsGenericParameters)
			{
				Type[] genericArguments = type.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					this.TypeArguments.Add(new CodeTypeReference(genericArguments[i]));
				}
				return;
			}
			if (!type.IsGenericTypeDefinition)
			{
				this.needsFixup = true;
			}
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x000F567A File Offset: 0x000F387A
		private void Initialize(string typeName)
		{
			this.Initialize(typeName, this.referenceOptions);
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x000F568C File Offset: 0x000F388C
		private void Initialize(string typeName, CodeTypeReferenceOptions options)
		{
			this.Options = options;
			if (typeName == null || typeName.Length == 0)
			{
				typeName = typeof(void).FullName;
				this.baseType = typeName;
				this.arrayRank = 0;
				this.arrayElementType = null;
				return;
			}
			typeName = this.RipOffAssemblyInformationFromTypeName(typeName);
			int num = typeName.Length - 1;
			int i = num;
			this.needsFixup = true;
			Queue queue = new Queue();
			while (i >= 0)
			{
				int num2 = 1;
				if (typeName[i--] != ']')
				{
					break;
				}
				while (i >= 0 && typeName[i] == ',')
				{
					num2++;
					i--;
				}
				if (i < 0 || typeName[i] != '[')
				{
					break;
				}
				queue.Enqueue(num2);
				i--;
				num = i;
			}
			i = num;
			ArrayList arrayList = new ArrayList();
			Stack stack = new Stack();
			if (i > 0 && typeName[i--] == ']')
			{
				this.needsFixup = false;
				int num3 = 1;
				int num4 = num;
				while (i >= 0)
				{
					if (typeName[i] == '[')
					{
						if (--num3 == 0)
						{
							break;
						}
					}
					else if (typeName[i] == ']')
					{
						num3++;
					}
					else if (typeName[i] == ',' && num3 == 1)
					{
						if (i + 1 < num4)
						{
							stack.Push(typeName.Substring(i + 1, num4 - i - 1));
						}
						num4 = i;
					}
					i--;
				}
				if (i > 0 && num - i - 1 > 0)
				{
					if (i + 1 < num4)
					{
						stack.Push(typeName.Substring(i + 1, num4 - i - 1));
					}
					while (stack.Count > 0)
					{
						string typeName2 = this.RipOffAssemblyInformationFromTypeName((string)stack.Pop());
						arrayList.Add(new CodeTypeReference(typeName2));
					}
					num = i - 1;
				}
			}
			if (num < 0)
			{
				this.baseType = typeName;
				return;
			}
			if (queue.Count > 0)
			{
				CodeTypeReference codeTypeReference = new CodeTypeReference(typeName.Substring(0, num + 1), this.Options);
				for (int j = 0; j < arrayList.Count; j++)
				{
					codeTypeReference.TypeArguments.Add((CodeTypeReference)arrayList[j]);
				}
				while (queue.Count > 1)
				{
					codeTypeReference = new CodeTypeReference(codeTypeReference, (int)queue.Dequeue());
				}
				this.baseType = null;
				this.arrayRank = (int)queue.Dequeue();
				this.arrayElementType = codeTypeReference;
			}
			else if (arrayList.Count > 0)
			{
				for (int k = 0; k < arrayList.Count; k++)
				{
					this.TypeArguments.Add((CodeTypeReference)arrayList[k]);
				}
				this.baseType = typeName.Substring(0, num + 1);
			}
			else
			{
				this.baseType = typeName;
			}
			if (this.baseType != null && this.baseType.IndexOf('`') != -1)
			{
				this.needsFixup = false;
			}
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000F5949 File Offset: 0x000F3B49
		public CodeTypeReference(string typeName, params CodeTypeReference[] typeArguments) : this(typeName)
		{
			if (typeArguments != null && typeArguments.Length != 0)
			{
				this.TypeArguments.AddRange(typeArguments);
			}
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x000F5965 File Offset: 0x000F3B65
		public CodeTypeReference(CodeTypeParameter typeParameter) : this((typeParameter == null) ? null : typeParameter.Name)
		{
			this.referenceOptions = CodeTypeReferenceOptions.GenericTypeParameter;
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x000F5980 File Offset: 0x000F3B80
		public CodeTypeReference(string baseType, int rank)
		{
			this.baseType = null;
			this.arrayRank = rank;
			this.arrayElementType = new CodeTypeReference(baseType);
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000F59A2 File Offset: 0x000F3BA2
		public CodeTypeReference(CodeTypeReference arrayType, int rank)
		{
			this.baseType = null;
			this.arrayRank = rank;
			this.arrayElementType = arrayType;
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06003B59 RID: 15193 RVA: 0x000F59BF File Offset: 0x000F3BBF
		// (set) Token: 0x06003B5A RID: 15194 RVA: 0x000F59C7 File Offset: 0x000F3BC7
		public CodeTypeReference ArrayElementType
		{
			get
			{
				return this.arrayElementType;
			}
			set
			{
				this.arrayElementType = value;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x000F59D0 File Offset: 0x000F3BD0
		// (set) Token: 0x06003B5C RID: 15196 RVA: 0x000F59D8 File Offset: 0x000F3BD8
		public int ArrayRank
		{
			get
			{
				return this.arrayRank;
			}
			set
			{
				this.arrayRank = value;
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x000F59E1 File Offset: 0x000F3BE1
		internal int NestedArrayDepth
		{
			get
			{
				if (this.arrayElementType == null)
				{
					return 0;
				}
				return 1 + this.arrayElementType.NestedArrayDepth;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06003B5E RID: 15198 RVA: 0x000F59FC File Offset: 0x000F3BFC
		// (set) Token: 0x06003B5F RID: 15199 RVA: 0x000F5A7B File Offset: 0x000F3C7B
		public string BaseType
		{
			get
			{
				if (this.arrayRank > 0 && this.arrayElementType != null)
				{
					return this.arrayElementType.BaseType;
				}
				if (string.IsNullOrEmpty(this.baseType))
				{
					return string.Empty;
				}
				string text = this.baseType;
				if (this.needsFixup && this.TypeArguments.Count > 0)
				{
					text = text + "`" + this.TypeArguments.Count.ToString(CultureInfo.InvariantCulture);
				}
				return text;
			}
			set
			{
				this.baseType = value;
				this.Initialize(this.baseType);
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06003B60 RID: 15200 RVA: 0x000F5A90 File Offset: 0x000F3C90
		// (set) Token: 0x06003B61 RID: 15201 RVA: 0x000F5A98 File Offset: 0x000F3C98
		[ComVisible(false)]
		public CodeTypeReferenceOptions Options
		{
			get
			{
				return this.referenceOptions;
			}
			set
			{
				this.referenceOptions = value;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06003B62 RID: 15202 RVA: 0x000F5AA1 File Offset: 0x000F3CA1
		[ComVisible(false)]
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				if (this.arrayRank > 0 && this.arrayElementType != null)
				{
					return this.arrayElementType.TypeArguments;
				}
				if (this.typeArguments == null)
				{
					this.typeArguments = new CodeTypeReferenceCollection();
				}
				return this.typeArguments;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x000F5AD9 File Offset: 0x000F3CD9
		internal bool IsInterface
		{
			get
			{
				return this.isInterface;
			}
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x000F5AE4 File Offset: 0x000F3CE4
		private string RipOffAssemblyInformationFromTypeName(string typeName)
		{
			int i = 0;
			int num = typeName.Length - 1;
			string result = typeName;
			while (i < typeName.Length)
			{
				if (!char.IsWhiteSpace(typeName[i]))
				{
					break;
				}
				i++;
			}
			while (num >= 0 && char.IsWhiteSpace(typeName[num]))
			{
				num--;
			}
			if (i < num)
			{
				if (typeName[i] == '[' && typeName[num] == ']')
				{
					i++;
					num--;
				}
				if (typeName[num] != ']')
				{
					int num2 = 0;
					for (int j = num; j >= i; j--)
					{
						if (typeName[j] == ',')
						{
							num2++;
							if (num2 == 4)
							{
								result = typeName.Substring(i, j - i);
								break;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04002C48 RID: 11336
		private string baseType;

		// Token: 0x04002C49 RID: 11337
		[OptionalField]
		private bool isInterface;

		// Token: 0x04002C4A RID: 11338
		private int arrayRank;

		// Token: 0x04002C4B RID: 11339
		private CodeTypeReference arrayElementType;

		// Token: 0x04002C4C RID: 11340
		[OptionalField]
		private CodeTypeReferenceCollection typeArguments;

		// Token: 0x04002C4D RID: 11341
		[OptionalField]
		private CodeTypeReferenceOptions referenceOptions;

		// Token: 0x04002C4E RID: 11342
		[OptionalField]
		private bool needsFixup;
	}
}
